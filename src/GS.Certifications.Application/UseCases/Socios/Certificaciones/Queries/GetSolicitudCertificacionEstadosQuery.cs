using AutoMapper;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Dto;
using GS.Certifications.Domain.Entities.Certificaciones;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Socios.Certificaciones.Queries
{
    public class GetSolicitudCertificacionEstadosQuery : IRequest<List<SolicitudCertificacionEstadoDto>>
    {
    }

    public class GetSolicitudCertificacionEstadosQueryHandler : BaseRequestHandler<List<SolicitudCertificacionEstado>, GetSolicitudCertificacionEstadosQuery, List<SolicitudCertificacionEstadoDto>>
    {
        private readonly ICertificationsDbContext Context;

        public GetSolicitudCertificacionEstadosQueryHandler(ICertificationsDbContext context, IMapper mapper) : base(mapper)
        {
            Context = context;
        }

        protected override async Task<List<SolicitudCertificacionEstado>> HandleRequestAsync(GetSolicitudCertificacionEstadosQuery request, CancellationToken cancellationToken)
        {
            return await Context.SolicitudCertificacionEstados.ToListAsync(cancellationToken);
        }
    }

}
