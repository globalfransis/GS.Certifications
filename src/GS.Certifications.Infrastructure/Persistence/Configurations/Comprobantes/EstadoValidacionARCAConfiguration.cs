using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Comprobantes;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Comprobantes;

public class EstadoValidacionARCAConfiguration : BaseFixedEntityInt16Configuration<EstadoValidacionARCA>
{
    protected override void ConfigureEntity(EntityTypeBuilder<EstadoValidacionARCA> builder)
    {
        builder.ToTable("com_EstadosValidacionARCA");

        builder.Property(i => i.Descripcion).IsRequired().HasMaxLength(250);
    }

    protected override void LoadSeedingData()
    {
        SeedingData.AddRange(
            new EstadoValidacionARCA() { Idm = EstadoValidacionARCA.VALIDADA, Descripcion = "Validada" },
            new EstadoValidacionARCA() { Idm = EstadoValidacionARCA.RECHAZADA, Descripcion = "Rechazada" },
            new EstadoValidacionARCA() { Idm = EstadoValidacionARCA.ERROR_VALIDACION, Descripcion = "Error validación" },
            new EstadoValidacionARCA() { Idm = EstadoValidacionARCA.NO_VALIDADA, Descripcion = "No validada" }
        );
    }
}
