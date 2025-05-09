using GSF.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using GS.Certifications.Web.Bootstrap;
using GS.Certifications.Infrastructure.Persistence.DbContexts;
using GS.Certifications.Infrastructure.Persistence.DbContexts.Seeding;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GS.Certifications.Web;

public class Program
{
    public async static Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using var scope = host.Services.CreateScope();

        var services = scope.ServiceProvider;

        var env = services.GetRequiredService<IWebHostEnvironment>();

        //Importante: eso es solo el valor default. Si se quieren cambiar en produccion
        //se puede hacer cambiandolo en el web config, pero no va a cambiar el valor que se obtiene de esta forma.
        //De todas maneras este es el valor default que usa .Net. Los componentes Vue estan hechos para trabajar con este valor
        //default pero pueden recibir otro.
        var iisServerOptions = services.GetRequiredService<IISServerOptions>();
        Environment.SetEnvironmentVariable("DefaultMaxFileSize", iisServerOptions.MaxRequestBodySize.ToString());

        if (env.IsDevelopment())
        {
            try
            {
                var context = services.GetRequiredService<CertificationsDbContext>();
                //context.Database.Migrate();
                //await ApplicationDbContextSeed.SeedAsync(context);
                //await CertificationsDbContextSeed.SeedAsync(context);
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, ex.Message);
                System.Threading.Thread.Sleep(5000);
                Debug.WriteLine("*********************************************************************************************");
                Debug.WriteLine("*** GSF Error while seeding Data: ");

                DumpExceptionMessage(ex);

                Debug.WriteLine("*** are all migrations executed with update-database?. \n*** Look at Seq for more details (http://localhost:5341).");
                Debug.WriteLine("*********************************************************************************************");
            }
        }
        else
        {
            await host.RunAsync();
        }

    }

    private static void DumpExceptionMessage(Exception ex, int level = 1)
    {
        var sep = new string('-', 3 * level);
        Debug.WriteLine($"|{sep}> [{ex.Message}].");
        if (ex.InnerException != null)
        {
            DumpExceptionMessage(ex.InnerException, ++level);
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }).UseSerilog();
}
