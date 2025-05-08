using AutoMapper;
using GSF.Application.Common.Mappings;
using GS.Certifications.Domain.Entities.Impuestos;

namespace GS.Certifications.Application.Commons.Dtos.Impuestos;

public class ImpuestoForListDto : IMapFrom<Impuesto>
{
    public short Id { get; set; }
    public string Nombre { get; set; }
    public string CodigoAFIP { get; set; }
    public string Provincia { get; set; }
    public decimal? Valor { get; set; }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Impuesto, ImpuestoForListDto>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dst => dst.CodigoAFIP, opt => opt.MapFrom(src => GetCodigoAFIP(src)))
                .ForMember(dst => dst.Provincia, opt => opt.MapFrom(src => src.Provincia.Name))
                .ForMember(dst => dst.Valor, opt => opt.MapFrom(src => GetValor(src)));
        }
    }
    private static string GetCodigoAFIP(Impuesto impuesto)
    {
        if (impuesto.AlicuotaId == null)
            return null;
        return impuesto.Alicuota.CodigoAFIP;
    }
    private static decimal? GetValor(Impuesto impuesto)
    {
        if (impuesto.AlicuotaId == null)
            return impuesto.Valor;
        return impuesto.Alicuota.Valor;
    }
}
