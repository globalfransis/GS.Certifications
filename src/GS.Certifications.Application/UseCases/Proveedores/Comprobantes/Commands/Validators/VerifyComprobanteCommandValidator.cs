using FluentValidation;
using GSF.Application.Common.Validators;
using GSFSharedResources;
using Microsoft.Extensions.Localization;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands.Validators;

public class VerifyComprobanteCommandValidator : AbstractGSValidator<VerifyComprobanteCommand>
{
    public VerifyComprobanteCommandValidator(IStringLocalizer<Shared> loc)
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
            .GreaterThan(0)
            .WithMessage(loc["El campo ‘{PropertyName}’ es inválido."])
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

        //RuleForEach(c => c.Detalles).Cascade(CascadeMode.Stop)
        //    .ChildRules(detalle =>
        //{
        //    detalle.RuleFor(d => d.Cantidad)
        //        .Cascade(CascadeMode.Stop)
        //        .NotNull()
        //        .GreaterThan(0)
        //        .WithMessage(loc["La cantidad debe ser mayor que 0."])
        //        .WithName("Cantidad");

        //    detalle.RuleFor(d => d.PrecioUnitario).Cascade(CascadeMode.Stop)
        //        .NotNull()
        //        .GreaterThanOrEqualTo(0)
        //        .WithMessage(loc["El precio unitario no puede ser negativo."])
        //        .WithName("Precio Unitario");

        //    detalle.RuleFor(d => d.Subtotal)
        //        .Cascade(CascadeMode.Stop)
        //        .NotNull()
        //        .GreaterThanOrEqualTo(0)
        //        .WithMessage(loc["El subtotal no puede ser negativo."])
        //        .WithName("Subtotal");

        //    detalle.RuleFor(d => d.Detalle)
        //        .Cascade(CascadeMode.Stop)
        //        .NotEmpty()
        //        .WithMessage(loc["El detalle no puede estar vacío."])
        //        .WithName("Detalle");

        //    // Regla: La suma de los ítems sin impuestos menos las bonificaciones debe ser igual al total neto
        //    RuleFor(c => c)
        //        .Must(comprobante =>
        //        {
        //            decimal sumaItemsSinImpuestos = comprobante.Detalles.Sum(d => d.PrecioUnitario * (d.Cantidad ?? 0));
        //            decimal totalBonificaciones = comprobante.Detalles.Sum(d => d.ImporteBonificacion);
        //            decimal totalNetoEsperado = sumaItemsSinImpuestos - totalBonificaciones;

        //            // Compara con el total neto real, con una tolerancia (ej. 0.01)
        //            return Math.Abs(comprobante.ImporteNeto ?? 0 - totalNetoEsperado) <= 0.01m;
        //        })
        //        .OverridePropertyName("Detalles")
        //        .WithMessage(loc["La suma de los ítems sin impuestos menos las bonificaciones debe ser igual al total neto."]);

        //    // Regla: La suma de los ítems sin impuestos debe ser igual al subtotal
        //    RuleFor(c => c)
        //        .Must(comprobante =>
        //        {
        //            decimal sumaItemsSinImpuestos = comprobante.Detalles.Sum(d => d.PrecioUnitario * (d.Cantidad ?? 0));

        //            // Compara con el subtotal real, con una tolerancia (ej. 0.01)
        //            return Math.Abs(comprobante.ImporteTotal ?? 0 - sumaItemsSinImpuestos) <= 0.01m;
        //        })
        //        .WithMessage(loc["La suma de los ítems sin impuestos debe ser igual al subtotal."]);


        //    detalle.RuleFor(d => d)
        //            .Cascade(CascadeMode.Stop)
        //            .Must(d => {
        //                var subtotalEsperado = (d.PrecioUnitario) * (d.Cantidad ?? 0) - (d.ImporteBonificacion);

        //                // Compara el subtotal calculado con el subtotal real, con una tolerancia (ej. 0.01)
        //                return Math.Abs(d.Subtotal - subtotalEsperado) <= (decimal)0.01;
        //            })
        //            .WithName("Subtotal")
        //            .OverridePropertyName("Subtotal")
        //                        .WithMessage(loc["El subtotal debe ser igual al precio unitario multiplicado por la cantidad, menos la bonificación."]);

        //}).WithMessage(loc["Error en los detalles del comprobante."]); // Mensaje general para los errores en los detalles


    }
}