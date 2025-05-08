using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.CQRS.DbContexts;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using Microsoft.EntityFrameworkCore;
using GS.Certifications.Domain.Entities.Comprobantes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.CategoriaTipos.Queries;

public class GetCategoriaTiposQuery : IRequest<List<CategoriaTipoDto>>
{

    // Handler definition for paginated query without a service
    public class GetCategoriaTiposQueryHandler : BaseRequestHandler<List<CategoriaTipo>, GetCategoriaTiposQuery, List<CategoriaTipoDto>>
    {

        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext Context;

        public GetCategoriaTiposQueryHandler(ICertificationsDbContext context, IMapper mapper) : base(mapper)
        {
            Context = context;
        }

        protected override async Task<List<CategoriaTipo>> HandleRequestAsync(GetCategoriaTiposQuery request, CancellationToken cancellationToken)
        {
            return await Context.CategoriaTipos.ToListAsync(cancellationToken);
        }
    }
}