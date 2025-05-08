using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Comprobantes;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Comprobantes;

public class ComprobanteEstadoConfiguration : BaseFixedEntityInt32Configuration<ComprobanteEstado>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ComprobanteEstado> builder)
    {
        builder.ToTable("com_ComprobanteEstados");

        builder.Property(i => i.Descripcion).IsRequired().HasMaxLength(500);
        builder.Property(i => i.Nombre).IsRequired().HasMaxLength(100);
        builder.Property(i => i.Valor).HasMaxLength(100);
    }

    protected override void LoadSeedingData()
    {
        SeedingData.AddRange(
            new ComprobanteEstado() { Idm = ComprobanteEstado.ARCHIVO_SUBIDO, Nombre = "Archivo subido", Descripcion = "Archivo subido", Valor = ComprobanteEstado.ARCHIVO_SUBIDO_VALOR },
            new ComprobanteEstado() { Idm = ComprobanteEstado.EN_PROCESO_CARGA, Nombre = "En proceso de carga", Descripcion = "En proceso de carga", Valor = ComprobanteEstado.EN_PROCESO_CARGA_VALOR },
            new ComprobanteEstado() { Idm = ComprobanteEstado.ERRORES_ARCA, Nombre = "Registro con errores ARCA", Descripcion = "Registro con errores ARCA", Valor = ComprobanteEstado.ERRORES_ARCA_VALOR },
            new ComprobanteEstado() { Idm = ComprobanteEstado.CONFIRMADO, Nombre = "Registro confirmado", Descripcion = "Registro confirmado", Valor = ComprobanteEstado.CONFIRMADO_VALOR },
            new ComprobanteEstado() { Idm = ComprobanteEstado.ACUSE_RECIBO_CLIENTE, Nombre = "Acuse Recibo Cliente", Descripcion = "Acuse Recibo Cliente", Valor = ComprobanteEstado.ACUSE_RECIBO_CLIENTE_VALOR },
            new ComprobanteEstado() { Idm = ComprobanteEstado.APROBADA_CLIENTE, Nombre = "Aprobada Cliente", Descripcion = "Aprobada Cliente", Valor = ComprobanteEstado.APROBADA_CLIENTE_VALOR },
            new ComprobanteEstado() { Idm = ComprobanteEstado.RECHAZADA_CLIENTE, Nombre = "Rechazada Cliente", Descripcion = "Rechazada Cliente", Valor = ComprobanteEstado.RECHAZADA_CLIENTE_VALOR },
            new ComprobanteEstado() { Idm = ComprobanteEstado.BORRADOR, Nombre = "Borrador", Descripcion = "Borrador", Valor = ComprobanteEstado.BORRADOR_VALOR },
            new ComprobanteEstado() { Idm = ComprobanteEstado.AUTORIZADO, Nombre = "Autorizado", Descripcion = "Autorizado", Valor = ComprobanteEstado.AUTORIZADO_VALOR }
        );
    }
}
