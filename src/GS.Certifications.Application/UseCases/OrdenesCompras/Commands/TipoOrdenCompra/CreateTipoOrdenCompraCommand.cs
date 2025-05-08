using DocumentFormat.OpenXml.Office2010.ExcelAc;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.OrdenesCompras.Services;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Security.Services.CurrentCompany;
using MediatR;
using GS.Certifications.Domain.Entities.OrdenesCompras;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.OrdenesCompras.Commands.TipoOrdenCompra
{
    public class CreateTipoOrdenCompraCommand : IRequest<int>, IOrdenCompraTipoCreate // Adjust TResponse properly
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool EsRequerida { get; set; }
        public bool EsAbierta { get; set; }
        public bool EsRecurrente { get; set; }
        public bool EsUnica { get; set; }
        public List<long> GruposId { get; set; }
    }

    public class CreateTipoOrdenCompraCommandHandler : BaseRequestHandler<int, CreateTipoOrdenCompraCommand, int> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;
        private readonly IOrdenCompraService _ordenCompraService;
        private readonly ICurrentCompanyService _companyService;
        // Add services

        public CreateTipoOrdenCompraCommandHandler(
            ICertificationsDbContext context, IOrdenCompraService ordenCompraService,
            ICurrentCompanyService companyService)
        {
            _context = context;
            _ordenCompraService = ordenCompraService;
            _companyService = companyService;
            // Inject dependencies
        }

        protected async override Task<int> HandleRequestAsync(CreateTipoOrdenCompraCommand request, CancellationToken cancellationToken)
        {
            OrdenCompraTipo ordenCompraTipo = await _ordenCompraService.CreateTipoAsync(request);
            ordenCompraTipo.CompanyId = (await _companyService.GetCurrentCompanyAsync()).Id;
            //ordenCompraTipo.CompanyId = 39;
            _context.OrdenesComprasTipos.Add(ordenCompraTipo);
            await _context.SaveChangesAsync(cancellationToken);
            return ordenCompraTipo.Id;
        }
    }
}
