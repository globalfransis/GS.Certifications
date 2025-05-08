using AutoMapper;
using GS.Certifications.Application.Commons.Dtos;
using GS.Certifications.Application.CQRS.DbContexts;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using Microsoft.EntityFrameworkCore;
using GS.Certifications.Domain.Entities.Percepciones;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Percepciones.Queries;

public class GetPercepcionesQuery : IRequest<List<PercepcionDto>>
{
    public long CompanyId { get; set; }
}

public class GetPercepcionesQueryHandler : BaseRequestHandler<List<Percepcion>, GetPercepcionesQuery, List<PercepcionDto>>
{

    private readonly ICertificationsDbContext Context;

    public GetPercepcionesQueryHandler(ICertificationsDbContext context, IMapper mapper) : base(mapper)
    {
        Context = context;
    }

    protected override async Task<List<Percepcion>> HandleRequestAsync(GetPercepcionesQuery request, CancellationToken cancellationToken)
    {
        return await Context.Percepciones.Where(p => p.CompanyId == request.CompanyId).ToListAsync(cancellationToken);
    }
}