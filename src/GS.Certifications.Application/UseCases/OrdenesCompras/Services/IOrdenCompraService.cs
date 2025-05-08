using DocumentFormat.OpenXml.Office2010.ExcelAc;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Services;
using GSF.Domain.Entities.Global;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.OrdenesCompras;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.OrdenesCompras.Services
{
    /// <summary>
    /// Interface para manejar servicios de OrdenCompra.
    /// </summary>
    public interface IOrdenCompraService
    {
        Task<OrdenCompra> GetAsync(int id);
        Task<IPaginatedQueryResult<OrdenCompra>> GetManyAsync(IOrdenCompraQueryParameter request);
        Task<OrdenCompra> CreateAsync(IOrdenCompraCreate t);
        Task UpdateAsync(IOrdenCompraUpdate p);
        Task DeleteAsync(int id);
        Task<OrdenCompraTipo> GetTipoAsync(int id);
        Task<IPaginatedQueryResult<OrdenCompraTipo>> GetManyTiposAsync(IOrdenCompraTipoQueryParameter request);
        Task<OrdenCompraTipo> CreateTipoAsync(IOrdenCompraTipoCreate t);
        Task UpdateTipoAsync(IOrdenCompraTipoUpdate p);
        Task DeleteTipoAsync(int id);
        Task<List<OrdenCompraEstado>> GetAllOrdenesComprasEstados();
        Task<List<OrdenCompraTipo>> GetAllOrdenesComprasTipos();
    }

    /// <summary>
    /// Filtros de consulta para OrdenCompra.
    /// </summary>
    public interface IOrdenCompraQueryParameter : IQueryParameter
    {
        int? EmpresaPortalId { get; set; }
        DateTime? FechaDesde { get; set; }
        DateTime? FechaHasta { get; set; }
        int? OrdenCompraTipoId { get; set; }
        int? OrdenCompraEstadoIdm { get; set; }
    }

    /// <summary>
    /// Define un contrato para crear un OrdenCompra.
    /// </summary>
    public interface IOrdenCompraCreate
    {
        string NumeroOrden { get; set; }
        DateTime Fecha { get; set; }
        int? EmpresaPortalId { get; set; }
        int? OrdenCompraTipoId { get; set; }
        //string CodigoHES { get; set; }
        string CodigoQAD { get; set; }
        int? OrdenCompraEstadoIdm { get; set; }
        decimal Importe { get; set; }
        long? MonedaId { get; set; }
        string Observaciones { get; set; }
    }

    /// <summary>
    /// Define un contrato para actualizar un OrdenCompra.
    /// </summary>
    public interface IOrdenCompraUpdate
    {
        int Id { get; set; }
        string NumeroOrden { get; set; }
        DateTime Fecha { get; set; }
        int? EmpresaPortalId { get; set; }
        int? OrdenCompraTipoId { get; set; }
        //string CodigoHES { get; set; }
        string CodigoQAD { get; set; }
        int? OrdenCompraEstadoIdm { get; set; }
        decimal Importe { get; set; }
        long? MonedaId { get; set; }
        string Observaciones { get; set; }
    }

    /// <summary>
    /// Filtros de consulta para OrdenCompra.
    /// </summary>
    public interface IOrdenCompraTipoQueryParameter : IQueryParameter
    {
        string Nombre { get; set; }
        int? Caracteristica { get; set; }
    }

    /// <summary>
    /// Define un contrato para crear un OrdenCompra.
    /// </summary>
    public interface IOrdenCompraTipoCreate
    {
        string Nombre { get; set; }
        string Descripcion { get; set; }
        bool EsRequerida { get; set; }
        bool EsAbierta { get; set; }
        bool EsRecurrente { get; set; }
        bool EsUnica { get; set; }
        List<long> GruposId { get; set; }
    }

    /// <summary>
    /// Define un contrato para actualizar un OrdenCompra.
    /// </summary>
    public interface IOrdenCompraTipoUpdate
    {
        int Id { get; set; }
        string Nombre { get; set; }
        string Descripcion { get; set; }
        bool EsRequerida { get; set; }
        bool EsAbierta { get; set; }
        bool EsRecurrente { get; set; }
        bool EsUnica { get; set; }
        List<long> GruposId { get; set; }
    }
}
