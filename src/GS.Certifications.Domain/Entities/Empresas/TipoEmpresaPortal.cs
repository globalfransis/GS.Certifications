using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Empresas
{
    public class TipoEmpresaPortal : BaseFixedShortEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activa { get; set; } = true;
    }
}
