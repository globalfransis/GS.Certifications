using GSF.Domain.Notifications;
using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Commons.Constants;
using GS.Certifications.Domain.Entities.GSF.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GS.Certifications.Infrastructure.Persistence.DbContexts.Seeding
{
    public class ConfiguracionNotificacionSeeding : BaseWithIdEntityConfiguration<ConfiguracionNotificacion>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<ConfiguracionNotificacion> builder)
        {
        }
        protected override void LoadSeedingData()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyResourcesNames = assembly.GetManifestResourceNames();

            var WPR_ActivacionUsuario = GetResourceContentFromAssembly(assembly, assemblyResourcesNames, "UserActivationTemplate_WPR");
            var WBO_VinculacionEmpresaTemplate = GetResourceContentFromAssembly(assembly, assemblyResourcesNames, "WBO_VinculacionUsuario");
            var WPR_VinculacionEmpresaTemplate = GetResourceContentFromAssembly(assembly, assemblyResourcesNames, "WPR_VinculacionUsuario");
            var PasswordRecoveryTemplate_WPR = GetResourceContentFromAssembly(assembly, assemblyResourcesNames, "PasswordRecoveryTemplate_WPR");

            SeedingData.AddRange(
                new ConfiguracionNotificacion
                {
                    Id = GSCertificationsConfiguracionNotificacion.WPR_UserActivationId,
                    Body = WPR_ActivacionUsuario,
                    Subject = "Web Proveedores | Activación de usuario",
                    UltimoEnvio = default,
                    Prioridad = 1,
                    HoraInicio = TimeSpan.Parse("00:00:00"),
                    HoraFin = TimeSpan.Parse("00:00:00"),
                    Activo = true,
                    FrecuenciaHora = 0,
                    FrecuenciaMinutos = 0,
                    FrecuenciaSegundos = 45,
                    DomainFIdm = DomainFIdmConstants.Socios,
                    Descripcion = "Activación de usuario",
                    TipoNotificacionIdm = TipoNotificacion.ACTIVACION_USUARIO_IDM,
                },
                new ConfiguracionNotificacion
                {
                    Id = GSCertificationsConfiguracionNotificacion.WPR_VinculacionEmpresaPortalId,
                    Body = WPR_VinculacionEmpresaTemplate,
                    Subject = "Web Proveedores | Se vinculó tu usuario con la empresa {empresaPortalSubject}", //subject
                    UltimoEnvio = default,
                    Prioridad = 2,
                    HoraInicio = TimeSpan.Parse("00:00:00"),
                    HoraFin = TimeSpan.Parse("00:00:00"),
                    Activo = true,
                    FrecuenciaHora = 0,
                    FrecuenciaMinutos = 0,
                    FrecuenciaSegundos = 45,
                    DomainFIdm = DomainFIdmConstants.Socios,
                    Descripcion = "Vinculación de usuario",
                    TipoNotificacionIdm = GSCertificationsTipoNotificacion.VinculacionUsuarioEmpresaPortal,
                },
                new ConfiguracionNotificacion
                {
                    Id = GSCertificationsConfiguracionNotificacion.WBO_VinculacionEmpresaPortalId,
                    Body = WBO_VinculacionEmpresaTemplate,
                    Subject = "Web Empresas | Se vinculó tu usuario con la empresa {empresaPortalSubject}", //subject
                    UltimoEnvio = default,
                    Prioridad = 1,
                    HoraInicio = TimeSpan.Parse("00:00:00"),
                    HoraFin = TimeSpan.Parse("00:00:00"),
                    Activo = true,
                    FrecuenciaHora = 0,
                    FrecuenciaMinutos = 0,
                    FrecuenciaSegundos = 45,
                    DomainFIdm = DomainFIdmConstants.Backoffice,
                    Descripcion = "Vinculación de usuario",
                    TipoNotificacionIdm = GSCertificationsTipoNotificacion.VinculacionUsuarioEmpresaPortal,
                },
                new ConfiguracionNotificacion
                {
                    Id = GSCertificationsConfiguracionNotificacion.WPR_PasswordRecoveryId,
                    Body = PasswordRecoveryTemplate_WPR,
                    Subject = "Web Proveedores | Recuperación de Contraseña", //subject
                    UltimoEnvio = default,
                    Prioridad = 1,
                    HoraInicio = TimeSpan.Parse("00:00:00"),
                    HoraFin = TimeSpan.Parse("00:00:00"),
                    Activo = true,
                    FrecuenciaHora = 0,
                    FrecuenciaMinutos = 0,
                    FrecuenciaSegundos = 45,
                    DomainFIdm = DomainFIdmConstants.Socios,
                    Descripcion = "Recuperación de Contraseña",
                    TipoNotificacionIdm = TipoNotificacion.RECUPERACION_CONTRASENIA_IDM,
                }
                );
        }

        private string GetResourceContentFromAssembly(Assembly assembly, string[] assemblyResourcesNames, string fileName)
        {
            var result = string.Empty;
            using (Stream stream = assembly.GetManifestResourceStream(assemblyResourcesNames.FirstOrDefault(r => r.Contains(fileName))))
            {
                if (stream == null)
                {
                    //result = "";
                    throw new FileNotFoundException("No se encontró el archivo embebido.", fileName);
                }

                using (StreamReader reader = new(stream))
                {
                    result = reader.ReadToEnd(); // Retorna el contenido del archivo como una cadena.
                }
            }
            return result;
        }
    }
}
