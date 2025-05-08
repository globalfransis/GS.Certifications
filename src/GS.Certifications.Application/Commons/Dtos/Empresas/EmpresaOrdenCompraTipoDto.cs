using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.OrdenesCompra;
using GS.Certifications.Domain.Entities.Empresas.OrdenesCompras;

namespace GS.Certifications.Application.Commons.Dtos.Empresas
{
    public class EmpresaOrdenCompraTipoDto
    {
        public int Id { get; set; }
        public int EmpresaPortalId { get; set; }
        public int OrdenCompraTipoId { get; set; }
        public OrdenCompraTipoDto OrdenCompraTipo { get; set; }
    }

    public class MappingProfileEmpresaOrdenCompraTipo : Profile
    {
        public MappingProfileEmpresaOrdenCompraTipo()
        {
            CreateMap<EmpresaOrdenCompraTipo, EmpresaOrdenCompraTipoDto>();
        }
    }
}
