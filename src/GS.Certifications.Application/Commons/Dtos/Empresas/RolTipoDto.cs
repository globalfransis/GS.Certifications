using AutoMapper;
using GSF.Application.Common.Mappings;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Empresas.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Application.Commons.Dtos.Empresas
{
    public class RolTipoDto //: IMapFrom<RolTipo>
    {
        public short Idm { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
    }
    public class RolTipoDtoProfile : Profile
    {
        public RolTipoDtoProfile()
        {
            CreateMap<RolTipo, RolTipoDto>()
            .ForMember(dst => dst.Idm, opt => opt.MapFrom(src => src.Idm))
            .ForMember(dst => dst.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
            .ForMember(dst => dst.Codigo, opt => opt.MapFrom(src => src.Codigo));
        }
    }
}
