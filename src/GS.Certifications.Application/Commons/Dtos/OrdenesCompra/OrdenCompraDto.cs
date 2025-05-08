using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GSF.Application.Common.Mappings;
using GSF.Application.Global.Currencies.Queries;
using GSF.Application.Security.Services;
using GS.Certifications.Domain.Entities.OrdenesCompras;
using System;

namespace GS.Certifications.Application.Commons.Dtos.OrdenesCompra
{
    public class OrdenCompraDto : IMapFrom<OrdenCompra>
    {
        public int Id { get; set; }
        public string NumeroOrden { get; set; }
        public DateTime Fecha { get; set; }
        public EmpresaPortalDto EmpresaPortal { get; set; }
        public OrdenCompraTipoDto OrdenCompraTipo { get; set; }
        //public string CodigoHES { get; set; }
        public string CodigoQAD { get; set; }
        public OrdenCompraEstadoDto OrdenCompraEstado { get; set; }
        public decimal Importe { get; set; } = default;
        public CurrencyDto Moneda { get; set; }
        public string Observaciones { get; set; }
        public SecurityUserCompaniesDto Company { get; set; }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<OrdenCompra, OrdenCompraDto>()
                .ForMember(dest => dest.EmpresaPortal, opt => opt.MapFrom(src => src.EmpresaPortal))
                .ForMember(dest => dest.OrdenCompraTipo, opt => opt.MapFrom(src => src.OrdenCompraTipo))
                .ForMember(dest => dest.OrdenCompraEstado, opt => opt.MapFrom(src => src.OrdenCompraEstado))
                .ForMember(dest => dest.Moneda, opt => opt.MapFrom(src => src.Moneda))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company));

                CreateMap<OrdenCompraTipo, OrdenCompraTipoDto>();
                CreateMap<OrdenCompraEstado, OrdenCompraEstadoDto>();
            }
        }
    }
}
