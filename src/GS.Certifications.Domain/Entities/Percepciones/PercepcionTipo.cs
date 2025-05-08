using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Percepciones;

public class PercepcionTipo : BaseFixedShortEntity
{
    public const short IVA = 1;
    public const short IIBB = 2;
    public const short MUNICIPALES = 3;

    public string Valor { get; set; }
    public string Descripcion { get; set; }
    public bool General { get; set; } = false;
}
