using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Comprobantes;

public class CategoriaTipo : BaseFixedShortEntity
{
    public string CodigoArca { get; set; }
    public string Descripcion { get; set; }
    public string CodigoExterno { get; set; }

    public const short RESPONSABLE_INSCRIPTO = 1;
    public const string RESPONSABLE_INSCRIPTO_DESC = "Responsable Inscripto";
    public const string RESPONSABLE_INSCRIPTO_CODIGO_ARCA = "1";

    public const short RESPONSABLE_NO_INSCRIPTO = 2;
    public const string RESPONSABLE_NO_INSCRIPTO_DESC = "Responsable no Inscripto";
    public const string RESPONSABLE_NO_INSCRIPTO_CODIGO_ARCA = "2";

    public const short NO_RESPONSABLE = 3;
    public const string NO_RESPONSABLE_DESC = "No Responsable";
    public const string NO_RESPONSABLE_CODIGO_ARCA = "3";

    public const short SUJETO_EXENTO = 4;
    public const string SUJETO_EXENTO_DESC = "Sujeto Exento";
    public const string SUJETO_EXENTO_CODIGO_ARCA = "4";

    public const short CONSUMIDOR_FINAL = 5;
    public const string CONSUMIDOR_FINAL_DESC = "Consumidor Final";
    public const string CONSUMIDOR_FINAL_CODIGO_ARCA = "5";

    public const short RESPONSABLE_MONOTRIBUTO = 6;
    public const string RESPONSABLE_MONOTRIBUTO_DESC = "Responsable Monotributo";
    public const string RESPONSABLE_MONOTRIBUTO_CODIGO_ARCA = "6";

    public const short SUJETO_NO_CATEGORIZADO = 7;
    public const string SUJETO_NO_CATEGORIZADO_DESC = "Sujeto no Categorizado";
    public const string SUJETO_NO_CATEGORIZADO_CODIGO_ARCA = "7";

    public const short IMPORTADOR_EXTERIOR = 8;
    public const string IMPORTADOR_EXTERIOR_DESC = "Importador del Exterior";
    public const string IMPORTADOR_EXTERIOR_CODIGO_ARCA = "8";

    public const short CLIENTE_EXTERIOR = 9;
    public const string CLIENTE_EXTERIOR_DESC = "Cliente del Exterior";
    public const string CLIENTE_EXTERIOR_CODIGO_ARCA = "9";

    public const short LIBERADO = 10;
    public const string LIBERADO_DESC = "Liberado Ley Nº 19.740";
    public const string LIBERADO_CODIGO_ARCA = "10";

    public const short AGENTE_PERCEPCION = 11;
    public const string AGENTE_PERCEPCION_DESC = "Responsable inscripto Agente de percepción";
    public const string AGENTE_PERCEPCION_CODIGO_ARCA = "11";

}
