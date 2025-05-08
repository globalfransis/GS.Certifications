using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GSF.Domain.Entities.Security;
using GS.Certifications.Domain.Entities.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Application.Commons.Services.UsuarioEmpresaPortales
{
    public class UsuarioEmpresaPortalDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int EmpresaPortalId { get; set; }
        public DateTime FechaRegistracion { get; set; }
        public List<RolTipoDto> Roles { get; set; }
    }
    public class UsuarioEmpresaPortalDtoProfile : Profile
    {
        public UsuarioEmpresaPortalDtoProfile()
        {
            CreateMap<UsuarioEmpresaPortal, UsuarioEmpresaPortalDto>()
            .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dst => dst.EmpresaPortalId, opt => opt.MapFrom(src => src.EmpresaPortalId))
            .ForMember(dst => dst.FechaRegistracion, opt => opt.MapFrom(src => src.FechaRegistracion))
            .ForMember(dst => dst.Roles, opt => opt.MapFrom(src => src.Roles.Select(uepr => uepr.RolTipo).ToList()))
            ;
        }
    }
}
