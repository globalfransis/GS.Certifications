using AutoMapper;
using GSF.Application.Common.Mappings;
using GS.Certifications.Domain.Entities.Comprobantes;

namespace GS.Certifications.Application.UseCases.CodigoAutorizacionTipos.Dto;

public class CodigoAutorizacionTipoDto : IMapFrom<CodigoAutorizacionTipo>
{
    public short Idm { get; set; }
    public string Descripcion { get; set; }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CodigoAutorizacionTipo, CodigoAutorizacionTipoDto>();
        }
    }
}
