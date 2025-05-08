using AutoMapper;
using GSF.Application.Common.Mappings;
using GS.Certifications.Domain.Entities.OrdenesCompras;
using System;

namespace GS.Certifications.Application.Commons.Dtos.OrdenesCompra
{
    public class OrdenCompraForListDto : IMapFrom<OrdenCompra>
    {
        public int Id { get; set; }
        public string NumeroOrden { get; set; }
        public DateTime Fecha { get; set; }
        public string RazonEmpresaPortal { get; set; }
        public string OrdenCompraTipo { get; set; }
        public string OrdenCompraEstado { get; set; }
        public decimal Importe { get; set; }
        public string Moneda { get; set; }
        public class OrdenCompraForListDtoProfile : Profile
        {
            public OrdenCompraForListDtoProfile()
            {
                CreateMap<OrdenCompra, OrdenCompraForListDto>()
                    .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dst => dst.NumeroOrden, opt => opt.MapFrom(src => src.NumeroOrden))
                    .ForMember(dst => dst.Fecha, opt => opt.MapFrom(src => src.Fecha))
                    .ForMember(dst => dst.RazonEmpresaPortal, opt => opt.MapFrom(src => src.EmpresaPortal.RazonSocial))
                    .ForMember(dst => dst.OrdenCompraTipo, opt => opt.MapFrom(src => src.OrdenCompraTipo.Nombre))
                    .ForMember(dst => dst.OrdenCompraEstado, opt => opt.MapFrom(src => src.OrdenCompraEstado.Nombre))
                    .ForMember(dst => dst.Importe, opt => opt.MapFrom(src => src.Importe))
                    .ForMember(dst => dst.Moneda, opt => opt.MapFrom(src => src.Moneda.Code))
                ;

                CreateMap<OrdenCompraTipo, OrdenCompraTipoDto>();
                CreateMap<OrdenCompraEstado, OrdenCompraEstadoDto>();
            }
        }
    }
}
