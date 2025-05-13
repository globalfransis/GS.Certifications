using GSF.Domain.Entities.Security;
using GSF.Infrastructure.Persistence.Configurations;
using GSF.Infrastructure.Persistence.Configurations.Security;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org.BouncyCastle.Crypto.Digests;
using GS.Certifications.Domain.Commons.Constants;
using System.Collections.Generic;

namespace GS.Certifications.Infrastructure.Persistence.DbContexts.Seeding;

public class GrantsSeeding : BaseWithIdEntityConfiguration<Grant>
{
    public const long SociosSolicitudCertificacionCreate = GrantConfiguration.frameworkReserved + 1;
    public const long SociosSolicitudCertificacionDelete = SociosSolicitudCertificacionCreate + 1;
    public const long SocioSolicitudCertificacionUpdate = SociosSolicitudCertificacionDelete + 1;

    public const long BackendSolicitudCertificacionCreate = SocioSolicitudCertificacionUpdate + 1;
    public const long BackendSolicitudCertificacionUpdate = BackendSolicitudCertificacionCreate + 1;
    public const long BackendSolicitudCertificacionDelete = BackendSolicitudCertificacionUpdate + 1;

    public const long BackendAdministracionCreate = BackendSolicitudCertificacionDelete + 1;
    public const long BackendAdministracionUpdate = BackendAdministracionCreate + 1;
    public const long BackendAdministracionDelete = BackendAdministracionUpdate + 1;

    public const long BackendUsuariosCreate = BackendAdministracionDelete + 1;
    public const long BackendUsuariosUpdate = BackendUsuariosCreate + 1;
    public const long BackendUsuariosDelete = BackendUsuariosUpdate + 1;

    public const long BackendImpuestosCreate = BackendUsuariosDelete + 1;
    public const long BackendImpuestosUpdate = BackendImpuestosCreate + 1;
    public const long BackendImpuestosDelete = BackendImpuestosUpdate + 1;

    public const long BackendPercepcionesCreate = BackendImpuestosDelete + 1;
    public const long BackendPercepcionesUpdate = BackendPercepcionesCreate + 1;
    public const long BackendPercepcionesDelete = BackendPercepcionesUpdate + 1;

    public const long BackendPeriodosCreate = BackendPercepcionesDelete + 1;
    public const long BackendPeriodosUpdate = BackendPeriodosCreate + 1;
    public const long BackendPeriodosDelete = BackendPeriodosUpdate + 1;

    public const long BackendOrdenesComprasCreate = BackendPeriodosDelete + 1;
    public const long BackendOrdenesComprasUpdate = BackendOrdenesComprasCreate + 1;
    public const long BackendOrdenesComprasDelete = BackendOrdenesComprasUpdate + 1;

    public const long BackendTiposOrdenesComprasCreate = BackendOrdenesComprasDelete + 1;
    public const long BackendTiposOrdenesComprasUpdate = BackendTiposOrdenesComprasCreate + 1;
    public const long BackendTiposOrdenesComprasDelete = BackendTiposOrdenesComprasUpdate + 1;

    public const long BackendConceptosGastosTiposCreate = BackendTiposOrdenesComprasDelete + 1;
    public const long BackendConceptosGastosTiposUpdate = BackendConceptosGastosTiposCreate + 1;
    public const long BackendConceptosGastosTiposDelete = BackendConceptosGastosTiposUpdate + 1;
    protected override void ConfigureEntity(EntityTypeBuilder<Grant> builder)
    {
    }

    protected override void LoadSeedingData()
    {
        AddProveedoresGrants();
        AddBackendGrants();
    }

