using GS.Certifications.Domain.Commons.Constants;
using GSF.Domain.Entities.Systems;
using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Certifications.Infrastructure.Persistence.DbContexts.Seeding;

public class GSCertificationsDomainFs_Seeding : BaseFixedEntityConfiguration<DomainF>
{
    protected override void ConfigureEntity(EntityTypeBuilder<DomainF> builder)
    {
        //Only for seeding!. Actual configuration in Gsf.Infrastructure.GroupConfigiration.cs
    }

    protected override void LoadSeedingData()
    {
        SeedingData.AddRange(
            new DomainF
            {
                Idm = DomainFIdmConstants.Socios,
                Name = "Socio",
                Description = "Usuario de socio",
                UserTypeIdm = DomainFIdmConstants.Socios
            });
    }
}