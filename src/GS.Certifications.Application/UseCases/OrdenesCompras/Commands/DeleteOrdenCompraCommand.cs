using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.OrdenesCompras.Services;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using GS.Certifications.Application.UseCases.Periodos.Services;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.OrdenesCompras.Commands
{
    public class DeleteOrdenCompraCommand : IRequest<Unit> // Adjust TResponse properly
    {
        public int Id { get; set; } // Adjust Id type properly
        public byte[] RowVersion { get; set; }
        // Add properties
    }

    public class DeleteOrdenCompraCommandHandler : BaseRequestHandler<Unit, DeleteOrdenCompraCommand, Unit> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;
        private readonly IOrdenCompraService _ordenCompraService;
        // Add services

        public DeleteOrdenCompraCommandHandler(
            ICertificationsDbContext context, IOrdenCompraService ordenCompraService)
        {
            _context = context;
            _ordenCompraService = ordenCompraService;
            // Inject dependencies
        }

        protected async override Task<Unit> HandleRequestAsync(DeleteOrdenCompraCommand request, CancellationToken cancellationToken)
        {
            await _ordenCompraService.DeleteAsync(request.Id);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
