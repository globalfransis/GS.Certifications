using GS.AI.DocumentIntelligence.Legacy.Extensions;
using GS.Certifications.Application.Commons.Services.UsuarioEmpresaPortales;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services.Analysis.Strategies;
using GSF.Application;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GS.Certifications.Application.Bootstrap;

public static class GSCertificationsApplicationLayerSetup
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var thisAssembly = Assembly.GetExecutingAssembly();

        _ = services.AddAutoMapper(Assembly.GetExecutingAssembly())
        .AddMediatR(thisAssembly);

        ApplicationLayerSetup.AddApplication(services);

        var appConfig = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        services.AddDocumentIntelligence(config =>
        {
            config.Endpoint = appConfig["GS.AI:DocumentIntelligence:Endpoint"];
            config.ApiKey = appConfig["GS.AI:DocumentIntelligence:ApiKey"];
            config.Custom = appConfig["GS.AI:DocumentIntelligence:Custom"];
        });


        services.AddTransient<IUsuarioEmpresaPortalService, UsuarioEmpresaPortalService>();
        services.AddTransient<IComprobanteHeaderStrategy, DefaultHeaderStrategy>();
        services.AddTransient<IComprobanteDetailStrategy, DefaultDetailStrategy>();
        services.AddTransient<IComprobanteTaxStrategy, DefaultTaxStrategy>();
        services.AddTransient<IComprobantePerceptionStrategy, DefaultPerceptionStrategy>();
        services.AddTransient<IComprobanteTotalStrategy, DefaultTotalStrategy>();

        return services;
    }
}
