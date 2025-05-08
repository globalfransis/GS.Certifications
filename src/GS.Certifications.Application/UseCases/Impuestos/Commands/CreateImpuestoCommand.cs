using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Impuestos.Services;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Security.Services.CurrentCompany;
using MediatR;
using GS.Certifications.Domain.Entities.Alicuotas;
using GS.Certifications.Domain.Entities.Impuestos;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Impuestos.Commands
{
    public class CreateImpuestoCommand : IRequest<int>, IImpuestoCreate // Adjust TResponse properly
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public short? TipoId { get; set; }
        public long? ProvinciaId { get; set; }
        public short? AlicuotaId { get; set; }
        public decimal? Valor { get; set; }
    }

    public class CreateImpuestoCommandHandler : BaseRequestHandler<int, CreateImpuestoCommand, int> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;
        private readonly IImpuestoService _impuestosService;
        private readonly ICurrentCompanyService _currentCompanyService;
        // Add services

        public CreateImpuestoCommandHandler(
            ICertificationsDbContext context,
            ICurrentCompanyService currentCompanyService,
            IImpuestoService impuestosService)
        {
            _context = context;
            _currentCompanyService = currentCompanyService;
            _impuestosService = impuestosService;
            // Inject dependencies
        }

        protected async override Task<int> HandleRequestAsync(CreateImpuestoCommand request, CancellationToken cancellationToken)
        {
            Impuesto impuesto = await _impuestosService.CreateAsync(request);
            impuesto.CompanyId = (await _currentCompanyService.GetCurrentCompanyAsync()).Id;
            //impuesto.CompanyId = 39;
            _context.Impuestos.Add(impuesto);
            await _context.SaveChangesAsync(cancellationToken);
            return impuesto.Id;
        }
    }
}
