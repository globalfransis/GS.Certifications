using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Empresas;

public class TipoCuenta : BaseFixedShortEntity
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }

    public const short CUENTA_CORRIENTE = 1;
    public const short CAJA_AHORRO = 2;
    public const short CUENTA_SUELDO = 3;
    public const short CUENTA_INVERSION = 4;
    public const short CUENTA_ESPECIAL = 5;
}
