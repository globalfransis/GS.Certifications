using GS.Certifications.Domain.Entities.Certificaciones;
using GS.Certifications.Domain.Entities.Certificaciones.Documentos;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Socios.Certificaciones.Services
{
    /// <summary>
    /// Interface para manejar servicios de Certificacion.
    /// </summary>
    public interface ICertificacionService
    {
        Task<Certificacion> GetAsync(int id);
        Task<SolicitudCertificacion> GetSolicitudAsync(int id);
        Task<DocumentoCargado> GetDocumentoAsync(int id);
        Task UpdateDocumentoDraftAsync(int id, ISolicitudCertificacionDocumentoUpdate solicitudCertificacionDocumentoUpdate);
        Task UpdateDocumentoAsync(int id, ISolicitudCertificacionDocumentoUpdate solicitudCertificacionDocumentoUpdate);
        Task<IPaginatedQueryResult<Certificacion>> GetCertificacionesAsync(ICertificacionQueryParameter request);
        Task<IPaginatedQueryResult<SolicitudCertificacion>> GetSolicitudesAsync(ISolicitudCertificacionQueryParameter request);
        Task<Certificacion> CreateAsync(ICertificacionCreate t);
        Task<SolicitudCertificacion> CreateSolicitudAsync(ISolicitudCertificacionCreate c);
        Task UpdateAsync(ICertificacionUpdate p);
        Task DeleteAsync(int id);
        Task DeleteSolicitudAsync(int id, byte[] rowVersion);
        Task DeleteDocumentoSolicitudAsync(int id, byte[] rowVersion);
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

    public interface IDocumentoSolicitudCertificacionAnalysisParameter
    {
        int Id { get; set; }
        IFormFile FormFile { get; set; }
        int SocioId { get; set; }
        int SolicitudId { get; set; }
    }

    /// <summary>
    /// Define un contrato para crear un Certificacion.
    /// </summary>
    public interface ISolicitudCertificacionCreate
    {
        int SocioId { get; set; }
        int CertificacionId { get; set; }
        short? EstadoId { get; set; }
        short CantidadAprobaciones { get; set; }
        string Observaciones { get; set; }
    }

    /// <summary>
    /// Define un contrato para actualizar un Certificacion.
    /// </summary>
    public interface ICertificacionUpdate
    {
        // Agregar propiedades de modificación
    }

    public interface ISolicitudCertificacionDocumentoUpdate
    {
        string ArchivoURL { get; set; }
        string Observaciones { get; set; }
        int? Version { get; set; }
        DateTime? FechaDesde { get; set; }
        DateTime? FechaHasta { get; set; }
        //short EstadoId { get; set; }
        //long? ValidadoPorId { get; set; }
        DateTime? FechaSubida { get; set; }
        byte[] RowVersion { get; set; }
    }
}
