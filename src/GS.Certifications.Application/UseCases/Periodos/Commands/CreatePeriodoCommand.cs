using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Periodos.Services;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Security.Services.CurrentCompany;
using MediatR;
using GS.Certifications.Domain.Entities.Impuestos;
using GS.Certifications.Domain.Entities.Periodos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Periodos.Commands
{
    public class CreatePeriodoCommand : IRequest<int>, IPeriodoCreate // Adjust TResponse properly
    {
        public int? Año { get; set; }
        public int? NumeroPeriodo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? EstadoIdm { get; set; }
    }

    public class CreatePeriodoCommandHandler : BaseRequestHandler<int, CreatePeriodoCommand, int> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;
        private readonly IPeriodoService _periodoService;
        private readonly ICurrentCompanyService _companyService;
        // Add services

        public CreatePeriodoCommandHandler(
            ICertificationsDbContext context, IPeriodoService periodoService,
            ICurrentCompanyService companyService)
        {
            _context = context;
            _periodoService = periodoService;
            _companyService = companyService;
            // Inject dependencies
        }

        protected override async Task<int> HandleRequestAsync(CreatePeriodoCommand request, CancellationToken cancellationToken)
        {
            Periodo periodo = await _periodoService.CreateAsync(request);
            periodo.CompanyId = (await _companyService.GetCurrentCompanyAsync()).Id;
            _context.Periodos.Add(periodo);
            await _context.SaveChangesAsync(cancellationToken);
            return periodo.Id;
        }
    }
}
