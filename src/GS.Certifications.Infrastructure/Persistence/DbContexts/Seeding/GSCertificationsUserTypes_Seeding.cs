using GS.Certifications.Domain.Commons.Constants;
using GSF.Domain.Entities.Security;
using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Certifications.Infrastructure.Persistence.DbContexts.Seeding;

public class GSCertificationsUserTypes_Seeding : BaseFixedEntityInt32Configuration<UserType>
{
    protected override void ConfigureEntity(EntityTypeBuilder<UserType> builder)
    {
        //Only for seeding!. Actual configuration in Gsf.Infrastructure.GroupConfigiration.cs
    }

    protected override void LoadSeedingData()
    {
        SeedingData.AddRange(
            new UserType
            {
                Idm = UserTypeIdmConstants.Socio,
                Code = "Socio",
                Description = "Usuario de socio",
            });
    }
}