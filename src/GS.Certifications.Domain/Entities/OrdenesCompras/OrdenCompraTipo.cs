using GSF.Domain.Common;
using GSF.Domain.Entities.Companies;
using System.Collections.Generic;

namespace GS.Certifications.Domain.Entities.OrdenesCompras
{
    public class OrdenCompraTipo : BaseIntEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool EsRequerida { get; set; }
        public bool EsAbierta { get; set; }
        public bool EsRecurrente { get; set; }
        public bool EsUnica { get; set; }
        public Company Company { get; set; }
        public long CompanyId { get; set; }
        public List<GrupoOrdenCompraTipo> Grupos { get; set; }
    }
}
