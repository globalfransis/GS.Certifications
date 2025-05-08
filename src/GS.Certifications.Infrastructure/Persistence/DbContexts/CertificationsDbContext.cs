using GSF.Application.Common.Interfaces;
using GSF.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Domain.Entities.Alicuotas;
using GS.Certifications.Domain.Entities.Comprobantes;
using GS.Certifications.Domain.Entities.ConceptosGastosTipos;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Empresas.Alicuotas;
using GS.Certifications.Domain.Entities.Empresas.ConceptosGastosTipos;
using GS.Certifications.Domain.Entities.Empresas.ModosLecturas;
using GS.Certifications.Domain.Entities.Empresas.OrdenesCompras;
using GS.Certifications.Domain.Entities.Empresas.Roles;
using GS.Certifications.Domain.Entities.Impuestos;
using GS.Certifications.Domain.Entities.MailProcessors;
using GS.Certifications.Domain.Entities.ModosLecturas;
using GS.Certifications.Domain.Entities.OrdenesCompras;
using GS.Certifications.Domain.Entities.Percepciones;
using GS.Certifications.Domain.Entities.Periodos;
using GS.Certifications.Domain.Entities.Seguridad;

namespace GS.Certifications.Infrastructure.Persistence.DbContexts;

public class CertificationsDbContext : ApplicationDbContext, ICertificationsDbContext
{
    public CertificationsDbContext(
      DbContextOptions<CertificationsDbContext> options,
      ICurrentUserService currentUserService,
      IDomainEventService domainEventService,
      IDateTime dateTime) : base(options, currentUserService, domainEventService, dateTime)
    {
    }

    public DbSet<EmpresaPortal> EmpresasPortales { get; set; }
    public DbSet<UsuarioEmpresaPortal> UsuarioEmpresasPortales { get; set; }
    public DbSet<UsuarioEmpresaPortalRol> UsuarioEmpresaPortalRol { get; set; }
    public DbSet<CategoriaTipo> CategoriasTipos { get; set; }
    public DbSet<ComprobanteTipo> ComprobanteTipos { get; set; }
    public DbSet<UnidadMedida> UnidadMedidas { get; set; }
    public DbSet<Comprobante> Comprobantes { get; set; }
    public DbSet<CodigoAutorizacionTipo> CodigoAutorizacionTipos { get; set; }
    public DbSet<CategoriaTipo> CategoriaTipos { get; set; }
    public DbSet<ComprobanteDetalle> ComprobanteDetalles { get; set; }
    public DbSet<Impuesto> Impuestos { get; set; }
    public DbSet<Percepcion> Percepciones { get; set; }
    public DbSet<RolTipo> RolesTipos { get; set; }
    public DbSet<TipoCuenta> TiposCuentas { get; set; }
    public DbSet<CompanyExtra> CompanyExtras { get; set; }
    public DbSet<EmpresaRol> EmpresasRoles { get; set; }
    public DbSet<EmpresaCurrency> EmpresasCurrencies { get; set; }
    public DbSet<ImpuestoDetalle> ImpuestoDetalles { get; set; }
    public DbSet<PercepcionDetalle> PercepcionDetalles { get; set; }
    public DbSet<ComprobanteEstado> ComprobanteEstados { get; set; }
    public DbSet<ImpuestoTipo> ImpuestosTipos { get; set; }
    public DbSet<Alicuota> Alicuotas { get; set; }
    public DbSet<EmpresaAlicuota> EmpresasAlicuotas { get; set; }
    public DbSet<PercepcionTipo> PercepcionesTipos { get; set; }
    public DbSet<Periodo> Periodos { get; set; }
    public DbSet<EstadoPeriodo> EstadosPeriodos { get; set; }
    public DbSet<ModoLectura> ModosLecturas { get; set; }
    public DbSet<EmpresaModoLectura> EmpresasModosLecturas { get; set; }
    public DbSet<ComprobanteEMailExtension> ComprobanteEMailExtensiones { get; set; }
    public DbSet<IntegracionFacturaPorCorreo> IntegracionesFacturaPorCorreo { get; set; }
    public DbSet<IntegracionFacturaPorCorreoDetalle> IntegracionFacturaPorCorreoDetalles { get; set; }
    public DbSet<CondicionVenta> CondicionVentas { get; set; }
    public DbSet<OrdenCompra> OrdenesCompras { get; set; }
    public DbSet<OrdenCompraTipo> OrdenesComprasTipos { get; set; }
    public DbSet<OrdenCompraEstado> OrdenesComprasEstados { get; set; }
    public DbSet<GrupoOrdenCompraTipo> GrupoOrdenesComprasTipos { get; set; }
    public DbSet<EmpresaOrdenCompraTipo> EmpresasOrdenesComprasTipos { get; set; }
    public DbSet<EmpresaConceptoGastoTipo> EmpresasConceptosGastosTipos { get; set; }

    public DbSet<ConceptoGastoTipo> ConceptosGastosTipos { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(CertificationsDbContext).Assembly);
    }
}
