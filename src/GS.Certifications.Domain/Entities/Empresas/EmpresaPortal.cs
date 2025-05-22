using GS.Certifications.Domain.Entities.Comprobantes;
using GS.Certifications.Domain.Entities.Empresas.Alicuotas;
using GS.Certifications.Domain.Entities.Empresas.ConceptosGastosTipos;
using GS.Certifications.Domain.Entities.Empresas.ModosLecturas;
using GS.Certifications.Domain.Entities.Empresas.OrdenesCompras;
using GS.Certifications.Domain.Entities.Empresas.Roles;
using GSF.Domain.Common;
using GSF.Domain.Entities.Companies;
using GSF.Domain.Entities.Geo;
using System;
using System.Collections.Generic;

namespace GS.Certifications.Domain.Entities.Empresas;

public class EmpresaPortal : BaseIntEntity
{
    public Guid Guid { get; private set; }
    public long CompanyId { get; set; }
    public Company Company { get; set; }
    public string CodigoProveedor { get; set; }
    public string RazonSocial { get; set; }
    public string NombreFantasia { get; set; }
    public string IdentificadorTributario { get; set; }
    public bool GranContribuyente { get; set; }
    public string Direccion { get; set; }
    public string CodigoPostal { get; set; }
    public long OrganizationId { get; set; }
    public Organization Organization { get; set; }
    public long? PaisId { get; set; }
    public Country Pais { get; set; }
    public long? ProvinciaId { get; set; }
    public Province Provincia { get; set; }
    public long? CiudadId { get; set; }
    public City Ciudad { get; set; }
    public string CiudadDescripcion { get; set; }
    public string TelefonoPrincipal { get; set; }
    public string TelefonoAlternativo { get; set; }
    public string EmailPrincipal { get; set; }
    public string EmailAlternativo { get; set; }
    public string Contacto { get; set; }
    public string ContactoAlternativo { get; set; }
    public short TipoResponsableId { get; set; }
    public CategoriaTipo TipoResponsable { get; set; }
    public string NumeroIngresosBrutos { get; set; }
    public short? TipoCuentaId { get; set; }
    public TipoCuenta TipoCuenta { get; set; }
    public string CuentaBancaria { get; set; }
    public string PaginaWeb { get; set; }
    public string RedesSociales { get; set; }
    public string DescripcionEmpresa { get; set; }
    public string ProductosServiciosOfrecidos { get; set; }
    public string ReferenciasComerciales { get; set; }
    public bool Confirmado { get; set; }
    public List<EmpresaCurrency> Monedas { get; set; }
    public List<EmpresaRol> Roles { get; set; }
    public List<EmpresaAlicuota> Alicuotas { get; set; }
    public List<EmpresaModoLectura> ModosLecturas { get; set; }
    public List<EmpresaOrdenCompraTipo> OrdenesComprasTipos { get; set; }
    public List<EmpresaConceptoGastoTipo> ConceptosGastosTipos { get; set; }

    public short? TipoId { get; set; }
    public TipoEmpresaPortal Tipo { get; set; }

    public EmpresaPortal()
    {
        Guid = Guid.NewGuid();
    }
}

public class CompanyExtra : BaseIntEntity
{
    public long CompanyId { get; set; }
    public string Direccion { get; set; }
    public string CodigoPostal { get; set; }
    public long OrganizationId { get; set; }
    public Organization Organization { get; set; }
    public long PaisId { get; set; }
    public Country Pais { get; set; }
    public long? ProvinciaId { get; set; }
    public Province Provincia { get; set; }
    public long? CiudadId { get; set; }
    public City Ciudad { get; set; }
    public string IdentificadorTributario { get; set; }
    public string CiudadDescripcion { get; set; }
    public string TelefonoPrincipal { get; set; }
    public string TelefonoAlternativo { get; set; }
    public string EmailPrincipal { get; set; }
    public string EmailAlternativo { get; set; }
    public string Contacto { get; set; }
    public string ContactoAlternativo { get; set; }
    public short TipoResponsableId { get; set; }
    public CategoriaTipo TipoResponsable { get; set; }
    public string NumeroIngresosBrutos { get; set; }
    public short? TipoCuentaId { get; set; }
    public TipoCuenta TipoCuenta { get; set; }
    public string CuentaBancaria { get; set; }
    public string IdMoneda { get; set; }
}