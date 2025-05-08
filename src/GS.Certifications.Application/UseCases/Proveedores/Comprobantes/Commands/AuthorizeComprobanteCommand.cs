using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Exceptions;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services;
using GSF.Application.Common.Exceptions;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands;

public class AuthorizeComprobanteCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public byte[] RowVersion { get; set; }
}

public class AuthorizeComprobanteCommandHandler : BaseRequestHandler<Unit, AuthorizeComprobanteCommand, Unit>
{
    private readonly ICertificationsDbContext Context;
    private readonly IComprobanteService comprobanteService;

    public AuthorizeComprobanteCommandHandler(ICertificationsDbContext context, IComprobanteService comprobanteService)
    {
        Context = context;
        this.comprobanteService = comprobanteService;
    }

    protected override async Task<Unit> HandleRequestAsync(AuthorizeComprobanteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await comprobanteService.AuthorizeAsync(request.Id, new ComprobanteAuthorize() { RowVersion = request.RowVersion });

            await Context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
        catch (ComprobantePeriodoRequeridoException ex)
        {
            throw new ValidationErrorException("Periodo", ex.Message);
        }
        //catch (ComprobanteTotalNetoInvalidoException ex)
        //{

        //    throw new ValidationErrorException("TotalNeto", ex.Message);
        //}
        //catch (ComprobanteImporteBonificacionInvalidoException ex)
        //{

        //    throw new ValidationErrorException("ImporteBonificacion", ex.Message);
        //}
        //catch (ComprobanteTotalImporteIVAInvalido ex)
        //{

        //    throw new ValidationErrorException("ImporteIVA", ex.Message);
        //}
        //catch (ComprobanteTotalImporteImpuestosInternosInvalido ex)
        //{

        //    throw new ValidationErrorException("ImporteImpuestosInternos", ex.Message);
        //}
        //catch (ComprobanteTotalImporteImpuestosProvInvalido ex)
        //{

        //    throw new ValidationErrorException("ImporteOtrosTributosProv", ex.Message);
        //}
        //catch (ComprobanteTotalPercepcionesIVAInvalido ex)
        //{

        //    throw new ValidationErrorException("ImportePercepcionIVA", ex.Message);
        //}
        //catch (ComprobanteTotalPercepcionesIIBBInvalido ex)
        //{

        //    throw new ValidationErrorException("ImportePercepcionIIBB", ex.Message);
        //}
        //catch (ComprobanteTotalPercepcionesMunicipalInvalido ex)
        //{

        //    throw new ValidationErrorException("ImportePercepcionMunicipal", ex.Message);
        //}
        //catch (ComprobanteSubtotalInvalidoException ex)
        //{

        //    throw new ValidationErrorException("Subtotal", ex.Message);
        //}
        //catch (ComprobanteTotalInvalidoException ex)
        //{

        //    throw new ValidationErrorException("ImporteTotal", ex.Message);
        //}
        //catch (Iva21InvalidoException ex)
        //{

        //    throw new ValidationErrorException("Iva21", ex.Message);
        //}
        //catch (Iva105InvalidoException ex)
        //{

        //    throw new ValidationErrorException("Iva105", ex.Message);
        //}
        //catch (ComprobanteDetalleInvalidoException ex)
        //{
        //    var failures = new List<ValidationFailure>();

        //    foreach (var item in ex.Errores)
        //    {
        //        failures.Add(new($"Detalles[{item.Item1}].{item.Item2}", item.Item3));
        //    }
        //    var exception = new ValidationErrorException(failures);
        //    throw exception;
        //}
        //catch (ComprobantePercepcionesInvalidasException ex)
        //{
        //    var failures = new List<ValidationFailure>();

        //    foreach (var item in ex.Errores)
        //    {
        //        failures.Add(new($"Percepciones[{item.Item1}].{item.Item2}", item.Item3));
        //    }
        //    var exception = new ValidationErrorException(failures);
        //    throw exception;
        //}
        catch (Exception)
        {
            throw;
        }
    }

    public class ComprobanteAuthorize : IComprobanteAuthorize
    {
        public byte[] RowVersion { get; set; }
    }
}
