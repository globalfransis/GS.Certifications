using GSF.Domain.Common;
using GSF.Domain.Entities.Security;
using GSF.Domain.Entities.Systems;
using GSF.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Domain.Commons.Constants;
using System;
using System.Threading.Tasks;


namespace GS.Certifications.Infrastructure.Persistence.DbContexts.Seeding;

public static class GSCertificationsDbContextSeed
{
    public const long BackOfficeIdm = DomainFIdmConstants.CentrosNavegacion;

    public static async Task SeedAsync(ICertificationsDbContext context)
    {
        ApplicationDbContextSeedSql.SeedStoredProcedures(context, @"GS.Certifications.Infrastructure\Persistence\SqlScripts");

        if (await context.DomainFs.AnyAsync())
        {
            var domain = await context.DomainFs.FirstOrDefaultAsync(x => x.Idm == DomainFIdmConstants.CentrosNavegacion);
            domain.Name = "BackOffice";
            domain.Description = "BackOffice";
            domain.RowVersion = domain.RowVersion;
            domain.UserTypeIdm = UserTypeIdmConstants.Backend;


        }

        if (await context.Users.AnyAsync())
        {
            var adminUser = await context.Users.FirstOrDefaultAsync(x => x.Id == 1);

            if (adminUser is not null)
            {
                User newAdminUSer = new()
                {

                    Login = "admin",
                    Email = "soporte@globalsis.com.ar",
                    IdNumber = "99000000",
                    LastName = "Del Sistema",
                    FirstName = "Administrador",

                    SystemUse = true,
                    Activated = true,
                    Blocked = false,
                    LoginFailedAttempts = 0,

                    Salt = adminUser.Salt,
                    Hash = adminUser.Hash,

                    GroupOwnerId = adminUser.GroupOwnerId,
                    UserTypeIdm = UserTypeIdmConstants.Socio,


                };

                context.Users.Add(newAdminUSer);


                UserDomainF adminUserDomain = new() { User = newAdminUSer, DomainFIdm = DomainFIdmConstants.Socios };
                context.UserDomainFs.Add(adminUserDomain);

                Group group = await context.Groups.FirstOrDefaultAsync(x => x.InternalCode == GroupsInternalCodeConstants.Admin);

                if (group is not null)
                {
                    CompanyUserGroup cug = new() { User = newAdminUSer, Group = group, CompanyId = 39 };

                    context.CompaniesUsersGroups.Add(cug);
                }
            }

        }

        try
        {
            await context.SaveChangesAsync(default);
        }
        catch (Exception ex)
        {
            var sadsa = ex.Message;
            throw;
        }


    }
}

public static class SeedExtension
{
    public static T FillAuditory<T>(this T entity) where T : CommonBaseEntity
    {
        entity.Created = DateTime.Parse("1986-06-29");
        entity.CreatedBy = "Temp Seed";
        entity.IsDeleted = false;
        return entity;
    }
}

