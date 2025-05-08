using AutoMapper;
using GSF.Application.Common.Mappings;
using GS.Certifications.Domain.Entities.Impuestos;

namespace GS.Certifications.Application.Commons.Dtos;

public class ImpuestoDto : IMapFrom<Impuesto>
{
    public short Idm { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public short? TipoId { get; set; }
    public long? ProvinciaId { get; set; }
    public decimal Alicuota { get; set; }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Impuesto, ImpuestoDto>()
                .ForMember(dst => dst.Idm, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Alicuota, opt => opt.MapFrom(src => src.Alicuota != null ? src.Alicuota.Valor : src.Valor))
                ;
        }
    }
}
