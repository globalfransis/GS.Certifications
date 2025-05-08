using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Periodos.Services;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Periodos.Commands
{
    public class UpdatePeriodoCommand : IRequest<Unit>, IPeriodoUpdate // Adjust TResponse properly
    {
        public int Id { get; set; } // Adjust Id type properly
        public byte[] RowVersion { get; set; }
        public int? Año { get; set; }
        public int? NumeroPeriodo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? EstadoIdm { get; set; }
    }

    public class UpdatePeriodoCommandHandler : BaseRequestHandler<Unit, UpdatePeriodoCommand, Unit> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;
        private readonly IPeriodoService _periodoService;
        // Add services

        public UpdatePeriodoCommandHandler(
            ICertificationsDbContext context, IPeriodoService periodoService)
        {
            _context = context;
            _periodoService = periodoService;
            // Inject dependencies
        }

        protected async override Task<Unit> HandleRequestAsync(UpdatePeriodoCommand request, CancellationToken cancellationToken)
        {
            await _periodoService.UpdateAsync(request);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
