using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Empresas;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Empresas;

public class TipoCuentaConfiguration : BaseFixedEntityInt16Configuration<TipoCuenta>
{
    protected override void ConfigureEntity(EntityTypeBuilder<TipoCuenta> builder)
    {
        builder.ToTable("emp_TipoCuentas");
        builder.Property(i => i.Nombre).HasMaxLength(50);
        builder.Property(i => i.Descripcion).HasMaxLength(250);
    }

    protected override void LoadSeedingData()
    {
        SeedingData.AddRange(
            new TipoCuenta() { Idm = TipoCuenta.CUENTA_CORRIENTE, Nombre = "Cuenta Corriente", Descripcion = "Cuenta básica para depósitos y retiros diarios." },
            new TipoCuenta() { Idm = TipoCuenta.CAJA_AHORRO, Nombre = "Caja de Ahorro", Descripcion = "Cuenta para ahorrar con disponibilidad inmediata." },
            new TipoCuenta() { Idm = TipoCuenta.CUENTA_SUELDO, Nombre = "Cuenta Sueldo", Descripcion = "Cuenta para recibir el salario de los empleados." },
            new TipoCuenta() { Idm = TipoCuenta.CUENTA_INVERSION, Nombre = "Cuenta de Inversión", Descripcion = "Cuenta para invertir en diferentes instrumentos financieros." },
            new TipoCuenta() { Idm = TipoCuenta.CUENTA_ESPECIAL, Nombre = "Cuenta Especial", Descripcion = "Cuenta para fines específicos, como el pago de impuestos." }
        );
    }
}