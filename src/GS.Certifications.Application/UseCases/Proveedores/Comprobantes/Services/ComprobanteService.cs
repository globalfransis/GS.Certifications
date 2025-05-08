using Azure.AI.DocumentIntelligence;
using GS.AI.DocumentIntelligence.Legacy.Models;
using GS.AI.DocumentIntelligence.Legacy.Services.Interfaces;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.Interfaces.SfeApi;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Exceptions;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services.Analysis.Strategies;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;
using GS.Certifications.Application.Commons.Dtos.SfeApi;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services.Analysis.Strategies;
using GS.Certifications.Domain.Entities.Comprobantes;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Impuestos;
using GS.Certifications.Domain.Entities.Percepciones;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services;

#region Servicio Concreto

/// <summary>
/// Implementación concreta de <see cref="IComprobanteService"/>.
/// Maneja la lógica de negocio y acceso a datos para los comprobantes.
/// </summary>
public class ComprobanteService : BaseGSFService, IComprobanteService
{
    private readonly IDocumentAnalysisService documentAnalysisService;

    private readonly ICertificationsDbContext context;

    private readonly IValidarComprobanteService validarComprobanteService;
    private readonly IServiceProvider serviceProvider;
    private IComprobanteHeaderStrategy _headerStrategy;
    private IComprobanteDetailStrategy _detailStrategy;
    private IComprobanteTaxStrategy _taxStrategy;
    private IComprobantePerceptionStrategy _perceptionStrategy;
    private IComprobanteTotalStrategy _totalStrategy;


    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ComprobanteService"/>.
    /// </summary>
    /// <param name="documentAnalysisService">Servicio para analizar documentos (ej: extraer datos de PDF).</param>
    /// <param name="context">Contexto de base de datos para las entidades de Socios.</param>
    /// <param name="validarComprobanteService">Servicio para la validación de comprobantes en ARCA.</param>
    public ComprobanteService(IDocumentAnalysisService documentAnalysisService, ICertificationsDbContext context/*, IValidarComprobanteService validarComprobanteService*/, IServiceProvider serviceProvider, IComprobanteTotalStrategy totalStrategy)
    {
        this.documentAnalysisService = documentAnalysisService;
        this.context = context;
        //this.validarComprobanteService = validarComprobanteService;
        this.serviceProvider = serviceProvider;
        //_headerStrategy = headerStrategy;
        //_detailStrategy = detailStrategy;
        //_taxStrategy = taxStrategy;
        //_perceptionStrategy = perceptionStrategy;
        _totalStrategy = totalStrategy;
    }

    /// <inheritdoc/>
    public async Task<Comprobante> GetAsync(int id)
    {
        var comprobante = await GetQueryable().FirstOrDefaultAsync(c => c.Id == id);

        return comprobante ?? throw new ComprobanteInexistenteException();
    }

    private IQueryable<Comprobante> GetQueryable()
    {
        return context.Comprobantes
                    .Include(c => c.ComprobanteEstado)
                    .Include(c => c.ComprobanteTipo)
                    .Include(c => c.TipoCodigoAutorizacion)
                    .Include(c => c.Empresa)
                    .Include(c => c.Moneda)
                    .Include(c => c.Impuestos)
                        .ThenInclude(i => i.Impuesto)
                            .ThenInclude(i => i.Tipo)
                    .Include(c => c.Percepciones)
                        .ThenInclude(i => i.Percepcion)
                            .ThenInclude(i => i.Provincia)
                    .Include(c => c.Detalles)
                        .ThenInclude(cd => cd.UnidadMedida)
                    .AsQueryable();
    }

    /// <inheritdoc/>
    public async Task<IPaginatedQueryResult<Comprobante>> GetManyAsync(IComprobanteQueryParameter parameters)
    {
        var comprobantesQueryable = GetQueryable();

        if (!string.IsNullOrEmpty(parameters.NroIdentificacionFiscalPro))
        {
            comprobantesQueryable = comprobantesQueryable.Where(c => c.NroIdentificacionFiscalPro.Contains(parameters.NroIdentificacionFiscalPro));
        }

        if (parameters.CategoriaTipoEmisorId is not null)
        {
            comprobantesQueryable = comprobantesQueryable.Where(c => c.CategoriaTipoEmisorId == parameters.CategoriaTipoEmisorId);
        }

        if (parameters.ComprobanteTipoId is not null)
        {
            comprobantesQueryable = comprobantesQueryable.Where(c => c.ComprobanteTipoId == parameters.ComprobanteTipoId);
        }

        if (parameters.CategoriaTipoReceptorId is not null)
        {
            comprobantesQueryable = comprobantesQueryable.Where(c => c.CategoriaTipoReceptorId == parameters.CategoriaTipoReceptorId);
        }

        if (parameters.PuntoVenta is not null)
        {
            comprobantesQueryable = comprobantesQueryable.Where(c => c.PuntoVenta == parameters.PuntoVenta);
        }

        if (parameters.Numero is not null)
        {
            comprobantesQueryable = comprobantesQueryable.Where(c => c.Numero == parameters.Numero);
        }

        if (parameters.FechaEmisionDesde is not null)
        {
            comprobantesQueryable = comprobantesQueryable.Where(c => c.FechaEmision >= parameters.FechaEmisionDesde);
        }

        if (parameters.FechaEmisionHasta is not null)
        {
            comprobantesQueryable = comprobantesQueryable.Where(c => c.FechaEmision >= parameters.FechaEmisionHasta);
        }

        if (parameters.FechaRegistracionDesde is not null)
        {
            comprobantesQueryable = comprobantesQueryable.Where(c => c.FechaRegistracion >= parameters.FechaRegistracionDesde);
        }

        if (parameters.FechaRegistracionHasta is not null)
        {
            comprobantesQueryable = comprobantesQueryable.Where(c => c.FechaRegistracion >= parameters.FechaRegistracionHasta);
        }

        if (parameters.TipoCodigoAutorizacionId is not null)
        {
            comprobantesQueryable = comprobantesQueryable.Where(c => c.TipoCodigoAutorizacionId == parameters.TipoCodigoAutorizacionId);
        }

        if (parameters.EmpresaId is not null)
        {
            comprobantesQueryable = comprobantesQueryable.Where(c => c.EmpresaId == parameters.EmpresaId);
        }

        if (parameters.CompanyId is not null)
        {
            comprobantesQueryable = comprobantesQueryable.Where(c => c.CompanyId == parameters.CompanyId);
        }

        if (parameters.ComprobanteEstadoId is not null)
        {
            comprobantesQueryable = comprobantesQueryable.Where(c => c.ComprobanteEstadoId == parameters.ComprobanteEstadoId);
        }

        return await Get(comprobantesQueryable, parameters);

    }

    /// <inheritdoc/>
    /// <remarks>
    /// Realiza validaciones internas antes de agregar la entidad al contexto.
    /// </remarks>
    public async Task<Comprobante> CreateAsync(IComprobanteCreate c, EstadoComprobante estado = EstadoComprobante.BORRADOR)
    {
        Comprobante nuevo = await MapComprobanteCreateAsync(c);

        nuevo.ComprobanteEstadoId = (short)estado;

        // Validaciones de negocio
        await ValidateAsync(nuevo);

        //// Validamos en ARCA antes de persistir
        //await VerifyEstadoARCAAsync(nuevo);

        context.Comprobantes.Add(nuevo);

        return nuevo;
    }

    private async Task<Comprobante> MapComprobanteCreateAsync(IComprobanteCreate c)
    {
        EmpresaPortal empresa = await context.EmpresasPortales.FirstOrDefaultAsync(e => e.Id == c.EmpresaId);
        return await MapComprobanteCreateAsync(empresa, c);
    }


