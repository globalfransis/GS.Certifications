using AutoMapper;
using GSF.Application.Common.Mappings;
using GS.Certifications.Domain.Entities.Percepciones;

namespace GS.Certifications.Application.Commons.Dtos.Percepciones
{
    public class PercepcionForListDto : IMapFrom<Percepcion>
    {
        public short Id { get; set; }
        public string Descripcion { get; set; }
        public string PercepcionTipo { get; set; }
        public string Provincia { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Percepcion, PercepcionForListDto>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dst => dst.Provincia, opt => opt.MapFrom(src => src.Provincia.Name))
                .ForMember(dst => dst.PercepcionTipo, opt => opt.MapFrom(src => src.PercepcionTipo.Descripcion));
        }
    }
}
