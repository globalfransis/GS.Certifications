using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Comprobantes;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Comprobantes;

public class CondicionVentaConfiguration : BaseFixedEntityInt16Configuration<CondicionVenta>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CondicionVenta> builder)
    {
        builder.ToTable("com_CondicionVentas");

        builder.Property(i => i.Descripcion).IsRequired().HasMaxLength(500);
    }

    protected override void LoadSeedingData()
    {
        SeedingData.AddRange(
            new CondicionVenta() { Idm = CondicionVenta.CONTADO, Descripcion = CondicionVenta.CONTADO_DESC },
            new CondicionVenta() { Idm = CondicionVenta.CUENTA_CORRIENTE, Descripcion = CondicionVenta.CUENTA_CORRIENTE_DESC },
            new CondicionVenta() { Idm = CondicionVenta.TARJETA_CREDITO, Descripcion = CondicionVenta.TARJETA_CREDITO_DESC },
            new CondicionVenta() { Idm = CondicionVenta.TARJETA_DEBITO, Descripcion = CondicionVenta.TARJETA_DEBITO_DESC },
            new CondicionVenta() { Idm = CondicionVenta.TRANSFERENCIA_BANCARIA, Descripcion = CondicionVenta.TRANSFERENCIA_BANCARIA_DESC },
            new CondicionVenta() { Idm = CondicionVenta.CHEQUE, Descripcion = CondicionVenta.CHEQUE_DESC },
            new CondicionVenta() { Idm = CondicionVenta.OTRO, Descripcion = CondicionVenta.OTRO_DESC }
        );
    }
}