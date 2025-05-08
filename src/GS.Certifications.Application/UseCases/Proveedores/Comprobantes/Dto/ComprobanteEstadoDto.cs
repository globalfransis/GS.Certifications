using AutoMapper;
using GSF.Application.Common.Mappings;
using GS.Certifications.Domain.Entities.Comprobantes;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Dto;

public class ComprobanteEstadoDto : IMapFrom<ComprobanteEstado>
{
    public short Idm { get; set; }
    public string Descripcion { get; set; }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ComprobanteEstado, ComprobanteEstadoDto>();
        }
    }
}