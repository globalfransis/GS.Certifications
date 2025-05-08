using GSF.Domain.Notifications;

namespace GS.Certifications.Domain.Entities.GSF.Extensions
{
    public class GSCertificationsConfiguracionNotificacion
    {
        public const long WPR_UserActivationId = ConfiguracionNotificacion.REPORTE_ERROR_IDM + 1;
        public const long WBO_VinculacionEmpresaPortalId = WPR_UserActivationId + 1;
        public const long WPR_VinculacionEmpresaPortalId = WBO_VinculacionEmpresaPortalId + 1;
        public const long WPR_PasswordRecoveryId = WPR_VinculacionEmpresaPortalId + 1;
    }
}
