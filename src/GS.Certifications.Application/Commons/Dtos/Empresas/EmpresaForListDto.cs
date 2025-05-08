using AutoMapper;
using GSF.Application.Common.Mappings;
using GSF.Application.Security.UserActivities.Dto;
using GSF.Domain.Entities.Security;
using GS.Certifications.Domain.Entities.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Application.Commons.Dtos.Empresas
{
    public class EmpresaForListDto : IMapFrom<EmpresaPortal>
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string IdentificadorTributario { get; set; }
        public string CodigoProveedor { get; set; }
        public string Contacto { get; set; }
        public bool Confirmado { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmpresaPortal, EmpresaForListDto>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.RazonSocial, opt => opt.MapFrom(src => src.RazonSocial))
                .ForMember(dst => dst.IdentificadorTributario, opt => opt.MapFrom(src => src.IdentificadorTributario))
                .ForMember(dst => dst.CodigoProveedor, opt => opt.MapFrom(src => src.CodigoProveedor))
                .ForMember(dst => dst.Contacto, opt => opt.MapFrom(src => src.Contacto))
                .ForMember(dst => dst.Confirmado, opt => opt.MapFrom(src => src.Confirmado))
                ;
        }
    }
}
