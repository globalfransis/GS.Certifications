using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Services;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using GS.Certifications.Domain.Entities.Alicuotas;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Impuestos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Impuestos.Services
{
    /// <summary>
    /// Interface para manejar servicios de Impuesto.
    /// </summary>
    public interface IImpuestoService
    {
        Task<Impuesto> GetAsync(int id);
        Task<IPaginatedQueryResult<Impuesto>> GetManyAsync(IImpuestoQueryParameter request);
        Task<Impuesto> CreateAsync(IImpuestoCreate t);
        Task UpdateAsync(IImpuestoUpdate p);
        Task DeleteAsync(int id);
        Task<IPaginatedQueryResult<Alicuota>> GetManyAlicuotasAsync(IAlicuotasQueryParameter request);
        Task<List<ImpuestoTipo>> GetAllImpuestosTiposCuentas();
        Task<List<Alicuota>> GetAllAlicuotasCuentas();
    }

    /// <summary>
    /// Filtros de consulta para Impuesto.
    /// </summary>
    public interface IImpuestoQueryParameter : IQueryParameter
    {
        string CodigoAFIP { get; set; }
        decimal? Valor { get; set; }
        int? TipoId { get; set; }
        int? ProvinciaId { get; set; }
    }

    public interface IAlicuotasQueryParameter : IQueryParameter
    {
        short Idm { get; set; }
        string CodigoAFIP { get; set; }
        decimal? Valor { get; set; }
    }

    /// <summary>
    /// Define un contrato para crear un Impuesto.
    /// </summary>
    public interface IImpuestoCreate
    {
        string Nombre { get; set; }
        string Descripcion { get; set; }
        short? TipoId { get; set; }
        long? ProvinciaId { get; set; }
        short? AlicuotaId { get; set; }
        decimal? Valor { get; set; }
    }

    /// <summary>
    /// Define un contrato para actualizar un Impuesto.
    /// </summary>
    public interface IImpuestoUpdate
    {
        int Id { get; set; }
        string Nombre { get; set; }
        string Descripcion { get; set; }
        short? TipoId { get; set; }
        long? ProvinciaId { get; set; }
        short? AlicuotaId { get; set; }
        decimal? Valor { get; set; }
    }
}
