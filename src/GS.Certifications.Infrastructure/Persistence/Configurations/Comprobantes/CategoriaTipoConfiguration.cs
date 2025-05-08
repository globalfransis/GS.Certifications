using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Comprobantes;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Comprobantes;

public class CategoriaTipoConfiguration : BaseFixedEntityInt16Configuration<CategoriaTipo>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CategoriaTipo> builder)
    {
        builder.ToTable("com_CategoriaTipos");

        builder.Property(i => i.CodigoArca).IsRequired().HasMaxLength(100);
        builder.Property(i => i.Descripcion).IsRequired().HasMaxLength(500);
        builder.Property(i => i.CodigoExterno).HasMaxLength(100);
    }

    protected override void LoadSeedingData()
    {
        SeedingData.AddRange(
            new CategoriaTipo() { Idm = CategoriaTipo.RESPONSABLE_INSCRIPTO, Descripcion = CategoriaTipo.RESPONSABLE_INSCRIPTO_DESC, CodigoArca = CategoriaTipo.RESPONSABLE_INSCRIPTO_CODIGO_ARCA },
            new CategoriaTipo() { Idm = CategoriaTipo.RESPONSABLE_NO_INSCRIPTO, Descripcion = CategoriaTipo.RESPONSABLE_NO_INSCRIPTO_DESC, CodigoArca = CategoriaTipo.RESPONSABLE_NO_INSCRIPTO_CODIGO_ARCA },
            new CategoriaTipo() { Idm = CategoriaTipo.NO_RESPONSABLE, Descripcion = CategoriaTipo.NO_RESPONSABLE_DESC, CodigoArca = CategoriaTipo.NO_RESPONSABLE_CODIGO_ARCA },
            new CategoriaTipo() { Idm = CategoriaTipo.SUJETO_EXENTO, Descripcion = CategoriaTipo.SUJETO_EXENTO_DESC, CodigoArca = CategoriaTipo.SUJETO_EXENTO_CODIGO_ARCA },
            new CategoriaTipo() { Idm = CategoriaTipo.CONSUMIDOR_FINAL, Descripcion = CategoriaTipo.CONSUMIDOR_FINAL_DESC, CodigoArca = CategoriaTipo.CONSUMIDOR_FINAL_CODIGO_ARCA },
            new CategoriaTipo() { Idm = CategoriaTipo.RESPONSABLE_MONOTRIBUTO, Descripcion = CategoriaTipo.RESPONSABLE_MONOTRIBUTO_DESC, CodigoArca = CategoriaTipo.RESPONSABLE_MONOTRIBUTO_CODIGO_ARCA },
            new CategoriaTipo() { Idm = CategoriaTipo.SUJETO_NO_CATEGORIZADO, Descripcion = CategoriaTipo.SUJETO_NO_CATEGORIZADO_DESC, CodigoArca = CategoriaTipo.SUJETO_NO_CATEGORIZADO_CODIGO_ARCA },
            new CategoriaTipo() { Idm = CategoriaTipo.IMPORTADOR_EXTERIOR, Descripcion = CategoriaTipo.IMPORTADOR_EXTERIOR_DESC, CodigoArca = CategoriaTipo.IMPORTADOR_EXTERIOR_CODIGO_ARCA },
            new CategoriaTipo() { Idm = CategoriaTipo.CLIENTE_EXTERIOR, Descripcion = CategoriaTipo.CLIENTE_EXTERIOR_DESC, CodigoArca = CategoriaTipo.CLIENTE_EXTERIOR_CODIGO_ARCA },
            new CategoriaTipo() { Idm = CategoriaTipo.LIBERADO, Descripcion = CategoriaTipo.LIBERADO_DESC, CodigoArca = CategoriaTipo.LIBERADO_CODIGO_ARCA },
            new CategoriaTipo() { Idm = CategoriaTipo.AGENTE_PERCEPCION, Descripcion = CategoriaTipo.AGENTE_PERCEPCION_DESC, CodigoArca = CategoriaTipo.AGENTE_PERCEPCION_CODIGO_ARCA }
        );
    }
}