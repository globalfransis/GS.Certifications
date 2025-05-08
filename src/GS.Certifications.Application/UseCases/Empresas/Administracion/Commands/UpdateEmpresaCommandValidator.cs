using FluentValidation;
using GS.Certifications.Application.CQRS.DbContexts;
using GSF.Application.Common.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Commands
{
    public class UpdateEmpresaCommandValidator : AbstractGSValidator<CreateEmpresaCommand>
    {
        private readonly ICertificationsDbContext context;
        public UpdateEmpresaCommandValidator(ICertificationsDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));

            RuleFor(c => c.CodigoProveedor)
                .NotEmpty()
                .WithMessage("El campo '{PropertyName}' es obligatorio")
                .MaximumLength(15)
                .WithMessage("El campo '{PropertyName}' no debe superar las 15 letras")
                .WithName("CodigoProveedor");

            RuleFor(c => c.RazonSocial)
                .NotEmpty()
                .WithMessage("El campo '{PropertyName}' es obligatorio")
                .MaximumLength(100)
                .WithMessage("El campo '{PropertyName}' no debe superar las 100 letras")
                .WithName("RazonSocial");

            RuleFor(c => c.NombreFantasia)
                .NotEmpty()
                .WithMessage("El campo '{PropertyName}' es obligatorio")
                .MaximumLength(100)
                .WithMessage("El campo '{PropertyName}' no debe superar las 100 letras")
                .WithName("NombreFantasia");

            RuleFor(c => c.IdentificadorTributario)
                .NotEmpty()
                .WithMessage("El campo '{PropertyName}' es obligatorio")
                .MaximumLength(30)
                .WithMessage("El campo '{PropertyName}' no debe superar las 30 letras")
                .WithName("IdentificadorTributario");

            RuleFor(c => c.Direccion)
                .MaximumLength(120)
                .WithMessage("El campo '{PropertyName}' no debe superar las 120 letras")
                .WithName("Direccion");

            RuleFor(c => c.CodigoPostal)
                .MaximumLength(20)
                .WithMessage("El campo '{PropertyName}' no debe superar las 20 letras")
                .WithName("CodigoPostal");

            RuleFor(c => c.CiudadDescripcion)
                .MaximumLength(1000)
                .WithMessage("El campo '{PropertyName}' no debe superar las 1000 letras")
                .WithName("CiudadDescripcion");

            RuleFor(c => c.TelefonoPrincipal)
                .NotEmpty()
                .WithMessage("El campo '{PropertyName}' es obligatorio")
                .MaximumLength(25)
                .WithMessage("El campo '{PropertyName}' no debe superar las 25 letras")
                .WithName("TelefonoPrincipal");

            RuleFor(c => c.TelefonoAlternativo)
                .MaximumLength(25)
                .WithMessage("El campo '{PropertyName}' no debe superar las 25 letras")
                .WithName("TelefonoAlternativo");

            RuleFor(c => c.EmailPrincipal)
                .NotEmpty()
                .WithMessage("El campo '{PropertyName}' es obligatorio")
                .MaximumLength(100)
                .WithMessage("El campo '{PropertyName}' no debe superar las 100 letras")
                .WithName("EmailPrincipal");

            RuleFor(c => c.EmailAlternativo)
                .MaximumLength(150)
                .WithMessage("El campo '{PropertyName}' no debe superar las 150 letras")
                .WithName("EmailAlternativo");

            RuleFor(c => c.Contacto)
                .NotEmpty()
                .WithMessage("El campo '{PropertyName}' es obligatorio")
                .MaximumLength(30)
                .WithMessage("El campo '{PropertyName}' no debe superar las 30 letras")
                .WithName("Contacto");

            RuleFor(c => c.ContactoAlternativo)
                .MaximumLength(30)
                .WithMessage("El campo '{PropertyName}' no debe superar las 30 letras")
                .WithName("ContactoAlternativo");

            RuleFor(c => c.NumeroIngresosBrutos)
                .MaximumLength(30)
                .WithMessage("El campo '{PropertyName}' no debe superar las 30 letras")
                .WithName("NumeroIngresosBrutos");

            RuleFor(c => c.CuentaBancaria)
                .MaximumLength(50)
                .WithMessage("El campo '{PropertyName}' no debe superar las 50 letras")
                .WithName("CuentaBancaria");

            RuleFor(c => c.IdMoneda)
               .MaximumLength(10)
               .WithMessage("El campo '{PropertyName}' no debe superar las 10 letras")
               .WithName("IdMoneda");

            RuleFor(c => c.PaginaWeb)
                .MaximumLength(100)
                .WithMessage("El campo '{PropertyName}' no debe superar las 100 letras")
                .WithName("PaginaWeb");

            RuleFor(c => c.RedesSociales)
                .MaximumLength(200)
                .WithMessage("El campo '{PropertyName}' no debe superar las 200 letras")
                .WithName("RedesSociales");

            RuleFor(c => c.DescripcionEmpresa)
                .MaximumLength(500)
                .WithMessage("El campo '{PropertyName}' no debe superar las 500 letras")
                .WithName("DescripcionEmpresa");

            RuleFor(c => c.ProductosServiciosOfrecidos)
                .MaximumLength(500)
                .WithMessage("El campo '{PropertyName}' no debe superar las 500 letras")
                .WithName("ProductosServiciosOfrecidos");

            RuleFor(c => c.ReferenciasComerciales)
                .MaximumLength(500)
                .WithMessage("El campo '{PropertyName}' no debe superar las 500 letras")
                .WithName("ReferenciasComerciales");
        }
    }
}
