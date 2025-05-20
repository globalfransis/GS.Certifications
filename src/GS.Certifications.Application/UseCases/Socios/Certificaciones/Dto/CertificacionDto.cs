using AutoMapper;
using GS.Certifications.Domain.Entities.Certificaciones;
using GS.Certifications.Domain.Entities.Certificaciones.Documentos;
using GSF.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Certifications.Application.UseCases.Socios.Certificaciones.Dto;

public class CertificacionDto : IMapFrom<Certificacion>
{
    public long CompanyId { get; set; }
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
    public int Id { get; set; }
    public int SocioId { get; set; }
    public string Socio { get; set; }
    public int CertificacionId { get; set; }
    public string Certificacion { get; set; }
    public short EstadoId { get; set; }
    public string Estado { get; set; }
    public short CantidadAprobaciones { get; set; } = default;
    public short CantDocsPendientes { get; set; } = default;
    public short CantDocsAprobados { get; set; } = default;
    public short CantDocsCargados { get; set; } = default;
    public DateTime? FechaSolicitud { get; set; }
    public DateTime? UltimaModificacionEstado { get; set; }
    public DateTime? VigenciaDesde { get; set; }
    public short OrigenId { get; set; }
    public short PropietarioActualId { get; set; }
    public DateTime? VigenciaHasta { get; set; }
    public string Observaciones { get; set; }
    public Guid Guid { get; set; }
    public byte[] RowVersion { get; set; }

    public List<DocumentoCargadoDto> DocumentosCargados { get; set; }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SolicitudCertificacion, SolicitudCertificacionDto>()
            .ForMember(dst => dst.Socio, opt => opt.MapFrom(src => src.Socio.RazonSocial))
            .ForMember(dst => dst.Certificacion, opt => opt.MapFrom(src => src.Certificacion.Nombre))
            .ForMember(dst => dst.Estado, opt => opt.MapFrom(src => src.Estado.Descripcion))
            .ForMember(dst => dst.CantidadAprobaciones, opt => opt.MapFrom(src => src.Certificacion.CantidadAprobaciones))
            .ForMember(dst => dst.CantDocsPendientes, opt => opt.MapFrom(src => src.DocumentosCargados.Where(d => d.EstadoId == DocumentoEstado.PENDIENTE).ToList().Count))
            .ForMember(dst => dst.CantDocsCargados, opt => opt.MapFrom(src => src.DocumentosCargados.Where(d => d.EstadoId != DocumentoEstado.PENDIENTE).ToList().Count))
            .ForMember(dst => dst.CantDocsAprobados, opt => opt.MapFrom(src => src.DocumentosCargados.Where(d => d.EstadoId == DocumentoEstado.VALIDADO).ToList().Count))
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


public class DocumentoCargadoDto : IMapFrom<DocumentoCargado>
{
    public int Id { get; set; }
    public int SolicitudId { get; set; }
    public int DocumentoRequeridoId { get; set; }
    public string Tipo { get; set; }
    public string ArchivoURL { get; set; }
    public int? Version { get; set; }
    public DateTime? FechaDesde { get; set; }
    public DateTime? FechaHasta { get; set; }
    public int EstadoId { get; set; }
    public string Estado { get; set; }
    public long? ValidadoPorId { get; set; }
    public string ValidadoPor { get; set; }
    public DateTime? FechaSubida { get; set; }
    public byte[] RowVersion { get; set; }
    public Guid Guid { get; set; }
    public Guid SolicitudGuid { get; set; }

    public string Observaciones { get; set; }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DocumentoCargado, DocumentoCargadoDto>()
                .ForMember(dst => dst.Tipo, opt => opt.MapFrom(src => src.DocumentoRequerido.Tipo.Nombre))
                .ForMember(dst => dst.ValidadoPor, opt => opt.MapFrom(src => src.ValidadoPor.Login))
                .ForMember(dst => dst.SolicitudGuid, opt => opt.MapFrom(src => src.Solicitud.Guid))
                .ForMember(dst => dst.Estado, opt => opt.MapFrom(src => src.Estado.Descripcion));
        }
    }
}