using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Comprobantes;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Comprobantes;

public class OrigenConfiguration : BaseFixedEntityInt16Configuration<Origen>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Origen> builder)
    {
        builder.ToTable("com_Origenes");

        builder.Property(i => i.Descripcion).IsRequired().HasMaxLength(500);
    }

    protected override void LoadSeedingData()
    {
        SeedingData.AddRange(
            new Origen() { Idm = Origen.SOCIOS, Descripcion = nameof(Origen.SOCIOS) },
            new Origen() { Idm = Origen.BACKOFFICE, Descripcion = nameof(Origen.BACKOFFICE) },
            new Origen() { Idm = Origen.CORREO, Descripcion = nameof(Origen.CORREO) }
        );
    }
}