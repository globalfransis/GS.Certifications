using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Comprobantes;

public class UnidadMedida : BaseFixedShortEntity
{
    public string CodigoAFIP { get; set; }
    public string CodigoARBA { get; set; }
    public string Descripcion { get; set; }

    public const short MILIMETRO = 1;
    public const string MILIMETRO_CODIGO_AFIP = "mm";
    public const string MILIMETRO_CODIGO_ARBA = "mm";

    public const short CENTIMETRO = 2;
    public const string CENTIMETRO_CODIGO_AFIP = "cm";
    public const string CENTIMETRO_CODIGO_ARBA = "cm";

    public const short METRO = 3;
    public const string METRO_CODIGO_AFIP = "m";
    public const string METRO_CODIGO_ARBA = "m";

    public const short MILIMETRO_CUADRADO = 4;
    public const string MILIMETRO_CUADRADO_CODIGO_AFIP = "mm2";
    public const string MILIMETRO_CUADRADO_CODIGO_ARBA = "mm2";

    public const short CENTIMETRO_CUADRADO = 5;
    public const string CENTIMETRO_CUADRADO_CODIGO_AFIP = "cm2";
    public const string CENTIMETRO_CUADRADO_CODIGO_ARBA = "cm2";

    public const short METRO_CUADRADO = 6;
    public const string METRO_CUADRADO_CODIGO_AFIP = "m2";
    public const string METRO_CUADRADO_CODIGO_ARBA = "m2";

    public const short GRAMO = 7;
    public const string GRAMO_CODIGO_AFIP = "g";
    public const string GRAMO_CODIGO_ARBA = "g";

    public const short KILOGRAMO = 8;
    public const string KILOGRAMO_CODIGO_AFIP = "Kg";
    public const string KILOGRAMO_CODIGO_ARBA = "Kg";

    public const short PORCENTAJE = 9;
    public const string PORCENTAJE_CODIGO_AFIP = "%";
    public const string PORCENTAJE_CODIGO_ARBA = "%";

    public const short SEGUNDO = 10;
    public const string SEGUNDO_CODIGO_AFIP = "seg";
    public const string SEGUNDO_CODIGO_ARBA = "seg";

    public const short MINUTO = 11;
    public const string MINUTO_CODIGO_AFIP = "min";
    public const string MINUTO_CODIGO_ARBA = "min";

    public const short HORA = 12;
    public const string HORA_CODIGO_AFIP = "hora";
    public const string HORA_CODIGO_ARBA = "hora";

    public const short UNIDAD = 13;
    public const string UNIDAD_CODIGO_AFIP = "Un.";
    public const string UNIDAD_CODIGO_ARBA = "Un.";

    public const short MILLAR = 14;
    public const string MILLAR_CODIGO_AFIP = "Millar";
    public const string MILLAR_CODIGO_ARBA = "Millar";

    public const short LITRO = 15;
    public const string LITRO_CODIGO_AFIP = "Litros";
    public const string LITRO_CODIGO_ARBA = "Litros";

    public const short CENTIMETRO_CUBICO = 18;
    public const string CENTIMETRO_CUBICO_CODIGO_AFIP = "cm3";
    public const string CENTIMETRO_CUBICO_CODIGO_ARBA = "cm3";

    public const short METRO_CUBICO = 19;
    public const string METRO_CUBICO_CODIGO_AFIP = "m3";
    public const string METRO_CUBICO_CODIGO_ARBA = "m3";

    public const short PAR = 20;
    public const string PAR_CODIGO_AFIP = "par";
    public const string PAR_CODIGO_ARBA = "par";

    public const short UNA = 21;
    public const string UNA_CODIGO_AFIP = "una";
    public const string UNA_CODIGO_ARBA = "una";

    public const short TONELADA = 22;
    public const string TONELADA_CODIGO_AFIP = "tonelada";
    public const string TONELADA_CODIGO_ARBA = "tonelada";
}