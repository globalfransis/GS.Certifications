using FluentValidation;
using GS.Certifications.Application.CQRS.DbContexts;
using GSF.Application.Common.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Percepciones.Commands
{
    public class UpdatePercepcionCommandValidator : AbstractGSValidator<UpdatePercepcionCommand>
    {
        private readonly ICertificationsDbContext _context;
        public UpdatePercepcionCommandValidator(ICertificationsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            RuleFor(c => c.Descripcion)
                .NotEmpty()
                .WithMessage("El campo '{PropertyName}' es obligatorio")
                .MaximumLength(500)
                .WithMessage("El campo '{PropertyName}' no debe superar las 500 letras")
                .WithName("Descripcion");
        }
    }
}
