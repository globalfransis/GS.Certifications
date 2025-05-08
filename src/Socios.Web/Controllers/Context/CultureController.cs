using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Socios.Web.Controllers.Common.Attributes;

namespace Socios.Web.Controllers.Context;

public class CultureController : Controller
{
    [Route("/Culture/ChangeCulture")]
    [AuthorizationGSF]
    public ActionResult ChangeCurrent(string culture)
    {

        Response.Cookies.Append(
             CookieRequestCultureProvider.DefaultCookieName,
             CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
             new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

        string referer = Request.Headers["Referer"].ToString();

        return Redirect(referer);
    }

}
