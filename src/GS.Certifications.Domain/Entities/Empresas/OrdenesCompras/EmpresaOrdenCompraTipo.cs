using GS.Certifications.Domain.Entities.OrdenesCompras;
using GSF.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Domain.Entities.Empresas.OrdenesCompras
{
    public class EmpresaOrdenCompraTipo : BaseIntEntity
    {
        public int EmpresaPortalId { get; set; }
        public int OrdenCompraTipoId { get; set; }
        public OrdenCompraTipo OrdenCompraTipo { get; set; }
    }
}