    private async Task<Comprobante> MapComprobanteCreateAsync(EmpresaPortal empresaportal, IComprobanteCreate c)
    {
        var nuevo = new Comprobante
        {
            // TODO: en el futuro hay que validar que los cuits del comprobante se correspondan con los del emisor/receptor
            NroIdentificacionFiscalPro = c.NroIdentificacionFiscalPro,
            NroIdentificacionFiscalCompany = c.NroIdentificacionFiscalCompany,
            CategoriaTipoEmisorId = c.CategoriaTipoEmisorId,
            ComprobanteTipoId = c.ComprobanteTipoId,
            PuntoVenta = c.PuntoVenta,
            Numero = c.Numero,
            FechaEmision = c.FechaEmision,
            FechaRegistracion = DateTime.Now,

            TipoCodigoAutorizacionId = c.TipoCodigoAutorizacionId,
            CodigoAutorizacion = c.CodigoAutorizacion,

            MonedaId = c.MonedaId,
            ImporteTotal = c.ImporteTotal,
            ImporteNeto = c.ImporteNeto,
            ImporteIVA = c.ImporteIVA,
            ImporteBonificacion = c.ImporteBonificacion,
            ImportePercepcionIVA = c.ImportePercepcionIVA,
            ImportePercepcionIIBB = c.ImportePercepcionIIBB,
            ImportePercepcionMunicipal = c.ImportePercepcionMunicipal,
            ImporteImpuestosInternos = c.ImporteImpuestosInternos,
            ImporteOtrosTributosProv = c.ImporteOtrosTributosProv,
            Comentarios = c.Comentarios,
            CompanyId = empresaportal.CompanyId,
            Empresa = empresaportal,
            ValidacionQR = c.ValidacionQR,
            QRValor = c.QRValor,
            EstadoValidacionARCAId = (short)c.EstadoConstatacion,
            ObservacionesARCA = c.ObservacionesARCA,
            EstadoValidacionARCAQRId = (short)c.EstadoConstatacionQR,
            ObservacionesARCAQR = c.ObservacionesARCA,
            NroOrdenCompra = c.NroOrdenCompra,
            NroRemito = c.NroRemito,
            CondicionVentaId = c.CondicionVentaId,
            OrigenId = c.OrigenId,
            PropietarioActualId = c.PropietarioActualId,
            NombreArchivo = c.FileName,
            FechaVencimiento = c.FechaVencimiento,
            FechaVencimientoCodigoAutorizacion = c.FechaVencimientoCodigoAutorizacion
        };

        foreach (var item in c.Detalles)
        {
            var detalle = new ComprobanteDetalle()
            {
                Comprobante = nuevo,
                Detalle = item.Detalle,
                UnidadMedidaId = item.UnidadMedidaId ?? UnidadMedida.UNIDAD,
                Cantidad = item.Cantidad,
                PrecioUnitario = item.PrecioUnitario,
                ImporteBonificacion = item.ImporteBonificacion ?? decimal.Zero,
                Subtotal = item.Subtotal,
                ImporteIVA = item.ImporteIVA ?? decimal.Zero,
                Alicuota = item.Alicuota ?? 0,
                Exento = item.Exento ?? false
            };
            nuevo.Detalles.Add(detalle);
        }


        foreach (var item in c.Impuestos)
        {
            var impuesto = await context.Impuestos.FirstOrDefaultAsync(i => i.Id == item.ImpuestoId);

            if (impuesto is null) continue;

            var impuestoDetalle = new ImpuestoDetalle()
            {
                Comprobante = nuevo,
                Impuesto = impuesto,
                ImpuestoId = (int)item.ImpuestoId,
                Descripcion = item.Descripcion,
                ImporteTotal = item.ImporteTotal
            };
            nuevo.Impuestos.Add(impuestoDetalle);
        }

        foreach (var item in c.Percepciones)
        {
            var percepcion = await context.Percepciones.FirstOrDefaultAsync(p => p.Id == item.PercepcionId);

            if (percepcion is null) continue;

            var percepcionDetalle = new PercepcionDetalle()
            {
                Comprobante = nuevo,
                Alicuota = item.Alicuota,
                PercepcionId = (int)item.PercepcionId,
                Percepcion = percepcion,
                Descripcion = item.Descripcion,
                ImporteTotal = item.ImporteTotal
            };
            nuevo.Percepciones.Add(percepcionDetalle);
        }

        return nuevo;
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Establece el estado del comprobante a un estado de borrador.
    /// No realiza las validaciones estrictas de <see cref="CreateAsync"/>.
    /// </remarks>
    public async Task<Comprobante> SaveDraftAsync(IComprobanteCreate c)
    {
        var nuevo = await MapComprobanteCreateAsync(c);

        // el estado default de un comprobante nuevo ya es BORRADOR

        context.Comprobantes.Add(nuevo);

        return nuevo;
    }


    /// <inheritdoc/>
    /// <remarks>
    /// Establece el estado del comprobante a un estado de borrador.
    /// No realiza las validaciones estrictas de <see cref="CreateAsync"/>.
    /// </remarks>
    public async Task<Comprobante> GenerateDraftAsync(EmpresaPortal e, IComprobanteCreate c)
    {
        var nuevo = await MapComprobanteCreateAsync(e, c);

        return nuevo;
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Establece el estado del comprobante a un estado de borrador.
    /// No realiza las validaciones estrictas de <see cref="CreateAsync"/>.
    /// </remarks>
    public async Task<Comprobante> GenerateDraftAsync(IComprobanteCreate c)
    {
        var nuevo = await MapComprobanteCreateAsync(c);

        return nuevo;
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Establece el estado del comprobante a un estado de borrador.
    /// No realiza las validaciones estrictas de <see cref="CreateAsync"/>.
    /// </remarks>
    public async Task<Comprobante> GenerateDraftAsync(IComprobanteAnalysisResult analysis, EmpresaPortal empresaPortal)
    {
        CompanyExtra companyExtra = await context.CompanyExtras.Where(c => c.CompanyId == empresaPortal.CompanyId).SingleOrDefaultAsync();
        bool ep_wihtUsers = await context.UsuarioEmpresasPortales.Where(uep => uep.EmpresaPortal == empresaPortal && uep.Habilitado).AnyAsync();

        // --- Parseo de Datos Clave (Lanzar excepción si falla) ---
        if (!int.TryParse(analysis.PuntoVenta, NumberStyles.Any, CultureInfo.InvariantCulture, out int puntoVenta))
        {
            puntoVenta = 0;
        }
        if (!int.TryParse(analysis.Numero, NumberStyles.Any, CultureInfo.InvariantCulture, out int numero))
        {
            numero = 0;
        }
        if (!DateTime.TryParseExact(analysis.FechaEmision, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaEmision))
        {
            fechaEmision = DateTime.MinValue;
        }

        short propietarioActualId = empresaPortal.Confirmado && ep_wihtUsers ? Origen.PROVEEDOR : Origen.BACKOFFICE;

        var saveArgs = new ComprobanteCreate
        {
            // --- Datos Cabecera ---   
            OrigenId = Origen.CORREO,
            PropietarioActualId = propietarioActualId,
            ComprobanteTipoId = analysis.ComprobanteTipoId,
            PuntoVenta = puntoVenta,
            Numero = numero,
            Letra = analysis.Letra,
            FechaEmision = fechaEmision,
            TipoCodigoAutorizacionId = analysis.TipoCodigoAutorizacionId,
            CodigoAutorizacion = analysis.CodigoAutorizacion,
            MonedaId = analysis.MonedaId,

            // --- Importes ---
            ImporteNeto = analysis.ImporteNeto ?? 0m,
            ImporteTotal = analysis.ImporteTotal ?? 0m,
            ImporteIVA = analysis.ImporteIVA ?? 0m,
            ImporteBonificacion = analysis.ImporteBonificacion ?? 0m,
            ImportePercepcionIVA = analysis.ImportePercepcionIVA ?? 0m,
            ImportePercepcionIIBB = analysis.ImportePercepcionIIBB ?? 0m,
            ImportePercepcionMunicipal = analysis.ImportePercepcionMunicipal ?? 0m,
            ImporteImpuestosInternos = analysis.ImporteImpuestosInternos ?? 0m,
            ImporteOtrosTributosProv = analysis.ImporteOtrosTributosProv ?? 0m,

            EstadoConstatacionQR = analysis.EstadoConstatacionQR,
            EstadoConstatacion = analysis.EstadoConstatacion,
            ObservacionesARCAQR = analysis.ObservacionesARCAQR,
            ObservacionesARCA = analysis.ObservacionesARCA,

            // --- Datos Adicionales y Compañía ---
            Comentarios = analysis.Comentarios,
            CodigoHES = analysis.CodigoHES,
            NroIdentificacionFiscalPro = empresaPortal.IdentificadorTributario,
            CategoriaTipoEmisorId = empresaPortal.TipoResponsableId,
            CategoriaTipoReceptorId = companyExtra.TipoResponsableId,
            NroIdentificacionFiscalCompany = companyExtra.IdentificadorTributario,

            // --- QR ---
            ValidacionQR = analysis.ValidacionQR,
            QRValor = analysis.QRValor,

            // --- Listas ---
            Detalles = new List<IComprobanteDetalleCreate>(analysis.Detalles?.Select(d => new ComprobanteDetalleCreate
            {
                Detalle = d.Detalle,
                UnidadMedidaId = d.UnidadMedidaId,
                Cantidad = d.Cantidad ?? 1,
                PrecioUnitario = d.PrecioUnitario ?? 0m, // Default a 0 si null
                ImporteBonificacion = d.ImporteBonificacion ?? 0m,
                Subtotal = d.Subtotal ?? 0m,
                ImporteIVA = d.ImporteIVA ?? 0m,
                Exento = d.Exento ?? false, // Default a false si null
                Alicuota = d.Alicuota,
                OrdenCompraId = d.OrdenCompraId
            })),

            Impuestos = new List<IComprobanteImpuestoCreate>(analysis.Impuestos?.Select(i => new ComprobanteImpuestoCreate
            {
                ImpuestoId = i.ImpuestoId,
                Descripcion = i.Descripcion,
                ImporteTotal = i.ImporteTotal ?? 0m,
                Alicuota = i.AlicuotaValor,
            })),

            Percepciones = new List<IComprobantePercepcionCreate>(analysis.Percepciones?.Select(p => new ComprobantePercepcionCreate
            {
                PercepcionId = p.PercepcionId,
                Descripcion = p.Descripcion,
                Alicuota = p.Alicuota ?? 0m,
                ImporteTotal = p.ImporteTotal ?? 0m,
            })),
        };

        return await GenerateDraftAsync(empresaPortal, saveArgs);
    }



    public async Task<Comprobante> CreateAsync(IComprobanteSave c, EstadoComprobante estado = EstadoComprobante.BORRADOR)
    {
        // TODO: we should may validate the invoice's header here, not in validator
        Comprobante comprobante = new()
        {
            NroIdentificacionFiscalPro = c.NroIdentificacionFiscalPro,
            CategoriaTipoEmisorId = c.CategoriaTipoEmisorId,
            ComprobanteTipoId = c.ComprobanteTipoId,
            PuntoVenta = c.PuntoVenta,
            Numero = c.Numero,
            FechaEmision = c.FechaEmision,
            FechaRegistracion = DateTime.Now,
            TipoCodigoAutorizacionId = c.TipoCodigoAutorizacionId,
            CodigoAutorizacion = c.CodigoAutorizacion,
            MonedaId = c.MonedaId,
            ImporteTotal = c.ImporteTotal,
            ImporteNeto = c.ImporteNeto,
            ImporteIVA = c.ImporteIVA,
            ImporteBonificacion = c.ImporteBonificacion,
            ImportePercepcionIVA = c.ImportePercepcionIVA,
            ImportePercepcionIIBB = c.ImportePercepcionIIBB,
            ImportePercepcionMunicipal = c.ImportePercepcionMunicipal,
            ImporteImpuestosInternos = c.ImporteImpuestosInternos,
            ImporteOtrosTributosProv = c.ImporteOtrosTributosProv,
            Comentarios = c.Comentarios,
            CompanyId = c.CompanyId,
            EmpresaId = c.EmpresaId,
            ValidacionQR = c.ValidacionQR,
            QRValor = c.QRValor,
            EstadoValidacionARCAId = (short)c.EstadoConstatacion,
            ObservacionesARCA = c.ObservacionesARCA,
        };

        foreach (var item in c.Detalles)
        {
            var detalle = new ComprobanteDetalle()
            {
                Comprobante = comprobante,
                Detalle = item.Detalle,
                UnidadMedidaId = item.UnidadMedidaId ?? UnidadMedida.UNIDAD,
                Cantidad = item.Cantidad ?? 0,
                PrecioUnitario = item.PrecioUnitario ?? 0,
                ImporteBonificacion = item.ImporteBonificacion ?? decimal.Zero,
                Subtotal = item.Subtotal ?? 0,
                ImporteIVA = item.ImporteIVA ?? 0,
                Exento = item.Exento ?? false
            };
            comprobante.Detalles.Add(detalle);
        }

        c.Impuestos = c.Impuestos.Where(i => i.ImpuestoId != null).ToList();

        foreach (var item in c.Impuestos)
        {
            var impuesto = await context.Impuestos.FirstOrDefaultAsync(i => i.Id == item.ImpuestoId);
            var impuestoDetalle = new ImpuestoDetalle()
            {
                Comprobante = comprobante,
                Impuesto = impuesto,
                ImpuestoId = (short)item.ImpuestoId,
                Descripcion = item.Descripcion,
                ImporteTotal = item.ImporteTotal ?? 0
            };
            comprobante.Impuestos.Add(impuestoDetalle);
        }

        c.Percepciones = c.Percepciones.Where(i => i.PercepcionId != null).ToList();

        foreach (var item in c.Percepciones)
        {
            var percepcionDetalle = new PercepcionDetalle()
            {
                Comprobante = comprobante,
                Alicuota = item.Alicuota ?? 0,
                PercepcionId = (short)item.PercepcionId,
                Descripcion = item.Descripcion,
                ImporteTotal = item.ImporteTotal ?? 0
            };
            comprobante.Percepciones.Add(percepcionDetalle);
        }

        comprobante.ComprobanteEstadoId = (int)estado;

        context.Comprobantes.Add(comprobante);

        return comprobante;
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Actualiza un comprobante existente con los datos proporcionados y lo marca como 'CONFIRMADO'.
    /// Maneja la adición, modificación y eliminación de detalles, impuestos y percepciones.
    /// Realiza validaciones de negocio al final.
    /// </remarks>
    public async Task UpdateAsync(IComprobanteUpdate c)
    {
        var comprobante = await GetAsync(c.Id);

        comprobante.RowVersion = c.RowVersion;

        await MapComprobanteUpdateAsync(c, comprobante);

        //// Validación en ARCA
        //await VerifyEstadoARCAAsync(comprobante);

        // Validaciones de negocio
        await ValidateAsync(comprobante, false);
    }

    private async Task MapComprobanteUpdateAsync(IComprobanteUpdate c, Comprobante comprobante)
    {
        comprobante.NroIdentificacionFiscalPro = c.NroIdentificacionFiscalPro;
        //comprobante.DomicilioPro = c.DomicilioPro;
        comprobante.CategoriaTipoEmisorId = c.CategoriaTipoEmisorId;
        comprobante.ComprobanteTipoId = c.ComprobanteTipoId;
        comprobante.PuntoVenta = c.PuntoVenta;
        comprobante.Numero = c.Numero;
        comprobante.FechaEmision = c.FechaEmision;
        comprobante.TipoCodigoAutorizacionId = c.TipoCodigoAutorizacionId;
        comprobante.CodigoAutorizacion = c.CodigoAutorizacion;
        comprobante.MonedaId = c.MonedaId;
        comprobante.ImporteTotal = c.ImporteTotal;
        comprobante.ImporteNeto = c.ImporteNeto;
        comprobante.ImporteIVA = c.ImporteIVA;
        comprobante.ImporteBonificacion = c.ImporteBonificacion;
        comprobante.ImportePercepcionIVA = c.ImportePercepcionIVA;
        comprobante.ImportePercepcionIIBB = c.ImportePercepcionIIBB;
        comprobante.ImportePercepcionMunicipal = c.ImportePercepcionMunicipal;
        comprobante.ImporteImpuestosInternos = c.ImporteImpuestosInternos;
        comprobante.ImporteOtrosTributosProv = c.ImporteOtrosTributosProv;
        comprobante.Comentarios = c.Comentarios;

        comprobante.NroOrdenCompra = c.NroOrdenCompra;
        comprobante.NroRemito = c.NroRemito;
        comprobante.CondicionVentaId = c.CondicionVentaId;

        comprobante.FechaVencimiento = c.FechaVencimiento;
        comprobante.FechaVencimientoCodigoAutorizacion = c.FechaVencimientoCodigoAutorizacion;

        // we understand these should not be updated
        //comprobante.CompanyId = c.CompanyId;
        //comprobante.EmpresaId = c.EmpresaId;
        //comprobante.FechaRegistracion = System.DateTime.Now;

        // Borramos todos los impuestos de tipo IVA para volver a generarlos en base a los montos de IVA actualizados
        //var impuestosIVA = comprobante.Impuestos.Where(i => i.Impuesto.TipoId == ImpuestoTipo.IVA);
        //context.ImpuestoDetalles.RemoveRange(impuestosIVA);
        //comprobante.Impuestos.RemoveAll(i => i.Impuesto.TipoId == ImpuestoTipo.IVA);

        var impuestosEliminados = c.Impuestos.Where(i => i.Id > 0 && i.Deleted).Select(i => i.Id);
        var impuestosNuevos = c.Impuestos.Where(i => i.Id == 0 && !i.Deleted);
        var impuestosModificados = c.Impuestos.Where(i => i.Id > 0 && !i.Deleted);

        var percepcionesEliminadas = c.Percepciones.Where(i => i.Id > 0 && i.Deleted).Select(i => i.Id);
        var percepcionesNuevas = c.Percepciones.Where(i => i.Id == 0 && !i.Deleted);
        var percepcionesModificadas = c.Percepciones.Where(i => i.Id > 0 && !i.Deleted);

        var detallesEliminados = c.Detalles.Where(i => i.Id > 0 && i.Deleted).Select(i => i.Id);
        var detallesNuevos = c.Detalles.Where(i => i.Id == 0 && !i.Deleted);
        var detallesModificados = c.Detalles.Where(i => i.Id > 0 && !i.Deleted);

        // Eliminamos los detalles marcados para su eliminación
        context.ComprobanteDetalles.RemoveRange(comprobante.Detalles.Where(d => detallesEliminados.Contains(d.Id)));
        comprobante.Detalles.RemoveAll(d => detallesEliminados.Contains(d.Id));

        // Creamos los detalles nuevos
        foreach (var item in detallesNuevos)
        {
            var detalle = new ComprobanteDetalle()
            {
                Comprobante = comprobante,
                Detalle = item.Detalle,
                UnidadMedidaId = item.UnidadMedidaId ?? UnidadMedida.UNIDAD,
                Cantidad = item.Cantidad,
                PrecioUnitario = item.PrecioUnitario,
                ImporteBonificacion = item.ImporteBonificacion ?? decimal.Zero,
                Subtotal = item.Subtotal,
                ImporteIVA = item.ImporteIVA ?? decimal.Zero,
                Exento = item.Exento ?? false
            };
            comprobante.Detalles.Add(detalle);
        }

        // Modificamos todos los demás (asumimos que vienen modificados)
        foreach (var item in detallesModificados)
        {
            var detalle = comprobante.Detalles.FirstOrDefault(d => d.Id == item.Id);
            detalle.Detalle = item.Detalle;

            detalle.UnidadMedidaId = item.UnidadMedidaId ?? UnidadMedida.UNIDAD;
            detalle.Cantidad = item.Cantidad;
            detalle.PrecioUnitario = item.PrecioUnitario;
            detalle.ImporteBonificacion = item.ImporteBonificacion ?? decimal.Zero;
            detalle.Subtotal = item.Subtotal;
            detalle.Exento = item.Exento ?? false;
        }

        // Eliminamos los impuestos marcados para su eliminación
        context.ImpuestoDetalles.RemoveRange(comprobante.Impuestos.Where(d => impuestosEliminados.Contains(d.Id)));
        comprobante.Impuestos.RemoveAll(d => impuestosEliminados.Contains(d.Id));

        foreach (var item in impuestosNuevos)
        {
            var impuesto = await context.Impuestos.FirstOrDefaultAsync(i => i.Id == item.ImpuestoId);
            var impuestoDetalle = new ImpuestoDetalle()
            {
                Comprobante = comprobante,
                ImpuestoId = (int)item.ImpuestoId,
                Impuesto = impuesto,
                Descripcion = item.Descripcion,
                ImporteTotal = item.ImporteTotal
            };
            comprobante.Impuestos.Add(impuestoDetalle);
        }

        foreach (var item in impuestosModificados)
        {
            var impuesto = comprobante.Impuestos.FirstOrDefault(i => i.Id == item.Id);
            impuesto.ImpuestoId = (int)item.ImpuestoId;
            impuesto.Descripcion = item.Descripcion;
            impuesto.ImporteTotal = item.ImporteTotal;
        }

        // Eliminamos las percepciones marcadas para su eliminación
        context.PercepcionDetalles.RemoveRange(comprobante.Percepciones.Where(d => percepcionesEliminadas.Contains(d.Id)));
        comprobante.Percepciones.RemoveAll(d => percepcionesEliminadas.Contains(d.Id));

        foreach (var item in percepcionesNuevas)
        {
            var percepcion = await context.Percepciones.FirstOrDefaultAsync(p => p.Id == item.PercepcionId);
            var percepcionDetalle = new PercepcionDetalle()
            {
                Comprobante = comprobante,
                Alicuota = item.Alicuota,
                PercepcionId = (int)item.PercepcionId,
                Percepcion = percepcion,
                Descripcion = item.Descripcion,
                ImporteTotal = item.ImporteTotal
            };
            comprobante.Percepciones.Add(percepcionDetalle);
        }

        foreach (var item in percepcionesModificadas)
        {
            var percepcion = comprobante.Percepciones.FirstOrDefault(i => i.Id == item.Id);
            percepcion.Alicuota = item.Alicuota;
            percepcion.PercepcionId = (int)item.PercepcionId;
            percepcion.Descripcion = item.Descripcion;
            percepcion.ImporteTotal = item.ImporteTotal;
        }
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Actualiza un comprobante en estado de borrador.
    /// La lógica es muy similar a <see cref="UpdateAsync"/>, pero opera sobre <see cref="IComprobanteSave"/>
    /// y mantiene el estado de borrador. No realiza las validaciones finales estrictas.
    /// </remarks>
    public async Task UpdateDraftAsync(IComprobanteUpdate c)
    {
        var comprobante = await GetAsync(c.Id);

        comprobante.RowVersion = c.RowVersion;

        await MapComprobanteUpdateAsync(c, comprobante);

        // Reseteamos el estado del comprobante en ARCA
        comprobante.EstadoValidacionARCAId = EstadoValidacionARCA.NO_VALIDADA;
        comprobante.ObservacionesARCA = string.Empty;
    }


    /// <inheritdoc/>
    /// <remarks>
    /// Actualiza un comprobante en estado de borrador.
    /// La lógica es muy similar a <see cref="UpdateAsync"/>, pero opera sobre <see cref="IComprobanteSave"/>
    /// y mantiene el estado de borrador. No realiza las validaciones finales estrictas.
    /// </remarks>
    public async Task<Comprobante> UpdateAsync(int comprobanteId, Comprobante c)
    {
        var comprobante = await GetAsync(comprobanteId);
        return Update(comprobante, c);
    }


    /// <inheritdoc/>
    /// <remarks>
    /// Actualiza un comprobante en estado de borrador.
    /// La lógica es muy similar a <see cref="UpdateAsync"/>, pero opera sobre <see cref="IComprobanteSave"/>
    /// y mantiene el estado de borrador. No realiza las validaciones finales estrictas.
    /// </remarks>
    public Comprobante Update(Comprobante comprobante, Comprobante c)
    {

        comprobante.NroIdentificacionFiscalPro = c.NroIdentificacionFiscalPro;
        //comprobante.DomicilioPro = c.DomicilioPro;
        comprobante.CategoriaTipoEmisorId = c.CategoriaTipoEmisorId;
        comprobante.ComprobanteTipoId = c.ComprobanteTipoId;
        comprobante.PuntoVenta = c.PuntoVenta;
        comprobante.Numero = c.Numero;
        comprobante.FechaEmision = c.FechaEmision;
        comprobante.TipoCodigoAutorizacionId = c.TipoCodigoAutorizacionId;
        comprobante.CodigoAutorizacion = c.CodigoAutorizacion;
        comprobante.MonedaId = c.MonedaId;
        comprobante.ImporteTotal = c.ImporteTotal;
        comprobante.ImporteNeto = c.ImporteNeto;
        comprobante.ImporteIVA = c.ImporteIVA;
        comprobante.ImporteBonificacion = c.ImporteBonificacion;
        comprobante.ImportePercepcionIVA = c.ImportePercepcionIVA;
        comprobante.ImportePercepcionIIBB = c.ImportePercepcionIIBB;
        comprobante.ImportePercepcionMunicipal = c.ImportePercepcionMunicipal;
        comprobante.ImporteImpuestosInternos = c.ImporteImpuestosInternos;
        comprobante.ImporteOtrosTributosProv = c.ImporteOtrosTributosProv;
        comprobante.Comentarios = c.Comentarios;
        comprobante.NroOrdenCompra = c.NroOrdenCompra;
        comprobante.NroRemito = c.NroRemito;
        comprobante.CondicionVentaId = c.CondicionVentaId;

        foreach (var item in comprobante.Detalles)
        {
            if (item.Id > 0) context.ComprobanteDetalles.Remove(item);
        }
        comprobante.Detalles = c.Detalles;

        foreach (var item in comprobante.Impuestos)
        {
            if (item.Id > 0) context.ImpuestoDetalles.Remove(item);
        }
        comprobante.Impuestos = c.Impuestos;

        foreach (var item in comprobante.Percepciones)
        {
            if (item.Id > 0) context.PercepcionDetalles.Remove(item);
        }
        comprobante.Percepciones = c.Percepciones;

        return comprobante;
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Elimina el comprobante y todas sus entidades relacionadas (detalles, impuestos, percepciones) del contexto.
    /// Utiliza el RowVersion para control de concurrencia optimista.
    /// </remarks>
    public async Task DeleteAsync(int id, byte[] rowVersion)
    {
        var comprobante = await GetAsync(id);
        comprobante.RowVersion = rowVersion;



        foreach (var item in comprobante.Detalles)
        {
            context.ComprobanteDetalles.Remove(item);
        }

        foreach (var item in comprobante.Impuestos)
        {
            context.ImpuestoDetalles.Remove(item);
        }


        foreach (var item in comprobante.Percepciones)
        {
            context.PercepcionDetalles.Remove(item);
        }

        context.Comprobantes.Remove(comprobante);
    }

    /// <summary>
    /// Analiza un archivo de comprobante utilizando estrategias configurables
    /// para extraer datos de cabecera, detalles, impuestos, percepciones y totales.
    /// </summary>
    /// <param name="bytesSource">Datos binarios del archivo.</param>
    /// <param name="parameters">Parámetros de análisis (ej. CompanyId).</param>
    /// <returns>El resultado del análisis.</returns>
    public async Task<IComprobanteAnalysisResult> AnalyzeAsync(BinaryData bytesSource, IComprobanteAnalysisParameter parameters)
    {
        Log.Logger.Information("Iniciando AnalyzeAsync para CompanyId={CompanyId}", parameters.CompanyId);

        // Add barcodes feature
        Features[] features = new Features[1];
        features[0] = Features.barcodes;

        var analysisResults = await documentAnalysisService.AnalyzeAsync(bytesSource, Model.Invoice, features);

        var document = analysisResults.FirstOrDefault();

        var comprobante = new ComprobanteAnalysisResult();

        // 2. Crear Contexto y Resultado Inicial
        var analisisContext = new AnalisysContext(document, parameters, comprobante);

        ResolveAnalysisStrategies(parameters);

        // 3. Ejecutar Estrategias en Secuencia
        try
        {
            // Cabecera (Intenta QR, luego campos, incluye validación QR si aplica)
            if (_headerStrategy is not null)
            {
                await _headerStrategy.PopulateHeaderAsync(analisisContext);
            }
            // Detalles
            if (_detailStrategy is not null)
            {
                await _detailStrategy.PopulateDetailsAsync(analisisContext);

            }
            // Impuestos (Globales/Resumidos) y Percepciones (Globales/Resumidas)
            if (_taxStrategy is not null)
            {
                await _taxStrategy.PopulateTaxesAsync(analisisContext);
                await _perceptionStrategy.PopulatePerceptionsAsync(analisisContext);
            }

            // Totales (Neto, Total)
            await _totalStrategy.PopulateTotalsAsync(analisisContext);

            // 4. Asignar Metadatos Finales (ejemplo)
            // analisisContext.ResultInProgress.FileName = "nombre_archivo_obtenido_de_algún_lado";
            // analisisContext.ResultInProgress.URL = analisisContext.ResultInProgress.QRValor != null ? "URL_QR_ORIGINAL" : null; // URL original si hubo QR

            Log.Logger.Information("Análisis completado exitosamente para CompanyId={CompanyId}, FileName={FileName}", parameters.CompanyId, analisisContext.ResultInProgress.FileName);
            return analisisContext.ResultInProgress;
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "Error durante la orquestación del análisis para CompanyId={CompanyId}", parameters.CompanyId);
            analisisContext.ResultInProgress.EstadoConstatacion = EstadoConstatacion.Error; // Usar estado general
            analisisContext.ResultInProgress.ObservacionesARCA = $"Error general durante análisis: {ex.Message}"; // Usar obs general
            return analisisContext.ResultInProgress; // Devolver resultado parcial con error
        }
    }

    private void ResolveAnalysisStrategies(IComprobanteAnalysisParameter parameters)
    {
        if (parameters.Modos.Contains(ModoAnalisis.QR) && parameters.Modos.Contains(ModoAnalisis.OCR_CABECERA))
        {
            //_headerStrategy = new DefaultHeaderStrategy(serviceProvider.GetRequiredService<ICertificationsDbContext>(), serviceProvider.GetRequiredService<IValidarComprobanteService>());
        }
        else if (parameters.Modos.Contains(ModoAnalisis.QR))
        {
            //_headerStrategy = new QRHeaderStrategy(serviceProvider.GetRequiredService<ICertificationsDbContext>(), serviceProvider.GetRequiredService<IValidarComprobanteService>());
            //_headerStrategy = new DefaultHeaderStrategy(serviceProvider.GetRequiredService<ICertificationsDbContext>(), serviceProvider.GetRequiredService<IValidarComprobanteService>());
        }
        else if (parameters.Modos.Contains(ModoAnalisis.OCR_CABECERA))
        {
            _headerStrategy = new OCRHeaderStrategy(serviceProvider.GetRequiredService<ICertificationsDbContext>());
        }
        else
        {
            _headerStrategy = null;
        }

        if (parameters.Modos.Contains(ModoAnalisis.OCR_DETALLE))
        {
            _detailStrategy = new DefaultDetailStrategy(serviceProvider.GetRequiredService<ICertificationsDbContext>());
        }
        else
        {
            _detailStrategy = new InferenceDetailStrategy();
        }

        if (parameters.Modos.Contains(ModoAnalisis.OCR_IMPUESTOS))
        {
            _taxStrategy = new DefaultTaxStrategy(serviceProvider.GetRequiredService<ICertificationsDbContext>());
            _perceptionStrategy = new DefaultPerceptionStrategy(serviceProvider.GetRequiredService<ICertificationsDbContext>());
        }
        else
        {
            _taxStrategy = null;
            _perceptionStrategy = null;
        }
    }

    /// <summary>
    /// Establece las propiedades de la cabecera del <see cref="ComprobanteAnalysisResult"/>
    /// basándose en los campos extraídos del análisis del documento.
    /// </summary>
    private async Task SetComprobanteCabecera(DocumentFieldDictionary extractedFields, ComprobanteAnalysisResult comprobante)
    {
        comprobante.NroIdentificacionFiscalPro = extractedFields.GetValueOrDefault("IdentificadorEmisor")?.ValueString;
        var comprobanteTipo = extractedFields.GetValueOrDefault("TipoComprobante")?.ValueString;
        var comprobanteTipoBuscado = await context.ComprobanteTipos.FirstOrDefaultAsync(ct => ct.CodigoArca.Contains(comprobanteTipo) || comprobanteTipo.Contains(ct.CodigoArca));
        comprobante.ComprobanteTipoId = comprobanteTipoBuscado?.Idm;
        comprobante.Numero = extractedFields.GetValueOrDefault("Nro")?.ValueString;
        comprobante.PuntoVenta = extractedFields.GetValueOrDefault("PuntoVenta")?.ValueString;
        comprobante.FechaEmision = extractedFields.GetValueOrDefault("FechaEmision")?.ValueDate?.ToString("yyyy-MM-dd")?.Split("T").FirstOrDefault();
        var tipoCodigoAutorizacionExtraido = extractedFields.GetValueOrDefault("TipoCodigoAutorizacion")?.ValueString;
        var codigoAutorizacionExtraido = extractedFields.GetValueOrDefault("CodigoAutorizacion")?.ValueString;
        string monedaExtraida = extractedFields.GetValueOrDefault("Moneda")?.ValueString;
        comprobante.MonedaId = (await context.Currencies.FirstOrDefaultAsync(ct => ct.Symbol.Contains(monedaExtraida) || monedaExtraida.Contains(ct.Symbol)))?.Idm;

        var tipoCodigoAutorizacion = await context.CodigoAutorizacionTipos.FirstOrDefaultAsync(ct => ct.Descripcion.ToLower() == tipoCodigoAutorizacionExtraido.ToLower());

        comprobante.TipoCodigoAutorizacionId = tipoCodigoAutorizacion != null ? tipoCodigoAutorizacion.Idm : CodigoAutorizacionTipo.CAE;
        comprobante.CodigoAutorizacion = codigoAutorizacionExtraido;
    }

    /// <summary>
    /// Establece las propiedades de la cabecera del <see cref="ComprobanteAnalysisResult"/>
    /// basándose en el JSON decodificado de un código QR de AFIP.
    /// </summary>
    private async Task SetComprobanteCabecera(ComprobanteAnalysisResult comprobante, string jsonString, long companyId)
    {
        comprobante.QRValor = jsonString;

        IComprobanteCabecera cabecera = JsonConvert.DeserializeObject<ComprobanteCabecera>(jsonString);
        try
        {
            var companyExtra = await context.CompanyExtras.FirstOrDefaultAsync(c => c.CompanyId == companyId);
            var resultadoConstatacion = await VerifyAsync(cabecera, long.TryParse(companyExtra.IdentificadorTributario, out long cuitCompany) ? cuitCompany : 0);

            comprobante.EstadoConstatacionQR = resultadoConstatacion.Estado;
            if (resultadoConstatacion.Observaciones != null && resultadoConstatacion.Observaciones.Any())
            {
                comprobante.ObservacionesARCAQR = "Obs: " + string.Join("; ", (IEnumerable<string>)resultadoConstatacion.Observaciones);
            }
        }
        catch (Exception ex)
        {

            comprobante.EstadoConstatacionQR = EstadoConstatacion.Error;
        }
        if (cabecera is null) throw new ComprobanteQRInvalido();

        comprobante.FechaEmision = cabecera.fecha;
        comprobante.NroIdentificacionFiscalPro = cabecera.cuit.ToString();
        comprobante.PuntoVenta = cabecera.ptoVta.ToString();

        var cmpTipoBuscado = await context.ComprobanteTipos.FirstOrDefaultAsync(ct => ct.CodigoArca == cabecera.tipoCmp.ToString());
        comprobante.ComprobanteTipoId = cmpTipoBuscado?.Idm;

        comprobante.Numero = cabecera.nroCmp.ToString();
        comprobante.ImporteTotal = cabecera.importe;

        // workaround
        if (cabecera.moneda == "PES")
        {
            comprobante.MonedaId = (await context.Currencies.FirstOrDefaultAsync(c => c.Code == "ARS"))?.Idm;
        }
        else
        {
            comprobante.MonedaId = (await context.Currencies.FirstOrDefaultAsync(c => c.Code == cabecera.moneda))?.Idm;
        }

        comprobante.NroIdentificacionFiscalCompany = cabecera.nroDocRec.ToString();

        comprobante.TipoCodigoAutorizacionId = cabecera.tipoCodAut.ToUpper() switch
        {
            "E" => CodigoAutorizacionTipo.CAE,
            "I" => CodigoAutorizacionTipo.CAI,
            "A" => CodigoAutorizacionTipo.CAEA,
            _ => (short?)CodigoAutorizacionTipo.CAE,
        };

        comprobante.CodigoAutorizacion = cabecera.codAut.ToString();

        comprobante.ValidacionQR = true;
    }

    /// <summary>
    /// Normaliza una descripción de percepción para facilitar la búsqueda (quita espacios, acentos, convierte a mayúsculas, reemplaza abreviaturas comunes).
    /// </summary>
    private string NormalizarDescripcionPercepcion(string descripcion)
    {
        if (string.IsNullOrEmpty(descripcion)) return string.Empty;

        string normalizada = descripcion.Trim();
        normalizada = normalizada.ToUpper();
        normalizada = normalizada.Replace(" ", "");
        normalizada = normalizada.Replace("-", "");
        normalizada = normalizada.Replace("/", "");
        normalizada = normalizada.Replace(".", "");
        normalizada = normalizada.Replace(",", "");

        // Abreviaturas y variaciones específicas de percepciones
        normalizada = normalizada.Replace("PER/RETDE", "PERCEPCIONRETENCIONDE");
        normalizada = normalizada.Replace("PER/RET", "PERCEPCIONRETENCION");
        normalizada = normalizada.Replace("PER", "PERCEPCION");
        normalizada = normalizada.Replace("RET", "RETENCION");
        normalizada = normalizada.Replace("GANANCIAS", "GANANCIA");
        normalizada = normalizada.Replace("IIBB", "INGRESOSBRUTOS");
        normalizada = normalizada.Replace("IMPUESTOALASGANANCIAS", "IMPUESTOAGANANCIAS");
        // *** IMPORTANTE:  Considerar el orden de los reemplazos ***

        return normalizada;
    }

    /// <summary>
    /// Normaliza una descripción de impuesto para facilitar la búsqueda (quita espacios, acentos, convierte a mayúsculas, reemplaza abreviaturas comunes).
    /// </summary>
    private string NormalizarDescripcionImpuesto(string descripcion)
    {
        if (string.IsNullOrEmpty(descripcion)) return string.Empty;

        string normalizada = descripcion.Trim();
        normalizada = normalizada.ToUpper();
        normalizada = normalizada.Replace(" ", "");
        normalizada = normalizada.Replace("-", "");
        normalizada = normalizada.Replace("/", "");
        normalizada = normalizada.Replace("%", "");
        normalizada = normalizada.Replace(".", "");
        normalizada = normalizada.Replace(",", "");

        normalizada = normalizada.Replace("IVA", "IMPUESTOALVALORAGREGADO");
        normalizada = normalizada.Replace("IIBB", "IMPUESTOINGRESOSBRUTOS");
        normalizada = normalizada.Replace("GAN.", "IMPUESTOALASGANANCIAS");
        normalizada = normalizada.Replace("IMPUESTOSINTERNOS", "IMPUESTOINTERNO");
        normalizada = normalizada.Replace("IMPUESTOSMUNICIPALES", "IMPUESTOOMUNICIPAL");
        normalizada = normalizada.Replace("IMPUESTOMUNICIPALES", "IMPUESTOOMUNICIPAL");
        normalizada = normalizada.Replace("IMPUESTOMUNICIPIO", "IMPUESTOOMUNICIPAL");
        normalizada = normalizada.Replace("IMPUESTOS", "IMPUESTO");
        normalizada = normalizada.Replace("INGRESOSBRUTOS", "INGRESOSBRUTO");
        normalizada = normalizada.Replace("GANANCIAS", "GANANCIA");

        normalizada = normalizada.Replace("VALORAGREGADO", "VALORAGREGADO");
        normalizada = normalizada.Replace("V.A.", "VALORAGREGADO");


        return normalizada;
    }

    /// <summary>
    /// Calcula el importe total esperado sumando neto, impuestos y percepciones.
    /// </summary>
    private decimal CalcularImporteTotal(ComprobanteAnalysisResult comprobante)
    {
        decimal sumaItemsSinImpuestos = comprobante.Detalles.Sum(d => d.PrecioUnitario ?? 0 * d.Cantidad ?? 0);

        return Math.Abs(comprobante.ImporteTotal ?? 0 - sumaItemsSinImpuestos);
    }

    /// <summary>
    /// Parsea una cadena que representa una alícuota de IVA (puede contener '%', ',', '.').
    /// </summary>
    private decimal? ParseAlicuotaIVA(DocumentField alicuotaIVAField)
    {
        if (alicuotaIVAField?.Content != null)
        {
            string alicuotaString = alicuotaIVAField.Content.Trim();

            alicuotaString = alicuotaString.Replace(",", ".").Replace(" ", "").Replace("%", "");

            if (decimal.TryParse(alicuotaString, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal alicuota))
            {
                return alicuota;
            }
            else
            {
                Console.WriteLine($"Error: No se pudo convertir la alícuota IVA '{alicuotaIVAField.ValueString}' a decimal.");
                return null;
            }
        }
        return null;
    }

    /// <summary>
    /// Calcula el importe neto esperado sumando los subtotales de los detalles y restando bonificaciones globales.
    /// </summary>
    private decimal CalcularImporteNeto(ComprobanteAnalysisResult comprobante)
    {
        // La suma de los ítems sin impuestos menos las bonificaciones es igual al total neto
        decimal sumaItemsSinImpuestos = comprobante.Detalles.Sum(d => d.PrecioUnitario ?? 0 * d.Cantidad ?? 0);
        decimal totalBonificaciones = comprobante.Detalles.Sum(d => d.ImporteBonificacion ?? 0);
        decimal totalNetoEsperado = sumaItemsSinImpuestos - totalBonificaciones;

        return totalNetoEsperado;
    }

    /// <summary>
    /// Intenta convertir el contenido de un campo de Document Intelligence a decimal, manejando diferentes formatos numéricos.
    /// </summary>
    private static decimal? ParseNumberFromContent(dynamic field)
    {
        //if (field?.ValueDouble != null)
        //{
        //    return (decimal)field.ValueDouble;
        //}

        if (field?.Content != null)
        {
            string content = field.Content.ToString();
            return ConvertirNumero(content);
        }

        return null;
    }

    /// <summary>
    /// Convierte una cadena con formato numérico (posiblemente con puntos y comas) a decimal.
    /// </summary>
    private static decimal? ConvertirNumero(string numeroStr)
    {
        // Limpieza inicial
        numeroStr = Regex.Replace(numeroStr, "[^0-9.,-]", "");
        numeroStr = numeroStr.Trim();

        // Detectar el separador de miles
        MatchCollection puntos = Regex.Matches(numeroStr, @"\.");
        MatchCollection comas = Regex.Matches(numeroStr, @",");

        if (puntos.Count > 0 && comas.Count > 0)
        {
            // Formato europeo: "3.456,22" -> quitar puntos y cambiar coma por punto
            if (comas[comas.Count - 1].Index > puntos[puntos.Count - 1].Index)
            {
                numeroStr = numeroStr.Replace(".", "").Replace(",", ".");
            }
            // Formato inglés: "3,456.22" -> quitar comas
            else
            {
                numeroStr = numeroStr.Replace(",", "");
            }
        }
        else if (comas.Count > 0)
        {
            // Si solo hay comas, es decimal (caso inglés), convertir la coma en punto
            numeroStr = numeroStr.Replace(",", ".");
        }

        // Convertir a número con CultureInfo que usa '.' como separador decimal
        if (double.TryParse(numeroStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double resultado))
        {
            return Convert.ToDecimal(resultado); // Convertir a decimal
                                                 //Si quieres devolverlo como string con formato español
                                                 //return resultado.ToString("0.##", new CultureInfo("es-ES"));
        }

        return null; // Devuelve null si no se pudo convertir
    }

    /// <summary>
    /// Extrae el valor del parámetro 'p' (código Base64) de una URL de QR de AFIP.
    /// </summary>
    private string GetBase64CodeFromUrl(string url)
    {
        if (string.IsNullOrEmpty(url)) return string.Empty;

        Uri uri = new Uri(url);
        string queryString = uri.Query; // Obtiene la parte de la consulta de la URL

        // Buscar el parámetro "p"
        var queryParams = System.Web.HttpUtility.ParseQueryString(queryString);
        string base64Code = queryParams["p"];

        return base64Code;
    }

    /// <summary>
    /// Decodifica una cadena Base64 a una cadena UTF8.
    /// </summary>
    private string DecodeBase64(string base64Code)
    {
        if (string.IsNullOrEmpty(base64Code)) return string.Empty;

        var bytebase64Bytes = Convert.FromBase64String(base64Code);
        return Encoding.UTF8.GetString(bytebase64Bytes);
    }

    /// <summary>
    /// Realiza validaciones de consistencia interna en un objeto Comprobante.
    /// </summary>
    /// <param name="comprobante">El comprobante a validar.</param>
    /// <param name="forCreation">Indica si la validación es para una creación (chequea existencia) o una actualización.</param>
    /// <exception cref="ComprobanteYaExisteException">Si <paramref name="forCreation"/> es true y ya existe un comprobante con el mismo número/empresa.</exception>
    /// <exception cref="Exception">Si no hay detalles.</exception>
    /// <exception cref="ComprobanteDetalleInvalidoException">Si un detalle tiene datos inconsistentes.</exception>
    /// <exception cref="ComprobanteImporteBonificacionInvalidoException">Si la bonificación total no coincide.</exception>
    /// <exception cref="ComprobanteTotalNetoInvalidoException">Si el neto total no coincide.</exception>
    /// <exception cref="ComprobantePercepcionesInvalidasException">Si una percepción tiene datos inconsistentes.</exception>
    /// <exception cref="ComprobanteTotalImporteIVAInvalido">...</exception>
    /// <exception cref="ComprobanteTotalImporteImpuestosInternosInvalido">...</exception>
    /// <exception cref="ComprobanteTotalImporteImpuestosProvInvalido">...</exception>
    /// <exception cref="ComprobanteTotalPercepcionesIVAInvalido">...</exception>
    /// <exception cref="ComprobanteTotalPercepcionesIIBBInvalido">...</exception>
    /// <exception cref="ComprobanteTotalPercepcionesMunicipalInvalido">...</exception>
    /// <exception cref="ComprobanteTotalInvalidoException">Si el total general no coincide.</exception>
    private async Task ValidateAsync(Comprobante comprobante, bool forCreation = true)
    {

        if (forCreation)
        {
            var comprobanteExiste = await context.Comprobantes.AnyAsync(c => c.Numero == comprobante.Numero && c.EmpresaId == comprobante.EmpresaId);
            if (comprobanteExiste)
            {
                throw new ComprobanteYaExisteException((int)comprobante.Numero);
            }
        }

        if (comprobante.Detalles == null || !comprobante.Detalles.Any())
        {
            throw new ComprobanteDetallesVaciosException();
        }

        int detallesIndex = 0;
        List<(int, string, string)> errores = new(); // Lista de tuplas para almacenar los errores de los detalles

        foreach (var detalle in comprobante.Detalles)
        {
            if (detalle.Cantidad <= 0)
            {
                errores.Add(new() { Item1 = detallesIndex, Item2 = nameof(detalle.Cantidad), Item3 = "La cantidad de un detalle debe ser mayor que 0." });
            }

            //// Chequear, porque si es negativo puede ser una bonificación
            //if (detalle.PrecioUnitario < 0)
            //{
            //    errores.Add(new() { Item1 = detallesIndex, Item2 = nameof(detalle.PrecioUnitario), Item3 = "El precio unitario de un detalle no puede ser negativo." });
            //}

            //if (detalle.Subtotal < 0)
            //{
            //    errores.Add(new() { Item1 = detallesIndex, Item2 = nameof(detalle.Subtotal), Item3 = "El subtotal de un detalle no puede ser negativo." });
            //}

            if (string.IsNullOrEmpty(detalle.Detalle))
            {
                errores.Add(new() { Item1 = detallesIndex, Item2 = nameof(detalle.Detalle), Item3 = "El detalle no puede estar vacío." });
            }

            var subtotalEsperado = detalle.PrecioUnitario * detalle.Cantidad - detalle.ImporteBonificacion;

            var tolerancia = 0.01m * detalle.Cantidad;

            var limiteInferior = detalle.Subtotal - tolerancia;
            var limiteSuperior = detalle.Subtotal + tolerancia;

            if (subtotalEsperado < limiteInferior || subtotalEsperado > limiteSuperior)
            {
                errores.Add(new()
                {
                    Item1 = detallesIndex,
                    Item2 = nameof(detalle.Subtotal),
                    Item3 = $"El subtotal calculado ({subtotalEsperado:F2}) está fuera del rango de tolerancia ({limiteInferior:F2} a {limiteSuperior:F2}) permitido alrededor del Subtotal declarado ({detalle.Subtotal:F2})."
                });
            }
            detallesIndex++;
        }

        if (errores.Count > 0)
        {
            throw new ComprobanteDetalleInvalidoException(errores);
        }

        decimal totalBonificaciones = comprobante.Detalles.Sum(d => d.ImporteBonificacion);

        if (totalBonificaciones != comprobante.ImporteBonificacion)
        {
            throw new ComprobanteImporteBonificacionInvalidoException();
        }

        // La suma de los ítems sin impuestos menos las bonificaciones es igual al total neto
        decimal sumaItemsSinImpuestos = comprobante.Detalles.Sum(d => d.PrecioUnitario * d.Cantidad - d.ImporteBonificacion);
        //decimal totalNetoEsperado = sumaItemsSinImpuestos - totalBonificaciones;

        if (Math.Abs((decimal)(comprobante.ImporteNeto - sumaItemsSinImpuestos)) > 0.01m)
        {
            throw new ComprobanteTotalNetoInvalidoException();
        }

        int percepcionesIndex = 0;
        List<(int, string, string)> erroresPercepciones = new();

        foreach (var item in comprobante.Percepciones)
        {
            if (string.IsNullOrEmpty(item.Descripcion))
            {
                erroresPercepciones.Add(new() { Item1 = percepcionesIndex, Item2 = nameof(item.Descripcion), Item3 = "La descripción no puede estar vacía." });
            }

            var subtotalEsperado = item.Alicuota * comprobante.ImporteNeto / 100;
            if (Math.Abs((decimal)(item.ImporteTotal - subtotalEsperado)) > 0.01m)
            {
                erroresPercepciones.Add(new() { Item1 = percepcionesIndex, Item2 = nameof(item.ImporteTotal), Item3 = "El subtotal no es válido." });
            }

            percepcionesIndex++;
        }

        if (erroresPercepciones.Count > 0)
        {
            throw new ComprobantePercepcionesInvalidasException(erroresPercepciones);
        }

        var impuestosIVA = comprobante.Impuestos?.Where(i => i.Impuesto.TipoId == ImpuestoTipo.IVA)?.ToList();

        if (impuestosIVA != null && impuestosIVA.Count > 0)
        {
            var totalImpuestosIVA = impuestosIVA?.Sum(i => i.ImporteTotal) ?? 0;

            if (Math.Abs(comprobante.ImporteIVA - totalImpuestosIVA) > 0.01m)
            {
                throw new ComprobanteTotalImporteIVAInvalido();
            }
        }

        var impuestosInternos = comprobante.Impuestos?.Where(i => i.Impuesto.TipoId == ImpuestoTipo.INTERNO)?.ToList();

        if (impuestosInternos != null && impuestosInternos.Count > 0)
        {
            var totalImpuestosInternos = impuestosInternos?.Sum(i => i.ImporteTotal) ?? 0;

            if (Math.Abs((int)comprobante.ImporteImpuestosInternos - totalImpuestosInternos) > 0.01m)
            {
                throw new ComprobanteTotalImporteImpuestosInternosInvalido();
            }
        }


        var impuestosProv = comprobante.Impuestos?.Where(i => i.Impuesto.TipoId == ImpuestoTipo.PROVINCIAL)?.ToList();

        if (impuestosProv != null && impuestosProv.Count > 0)
        {
            var totalImpuestosProv = impuestosProv?.Sum(i => i.ImporteTotal) ?? 0;

            if (Math.Abs((int)comprobante.ImporteOtrosTributosProv - totalImpuestosProv) > 0.01m)
            {
                throw new ComprobanteTotalImporteImpuestosProvInvalido();
            }
        }

        var percepcionesIVA = comprobante.Percepciones?.Where(p => p.Percepcion.PercepcionTipoId == PercepcionTipo.IVA)?.ToList();

        if (percepcionesIVA != null && percepcionesIVA.Count > 0)
        {
            var totalPercepcionesIVA = percepcionesIVA?.Sum(i => i.ImporteTotal) ?? 0;

            if (Math.Abs(comprobante.ImportePercepcionIVA - totalPercepcionesIVA) > 0.01m)
            {
                throw new ComprobanteTotalPercepcionesIVAInvalido();
            }
        }


        var percepcionesIIBB = comprobante.Percepciones?.Where(p => p.Percepcion.PercepcionTipoId == PercepcionTipo.IIBB)?.ToList();

        if (percepcionesIIBB != null && percepcionesIIBB.Count > 0)
        {
            var totalPercepcionesIIBB = percepcionesIIBB?.Sum(i => i.ImporteTotal) ?? 0;

            if (Math.Abs(comprobante.ImportePercepcionIIBB - totalPercepcionesIIBB) > 0.01m)
            {
                throw new ComprobanteTotalPercepcionesIIBBInvalido();
            }
        }

        var percepcionesMunicipales = comprobante.Percepciones?.Where(p => p.Percepcion.PercepcionTipoId == PercepcionTipo.MUNICIPALES)?.ToList();

        if (percepcionesMunicipales != null && percepcionesMunicipales.Count > 0)
        {
            var totalPercepcionesMunicipales = percepcionesMunicipales?.Sum(i => i.ImporteTotal) ?? 0;

            if (Math.Abs(comprobante.ImportePercepcionMunicipal - totalPercepcionesMunicipales) > 0.01m)
            {
                throw new ComprobanteTotalPercepcionesMunicipalInvalido();
            }
        }

        decimal totalEsperado = (decimal)(comprobante.ImporteNeto + comprobante.ImporteIVA +
                        comprobante.ImportePercepcionIVA + comprobante.ImportePercepcionIIBB +
                        comprobante.ImportePercepcionMunicipal + comprobante.ImporteImpuestosInternos +
                        comprobante.ImporteOtrosTributosProv);

        if (Math.Abs(comprobante.ImporteTotal - totalEsperado) > 0.01m)
        {
            throw new ComprobanteTotalInvalidoException(totalEsperado, comprobante.ImporteTotal);
        }
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Implementación concreta que realiza la verificación externa:
    /// 1. Mapea los datos de la <paramref name="cabecera"/> al DTO requerido por el servicio de validación (<c>ValidarComprobanteRequestDto</c>).
    /// 2. Invoca al <see cref="validarComprobanteService"/> para realizar la consulta contra la autoridad fiscal (ej. AFIP).
    /// 3. Construye y devuelve un objeto <see cref="IComprobanteConstatacionResult"/> basado en la respuesta del servicio externo, indicando el estado (Ok/Error) y las observaciones.
    /// </remarks>
    public async Task<IComprobanteConstatacionResult> VerifyAsync(IComprobanteCabecera cabecera, long cuitCompany)
    {

        var request = new ValidarComprobanteRequestDto()
        {
            Modo = cabecera.tipoCodAut switch
            {
                "E" => nameof(CodigoAutorizacionTipo.CAE),
                "I" => nameof(CodigoAutorizacionTipo.CAI),
                "A" => nameof(CodigoAutorizacionTipo.CAEA),
                _ => nameof(CodigoAutorizacionTipo.CAE),
            },
            CuitEmisor = cabecera.cuit,
            PtoVta = cabecera.ptoVta,
            CbteTipo = cabecera.tipoCmp,
            CbteNro = cabecera.nroCmp,
            CbteFch = DateOnly.Parse(cabecera.fecha),
            ImpTotal = decimal.ToDouble(cabecera.importe),
            CodAutorizacion = cabecera.codAut.ToString(),
            // check
            DocNroReceptor = cabecera.nroDocRec,
            DocTipoReceptor = 80
        };

        //var response = await validarComprobanteService.ValidarComprobanteAsync(cuitCompany, request);
        var response = await validarComprobanteService.ValidarComprobanteAsync(30707466967, request); // TODO: quitar el cuit de globalsis hardcodeado cuando se defina de donde va a salir el cuit operador


        var result = new ComprobanteConstatacionResult(response.Autorizado ? EstadoConstatacion.Ok : EstadoConstatacion.Rechazado, response.Observaciones);

        return result;
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Cambia el estado del comprobante a 'APROBADA_CLIENTE'.
    /// Utiliza RowVersion para concurrencia.
    /// </remarks>
    public async Task ApproveAsync(int id, IComprobanteApprove approveParameters)
    {
        var comprobante = await GetAsync(id);

        if (comprobante.EstadoValidacionARCAId == EstadoValidacionARCA.NO_VALIDADA)
        {
            throw new ComprobanteSinConstatarEnARCAException();
        }

        if (comprobante.ComprobanteEstadoId == ComprobanteEstado.APROBADA_CLIENTE)
        {
            throw new ComprobanteYaFueAprobadoException();
        }
        comprobante.ComprobanteEstadoId = ComprobanteEstado.APROBADA_CLIENTE;
        comprobante.RowVersion = approveParameters.RowVersion;
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Cambia el estado del comprobante a 'RECHAZADA_CLIENTE' y registra motivo/usuario/fecha.
    /// Utiliza RowVersion para concurrencia.
    /// </remarks>
    public async Task RejectAsync(int id, IComprobanteReject rejectParameters)
    {
        var comprobante = await GetAsync(id);
        comprobante.ComprobanteEstadoId = ComprobanteEstado.RECHAZADA_CLIENTE;
        comprobante.UsuarioRechazo = rejectParameters.UsuarioRechazo;
        comprobante.MotivoRechazo = rejectParameters.MotivoRechazo;
        comprobante.FechaRechazo = DateTime.Now;
        comprobante.RowVersion = rejectParameters.RowVersion;
    }

    /// <summary>
    /// Verifica el estado de un comprobante existente contra el servicio externo ARCA/AFIP
    /// y actualiza su estado de validación interno.
    /// </summary>
    /// <param name="comprobante">El comprobante a verificar.</param>
    /// <returns>Una tarea que representa la operación asíncrona.</returns>
    /// <exception cref="ComprobanteInexistenteException">Si no se encuentra el comprobante con el ID especificado.</exception>
    /// <exception cref="InvalidOperationException">Si ocurre un error durante el mapeo de datos necesarios para la verificación.</exception>
    /// <remarks>
    /// Actualiza el campo <c>EstadoValidacionARCAId</c> del comprobante.
    /// La persistencia de los cambios debe manejarse externamente (ej. Unit of Work).
    /// </remarks>
    public async Task VerifyEstadoARCAAsync(int comprobanteId, byte[] rowVersion)
    {
        var comprobante = await GetAsync(comprobanteId);

        comprobante.RowVersion = rowVersion;

        // Verificación del QR en ARCA, solamente si hubo error de comunicación con la api de AFIP
        await VerifyQRAsync(comprobante);

        ComprobanteCabecera cabeceraParaVerificar;
        try
        {
            cabeceraParaVerificar = MapComprobanteToCabeceraAfip(comprobante);
        }
        catch (Exception mapEx)
        {
            Log.Logger.Error($"ERROR: Falla al mapear datos del Comprobante {comprobante.Numero:D8} para verificación ARCA. Exception: {mapEx.Message}");
            comprobante.EstadoValidacionARCAId = EstadoValidacionARCA.ERROR_VALIDACION;
            comprobante.RowVersion = comprobante.RowVersion;
            return;
        }

        IComprobanteConstatacionResult constatacionResult;
        short nuevoEstadoArca;
        string observacionesArca = null;

        try
        {
            var companyExtra = await context.CompanyExtras.FirstOrDefaultAsync(c => c.CompanyId == comprobante.CompanyId);
            constatacionResult = await VerifyAsync(cabeceraParaVerificar, long.TryParse(companyExtra.IdentificadorTributario, out long cuitCompany) ? cuitCompany : 0); // TODO: 0 por defecto, hsbria que evaluar si es posible que no exista una CompanyExtra para la company del comprobante (entiendo que no)

            switch (constatacionResult.Estado)
            {
                case EstadoConstatacion.Ok:
                    nuevoEstadoArca = EstadoValidacionARCA.VALIDADA;
                    observacionesArca = "Validado OK por ARCA/AFIP.";
                    if (constatacionResult.Observaciones != null && constatacionResult.Observaciones.Any())
                    {
                        observacionesArca += " Obs: " + string.Join("; ", (IEnumerable<string>)constatacionResult.Observaciones);
                    }
                    break;

                case EstadoConstatacion.Error:
                default:
                    nuevoEstadoArca = EstadoValidacionARCA.RECHAZADA;
                    if (constatacionResult.Observaciones != null && constatacionResult.Observaciones.Any())
                    {
                        observacionesArca = string.Join("; ", (IEnumerable<string>)constatacionResult.Observaciones);
                    }
                    else
                    {
                        observacionesArca = "Rechazado por ARCA/AFIP (sin observaciones detalladas).";
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            // Manejo de errores durante la llamada a VerifyAsync
            Log.Logger.Error($"ERROR: Falla técnica al verificar el comprobante {comprobante.Numero:D8} contra ARCA/AFIP. Exception: {ex.Message}");
            nuevoEstadoArca = EstadoValidacionARCA.ERROR_VALIDACION;
            observacionesArca = $"Error técnico durante validación: {ex.Message}";
        }

        //// Si es un comprobante nuevo o en borrador 
        //if (comprobante.ComprobanteEstadoId == ComprobanteEstado.BORRADOR)
        //{
        //    comprobante.ComprobanteEstadoId = ComprobanteEstado.EN_PROCESO_CARGA;
        //} <--------------------- CHEQUEAR si esto sigue valiendo

        comprobante.ObservacionesARCA = observacionesArca;
        comprobante.EstadoValidacionARCAId = nuevoEstadoArca;
        comprobante.RowVersion = comprobante.RowVersion;
    }

    private async Task VerifyQRAsync(Comprobante comprobante)
    {
        if (comprobante.EstadoValidacionARCAQRId == EstadoValidacionARCA.ERROR_VALIDACION)
        {
            IComprobanteCabecera cabecera = JsonConvert.DeserializeObject<ComprobanteCabecera>(comprobante.QRValor);
            try
            {
                var companyExtra = await context.CompanyExtras.FirstOrDefaultAsync(c => c.CompanyId == comprobante.CompanyId);
                var resultadoConstatacion = await VerifyAsync(cabecera, long.TryParse(companyExtra.IdentificadorTributario, out long cuitCompany) ? cuitCompany : 0);

                comprobante.EstadoValidacionARCAQRId = (short?)resultadoConstatacion.Estado;
                if (resultadoConstatacion.Observaciones != null && resultadoConstatacion.Observaciones.Any())
                {
                    comprobante.ObservacionesARCAQR = "Obs: " + string.Join("; ", (IEnumerable<string>)resultadoConstatacion.Observaciones);
                }
            }
            catch (Exception)
            {

                comprobante.EstadoValidacionARCAQRId = (short?)EstadoConstatacion.Error;
            }
        }
    }

    /// <summary>
    /// Mapea una entidad Comprobante a la estructura ComprobanteCabecera requerida por AFIP/QR.
    /// </summary>
    /// <param name="comprobante">La entidad Comprobante obtenida de la base de datos (con relaciones incluidas).</param>
    /// <returns>Un objeto ComprobanteCabecera poblado.</returns>
    /// <exception cref="InvalidOperationException">Si faltan datos críticos o hay errores de conversión.</exception>
    private ComprobanteCabecera MapComprobanteToCabeceraAfip(Comprobante comprobante)
    {
        // --- CUIT Emisor ---
        if (!long.TryParse(comprobante.NroIdentificacionFiscalPro?.Trim(), out long cuitEmisor))
        {
            throw new InvalidOperationException($"CUIT del emisor inválido o faltante: '{comprobante.NroIdentificacionFiscalPro}'");
        }

        // --- Tipo Comprobante (Requiere Código AFIP de la entidad relacionada) ---
        if (comprobante.ComprobanteTipo == null || string.IsNullOrWhiteSpace(comprobante.ComprobanteTipo.CodigoArca))
        {
            throw new InvalidOperationException($"Falta ComprobanteTipo o su Código AFIP/ARCA asociado para el ID: {comprobante.ComprobanteTipoId}");
        }
        if (!int.TryParse(comprobante.ComprobanteTipo.CodigoArca, out int tipoCmpAfip))
        {
            throw new InvalidOperationException($"No se pudo convertir el Código AFIP/ARCA '{comprobante.ComprobanteTipo.CodigoArca}' a entero.");
        }

        // --- Código Autorización (CAE/CAEA/CAI) ---
        if (!long.TryParse(comprobante.CodigoAutorizacion?.Trim(), out long codAutAfip))
        {
            throw new InvalidOperationException($"Código de Autorización inválido o faltante: '{comprobante.CodigoAutorizacion}'");
        }

        // --- Tipo Código Autorización (ID a Letra) ---
        string tipoCodAutAfip = MapTipoCodAutIdToLetra((short)comprobante.TipoCodigoAutorizacionId);

        // --- Documento Receptor ---
        long nroDocRecAfip = default;
        int tipoDocRecAfip = default; // Default Consumidor Final
        if (!string.IsNullOrWhiteSpace(comprobante.NroIdentificacionFiscalCompany))
        {
            if (!long.TryParse(comprobante.NroIdentificacionFiscalCompany.Trim(), out nroDocRecAfip))
            {
                throw new InvalidOperationException($"Documento del receptor inválido o faltante: '{comprobante.NroIdentificacionFiscalCompany}'");
            }
            // Mapear CategoriaTipoReceptorId (de la entidad relacionada) a tipoDocRec AFIP
            tipoDocRecAfip = MapCategoriaReceptorToTipoDocAfip(comprobante.CategoriaTipoReceptor); // Pasar el objeto relacionado
        }

        // --- Moneda (ID a Código AFIP) ---
        if (comprobante.Moneda == null || string.IsNullOrWhiteSpace(comprobante.Moneda.Code))
        {
            throw new InvalidOperationException($"Falta Moneda o su Código asociado para el ID: {comprobante.MonedaId}");
        }
        string monedaAfip = comprobante.Moneda.Code == "ARS" ? "PES" : comprobante.Moneda.Code;

        // --- Cotización ---
        // Default a 1 para ARS/PES. Para otras monedas, necesitaríamos obtenerla (posiblemente de la entidad Moneda si tiene cotización).
        // decimal cotizacion = (monedaAfip == "PES") ? 1m : (comprobante.Moneda.Cotizacion ?? 1m); // Ejemplo si Moneda tiene Cotizacion
        // string ctzAfip = cotizacion.ToString("F6", CultureInfo.InvariantCulture); // Formato AFIP con 6 decimales
        var ctzAfip = comprobante.Cotizacion;

        return new ComprobanteCabecera
        {
            Id = comprobante.Id,
            ver = 1,
            fecha = comprobante.FechaEmision?.ToString("yyyy-MM-dd"),
            cuit = cuitEmisor,
            ptoVta = (int)comprobante.PuntoVenta,
            tipoCmp = tipoCmpAfip,
            nroCmp = (int)comprobante.Numero,
            importe = (int)comprobante.ImporteTotal,
            moneda = monedaAfip,
            ctz = ctzAfip,
            tipoDocRec = tipoDocRecAfip,
            nroDocRec = nroDocRecAfip,
            tipoCodAut = tipoCodAutAfip,
            codAut = codAutAfip
        };
    }

    /// <summary>
    /// Mapea el ID interno del Tipo de Código de Autorización a la letra requerida por AFIP.
    /// </summary>
    private string MapTipoCodAutIdToLetra(short tipoCodAutId)
    {
        // Asumiendo que CodigoAutorizacionTipo es una clase estática con constantes short
        return tipoCodAutId switch
        {
            CodigoAutorizacionTipo.CAE => "E",
            CodigoAutorizacionTipo.CAEA => "A",
            CodigoAutorizacionTipo.CAI => "I", // Verificar código correcto para CAI
            _ => throw new InvalidOperationException($"Tipo de Código de Autorización ID desconocido: {tipoCodAutId}")
        };
    }

    /// <summary>
    /// Mapea la entidad Categoría del Receptor al código de Tipo de Documento de AFIP.
    /// </summary>
    /// <param name="categoriaReceptor">La entidad CategoriaTipo relacionada.</param>
    private int MapCategoriaReceptorToTipoDocAfip(CategoriaTipo categoriaReceptor)
    {
        if (categoriaReceptor == null) return 99; // Consumidor Final si no hay categoría

        // Mapeo basado en Descripción (MENOS RECOMENDADO, más frágil)
        string descLower = categoriaReceptor.Descripcion?.ToLowerInvariant() ?? "";
        if (descLower.Contains("cuit")) return 80;
        if (descLower.Contains("cuil")) return 86;
        if (descLower.Contains("dni") || descLower.Contains("documento nacional")) return 96;
        if (descLower.Contains("consumidor final")) return 99;

        Console.WriteLine($"WARN: Mapeo no definido para CategoriaTipoReceptor '{categoriaReceptor.Descripcion}'. Usando CUIT (80) por defecto.");
        return 80;
    }

    // Helper hasattr (ya definido previamente)
    private bool hasattr(object obj, string propertyName)
    {
        return obj.GetType().GetProperty(propertyName) != null;
    }

    public async Task ConfirmAsync(int id, IComprobanteConfirm parameters)
    {
        var comprobante = await GetAsync(id);

        if (comprobante.EstadoValidacionARCAId == EstadoValidacionARCA.NO_VALIDADA)
        {
            throw new ComprobanteSinConstatarEnARCAException();
        }

        comprobante.ComprobanteEstadoId = ComprobanteEstado.CONFIRMADO;

        // Regla: cuando se confirma el comprobante, siempre pasa a propietario BACKOFFICE
        comprobante.PropietarioActualId = Origen.BACKOFFICE;
        comprobante.RowVersion = parameters.RowVersion;
    }

    public async Task AuthorizeAsync(int id, IComprobanteAuthorize parameters)
    {
        var comprobante = await GetAsync(id);
        if (comprobante.EstadoValidacionARCAId == EstadoValidacionARCA.NO_VALIDADA)
        {
            throw new ComprobanteSinConstatarEnARCAException();
        }

        if (comprobante.PeriodoId is null)
        {
            throw new ComprobantePeriodoRequeridoException();
        }

        comprobante.ComprobanteEstadoId = ComprobanteEstado.AUTORIZADO;
        comprobante.RowVersion = parameters.RowVersion;
    }

    public async Task UpdatePeriodoAsync(int id, IComprobantePeriodoUpdate parameters)
    {
        if (parameters.PeriodoId is null)
        {
            throw new ArgumentNullException(nameof(parameters.PeriodoId));
        }

        var comprobante = await GetAsync(id);

        comprobante.PeriodoId = parameters.PeriodoId;
        comprobante.RowVersion = parameters.RowVersion;
    }
}

#endregion

#region Clases DTO Concretas

/// <summary>
/// Implementación concreta de <see cref="IComprobanteConstatacionResult"/>.
/// </summary>
public class ComprobanteConstatacionResult : IComprobanteConstatacionResult
{
    /// <inheritdoc/>
    public EstadoConstatacion Estado { get; }
    /// <inheritdoc/>
    public ICollection<string> Observaciones { get; }

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="ComprobanteConstatacionResult"/>.
    /// </summary>
    /// <param name="estado">El estado de la constatación.</param>
    /// <param name="observaciones">Las observaciones asociadas. Este parámetro es opcional.</param>
    public ComprobanteConstatacionResult(EstadoConstatacion estado, ICollection<string> observaciones = null)
    {
        Estado = estado;
        Observaciones = observaciones ?? new List<string>();
    }
}

/// <summary>
/// Implementación concreta de <see cref="IComprobanteAnalysisResult"/>. Representa los datos extraídos de un comprobante.
/// </summary>
public class ComprobanteAnalysisResult : IComprobanteAnalysisResult
{
    /// <inheritdoc/>
    public string RazonSocialPro { get; set; }
    /// <inheritdoc/>
    public string NroIdentificacionFiscalPro { get; set; }
    /// <inheritdoc/>
    public short? CategoriaTipoEmisorId { get; set; }
    /// <inheritdoc/>
    public short? ComprobanteTipoId { get; set; }
    /// <inheritdoc/>
    public short? CategoriaTipoReceptorId { get; set; }
    /// <inheritdoc/>
    public string NroIdentificacionFiscalCompany { get; set; }
    /// <inheritdoc/>
    public string PuntoVenta { get; set; }
    /// <inheritdoc/>
    public string Letra { get; set; }
    /// <inheritdoc/>
    public string Numero { get; set; }
    /// <inheritdoc/>
    public string FechaEmision { get; set; }
    /// <inheritdoc/>
    public short? TipoCodigoAutorizacionId { get; set; }
    /// <inheritdoc/>
    public string CodigoAutorizacion { get; set; }
    /// <inheritdoc/>
    public long? MonedaId { get; set; }
    /// <inheritdoc/>
    public decimal? ImporteNeto { get; set; } = 0;
    /// <inheritdoc/>
    public decimal? ImporteTotal { get; set; } = 0;
    /// <inheritdoc/>
    public decimal? ImporteIVA { get; set; } = 0;
    /// <inheritdoc/>
    public decimal? ImporteBonificacion { get; set; } = 0;
    /// <inheritdoc/>
    public decimal? ImportePercepcionIVA { get; set; } = 0;
    /// <inheritdoc/>
    public decimal? ImportePercepcionIIBB { get; set; } = 0;
    /// <inheritdoc/>
    public decimal? ImportePercepcionMunicipal { get; set; } = 0;
    /// <inheritdoc/>
    public decimal? ImporteImpuestosInternos { get; set; } = 0;
    /// <inheritdoc/>
    public decimal? ImporteOtrosTributosProv { get; set; } = 0;
    /// <inheritdoc/>   
    public string CodigoHES { get; set; }
    /// <inheritdoc/>
    public string Comentarios { get; set; }
    /// <inheritdoc/>
    public List<IComprobanteDetalleAnalysisResult> Detalles { get; set; } = new();
    /// <inheritdoc/>
    public List<IComprobanteImpuestoAnalysisResult> Impuestos { get; set; } = new();
    /// <inheritdoc/>
    public List<IComprobantePercepcionAnalysisResult> Percepciones { get; set; } = new();
    /// <inheritdoc/>
    public bool? ValidacionQR { get; set; } = false;
    /// <inheritdoc/>
    public string QRValor { get; set; }
    /// <inheritdoc/>
    public string FileName { get; set; }
    /// <inheritdoc/>
    public string URL { get; set; }
    /// <inheritdoc/>
    public string FolderPath { get; set; }
    /// <inheritdoc/>
    /// <remarks>Por defecto, es Sin Constatar.</remarks>
    public EstadoConstatacion EstadoConstatacionQR { get; set; } = EstadoConstatacion.SinConstatar;
    /// <inheritdoc/>
    /// <remarks>Por defecto, es Sin Constatar.</remarks>
    public EstadoConstatacion EstadoConstatacion { get; set; } = EstadoConstatacion.SinConstatar;
    /// <inheritdoc/>
    public string ObservacionesARCA { get; set; }
    /// <inheritdoc/>
    public string ObservacionesARCAQR { get; set; }
    /// <inheritdoc/>
    public DateTime? FechaVencimiento { get; set; }
    /// <inheritdoc/>
    public DateTime? FechaVencimientoCodigoAutorizacion { get; set; }
    /// <inheritdoc/>
    public decimal? Cotizacion { get; set; }
    /// <inheritdoc/>
    public string NroRemito { get; set; }
    /// <inheritdoc/>
    public string NroOrdenCompra { get; set; }

    public short? CondicionVentaId { get; set; } = CondicionVenta.OTRO;
}

/// <summary>
/// Implementación concreta de <see cref="IComprobanteDetalleAnalysisResult"/>. Representa un ítem extraído.
/// </summary>
public class ComprobanteDetalleAnalysisResult : IComprobanteDetalleAnalysisResult
{
    /// <inheritdoc/>
    public short? UnidadMedidaId { get; set; }
    /// <inheritdoc/>
    public int? Cantidad { get; set; }
    /// <inheritdoc/>
    public decimal? PrecioUnitario { get; set; }
    /// <inheritdoc/>
    public decimal? ImporteBonificacion { get; set; }
    /// <inheritdoc/>
    public decimal? Subtotal { get; set; }
    /// <inheritdoc/>
    public decimal? ImporteIVA { get; set; } = 0;
    /// <inheritdoc/>
    public bool? Exento { get; set; }
    /// <inheritdoc/>
    public decimal? Alicuota { get; set; }
    /// <inheritdoc/>
    public int? OrdenCompraId { get; set; }
    /// <inheritdoc/>
    public string Detalle { get; set; }
    /// <inheritdoc/>
    public int? ImpuestoIVAId { get; set; }
}

/// <summary>
/// Implementación concreta de <see cref="IComprobanteImpuestoAnalysisResult"/>. Representa un impuesto extraído.
/// </summary>
public class ComprobanteImpuestoAnalysisResult : IComprobanteImpuestoAnalysisResult
{
    /// <inheritdoc/>
    public int? ImpuestoId { get; set; }
    /// <inheritdoc/>
    public string Descripcion { get; set; }
    /// <inheritdoc/>
    public decimal? ImporteTotal { get; set; } = 0;
    /// <inheritdoc/>
    public decimal? AlicuotaValor { get; set; }
    /// <inheritdoc/>
    public short? TipoId { get; set; }
}

/// <summary>
/// Implementación concreta de <see cref="IComprobantePercepcionAnalysisResult"/>. Representa una percepción extraída.
/// </summary>
public class ComprobantePercepcionAnalysisResult : IComprobantePercepcionAnalysisResult
{
    /// <inheritdoc/>
    public int? PercepcionId { get; set; }
    /// <inheritdoc/>
    public string Descripcion { get; set; }
    /// <inheritdoc/>
    public decimal? Alicuota { get; set; } = 0;
    /// <inheritdoc/>
    public decimal? ImporteTotal { get; set; } = 0;
    /// <inheritdoc/>
    public short? TipoId { get; set; }
}

/// <summary>
/// Implementación concreta de <see cref="IComprobanteCreate"/>. Usado para enviar datos para crear un comprobante.
/// </summary>
public class ComprobanteCreate : IComprobanteCreate
{
    /// <inheritdoc/>
    public string NroIdentificacionFiscalPro { get; set; }
    /// <inheritdoc/>
    public short? CategoriaTipoEmisorId { get; set; }
    /// <inheritdoc/>
    public short? ComprobanteTipoId { get; set; }
    /// <inheritdoc/>
    public short? CategoriaTipoReceptorId { get; set; }
    /// <inheritdoc/>
    public string NroIdentificacionFiscalCompany { get; set; }
    /// <inheritdoc/>
    public int PuntoVenta { get; set; }
    /// <inheritdoc/>
    public string Letra { get; set; }
    /// <inheritdoc/>
    public int Numero { get; set; }
    /// <inheritdoc/>
    public DateTime? FechaEmision { get; set; }
    /// <inheritdoc/>
    public short? TipoCodigoAutorizacionId { get; set; }
    /// <inheritdoc/>
    public string CodigoAutorizacion { get; set; }
    /// <inheritdoc/>
    public long? MonedaId { get; set; }
    /// <inheritdoc/>
    public decimal ImporteNeto { get; set; }
    /// <inheritdoc/>
    public decimal ImporteTotal { get; set; }
    /// <inheritdoc/>
    public decimal ImporteIVA { get; set; }
    /// <inheritdoc/>
    public decimal ImporteBonificacion { get; set; } = decimal.Zero;
    /// <inheritdoc/>
    public decimal ImportePercepcionIVA { get; set; }
    /// <inheritdoc/>
    public decimal ImportePercepcionIIBB { get; set; }
    /// <inheritdoc/>
    public decimal ImportePercepcionMunicipal { get; set; }
    /// <inheritdoc/>
    public decimal ImporteImpuestosInternos { get; set; }
    /// <inheritdoc/>
    public decimal ImporteOtrosTributosProv { get; set; }
    /// <inheritdoc/>
    public int? EmpresaId { get; set; }
    /// <inheritdoc/>
    public long? CompanyId { get; set; }
    /// <inheritdoc/>
    public string CodigoHES { get; set; }
    /// <inheritdoc/>
    public int? ComprobanteEstadoId { get; set; }
    /// <inheritdoc/>
    public string Comentarios { get; set; }
    /// <inheritdoc/>
    public List<IComprobanteDetalleCreate> Detalles { get; set; } = new();
    /// <inheritdoc/>
    public List<IComprobanteImpuestoCreate> Impuestos { get; set; } = new();
    /// <inheritdoc/>
    public List<IComprobantePercepcionCreate> Percepciones { get; set; } = new();
    /// <inheritdoc/>
    public bool? ValidacionQR { get; set; } = false;
    /// <inheritdoc/>
    public string QRValor { get; set; }
    public EstadoConstatacion EstadoConstatacion { get; set; }
    /// <inheritdoc/>
    public EstadoConstatacion EstadoConstatacionQR { get; set; }
    /// <inheritdoc/>
    public string ObservacionesARCA { get; set; }
    /// <inheritdoc/>
    public string ObservacionesARCAQR { get; set; }
    /// <inheritdoc/>
    public decimal? Cotizacion { get; set; }
    /// <inheritdoc/>
    public string NroRemito { get; set; }
    /// <inheritdoc/>
    public string NroOrdenCompra { get; set; }
    /// <inheritdoc/>
    public short OrigenId { get; set; }
    /// <inheritdoc/>
    public short PropietarioActualId { get; set; }
    /// <inheritdoc/>
    public string FileName { get; set; }
    /// <inheritdoc/>
    public short? CondicionVentaId { get; set; }
    /// <inheritdoc/>
    public DateTime? FechaVencimiento { get; set; }
    /// <inheritdoc/>
    public DateTime? FechaVencimientoCodigoAutorizacion { get; set; }
}




/// <summary>
/// Implementación concreta de <see cref="IComprobanteUpdate"/>. Usado para enviar datos para actualizar un comprobante.
/// </summary>
public class ComprobanteUpdate : IComprobanteUpdate
{
    /// <inheritdoc/>
    public int Id { get; set; }
    /// <inheritdoc/>
    public string NroIdentificacionFiscalPro { get; set; }
    /// <inheritdoc/>
    public short CategoriaTipoEmisorId { get; set; }
    /// <inheritdoc/>
    public short ComprobanteTipoId { get; set; }
    /// <inheritdoc/>
    public short? CategoriaTipoReceptorId { get; set; }
    /// <inheritdoc/>
    public string NroIdentificacionFiscalCompany { get; set; }
    /// <inheritdoc/>
    public int PuntoVenta { get; set; }
    /// <inheritdoc/>
    public string Letra { get; set; }
    /// <inheritdoc/>
    public int Numero { get; set; }
    /// <inheritdoc/>
    public DateTime FechaEmision { get; set; }
    /// <inheritdoc/>
    public short TipoCodigoAutorizacionId { get; set; }
    /// <inheritdoc/>
    public string CodigoAutorizacion { get; set; }
    /// <inheritdoc/>
    public long MonedaId { get; set; }
    /// <inheritdoc/>
    public decimal ImporteNeto { get; set; }
    /// <inheritdoc/>
    public decimal ImporteTotal { get; set; }
    /// <inheritdoc/>
    public decimal ImporteIVA { get; set; }
    /// <inheritdoc/>
    public decimal ImporteBonificacion { get; set; }
    /// <inheritdoc/>
    public decimal ImportePercepcionIVA { get; set; }
    /// <inheritdoc/>
    public decimal ImportePercepcionIIBB { get; set; }
    /// <inheritdoc/>
    public decimal ImportePercepcionMunicipal { get; set; }
    /// <inheritdoc/>
    public decimal ImporteImpuestosInternos { get; set; }
    /// <inheritdoc/>
    public decimal ImporteOtrosTributosProv { get; set; }
    /// <inheritdoc/>
    public int? EmpresaId { get; set; }
    /// <inheritdoc/>
    public long? CompanyId { get; set; }
    /// <inheritdoc/>
    public string CodigoHES { get; set; }
    /// <inheritdoc/>
    public int? ComprobanteEstadoId { get; set; }
    /// <inheritdoc/>
    public string Comentarios { get; set; }
    /// <inheritdoc/>
    public List<IComprobanteDetalleUpdate> Detalles { get; set; } = new();
    /// <inheritdoc/>
    public List<IComprobanteImpuestoUpdate> Impuestos { get; set; } = new();
    /// <inheritdoc/>
    public List<IComprobantePercepcionUpdate> Percepciones { get; set; } = new();
    /// <inheritdoc/>
    public bool? ValidacionQR { get; set; } = false;
    /// <inheritdoc/>
    public string QRValor { get; set; }
    /// <inheritdoc/>
    public byte[] RowVersion { get; set; }
    /// <inheritdoc/>
    public decimal? Cotizacion { get; set; }
    /// <inheritdoc/>
    public string NroRemito { get; set; }
    /// <inheritdoc/>
    public string NroOrdenCompra { get; set; }
    /// <inheritdoc/>
    public short? CondicionVentaId { get; set; }
    /// <inheritdoc/>
    public short OrigenId { get; set; }
    /// <inheritdoc/>
    public DateTime? FechaVencimiento { get; set; }
    /// <inheritdoc/>
    public DateTime? FechaVencimientoCodigoAutorizacion { get; set; }
}

/// <summary>
/// Implementación concreta de <see cref="IComprobanteDetalleCreate"/>. Usado dentro de <see cref="ComprobanteCreate"/>.
/// </summary>
public class ComprobanteDetalleCreate : IComprobanteDetalleCreate
{
    /// <inheritdoc/>
    public short? UnidadMedidaId { get; set; } = UnidadMedida.UNIDAD;
    /// <inheritdoc/>
    public int Cantidad { get; set; } = 1;
    /// <inheritdoc/>
    public decimal PrecioUnitario { get; set; } = decimal.Zero;
    /// <inheritdoc/>
    public decimal? ImporteBonificacion { get; set; } = decimal.Zero;
    /// <inheritdoc/>
    public decimal Subtotal { get; set; } = decimal.Zero;
    /// <inheritdoc/>
    public decimal? ImporteIVA { get; set; } = 0;
    /// <inheritdoc/>
    public bool? Exento { get; set; }
    /// <inheritdoc/>
    public decimal? Alicuota { get; set; } = 0;
    /// <inheritdoc/>
    public int? OrdenCompraId { get; set; }
    /// <inheritdoc/>
    public string Detalle { get; set; }
}

/// <summary>
/// Implementación concreta de <see cref="IComprobanteImpuestoCreate"/>. Usado dentro de <see cref="ComprobanteCreate"/>.
/// </summary>
public class ComprobanteImpuestoCreate : IComprobanteImpuestoCreate
{
    /// <inheritdoc/>
    public int? ImpuestoId { get; set; }
    /// <inheritdoc/>
    public string Descripcion { get; set; }
    /// <inheritdoc/>
    public decimal ImporteTotal { get; set; }
    /// <inheritdoc/>
    public decimal? Alicuota { get; set; } = 0;
}

/// <summary>
/// Implementación concreta de <see cref="IComprobantePercepcionCreate"/>. Usado dentro de <see cref="ComprobanteCreate"/>.
/// </summary>
public class ComprobantePercepcionCreate : IComprobantePercepcionCreate
{
    /// <inheritdoc/>
    public int? PercepcionId { get; set; }
    /// <inheritdoc/>
    public string Descripcion { get; set; }
    /// <inheritdoc/>
    public decimal Alicuota { get; set; } = 0;
    /// <inheritdoc/>
    public decimal ImporteTotal { get; set; }
}

/// <summary>
/// Implementación concreta de <see cref="IComprobanteDetalleSave"/>. Usado dentro de <see cref="ComprobanteSave"/>.
/// </summary>
public class ComprobanteDetalleSave : IComprobanteDetalleSave
{
    /// <inheritdoc/>
    public int Id { get; set; }
    /// <inheritdoc/>
    public short? UnidadMedidaId { get; set; }
    /// <inheritdoc/>
    public int? Cantidad { get; set; } = 1;
    /// <inheritdoc/>
    public decimal? PrecioUnitario { get; set; } = decimal.Zero;
    /// <inheritdoc/>
    public decimal? ImporteBonificacion { get; set; } = decimal.Zero;
    /// <inheritdoc/>
    public decimal? Subtotal { get; set; } = decimal.Zero;
    /// <inheritdoc/>
    public decimal? ImporteIVA { get; set; } = 0;
    /// <inheritdoc/>
    public bool? Exento { get; set; }
    /// <inheritdoc/>
    public decimal? Alicuota { get; set; }
    /// <inheritdoc/>
    public int? OrdenCompraId { get; set; }
    /// <inheritdoc/>
    public string Detalle { get; set; }
    /// <inheritdoc/>
    public bool Deleted { get; set; } = false;
}

/// <summary>
/// Implementación concreta de <see cref="IComprobanteImpuestoSave"/>. Usado dentro de <see cref="ComprobanteSave"/>.
/// </summary>
public class ComprobanteImpuestoSave : IComprobanteImpuestoSave
{
    /// <inheritdoc/>
    public int Id { get; set; }
    /// <inheritdoc/>
    public int? ImpuestoId { get; set; }
    /// <inheritdoc/>
    public string Descripcion { get; set; }
    /// <inheritdoc/>
    public decimal? ImporteTotal { get; set; }
    /// <inheritdoc/>
    public decimal? Alicuota { get; set; }
    /// <inheritdoc/>
    public bool Deleted { get; set; } = false;
}

/// <summary>
/// Implementación concreta de <see cref="IComprobantePercepcionSave"/>. Usado dentro de <see cref="ComprobanteSave"/>.
/// </summary>
public class ComprobantePercepcionSave : IComprobantePercepcionSave
{
    /// <inheritdoc/>
    public int Id { get; set; }
    /// <inheritdoc/>
    public int? PercepcionId { get; set; }
    /// <inheritdoc/>
    public string Descripcion { get; set; }
    /// <inheritdoc/>
    public decimal? Alicuota { get; set; } = decimal.Zero;
    /// <inheritdoc/>
    public decimal? ImporteTotal { get; set; } = decimal.Zero;
    /// <inheritdoc/>
    public bool Deleted { get; set; } = false;
}

/// <summary>
/// Implementación concreta de <see cref="IComprobanteDetalleUpdate"/>. Usado dentro de <see cref="ComprobanteUpdate"/>.
/// </summary>
public class ComprobanteDetalleUpdate : IComprobanteDetalleUpdate
{
    /// <inheritdoc/>
    public int Id { get; set; }
    /// <inheritdoc/>
    public short? UnidadMedidaId { get; set; }
    /// <inheritdoc/>
    public int Cantidad { get; set; }
    /// <inheritdoc/>
    public decimal PrecioUnitario { get; set; }
    /// <inheritdoc/>
    public decimal? ImporteBonificacion { get; set; }
    /// <inheritdoc/>
    public decimal Subtotal { get; set; }
    /// <inheritdoc/>
    public decimal? ImporteIVA { get; set; } = 0;
    /// <inheritdoc/>
    public bool? Exento { get; set; }
    /// <inheritdoc/>
    public decimal? Alicuota { get; set; }
    /// <inheritdoc/>
    public int? OrdenCompraId { get; set; }
    /// <inheritdoc/>
    public string Detalle { get; set; }
    /// <inheritdoc/>
    public bool Deleted { get; set; }
}

/// <summary>
/// Implementación concreta de <see cref="IComprobanteImpuestoUpdate"/>. Usado dentro de <see cref="ComprobanteUpdate"/>.
/// </summary>
public class ComprobanteImpuestoUpdate : IComprobanteImpuestoUpdate
{
    /// <inheritdoc/>
    public int Id { get; set; }
    /// <inheritdoc/>
    public int? ImpuestoId { get; set; }
    /// <inheritdoc/>
    public string Descripcion { get; set; }
    /// <inheritdoc/>
    public decimal ImporteTotal { get; set; }
    /// <inheritdoc/>
    public bool Deleted { get; set; }
}

/// <summary>
/// Implementación concreta de <see cref="IComprobantePercepcionUpdate"/>. Usado dentro de <see cref="ComprobanteUpdate"/>.
/// </summary>
public class ComprobantePercepcionUpdate : IComprobantePercepcionUpdate
{
    /// <inheritdoc/>
    public int Id { get; set; }
    /// <inheritdoc/>
    public int? PercepcionId { get; set; }
    /// <inheritdoc/>
    public string Descripcion { get; set; }
    /// <inheritdoc/>
    public decimal Alicuota { get; set; }
    /// <inheritdoc/>
    public decimal ImporteTotal { get; set; }
    /// <inheritdoc/>
    public bool Deleted { get; set; }
}

/// <summary>
/// Implementación concreta de <see cref="IComprobanteCabecera"/>. Usado para enviar datos de cabecera.
/// </summary>
public class ComprobanteCabecera : IComprobanteCabecera
{
    /// <inheritdoc/>
    public int Id { get; set; }
    /// <summary>
    /// Versión del formato del QR.
    /// </summary>
    public int ver { get; set; }
    /// <summary>
    /// Fecha de emisión del comprobante (formato yyyy-MM-dd).
    /// </summary>
    public string fecha { get; set; }
    /// <summary>
    /// CUIT del emisor del comprobante.
    /// </summary>
    public long cuit { get; set; }
    /// <summary>
    /// Punto de venta del comprobante.
    /// </summary>
    public int ptoVta { get; set; }
    /// <summary>
    /// Código numérico del tipo de comprobante según AFIP.
    /// </summary>
    public int tipoCmp { get; set; }
    /// <summary>
    /// Número del comprobante.
    /// </summary>
    public int nroCmp { get; set; }
    /// <summary>
    /// Importe total del comprobante.
    /// </summary>
    public decimal importe { get; set; }
    /// <summary>
    /// Código de la moneda ("PES", "DOL", etc.).
    /// </summary>
    public string moneda { get; set; }
    /// <summary>
    /// Cotización de la moneda (si aplica, para moneda extranjera).
    /// </summary>
    public decimal? ctz { get; set; }
    /// <summary>
    /// Código del tipo de documento del receptor según AFIP (ej: 80 para CUIT).
    /// </summary>
    public int tipoDocRec { get; set; }
    /// <summary>
    /// Número de documento del receptor (CUIT/CUIL/DNI).
    /// </summary>
    public long nroDocRec { get; set; }
    /// <summary>
    /// Tipo de código de autorización ("E" para CAE, "A" para CAEA).
    /// </summary>
    public string tipoCodAut { get; set; }
    /// <summary>
    /// Código de autorización (CAE o CAEA).
    /// </summary>
    public long codAut { get; set; }
}

/// <summary>
/// Implementación concreta de <see cref="IComprobanteSave"/>. Usado para enviar datos para guardar un borrador.
/// </summary>
public class ComprobanteSave : IComprobanteSave
{
    /// <inheritdoc/>
    public int Id { get; set; } = 0;
    /// <inheritdoc/>
    public string NroIdentificacionFiscalPro { get; set; }
    /// <inheritdoc/>
    public short CategoriaTipoEmisorId { get; set; }
    /// <inheritdoc/>
    public short ComprobanteTipoId { get; set; }
    /// <inheritdoc/>
    public short CategoriaTipoReceptorId { get; set; }
    /// <inheritdoc/>
    public string NroIdentificacionFiscalCompany { get; set; }
    /// <inheritdoc/>
    public int PuntoVenta { get; set; }
    /// <inheritdoc/>
    public string Letra { get; set; }
    /// <inheritdoc/>
    public int Numero { get; set; }
    /// <inheritdoc/>
    public DateTime FechaEmision { get; set; }
    /// <inheritdoc/>
    public short TipoCodigoAutorizacionId { get; set; }
    /// <inheritdoc/>
    public string CodigoAutorizacion { get; set; }
    /// <inheritdoc/>
    public long MonedaId { get; set; }
    /// <inheritdoc/>
    public decimal ImporteNeto { get; set; }
    /// <inheritdoc/>
    public decimal ImporteTotal { get; set; }
    /// <inheritdoc/>
    public decimal ImporteIVA { get; set; }
    /// <inheritdoc/>
    public decimal ImporteBonificacion { get; set; }
    /// <inheritdoc/>
    public decimal ImportePercepcionIVA { get; set; }
    /// <inheritdoc/>
    public decimal ImportePercepcionIIBB { get; set; }
    /// <inheritdoc/>
    public decimal ImportePercepcionMunicipal { get; set; }
    /// <inheritdoc/>
    public decimal ImporteImpuestosInternos { get; set; }
    /// <inheritdoc/>
    public decimal ImporteOtrosTributosProv { get; set; }
    /// <inheritdoc/>
    public int? EmpresaId { get; set; }
    /// <inheritdoc/>
    public long? CompanyId { get; set; }
    /// <inheritdoc/>
    public string CodigoHES { get; set; }
    /// <inheritdoc/>
    public int? ComprobanteEstadoId { get; set; }
    /// <inheritdoc/>
    public string Comentarios { get; set; }
    /// <inheritdoc/>
    public List<IComprobanteDetalleSave> Detalles { get; set; } = new();
    /// <inheritdoc/>
    public List<IComprobanteImpuestoSave> Impuestos { get; set; } = new();
    /// <inheritdoc/>
    public List<IComprobantePercepcionSave> Percepciones { get; set; } = new();
    /// <inheritdoc/>
    public bool? ValidacionQR { get; set; } = false;
    /// <inheritdoc/>
    public string QRValor { get; set; }
    /// <inheritdoc/>
    public EstadoConstatacion EstadoConstatacion { get; set; } = EstadoConstatacion.SinConstatar;
    /// <inheritdoc/>
    public EstadoConstatacion EstadoConstatacionQR { get; set; } = EstadoConstatacion.SinConstatar;
    /// <inheritdoc/>
    public string ObservacionesARCA { get; set; }
    /// <inheritdoc/>
    public string ObservacionesARCAQR { get; set; }
}


#endregion