using AlertasGSF.Interfaces;
using AlertasGSF.ReadOnlyQueries;
using AlertasGSF.ReadOnlyQueries.Dapper;
using AlertasGSF.Services;
using Destructurama;
using FluentValidation.AspNetCore;
using GS.Certifications.Application.Bootstrap;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.Interfaces;
using GS.Certifications.Infrastructure.Bootstrap;
using GSF.Application.Common.Interfaces;
using GSF.Application.Interfaces;
using GSF.Application.Security.SecurityTemp;
using GSF.Application.Security.Services.CurrentCompany;
using GSF.Application.Security.Token;
using GSF.Infrastructure.Identity;
using GSFWebFileTransferService.Abstractions.Builder;
using GSFWebFileTransferService.Abstractions.DefaultValueObjects;
using GSFWebFileTransferService.WebUtilities.Builders;
using GSFWebFileTransferService.WebUtilities.FileSignatures;
using GSFWebFileTransferService.WebUtilities.Implementations.FileSignatures;
using HostCore.Services.Encription;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using Serilog.AspNetCore;
using Serilog.Events;
using Serilog.Filters;
using Socios.Web.Common.Resources;
using Socios.Web.Common.Serialization;
using Socios.Web.Common.Services;
using Socios.Web.Common.Services.Common;
using Socios.Web.Common.Services.Empresa;
using Socios.Web.Common.Services.Empresa.Storage;
using Socios.Web.Common.Services.Handshake;
using Socios.Web.Common.Services.Modules;
using Socios.Web.Common.Services.Token;
using Socios.Web.Common.Session;
using Socios.Web.Filters;
using Socios.Web.Hubs;
using Socios.Web.Middlewares;
using System.Reflection;


