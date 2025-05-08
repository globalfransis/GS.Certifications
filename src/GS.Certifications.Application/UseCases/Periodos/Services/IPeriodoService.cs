using DocumentFormat.OpenXml.Office2010.ExcelAc;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Services;
using GS.Certifications.Domain.Entities.Periodos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Periodos.Services
{
    /// <summary>
    /// Interface para manejar servicios de Periodo.
    /// </summary>
    public interface IPeriodoService
    {
        Task<Periodo> GetAsync(int id);
        Task<IPaginatedQueryResult<Periodo>> GetManyAsync(IPeriodoQueryParameter request);
        Task<Periodo> CreateAsync(IPeriodoCreate t);
        Task UpdateAsync(IPeriodoUpdate p);
        Task DeleteAsync(int id);
        Task<List<EstadoPeriodo>> GetAllEstadosPeriodos();
    }

    /// <summary>
    /// Filtros de consulta para Periodo.
    /// </summary>
    public interface IPeriodoQueryParameter : IQueryParameter
    {
        int? EstadoIdm { get; set; }
        DateTime? FechaInicio { get; set; }
        DateTime? FechaFin { get; set; }
    }

    /// <summary>
    /// Define un contrato para crear un Periodo.
    /// </summary>
    public interface IPeriodoCreate
    {
        int? Año { get; set; }
        int? NumeroPeriodo { get; set; }
        DateTime? FechaInicio { get; set; }
        DateTime? FechaFin { get; set; }
        int? EstadoIdm { get; set; }
    }

    /// <summary>
    /// Define un contrato para actualizar un Periodo.
    /// </summary>
    public interface IPeriodoUpdate
    {
        int Id { get; set; }
        int? Año { get; set; }
        int? NumeroPeriodo { get; set; }
        DateTime? FechaInicio { get; set; }
        DateTime? FechaFin { get; set; }
        int? EstadoIdm { get; set; }
    }
}
