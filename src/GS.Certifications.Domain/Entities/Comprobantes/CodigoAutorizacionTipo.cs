using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Comprobantes;

public class CodigoAutorizacionTipo : BaseFixedShortEntity
{
    public string CodigoArca { get; set; }
    public string Descripcion { get; set; }
    public string CodigoExterno { get; set; }

    public const short CAE = 1;
    public const short CAEA = 2;
    public const short CAI = 3;
}
