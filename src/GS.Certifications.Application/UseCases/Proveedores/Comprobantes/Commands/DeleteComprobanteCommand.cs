using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands
{
    public class DeleteComprobanteCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
    }

    public class DeleteComprobanteCommandHandler : BaseRequestHandler<Unit, DeleteComprobanteCommand, Unit>
    {
        private readonly ICertificationsDbContext Context;

        private readonly IComprobanteService comprobanteService;
        public DeleteComprobanteCommandHandler(ICertificationsDbContext context, IComprobanteService comprobanteService)
        {
            Context = context;
            this.comprobanteService = comprobanteService;
        }

        protected override async Task<Unit> HandleRequestAsync(DeleteComprobanteCommand request, CancellationToken cancellationToken)
        {
            await comprobanteService.DeleteAsync(request.Id, request.RowVersion);
            await Context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
