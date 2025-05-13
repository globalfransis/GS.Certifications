using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Exceptions;
using GS.Certifications.Domain.Entities.Certificaciones;
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

        public async Task<Certificacion> GetAsync(int id)
        {
            var queryable = getCertificacionesQueryable();
            return await queryable.FirstOrDefaultAsync(c => c.Id == id) ?? throw new CertificacionInexistenteException(); ;
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
                .AsQueryable();
            return queryable;
        }
        #endregion
    }
}
