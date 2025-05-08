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

public class GetCondicionVentasQuery : IRequest<List<CondicionVentaDto>>
{
}

public class GetCondicionVentasQueryHandler : BaseRequestHandler<List<CondicionVenta>, GetCondicionVentasQuery, List<CondicionVentaDto>>
{

    private readonly ICertificationsDbContext Context;

    public GetCondicionVentasQueryHandler(ICertificationsDbContext context, IMapper mapper) : base(mapper)
    {
        Context = context;
    }

    protected override async Task<List<CondicionVenta>> HandleRequestAsync(GetCondicionVentasQuery request, CancellationToken cancellationToken)
    {
        return await Context.CondicionVentas.ToListAsync(cancellationToken);
    }
}

