using FluentValidation;
using GS.Certifications.Application.CQRS.DbContexts;
using GSF.Application.Common.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.ConceptosGastosTipos.Commands
{
    public class UpdateConceptoGastoTipoCommandValidator : AbstractGSValidator<UpdateConceptoGastoTipoCommand>
    {
        private readonly ICertificationsDbContext _context;
        public UpdateConceptoGastoTipoCommandValidator(ICertificationsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            RuleFor(c => c.Nombre)
                .NotEmpty()
                .WithMessage("El campo '{PropertyName}' es obligatorio")
                .MaximumLength(100)
                .WithMessage("El campo '{PropertyName}' no debe superar las 50 letras")
                .WithName("Nombre");

            RuleFor(c => c.Descripcion)
                .NotEmpty()
                .WithMessage("El campo '{PropertyName}' es obligatorio")
                .MaximumLength(255)
                .WithMessage("El campo '{PropertyName}' no debe superar las 255 letras")
                .WithName("Descripcion");

            RuleFor(c => c.ConceptoContableNombre)
                .MaximumLength(255)
                .WithMessage("El campo 'Nombre de Concepto Contable' no debe superar las 255 letras")
                .WithName("ConceptoContableNombre");

            RuleFor(c => c.ConceptoContableValor)
                .MaximumLength(255)
                .WithMessage("El campo 'Valor de Concepto Contable' no debe superar las 255 letras")
                .WithName("ConceptoContableValor");
        }
    }
}
