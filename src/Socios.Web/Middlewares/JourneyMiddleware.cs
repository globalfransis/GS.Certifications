using Serilog;
using System.Diagnostics;

namespace Socios.Web.Middlewares;

/// <summary>Collects data to support journey navigation
/// http://rmglobal.globalsis.com.ar:8085/redmine/projects/nuevo-fw/wiki/Navegación_en_GSF
/// </summary>
[DebuggerStepThrough]
public class JourneyMiddleware
{
    private readonly RequestDelegate _next;

    public JourneyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        string navigationKey = null;
        navigationKey = httpContext.Request.Path.ToString().ToLower();

        if (httpContext.Items.ContainsKey("navigationKey"))
        {
            Log.Warning("ya existe navigationKey: {navigationKey} en httpContext, al tratar de ingresar una nueva key: {1}", httpContext.Items["navigationKey"], navigationKey);
        }
        else
        {
            Log.Debug("Agregando NavigationKey: {navigationKey}:", navigationKey);
            httpContext.Items.Add("navigationKey", navigationKey);
        }

        await _next.Invoke(httpContext);


    }
}
public static class JourneyMiddlewareExtension
{
    public static IApplicationBuilder UseJourneyNavigation(
       this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JourneyMiddleware>();
    }
}
