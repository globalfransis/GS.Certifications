using AutoMapper;
using GSF.Application.Common.Mappings;
using GSF.Application.Segurity.Companies.Queries.GetGroupsByCurrentGroupOwner;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.OrdenesCompras;
using System.Collections.Generic;
using System.Linq;

namespace GS.Certifications.Application.Commons.Dtos.OrdenesCompra
{
    public class OrdenCompraTipoDto : IMapFrom<OrdenCompraTipo>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool EsRequerida { get; set; }
        public bool EsAbierta { get; set; }
        public bool EsRecurrente { get; set; }
        public bool EsUnica { get; set; }
        public List<GrupoOrdenCompraTipoDto> Grupos { get; set; } = new List<GrupoOrdenCompraTipoDto>();
    }
    public class OrdenCompraTipoDtoProfile : Profile
    {
        public OrdenCompraTipoDtoProfile()
        {
            CreateMap<OrdenCompraTipo, OrdenCompraTipoDto>()
            ;
        }
    }
}
