using AutoMapper;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.GSFExtensions.GSFWebFilteTransferService;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Services;
using GSF.Application.Extensions.GSFMediatR;
using GSFSharedResources;
using GSFWebFileTransferService.Abstractions.Builder;
using GSFWebFileTransferService.Abstractions.DefaultValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands;

public class AnalyzeDocumentoSolicitudCertificacionCommand : IRequest<int>
{
    public int Id { get; set; }
    public IFormFile FormFile { get; set; }
    //public int? SocioId { get; set; } = null;
    public int? SolicitudId { get; set; } = null;
}

public class AnalysisParameters : IDocumentoSolicitudCertificacionAnalysisParameter
{
    public int Id { get; set; }
    //public int SocioId { get; set; }
    public int SolicitudId { get; set; }
}

public class DocumentoSolicitudCertificacionAnalysisParameter : IDocumentoSolicitudCertificacionAnalysisParameter
{
    public int Id { get; set; }
    public IFormFile FormFile { get; set; }
    public int SocioId { get; set; }
    public int SolicitudId { get; set; }
}

public class AnalyzeDocumentoSolicitudCertificacionCommandHandler : BaseRequestHandler<int, AnalyzeDocumentoSolicitudCertificacionCommand, int>
{
    private readonly ICertificacionService certificacionService;
    private readonly ICertificationsDbContext _context;
    private readonly IWebFileTransferServiceBuilder<StorageTypeGSFWFTS, BasicFileConfigurationTypeGSFWFTS> fileTransferServiceBuilder;

    private readonly IStringLocalizer<Shared> _loc;

    public AnalyzeDocumentoSolicitudCertificacionCommandHandler(IStringLocalizer<Shared> loc, ICertificationsDbContext context, ICertificacionService service, IMapper mapper, IWebFileTransferServiceBuilder<StorageTypeGSFWFTS, BasicFileConfigurationTypeGSFWFTS> fileTransferServiceBuilder) : base(mapper)
    {
        this.fileTransferServiceBuilder = fileTransferServiceBuilder;
        _loc = loc;
        _context = context;
        certificacionService = service;
    }

    /// <summary>
    /// Maneja la lógica para analizar un archivo de documento y guardar el resultado como borrador.
    /// </summary>
    protected override async Task<int> HandleRequestAsync(AnalyzeDocumentoSolicitudCertificacionCommand request, CancellationToken cancellationToken)
    {
        // --- 1. Upload y Lectura del Archivo ---
        ArgumentNullException.ThrowIfNull(request.FormFile, nameof(request.FormFile));
        if (request.FormFile.Length == 0)
        {
            throw new ArgumentException("El archivo proporcionado está vacío.", nameof(request.FormFile));
        }

        var fileTransferService = fileTransferServiceBuilder.GetIWebFileTransferService(StorageTypeGSFWFTS.FileSystemStorage, CertificacionesFileConfiguration.DocumentoSolicitudCertificacion);
        var tempFolderPath = $"DocumentoSolicitudCertificacion_{DateTime.Now:yyyyMMddHHmmssfff}"; // Carpeta temporal

        var transferUploadResult = await fileTransferService.UploadTempFile(request.FormFile, tempFolderPath);
        Log.Logger.Information("Archivo {FileName} subido temporalmente a {Path}", request.FormFile.FileName, tempFolderPath);


        byte[] bytes;
        using (var memoryStream = new MemoryStream())
        {
            await request.FormFile.CopyToAsync(memoryStream, cancellationToken);
            bytes = memoryStream.ToArray();
        }
        var binData = new BinaryData(bytes);

        try
        {
            Log.Logger.Information("Iniciando análisis de documento para SolicitudId={SolicitudId}, FileName={FileName}", request.SolicitudId, request.FormFile.FileName);

            var documentoCargado = await certificacionService.GetDocumentoAsync(request.Id);

            var folderPath = $"Doc_{documentoCargado.Guid}";

            var analysisParams = new AnalysisParameters()
            {
                Id = request.Id,
                //SocioId = (int)request.SocioId,
                SolicitudId = (int)request.SolicitudId,
            };

            string operationId = await certificacionService.AnalyzeDocumentoAsync(
                binData,
                analysisParams,
                ProcessAnalysisCompletion, // Referencia al método callback
                cancellationToken
            );

            documentoCargado.OperationId = operationId;
            documentoCargado.OperationStatus = Domain.Commons.Enums.OperationStatus.PROCESSING;
            documentoCargado.ArchivoURL = request.FormFile.FileName;
            documentoCargado.FechaSubida = DateTime.Now;

            string directorioBase = $"Solicitud_{documentoCargado.Solicitud.Guid}/Doc_{documentoCargado.Guid}";

            await fileTransferService.UploadFinalDirectoryFile(request.FormFile, directorioBase);

            await _context.SaveChangesAsync(cancellationToken);

            return documentoCargado.Id;
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "Error inesperado al guardar el comprobante analizado como borrador para SolicitudId={SolicitudId}, FileName={FileName}", request.SolicitudId, request.FormFile.FileName);
            throw;
        }
    }

    private async Task ProcessAnalysisCompletion(AnalysisCompletionData completionData, ICertificationsDbContext dbContext)
    {
        // Volvemos a cargar la entidad usando el DbContext proporcionado para asegurar que está rastreada
        var documento = await dbContext.DocumentoCargados.FindAsync(
            new object[] { completionData.DocumentoId },
            completionData.OriginalCancellationToken
        );

        if (documento == null || documento.OperationId != completionData.OperationId)
        {
            Console.WriteLine($"Callback: Documento {completionData.DocumentoId} no encontrado o OperationId no coincide.");
            return;
        }

        if (completionData.FinalStatus == Domain.Commons.Enums.OperationStatus.COMPLETED && completionData.Result != null)
        {
            documento.FechaDesde = completionData.Result.FechaDesde;
            documento.FechaHasta = completionData.Result.FechaHasta;
            documento.OperationStatus = Domain.Commons.Enums.OperationStatus.COMPLETED;
        }
        else // hubo una falla en la operación de IA
        {
            documento.OperationStatus = Domain.Commons.Enums.OperationStatus.FAILED;
            Console.WriteLine($"Callback: Operación {completionData.OperationId} falló: {completionData.Exception?.Message}");
        }

        await dbContext.SaveChangesAsync(completionData.OriginalCancellationToken);
        Console.WriteLine($"Callback: Documento {documento.Id} actualizado a estado {documento.OperationStatus}.");
    }
}

