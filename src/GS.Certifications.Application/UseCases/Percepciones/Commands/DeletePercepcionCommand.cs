using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Percepciones.Services;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using GS.Certifications.Application.UseCases.Impuestos.Services;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Percepciones.Commands
{
    public class DeletePercepcionCommand : IRequest<Unit> // Adjust TResponse properly
    {
        public int Id { get; set; } // Adjust Id type properly
        public byte[] RowVersion { get; set; }
        // Add properties
    }

    public class DeletePercepcionCommandHandler : BaseRequestHandler<Unit, DeletePercepcionCommand, Unit> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;
        private readonly IPercepcionService _percepcionService;
        // Add services

        public DeletePercepcionCommandHandler(
            ICertificationsDbContext context,
            IPercepcionService percepcionService)
        {
            _context = context;
            _percepcionService = percepcionService;
            // Inject dependencies
        }

        protected async override Task<Unit> HandleRequestAsync(DeletePercepcionCommand request, CancellationToken cancellationToken)
        {
            await _percepcionService.DeleteAsync(request.Id);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
