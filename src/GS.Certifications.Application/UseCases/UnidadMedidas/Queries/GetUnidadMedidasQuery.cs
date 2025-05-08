using AutoMapper;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.UnidadMedidas.Dto;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using Microsoft.EntityFrameworkCore;
using GS.Certifications.Domain.Entities.Comprobantes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.UnidadMedidas.Queries;

public class GetUnidadMedidasQuery : IRequest<List<UnidadMedidaDto>>
{
}

// Handler definition for paginated query without a service
public class GetUnidadMedidasQueryHandler : BaseRequestHandler<List<UnidadMedida>, GetUnidadMedidasQuery, List<UnidadMedidaDto>>
{

    // IMPORTANT: use the corresponding DbContext interface
    private readonly ICertificationsDbContext Context;

    public GetUnidadMedidasQueryHandler(ICertificationsDbContext context, IMapper mapper) : base(mapper)
    {
        Context = context;
    }

    protected override async Task<List<UnidadMedida>> HandleRequestAsync(GetUnidadMedidasQuery request, CancellationToken cancellationToken)
    {
        return await Context.UnidadMedidas.ToListAsync(cancellationToken);
    }
}