    public void AddBackendGrants()
    {
        SeedingData.AddRange(
            new Grant()
            {
                Id = BackendSolicitudCertificacionCreate,
                Name = "solicitudcertificacion.create",
                Description = "Alta de Solicitud de Certificación",
                OptionId = OptionSeeding.CertificacionesBackendOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Grant()
            {
                Id = BackendSolicitudCertificacionDelete,
                Name = "solicitudcertificacion.delete",
                Description = "Baja de Solicitud de Certificacion",
                OptionId = OptionSeeding.CertificacionesBackendOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Grant()
            {
                Id = BackendSolicitudCertificacionUpdate,
                Name = "solicitudcertificacion.update",
                Description = "Modificación de Comprobantes",
                OptionId = OptionSeeding.CertificacionesBackendOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },

            new Grant()
            {
                Id = BackendAdministracionCreate,
                Name = "empresasportales.create",
                Description = "Alta de Empresas Portales",
                OptionId = OptionSeeding.AdministracionOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Grant()
            {
                Id = BackendAdministracionDelete,
                Name = "empresasportales.delete",
                Description = "Baja de Empresas Portales",
                OptionId = OptionSeeding.AdministracionOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Grant()
            {
                Id = BackendAdministracionUpdate,
                Name = "empresasportales.update",
                Description = "Modificación de Empresas Portales",
                OptionId = OptionSeeding.AdministracionOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },

            new Grant()
            {
                Id = BackendUsuariosCreate,
                Name = "usuariosempresas.create",
                Description = "Alta de Usuarios de Empresas Portales",
                OptionId = OptionSeeding.AdministracionOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Grant()
            {
                Id = BackendUsuariosDelete,
                Name = "usuariosempresas.delete",
                Description = "Baja de Usuarios de Empresas Portales",
                OptionId = OptionSeeding.AdministracionOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Grant()
            {
                Id = BackendUsuariosUpdate,
                Name = "usuariosempresas.update",
                Description = "Modificación de Usuarios de Empresas Portales",
                OptionId = OptionSeeding.AdministracionOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },

            new Grant()
            {
                Id = BackendImpuestosCreate,
                Name = "impuestos.create",
                Description = "Alta de Impuestos",
                OptionId = OptionSeeding.ImpuestosOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Grant()
            {
                Id = BackendImpuestosDelete,
                Name = "impuestos.delete",
                Description = "Baja de Impuestos",
                OptionId = OptionSeeding.ImpuestosOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Grant()
            {
                Id = BackendImpuestosUpdate,
                Name = "impuestos.update",
                Description = "Modificación de Impuestos",
                OptionId = OptionSeeding.ImpuestosOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },

            new Grant()
            {
                Id = BackendPercepcionesCreate,
                Name = "Percepciones.create",
                Description = "Alta de Percepciones",
                OptionId = OptionSeeding.PercepcionesOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Grant()
            {
                Id = BackendPercepcionesDelete,
                Name = "Percepciones.delete",
                Description = "Baja de Percepciones",
                OptionId = OptionSeeding.PercepcionesOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Grant()
            {
                Id = BackendPercepcionesUpdate,
                Name = "Percepciones.update",
                Description = "Modificación de Percepciones",
                OptionId = OptionSeeding.PercepcionesOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },

            new Grant()
            {
                Id = BackendPeriodosCreate,
                Name = "periodos.create",
                Description = "Alta de Periodos",
                OptionId = OptionSeeding.PeriodosOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Grant()
            {
                Id = BackendPeriodosDelete,
                Name = "periodos.delete",
                Description = "Baja de Periodos",
                OptionId = OptionSeeding.PeriodosOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Grant()
            {
                Id = BackendPeriodosUpdate,
                Name = "periodos.update",
                Description = "Modificación de Periodos",
                OptionId = OptionSeeding.PeriodosOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },

            new Grant()
            {
                Id = BackendOrdenesComprasCreate,
                Name = "ordenescompras.create",
                Description = "Alta de Documentos de Compras",
                OptionId = OptionSeeding.OrdenesComprasOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Grant()
            {
                Id = BackendOrdenesComprasDelete,
                Name = "ordenescompras.delete",
                Description = "Baja de Documentos de Compras",
                OptionId = OptionSeeding.OrdenesComprasOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Grant()
            {
                Id = BackendOrdenesComprasUpdate,
                Name = "ordenescompras.update",
                Description = "Modificación de Documentos de Compras",
                OptionId = OptionSeeding.OrdenesComprasOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },

            new Grant()
            {
                Id = BackendTiposOrdenesComprasCreate,
                Name = "tiposordenescompras.create",
                Description = "Alta de Tipos de Documentos de Compras",
                OptionId = OptionSeeding.TiposOrdenesComprasOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Grant()
            {
                Id = BackendTiposOrdenesComprasDelete,
                Name = "tiposordenescompras.delete",
                Description = "Baja de Tipos de Documentos de Compras",
                OptionId = OptionSeeding.TiposOrdenesComprasOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Grant()
            {
                Id = BackendTiposOrdenesComprasUpdate,
                Name = "tiposordenescompras.update",
                Description = "Modificación de Tipos de Documentos de Compras",
                OptionId = OptionSeeding.TiposOrdenesComprasOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },

            new Grant()
            {
                Id = BackendConceptosGastosTiposCreate,
                Name = "conceptosgastostipos.create",
                Description = "Alta de Tipos de Conceptos de Gastos",
                OptionId = OptionSeeding.ConceptosGastosTiposOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Grant()
            {
                Id = BackendConceptosGastosTiposDelete,
                Name = "conceptosgastostipos.delete",
                Description = "Baja de Tipos de Conceptos de Gastos",
                OptionId = OptionSeeding.ConceptosGastosTiposOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Grant()
            {
                Id = BackendConceptosGastosTiposUpdate,
                Name = "conceptosgastostipos.update",
                Description = "Modificación de Tipos de Conceptos de Gastos",
                OptionId = OptionSeeding.ConceptosGastosTiposOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            }
        );
    }

    public void AddProveedoresGrants()
    {
        SeedingData.AddRange(
            new Grant()
            {
                Id = SociosSolicitudCertificacionCreate,
                Name = "solicitudcertificacion.create",
                Description = "Alta de Solicitud de Certificación",
                OptionId = OptionSeeding.SolicitudesCertificacionOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Socios
            },
            new Grant()
            {
                Id = SociosSolicitudCertificacionDelete,
                Name = "solicitudcertificacion.delete",
                Description = "Baja de Solicitud de Certificación",
                OptionId = OptionSeeding.SolicitudesCertificacionOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Socios
            },
            new Grant()
            {
                Id = SocioSolicitudCertificacionUpdate,
                Name = "solicitudcertificacion.update",
                Description = "Modificación de Solicitud de Certificación",
                OptionId = OptionSeeding.SolicitudesCertificacionOptionId,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Socios
            }
        );
    }
}