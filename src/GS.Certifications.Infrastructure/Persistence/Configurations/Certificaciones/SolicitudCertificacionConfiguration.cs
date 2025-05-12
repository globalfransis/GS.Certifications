using GS.Certifications.Domain.Entities.Certificaciones;
using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.SolicitudCertificaciones
{
    public class SolicitudCertificacionConfiguration : BaseWithInt32IdEntityConfiguration<SolicitudCertificacion>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<SolicitudCertificacion> builder)
        {
            builder.ToTable("cer_SolicitudCertificaciones");
            builder.Property(i => i.Observaciones).HasMaxLength(5000);
        }
    }

    public class SolicitudCertificacionEstadoConfiguration : BaseFixedEntityInt16Configuration<SolicitudCertificacionEstado>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<SolicitudCertificacionEstado> builder)
        {
            builder.ToTable("cer_SolicitudCertificacionEstados");

            builder.Property(i => i.Descripcion).IsRequired().HasMaxLength(500);
        }

        protected override void LoadSeedingData()
        {
            SeedingData.AddRange(
                new SolicitudCertificacionEstado() { Idm = SolicitudCertificacionEstado.PENDIENTE, Descripcion = "Pendiente" },
                new SolicitudCertificacionEstado() { Idm = SolicitudCertificacionEstado.EN_PROCESO, Descripcion = "En proceso" },
                new SolicitudCertificacionEstado() { Idm = SolicitudCertificacionEstado.APROBADA, Descripcion = "Aprobada" },
                new SolicitudCertificacionEstado() { Idm = SolicitudCertificacionEstado.RECHAZADA, Descripcion = "Rechazada" }
            );
        }
    }


}
