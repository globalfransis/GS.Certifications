using GS.Certifications.Domain.Entities.Alicuotas;
using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Empresas.Alicuotas
{
    public class EmpresaAlicuota : BaseIntEntity
    {
        public int EmpresaPortalId { get; set; }
        public Alicuota Alicuota { get; set; }
        public short AlicuotaIdm { get; set; }
    }
}
