using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.OrdenesCompras.Services;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using GS.Certifications.Application.UseCases.Periodos.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.OrdenesCompras.Commands
{
    public class UpdateOrdenCompraCommand : IRequest<Unit>, IOrdenCompraUpdate // Adjust TResponse properly
    {
        public int Id { get; set; } // Adjust Id type properly
        public byte[] RowVersion { get; set; }
        public string NumeroOrden { get; set; }
        public DateTime Fecha { get; set; }
        public int? EmpresaPortalId { get; set; }
        public int? OrdenCompraTipoId { get; set; }
        //public string CodigoHES { get; set; }
        public string CodigoQAD { get; set; }
        public int? OrdenCompraEstadoIdm { get; set; }
        public decimal Importe { get; set; }
        public long? MonedaId { get; set; }
        public string Observaciones { get; set; }
    }

    public class UpdateOrdenCompraCommandHandler : BaseRequestHandler<Unit, UpdateOrdenCompraCommand, Unit> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;
        private readonly IOrdenCompraService _ordenCompraService;
        // Add services

        public UpdateOrdenCompraCommandHandler(
            ICertificationsDbContext context, IOrdenCompraService ordenCompraService)
        {
            _context = context;
            _ordenCompraService = ordenCompraService;
            // Inject dependencies
        }

        protected async override Task<Unit> HandleRequestAsync(UpdateOrdenCompraCommand request, CancellationToken cancellationToken)
        {
            await _ordenCompraService.UpdateAsync(request);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
