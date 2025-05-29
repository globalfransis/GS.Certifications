using GS.AI.DocumentIntelligence.Legacy.Models;
using GS.AI.DocumentIntelligence.Legacy.Services.Interfaces;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Exceptions;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Helpers;
using GS.Certifications.Domain.Entities.Certificaciones;
using GS.Certifications.Domain.Entities.Certificaciones.Documentos;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Socios.Certificaciones.Services
{
    public class CertificacionService : BaseGSFService, ICertificacionService
    {
        private readonly ICertificationsDbContext context;
        private readonly IDocumentAnalysisService documentAnalysisService;
        private readonly IServiceScopeFactory serviceScopeFactory;
        public CertificacionService(ICertificationsDbContext context, IDocumentAnalysisService documentAnalysisService, IServiceScopeFactory serviceScopeFactory)
        {
            this.context = context;
            this.documentAnalysisService = documentAnalysisService;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<SolicitudCertificacion> GetSolicitudAsync(int id)
        {
            var queryable = getSolicitudesQueryable();
            return await queryable.FirstOrDefaultAsync(c => c.Id == id) ?? throw new SolicitudCertificacionInexistenteException();
        }


        public async Task<DocumentoCargado> GetDocumentoAsync(int id)
        {
            var queryable = getDocumentosCargadosQueryable();
            return await queryable.FirstOrDefaultAsync(c => c.Id == id) ?? throw new SolicitudCertificacionInexistenteException();
        }

        public async Task<string> AnalyzeDocumentoAsync(
            BinaryData bytesSource,
            IDocumentoSolicitudCertificacionAnalysisParameter parameters,
            AnalysisCompletedCallback onCompletedCallback,
            CancellationToken cancellationToken)
        {
            var analysisOperation = await documentAnalysisService.StartAnalyzeAsync(bytesSource, model: Model.Custom, cancellationToken: cancellationToken);
            string analysisOperationId = analysisOperation.Id;

            //var documento = await GetDocumentoAsync(parameters.Id);
            //documento.OperationId = analysisOperationId;
            //documento.OperationStatus = Domain.Commons.Enums.OperationStatus.PROCESSING;

            await context.SaveChangesAsync(cancellationToken);

            _ = Task.Run(async () =>
            {
                IEnumerable<DocumentAnalysisResult>? completionResults = null;
                Exception? capturedException = null;
                Domain.Commons.Enums.OperationStatus finalStatus;

                try
                {
                    completionResults = await analysisOperation.WaitForCompletionAsync(cancellationToken);
                    finalStatus = Domain.Commons.Enums.OperationStatus.COMPLETED;
                }
                catch (Exception ex)
                {
                    capturedException = ex;
                    finalStatus = Domain.Commons.Enums.OperationStatus.FAILED;
                    // se puede loggear la excepción si es necesario, o guardar algun detalle del error
                }

                var result = completionResults?.FirstOrDefault();

                DateTime? fechaDesde = null;
                DateTime? fechaHasta = null;

                if (result != null)
                {
                    var fechaDesdeField = result.ExtractedFields.GetValueOrDefault("VigenciaDesde");
                    fechaDesde = fechaDesdeField.ValueDate?.DateTime;
                
                    if (fechaDesde == null)
                    {
                        var fechaDesdeDesc = result.ExtractedFields.GetValueOrDefault("VigenciaDesdeDescripcion")?.Content;
                        fechaDesde = DocumentAnalysisHelper.TryParseDateInternal(fechaDesdeDesc);
                    }

                    var fechaHastaField = result.ExtractedFields.GetValueOrDefault("VigenciaHasta");
                    fechaHasta = fechaHastaField.ValueDate?.DateTime;
                    
                    if (fechaHasta == null)
                    {
                        var fechaHastaDesc = result.ExtractedFields.GetValueOrDefault("VigenciaHastaDescripcion")?.Content;
                        fechaHasta = DocumentAnalysisHelper.TryParseDateInternal(fechaHastaDesc);
                    }
                }

                var analysisResult = new DocumentoAnalysisResult()
                {
                    FechaDesde = fechaDesde,
                    FechaHasta = fechaHasta,
                };

                var completionData = new AnalysisCompletionData
                {
                    DocumentoId = parameters.Id,
                    OperationId = analysisOperationId,
                    FinalStatus = finalStatus,
                    //Results = completionResults,
                    Result = analysisResult,
                    Exception = capturedException,
                    OriginalCancellationToken = cancellationToken
                };

                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var dbContextForCallback = scope.ServiceProvider.GetRequiredService<ICertificationsDbContext>();
                    try
                    {
                        Console.WriteLine($"=== EJECUTANDO CALLBACK onCompletedCallback");

                        await onCompletedCallback(completionData, dbContextForCallback);
                        // el callback es responsable de la lógica de actualización de la entidad
                        // y de llamar a dbContextForCallback.SaveChangesAsync()
                    }
                    catch (Exception callbackEx)
                    {
                        Console.WriteLine($"Error en el callback de AnalyzeDocumentoAsync: {callbackEx.Message}");
                    }
                }
            }, cancellationToken);

            return analysisOperationId;
        }

        public async Task<Certificacion> GetAsync(int id)
        {
            var queryable = getCertificacionesQueryable();
            return await queryable.FirstOrDefaultAsync(c => c.Id == id) ?? throw new CertificacionInexistenteException();
        }

        public async Task<IPaginatedQueryResult<Certificacion>> GetCertificacionesAsync(ICertificacionQueryParameter parameters)
        {
            var certificacionesQueryable = getCertificacionesQueryable();

            if (parameters.TipoSocioId is not null)
            {
                certificacionesQueryable = certificacionesQueryable.Where(c => c.TipoEmpresaPortalId == parameters.TipoSocioId);
            }

            if (!string.IsNullOrEmpty(parameters.Nombre))
            {
                certificacionesQueryable = certificacionesQueryable.Where(c => c.Nombre.Contains(parameters.Nombre));
            }

            if (parameters.Activa is not null)
            {
                certificacionesQueryable = certificacionesQueryable.Where(c => c.Activa == parameters.Activa);
            }

            if (parameters.EstadoId is not null)
            {
                certificacionesQueryable = certificacionesQueryable.Where(c => c.Solicitudes.Any(s => s.EstadoId == parameters.EstadoId));
            }

            if (parameters.SocioId is not null)
            {
                certificacionesQueryable = certificacionesQueryable.Where(c => c.Solicitudes.Any(s => s.SocioId == parameters.SocioId));
            }

            return await Get(certificacionesQueryable, parameters);
        }

        public async Task<IPaginatedQueryResult<SolicitudCertificacion>> GetSolicitudesAsync(ISolicitudCertificacionQueryParameter parameters)
        {
            var solicitudesQueryable = getSolicitudesQueryable();

            if (parameters.CertificacionId is not null)
            {
                solicitudesQueryable = solicitudesQueryable.Where(c => c.CertificacionId == parameters.CertificacionId);
            }

            if (parameters.TipoSocioId is not null)
            {
                solicitudesQueryable = solicitudesQueryable.Where(c => c.Certificacion.TipoEmpresaPortalId == parameters.TipoSocioId);
            }

            if (!string.IsNullOrEmpty(parameters.Nombre))
            {
                solicitudesQueryable = solicitudesQueryable.Where(c => c.Certificacion.Nombre.Contains(parameters.Nombre));
            }

            if (parameters.Activa is not null)
            {
                solicitudesQueryable = solicitudesQueryable.Where(c => c.Certificacion.Activa == parameters.Activa);
            }

            if (parameters.EstadoId is not null)
            {
                solicitudesQueryable = solicitudesQueryable.Where(s => s.EstadoId == parameters.EstadoId);
            }

            if (parameters.SocioId is not null)
            {
                solicitudesQueryable = solicitudesQueryable.Where(s => s.SocioId == parameters.SocioId);
            }

            return await Get(solicitudesQueryable, parameters);
        }

        public Task<Certificacion> CreateAsync(ICertificacionCreate c)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteSolicitudAsync(int id, byte[] rowVersion)
        {
            var solicitud = await GetSolicitudAsync(id);
            solicitud.RowVersion = rowVersion;

            foreach (var item in solicitud.DocumentosCargados)
            {
                context.DocumentoCargados.Remove(item);
            }

            context.SolicitudCertificaciones.Remove(solicitud);
        }


        public async Task DeleteDocumentoSolicitudAsync(int id, byte[] rowVersion)
        {
            var documento = await GetDocumentoAsync(id);
            documento.RowVersion = rowVersion;

            context.DocumentoCargados.Remove(documento);
        }

        public async Task UpdateDocumentoAsync(int id, ISolicitudCertificacionDocumentoUpdate solicitudCertificacionDocumentoUpdate)
        {
            var documento = await GetDocumentoAsync(id);
            var hasBeenUpdated = false;

            if (!string.IsNullOrEmpty(solicitudCertificacionDocumentoUpdate.ArchivoURL))
            {
                documento.ArchivoURL = solicitudCertificacionDocumentoUpdate.ArchivoURL;
                hasBeenUpdated = true;
            }

            if (!string.IsNullOrEmpty(solicitudCertificacionDocumentoUpdate.Observaciones))
            {
                documento.Observaciones = solicitudCertificacionDocumentoUpdate.Observaciones;
                hasBeenUpdated = true;
            }

            if (solicitudCertificacionDocumentoUpdate.Version is not null)
            {
                documento.Version = solicitudCertificacionDocumentoUpdate.Version;
                hasBeenUpdated = true;
            }

            if (solicitudCertificacionDocumentoUpdate.FechaDesde is not null)
            {
                documento.FechaDesde = solicitudCertificacionDocumentoUpdate.FechaDesde;
                hasBeenUpdated = true;
            }

            if (solicitudCertificacionDocumentoUpdate.FechaHasta is not null)
            {
                documento.FechaHasta = solicitudCertificacionDocumentoUpdate.FechaHasta;
                hasBeenUpdated = true;
            }

            if (solicitudCertificacionDocumentoUpdate.ValidadoPorId is not null)
            {
                documento.ValidadoPorId = solicitudCertificacionDocumentoUpdate.ValidadoPorId;
                hasBeenUpdated = true;
            }

            if (solicitudCertificacionDocumentoUpdate.EstadoId is not null)
            {
                ValidarCambioEstadoDocumento(solicitudCertificacionDocumentoUpdate, documento);
                documento.EstadoId = (short)solicitudCertificacionDocumentoUpdate.EstadoId;
                hasBeenUpdated = true;
            }

            if (hasBeenUpdated)
            {
                documento.RowVersion = solicitudCertificacionDocumentoUpdate.RowVersion;
            }
        }

        public async Task UpdateDocumentoDraftAsync(int id, ISolicitudCertificacionDocumentoUpdate solicitudCertificacionDocumentoUpdate)
        {
            var documento = await GetDocumentoAsync(id);

            documento.ArchivoURL = solicitudCertificacionDocumentoUpdate.ArchivoURL;
            documento.Observaciones = solicitudCertificacionDocumentoUpdate.Observaciones;
            documento.Version = solicitudCertificacionDocumentoUpdate.Version;
            documento.FechaDesde = solicitudCertificacionDocumentoUpdate.FechaDesde;
            documento.FechaHasta = solicitudCertificacionDocumentoUpdate.FechaHasta;
            documento.RowVersion = solicitudCertificacionDocumentoUpdate.RowVersion;
        }

        public async Task<SolicitudCertificacion> CreateSolicitudAsync(ISolicitudCertificacionCreate c)
        {

            var yaExisteSolicitud = await getSolicitudesQueryable().Where(s => s.CertificacionId == c.CertificacionId && s.SocioId == c.SocioId).AnyAsync();

            if (yaExisteSolicitud)
            {
                throw new YaExisteSolicitudCertificacionException();
            }

            var nueva = new SolicitudCertificacion()
            {
                SocioId = c.SocioId,
                CertificacionId = c.CertificacionId,
                EstadoId = c.EstadoId ?? SolicitudCertificacionEstado.BORRADOR,
                Observaciones = c.Observaciones,
                FechaSolicitud = DateTime.Now,
                OrigenId = c.OrigenId,
                PropietarioActualId = c.PropietarioId
            };

            var certificacion = await GetAsync(c.CertificacionId);

            nueva.DocumentosCargados = certificacion.DocumentosRequeridos
                .Select(
                d => new DocumentoCargado()
                {
                    Solicitud = nueva,
                    DocumentoRequerido = d,
                    ArchivoURL = string.Empty,
                    EstadoId = DocumentoEstado.PENDIENTE,
                    DocumentoRequeridoId = d.Id,
                    Version = 1
                })
                .ToList();

            return nueva;
        }

        public async Task UpdateAsync(ICertificacionUpdate e)
        {
            throw new NotImplementedException();
        }

        public DocumentoCargado CreateDocumento(ISolicitudCertificacionDocumentoCreate d)
        {
            var nuevo = new DocumentoCargado()
            {
                SolicitudId = d.SolicitudId,
                ArchivoURL = string.Empty,
                EstadoId = DocumentoEstado.PENDIENTE,
                DocumentoRequeridoId = d.DocumentoRequeridoId,
                Version = d.Version ?? 1
            };
            return nuevo;
        }


        public async Task UpdateSolicitudAsync(int id, ISolicitudCertificacionUpdate solicitud)
        {
            var solicitudToUpdate = await GetSolicitudAsync(id);

            if (solicitud.EstadoId is not null)
            {
                ValidarCambioEstado(solicitud, solicitudToUpdate);
                solicitudToUpdate.EstadoId = (short)solicitud.EstadoId;
            }

            if (solicitud.PropietarioId is not null)
            {
                solicitudToUpdate.PropietarioActualId = (short)solicitud.PropietarioId;
            }

            if (!string.IsNullOrEmpty(solicitud.Observaciones))
            {
                solicitudToUpdate.Observaciones = solicitud.Observaciones;
            }

            if (solicitud.FechaSolicitud is not null)
            {
                solicitudToUpdate.FechaSolicitud = solicitud.FechaSolicitud;
            }

            if (solicitud.UltimaModificacionEstado is not null)
            {
                solicitudToUpdate.UltimaModificacionEstado = solicitud.UltimaModificacionEstado;
            }

            if (solicitud.VigenciaDesde is not null)
            {
                solicitudToUpdate.VigenciaDesde = solicitud.VigenciaDesde;
            }

            if (solicitud.VigenciaHasta is not null)
            {
                solicitudToUpdate.VigenciaHasta = solicitud.VigenciaHasta;
            }

        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        #region Private
        private IQueryable<Certificacion> getCertificacionesQueryable()
        {
            var queryable = context.Certificaciones
                .Include(c => c.TipoEmpresaPortal)
                .Include(c => c.Solicitudes)
                    .ThenInclude(s => s.Socio)
                .Include(c => c.Solicitudes)
                    .ThenInclude(s => s.Estado)
                .Include(c => c.Solicitudes)
                    .ThenInclude(s => s.Socio)
                .Include(c => c.DocumentosRequeridos)
                    .ThenInclude(s => s.Tipo)
                .AsQueryable();
            return queryable;
        }

        private IQueryable<SolicitudCertificacion> getSolicitudesQueryable()
        {
            var queryable = context.SolicitudCertificaciones
                .Include(c => c.Socio)
                .Include(c => c.Certificacion)
                    .ThenInclude(s => s.TipoEmpresaPortal)
                .Include(c => c.Estado)
                .Include(c => c.DocumentosCargados)
                    .ThenInclude(s => s.DocumentoRequerido)
                        .ThenInclude(s => s.Tipo)
                .Include(c => c.DocumentosCargados)
                    .ThenInclude(s => s.Estado)
                .Include(c => c.DocumentosCargados)
                    .ThenInclude(s => s.ValidadoPor)
                .AsQueryable();
            return queryable;
        }

        private IQueryable<DocumentoCargado> getDocumentosCargadosQueryable()
        {
            var queryable = context.DocumentoCargados
                .Include(c => c.Solicitud)
                    .ThenInclude(s => s.Certificacion)
                        .ThenInclude(s => s.TipoEmpresaPortal)
                .Include(c => c.Solicitud)
                    .ThenInclude(s => s.Estado)
                .Include(c => c.Solicitud)
                    .ThenInclude(s => s.Socio)
                .Include(c => c.DocumentoRequerido)
                    .ThenInclude(s => s.Certificacion)
                .Include(c => c.DocumentoRequerido)
                    .ThenInclude(s => s.Tipo)
                .Include(c => c.Estado)
                .Include(c => c.ValidadoPor)
                .AsQueryable();
            return queryable;
        }

        private static void ValidarCambioEstado(ISolicitudCertificacionUpdate solicitud, SolicitudCertificacion solicitudToUpdate)
        {
            if (solicitud.EstadoId == SolicitudCertificacionEstado.PRESENTADA)
            {
                var documentosAgrupadosPorRequerido = solicitudToUpdate.DocumentosCargados
                    .GroupBy(dc => dc.DocumentoRequeridoId);

                foreach (var grupo in documentosAgrupadosPorRequerido)
                {
                    var ultimaVersionDocumentoCargado = grupo
                        .OrderByDescending(dc => dc.Version)
                        .FirstOrDefault();

                    if (ultimaVersionDocumentoCargado != null && ultimaVersionDocumentoCargado.EstadoId != DocumentoEstado.PRESENTADO)
                    {
                        throw new PresentacionSolicitudDocumentosInvalidosException();
                    }
                }
            }
            if (solicitud.EstadoId == SolicitudCertificacionEstado.APROBADA)
            {
                if (!solicitudToUpdate.DocumentosCargados.All(d => d.EstadoId == DocumentoEstado.VALIDADO)) throw new AprobacionSolicitudDocumentosInvalidosException();

                if (solicitud.VigenciaDesde is null || solicitud.VigenciaHasta is null)
                {
                    throw new SolicitudVigenciaNulaException();
                }

                if (solicitud.VigenciaDesde > solicitud.VigenciaHasta)
                {
                    throw new SolicitudVigenciaNulaException();
                }
            }
        }


        private static void ValidarCambioEstadoDocumento(ISolicitudCertificacionDocumentoUpdate solicitudCertificacionDocumentoUpdate, DocumentoCargado documento)
        {
            if (solicitudCertificacionDocumentoUpdate.EstadoId == DocumentoEstado.VALIDADO)
            {
                if (documento.FechaDesde is null || documento.FechaHasta is null)
                {
                    throw new DocumentoVigenciaNulaException();
                }

                if (documento.FechaDesde > documento.FechaHasta)
                {
                    throw new DocumentoVigenciaNulaException();
                }
            }
        }
        #endregion

        #region Classes
        public class DocumentoAnalysisResult : IDocumentoAnalysisResult
        {
            public DateTime? FechaDesde { get; set; }
            public DateTime? FechaHasta { get; set; }
        }

        public class SolicitudCertificacionUpdate : ISolicitudCertificacionUpdate
        {
            public short? EstadoId { get; set; }
            public string Observaciones { get; set; }
            public DateTime? FechaSolicitud { get; set; }
            public DateTime? UltimaModificacionEstado { get; set; }
            public DateTime? VigenciaDesde { get; set; }
            public DateTime? VigenciaHasta { get; set; }
            public short? PropietarioId { get; set; }
        }

        public class SolicitudCertificacionDocumentoUpdate : ISolicitudCertificacionDocumentoUpdate
        {
            public string ArchivoURL { get; set; }
            public string Observaciones { get; set; }
            public int? Version { get; set; }
            public DateTime? FechaDesde { get; set; }
            public DateTime? FechaHasta { get; set; }
            public short? EstadoId { get; set; }
            public long? ValidadoPorId { get; set; }
            public DateTime? FechaSubida { get; set; }
            public byte[] RowVersion { get; set; }
        }

        public class SolicitudCertificacionDocumentoCreate : ISolicitudCertificacionDocumentoCreate
        {
            public int SolicitudId { get; set; }
            //public string Observaciones { get; set; }
            public int? Version { get; set; }
            public DateTime? FechaDesde { get; set; }
            public DateTime? FechaHasta { get; set; }
            public int DocumentoRequeridoId { get; set; }
        }
        #endregion

    }
}
