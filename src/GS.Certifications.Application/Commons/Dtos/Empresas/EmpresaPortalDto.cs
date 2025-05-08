using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Alicuotas;
using GSF.Application.Common.Mappings;
using GSF.Application.Companies;
using GSF.Application.Parameters.Countries.Queries.GetCountries;
using GSF.Application.Parametrics.Geo.Cities;
using GSF.Application.Parametrics.Geo.Provinces;
using GSF.Application.Security.Services;
using GSF.Domain.Entities.Companies;
using GS.Certifications.Domain.Entities.Comprobantes;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Empresas.Roles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Certifications.Application.Commons.Dtos.Empresas;

public record EmpresaPortalDto : IMapFrom<EmpresaPortal>
{
    public int Id { get; set; }
    public long CompanyId { get; set; }
    public SecurityUserCompaniesDto Company { get; set; }
    public string CodigoProveedor { get; set; }
    public string RazonSocial { get; set; }
    public string NombreFantasia { get; set; }
    public string IdentificadorTributario { get; set; }
    public bool GranContribuyente { get; set; }
    public string Direccion { get; set; }
    public string CodigoPostal { get; set; }
    public long OrganizationId { get; set; }
    public SecurityOrganizationDto Organization { get; set; }
    public long PaisId { get; set; }
    public CountryDto Pais { get; set; }
    public long? ProvinciaId { get; set; }
    public ProvinceDto Provincia { get; set; }
    public long? CiudadId { get; set; }
    public CityDto Ciudad { get; set; }
    public string CiudadDescripcion { get; set; }
    public string TelefonoPrincipal { get; set; }
    public string TelefonoAlternativo { get; set; }
    public string EmailPrincipal { get; set; }
    public string EmailAlternativo { get; set; }
    public string Contacto { get; set; }
    public string ContactoAlternativo { get; set; }
    public short TipoResponsableId { get; set; }
    public CategoriaTipoDto TipoResponsable { get; set; }
    public string NumeroIngresosBrutos { get; set; }
    public short? TipoCuentaId { get; set; }
    public TipoCuentaDto TipoCuenta { get; set; }
    public string CuentaBancaria { get; set; }
    public string PaginaWeb { get; set; }
    public string RedesSociales { get; set; }
    public string DescripcionEmpresa { get; set; }
    public string ProductosServiciosOfrecidos { get; set; }
    public string ReferenciasComerciales { get; set; }
    public bool Confirmado { get; set; }
    public List<EmpresaCurrencyDto> Monedas { get; set; } = new List<EmpresaCurrencyDto>();
    public List<RolTipoDto> Roles { get; set; } = new List<RolTipoDto>();
    public List<AlicuotaDto> Alicuotas { get; set; } = new List<AlicuotaDto>();

    public class EmpresaDtoProfile : Profile
    {
        public EmpresaDtoProfile()
        {
            CreateMap<EmpresaPortal, EmpresaPortalDto>()
            .ForMember(dst => dst.Roles, opt => opt.MapFrom(src => src.Roles.Select(er => er.RolTipo).ToList()))
            .ForMember(dst => dst.Alicuotas, opt => opt.MapFrom(src => src.Alicuotas.Select(er => er.Alicuota).ToList()))
            .ForMember(dst => dst.Monedas, opt => opt.MapFrom(src => src.Monedas.ToList()))
            ;
        }
    }
}

