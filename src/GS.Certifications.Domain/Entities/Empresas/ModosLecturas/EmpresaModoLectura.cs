using GS.Certifications.Domain.Entities.ModosLecturas;
using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Empresas.ModosLecturas
{
    public class EmpresaModoLectura : BaseIntEntity
    {
        public int EmpresaPortalId { get; set; }
        public short ModoLecturaIdm { get; set; }
        public ModoLectura ModoLectura { get; set; }
    }
}
