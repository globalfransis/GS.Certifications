using AutoMapper;
using GSF.Application.Common.Mappings;
using GS.Certifications.Domain.Entities.Periodos;
using System;
using System.Globalization;

namespace GS.Certifications.Application.Commons.Dtos.Periodos
{
    public class PeriodoForListDto : IMapFrom<Periodo>
    {
        public int? Id { get; set; }
        public int? Año { get; set; }
        public int? NumeroPeriodo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Estado { get; set; }
        public string Vigencia { get; set; }
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Periodo, PeriodoForListDto>()
                    .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dst => dst.Estado, opt => opt.MapFrom(src => src.Estado.Descripcion))
                    .ForMember(dst => dst.Vigencia, opt => opt.MapFrom(src => FormatVigencia(src)))
                ;
            }
        }

        // Método helper para formatear la vigencia
        private static string FormatVigencia(Periodo src)
        {
            string fechaInicioStr = src.FechaInicio.HasValue
                                    ? src.FechaInicio.Value.ToString("MM/yyyy", CultureInfo.InvariantCulture) // mes/año
                                    : "N/A";
            string fechaFinStr = src.FechaFin.HasValue
                                 ? src.FechaFin.Value.ToString("MM/yyyy", CultureInfo.InvariantCulture) // mes/año
                                 : "N/A";

            string estadoStr = src.Estado?.Descripcion ?? "Sin Estado";

            return $"{fechaInicioStr} - {fechaFinStr} ({estadoStr})";
        }
    }
}
