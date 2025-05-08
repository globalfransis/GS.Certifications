using Duende.IdentityServer.Services;
using GS.Certifications.Application.CQRS.DbContexts;
using GSF.Application.Common.Interfaces;
using GSF.Application.Common.Models;
using GSF.Application.Notifications;
using GSF.Application.Notifications.Services;
using GSF.Application.Security.Token;
using GSF.Domain.Entities.Security;
using GSF.Domain.Entities.Security.Events;
using GSF.Domain.Notifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using GS.Certifications.Domain.Entities.GSF.Extensions;
using GS.Certifications.Domain.Entities.Seguridad.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.EventsHandler
{
    public class VinculacionUsuarioNuevoEmpresaEventHandler :
        INotificationHandler<DomainEventNotification<VinculacionUsuarioNuevoEmpresaEvent>>
    {
        public readonly ITokenManagerService _tokenManagerService;
        public readonly ICertificationsDbContext _context;
        public readonly INotificacionService _notificacionService;
        public readonly ITargetPathResolver _targetPathResolver;
        private readonly IConfiguration _configuration;

        public VinculacionUsuarioNuevoEmpresaEventHandler
            (ICertificationsDbContext dbContext,
            ITokenManagerService tokenManagerService,
            INotificacionService notificacionService,
            ITargetPathResolver targetPathResolver,
            IConfiguration configuration)
        {
            _context = dbContext;
            _tokenManagerService = tokenManagerService;
            _notificacionService = notificacionService;
            _targetPathResolver = targetPathResolver;
            _configuration = configuration;
        }

        public async Task Handle(DomainEventNotification<VinculacionUsuarioNuevoEmpresaEvent> notification, CancellationToken cancellationToken)
        {
            var tokenKey = notification.DomainEvent.User.SetActivationToken();
            var token = _tokenManagerService.GenerateTokenForUserActivation
                (notification.DomainEvent.User.Id.ToString(), notification.DomainEvent.DomainFIdm.ToString(), tokenKey);

            User user = notification.DomainEvent.User;
            var domainFIdm = notification.DomainEvent.DomainFIdm;

            string recoveryPath = _targetPathResolver.GetResolvedUri(string.Empty);
            string recoveryHost = _targetPathResolver.GetHost();
            string scheme = _targetPathResolver.GetScheme();

            string applicationName = _configuration.GetSection("Application:Name").Value;

            var configuracionNotificacion = await _context.ConfiguracionNotificaciones.FirstOrDefaultAsync(c => c.TipoNotificacionIdm == TipoNotificacion.ACTIVACION_USUARIO_IDM && c.DomainFIdm == domainFIdm, cancellationToken: cancellationToken);

            var notificationActivacion = new NotificacionDto()
            {
                ConfiguracionNotificacionId = configuracionNotificacion.Id,
                Destinatarios = user.Email,
                EstadoIdm = EstadoNotificacion.NuevoIdm,
                FechaEncolado = DateTime.Now,
                Etiquetas = new()
            };

            notificationActivacion.Etiquetas.Add(new NotificacionEtiquetaDto()
            {
                Etiqueta = "root",
                Valor = $"{scheme}://{recoveryHost}{recoveryPath}",
                NotificacionEtiquetaTipoIdm = NotificacionEtiquetaTipo.CUERPO_TIPO_IDM
            });

            notificationActivacion.Etiquetas.Add(new NotificacionEtiquetaDto()
            {
                Etiqueta = "token",
                Valor = token,
                NotificacionEtiquetaTipoIdm = NotificacionEtiquetaTipo.CUERPO_TIPO_IDM
            });

            notificationActivacion.Etiquetas.Add(new NotificacionEtiquetaDto()
            {
                Etiqueta = "seHaSolicitadoActivacionDeCuenta",
                Valor = "Se ha solicitado la activación de la cuenta",
                NotificacionEtiquetaTipoIdm = NotificacionEtiquetaTipo.CUERPO_TIPO_IDM
            });

            notificationActivacion.Etiquetas.Add(new NotificacionEtiquetaDto()
            {
                Etiqueta = "activarMiCuenta",
                Valor = "Activar mi cuenta",
                NotificacionEtiquetaTipoIdm = NotificacionEtiquetaTipo.CUERPO_TIPO_IDM
            });

            notificationActivacion.Etiquetas.Add(new NotificacionEtiquetaDto()
            {
                Etiqueta = "recuperarMiContraseña",
                Valor = "Recuperar mi contraseña",
                NotificacionEtiquetaTipoIdm = NotificacionEtiquetaTipo.CUERPO_TIPO_IDM
            });

            notificationActivacion.Etiquetas.Add(new NotificacionEtiquetaDto()
            {
                Etiqueta = "application",
                Valor = applicationName,
                NotificacionEtiquetaTipoIdm = NotificacionEtiquetaTipo.CUERPO_TIPO_IDM
            });

            Notificacion notificacionActivacionObtenida = _notificacionService.CreateNotificacion(notificationActivacion);

            _context.Notificaciones.Add(notificacionActivacionObtenida);

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

            NotificacionDto notificacionVinculacion = new()
            {
                ConfiguracionNotificacionId = configuracionNotificacionId,
                Destinatarios = notification.DomainEvent.User.Email,
                Etiquetas = etiquetas
            };

            Notificacion notificacionVinculacionObtenida = _notificacionService.CreateNotificacion(notificacionVinculacion);

            _context.Notificaciones.Add(notificacionVinculacionObtenida);

            await Task.FromResult(Unit.Value);
        }
    }
}
