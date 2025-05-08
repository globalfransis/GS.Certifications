using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands
{
    public class RejectComprobanteCommand : IRequest<Unit>, IComprobanteReject
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public string MotivoRechazo { get; set; }
        public string UsuarioRechazo { get; set; }
    }

    public class RejectComprobanteCommandHandler : BaseRequestHandler<Unit, RejectComprobanteCommand, Unit>
    {
        private readonly ICertificationsDbContext Context;
        private readonly IComprobanteService comprobanteService;
        private ICurrentUserService _currentUserService;

        public RejectComprobanteCommandHandler(ICertificationsDbContext context, IComprobanteService comprobanteService, ICurrentUserService currentUserService)
        {
            Context = context;
            this.comprobanteService = comprobanteService;
            _currentUserService = currentUserService;
        }

        protected override async Task<Unit> HandleRequestAsync(RejectComprobanteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await comprobanteService.RejectAsync(request.Id, request);
                await Context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch
            {
                throw;
            }
        }

        protected override RejectComprobanteCommand PreHandleInvoke(RejectComprobanteCommand request)
        {
            request.UsuarioRechazo = _currentUserService.UserLogin;
            return base.PreHandleInvoke(request);
        }
    }
}
