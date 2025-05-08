using AutoMapper;
using GSF.Application.Parametrics.Geo.Provinces;
using GSF.Application.Security.Services;
using GSF.Domain.Entities.Geo;
using GS.Certifications.Domain.Entities.Impuestos;
using GS.Certifications.Domain.Entities.Percepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Application.Commons.Dtos.Percepciones
{
    public class PercepcionBackendDto
    {
        public int Id { get; set; }
        public short PercepcionTipoId { get; set; }
        public PercepcionTipoDto PercepcionTipo { get; set; }
        public long? ProvinciaId { get; set; }
        public ProvinceDto Provincia { get; set; }
        public string Descripcion { get; set; }
        public long? CompanyId { get; set; }
        public SecurityUserCompaniesDto Company { get; set; }
        public class MappingPercepcionProfile : Profile
        {
            public MappingPercepcionProfile()
            {
                CreateMap<Percepcion, PercepcionBackendDto>()
                    .ForMember(dst => dst.PercepcionTipo, opt => opt.MapFrom(src => src.PercepcionTipo))
                    .ForMember(dst => dst.Provincia, opt => opt.MapFrom(src => src.Provincia))
                    .ForMember(dst => dst.Company, opt => opt.MapFrom(src => src.Company))
                    ;
            }
        }
    }
}
