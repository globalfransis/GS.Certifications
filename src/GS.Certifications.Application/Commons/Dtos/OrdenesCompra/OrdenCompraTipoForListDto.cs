using AutoMapper;
using Azure.Core;
using GSF.Application.Common.Mappings;
using GS.Certifications.Domain.Entities.OrdenesCompras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Application.Commons.Dtos.OrdenesCompra
{
    public class OrdenCompraTipoForListDto : IMapFrom<OrdenCompraTipo>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Caracteristica { get; set; }
        public bool EsRequerida { get; set; }

        public class OrdenCompraTipoForListDtoProfile : Profile
        {
            public OrdenCompraTipoForListDtoProfile()
            {
                CreateMap<OrdenCompraTipo, OrdenCompraTipoForListDto>()
                    .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dst => dst.Nombre, opt => opt.MapFrom(src => src.Nombre))
                    .ForMember(dst => dst.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                    .ForMember(dst => dst.Caracteristica, opt => opt.MapFrom(src => DefinirEstado(src.EsAbierta, src.EsRecurrente, src.EsUnica)))
                    .ForMember(dst => dst.EsRequerida, opt => opt.MapFrom(src => src.EsRequerida))
                ;
            }
        }

        private static string DefinirEstado(bool esAbierta, bool esRecurrente, bool esUnica)
        {
            if (esAbierta)
                return "Abierta";
            else if (esRecurrente)
                return "Recurrente";
            else if (esUnica)
                return "Unica";
            return "";
        }
    }
}
