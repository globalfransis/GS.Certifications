using AutoMapper;
using GSF.Application.Common.Mappings;
using GS.Certifications.Domain.Entities.Periodos;

namespace GS.Certifications.Application.Commons.Dtos.Periodos
{
    public class EstadoPeriodoDto : IMapFrom<EstadoPeriodo>
    {
        public short Idm { get; set; }
        public string Descripcion { get; set; }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<EstadoPeriodo, EstadoPeriodoDto>()
                    .ForMember(dest => dest.Idm, opt => opt.MapFrom(src => src.Idm))
                    .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion));
            }
        }
    }
}
