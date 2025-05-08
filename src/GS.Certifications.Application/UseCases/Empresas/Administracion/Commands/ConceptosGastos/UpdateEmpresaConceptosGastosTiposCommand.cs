using GS.Certifications.Application.Commons.Dtos.ConceptosGastosTipos;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using GS.Certifications.Application.Commons.Dtos.OrdenesCompra;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Commands.OrdenesCompras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Commands.ConceptosGastos
{
    public class UpdateEmpresaConceptosGastosTiposCommand : IRequest<Unit> // Adjust TResponse properly
    {
        public int Id { get; set; } // Adjust Id type properly
        public byte[] RowVersion { get; set; }
        public int EmpresaPortalId { get; set; }
        public List<ConceptoGastoTipoDto> ConceptosGastosTipos { get; set; }
    }

    public class UpdateEmpresaConceptosGastosTiposCommandHandler : BaseRequestHandler<Unit, UpdateEmpresaConceptosGastosTiposCommand, Unit> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;
        private readonly IEmpresaPortalService _empresasService;
        // Add services

        public UpdateEmpresaConceptosGastosTiposCommandHandler(ICertificationsDbContext context, IEmpresaPortalService empresasService)
        {
            _context = context;
            _empresasService = empresasService;
        }

        protected async override Task<Unit> HandleRequestAsync(UpdateEmpresaConceptosGastosTiposCommand request, CancellationToken cancellationToken)
        {
            await _empresasService.ModificarConceptosGastosTipos(request.EmpresaPortalId, request.ConceptosGastosTipos);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
