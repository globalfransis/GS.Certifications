using FluentValidation;
using GS.Certifications.Application.CQRS.DbContexts;
using GSF.Application.Common.Validators;
using System;

namespace GS.Certifications.Application.UseCases.OrdenesCompras.Commands.TipoOrdenCompra
{
    public class UpdateTipoOrdenCompraCommandValidator : AbstractGSValidator<UpdateTipoOrdenCompraCommand>
    {
        private readonly ICertificationsDbContext _context;
        public UpdateTipoOrdenCompraCommandValidator(ICertificationsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            RuleFor(c => c.Nombre)
                .NotEmpty()
                .WithMessage("El campo '{PropertyName}' es obligatorio")
                .MaximumLength(100)
                .WithMessage("El campo '{PropertyName}' no debe superar las 50 letras")
                .WithName("Nombre");

            RuleFor(c => c.Descripcion)
                .MaximumLength(255)
                .WithMessage("El campo '{PropertyName}' no debe superar las 255 letras")
                .WithName("Descripcion");
        }
    }
}
