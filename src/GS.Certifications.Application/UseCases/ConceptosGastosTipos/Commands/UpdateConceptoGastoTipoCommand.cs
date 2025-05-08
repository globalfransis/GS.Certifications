using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.ConceptosGastosTipos.Services;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using GS.Certifications.Application.UseCases.OrdenesCompras.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.ConceptosGastosTipos.Commands
{
    public class UpdateConceptoGastoTipoCommand : IRequest<Unit>, IConceptoGastoTipoUpdate // Adjust TResponse properly
    {
        public int Id { get; set; } // Adjust Id type properly
        public byte[] RowVersion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ConceptoContableNombre { get; set; }
        public string ConceptoContableValor { get; set; }
    }

    public class UpdateConceptoGastoTipoCommandHandler : BaseRequestHandler<Unit, UpdateConceptoGastoTipoCommand, Unit> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;
        private readonly IConceptoGastoTipoService _conceptoGastoTipoService;
        // Add services

        public UpdateConceptoGastoTipoCommandHandler(
            ICertificationsDbContext context, IConceptoGastoTipoService conceptoGastoTipoService)
        {
            _context = context;
            _conceptoGastoTipoService = conceptoGastoTipoService;
        }

        protected async override Task<Unit> HandleRequestAsync
            (UpdateConceptoGastoTipoCommand request, CancellationToken cancellationToken)
        {
            await _conceptoGastoTipoService.UpdateAsync(request);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
