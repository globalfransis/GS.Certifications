using FluentValidation.Results;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Exceptions;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services;
using GSF.Application.Common.Exceptions;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands;

public class UpdateComprobanteCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public byte[] RowVersion { get; set; }
    public string NroIdentificacionFiscalPro { get; set; }
    public string NroIdentificacionFiscalCompany { get; set; }
    public string DomicilioPro { get; set; }
    public short? CategoriaTipoEmisorId { get; set; }
    public short? ComprobanteTipoId { get; set; }
    public int? PuntoVenta { get; set; }
    public string Letra { get; set; }
    public int? Numero { get; set; }
    public DateTime? FechaEmision { get; set; }
    public short? TipoCodigoAutorizacionId { get; set; }
    public string CodigoAutorizacion { get; set; }
    public long? MonedaId { get; set; }
    public decimal? ImporteNeto { get; set; }
    public decimal? ImporteTotal { get; set; }
    public decimal? ImporteIVA { get; set; }
    public decimal? ImporteBonificacion { get; set; } = decimal.Zero;
    public decimal? ImportePercepcionIVA { get; set; } = decimal.Zero;
    public decimal? ImportePercepcionIIBB { get; set; } = decimal.Zero;
    public decimal? ImportePercepcionMunicipal { get; set; } = decimal.Zero;
    public decimal? ImporteImpuestosInternos { get; set; } = decimal.Zero;
    public decimal? ImporteOtrosTributosProv { get; set; } = decimal.Zero;
    public string CodigoHES { get; set; }
    public int? ComprobanteEstadoId { get; set; }
    public string Comentarios { get; set; }
    public List<ComprobanteDetalleUpdate> Detalles { get; set; }
    public List<ComprobanteImpuestoUpdate> Impuestos { get; set; }
    public List<ComprobantePercepcionUpdate> Percepciones { get; set; }

    public decimal? Cotizacion { get; set; }
    public string NroRemito { get; set; }
    public string NroOrdenCompra { get; set; }
    public short? CondicionVentaId { get; set; }

    public DateTime? FechaVencimiento { get; set; }
    public DateTime? FechaVencimientoCodigoAutorizacion { get; set; }

    public long CompanyId { get; set; }

    public bool? ValidacionQR { get; set; } = false;
    public string QRValor { get; set; }
}

public class UpdateComprobanteCommandHandler : BaseRequestHandler<Unit, UpdateComprobanteCommand, Unit> // Adjust TEntity and TResponse properly
{
    private readonly ICertificationsDbContext Context;
    private readonly IComprobanteService comprobanteService;

    public UpdateComprobanteCommandHandler(ICertificationsDbContext context, IComprobanteService comprobanteService)
    {
        Context = context;
        this.comprobanteService = comprobanteService;
    }

