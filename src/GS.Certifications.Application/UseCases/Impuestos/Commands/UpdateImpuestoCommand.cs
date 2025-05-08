using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Impuestos.Services;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Impuestos.Commands
{
    public class UpdateImpuestoCommand : IRequest<Unit>, IImpuestoUpdate // Adjust TResponse properly
    {
        public int Id { get; set; } // Adjust Id type properly
        public byte[] RowVersion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public short? TipoId { get; set; }
        public long? ProvinciaId { get; set; }
        public short? AlicuotaId { get; set; }
        public decimal? Valor { get; set; }
    }

    public class UpdateImpuestoCommandHandler : BaseRequestHandler<Unit, UpdateImpuestoCommand, Unit> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;
        private readonly IImpuestoService _impuestoService;
        // Add services

        public UpdateImpuestoCommandHandler(
            ICertificationsDbContext context,
            IImpuestoService impuestoService)
        {
            _context = context;
            _impuestoService = impuestoService;
        }

        protected async override Task<Unit> HandleRequestAsync(UpdateImpuestoCommand request, CancellationToken cancellationToken)
        {
            await _impuestoService.UpdateAsync(request);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
