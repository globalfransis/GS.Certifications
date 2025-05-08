using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Periodos.Services;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Periodos.Commands
{
    public class DeletePeriodoCommand : IRequest<Unit> // Adjust TResponse properly
    {
        public int Id { get; set; } // Adjust Id type properly
        public byte[] RowVersion { get; set; }
    }

    public class DeletePeriodoCommandHandler : BaseRequestHandler<Unit, DeletePeriodoCommand, Unit> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;
        private readonly IPeriodoService _periodoService;
        // Add services

        public DeletePeriodoCommandHandler(
            ICertificationsDbContext context, IPeriodoService periodoService)
        {
            _context = context;
            _periodoService = periodoService;
            // Inject dependencies
        }

        protected async override Task<Unit> HandleRequestAsync(DeletePeriodoCommand request, CancellationToken cancellationToken)
        {
            await _periodoService.DeleteAsync(request.Id);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
