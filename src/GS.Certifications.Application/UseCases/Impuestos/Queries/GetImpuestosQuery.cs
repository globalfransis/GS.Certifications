using AutoMapper;
using GS.Certifications.Application.Commons.Dtos;
using GS.Certifications.Application.CQRS.DbContexts;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using Microsoft.EntityFrameworkCore;
using GS.Certifications.Domain.Entities.Impuestos;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Impuestos.Queries
{
    public class GetImpuestosQuery : IRequest<List<ImpuestoDto>>
    {
        public long CompanyId { get; set; }
        public bool? IVA { get; set; } = false;
    }

    public class GetImpuestosQueryHandler : BaseRequestHandler<List<Impuesto>, GetImpuestosQuery, List<ImpuestoDto>>
    {

        private readonly ICertificationsDbContext Context;

        public GetImpuestosQueryHandler(ICertificationsDbContext context, IMapper mapper) : base(mapper)
        {
            Context = context;
        }

        protected override async Task<List<Impuesto>> HandleRequestAsync(GetImpuestosQuery request, CancellationToken cancellationToken)
        {
            var queryable = Context.Impuestos
                .Where(i => i.CompanyId == request.CompanyId);

            if (request.IVA is true)
            {
                queryable = queryable.Where(i => i.TipoId == ImpuestoTipo.IVA);
            }

            return await queryable
                .Include(i => i.Alicuota)
                .ToListAsync(cancellationToken);
        }
    }
}
