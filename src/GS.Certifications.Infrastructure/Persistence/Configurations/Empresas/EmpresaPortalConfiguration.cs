using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Empresas;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Empresas
{
    public class EmpresaConfiguration : BaseWithInt32IdEntityConfiguration<EmpresaPortal>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<EmpresaPortal> builder)
        {
            builder.ToTable("emp_EmpresasPortales");

            builder.Property(i => i.CodigoProveedor).IsRequired().HasMaxLength(15);
            builder.Property(i => i.RazonSocial).IsRequired().HasMaxLength(100);
            builder.Property(i => i.NombreFantasia).IsRequired().HasMaxLength(100);
            builder.Property(i => i.IdentificadorTributario).IsRequired().HasMaxLength(30);
            builder.Property(i => i.Direccion).HasMaxLength(120);
            builder.Property(i => i.CodigoPostal).HasMaxLength(20);
            builder.Property(i => i.CiudadDescripcion).HasMaxLength(1000);
            builder.Property(i => i.TelefonoPrincipal).IsRequired().HasMaxLength(25);
            builder.Property(i => i.TelefonoAlternativo).HasMaxLength(25);
            builder.Property(i => i.EmailPrincipal).IsRequired().HasMaxLength(150);
            builder.Property(i => i.EmailAlternativo).HasMaxLength(150);
            builder.Property(i => i.Contacto).IsRequired().HasMaxLength(30);
            builder.Property(i => i.ContactoAlternativo).HasMaxLength(30);
            builder.Property(i => i.NumeroIngresosBrutos).HasMaxLength(30);
            builder.Property(i => i.CuentaBancaria).HasMaxLength(50);
            builder.Property(i => i.PaginaWeb).HasMaxLength(100);
            builder.Property(i => i.RedesSociales).HasMaxLength(200);
            builder.Property(i => i.DescripcionEmpresa).HasMaxLength(500);
            builder.Property(i => i.ProductosServiciosOfrecidos).HasMaxLength(500);
            builder.Property(i => i.ReferenciasComerciales).HasMaxLength(500);
        }
    }

    public class CompanyExtraConfiguration : BaseWithInt32IdEntityConfiguration<CompanyExtra>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<CompanyExtra> builder)
        {
            builder.ToTable("emp_CompanyExtras");


            builder.Property(i => i.Direccion).HasMaxLength(120);
            builder.Property(i => i.CodigoPostal).HasMaxLength(20);
            builder.Property(i => i.CiudadDescripcion).HasMaxLength(1000);
            builder.Property(i => i.TelefonoPrincipal).IsRequired().HasMaxLength(25);
            builder.Property(i => i.TelefonoAlternativo).HasMaxLength(25);
            builder.Property(i => i.EmailPrincipal).IsRequired().HasMaxLength(150);
            builder.Property(i => i.EmailAlternativo).HasMaxLength(150);
            builder.Property(i => i.Contacto).IsRequired().HasMaxLength(30);
            builder.Property(i => i.ContactoAlternativo).HasMaxLength(30);
            builder.Property(i => i.NumeroIngresosBrutos).HasMaxLength(30);
            builder.Property(i => i.CuentaBancaria).HasMaxLength(50);
            builder.Property(i => i.IdMoneda).HasMaxLength(10);
            builder.Property(i => i.IdentificadorTributario).IsRequired().HasMaxLength(30);
        }
    }

}
