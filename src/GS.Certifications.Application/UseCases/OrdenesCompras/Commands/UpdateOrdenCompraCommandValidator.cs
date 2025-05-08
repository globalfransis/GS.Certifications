using FluentValidation;
using GS.Certifications.Application.CQRS.DbContexts;
using GSF.Application.Common.Validators;
using System;

namespace GS.Certifications.Application.UseCases.OrdenesCompras.Commands
{
    public class UpdateOrdenCompraCommandValidator : AbstractGSValidator<UpdateOrdenCompraCommand>
    {
        private readonly ICertificationsDbContext _context;
        public UpdateOrdenCompraCommandValidator(ICertificationsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            RuleFor(c => c.NumeroOrden)
                .NotEmpty()
                .WithMessage("El campo '{PropertyName}' es obligatorio")
                .MaximumLength(50)
                .WithMessage("El campo '{PropertyName}' no debe superar las 50 letras")
                .WithName("NumeroOrden");

            //RuleFor(c => c.CodigoHES)
            //    .MaximumLength(50)
            //    .WithMessage("El campo '{PropertyName}' no debe superar las 50 letras")
            //    .WithName("CodigoHes");

            RuleFor(c => c.CodigoQAD)
                .MaximumLength(50)
                .WithMessage("El campo '{PropertyName}' no debe superar las 50 letras")
                .WithName("CodigoQAD");
        }
    }
}
