using GSF.Domain.Common;
using GSF.Domain.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Domain.Entities.OrdenesCompras
{
    public class GrupoOrdenCompraTipo : BaseIntEntity
    {
        public int OrdenCompraTipoId { get; set; }
        public long GrupoId { get; set; }
        public Group Grupo { get; set; }
    }
}
