using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Comprobantes;

public class CondicionVenta : BaseFixedShortEntity
{
    public const short CONTADO = 1;
    public const string CONTADO_DESC = "Contado";

    public const short CUENTA_CORRIENTE = 2;
    public const string CUENTA_CORRIENTE_DESC = "Cuenta Corriente";

    public const short TARJETA_CREDITO = 3;
    public const string TARJETA_CREDITO_DESC = "Tarjeta de Crédito";

    public const short TARJETA_DEBITO = 4;
    public const string TARJETA_DEBITO_DESC = "Tarjeta de Débito";

    public const short TRANSFERENCIA_BANCARIA = 5;
    public const string TRANSFERENCIA_BANCARIA_DESC = "Transferencia Bancaria";

    public const short CHEQUE = 6;
    public const string CHEQUE_DESC = "Cheque";

    public const short OTRO = 7;
    public const string OTRO_DESC = "Otro";


    public string Descripcion { get; set; }
}
