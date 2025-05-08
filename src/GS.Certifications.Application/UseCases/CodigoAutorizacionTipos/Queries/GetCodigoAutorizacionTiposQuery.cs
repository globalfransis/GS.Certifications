using AutoMapper;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.CodigoAutorizacionTipos.Dto;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using Microsoft.EntityFrameworkCore;
using GS.Certifications.Domain.Entities.Comprobantes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.CodigoAutorizacionTipos.Queries;

public class GetCodigoAutorizacionTiposQuery : IRequest<List<CodigoAutorizacionTipoDto>>
{
}

// Handler definition for paginated query without a service
public class GetCodigoAutorizacionTiposQueryHandler : BaseRequestHandler<List<CodigoAutorizacionTipo>, GetCodigoAutorizacionTiposQuery, List<CodigoAutorizacionTipoDto>>
{

    // IMPORTANT: use the corresponding DbContext interface
    private readonly ICertificationsDbContext Context;

    public GetCodigoAutorizacionTiposQueryHandler(ICertificationsDbContext context, IMapper mapper) : base(mapper)
    {
        Context = context;
    }

    protected override async Task<List<CodigoAutorizacionTipo>> HandleRequestAsync(GetCodigoAutorizacionTiposQuery request, CancellationToken cancellationToken)
    {
        return await Context.CodigoAutorizacionTipos.ToListAsync(cancellationToken);
    }
}