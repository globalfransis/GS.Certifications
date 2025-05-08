using AutoMapper;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Dto;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Extensions.GSFMediatR.Pagination.Request;
using GSF.Application.Helpers.Pagination.Interfaces;
using GS.Certifications.Domain.Entities.Comprobantes;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Queries;

public class GetComprobantesCompanyQuery : BaseQueryWithPagination<ComprobanteDto>, IComprobanteQueryParameter
{
    public string NroIdentificacionFiscalPro { get; set; }
    public short? CategoriaTipoEmisorId { get; set; }
    public short? ComprobanteTipoId { get; set; }
    public short? CategoriaTipoReceptorId { get; set; }
    public int? PuntoVenta { get; set; }
    public int? Numero { get; set; }
    public DateTime? FechaEmisionDesde { get; set; }
    public DateTime? FechaEmisionHasta { get; set; }
    public DateTime? FechaRegistracionDesde { get; set; }
    public DateTime? FechaRegistracionHasta { get; set; }
    public short? TipoCodigoAutorizacionId { get; set; }
    public int? EmpresaId { get; set; }
    public long? CompanyId { get; set; }
    public int? ComprobanteEstadoId { get; set; }
    public bool? InMemory { get; set; }
}

public class GetComprobantesCompanyQueryHandler : BaseRequestHandler<IPaginatedQueryResult<Comprobante>, GetComprobantesCompanyQuery, IPaginatedQueryResult<ComprobanteDto>>
{
    private readonly IComprobanteService service;

    public GetComprobantesCompanyQueryHandler(IComprobanteService service, IMapper mapper) : base(mapper)
    {
        this.service = service;
    }

    protected override async Task<IPaginatedQueryResult<Comprobante>> HandleRequestAsync(GetComprobantesCompanyQuery request, CancellationToken cancellationToken)
    {
        return await service.GetManyAsync(request);
    }
}