    protected override async Task<Unit> HandleRequestAsync(UpdateComprobanteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var companyExtra = await Context.CompanyExtras.FirstOrDefaultAsync(c => c.CompanyId == request.CompanyId) ?? throw new Exception("No existen datos extra de la Company.");

            var comprobanteUpdateArgs = new ComprobanteUpdate()
            {
                Id = request.Id,
                NroIdentificacionFiscalPro = request.NroIdentificacionFiscalPro,
                NroIdentificacionFiscalCompany = request.NroIdentificacionFiscalCompany,
                //DomicilioPro = request.DomicilioPro,
                CategoriaTipoEmisorId = (short)request.CategoriaTipoEmisorId,
                ComprobanteTipoId = (short)request.ComprobanteTipoId,
                PuntoVenta = (int)request.PuntoVenta,
                Numero = (int)request.Numero,
                FechaEmision = (DateTime)request.FechaEmision,
                TipoCodigoAutorizacionId = (short)request.TipoCodigoAutorizacionId,
                CodigoAutorizacion = request.CodigoAutorizacion,
                MonedaId = (long)request.MonedaId,
                ImporteNeto = (decimal)request.ImporteNeto,
                ImporteTotal = (decimal)request.ImporteTotal,
                ImporteBonificacion = (decimal)request.ImporteBonificacion,
                ImporteIVA = request.ImporteIVA ?? 0,
                ImporteImpuestosInternos = request.ImporteImpuestosInternos ?? 0,
                ImporteOtrosTributosProv = request.ImporteOtrosTributosProv ?? 0,
                ImportePercepcionIIBB = request.ImportePercepcionIIBB ?? 0,
                ImportePercepcionIVA = request.ImportePercepcionIVA ?? 0,
                ImportePercepcionMunicipal = request.ImportePercepcionMunicipal ?? 0,
                Comentarios = request.Comentarios,
                Detalles = new(request.Detalles),
                Impuestos = new(request.Impuestos),
                Percepciones = new(request.Percepciones),
                ValidacionQR = request.ValidacionQR,
                QRValor = request.QRValor,

                Cotizacion = request.Cotizacion,
                NroRemito = request.NroRemito,
                NroOrdenCompra = request.NroOrdenCompra,
                CondicionVentaId = request.CondicionVentaId,
                FechaVencimiento = request.FechaVencimiento,
                FechaVencimientoCodigoAutorizacion = request.FechaVencimientoCodigoAutorizacion,
                // Company
                CategoriaTipoReceptorId = companyExtra.TipoResponsableId,
                //NroIdentificacionFiscalCompany = companyExtra.IdentificadorTributario,
                CompanyId = request.CompanyId
            };

            //if (request.Iva105 is not null && request.Iva105 > 0)
            //{
            //    comprobanteUpdateArgs.Impuestos.Add(new ComprobanteImpuestoUpdate()
            //    {
            //        ImpuestoId = Impuesto.IVA_10_5,
            //        Descripcion = "Impuesto al Valor Agregado 10.5%",
            //        ImporteTotal = (decimal)request.Iva105
            //    });
            //}

            //if (request.Iva21 is not null && request.Iva21 > 0)
            //{
            //    comprobanteUpdateArgs.Impuestos.Add(new ComprobanteImpuestoUpdate()
            //    {
            //        ImpuestoId = Impuesto.IVA_21,
            //        Descripcion = "Impuesto al Valor Agregado 21%",
            //        ImporteTotal = (decimal)request.Iva21
            //    });
            //}

            await comprobanteService.UpdateAsync(comprobanteUpdateArgs);

            await comprobanteService.VerifyEstadoARCAAsync(comprobanteUpdateArgs.Id, request.RowVersion);

            await Context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
        catch (ComprobanteDetallesVaciosException ex)
        {
            throw new ValidationErrorException("Detalles", ex.Message);
        }
        catch (ComprobanteTotalNetoInvalidoException ex)
        {

            throw new ValidationErrorException("TotalNeto", ex.Message);
        }
        catch (ComprobanteImporteBonificacionInvalidoException ex)
        {

            throw new ValidationErrorException("ImporteBonificacion", ex.Message);
        }
        catch (ComprobanteTotalImporteIVAInvalido ex)
        {

            throw new ValidationErrorException("ImporteIVA", ex.Message);
        }
        catch (ComprobanteTotalImporteImpuestosInternosInvalido ex)
        {

            throw new ValidationErrorException("ImporteImpuestosInternos", ex.Message);
        }
        catch (ComprobanteTotalImporteImpuestosProvInvalido ex)
        {

            throw new ValidationErrorException("ImporteOtrosTributosProv", ex.Message);
        }
        catch (ComprobanteTotalPercepcionesIVAInvalido ex)
        {

            throw new ValidationErrorException("ImportePercepcionIVA", ex.Message);
        }
        catch (ComprobanteTotalPercepcionesIIBBInvalido ex)
        {

            throw new ValidationErrorException("ImportePercepcionIIBB", ex.Message);
        }
        catch (ComprobanteTotalPercepcionesMunicipalInvalido ex)
        {

            throw new ValidationErrorException("ImportePercepcionMunicipal", ex.Message);
        }
        catch (ComprobanteSubtotalInvalidoException ex)
        {

            throw new ValidationErrorException("Subtotal", ex.Message);
        }
        catch (ComprobanteTotalInvalidoException ex)
        {

            throw new ValidationErrorException("ImporteTotal", ex.Message);
        }
        catch (Iva21InvalidoException ex)
        {

            throw new ValidationErrorException("Iva21", ex.Message);
        }
        catch (Iva105InvalidoException ex)
        {

            throw new ValidationErrorException("Iva105", ex.Message);
        }
        catch (ComprobanteDetalleInvalidoException ex)
        {
            var failures = new List<ValidationFailure>();

            foreach (var item in ex.Errores)
            {
                failures.Add(new($"Detalles[{item.Item1}].{item.Item2}", item.Item3));
            }
            var exception = new ValidationErrorException(failures);
            throw exception;
        }
        catch (ComprobantePercepcionesInvalidasException ex)
        {
            var failures = new List<ValidationFailure>();

            foreach (var item in ex.Errores)
            {
                failures.Add(new($"Percepciones[{item.Item1}].{item.Item2}", item.Item3));
            }
            var exception = new ValidationErrorException(failures);
            throw exception;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
