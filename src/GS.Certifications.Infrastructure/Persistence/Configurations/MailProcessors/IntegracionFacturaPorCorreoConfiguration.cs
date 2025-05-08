using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.MailProcessors;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.MailProcessors;

public class IntegracionFacturaPorCorreoConfiguration : BaseWithInt32IdEntityConfiguration<IntegracionFacturaPorCorreo>
{
    protected override void ConfigureEntity(EntityTypeBuilder<IntegracionFacturaPorCorreo> builder)
    {
        builder.ToTable("mpc_IntegracionesFacturaPorCorreo")
               .Property(p => p.AutoCreateEmpresaPortal).HasDefaultValue(true);
    }
}