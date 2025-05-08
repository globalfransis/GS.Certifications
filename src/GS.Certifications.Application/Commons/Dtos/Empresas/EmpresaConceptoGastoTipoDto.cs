using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.ConceptosGastosTipos;
using GS.Certifications.Domain.Entities.Empresas.ConceptosGastosTipos;

namespace GS.Certifications.Application.Commons.Dtos.Empresas
{
    public class EmpresaConceptoGastoTipoDto
    {
        public int Id { get; set; }
        public int EmpresaPortalId { get; set; }
        public int ConceptoGastoTipoId { get; set; }
        public ConceptoGastoTipoDto ConceptoGastoTipo { get; set; }
    }
    public class MappingProfileEmpresaConceptoGastoTipo : Profile
    {
        public MappingProfileEmpresaConceptoGastoTipo()
        {
            CreateMap<EmpresaConceptoGastoTipo, EmpresaConceptoGastoTipoDto>();
        }
    }
}
