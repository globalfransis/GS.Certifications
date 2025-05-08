using AutoMapper;
using FluentValidation.Results;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.GSFExtensions.GSFWebFilteTransferService;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services;
using GSF.Application.Extensions.GSFMediatR;
using GSFSharedResources;
using GSFWebFileTransferService.Abstractions.Builder;
using GSFWebFileTransferService.Abstractions.DefaultValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Serilog;
using GS.Certifications.Domain.Entities.Comprobantes;
using GS.Certifications.Domain.Entities.Empresas;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands;

public class AnalyzeComprobanteCommand : IRequest<int>, IComprobanteAnalysisParameter
{
    public int Id { get; set; }
    public IFormFile FormFile { get; set; }
    public long CompanyId { get; set; }
    public int? EmpresaId { get; set; } = null;
    public short OrigenId { get; set; }

    public ICollection<ModoAnalisis> Modos { get; set; } = new List<ModoAnalisis>() { ModoAnalisis.QR, ModoAnalisis.OCR_DETALLE, ModoAnalisis.OCR_IMPUESTOS, ModoAnalisis.OCR_CABECERA, ModoAnalisis.MANUAL };
}

// TODO: we MUST validate if extracted data of the invoice matches with the current EmpresaPortal!!!!!!!
public class AnalyzeComprobanteCommandHandler : BaseRequestHandler<int, AnalyzeComprobanteCommand, int>
{
    private readonly IComprobanteService comprobanteService;
    private readonly ICertificationsDbContext _context;
    private readonly IWebFileTransferServiceBuilder<StorageTypeGSFWFTS, BasicFileConfigurationTypeGSFWFTS> fileTransferServiceBuilder;

    private readonly IStringLocalizer<Shared> _loc;

    public AnalyzeComprobanteCommandHandler(IStringLocalizer<Shared> loc, ICertificationsDbContext context, IComprobanteService service, IMapper mapper, IWebFileTransferServiceBuilder<StorageTypeGSFWFTS, BasicFileConfigurationTypeGSFWFTS> fileTransferServiceBuilder) : base(mapper)
    {
        this.fileTransferServiceBuilder = fileTransferServiceBuilder;
        _loc = loc;
        _context = context;
        comprobanteService = service;
    }

    /// <summary>
    /// Maneja la lógica para analizar un archivo de comprobante y guardar el resultado como borrador.
    /// </summary>
    protected override async Task<int> HandleRequestAsync(AnalyzeComprobanteCommand request, CancellationToken cancellationToken)
    {
        // --- 1. Upload y Lectura del Archivo ---
        ArgumentNullException.ThrowIfNull(request.FormFile, nameof(request.FormFile));
        if (request.FormFile.Length == 0)
        {
            throw new ArgumentException("El archivo proporcionado está vacío.", nameof(request.FormFile));
        }

        var fileTransferService = fileTransferServiceBuilder.GetIWebFileTransferService(StorageTypeGSFWFTS.FileSystemStorage, ProveedoresFileConfiguration.Comprobantes);
        var folderPath = $"Comprobante_{DateTime.Now:yyyyMMddHHmmssfff}"; // Carpeta temporal

        var transferUploadResult = await fileTransferService.UploadTempFile(request.FormFile, folderPath);
        Log.Logger.Information("Archivo {FileName} subido temporalmente a {Path}", request.FormFile.FileName, folderPath);


        byte[] bytes;
        using (var memoryStream = new MemoryStream())
        {
            await request.FormFile.CopyToAsync(memoryStream, cancellationToken);
            bytes = memoryStream.ToArray();
        }
        var binData = new BinaryData(bytes);

        // --- 2. Análisis del Comprobante ---
        Log.Logger.Information("Iniciando análisis de comprobante para CompanyId={CompanyId}, FileName={FileName}", request.CompanyId, request.FormFile.FileName);

        var empresaPortal = await _context.EmpresasPortales.Include(e => e.ModosLecturas).FirstOrDefaultAsync(e => e.Id == request.EmpresaId);

        request.Modos = empresaPortal.ModosLecturas.Select(e => (ModoAnalisis)e.ModoLecturaIdm).ToList();

        var result = await comprobanteService.AnalyzeAsync(binData, request);
        Log.Logger.Information("Análisis de comprobante finalizado para CompanyId={CompanyId}, FileName={FileName}", request.CompanyId, request.FormFile.FileName);

        // Esto ya debería estar en la implementación de AnalyzeAsync si la interfaz IComprobanteAnalysisResult lo define
        result.FileName = request.FormFile.FileName; // setteamos a result el nombre del archivo analizado

        // --- 3. Datos Adicionales (CompanyExtra) ---
        var companyExtra = await _context.CompanyExtras
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(c => c.CompanyId == request.CompanyId, cancellationToken)
                                ?? throw new InvalidOperationException($"No se encontraron datos de configuración (CompanyExtra) para la compañía ID {request.CompanyId}.");


        // --- 4. Mapeo: IComprobanteAnalysisResult -> ComprobanteCreate ---
        ComprobanteCreate comprobanteCreateArgs;
        try
        {
            comprobanteCreateArgs = await MapAnalysisResultToSaveArgsAsync(result, companyExtra, request.CompanyId, empresaPortal, request.OrigenId);
            Log.Logger.Debug("Mapeo de AnalysisResult a ComprobanteCreate completado para CompanyId={CompanyId}", request.CompanyId);
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "Error al mapear resultado del análisis para CompanyId={CompanyId}, FileName={FileName}", request.CompanyId, result.FileName);
            throw new InvalidOperationException($"Error al preparar datos del análisis para guardar borrador: {ex.Message}", ex);
        }

