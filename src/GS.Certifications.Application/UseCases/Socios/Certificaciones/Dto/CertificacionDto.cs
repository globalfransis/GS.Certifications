using AutoMapper;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Dto;
using GS.Certifications.Domain.Entities.Certificaciones;
using GS.Certifications.Domain.Entities.Comprobantes;
using GSF.Application.Common.Mappings;
using System.Collections.Generic;

namespace GS.Certifications.Application.UseCases.Socios.Certificaciones.Dto;

public class CertificacionDto : IMapFrom<Certificacion>
{
    public short TipoEmpresaPortalId { get; set; }
    public string TipoEmpresaPortal { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public bool Activa { get; set; }
    public List<SolicitudCertificacionDto> Solicitudes { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Certificacion, CertificacionDto>()
        .ForMember(dst => dst.TipoEmpresaPortal, opt => opt.MapFrom(src => src.TipoEmpresaPortal.Descripcion))
        ;
    }
}

public class SolicitudCertificacionDto : IMapFrom<SolicitudCertificacion>
{
    public int SocioId { get; set; }
    public string Socio { get; set; }
    public int CertificacionId { get; set; }
    public string Certificacion { get; set; }
    public short EstadoId { get; set; }
    public string Estado { get; set; }
    public short CantidadAprobaciones { get; set; } = default;
    public string Observaciones { get; set; }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SolicitudCertificacion, SolicitudCertificacionDto>()
            .ForMember(dst => dst.Socio, opt => opt.MapFrom(src => src.Socio.RazonSocial))
            .ForMember(dst => dst.Certificacion, opt => opt.MapFrom(src => src.Certificacion.Nombre))
            .ForMember(dst => dst.Estado, opt => opt.MapFrom(src => src.Estado.Descripcion))
            ;
        }
    }
}

public class SolicitudCertificacionEstadoDto : IMapFrom<SolicitudCertificacionEstado>
{
    public short Idm { get; set; }
    public string Descripcion { get; set; }


    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SolicitudCertificacionEstado, SolicitudCertificacionEstadoDto>();
        }
    }
}
