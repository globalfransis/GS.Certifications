using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GS.Certifications.Infrastructure.Persistence.ReadOnlyQueries;
using GS.Certifications.Application.CQRS.ReadOnlyQueries;

namespace GS.Certifications.Web.Bootstrap;

public static class ReadOnlyQuerySetup
{
    public static IServiceCollection AddReadOnlyQueryService(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        CertificationsReadOnlyQueryDataBaseConfiguration dataBaseConfiguration = new();
        dataBaseConfiguration.ConnectionString = configuration.GetConnectionString("DefaultReadOnlyConnection");
        services.AddSingleton<ICertificationsReadOnlyQueryDataBaseConfiguration>(dataBaseConfiguration);

        services.AddSingleton<ICertificationsReadOnlyQuery, CertificationsReadOnlyQuery>();

        return services;
    }
}
