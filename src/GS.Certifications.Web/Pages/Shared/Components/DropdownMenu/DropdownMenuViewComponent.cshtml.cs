using GSF.Application.Security.Modulos;
using GSF.Application.Security.Modulos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class DropdownMenuViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
    {
        var queryMenu = new GetMenuQuery { };
        IMediator _mediator = (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));

        ModuloDto menu = await _mediator.Send(queryMenu);

        return View("MenuViewComponent", menu);
    }
}
