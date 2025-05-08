using AutoMapper;
using GSF.Application.Common.Mappings;
using GSF.Application.Segurity.Companies.Queries.GetGroupsByCurrentGroupOwner;
using GS.Certifications.Domain.Entities.OrdenesCompras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Application.Commons.Dtos.OrdenesCompra
{
    public class GrupoOrdenCompraTipoDto : IMapFrom<GrupoOrdenCompraTipo>
    {
        public int OrdenCompraTipoId { get; set; }
        public long GrupoId { get; set; }
        public GroupListDto Grupo { get; set; }
    }
    public class GrupoOrdenCompraTipoDtoProfile : Profile
    {
        public GrupoOrdenCompraTipoDtoProfile()
        {
            CreateMap<GrupoOrdenCompraTipo, GrupoOrdenCompraTipoDto>()
            ;
        }
    }
}
