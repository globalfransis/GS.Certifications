using DocumentFormat.OpenXml.Office2010.ExcelAc;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Services;
using GS.Certifications.Domain.Entities.ConceptosGastosTipos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.ConceptosGastosTipos.Services
{
    /// <summary>
    /// Interface para manejar servicios de ConceptoGastoTipo.
    /// </summary>
    public interface IConceptoGastoTipoService
    {
        Task<ConceptoGastoTipo> GetAsync(int id);
        Task<IPaginatedQueryResult<ConceptoGastoTipo>> GetManyAsync(IConceptoGastoTipoQueryParameter request);
        Task<ConceptoGastoTipo> CreateAsync(IConceptoGastoTipoCreate t);
        Task UpdateAsync(IConceptoGastoTipoUpdate p);
        Task DeleteAsync(int id);
        Task<List<ConceptoGastoTipo>> GetAllConceptosGastosTipos();
    }

    /// <summary>
    /// Filtros de consulta para ConceptoGastoTipo.
    /// </summary>
    public interface IConceptoGastoTipoQueryParameter : IQueryParameter
    {
        string Nombre { get; set; }
    }

    /// <summary>
    /// Define un contrato para crear un ConceptoGastoTipo.
    /// </summary>
    public interface IConceptoGastoTipoCreate
    {
        string Nombre { get; set; }
        string Descripcion { get; set; }
        string ConceptoContableNombre { get; set; }
        string ConceptoContableValor { get; set; }
    }

    /// <summary>
    /// Define un contrato para actualizar un ConceptoGastoTipo.
    /// </summary>
    public interface IConceptoGastoTipoUpdate
    {
        int Id { get; set; } // Adjust Id type properly
        byte[] RowVersion { get; set; }
        string Nombre { get; set; }
        string Descripcion { get; set; }
        string ConceptoContableNombre { get; set; }
        string ConceptoContableValor { get; set; }
    }
}
