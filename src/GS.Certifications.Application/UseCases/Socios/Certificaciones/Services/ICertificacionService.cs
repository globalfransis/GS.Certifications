using GS.AI.DocumentIntelligence.Legacy.Models;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Domain.Entities.Certificaciones;
using GS.Certifications.Domain.Entities.Certificaciones.Documentos;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Services;
using GSF.Domain.Entities.Security;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
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
        Task<string> AnalyzeDocumentoAsync(BinaryData bytesSource, IDocumentoSolicitudCertificacionAnalysisParameter parameters, AnalysisCompletedCallback onCompletedCallback, CancellationToken cancellationToken);
        DocumentoCargado CreateDocumento(ISolicitudCertificacionDocumentoCreate d);
        Task UpdateDocumentoDraftAsync(int id, ISolicitudCertificacionDocumentoUpdate solicitudCertificacionDocumentoUpdate);
        Task UpdateDocumentoAsync(int id, ISolicitudCertificacionDocumentoUpdate solicitudCertificacionDocumentoUpdate);
        Task<IPaginatedQueryResult<Certificacion>> GetCertificacionesAsync(ICertificacionQueryParameter request);
        Task<IPaginatedQueryResult<SolicitudCertificacion>> GetSolicitudesAsync(ISolicitudCertificacionQueryParameter request);
        Task<Certificacion> CreateAsync(ICertificacionCreate t);
        Task<SolicitudCertificacion> CreateSolicitudAsync(ISolicitudCertificacionCreate c);
        Task UpdateAsync(ICertificacionUpdate p);
        Task UpdateSolicitudAsync(int id, ISolicitudCertificacionUpdate solicitud);
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

    public interface ISolicitudCertificacionAnalysisParameters
    {
        public int Id { get; set; }
        public int SolicitudId { get; set; }
        public int DocumentoRequeridoId { get; set; }
        public string ArchivoURL { get; set; }
        public long? ValidadoPorId { get; set; }
        public User ValidadoPor { get; set; }
        public DateTime? FechaSubida { get; set; }
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
        string Observaciones { get; set; }
        short OrigenId { get; set; }
        short PropietarioId { get; set; }
    }

    /// <summary>
    /// Define un contrato para actualizar un Certificacion.
    /// </summary>
    public interface ICertificacionUpdate
    {
        // Agregar propiedades de modificación
    }

    public interface ISolicitudCertificacionUpdate
    {
        short? EstadoId { get; set; }
        string Observaciones { get; set; }
        DateTime? FechaSolicitud { get; set; }
        DateTime? UltimaModificacionEstado { get; set; }
        DateTime? VigenciaDesde { get; set; }
        DateTime? VigenciaHasta { get; set; }
        short? PropietarioId { get; set; }
    }

    public interface ISolicitudCertificacionDocumentoCreate
    {
        int SolicitudId { get; set; }
        //string Observaciones { get; set; }
        int? Version { get; set; }
        DateTime? FechaDesde { get; set; }
        DateTime? FechaHasta { get; set; }
        int DocumentoRequeridoId { get; set; }
    }

    public interface ISolicitudCertificacionDocumentoUpdate
    {
        string ArchivoURL { get; set; }
        string Observaciones { get; set; }
        int? Version { get; set; }
        DateTime? FechaDesde { get; set; }
        DateTime? FechaHasta { get; set; }
        short? EstadoId { get; set; }
        long? ValidadoPorId { get; set; }
        DateTime? FechaSubida { get; set; }
        byte[] RowVersion { get; set; }
    }

    public class AnalysisCompletionData
    {
        public int DocumentoId { get; init; }
        public string OperationId { get; init; }
        public Domain.Commons.Enums.OperationStatus FinalStatus { get; init; }
        public IEnumerable<DocumentAnalysisResult>? Results { get; init; } // Null si falló
        public Exception? Exception { get; init; } // Null si tuvo éxito
        public CancellationToken OriginalCancellationToken { get; init; }
    }

    // Firma del Callback
    // El callback recibe los datos de completación y el DbContext del scope creado en Task.Run
    public delegate Task AnalysisCompletedCallback(
        AnalysisCompletionData completionData,
        ICertificationsDbContext dbContextForCallback // El servicio proveerá este contexto
    );
}
