using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Comprobantes;

public class EstadoValidacionARCA : BaseFixedShortEntity
{
    public const short VALIDADA = 1;
    public const short RECHAZADA = 2;
    public const short ERROR_VALIDACION = 3;
    public const short NO_VALIDADA = 4;

    public string Descripcion { get; set; }
}
