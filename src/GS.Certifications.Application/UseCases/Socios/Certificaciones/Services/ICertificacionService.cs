using GS.Certifications.Domain.Entities.Certificaciones;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Services;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Socios.Certificaciones.Services
{
    /// <summary>
    /// Interface para manejar servicios de Certificacion.
    /// </summary>
    public interface ICertificacionService
    {
        Task<Certificacion> GetAsync(int id);
        Task<IPaginatedQueryResult<Certificacion>> GetCertificacionesAsync(ICertificacionQueryParameter request);
        Task<IPaginatedQueryResult<SolicitudCertificacion>> GetSolicitudesAsync(ISolicitudCertificacionQueryParameter request);
        Task<Certificacion> CreateAsync(ICertificacionCreate t);
        Task UpdateAsync(ICertificacionUpdate p);
        Task DeleteAsync(int id);
    }

    /// <summary>
    /// Filtros de consulta para Certificacion.
    /// </summary>
    public interface ICertificacionQueryParameter : IQueryParameter
    {
        int? SocioId { get; set; }
        int? TipoSocioId { get; set; }
        string Nombre { get; set; }
        bool? Activa { get; set; }
        short? EstadoId { get; set; }
    }

    /// <summary>
    /// Filtros de consulta para Certificacion.
    /// </summary>
    public interface ISolicitudCertificacionQueryParameter : IQueryParameter
    {
        int? CertificacionId { get; set; }
        int? SocioId { get; set; }
        int? TipoSocioId { get; set; }
        string Nombre { get; set; }
        bool? Activa { get; set; }
        short? EstadoId { get; set; }
    }

    /// <summary>
    /// Define un contrato para crear un Certificacion.
    /// </summary>
    public interface ICertificacionCreate
    {
        // Agregar propiedades de creación
    }

    /// <summary>
    /// Define un contrato para actualizar un Certificacion.
    /// </summary>
    public interface ICertificacionUpdate
    {
        // Agregar propiedades de modificación
    }
}
