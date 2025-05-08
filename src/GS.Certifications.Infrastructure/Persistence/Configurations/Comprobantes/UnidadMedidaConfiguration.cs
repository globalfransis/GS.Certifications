using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Comprobantes;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Comprobantes;

public class UnidadMedidaConfiguration : BaseFixedEntityInt16Configuration<UnidadMedida>
{
    protected override void ConfigureEntity(EntityTypeBuilder<UnidadMedida> builder)
    {
        builder.ToTable("com_UnidadMedidas");
        builder.Property(i => i.CodigoAFIP).HasMaxLength(50);
        builder.Property(i => i.CodigoARBA).HasMaxLength(50);
        builder.Property(i => i.Descripcion).HasMaxLength(50);
    }

    protected override void LoadSeedingData()
    {
        SeedingData.AddRange(
            new UnidadMedida() { Idm = UnidadMedida.MILIMETRO, CodigoAFIP = UnidadMedida.MILIMETRO_CODIGO_AFIP, CodigoARBA = UnidadMedida.MILIMETRO_CODIGO_ARBA, Descripcion = "Milímetro" },
            new UnidadMedida() { Idm = UnidadMedida.CENTIMETRO, CodigoAFIP = UnidadMedida.CENTIMETRO_CODIGO_AFIP, CodigoARBA = UnidadMedida.CENTIMETRO_CODIGO_ARBA, Descripcion = "Centímetro" },
            new UnidadMedida() { Idm = UnidadMedida.METRO, CodigoAFIP = UnidadMedida.METRO_CODIGO_AFIP, CodigoARBA = UnidadMedida.METRO_CODIGO_ARBA, Descripcion = "Metro" },
            new UnidadMedida() { Idm = UnidadMedida.MILIMETRO_CUADRADO, CodigoAFIP = UnidadMedida.MILIMETRO_CUADRADO_CODIGO_AFIP, CodigoARBA = UnidadMedida.MILIMETRO_CUADRADO_CODIGO_ARBA, Descripcion = "Milímetro cuadrado" },
            new UnidadMedida() { Idm = UnidadMedida.CENTIMETRO_CUADRADO, CodigoAFIP = UnidadMedida.CENTIMETRO_CUADRADO_CODIGO_AFIP, CodigoARBA = UnidadMedida.CENTIMETRO_CUADRADO_CODIGO_ARBA, Descripcion = "Centímetro cuadrado" },
            new UnidadMedida() { Idm = UnidadMedida.METRO_CUADRADO, CodigoAFIP = UnidadMedida.METRO_CUADRADO_CODIGO_AFIP, CodigoARBA = UnidadMedida.METRO_CUADRADO_CODIGO_ARBA, Descripcion = "Metro cuadrado" },
            new UnidadMedida() { Idm = UnidadMedida.GRAMO, CodigoAFIP = UnidadMedida.GRAMO_CODIGO_AFIP, CodigoARBA = UnidadMedida.GRAMO_CODIGO_ARBA, Descripcion = "Gramo" },
            new UnidadMedida() { Idm = UnidadMedida.KILOGRAMO, CodigoAFIP = UnidadMedida.KILOGRAMO_CODIGO_AFIP, CodigoARBA = UnidadMedida.KILOGRAMO_CODIGO_ARBA, Descripcion = "Kilogramo" },
            new UnidadMedida() { Idm = UnidadMedida.PORCENTAJE, CodigoAFIP = UnidadMedida.PORCENTAJE_CODIGO_AFIP, CodigoARBA = UnidadMedida.PORCENTAJE_CODIGO_ARBA, Descripcion = "Porcentaje" },
            new UnidadMedida() { Idm = UnidadMedida.SEGUNDO, CodigoAFIP = UnidadMedida.SEGUNDO_CODIGO_AFIP, CodigoARBA = UnidadMedida.SEGUNDO_CODIGO_ARBA, Descripcion = "Segundo" },
            new UnidadMedida() { Idm = UnidadMedida.MINUTO, CodigoAFIP = UnidadMedida.MINUTO_CODIGO_AFIP, CodigoARBA = UnidadMedida.MINUTO_CODIGO_ARBA, Descripcion = "Minuto" },
            new UnidadMedida() { Idm = UnidadMedida.HORA, CodigoAFIP = UnidadMedida.HORA_CODIGO_AFIP, CodigoARBA = UnidadMedida.HORA_CODIGO_ARBA, Descripcion = "Hora" },
            new UnidadMedida() { Idm = UnidadMedida.UNIDAD, CodigoAFIP = UnidadMedida.UNIDAD_CODIGO_AFIP, CodigoARBA = UnidadMedida.UNIDAD_CODIGO_ARBA, Descripcion = "Unidad" },
            new UnidadMedida() { Idm = UnidadMedida.MILLAR, CodigoAFIP = UnidadMedida.MILLAR_CODIGO_AFIP, CodigoARBA = UnidadMedida.MILLAR_CODIGO_ARBA, Descripcion = "Millar" },
            new UnidadMedida() { Idm = UnidadMedida.LITRO, CodigoAFIP = UnidadMedida.LITRO_CODIGO_AFIP, CodigoARBA = UnidadMedida.LITRO_CODIGO_ARBA, Descripcion = "Litro" },
            new UnidadMedida() { Idm = UnidadMedida.CENTIMETRO_CUBICO, CodigoAFIP = UnidadMedida.CENTIMETRO_CUBICO_CODIGO_AFIP, CodigoARBA = UnidadMedida.CENTIMETRO_CUBICO_CODIGO_ARBA, Descripcion = "Centímetro cúbico" },
            new UnidadMedida() { Idm = UnidadMedida.METRO_CUBICO, CodigoAFIP = UnidadMedida.METRO_CUBICO_CODIGO_AFIP, CodigoARBA = UnidadMedida.METRO_CUBICO_CODIGO_ARBA, Descripcion = "Metro cúbico" },
            new UnidadMedida() { Idm = UnidadMedida.PAR, CodigoAFIP = UnidadMedida.PAR_CODIGO_AFIP, CodigoARBA = UnidadMedida.PAR_CODIGO_ARBA, Descripcion = "Par" },
            new UnidadMedida() { Idm = UnidadMedida.UNA, CodigoAFIP = UnidadMedida.UNA_CODIGO_AFIP, CodigoARBA = UnidadMedida.UNA_CODIGO_ARBA, Descripcion = "Una" },
            new UnidadMedida() { Idm = UnidadMedida.TONELADA, CodigoAFIP = UnidadMedida.TONELADA_CODIGO_AFIP, CodigoARBA = UnidadMedida.TONELADA_CODIGO_ARBA, Descripcion = "Tonelada" }
        );
    }
}