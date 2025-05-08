using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Comprobantes;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Comprobantes;

public class CodigoAutorizacionTipoConfiguration : BaseFixedEntityInt16Configuration<CodigoAutorizacionTipo>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CodigoAutorizacionTipo> builder)
    {
        builder.ToTable("com_CodigoAutorizacionTipos");
        builder.Property(i => i.CodigoArca).HasMaxLength(250);
        builder.Property(i => i.CodigoExterno).HasMaxLength(250);
        builder.Property(i => i.Descripcion).HasMaxLength(500);
    }

    protected override void LoadSeedingData()
    {
        SeedingData.AddRange(
            new CodigoAutorizacionTipo() { Idm = CodigoAutorizacionTipo.CAE, Descripcion = nameof(CodigoAutorizacionTipo.CAE) },
            new CodigoAutorizacionTipo() { Idm = CodigoAutorizacionTipo.CAEA, Descripcion = nameof(CodigoAutorizacionTipo.CAEA) },
            new CodigoAutorizacionTipo() { Idm = CodigoAutorizacionTipo.CAI, Descripcion = nameof(CodigoAutorizacionTipo.CAI) }
        );
    }
}