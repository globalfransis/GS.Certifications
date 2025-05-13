using GS.Certifications.Domain.Commons.Constants;
using GSF.Domain.Entities.Security;
using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Certifications.Infrastructure.Persistence.DbContexts.Seeding;

public class GroupsSeeding : BaseWithIdEntityConfiguration<Group>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Group> builder)
    {
        //Only for seeding!. Actual configuration in Gsf.Infrastructure.GroupConfigiration.cs
    }

    protected override void LoadSeedingData()
    {
        SeedingData.AddRange(
            new Group
            {
                Id = 1000, // HACER CONSTANTE
                Name = "AdminSocios",
                Description = "AdminSocios",
                InternalCode = "AdminSocios",
                DomainFIdm = DomainFIdmConstants.Socios,
                SystemUse = true,
                GroupOwnerId = 1,
            });
    }
}