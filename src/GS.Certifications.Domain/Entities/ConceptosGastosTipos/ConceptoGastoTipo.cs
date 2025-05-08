using GSF.Domain.Common;
using GSF.Domain.Entities.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Domain.Entities.ConceptosGastosTipos
{
    public class ConceptoGastoTipo : BaseIntEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ConceptoContableNombre { get; set; }
        public string ConceptoContableValor { get; set; }
        public Company Company { get; set; }
        public long CompanyId { get; set; }
    }
}
