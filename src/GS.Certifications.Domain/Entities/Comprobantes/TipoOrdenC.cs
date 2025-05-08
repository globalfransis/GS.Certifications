using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Comprobantes;

public class TipoOrdenC : BaseFixedShortEntity
{
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public bool DetalleItems { get; set; }
}