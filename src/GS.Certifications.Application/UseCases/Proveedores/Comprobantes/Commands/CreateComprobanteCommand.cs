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

public class CreateComprobanteCommand : IRequest<int>
{
    public string NroIdentificacionFiscalPro { get; set; }
    //public string DomicilioPro { get; set; }
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
    public int? EmpresaId { get; set; }
    public long? CompanyId { get; set; }
    public string CodigoHES { get; set; }
    public int? ComprobanteEstadoId { get; set; }
    public string Comentarios { get; set; }
    public List<ComprobanteDetalleCreate> Detalles { get; set; }
    public List<ComprobanteImpuestoCreate> Impuestos { get; set; }
    public List<ComprobantePercepcionCreate> Percepciones { get; set; }
    public bool? ValidacionQR { get; set; } = false;
    public string QRValor { get; set; }
    public DateTime? FechaVencimiento { get; set; }
    public DateTime? FechaVencimientoCodigoAutorizacion { get; set; }
}

public class CreateComprobanteCommandHandler : BaseRequestHandler<int, CreateComprobanteCommand, int>
{
    private readonly ICertificationsDbContext Context;
    private readonly IComprobanteService comprobanteService;

    public CreateComprobanteCommandHandler(ICertificationsDbContext context, IComprobanteService comprobanteService)
    {
        Context = context;
        this.comprobanteService = comprobanteService;
    }

    protected override async Task<int> HandleRequestAsync(CreateComprobanteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var companyExtra = await Context.CompanyExtras.FirstOrDefaultAsync(c => c.CompanyId == request.CompanyId) ?? throw new Exception("No existen datos extra de la Company.");

            var comprobanteCreateArgs = new ComprobanteCreate()
            {
                NroIdentificacionFiscalPro = request.NroIdentificacionFiscalPro,
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
                ImporteImpuestosInternos = (decimal)request.ImporteImpuestosInternos,
                ImporteOtrosTributosProv = (decimal)request.ImporteOtrosTributosProv,
                ImportePercepcionIIBB = (decimal)request.ImportePercepcionIIBB,
                ImportePercepcionIVA = (decimal)request.ImportePercepcionIVA,
                ImportePercepcionMunicipal = (decimal)request.ImportePercepcionMunicipal,
                ImporteBonificacion = (decimal)request.ImporteBonificacion,
                Comentarios = request.Comentarios,
                Detalles = new(request.Detalles),
                Impuestos = new(request.Impuestos),
                Percepciones = new(request.Percepciones),
                EmpresaId = request.EmpresaId,

                ImporteIVA = request.ImporteIVA ?? 0,

                ValidacionQR = request.ValidacionQR,
                QRValor = request.QRValor,

                FechaVencimiento = request.FechaVencimiento,
                FechaVencimientoCodigoAutorizacion = request.FechaVencimientoCodigoAutorizacion,

                // Company
                CategoriaTipoReceptorId = companyExtra.TipoResponsableId,
                NroIdentificacionFiscalCompany = companyExtra.IdentificadorTributario,
                CompanyId = request.CompanyId
            };

            //if (request.Iva105 is not null && request.Iva105 > 0)
            //{
            //    comprobanteCreateArgs.Impuestos.Add(new ComprobanteImpuestoCreate()
            //    {
            //        ImpuestoId = Impuesto.IVA_10_5,
            //        Descripcion = "Impuesto al Valor Agregado 10.5%",
            //        ImporteTotal = (decimal)request.Iva105
            //    });
            //}

            //if (request.Iva21 is not null && request.Iva21 > 0)
            //{
            //    comprobanteCreateArgs.Impuestos.Add(new ComprobanteImpuestoCreate()
            //    {
            //        ImpuestoId = Impuesto.IVA_21,
            //        Descripcion = "Impuesto al Valor Agregado 21%",
            //        ImporteTotal = (decimal)request.Iva21
            //    });
            //}

            var nuevo = await comprobanteService.CreateAsync(comprobanteCreateArgs);

            await Context.SaveChangesAsync(cancellationToken);
            return nuevo.Id;
        }
        catch (ComprobanteYaExisteException ex)
        {
            throw new ValidationErrorException("Numero", ex.Message);
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
        catch (ComprobanteSubtotalInvalidoException ex)
        {

            throw new ValidationErrorException("Subtotal", ex.Message);
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