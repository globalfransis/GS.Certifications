using AutoMapper;
using GSF.Application.Common.Mappings;
using GS.Certifications.Domain.Entities.OrdenesCompras;

namespace GS.Certifications.Application.Commons.Dtos.OrdenesCompra
{
    public class OrdenCompraEstadoDto : IMapFrom<OrdenCompraEstado>
    {
        public short Idm { get; set; }
        public int Valor { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
