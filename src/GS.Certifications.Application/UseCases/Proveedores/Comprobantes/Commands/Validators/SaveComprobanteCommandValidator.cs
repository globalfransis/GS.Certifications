using FluentValidation;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands;
using GSF.Application.Common.Validators;
using GSFSharedResources;
using Microsoft.Extensions.Localization;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands.Validators;

public class SaveComprobanteCommandValidator : AbstractGSValidator<SaveComprobanteDraftCommand>
{
    public SaveComprobanteCommandValidator(IStringLocalizer<Shared> loc)
    {

        RuleFor(c => c.ComprobanteTipoId)
            .NotEmpty()
            .WithMessage(loc["El campo ‘{PropertyName}’ es obligatorio."])
            .WithName("Tipo Comprobante");

        RuleFor(c => c.PuntoVenta)
            .NotEmpty()
            .WithMessage(loc["El campo ‘{PropertyName}’ es obligatorio."])
            .WithName("Punto de Venta");

        RuleFor(c => c.Numero)
            .NotEmpty()
            .WithMessage(loc["El campo ‘{PropertyName}’ es obligatorio."])
            .WithName("Número");

        RuleFor(c => c.FechaEmision)
            .NotEmpty()
            .WithMessage(loc["El campo ‘{PropertyName}’ es obligatorio."])
            .WithName("Fecha Emisión");

        RuleFor(c => c.TipoCodigoAutorizacionId)
            .NotEmpty()
            .WithMessage(loc["El campo ‘{PropertyName}’ es obligatorio."])
            .WithName("Tipo Código Autorización");

        RuleFor(c => c.CodigoAutorizacion)
            .NotEmpty()
            .WithMessage(loc["El campo ‘{PropertyName}’ es obligatorio."])
            .WithName("Código Autorización");

        RuleFor(c => c.MonedaId)
            .NotEmpty()
            .WithMessage(loc["El campo ‘{PropertyName}’ es obligatorio."])
            .WithName("Moneda");

        RuleFor(c => c.Detalles)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(loc["No se han agregado detalles al comprobante."])
            .WithName("Detalles");
        ;


    }
}