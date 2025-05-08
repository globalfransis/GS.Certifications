using AutoMapper;
using GSF.Application.Common.Mappings;
using GS.Certifications.Domain.Entities.Percepciones;

namespace GS.Certifications.Application.Commons.Dtos;

public class PercepcionDto : IMapFrom<Percepcion>
{
    public short Idm { get; set; }
    public short PercepcionTipoId { get; set; }
    public long ProvinciaId { get; set; }
    public string Descripcion { get; set; }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Percepcion, PercepcionDto>()
                .ForMember(dst => dst.Idm, opt => opt.MapFrom(src => src.Id))
                ;
        }
    }
}
