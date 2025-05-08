using AutoMapper;
using GSF.Application.Common.Mappings;
using GS.Certifications.Domain.Entities.Comprobantes;

namespace GS.Certifications.Application.UseCases.UnidadMedidas.Dto;

public class UnidadMedidaDto : IMapFrom<UnidadMedida>
{
    public short Idm { get; set; }
    public string CodigoAFIP { get; set; }
    public string Descripcion { get; set; }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UnidadMedida, UnidadMedidaDto>();
        }
    }
}
