using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Middlewares;

/// <summary>Extensión to add SerilogWebContextMiddleware in Startup. It must be placed before .UseMvc / .UseRazorPages and after Autentication.
/// </summary>
public static class SerilogWebContextMiddlewareExtension
{
    public static IApplicationBuilder UseSerilogWebContextMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SerilogWebContextMiddleware>();
    }
}

/// <summaryThis Middleware automatically adds usefull properties to serilogs entries
/// </summary>
public class SerilogWebContextMiddleware
{
    private readonly RequestDelegate _next;

    public SerilogWebContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        using (LogContext.PushProperty("IpAddress", context.Connection.RemoteIpAddress))
        using (LogContext.PushProperty("UserName", context.User?.Identity?.Name ?? "-"))
        using (LogContext.PushProperty("SessionId", context.Session?.Id ?? "-"))
        {
            await _next.Invoke(context);
        }
    }
}
