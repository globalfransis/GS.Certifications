using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.MailProcessors;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.MailProcessors;

public class IntegracionFacturaPorCorreoDetalleConfiguration : BaseWithInt32IdEntityConfiguration<IntegracionFacturaPorCorreoDetalle>
{
    protected override void ConfigureEntity(EntityTypeBuilder<IntegracionFacturaPorCorreoDetalle> builder)
    {
        builder.ToTable("mpc_IntegracionFacturaPorCorreoDetalles");
    }
}