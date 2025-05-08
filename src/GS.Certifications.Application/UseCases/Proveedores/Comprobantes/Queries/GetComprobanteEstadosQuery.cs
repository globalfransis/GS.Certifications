using AutoMapper;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Dto;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using Microsoft.EntityFrameworkCore;
using GS.Certifications.Domain.Entities.Comprobantes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Queries;

public class GetComprobanteEstadosQuery : IRequest<List<ComprobanteEstadoDto>>
{
}

public class GetComprobanteEstadosQueryHandler : BaseRequestHandler<List<ComprobanteEstado>, GetComprobanteEstadosQuery, List<ComprobanteEstadoDto>>
{

    // IMPORTANT: use the corresponding DbContext interface
    private readonly ICertificationsDbContext Context;

    public GetComprobanteEstadosQueryHandler(ICertificationsDbContext context, IMapper mapper) : base(mapper)
    {
        Context = context;
    }

    protected override async Task<List<ComprobanteEstado>> HandleRequestAsync(GetComprobanteEstadosQuery request, CancellationToken cancellationToken)
    {
        return await Context.ComprobanteEstados.ToListAsync(cancellationToken);
    }
}
