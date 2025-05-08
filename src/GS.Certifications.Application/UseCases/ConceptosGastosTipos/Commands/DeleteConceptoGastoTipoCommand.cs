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
    public class DeleteConceptoGastoTipoCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
    }

    public class DeleteConceptoGastoTipoCommandHandler : BaseRequestHandler
        <Unit, DeleteConceptoGastoTipoCommand, Unit>
    {
        private readonly ICertificationsDbContext _context;
        private readonly IConceptoGastoTipoService _conceptoGastoTipoService;

        public DeleteConceptoGastoTipoCommandHandler(
            ICertificationsDbContext context, IConceptoGastoTipoService conceptoGastoTipoService)
        {
            _context = context;
            _conceptoGastoTipoService = conceptoGastoTipoService;
        }

        protected async override Task<Unit> HandleRequestAsync
            (DeleteConceptoGastoTipoCommand request, CancellationToken cancellationToken)
        {
            await _conceptoGastoTipoService.DeleteAsync(request.Id);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
