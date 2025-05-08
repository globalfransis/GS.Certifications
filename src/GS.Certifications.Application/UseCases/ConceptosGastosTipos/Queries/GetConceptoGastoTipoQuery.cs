using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.ConceptosGastosTipos;
using GS.Certifications.Application.UseCases.ConceptosGastosTipos.Services;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using GS.Certifications.Domain.Entities.ConceptosGastosTipos;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.ConceptosGastosTipos.Queries;

// Query definition
public class GetConceptoGastoTipoQuery : IRequest<ConceptoGastoTipoDto>
{
    public int Id { get; set; }
}

// Query handler definition
public class GetConceptoGastoTipoQueryHandler : BaseRequestHandler
    <ConceptoGastoTipo, GetConceptoGastoTipoQuery, ConceptoGastoTipoDto>
{
    private readonly IConceptoGastoTipoService _service;

    public GetConceptoGastoTipoQueryHandler(IConceptoGastoTipoService service, IMapper mapper) : base(mapper)
    {
        _service = service;
    }

    protected override async Task<ConceptoGastoTipo> HandleRequestAsync(GetConceptoGastoTipoQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAsync(request.Id);
    }
}
