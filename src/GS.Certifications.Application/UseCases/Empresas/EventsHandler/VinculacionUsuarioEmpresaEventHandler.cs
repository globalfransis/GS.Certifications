using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using Duende.IdentityServer.Services;
using GS.Certifications.Application.CQRS.DbContexts;
using GSF.Application.Common.Models;
using GSF.Application.Notifications;
using GSF.Application.Notifications.Services;
using GSF.Application.Security.Token;
using GSF.Domain.Entities.Security.Events;
using GSF.Domain.Notifications;
using MediatR;
using GS.Certifications.Domain.Commons.Constants;
using GS.Certifications.Domain.Entities.GSF.Extensions;
using GS.Certifications.Domain.Entities.Seguridad.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.EventsHandler
{
    public class VinculacionUsuarioEmpresaEventHandler :
        INotificationHandler<DomainEventNotification<VinculacionUsuarioEmpresaEvent>>
    {
        public readonly ICertificationsDbContext _context;
        public readonly INotificacionService _notificacionService;
        public readonly ITokenManagerService _tokenService;

        public VinculacionUsuarioEmpresaEventHandler
            (ICertificationsDbContext dbContext,
            INotificacionService notificacionService,
            ITokenManagerService tokenService)
        {
            _context = dbContext;
            _notificacionService = notificacionService;
            _tokenService = tokenService;
        }

        public async Task Handle(DomainEventNotification<VinculacionUsuarioEmpresaEvent> notification, CancellationToken cancellationToken)
        {
            HashSet<NotificacionEtiquetaDto> etiquetas = new();

            NotificacionEtiquetaDto etiquetaCuerpo = new()
            {
                Etiqueta = "empresaPortalBody",
                Valor = notification.DomainEvent.EmpresaPortal.RazonSocial,
                NotificacionEtiquetaTipoIdm = NotificacionEtiquetaTipo.CUERPO_TIPO_IDM
            };

            etiquetas.Add(etiquetaCuerpo);

            NotificacionEtiquetaDto etiquetaAsunto = new()
            {
                Etiqueta = "empresaPortalSubject",
                Valor = notification.DomainEvent.EmpresaPortal.RazonSocial,
                NotificacionEtiquetaTipoIdm = NotificacionEtiquetaTipo.ASUNTO_TIPO_IDM
            };
            etiquetas.Add(etiquetaAsunto);

            long configuracionNotificacionId = _context.ConfiguracionNotificaciones
                .FirstOrDefault(src => src.DomainFIdm == notification.DomainEvent.DomainFIdm
                && src.TipoNotificacionIdm == GSCertificationsTipoNotificacion.VinculacionUsuarioEmpresaPortal).Id;

            NotificacionDto notificacion = new()
            {
                ConfiguracionNotificacionId = configuracionNotificacionId,
                Destinatarios = notification.DomainEvent.User.Email,
                Etiquetas = etiquetas
            };

            Notificacion notificacionObtenida = _notificacionService.CreateNotificacion(notificacion);

            _context.Notificaciones.Add(notificacionObtenida);

            await Task.FromResult(Unit.Value);
        }
    }
}
