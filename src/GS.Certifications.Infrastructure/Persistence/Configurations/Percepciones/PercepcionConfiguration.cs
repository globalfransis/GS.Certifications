using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Impuestos;
using GS.Certifications.Domain.Entities.Percepciones;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Percepciones;

public class PercepcionConfiguration : BaseWithInt32IdEntityConfiguration<Percepcion>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Percepcion> builder)
    {
        builder.ToTable("per_Percepciones");
        builder.Property(i => i.Descripcion).HasMaxLength(500);
    }
}

public class PercepcionTipoConfiguration : BaseFixedEntityInt16Configuration<PercepcionTipo>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PercepcionTipo> builder)
    {
        builder.ToTable("per_PercepcionTipos");
        builder.Property(i => i.Valor).HasMaxLength(500);
        builder.Property(i => i.Descripcion).HasMaxLength(500);
    }

    protected override void LoadSeedingData()
    {
        SeedingData.AddRange(
                new PercepcionTipo { Idm = PercepcionTipo.IVA, Valor = "IVA", Descripcion = "Percepción de IVA", General = true },
                new PercepcionTipo { Idm = PercepcionTipo.IIBB, Valor = "IIBB", Descripcion = "Percepción de Ingresos Brutoss", General = false },
                new PercepcionTipo { Idm = PercepcionTipo.MUNICIPALES, Valor = "MUNICIPAL", Descripcion = "Percepción Municipal", General = false }
        );
    }
}

public class PercepcionDetalleConfiguration : BaseWithInt32IdEntityConfiguration<PercepcionDetalle>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PercepcionDetalle> builder)
    {
        builder.ToTable("com_PercepcionDetalles");
        builder.Property(i => i.Descripcion).HasMaxLength(500);
        builder.Property(i => i.ImporteTotal).HasColumnType("money");
    }
}

public class PercepcionCompanyConfiguration : BaseWithInt32IdEntityConfiguration<PercepcionCompany>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PercepcionCompany> builder)
    {
        builder.ToTable("com_PercepcionCompanies");
    }
}