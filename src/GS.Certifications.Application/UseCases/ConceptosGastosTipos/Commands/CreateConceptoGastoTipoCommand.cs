using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.ConceptosGastosTipos.Services;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Security.Services.CurrentCompany;
using MediatR;
using GS.Certifications.Application.UseCases.OrdenesCompras.Services;
using GS.Certifications.Domain.Entities.ConceptosGastosTipos;
using GS.Certifications.Domain.Entities.OrdenesCompras;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.ConceptosGastosTipos.Commands
{
    public class CreateConceptoGastoTipoCommand : IRequest<int>, IConceptoGastoTipoCreate // Adjust TResponse properly
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ConceptoContableNombre { get; set; }
        public string ConceptoContableValor { get; set; }
    }

    public class CreateConceptoGastoTipoCommandHandler : BaseRequestHandler
        <int, CreateConceptoGastoTipoCommand, int>
    {
        private readonly ICertificationsDbContext _context;
        private readonly IConceptoGastoTipoService _conceptoGastoTipoService;
        private readonly ICurrentCompanyService _companyService;
        // Add services

        public CreateConceptoGastoTipoCommandHandler(
            ICertificationsDbContext context, IConceptoGastoTipoService conceptoGastoTipoService,
            ICurrentCompanyService companyService)
        {
            _context = context;
            _conceptoGastoTipoService = conceptoGastoTipoService;
            _companyService = companyService;
            // Inject dependencies
        }

        protected async override Task<int> HandleRequestAsync
            (CreateConceptoGastoTipoCommand request, CancellationToken cancellationToken)
        {
            ConceptoGastoTipo conceptoGastoTipo = await _conceptoGastoTipoService.CreateAsync(request);
            conceptoGastoTipo.CompanyId = (await _companyService.GetCurrentCompanyAsync()).Id;
            //conceptoGastoTipo.CompanyId = 39;
            _context.ConceptosGastosTipos.Add(conceptoGastoTipo);
            await _context.SaveChangesAsync(cancellationToken);
            return conceptoGastoTipo.Id;
        }
    }
}
