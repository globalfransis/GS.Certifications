using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.OrdenesCompras.Services;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Security.Services.CurrentCompany;
using MediatR;
using GS.Certifications.Domain.Entities.OrdenesCompras;
using GS.Certifications.Domain.Entities.Periodos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.OrdenesCompras.Commands
{
    public class CreateOrdenCompraCommand : IRequest<int>, IOrdenCompraCreate // Adjust TResponse properly
    {
        public string NumeroOrden { get; set; }
        public DateTime Fecha { get; set; }
        public int? EmpresaPortalId { get; set; }
        public int? OrdenCompraTipoId { get; set; }
        //public string CodigoHES { get; set; }
        public string CodigoQAD { get; set; }
        public int? OrdenCompraEstadoIdm { get; set; }
        public decimal Importe { get; set; }
        public long? MonedaId { get; set; }
        public string Observaciones { get; set; }
    }

    public class CreateOrdenCompraCommandHandler : BaseRequestHandler<int, CreateOrdenCompraCommand, int> // Adjust TEntity and TResponse properly
    {
        private readonly ICertificationsDbContext _context;
        private readonly IOrdenCompraService _ordenCompraService;
        private readonly ICurrentCompanyService _companyService;
        // Add services

        public CreateOrdenCompraCommandHandler(
            ICertificationsDbContext context, IOrdenCompraService ordenCompraService,
            ICurrentCompanyService companyService)
        {
            _context = context;
            _ordenCompraService = ordenCompraService;
            _companyService = companyService;
            // Inject dependencies
        }

        protected async override Task<int> HandleRequestAsync(CreateOrdenCompraCommand request, CancellationToken cancellationToken)
        {
            OrdenCompra ordenCompra = await _ordenCompraService.CreateAsync(request);
            ordenCompra.CompanyId = (await _companyService.GetCurrentCompanyAsync()).Id;
            _context.OrdenesCompras.Add(ordenCompra);
            await _context.SaveChangesAsync(cancellationToken);
            return ordenCompra.Id;
        }
    }
}
