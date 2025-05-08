using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Infrastructure.Persistence.DbContexts;
using GSF.Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GS.Certifications.Infrastructure.Bootstrap;

public static class InfrastructureLayerSetup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {

        services.AddDbContext<CertificationsDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(CertificationsDbContext).Assembly.FullName));
#if DEBUG
            options.EnableSensitiveDataLogging();
#endif
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<CertificationsDbContext>());
        services.AddScoped<ICertificationsDbContext>(provider => provider.GetService<CertificationsDbContext>());

        GSF.Infrastructure.InfrastructureLayerSetup.AddCommonInfrastructure(services, configuration, environment);


        return services;
    }
}

