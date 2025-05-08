using AutoMapper;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Empresas.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Application.Commons.Dtos.Empresas
{
    public class EmpresaRolDto
    {
        public int EmpresaId { get; set; }
        public short RolTipoId { get; set; }
        public RolTipoDto RolTipo { get; set; }
    }
    public class EmpresaRolDtoProfile : Profile
    {
        public EmpresaRolDtoProfile()
        {
            CreateMap<EmpresaRol, EmpresaRolDto>()
            .ForMember(dst => dst.EmpresaId, opt => opt.MapFrom(src => src.EmpresaPortalId))
            .ForMember(dst => dst.RolTipo, opt => opt.MapFrom(src => src.RolTipo));
        }
    }
}
