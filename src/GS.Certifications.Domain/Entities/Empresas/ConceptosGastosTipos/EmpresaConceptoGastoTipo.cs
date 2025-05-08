using GS.Certifications.Domain.Entities.ConceptosGastosTipos;
using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Empresas.ConceptosGastosTipos
{
    public class EmpresaConceptoGastoTipo : BaseIntEntity
    {
        public int EmpresaPortalId { get; set; }
        public int ConceptoGastoTipoId { get; set; }
        public ConceptoGastoTipo ConceptoGastoTipo { get; set; }
    }
}
