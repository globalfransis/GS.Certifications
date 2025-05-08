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

// Query definition with pagination, without direct service dependency
public class GetComprobanteTiposQuery : IRequest<List<ComprobanteTipoDto>>
{
}

// Handler definition for paginated query without a service
public class GetComprobanteTiposQueryHandler : BaseRequestHandler<List<ComprobanteTipo>, GetComprobanteTiposQuery, List<ComprobanteTipoDto>>
{

    // IMPORTANT: use the corresponding DbContext interface
    private readonly ICertificationsDbContext Context;

    public GetComprobanteTiposQueryHandler(ICertificationsDbContext context, IMapper mapper) : base(mapper)
    {
        Context = context;
    }

    protected override async Task<List<ComprobanteTipo>> HandleRequestAsync(GetComprobanteTiposQuery request, CancellationToken cancellationToken)
    {
        return await Context.ComprobanteTipos.ToListAsync(cancellationToken);
    }
}
