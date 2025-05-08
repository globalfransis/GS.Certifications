using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.MailProcessors;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.MailProcessors;

public class ComprobanteEMailExtensionConfiguration : BaseWithInt32IdEntityConfiguration<ComprobanteEMailExtension>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ComprobanteEMailExtension> builder)
    {
        builder.ToTable("mpc_ComprobanteEMailExtensiones");
    }
}