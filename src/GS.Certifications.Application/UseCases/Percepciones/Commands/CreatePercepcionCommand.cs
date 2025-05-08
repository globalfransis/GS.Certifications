using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Percepciones.Services;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Security.Services.CurrentCompany;
using MediatR;
using GS.Certifications.Application.UseCases.Impuestos.Services;
using GS.Certifications.Domain.Entities.Impuestos;
using GS.Certifications.Domain.Entities.Percepciones;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Percepciones.Commands
{
    public class CreatePercepcionCommand : IRequest<int>, IPercepcionCreate // Adjust TResponse properly
    {
        public string Descripcion { get; set; }
        public short? PercepcionTipoId { get; set; }
        public long? ProvinciaId { get; set; }
    }

    public class CreatePercepcionCommandHandler : BaseRequestHandler<int, CreatePercepcionCommand, int> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;
        private readonly IPercepcionService _percepcionesService;
        private readonly ICurrentCompanyService _currentCompanyService;
        // Add services

        public CreatePercepcionCommandHandler(
            ICertificationsDbContext context,
            ICurrentCompanyService currentCompanyService,
            IPercepcionService percepcionesService)
        {
            _context = context;
            _currentCompanyService = currentCompanyService;
            _percepcionesService = percepcionesService;
            // Inject dependencies
        }

        protected async override Task<int> HandleRequestAsync
            (CreatePercepcionCommand request, CancellationToken cancellationToken)
        {
            Percepcion percepcion = await _percepcionesService.CreateAsync(request);
            percepcion.CompanyId = (await _currentCompanyService.GetCurrentCompanyAsync()).Id;
            //impuesto.CompanyId = 39;
            _context.Percepciones.Add(percepcion);
            await _context.SaveChangesAsync(cancellationToken);
            return percepcion.Id;
        }
    }
}
