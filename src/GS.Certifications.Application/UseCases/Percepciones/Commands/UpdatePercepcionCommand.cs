using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Percepciones.Services;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Security.Services.CurrentCompany;
using MediatR;
using GS.Certifications.Application.UseCases.Impuestos.Services;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Percepciones.Commands
{
    public class UpdatePercepcionCommand : IRequest<Unit>, IPercepcionUpdate // Adjust TResponse properly
    {
        public int Id { get; set; } // Adjust Id type properly
        public byte[] RowVersion { get; set; }
        public string Descripcion { get; set; }
        public short? PercepcionTipoId { get; set; }
        public long? ProvinciaId { get; set; }
    }

    public class UpdatePercepcionCommandHandler : BaseRequestHandler
        <Unit, UpdatePercepcionCommand, Unit> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;
        private readonly IPercepcionService _percepcionesService;
        private readonly ICurrentCompanyService _currentCompanyService;
        // Add services

        public UpdatePercepcionCommandHandler(
            ICertificationsDbContext context,
            ICurrentCompanyService currentCompanyService,
            IPercepcionService percepcionesService)
        {
            _context = context;
            _currentCompanyService = currentCompanyService;
            _percepcionesService = percepcionesService;
            // Inject dependencies
        }

        protected async override Task<Unit> HandleRequestAsync
            (UpdatePercepcionCommand request, CancellationToken cancellationToken)
        {
            await _percepcionesService.UpdateAsync(request);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
