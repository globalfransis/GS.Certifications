using AutoMapper;
using GSF.Application.Common.Mappings;
using GSF.Domain.Entities.Security;

namespace GS.Certifications.Application.Commons.Dtos.Empresas
{
    public class UsuarioExternoDto : IMapFrom<User>
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        public string Telefono { get; set; }
        public class UsuarioExternoDtoProfile : Profile
        {
            public UsuarioExternoDtoProfile()
            {
                CreateMap<User, UsuarioExternoDto>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dst => dst.Nombre, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dst => dst.Apellido, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dst => dst.Telefono, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dst => dst.Documento, opt => opt.MapFrom(src => src.IdNumber));
                ;
            }
        }
    }
}
