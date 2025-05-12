using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Certificaciones.Documentos;

public class TipoDocumento : BaseFixedShortEntity
{
    public string Nombre { get; set; }
    public bool RequiereVigencia { get; set; }
    public bool RequiereValidacion { get; set; }
}
