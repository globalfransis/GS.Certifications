using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.OrdenesCompras.Services;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.OrdenesCompras.Commands.TipoOrdenCompra
{
    public class DeleteTipoOrdenCompraCommand : IRequest<Unit> // Adjust TResponse properly
    {
        public int Id { get; set; } // Adjust Id type properly
        public byte[] RowVersion { get; set; }
        // Add properties
    }

    public class DeleteTipoOrdenCompraCommandHandler : BaseRequestHandler<Unit, DeleteTipoOrdenCompraCommand, Unit> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;
        private readonly IOrdenCompraService _ordenCompraService;
        // Add services

        public DeleteTipoOrdenCompraCommandHandler(
            ICertificationsDbContext context, IOrdenCompraService ordenCompraService)
        {
            _context = context;
            _ordenCompraService = ordenCompraService;
            // Inject dependencies
        }

        protected async override Task<Unit> HandleRequestAsync(DeleteTipoOrdenCompraCommand request, CancellationToken cancellationToken)
        {
            await _ordenCompraService.DeleteTipoAsync(request.Id);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
