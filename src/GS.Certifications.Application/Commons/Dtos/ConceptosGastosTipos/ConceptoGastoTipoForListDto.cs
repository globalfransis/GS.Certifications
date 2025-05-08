using AutoMapper;
using GSF.Application.Common.Mappings;
using GS.Certifications.Domain.Entities.ConceptosGastosTipos;

namespace GS.Certifications.Application.Commons.Dtos.ConceptosGastosTipos
{
    public class ConceptoGastoTipoForListDto : IMapFrom<ConceptoGastoTipo>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ConceptoContableNombre { get; set; }
        public string ConceptoContableValor { get; set; }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<ConceptoGastoTipo, ConceptoGastoTipoForListDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                    .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                    .ForMember(dest => dest.ConceptoContableNombre, opt => opt.MapFrom(src => src.ConceptoContableNombre))
                    .ForMember(dest => dest.ConceptoContableValor, opt => opt.MapFrom(src => src.ConceptoContableValor))
                    ;
            }
        }
    }
}