namespace Socios.Web.Bootstrap;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    /// <summary>
    /// Configures the available services.
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddLocalization();

        services.AddDistributedMemoryCache();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromHours(3);
            //options.IdleTimeout = TimeSpan.FromSeconds(10);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
            options.Cookie.Name = "Socios.Web.Session";

        });

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
            {
                config.ExpireTimeSpan = TimeSpan.FromHours(3);
                config.Cookie.Name = "Socios.Web.Auth";
                config.LoginPath = "/Security/Login";

            });

        services.AddTransient<IAlertaCRUDService, AlertaCRUDService>();
        services.AddTransient<IReadOnlyQuery, DapperReadOnlyQueryService>();

        AddSerilogConfiguration();

        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var applicationAssemblies = assemblies.Where(a => a.GetName().Name.EndsWith("Application"));
        services.AddScoped<IIdentityService, IdentityService>();

        foreach (var assembly in applicationAssemblies)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly(), assembly);
        }

        services.AddHttpContextAccessor();

        services.AddApplication();

        AddApplicationImplementations(services, Configuration);

        services.AddSingleton<IReadOnlyQueryDataBaseConfiguration, ReadOnlyQueryDataBaseConfiguration>();

        services.AddSingleton<IReadOnlyQueryDataBaseConfiguration>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            return new ReadOnlyQueryDataBaseConfiguration
            {
                ConnectionString = configuration["ConnectionStrings:DefaultConnection"]
            };
        });

        services.AddInfrastructure(Configuration, Environment);

        services.AddScoped<IArgumentFowardingService, ArgumentFowardingService>();

        services.AddScoped<INotificationSuscribedUsersService, NotifiedUserServiceMock>();

        services.AddScoped<INotificationService, NotificationServiceMock>();

        services.AddSignalR();

        services.AddControllers();

        services.AddScoped<IISServerOptions, IISServerOptions>();

        services.AddRazorPages()
            .AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.Converters.Add(new SimpleValueObjectJsonConverter());
                //opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            })
            .AddViewLocalization()
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                //Set different resource file to different pages can be achieved by using the `type` parameter with condition logic. (maybe asking for some attribute?)
                factory.Create(typeof(AttributeResources));
            })
            .AddRazorRuntimeCompilation()
            .AddRazorPagesOptions(options =>
            {
            })
            .AddMvcOptions(options =>
            {
                options.Filters.Add(new ValidationFilter(Environment));
                //options.Filters.Add(new ApiExceptionFilterAttribute());
                options.Filters.Add(new SessionFilter());

            })
            .AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<IApplicationDbContext>();
                fv.RegisterValidatorsFromAssemblyContaining<ICertificationsDbContext>();
                fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                fv.ImplicitlyValidateChildProperties = true;
            });

        services.AddReadOnlyQueryService(Configuration, Environment);

        services.AddTransient<ICurrentEmpresaPortalRepository, SessionEmpresaRepository>();
        services.AddTransient<ICurrentSocioService, CurrentSocioService>();


    }
    /// <summary>
    /// Configures UI implementations of application interfaces.
    /// </summary>
    private static void AddApplicationImplementations(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddScoped<ICurrentDomainService, CurrentDomainService>();

        services.AddScoped<ICurrentCompanyService, CurrentCompanyService>();

        services.AddScoped<ISecurityTempStoreService, SecuritySessionStoreService>();

        services.AddScoped<ITargetPathResolver, TargetPathResolver>();

        services.AddScoped<IAppInfoService, AppInfoService>();

        services.AddScoped<IStandardLoginService, WebLoginService>();

        services.AddScoped<ISociosContextDataTempStoreService, SociosContextDataSessionStoreService>();

        services.AddScoped<IParametersSessionStoreService, ParametersSessionStoreService>();

        ModulesConfiguration modulesConfiguration = new();
        configuration.Bind("GS.MIFRO.Host", modulesConfiguration);
        services.AddSingleton(modulesConfiguration);
        services.AddSingleton<IModulesService, ModulesService>();
        services.AddTransient<IHandshakeService, HandshakeService>();
        services.AddScoped<IEncriptionService, AesEncryptionService>();

        #region Clases relacionados al manejo de token
        services.AddScoped<ITokenManagerService, JwtTokenManagerService>();
        services.AddTransient<IHandshakeTokenService, JwtTokenManagerService>();
        services.AddScoped<TokenConfigurationBuilder, TokenConfigurationBuilder>();
        services.AddScoped<TokenEncryptorConfiguration, WebTokenEncryptorConfiguration>();

        services.AddScoped<TokenEncryptor, TokenEncryptor>();

        #endregion

        #region Clases relacionadas a descarga y subida de archivos
        services.AddScoped<IWebFileTransferServiceBuilder<StorageTypeGSFWFTS, BasicFileConfigurationTypeGSFWFTS>, BaseFileTransferServiceBuilder>();
        services.AddScoped<FileSignatureHelper, BaseFileSignatureHelper>();
        #endregion


    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (Configuration.GetValue<bool>("Application:UseDeveloperExceptionPage"))
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            //app.UseExceptionHandler("/Error");
            app.UseWhen(x => !x.Request.Path.Value.Contains("/api") && x.Request.Headers["x-requested-with"] != "XMLHttpRequest", builder =>
            {
                builder.UseExceptionHandler("/Error");
            });

            //app.UseExceptionHandler(new ExceptionHandlerOptions()
            //{
            //    ExceptionHandler = async (ctx) =>
            //    {
            //        if (ctx.Request.Path.Value.StartsWith("/api") || ctx.Request.Headers["x-requested-with"] == "XMLHttpRequest")
            //        {
            //            var feature = ctx.Features.Get<IExceptionHandlerFeature>();
            //            var error = feature?.Error;
            //            // send json
            //            var error1 = new
            //            {
            //                message = error
            //            };
            //            ctx.Response.ContentType = "application/json";
            //            var stream = ctx.Response.Body;
            //            await JsonSerializer.SerializeAsync(stream, error1);

            //        }
            //        else
            //        {
            //            // redirect error page
            //        }
            //    }
            //});

            //The default HSTS value is 30 days.You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        var supportedCultures = new[] { "es", "en" };
        var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);
        app.UseRequestLocalization(localizationOptions);

        app.UseRouting();

        app.UseSerilogRequestLogging(opt => new RequestLoggingOptions() { });

        //app.UseSerilogRequestLogging(opt => opt.EnrichDiagnosticContext = LogHelper.EnrichFromRequest);

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSession();
        app.UseNavigationSecurityMiddlware();
        app.UseJourneyNavigation();

        app.UseSerilogWebContextMiddleware();

        app.UseCors(builder =>
        {
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
            builder.WithOrigins("*");
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapControllers();
            endpoints.MapHub<NotificationHub>("/notificationHub");
        });


    }

    private void AddSerilogConfiguration()
    {
        string AppId = Configuration["Application:AppId"];
        var serilogConfiguration = new LoggerConfiguration()
                .Destructure.UsingAttributes()
                //.MinimumLevel.Verbose()
                //filter events like EF queries. Can disable by
                //.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                // Filter out ASP.NET Core infrastructre logs that are Information and below
                //.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.WithProperty("Application", AppId)
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentUserName()
                .Filter.ByExcluding(Matching.FromSource("Quartz.Core.QuartzScheduler"))
                .Filter.ByExcluding(Matching.FromSource("Quartz.Simpl.RAMJobStore"))
                .Filter.ByExcluding(Matching.FromSource("Quartz.Core.SchedulerSignalerImpl"))
                .Filter.ByExcluding(Matching.FromSource("Quartz.Impl.StdSchedulerFactory"));

        SetLogLevel();

        if (Configuration.GetValue<bool>("Serilog:WriteToFile"))
        {
            string path = Path.Combine(Configuration["Serilog:DefaultLogFolder"], AppId, "Serilog -.txt");
            serilogConfiguration
                .WriteTo.File(path, rollingInterval: RollingInterval.Day);
        }

        if (Configuration.GetValue<bool>("Serilog:WriteToSeq"))
        {
            serilogConfiguration
                .WriteTo.Seq(Configuration["Serilog:SeqUrl"]);
        }

        Log.Logger = serilogConfiguration.CreateLogger();

        //Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
        //Serilog.Debugging.SelfLog.Enable(Console.Error);
        Log.Information("Inicio de la aplicación {App} en {server}. Entorno: {environment}", AppId, System.Environment.MachineName, Environment.EnvironmentName);

        void SetLogLevel()
        {
            ///Default level value
            var logLevelSite = DetermineLogLevelEvent("Serilog:MinimumLevel:Default");

            serilogConfiguration.MinimumLevel.Is(logLevelSite);

            ///Source level value override
            var overrideLogLevelValues = Configuration.GetSection($"Serilog:MinimumLevel:Override")
                .GetChildren()
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (var entryValue in overrideLogLevelValues)
            {
                var key = entryValue.Key;
                var logEventLevel = DetermineLogLevelEvent($"Serilog:MinimumLevel:Override:{key}");
                if (logEventLevel != logLevelSite)
                    serilogConfiguration.MinimumLevel.Override(key, logEventLevel);
            }
        }

        LogEventLevel DetermineLogLevelEvent(string eventName)
        {
            var logLevel = Configuration.GetValue<string>(eventName);

            var parsed = Enum.TryParse(logLevel, out LogEventLevel logLevelValue);//The default value is Trace.
            return parsed ? logLevelValue : LogEventLevel.Verbose;
        }
    }
}

//public static class LogHelper
//{
//    public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
//    {
//        var request = httpContext.Request;

//        // Set all the common properties available for every request

//        diagnosticContext.Set("UserName", httpContext?.User?.Identity?.Name ?? "-");
//        //diagnosticContext.Set("SessionId", httpContext.curre.Session?.Id ?? "-");
//        diagnosticContext.Set("Host", request.Host);
//        diagnosticContext.Set("Protocol", request.Protocol);
//        diagnosticContext.Set("Scheme", request.Scheme);

//        // Only set it if available. You're not sending sensitive data in a querystring right?!
//        //if (request.QueryString.HasValue)
//        //{
//        //    diagnosticContext.Set("QueryString", request.QueryString.Value);
//        //}

//        //// Set the content-type of the Response at this point
//        //diagnosticContext.Set("ContentType", httpContext.Response.ContentType);

//        //// Retrieve the IEndpointFeature selected for the request
//        //var endpoint = httpContext.GetEndpoint();
//        //if (endpoint is object) // endpoint != null
//        //{
//        //    diagnosticContext.Set("EndpointName", endpoint.DisplayName);
//        //}
//    }
//}