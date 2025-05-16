using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Exceptions;
using GS.Certifications.Domain.Entities.Certificaciones;
using GS.Certifications.Domain.Entities.Certificaciones.Documentos;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Socios.Certificaciones.Services
{
    public class CertificacionService : BaseGSFService, ICertificacionService
    {
        private readonly ICertificationsDbContext context;

        public CertificacionService(ICertificationsDbContext context)
        {
            this.context = context;
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

            documento.ArchivoURL = solicitudCertificacionDocumentoUpdate.ArchivoURL;
            documento.Observaciones = solicitudCertificacionDocumentoUpdate.Observaciones;
            documento.Version = solicitudCertificacionDocumentoUpdate.Version;
            documento.FechaDesde = solicitudCertificacionDocumentoUpdate.FechaDesde;
            documento.FechaHasta = solicitudCertificacionDocumentoUpdate.FechaHasta;
            documento.RowVersion = solicitudCertificacionDocumentoUpdate.RowVersion;
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
            var nueva = new SolicitudCertificacion()
            {
                SocioId = c.SocioId,
                CertificacionId = c.CertificacionId,
                EstadoId = c.EstadoId ?? SolicitudCertificacionEstado.BORRADOR,
                CantidadAprobaciones = c.CantidadAprobaciones,
                Observaciones = c.Observaciones,
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
                    DocumentoRequeridoId = d.Id
                })
                .ToList();

            return nueva;
        }

        public async Task UpdateAsync(ICertificacionUpdate e)
        {
            throw new NotImplementedException();
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
        #endregion
    }
}
