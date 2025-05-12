using GS.Certifications.Domain.Entities.Certificaciones;
using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Certificaciones
{
    public class CertificacionConfiguration : BaseWithInt32IdEntityConfiguration<Certificacion>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Certificacion> builder)
        {
            builder.ToTable("cer_Certificaciones");
            builder.Property(i => i.Nombre).HasMaxLength(250);
            builder.Property(i => i.Descripcion).HasMaxLength(1000);
        }
    }
}
