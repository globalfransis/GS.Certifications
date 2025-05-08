using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.OrdenesCompras.Services;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.OrdenesCompras.Commands.TipoOrdenCompra
{
    public class UpdateTipoOrdenCompraCommand : IRequest<Unit>, IOrdenCompraTipoUpdate // Adjust TResponse properly
    {
        public int Id { get; set; } // Adjust Id type properly
        public byte[] RowVersion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool EsRequerida { get; set; }
        public bool EsAbierta { get; set; }
        public bool EsRecurrente { get; set; }
        public bool EsUnica { get; set; }
        public List<long> GruposId { get; set; }
    }

    public class UpdateTipoOrdenCompraCommandHandler : BaseRequestHandler<Unit, UpdateTipoOrdenCompraCommand, Unit> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;
        private readonly IOrdenCompraService _ordenCompraService;
        // Add services

        public UpdateTipoOrdenCompraCommandHandler(
            ICertificationsDbContext context, IOrdenCompraService ordenCompraService)
        {
            _context = context;
            _ordenCompraService = ordenCompraService;
            // Inject dependencies
        }

        protected async override Task<Unit> HandleRequestAsync(UpdateTipoOrdenCompraCommand request, CancellationToken cancellationToken)
        {
            await _ordenCompraService.UpdateTipoAsync(request);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
