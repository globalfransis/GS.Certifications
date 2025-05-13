using GS.Certifications.Domain.Commons.Constants;
using GSF.Domain.Entities.Security;
using GSF.Infrastructure.Persistence.Configurations;
using GSF.Infrastructure.Persistence.Configurations.Security;
using GSF.Infrastructure.Persistence.Configurations.Systems;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Certifications.Infrastructure.Persistence.DbContexts.Seeding;

public class OptionSeeding : BaseWithIdEntityConfiguration<Option>
{
    public const long ParametrizationRootOptionId = 10; // opcion padre de paraetrizacion

    public const long SociosOptionId = OptionConfiguration.frameworkReserved + 1;
    public const long SolicitudesCertificacionOptionId = SociosOptionId + 1;
    public const long SociosBackendOptionId = SolicitudesCertificacionOptionId + 1;
    public const long CertificacionesBackendOptionId = SociosBackendOptionId + 1;
    public const long AdministracionOptionId = CertificacionesBackendOptionId + 1;
    public const long ImpuestosOptionId = AdministracionOptionId + 1;
    public const long PercepcionesOptionId = ImpuestosOptionId + 1;

    public const long PeriodosOptionId = PercepcionesOptionId + 1;

    public const long OrdenesComprasOptionId = PeriodosOptionId + 1;
    public const long TiposOrdenesComprasOptionId = OrdenesComprasOptionId + 1;

    public const long ConceptosGastosTiposOptionId = TiposOrdenesComprasOptionId + 1;

    protected override void ConfigureEntity(EntityTypeBuilder<Option> builder)
    {
        //Nothing Here. This class is only for seeding.
    }

    protected override void LoadSeedingData()
    {
        LoadProveedoresOptions();
        LoadBackendOptions();
    }

    private void LoadProveedoresOptions()
    {
        SeedingData.AddRange(
            new Option()
            {
                Id = SociosOptionId,
                Name = "Socios",
                OrderNo = 50,
                Icon = "fas fa-users",
                Code = "SOC",
                Description = "Módulo de Socios",
                TargetPath = string.Empty,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Socios
            },
            new Option()
            {
                Id = SolicitudesCertificacionOptionId,
                Name = "Certificaciones",
                OrderNo = 1,
                ParentId = SociosOptionId,
                Code = "SOC-CER",
                Description = "Gestión de Solicitudes de Certificación",
                TargetPath = "Socios/Certificaciones/Index",
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Socios
            }
            );
    }

    private void LoadBackendOptions()
    {
        SeedingData.AddRange(
            new Option()
            {
                Id = SociosBackendOptionId,
                Name = "Socios",
                OrderNo = 50,
                Icon = "fas fa-users",
                Code = "PRO",
                Description = "Módulo de Socios",
                TargetPath = string.Empty,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Option()
            {
                Id = CertificacionesBackendOptionId,
                Name = "Certificaciones",
                OrderNo = 1,
                ParentId = SociosBackendOptionId,
                Code = "SOC-CER",
                Description = "Gestión de Solicitudes de Certificación",
                TargetPath = "Socios/Certificaciones/Index",
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Option
            {
                Id = AdministracionOptionId,
                Name = "Administracion",
                OrderNo = 50,
                Code = "PRO-ADM",
                Description = "Administracion",
                Transferable = true,
                DomainFIdm = DomainFConfiguration.GsfBackOfficeIdm,
                TargetPath = "/Proveedores/Empresas/Index",
                ParentId = SociosBackendOptionId
            },
            new Option
            {
                Id = ImpuestosOptionId,
                Name = "Impuestos",
                OrderNo = 50,
                Code = "PRO-IMP",
                Description = "Impuestos",
                Transferable = true,
                DomainFIdm = DomainFConfiguration.GsfBackOfficeIdm,
                TargetPath = "/Proveedores/Impuestos/Index",
                ParentId = ParametrizationRootOptionId
            },
            new Option
            {
                Id = PercepcionesOptionId,
                Name = "Percepciones",
                OrderNo = 50,
                Code = "PRO-PER",
                Description = "Percepciones",
                Transferable = true,
                DomainFIdm = DomainFConfiguration.GsfBackOfficeIdm,
                TargetPath = "/Proveedores/Percepciones/Index",
                ParentId = ParametrizationRootOptionId
            },
            new Option
            {
                Id = PeriodosOptionId,
                Name = "Periodos",
                OrderNo = 50,
                Code = "PRO-PRD",
                Description = "Periodos",
                Transferable = true,
                DomainFIdm = DomainFConfiguration.GsfBackOfficeIdm,
                TargetPath = "/Configuration/Periodos/Index",
                ParentId = ParametrizationRootOptionId
            },
            new Option
            {
                Id = OrdenesComprasOptionId,
                Name = "Documentos de Compras",
                OrderNo = 50,
                Code = "PRO-ODC",
                Description = "Documentos de Compras",
                Transferable = true,
                DomainFIdm = DomainFConfiguration.GsfBackOfficeIdm,
                TargetPath = "/Proveedores/OrdenesCompras/Index",
                ParentId = SociosBackendOptionId
            },
            new Option
            {
                Id = TiposOrdenesComprasOptionId,
                Name = "Tipos de Docs de Compra",
                OrderNo = 50,
                Code = "PRO-TOC",
                Description = "Tipos de Docs de Compra",
                Transferable = true,
                DomainFIdm = DomainFConfiguration.GsfBackOfficeIdm,
                TargetPath = "/Configuration/TiposOrdenesCompras/Index",
                ParentId = ParametrizationRootOptionId
            },
            new Option
            {
                Id = ConceptosGastosTiposOptionId,
                Name = "Conceptos de Gastos",
                OrderNo = 50,
                Code = "PRO-CGT",
                Description = "Conceptos de Gastos",
                Transferable = true,
                DomainFIdm = DomainFConfiguration.GsfBackOfficeIdm,
                TargetPath = "/Configuration/ConceptosGastosTipos/Index",
                ParentId = ParametrizationRootOptionId
            }
            );
    }
}