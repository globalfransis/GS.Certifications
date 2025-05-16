using AutoMapper;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.GSFExtensions.GSFWebFilteTransferService;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Services;
using GS.Certifications.Domain.Entities.Certificaciones.Documentos;
using GSF.Application.Extensions.GSFMediatR;
using GSFSharedResources;
using GSFWebFileTransferService.Abstractions.Builder;
using GSFWebFileTransferService.Abstractions.DefaultValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Serilog;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands;

public class AnalyzeDocumentoSolicitudCertificacionCommand : IRequest<int>
{
    public int Id { get; set; }
    public IFormFile FormFile { get; set; }
    public int? SocioId { get; set; } = null;
    public int? SolicitudId { get; set; } = null;
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

        // --- 2. Análisis del Documento ---
        Log.Logger.Information("Iniciando análisis de documento para SolicitudId={SolicitudId}, FileName={FileName}", request.SolicitudId, request.FormFile.FileName);


        //var result = await certificacionService.AnalyzeAsync(binData, request);
        Log.Logger.Information("Análisis de documento finalizado para SolicitudId={SolicitudId}, FileName={FileName}", request.SolicitudId, request.FormFile.FileName);

        // Esto ya debería estar en la implementación de AnalyzeAsync si la interfaz IComprobanteAnalysisResult lo define
        //result.FileName = request.FormFile.FileName; // setteamos a result el nombre del archivo analizado

        //// --- 3. Datos Adicionales (CompanyExtra) ---
        //var companyExtra = await _context.CompanyExtras
        //                           .AsNoTracking()
        //                           .FirstOrDefaultAsync(c => c.SolicitudId == request.SolicitudId, cancellationToken)
        //                        ?? throw new InvalidOperationException($"No se encontraron datos de configuración (CompanyExtra) para la compañía ID {request.SolicitudId}.");


        //// --- 4. Mapeo: IComprobanteAnalysisResult -> ComprobanteCreate ---
        //ComprobanteCreate comprobanteCreateArgs;
        //try
        //{
        //    comprobanteCreateArgs = await MapAnalysisResultToSaveArgsAsync(result, companyExtra, request.SolicitudId, empresaPortal, request.OrigenId);
        //    Log.Logger.Debug("Mapeo de AnalysisResult a ComprobanteCreate completado para SolicitudId={SolicitudId}", request.SolicitudId);
        //}
        //catch (Exception ex)
        //{
        //    Log.Logger.Error(ex, "Error al mapear resultado del análisis para SolicitudId={SolicitudId}, FileName={FileName}", request.SolicitudId, result.FileName);
        //    throw new InvalidOperationException($"Error al preparar datos del análisis para guardar borrador: {ex.Message}", ex);
        //}

        // --- 5. Validación Manual ---
        //var validationFailures = ValidateComprobanteCreate(comprobanteCreateArgs);
        //if (validationFailures.Any())
        //{
        //    Log.Logger.Warning("Validación manual fallida para borrador de comprobante analizado (SolicitudId={SolicitudId}). Errores: {@ValidationFailures}", request.SolicitudId, validationFailures);
        //    throw new ValidationErrorException(validationFailures);
        //}
        //Log.Logger.Debug("Validación manual completada exitosamente para borrador (SolicitudId={SolicitudId})", request.SolicitudId);


        // --- 6. Borrador ---
        try
        {
            //Log.Logger.Information("Intentando guardar documento analizado como borrador para SolicitudId={SolicitudId}, FileName={FileName}", request.SolicitudId, result.FileName);
            //var borrador = await certificacionService.SaveDraftAsync(comprobanteCreateArgs);

            //Log.Logger.Information("Documento analizado (FileName={FileName}) guardado como borrador Id={BorradorId} para SolicitudId={SolicitudId}", result.FileName, borrador.Id, request.SolicitudId);
            
            var documentoCargado = await _context.DocumentoCargados
                .Include(d => d.Solicitud)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            var folderPath = $"Doc_{documentoCargado.Guid}";
            documentoCargado.ArchivoURL = request.FormFile.FileName;
            documentoCargado.EstadoId = DocumentoEstado.PRESENTADO;
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
}