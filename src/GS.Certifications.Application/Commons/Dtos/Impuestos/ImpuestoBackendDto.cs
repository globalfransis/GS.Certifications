using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Alicuotas;
using GSF.Application.Parametrics.Geo.Provinces;
using GSF.Application.Security.Services;
using GSF.Domain.Entities.Geo;
using GS.Certifications.Domain.Entities.Alicuotas;
using GS.Certifications.Domain.Entities.Impuestos;

namespace GS.Certifications.Application.Commons.Dtos.Impuestos
{
    public class ImpuestoBackendDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public ImpuestoTipoDto Tipo { get; set; }
        public ProvinceDto Provincia { get; set; }
        public SecurityUserCompaniesDto Company { get; set; }
        public AlicuotaDto Alicuota { get; set; }
        public decimal? Valor { get; set; }

        public class MappingImpuestoProfile : Profile
        {
            public MappingImpuestoProfile()
            {
                CreateMap<Impuesto, ImpuestoBackendDto>()
                    .ForMember(dst => dst.Tipo, opt => opt.MapFrom(src => src.Tipo))
                    .ForMember(dst => dst.Provincia, opt => opt.MapFrom(src => src.Provincia))
                    .ForMember(dst => dst.Company, opt => opt.MapFrom(src => src.Company))
                    .ForMember(dst => dst.Alicuota, opt => opt.MapFrom(src => src.Alicuota))
                    ;
            }
        }
    }
}
