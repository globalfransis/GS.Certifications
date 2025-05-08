using AutoMapper;
using GSF.Application.Common.Mappings;
using GS.Certifications.Domain.Entities.Periodos;
using System;

namespace GS.Certifications.Application.Commons.Dtos.Periodos
{
    public class PeriodoDto : IMapFrom<Periodo>
    {
        public int Id { get; set; }
        public int? Año { get; set; }
        public int? NumeroPeriodo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public EstadoPeriodoDto Estado { get; set; }
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Periodo, PeriodoDto>()
                    .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dst => dst.Estado, opt => opt.MapFrom(src => src.Estado))
                    ;
            }
        }
    }
}
