using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Impuestos;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Impuestos;

public class ImpuestoConfiguration : BaseWithInt32IdEntityConfiguration<Impuesto>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Impuesto> builder)
    {
        builder.ToTable("com_Impuestos");
        builder.Property(i => i.Nombre).HasMaxLength(250);
        builder.Property(i => i.Descripcion).HasMaxLength(500);
    }
}

public class ImpuestoTipoConfiguration : BaseFixedEntityInt16Configuration<ImpuestoTipo>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ImpuestoTipo> builder)
    {
        builder.ToTable("com_ImpuestoTipos");
        builder.Property(i => i.Valor).HasMaxLength(250);
        builder.Property(i => i.Descripcion).HasMaxLength(500);
    }

    protected override void LoadSeedingData()
    {
        SeedingData.AddRange(
                new ImpuestoTipo { Idm = ImpuestoTipo.IVA, Valor = "IVA", Descripcion = "Impuesto al valor agregado", General = true },
                new ImpuestoTipo { Idm = ImpuestoTipo.INTERNO, Valor = "INT", Descripcion = "Impuestos internos", General = false },
                new ImpuestoTipo { Idm = ImpuestoTipo.PROVINCIAL, Valor = "PROV", Descripcion = "Impuestos provinciales", General = false }
        );
    }
}

public class ImpuestoDetalleConfiguration : BaseWithInt32IdEntityConfiguration<ImpuestoDetalle>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ImpuestoDetalle> builder)
    {
        builder.ToTable("com_ImpuestoDetalles");
        builder.Property(i => i.Descripcion).HasMaxLength(400);
        builder.Property(i => i.ImporteTotal).HasColumnType("money");
    }
}