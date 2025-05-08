using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Impuestos.Services;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Impuestos.Commands
{
    public class DeleteImpuestoCommand : IRequest<Unit> // Adjust TResponse properly
    {
        public int Id { get; set; } // Adjust Id type properly
        public byte[] RowVersion { get; set; }
        // Add properties
    }

    public class DeleteImpuestoCommandHandler : BaseRequestHandler<Unit, DeleteImpuestoCommand, Unit> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;
        private readonly IImpuestoService _impuestoService;
        // Add services

        public DeleteImpuestoCommandHandler(
            ICertificationsDbContext context,
            IImpuestoService impuestoService)
        {
            _context = context;
            _impuestoService = impuestoService;
            // Inject dependencies
        }

        protected async override Task<Unit> HandleRequestAsync(DeleteImpuestoCommand request, CancellationToken cancellationToken)
        {
            await _impuestoService.DeleteAsync(request.Id);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
