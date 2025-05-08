using AutoMapper;
using GS.Certifications.Domain.Entities.Alicuotas;

namespace GS.Certifications.Application.Commons.Dtos.Alicuotas
{
    public class AlicuotaDto
    {
        public short Idm { get; set; }
        public string CodigoAFIP { get; set; }
        public decimal? Valor { get; set; }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Alicuota, AlicuotaDto>();
            }
        }
    }
}
