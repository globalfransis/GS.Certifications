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

    public const long ProveedoresOptionId = OptionConfiguration.frameworkReserved + 1;
    public const long ComprobantesOptionId = ProveedoresOptionId + 1;
    public const long ProveedoresBackendOptionId = ComprobantesOptionId + 1;
    public const long ComprobantesBackendOptionId = ProveedoresBackendOptionId + 1;
    public const long AdministracionOptionId = ComprobantesBackendOptionId + 1;
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
                Id = ProveedoresOptionId,
                Name = "Proveedores",
                OrderNo = 50,
                Icon = "fas fa-users",
                Code = "PRO",
                Description = "Modulo de Proveedores",
                TargetPath = string.Empty,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Socios
            },
            new Option()
            {
                Id = ComprobantesOptionId,
                Name = "Comprobantes",
                OrderNo = 1,
                ParentId = ProveedoresOptionId,
                Code = "PRO-COM",
                Description = "Carga de Comprobantes",
                TargetPath = "Proveedores/Comprobantes/Index",
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
                Id = ProveedoresBackendOptionId,
                Name = "Socios",
                OrderNo = 50,
                Icon = "fas fa-users",
                Code = "PRO",
                Description = "Modulo de Socios",
                TargetPath = string.Empty,
                Transferable = true,
                DomainFIdm = DomainFIdmConstants.Backoffice
            },
            new Option()
            {
                Id = ComprobantesBackendOptionId,
                Name = "Comprobantes",
                OrderNo = 1,
                ParentId = ProveedoresBackendOptionId,
                Code = "PRO-COM",
                Description = "Carga de Comprobantes",
                TargetPath = "Proveedores/Comprobantes/Index",
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
                ParentId = ProveedoresBackendOptionId
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
                ParentId = ProveedoresBackendOptionId
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