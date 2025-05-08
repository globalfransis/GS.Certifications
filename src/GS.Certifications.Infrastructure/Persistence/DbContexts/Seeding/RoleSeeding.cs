using GS.Certifications.Domain.Commons.Constants;
using GSF.Domain.Entities.Security;
using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Certifications.Infrastructure.Persistence.DbContexts.Seeding;

public class RoleSeeding : BaseWithIdEntityConfiguration<Role>
{
    public const long frameworkReserved = 1000;
    public const long ADMIN_SUPPLIER = frameworkReserved + 1;

    protected override void ConfigureEntity(EntityTypeBuilder<Role> builder)
    {
    }

    protected override void LoadSeedingData()
    {
        SeedingData.AddRange(
            new Role
            {
                Id = ADMIN_SUPPLIER,
                InternalCode = "Socios_Admin", // HACER CONSTANTE
                Name = "Admin",
                Description = "Administrador del Portal de Socios",
                GroupOwnerId = 1000, // HACER CONSTANTE
                SystemUse = true,
                DomainFIdm = DomainFIdmConstants.Socios
            }
            );
    }

}