        // --- 5. Validación Manual ---
        //var validationFailures = ValidateComprobanteCreate(comprobanteCreateArgs);
        //if (validationFailures.Any())
        //{
        //    Log.Logger.Warning("Validación manual fallida para borrador de comprobante analizado (CompanyId={CompanyId}). Errores: {@ValidationFailures}", request.CompanyId, validationFailures);
        //    throw new ValidationErrorException(validationFailures);
        //}
        //Log.Logger.Debug("Validación manual completada exitosamente para borrador (CompanyId={CompanyId})", request.CompanyId);


        // --- 6. Borrador ---
        try
        {
            Log.Logger.Information("Intentando guardar comprobante analizado como borrador para CompanyId={CompanyId}, FileName={FileName}", request.CompanyId, result.FileName);
            var borrador = await comprobanteService.SaveDraftAsync(comprobanteCreateArgs);

            Log.Logger.Information("Comprobante analizado (FileName={FileName}) guardado como borrador Id={BorradorId} para CompanyId={CompanyId}", result.FileName, borrador.Id, request.CompanyId);

            result.FolderPath = $"Cmp_{borrador.Guid}";

            var finalPath = Path.Combine(result.FolderPath, result.FileName);

            await fileTransferService.UploadFinalDirectoryFile(request.FormFile, finalPath);

            await _context.SaveChangesAsync(cancellationToken);

            return borrador.Id;
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "Error inesperado al guardar el comprobante analizado como borrador para CompanyId={CompanyId}, FileName={FileName}", request.CompanyId, result.FileName);
            throw;
        }
    }

    // ================================================
    // --- Funciones Auxiliares ---
    // ================================================

    /// <summary>
    /// Mapea los datos extraídos del análisis (IComprobanteAnalysisResult)
    /// a la estructura necesaria para guardar un borrador (ComprobanteCreate).
    /// </summary>
    /// <param name="result">El resultado del análisis.</param>
    /// <param name="companyExtra">Datos adicionales de la compañía.</param>
    /// <param name="companyId">ID de la compañía del request.</param>
    /// <param name="empresaPortal">Empresa portal del request.</param>
    /// <returns>Un objeto ComprobanteCreate poblado.</returns>
    /// <exception cref="FormatException">Si algún dato clave no se puede parsear correctamente.</exception>
    /// <exception cref="ArgumentNullException">Si falta algún dato obligatorio inferido (ej: MonedaId).</exception>
    private async Task<ComprobanteCreate> MapAnalysisResultToSaveArgsAsync(IComprobanteAnalysisResult result, CompanyExtra companyExtra, long companyId, EmpresaPortal empresaPortal, short origen)
    {
        // --- Parseo de Datos Clave (Lanzar excepción si falla) ---
        if (!int.TryParse(result.PuntoVenta, NumberStyles.Any, CultureInfo.InvariantCulture, out int puntoVenta))
        {
            // Considerar asignar 0 y dejar que falle en validación, o lanzar ahora
            // Lanzar ahora puede ser más claro sobre dónde ocurrió el problema.
            Log.Logger.Warning("No se pudo parsear PuntoVenta '{PuntoVenta}' a entero.", result.PuntoVenta);
            // throw new FormatException($"El Punto de Venta '{result.PuntoVenta}' extraído del documento no es un número válido.");
            puntoVenta = 0; // Dejar que falle en validación
        }
        if (!int.TryParse(result.Numero, NumberStyles.Any, CultureInfo.InvariantCulture, out int numero))
        {
            Log.Logger.Warning("No se pudo parsear Numero '{Numero}' a entero.", result.Numero);
            //throw new FormatException($"El Número '{result.Numero}' extraído del documento no es un número válido.");
            numero = 0; // Dejar que falle en validación
        }
        if (!DateTime.TryParseExact(result.FechaEmision, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaEmision))
        {
            Log.Logger.Warning("No se pudo parsear FechaEmision '{FechaEmision}' a DateTime (formato yyyy-MM-dd).", result.FechaEmision);
            //throw new FormatException($"La Fecha de Emisión '{result.FechaEmision}' extraída no tiene el formato yyyy-MM-dd.");
            fechaEmision = DateTime.MinValue; // Dejar que falle en validación
        }

        // --- Chequeo de IDs Obligatorios (Nullable en result, no nullable en save) ---
        if (!result.CategoriaTipoEmisorId.HasValue)
        {
            Log.Logger.Warning("CategoriaTipoEmisorId es null en el resultado del análisis.");
            // throw new ArgumentNullException(nameof(result.CategoriaTipoEmisorId), "Falta la Categoría del Emisor en los datos extraídos.");
        }
        if (!result.ComprobanteTipoId.HasValue)
        {
            Log.Logger.Warning("ComprobanteTipoId es null en el resultado del análisis.");
            // throw new ArgumentNullException(nameof(result.ComprobanteTipoId), "Falta el Tipo de Comprobante en los datos extraídos.");
        }
        if (!result.TipoCodigoAutorizacionId.HasValue)
        {
            Log.Logger.Warning("TipoCodigoAutorizacionId es null en el resultado del análisis.");
            // throw new ArgumentNullException(nameof(result.TipoCodigoAutorizacionId), "Falta el Tipo de Código de Autorización en los datos extraídos.");
        }
        if (!result.MonedaId.HasValue)
        {
            Log.Logger.Warning("MonedaId es null en el resultado del análisis.");
            // throw new ArgumentNullException(nameof(result.MonedaId), "Falta la Moneda en los datos extraídos.");
        }

        var saveArgs = new ComprobanteCreate
        {
            // --- Datos Cabecera ---
            NroIdentificacionFiscalPro = string.IsNullOrEmpty(result.NroIdentificacionFiscalPro) ? empresaPortal.IdentificadorTributario : result.NroIdentificacionFiscalPro, // Si la lectura no detectó este campo, tomamos el de la empresa portal
            NroIdentificacionFiscalCompany = string.IsNullOrEmpty(result.NroIdentificacionFiscalCompany) ? companyExtra.IdentificadorTributario : result.NroIdentificacionFiscalCompany,
            //CategoriaTipoEmisorId = result.CategoriaTipoEmisorId ?? 0, // Asignar 0 si es null, validación lo detectará
            ComprobanteTipoId = result.ComprobanteTipoId,     // Asignar 0 si es null, validación lo detectará
            PuntoVenta = puntoVenta,
            Numero = numero,
            Letra = result.Letra,
            FechaEmision = fechaEmision,
            TipoCodigoAutorizacionId = result.TipoCodigoAutorizacionId, // Asignar 0 si es null, validación lo detectará
            CodigoAutorizacion = result.CodigoAutorizacion,
            MonedaId = result.MonedaId,             // Asignar 0 si es null, validación lo detectará

            FechaVencimiento = result.FechaVencimiento,
            FechaVencimientoCodigoAutorizacion = result.FechaVencimientoCodigoAutorizacion,

            // --- Importes ---
            ImporteNeto = result.ImporteNeto ?? 0m, // Usar 0m como default si es null
            ImporteTotal = result.ImporteTotal ?? 0m,
            ImporteIVA = result.ImporteIVA ?? 0m,
            ImporteBonificacion = result.ImporteBonificacion ?? 0m,
            ImportePercepcionIVA = result.ImportePercepcionIVA ?? 0m,
            ImportePercepcionIIBB = result.ImportePercepcionIIBB ?? 0m,
            ImportePercepcionMunicipal = result.ImportePercepcionMunicipal ?? 0m,
            ImporteImpuestosInternos = result.ImporteImpuestosInternos ?? 0m,
            ImporteOtrosTributosProv = result.ImporteOtrosTributosProv ?? 0m,

            EstadoConstatacionQR = result.EstadoConstatacionQR,
            EstadoConstatacion = result.EstadoConstatacion,
            ObservacionesARCAQR = result.ObservacionesARCAQR,
            ObservacionesARCA = result.ObservacionesARCA,

            // --- Datos Adicionales y Compañía ---
            Comentarios = result.Comentarios,
            CodigoHES = result.CodigoHES,
            // EmpresaId: Priorizar el extraído. Si no, el del contexto (si aplica).
            EmpresaId = empresaPortal?.Id,
            //NroIdentificacionFiscalPro = empresaPortal.IdentificadorTributario,
            CategoriaTipoEmisorId = empresaPortal.TipoResponsableId,
            CompanyId = companyId,
            // Datos de CompanyExtra
            CategoriaTipoReceptorId = companyExtra.TipoResponsableId,
            //NroIdentificacionFiscalCompany = companyExtra.IdentificadorTributario,

            // --- QR ---
            ValidacionQR = result.ValidacionQR,
            QRValor = result.QRValor,

            // --- Otros ---
            NroOrdenCompra = result.NroOrdenCompra,
            NroRemito = result.NroRemito,
            CondicionVentaId = result.CondicionVentaId,

            FileName = result.FileName,

            OrigenId = origen,
            // al momento de crear el comprobante, el propietario es el origen
            PropietarioActualId = origen,

            // --- Listas ---
            Detalles = new List<IComprobanteDetalleCreate>(result.Detalles?.Select(d => new ComprobanteDetalleCreate
            {
                Detalle = d.Detalle,
                UnidadMedidaId = d.UnidadMedidaId ?? UnidadMedida.UNIDAD,
                Cantidad = d.Cantidad ?? 1,
                PrecioUnitario = d.PrecioUnitario ?? 0m, // Default a 0 si null
                ImporteBonificacion = d.ImporteBonificacion ?? 0m,
                Subtotal = d.Subtotal ?? 0m,
                ImporteIVA = d.ImporteIVA ?? 0m,
                Exento = d.Exento ?? false, // Default a false si null
                Alicuota = d.Alicuota,
                OrdenCompraId = d.OrdenCompraId
            })),

            Impuestos = new List<IComprobanteImpuestoCreate>(result.Impuestos?.Select(i => new ComprobanteImpuestoCreate
            {
                ImpuestoId = i.ImpuestoId,
                Descripcion = i.Descripcion,
                ImporteTotal = i.ImporteTotal ?? 0m,
                Alicuota = i.AlicuotaValor ?? 0,
            })),

            Percepciones = new List<IComprobantePercepcionCreate>(result.Percepciones?.Select(p => new ComprobantePercepcionCreate
            {
                PercepcionId = p.PercepcionId,
                Descripcion = p.Descripcion,
                Alicuota = p.Alicuota ?? 0m,
                ImporteTotal = p.ImporteTotal ?? 0m,
            })),
        };

        // Filtrar impuestos/percepciones sin ID si SaveDraftAsync no los maneja
        // (SaveDraftAsync ya lo hace internamente, así que no es estrictamente necesario aquí)
        // saveArgs.Impuestos = saveArgs.Impuestos.Where(i => i.ImpuestoId.HasValue && i.ImpuestoId > 0).ToList();
        // saveArgs.Percepciones = saveArgs.Percepciones.Where(p => p.PercepcionId.HasValue && p.PercepcionId > 0).ToList();


        return saveArgs;
    }

    /// <summary>
    /// Realiza validaciones sobre los datos mapeados para guardar como borrador.
    /// </summary>
    /// <returns>Una lista de <see cref="ValidationFailure"/>. Lista vacía si es válido.</returns>
    private List<ValidationFailure> ValidateComprobanteCreate(ComprobanteCreate args)
    {
        var failures = new List<ValidationFailure>();

        // Nombres de propiedades para mensajes (podrían definirse como constantes)
        string p_TipoComprobante = _loc["Tipo Comprobante"];
        string p_PuntoVenta = _loc["Punto de Venta"];
        string p_Numero = _loc["Número"];
        string p_FechaEmision = _loc["Fecha Emisión"];
        string p_TipoCodAut = _loc["Tipo Código Autorización"];
        string p_CodAut = _loc["Código Autorización"];
        string p_Moneda = _loc["Moneda"];
        string p_Detalles = _loc["Detalles"];
        string p_CuitEmisor = _loc["CUIT Emisor"];

        // --- Validaciones ---
        if (args.ComprobanteTipoId <= 0)
            failures.Add(new ValidationFailure(nameof(args.ComprobanteTipoId), _loc["El campo ‘{0}’ es obligatorio.", p_TipoComprobante]));

        if (args.PuntoVenta <= 0)
            failures.Add(new ValidationFailure(nameof(args.PuntoVenta), _loc["El campo ‘{0}’ es obligatorio.", p_PuntoVenta]));

        if (args.Numero <= 0)
            failures.Add(new ValidationFailure(nameof(args.Numero), _loc["El campo ‘{0}’ es obligatorio.", p_Numero]));

        if (args.FechaEmision == DateTime.MinValue)
            failures.Add(new ValidationFailure(nameof(args.FechaEmision), _loc["El campo ‘{0}’ es obligatorio o inválido.", p_FechaEmision]));
        // else if (args.FechaEmision > DateTime.Today.AddDays(1).AddTicks(-1)) // Ejemplo: No futura
        //     failures.Add(new ValidationFailure(nameof(args.FechaEmision), _loc["La fecha de emisión no puede ser futura."]));

        if (args.TipoCodigoAutorizacionId <= 0)
            failures.Add(new ValidationFailure(nameof(args.TipoCodigoAutorizacionId), _loc["El campo ‘{0}’ es obligatorio.", p_TipoCodAut]));

        if (string.IsNullOrWhiteSpace(args.CodigoAutorizacion))
            failures.Add(new ValidationFailure(nameof(args.CodigoAutorizacion), _loc["El campo ‘{0}’ es obligatorio.", p_CodAut]));
        // else if (args.CodigoAutorizacion.Length != 14) // Ejemplo: Validar longitud CAE
        //    failures.Add(new ValidationFailure(nameof(args.CodigoAutorizacion), _loc["El formato del Código de Autorización no es válido."]));

        if (args.MonedaId <= 0)
            failures.Add(new ValidationFailure(nameof(args.MonedaId), _loc["El campo ‘{0}’ es obligatorio.", p_Moneda]));

        if (string.IsNullOrWhiteSpace(args.NroIdentificacionFiscalPro))
            failures.Add(new ValidationFailure(nameof(args.NroIdentificacionFiscalPro), _loc["El campo ‘{0}’ es obligatorio.", p_CuitEmisor]));
        // else if (!EsCuitValido(args.NroIdentificacionFiscalPro)) // Ejemplo: Usar función de validación CUIT
        //    failures.Add(new ValidationFailure(nameof(args.NroIdentificacionFiscalPro), _loc["El CUIT Emisor no es válido."]));


        if (args.Detalles == null || !args.Detalles.Any())
            failures.Add(new ValidationFailure(nameof(args.Detalles), _loc["No se han agregado detalles al comprobante."]));
        //else
        //{
        //    // Opcional: Validaciones básicas sobre los detalles si son críticas para el borrador
        //    for (int i = 0; i < args.Detalles.Count; i++)
        //    {
        //        var detalle = args.Detalles[i];
        //        if (string.IsNullOrWhiteSpace(detalle.Detalle))
        //        {
        //            failures.Add(new ValidationFailure($"Detalles[{i}].{nameof(detalle.Detalle)}", _loc["El detalle del ítem {0} no puede estar vacío.", i + 1]));
        //        }
        //        if (detalle.Subtotal == null) // Subtotal es nullable en IComprobanteDetalleCreate
        //        {
        //            failures.Add(new ValidationFailure($"Detalles[{i}].{nameof(detalle.Subtotal)}", _loc["El subtotal del ítem {0} es requerido.", i + 1]));
        //        }
        //    }
        //}

        return failures;
    }
}