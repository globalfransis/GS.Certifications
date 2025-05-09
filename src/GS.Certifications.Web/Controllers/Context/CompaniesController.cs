using GSF.Application.Common.Interfaces;
using GSF.Application.Security.Companies.Commands;
using GSF.Application.Security.Grants.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Context;

[AuthorizationGSF]
public class CompanyController : Controller
{

    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    public ICurrentUserService _currentUserService { get; }

    public CompanyController(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    [Route("/Company/ChangeCompany")]
    public async Task<ActionResult> ChangeCurrent(long companyId)
    {
        long UserId = _currentUserService.UserId;

        string referer = Request.Headers["Referer"].ToString();

        var setCurrentCompany = new SetCurrentCompanyCommand() { CompanyId = companyId, UserId = UserId };
        await Mediator.Send(setCurrentCompany);

        //var queryOptions = new GetOptionsTreeQuery() { UserId = UserId };
        //Mediator.Send(queryOptions);

        var queryGrants = new LoadGrantsByUserQuery() { UserId = UserId };
        await Mediator.Send(queryGrants);

        //return Redirect(referer);
        return RedirectToPage("/Index");
    }
}