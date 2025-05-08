using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Comprobantes;

public class ComprobanteEstado : BaseFixedIntEntity
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string Valor { get; set; }

    public const int ARCHIVO_SUBIDO = 1;
    public const string ARCHIVO_SUBIDO_VALOR = "100";
    public const int EN_PROCESO_CARGA = 2;
    public const string EN_PROCESO_CARGA_VALOR = "200";
    public const int ERRORES_ARCA = 3;
    public const string ERRORES_ARCA_VALOR = "300";
    public const int CONFIRMADO = 4;
    public const string CONFIRMADO_VALOR = "400";
    public const int ACUSE_RECIBO_CLIENTE = 5;
    public const string ACUSE_RECIBO_CLIENTE_VALOR = "500";
    public const int APROBADA_CLIENTE = 6;
    public const string APROBADA_CLIENTE_VALOR = "600";
    public const int RECHAZADA_CLIENTE = 7;
    public const string RECHAZADA_CLIENTE_VALOR = "700";
    public const int BORRADOR = 8;
    public const string BORRADOR_VALOR = "800";
    public const int AUTORIZADO = 9;
    public const string AUTORIZADO_VALOR = "900";
}