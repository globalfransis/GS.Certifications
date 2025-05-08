using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Commands
{
    public class DeleteEmpresaCommand : IRequest<Unit> // Adjust TResponse properly
    {
        public int Id { get; set; } // Adjust Id type properly
        public byte[] RowVersion { get; set; }
        // Add properties
    }

    public class DeleteEmpresaCommandHandler : BaseRequestHandler<Unit, DeleteEmpresaCommand, Unit> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext Context;
        private readonly IEmpresaPortalService EmpresasService;
        // Add services

        public DeleteEmpresaCommandHandler(ICertificationsDbContext context, IEmpresaPortalService empresasService)
        {
            Context = context;
            EmpresasService = empresasService;
            // Inject dependencies
        }

        protected override async Task<Unit> HandleRequestAsync(DeleteEmpresaCommand request, CancellationToken cancellationToken)
        {
            await EmpresasService.DeleteAsync(request.Id);
            await Context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
