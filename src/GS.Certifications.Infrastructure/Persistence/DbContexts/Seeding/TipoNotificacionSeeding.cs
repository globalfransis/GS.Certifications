using GS.Certifications.Domain.Entities.GSF.Extensions;
using GSF.Domain.Notifications;
using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Certifications.Infrastructure.Persistence.DbContexts.Seeding
{
    public class TipoNotificacionSeeding : BaseFixedEntityConfiguration<TipoNotificacion>
    {
        public const long RecuperacionContraseña = TipoNotificacion.RECUPERACION_CONTRASENIA_IDM;
        public const long ActivacionUsuario = TipoNotificacion.ACTIVACION_USUARIO_IDM;
        public const long ErrorReporte = TipoNotificacion.REPORTE_ERROR_IDM;

        public const long VinculacionUsuarioEmpresaPortal = ErrorReporte + 1;

        protected override void ConfigureEntity(EntityTypeBuilder<TipoNotificacion> builder)
        {

        }

        protected override void LoadSeedingData()
        {
            SeedingData.AddRange(new TipoNotificacion()
            {
                Idm = GSCertificationsTipoNotificacion.VinculacionUsuarioEmpresaPortal,
                Descripcion = "Vinculación de usuario con empresa portal"
            });
        }
    }
}