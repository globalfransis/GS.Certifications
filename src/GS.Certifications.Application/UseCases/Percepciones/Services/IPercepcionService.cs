using DocumentFormat.OpenXml.Office2010.ExcelAc;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Services;
using GS.Certifications.Domain.Entities.Percepciones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Percepciones.Services
{
    /// <summary>
    /// Interface para manejar servicios de Percepcion.
    /// </summary>
    public interface IPercepcionService
    {
        Task<Percepcion> GetAsync(int id);
        Task<IPaginatedQueryResult<Percepcion>> GetManyAsync(IPercepcionQueryParameter request);
        Task<Percepcion> CreateAsync(IPercepcionCreate t);
        Task UpdateAsync(IPercepcionUpdate p);
        Task DeleteAsync(int id);
        Task<List<PercepcionTipo>> GetAllPercepcionesTiposCuentas();
    }

    /// <summary>
    /// Filtros de consulta para Percepcion.
    /// </summary>
    public interface IPercepcionQueryParameter : IQueryParameter
    {
        string Descripcion { get; set; }
        int? PercepcionTipoId { get; set; }
        int? ProvinciaId { get; set; }
    }

    /// <summary>
    /// Define un contrato para crear un Percepcion.
    /// </summary>
    public interface IPercepcionCreate
    {
        string Descripcion { get; set; }
        short? PercepcionTipoId { get; set; }
        long? ProvinciaId { get; set; }
    }

    /// <summary>
    /// Define un contrato para actualizar un Percepcion.
    /// </summary>
    public interface IPercepcionUpdate
    {
        int Id { get; set; }
        string Descripcion { get; set; }
        short? PercepcionTipoId { get; set; }
        long? ProvinciaId { get; set; }
    }
}
