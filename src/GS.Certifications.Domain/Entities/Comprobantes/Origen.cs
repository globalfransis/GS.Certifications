using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Comprobantes;

public class Origen : BaseFixedShortEntity
{
    public const short SOCIOS = 1;
    public const short BACKOFFICE = 2;
    public const short CORREO = 3;
    public string Descripcion { get; set; }
}
