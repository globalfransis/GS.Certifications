using GSF.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
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

namespace GS.Certifications.Application.CQRS.DbContexts;

public interface ICertificationsDbContext : IApplicationDbContext
{
    DbSet<EmpresaPortal> EmpresasPortales { get; set; }
    DbSet<UsuarioEmpresaPortal> UsuarioEmpresasPortales { get; set; }
    DbSet<UsuarioEmpresaPortalRol> UsuarioEmpresaPortalRol { get; set; }
    DbSet<CategoriaTipo> CategoriasTipos { get; set; }
    DbSet<ComprobanteTipo> ComprobanteTipos { get; set; }
    DbSet<UnidadMedida> UnidadMedidas { get; set; }
    DbSet<Comprobante> Comprobantes { get; set; }
    DbSet<ComprobanteDetalle> ComprobanteDetalles { get; set; }
    DbSet<CodigoAutorizacionTipo> CodigoAutorizacionTipos { get; set; }
    DbSet<CategoriaTipo> CategoriaTipos { get; set; }
    DbSet<Impuesto> Impuestos { get; set; }
    DbSet<Percepcion> Percepciones { get; set; }
    DbSet<CompanyExtra> CompanyExtras { get; set; }
    DbSet<RolTipo> RolesTipos { get; set; }
    DbSet<TipoCuenta> TiposCuentas { get; set; }
    DbSet<ImpuestoDetalle> ImpuestoDetalles { get; set; }
    DbSet<PercepcionDetalle> PercepcionDetalles { get; set; }

    DbSet<EmpresaRol> EmpresasRoles { get; set; }
    DbSet<EmpresaCurrency> EmpresasCurrencies { get; set; }
    DbSet<ComprobanteEstado> ComprobanteEstados { get; set; }

    DbSet<Periodo> Periodos { get; set; }
    DbSet<EstadoPeriodo> EstadosPeriodos { get; set; }

    DbSet<ImpuestoTipo> ImpuestosTipos { get; set; }
    DbSet<Alicuota> Alicuotas { get; set; }
    DbSet<EmpresaAlicuota> EmpresasAlicuotas { get; set; }
    DbSet<PercepcionTipo> PercepcionesTipos { get; set; }

    DbSet<ModoLectura> ModosLecturas { get; set; }
    DbSet<EmpresaModoLectura> EmpresasModosLecturas { get; set; }

    DbSet<ComprobanteEMailExtension> ComprobanteEMailExtensiones { get; set; }
    DbSet<IntegracionFacturaPorCorreo> IntegracionesFacturaPorCorreo { get; set; }
    DbSet<IntegracionFacturaPorCorreoDetalle> IntegracionFacturaPorCorreoDetalles { get; set; }

    DbSet<CondicionVenta> CondicionVentas { get; set; }

    DbSet<OrdenCompra> OrdenesCompras { get; set; }
    DbSet<OrdenCompraTipo> OrdenesComprasTipos { get; set; }
    DbSet<OrdenCompraEstado> OrdenesComprasEstados { get; set; }
    DbSet<EmpresaOrdenCompraTipo> EmpresasOrdenesComprasTipos { get; set; }

    DbSet<ConceptoGastoTipo> ConceptosGastosTipos { get; set; }
    DbSet<GrupoOrdenCompraTipo> GrupoOrdenesComprasTipos { get; set; }
    DbSet<EmpresaConceptoGastoTipo> EmpresasConceptosGastosTipos { get; set; }
}
