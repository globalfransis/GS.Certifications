﻿using FluentValidation;
using GS.Certifications.Application.CQRS.DbContexts;
using GSF.Application.Common.Validators;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Impuestos.Commands
{
    public class CreateImpuestoCommandValidator : AbstractGSValidator<CreateImpuestoCommand>
    {
        private readonly ICertificationsDbContext _context;
        public CreateImpuestoCommandValidator(ICertificationsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            RuleFor(c => c.Nombre)
                .NotEmpty()
                .WithMessage("El campo '{PropertyName}' es obligatorio")
                .MaximumLength(250)
                .WithMessage("El campo '{PropertyName}' no debe superar las 250 letras")
                .WithName("Nombre");

            RuleFor(c => c.Descripcion)
                .NotEmpty()
                .WithMessage("El campo '{PropertyName}' es obligatorio")
                .MaximumLength(500)
                .WithMessage("El campo '{PropertyName}' no debe superar las 500 letras")
                .WithName("Descripcion");
        }
    }
}
