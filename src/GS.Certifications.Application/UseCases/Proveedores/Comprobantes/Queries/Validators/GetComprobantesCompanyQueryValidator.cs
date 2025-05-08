using GSF.Application.Common.Validators;
using GSFSharedResources;
using Microsoft.Extensions.Localization;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Queries.Validators;

public class GetComprobantesCompanyQueryValidator : AbstractGSValidator<GetComprobantesCompanyQuery>
{
    public GetComprobantesCompanyQueryValidator(IStringLocalizer<Shared> loc)
    {
        //RuleFor(q => q.EmpresaId)
        //    .NotEmpty()
        //    .WithMessage(loc["Se debe especificar un Proveedor."])
        //    .WithName("Proveedor");
    }
}