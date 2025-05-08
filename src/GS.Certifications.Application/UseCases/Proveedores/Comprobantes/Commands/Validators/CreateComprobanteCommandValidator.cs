using FluentValidation;
using GSF.Application.Common.Validators;
using GSFSharedResources;
using Microsoft.Extensions.Localization;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands.Validators;

public class CreateComprobanteCommandValidator : AbstractGSValidator<CreateComprobanteCommand>
{
    public CreateComprobanteCommandValidator(IStringLocalizer<Shared> loc)
    {
        //RuleFor(c => c.NroIdentificacionFiscalPro)
        //    .NotEmpty()
        //    .WithMessage(loc["El campo ‘{PropertyName}’ es obligatorio."])
        //    .WithName("CUIT Emisor");

        //RuleFor(c => c.DomicilioPro)
        //    .NotEmpty()
        //    .WithMessage(loc["El campo ‘{PropertyName}’ es obligatorio."])
        //    .WithName("Domicilio Emisor");

        //RuleFor(c => c.CategoriaTipoEmisorId)
        //    .NotEmpty()
        //    .WithMessage(loc["El campo ‘{PropertyName}’ es obligatorio."])
        //    .WithName("Categoría Emisor");

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

        RuleFor(c => c.MonedaId)
            .NotEmpty()
            .WithMessage(loc["El campo ‘{PropertyName}’ es obligatorio."])
            .WithName("Moneda");

        RuleFor(c => c.ImporteTotal)
            .NotEmpty()
            .WithMessage(loc["El campo ‘{PropertyName}’ es obligatorio."])
            .WithName("Total");

        RuleFor(c => c.Detalles)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(loc["No se han agregado detalles al comprobante."])
            .WithName("Detalles");

        RuleForEach(c => c.Impuestos).Cascade(CascadeMode.Stop)
            .ChildRules(detalle =>
        {
            detalle.RuleFor(d => d.ImpuestoId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(loc["Se debe especificar el impuesto."])
                .GreaterThan(0)
                .WithMessage(loc["Se debe especificar el impuesto."])
                .WithName("Impuesto");



        }).WithMessage(loc["Error en los impuestos del comprobante."]);

        RuleForEach(c => c.Percepciones).Cascade(CascadeMode.Stop)
            .ChildRules(detalle =>
            {
                detalle.RuleFor(d => d.PercepcionId)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .WithMessage(loc["Se debe especificar la percepción."])
                    .GreaterThan(0)
                    .WithMessage(loc["Se debe especificar la percepción."])
                    .WithName("Percepción");



            }).WithMessage(loc["Error en las percepciones del comprobante."]);


    }
}