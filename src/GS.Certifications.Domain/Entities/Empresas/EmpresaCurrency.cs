using GSF.Domain.Common;
using GSF.Domain.Entities.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Domain.Entities.Empresas
{
    public class EmpresaCurrency : BaseIntEntity
    {
        public int EmpresaPortalId { get; set; }
        public Currency Currency { get; set; }
        public long CurrencyId { get; set; }
        public bool MonedaPorDefecto { get; set; }
    }
}
