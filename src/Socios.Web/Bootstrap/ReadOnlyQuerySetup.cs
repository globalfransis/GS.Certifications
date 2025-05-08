using GS.Certifications.Application.CQRS.ReadOnlyQueries;
using GS.Certifications.Infrastructure.Persistence.ReadOnlyQueries;
using Microsoft.EntityFrameworkCore;

namespace Socios.Web.Bootstrap;

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
