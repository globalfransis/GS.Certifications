using GS.Certifications.Application.Commons.Dtos.ConceptosGastosTipos;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.Commons.Dtos.ModosLecturas;
using GS.Certifications.Application.Commons.Dtos.OrdenesCompra;
using GSF.Application.Global.Currencies.Queries;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Services;
using GSF.Domain.Entities.Global;
using GSF.Domain.Entities.Security;
using GS.Certifications.Domain.Entities.Comprobantes;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Empresas.ConceptosGastosTipos;
using GS.Certifications.Domain.Entities.Empresas.ModosLecturas;
using GS.Certifications.Domain.Entities.Empresas.OrdenesCompras;
using GS.Certifications.Domain.Entities.Empresas.Roles;
using GS.Certifications.Domain.Entities.ModosLecturas;
using GS.Certifications.Domain.Entities.Seguridad;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Services
{
    /// <summary>
    /// Interface para manejar servicios de Empresas.
    /// </summary>
    public interface IEmpresaPortalService
    {
        Task<EmpresaPortal> GetAsync(long id);
        Task<IPaginatedQueryResult<EmpresaPortal>> GetManyAsync(IEmpresasQueryParameter request);
        Task<EmpresaPortal> CreateAsync(IEmpresasCreate t);
        Task UpdateAsync(IEmpresasUpdate p);
        Task DeleteAsync(int id);
        Task<IPaginatedQueryResult<RolTipo>> GetManyRolesTiposAsync(IRolesTiposQueryParameter request);
        Task<IPaginatedQueryResult<EmpresaRol>> GetEmpresasRolesManyAsync(IEmpresasRolesQueryParameter request);
        Task<List<Currency>> GetAllCurrencies();
        Task<List<ModoLectura>> GetAllModosLecturas();
        Task<List<EmpresaModoLectura>> GetAllEmpresasModosLecturas(long? empresaPortalId);
        Task<List<EmpresaOrdenCompraTipo>> GetAllEmpresasOrdenesComprasTipos(long? empresaPortalId);
        Task<List<EmpresaConceptoGastoTipo>> GetAllEmpresasConceptosGastosTipos(long? empresaPortalId);
        Task<List<TipoCuenta>> GetAllTiposCuentas();
        Task<List<CategoriaTipo>> GetAllCategoriasTipos();
        Task<User> GetUsuarioExternoAsync(string email);
        Task<UsuarioEmpresaPortal> CreateUsuarioEmpresaAsync(IUsuarioEmpresaCreate t);
        Task ModificarModosLectura(int empresaPortalId, List<ModoLecturaDto> modosLecturas);
        Task ModificarOrdenesComprasTipos(int empresaPortalId, List<OrdenCompraTipoDto> modosLecturas);
        Task ModificarConceptosGastosTipos(int empresaPortalId, List<ConceptoGastoTipoDto> modosLecturas);
    }


    public interface IUsuarioEmpresaCreate
    {
        int EmpresaPortalId { get; set; }
        string Login { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string IdNumber { get; set; }
        string PhoneNumber { get; set; }
        string Email { get; set; }
        List<RolTipoDto> Roles { get; set; }
    }

    public interface IEmpresasRolesQueryParameter : IQueryParameter
    {
        int? EmpresaPortalId { get; set; }
    }
    public interface IRolesTiposQueryParameter : IQueryParameter
    {
        int? EmpresaPortalId { get; set; }
        string Descripcion { get; set; }
        string Codigo { get; set; }
    }

    /// <summary>
    /// Filtros de consulta para Empresas.
    /// </summary>
    public interface IEmpresasQueryParameter : IQueryParameter
    {
        string RazonSocial { get; set; }
        string NombreFantasia { get; set; }
        string IdentificadorTributario { get; set; }
        bool? GranContribuyente { get; set; }
        string Contacto { get; set; }
        long? PaisId { get; set; }
        long? ProvinciaId { get; set; }
        long? CiudadId { get; set; }
    }

    /// <summary>
    /// Define un contrato para crear un Empresas.
    /// </summary>
    public interface IEmpresasCreate
    {
        string CodigoProveedor { get; set; }
        string RazonSocial { get; set; }
        string NombreFantasia { get; set; }
        string IdentificadorTributario { get; set; }
        bool GranContribuyente { get; set; }
        string Direccion { get; set; }
        string CodigoPostal { get; set; }
        long? PaisId { get; set; }
        long? ProvinciaId { get; set; }
        long? CiudadId { get; set; }
        string CiudadDescripcion { get; set; }
        string TelefonoPrincipal { get; set; }
        string TelefonoAlternativo { get; set; }
        string EmailPrincipal { get; set; }
        string EmailAlternativo { get; set; }
        string Contacto { get; set; }
        string ContactoAlternativo { get; set; }
        short TipoResponsableId { get; set; }
        string NumeroIngresosBrutos { get; set; }
        short? TipoCuentaId { get; set; }
        string CuentaBancaria { get; set; }
        string PaginaWeb { get; set; }
        string RedesSociales { get; set; }
        string DescripcionEmpresa { get; set; }
        string ProductosServiciosOfrecidos { get; set; }
        string ReferenciasComerciales { get; set; }
        bool Confirmado { get; set; }
        List<short> RolesIdm { get; set; }
        List<short> AlicuotasIdm { get; set; }
        List<short> OrdenesComprasTiposId { get; set; }
        List<short> ConceptosGastosTiposId { get; set; }
        List<IEmpresaCurrencyCreate> Monedas { get; set; }
    }

    public interface IEmpresaCurrencyCreate
    {
        int? Id { get; set; }
        byte?[] RowVersion { get; set; }
        int? EmpresaPortalId { get; set; }
        CurrencyDto Currency { get; set; }
        long CurrencyId { get; set; }
        bool MonedaPorDefecto { get; set; }
        bool Deleted { get; set; }
    }
    /// <summary>
    /// Define un contrato para actualizar un Empresas.
    /// </summary>
    public interface IEmpresasUpdate
    {
        long Id { get; set; }
        string CodigoProveedor { get; set; }
        string RazonSocial { get; set; }
        string NombreFantasia { get; set; }
        string IdentificadorTributario { get; set; }
        bool GranContribuyente { get; set; }
        string Direccion { get; set; }
        string CodigoPostal { get; set; }
        long? PaisId { get; set; }
        long? ProvinciaId { get; set; }
        long? CiudadId { get; set; }
        string CiudadDescripcion { get; set; }
        string TelefonoPrincipal { get; set; }
        string TelefonoAlternativo { get; set; }
        string EmailPrincipal { get; set; }
        string EmailAlternativo { get; set; }
        string Contacto { get; set; }
        string ContactoAlternativo { get; set; }
        short TipoResponsableId { get; set; }
        string NumeroIngresosBrutos { get; set; }
        short? TipoCuentaId { get; set; }
        string CuentaBancaria { get; set; }
        string PaginaWeb { get; set; }
        string RedesSociales { get; set; }
        string DescripcionEmpresa { get; set; }
        string ProductosServiciosOfrecidos { get; set; }
        string ReferenciasComerciales { get; set; }
        bool Confirmado { get; set; }
        List<short> RolesIdm { get; set; }
        List<short> AlicuotasIdm { get; set; }
        List<IEmpresaCurrencyUpdate> Monedas { get; set; }
    }

    public interface IEmpresaCurrencyUpdate
    {
        int? Id { get; set; }
        byte?[] RowVersion { get; set; }
        int? EmpresaPortalId { get; set; }
        CurrencyDto Currency { get; set; }
        long CurrencyId { get; set; }
        bool MonedaPorDefecto { get; set; }
        bool Deleted { get; set; }
    }
}
