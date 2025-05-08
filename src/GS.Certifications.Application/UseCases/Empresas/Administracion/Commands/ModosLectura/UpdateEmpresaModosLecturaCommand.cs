using DocumentFormat.OpenXml.InkML;
using GS.Certifications.Application.Commons.Dtos.ModosLecturas;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using GS.Certifications.Domain.Entities;
using GS.Certifications.Domain.Entities.Empresas;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Commands.ModosLectura
{
    public class UpdateEmpresaModosLecturaCommand : IRequest<Unit> // Adjust TResponse properly
    {
        public int Id { get; set; } // Adjust Id type properly
        public byte[] RowVersion { get; set; }
        public int EmpresaPortalId { get; set; }
        public List<ModoLecturaDto> ModosLecturas { get; set; }
    }

    public class UpdateEmpresaModosLecturaCommandHandler : BaseRequestHandler<Unit, UpdateEmpresaModosLecturaCommand, Unit> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;
        private readonly IEmpresaPortalService _empresasService;
        // Add services

        public UpdateEmpresaModosLecturaCommandHandler(ICertificationsDbContext context, IEmpresaPortalService empresasService)
        {
            _context = context;
            _empresasService = empresasService;
        }

        protected async override Task<Unit> HandleRequestAsync(UpdateEmpresaModosLecturaCommand request, CancellationToken cancellationToken)
        {
            await _empresasService.ModificarModosLectura(request.EmpresaPortalId, request.ModosLecturas);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
