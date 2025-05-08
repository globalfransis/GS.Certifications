using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Socios.Web.Common;

namespace Socios.Web.Controllers.Common.Attributes;

/// <summary>
/// Attribute used to limit the Request made to the controller or action to those
/// that are made from user session that its alive.
/// </summary>
public class AuthorizationGSFAttribute : ActionFilterAttribute, IAuthorizationFilter/*, IExceptionFilter*/
{
    //public override void OnActionExecuting(ActionExecutingContext filterContext)
    //{
    //    var sessionIsAlive = filterContext.HttpContext.Session != null && filterContext.HttpContext.Session.Keys.Contains(Constants.SessionAliveFlagKey);

    //    if (!sessionIsAlive)
    //        filterContext.Result = new UnauthorizedObjectResult(null);
    //}

    private IStringLocalizer<Shared> _loc;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        _loc = context.HttpContext.RequestServices.GetService<IStringLocalizer<Shared>>();
        var sessionIsAlive = context.HttpContext.Session != null && context.HttpContext.Session.Keys.Contains(Constants.SessionAliveFlagKey);

        if (!sessionIsAlive)
            context.Result = new UnauthorizedObjectResult(new { GsfSessionExpiredMessage = "Session Expired" });
        //context.Result = new UnauthorizedObjectResult(new { GsfSessionExpiredMessage = _loc["Sesión expirada."] });
        //context.Result = new UnauthorizedObjectResult(new GSFSessionExpiredException(_loc["Sesión expirada."]));
        //context.Result = new UnauthorizedObjectResult(null);

        //if (!sessionIsAlive)
        //    throw new GSFSessionExpiredException(_loc["Sesión expirada."]);
    }

    //public void OnException(ExceptionContext context)
    //{
    //    context.Result = new UnauthorizedObjectResult(context.Exception);
    //}
}