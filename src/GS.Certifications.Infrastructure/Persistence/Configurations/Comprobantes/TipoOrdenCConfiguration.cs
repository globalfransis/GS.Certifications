using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Comprobantes;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Comprobantes;

public class TipoOrdenCConfiguration : BaseFixedEntityInt16Configuration<TipoOrdenC>
{
    protected override void ConfigureEntity(EntityTypeBuilder<TipoOrdenC> builder)
    {
        builder.ToTable("int_TipoOrdenC");

        builder.Property(i => i.Nombre).IsRequired().HasMaxLength(100);
        builder.Property(i => i.Codigo).IsRequired().HasMaxLength(10);
    }
}
