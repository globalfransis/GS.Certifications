using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Comprobantes;

public class ComprobanteTipo : BaseFixedShortEntity
{
    public string CodigoArca { get; set; }
    public string Descripcion { get; set; }
    public short TipoOperacion { get; set; }
    public string DescAbreviada { get; set; }
    public string NombreComprobante { get; set; }
    public string Letra { get; set; }
    public string CodigoExterno { get; set; }

    public const short FACTURA_A = 1;
    public const string FACTURA_A_CODIGO_AFIP = "1";
    public const string FACTURA_A_CODIGO_LETRA = "A";

    public const short NOTA_DEBITO_A = 2;
    public const string NOTA_DEBITO_A_CODIGO_AFIP = "2";
    public const string NOTA_DEBITO_A_CODIGO_LETRA = "A";

    public const short NOTA_CREDITO_A = 3;
    public const string NOTA_CREDITO_A_CODIGO_AFIP = "3";
    public const string NOTA_CREDITO_A_CODIGO_LETRA = "A";

    public const short RECIBO_A = 4;
    public const string RECIBO_A_CODIGO_AFIP = "4";
    public const string RECIBO_A_CODIGO_LETRA = "A";

    public const short NOTA_VENTA_CONTADO_A = 5;
    public const string NOTA_VENTA_CONTADO_A_CODIGO_AFIP = "5";
    public const string NOTA_VENTA_CONTADO_A_CODIGO_LETRA = "A";

    public const short FACTURA_B = 6;
    public const string FACTURA_B_CODIGO_AFIP = "6";
    public const string FACTURA_B_CODIGO_LETRA = "B";

    public const short NOTA_DEBITO_B = 7;
    public const string NOTA_DEBITO_B_CODIGO_AFIP = "7";
    public const string NOTA_DEBITO_B_CODIGO_LETRA = "B";

    public const short NOTA_CREDITO_B = 8;
    public const string NOTA_CREDITO_B_CODIGO_AFIP = "8";
    public const string NOTA_CREDITO_B_CODIGO_LETRA = "B";

    public const short RECIBO_B = 9;
    public const string RECIBO_B_CODIGO_AFIP = "9";
    public const string RECIBO_B_CODIGO_LETRA = "B";

    public const short NOTA_VENTA_CONTADO_B = 10;
    public const string NOTA_VENTA_CONTADO_B_CODIGO_AFIP = "10";
    public const string NOTA_VENTA_CONTADO_B_CODIGO_LETRA = "B";

    public const short COMPROBANTE_A_CUMPLE_3419 = 11;
    public const string COMPROBANTE_A_CUMPLE_3419_CODIGO_AFIP = "39";
    public const string COMPROBANTE_A_CUMPLE_3419_CODIGO_LETRA = "A";

    public const short COMPROBANTE_B_CUMPLE_3419 = 12;
    public const string COMPROBANTE_B_CUMPLE_3419_CODIGO_AFIP = "40";
    public const string COMPROBANTE_B_CUMPLE_3419_CODIGO_LETRA = "B";

    public const short CUENTA_VENTA_LIQUIDO_A = 13;
    public const string CUENTA_VENTA_LIQUIDO_A_CODIGO_AFIP = "60";
    public const string CUENTA_VENTA_LIQUIDO_A_CODIGO_LETRA = "A";

    public const short CUENTA_VENTA_LIQUIDO_B = 14;
    public const string CUENTA_VENTA_LIQUIDO_B_CODIGO_AFIP = "61";
    public const string CUENTA_VENTA_LIQUIDO_B_CODIGO_LETRA = "B";

    public const short LIQUIDACION_A = 15;
    public const string LIQUIDACION_A_CODIGO_AFIP = "63";
    public const string LIQUIDACION_A_CODIGO_LETRA = "A";

    public const short LIQUIDACION_B = 16;
    public const string LIQUIDACION_B_CODIGO_AFIP = "64";
    public const string LIQUIDACION_B_CODIGO_LETRA = "B";

    public const short FACTURA_C = 17;
    public const string FACTURA_C_CODIGO_AFIP = "11";
    public const string FACTURA_C_CODIGO_LETRA = "C";

    public const short DOCUMENTO_ADUANERO = 18;
    public const string DOCUMENTO_ADUANERO_CODIGO_AFIP = "14";
    public const string DOCUMENTO_ADUANERO_CODIGO_LETRA = "A";

    public const short RECIBO_C = 19;
    public const string RECIBO_C_CODIGO_AFIP = "15";
    public const string RECIBO_C_CODIGO_LETRA = "C";

    public const short NOTA_VENTA_CONTADO_C = 20;
    public const string NOTA_VENTA_CONTADO_C_CODIGO_AFIP = "16";
    public const string NOTA_VENTA_CONTADO_C_CODIGO_LETRA = "C";

    public const short FACTURA_E = 21;
    public const string FACTURA_E_CODIGO_AFIP = "19";
    public const string FACTURA_E_CODIGO_LETRA = "E";

    public const short NOTA_DEBITO_E = 22;
    public const string NOTA_DEBITO_E_CODIGO_AFIP = "20";
    public const string NOTA_DEBITO_E_CODIGO_LETRA = "E";

    public const short NOTA_CREDITO_E = 23;
    public const string NOTA_CREDITO_E_CODIGO_AFIP = "21";
    public const string NOTA_CREDITO_E_CODIGO_LETRA = "E";

    public const short PERMISO_EXPORT_SIMPLIFICADO = 24;
    public const string PERMISO_EXPORT_SIMPLIFICADO_CODIGO_AFIP = "22";
    public const string PERMISO_EXPORT_SIMPLIFICADO_CODIGO_LETRA = null;

    public const short COMPRA_BIENES_USADOS = 25;
    public const string COMPRA_BIENES_USADOS_CODIGO_AFIP = "30";
    public const string COMPRA_BIENES_USADOS_CODIGO_LETRA = null;

    public const short COMPROBANTE_A_RG_1415 = 26;
    public const string COMPROBANTE_A_RG_1415_CODIGO_AFIP = "34";
    public const string COMPROBANTE_A_RG_1415_CODIGO_LETRA = null;

    public const short COMPROBANTE_B_RG_1415 = 27;
    public const string COMPROBANTE_B_RG_1415_CODIGO_AFIP = "35";
    public const string COMPROBANTE_B_RG_1415_CODIGO_LETRA = null;

    public const short COMPROBANTE_C_RG_1415 = 28;
    public const string COMPROBANTE_C_RG_1415_CODIGO_AFIP = "36";
    public const string COMPROBANTE_C_RG_1415_CODIGO_LETRA = null;

    public const short NOTA_DEBITO_RG_1415 = 29;
    public const string NOTA_DEBITO_RG_1415_CODIGO_AFIP = "37";
    public const string NOTA_DEBITO_RG_1415_CODIGO_LETRA = null;

    public const short NOTA_CREDITO_RG_1415 = 30;
    public const string NOTA_CREDITO_RG_1415_CODIGO_AFIP = "38";
    public const string NOTA_CREDITO_RG_1415_CODIGO_LETRA = null;

    public const short OTRO_COMPROBANTES_A_RG_1415 = 31;
    public const string OTRO_COMPROBANTES_A_RG_1415_CODIGO_AFIP = "39";
    public const string OTRO_COMPROBANTES_A_RG_1415_CODIGO_LETRA = null;

    public const short OTRO_COMPROBANTES_B_RG_1415 = 32;
    public const string OTRO_COMPROBANTES_B_RG_1415_CODIGO_AFIP = "40";
    public const string OTRO_COMPROBANTES_B_RG_1415_CODIGO_LETRA = null;

    public const short OTRO_COMPROBANTES_C_RG_1415 = 33;
    public const string OTRO_COMPROBANTES_C_RG_1415_CODIGO_AFIP = "41";
    public const string OTRO_COMPROBANTES_C_RG_1415_CODIGO_LETRA = null;

    public const short FACTURA_M = 34;
    public const string FACTURA_M_CODIGO_AFIP = "51";
    public const string FACTURA_M_CODIGO_LETRA = "M";

    public const short NOTA_DEBITO_M = 35;
    public const string NOTA_DEBITO_M_CODIGO_AFIP = "52";
    public const string NOTA_DEBITO_M_CODIGO_LETRA = "M";

    public const short NOTA_CREDITO_M = 36;
    public const string NOTA_CREDITO_M_CODIGO_AFIP = "53";
    public const string NOTA_CREDITO_M_CODIGO_LETRA = "M";

    public const short RECIBO_M = 37;
    public const string RECIBO_M_CODIGO_AFIP = "54";
    public const string RECIBO_M_CODIGO_LETRA = null;

    public const short NOTA_VENTA_CONTADO_M = 38;
    public const string NOTA_VENTA_CONTADO_M_CODIGO_AFIP = "55";
    public const string NOTA_VENTA_CONTADO_M_CODIGO_LETRA = null;

    public const short COMPROBANTE_M_RG_1415 = 39;
    public const string COMPROBANTE_M_RG_1415_CODIGO_AFIP = "56";
    public const string COMPROBANTE_M_RG_1415_CODIGO_LETRA = null;

    public const short OTRO_COMPROBANTE_M_RG_1415 = 40;
    public const string OTRO_COMPROBANTE_M_RG_1415_CODIGO_AFIP = "57";
    public const string OTRO_COMPROBANTE_M_RG_1415_CODIGO_LETRA = null;

    public const short CUENTA_VENTA_LIQUIDO_M = 41;
    public const string CUENTA_VENTA_LIQUIDO_M_CODIGO_AFIP = "58";
    public const string CUENTA_VENTA_LIQUIDO_M_CODIGO_LETRA = null;

    public const short LIQUIDACION_M = 42;
    public const string LIQUIDACION_M_CODIGO_AFIP = "59";
    public const string LIQUIDACION_M_CODIGO_LETRA = null;

    public const short CUENTA_VENTA_LIQUIDO_C = 43;
    public const string CUENTA_VENTA_LIQUIDO_C_CODIGO_AFIP = "62";
    public const string CUENTA_VENTA_LIQUIDO_C_CODIGO_LETRA = "C";

    public const short LIQUIDACION_C = 44;
    public const string LIQUIDACION_C_CODIGO_AFIP = "65";
    public const string LIQUIDACION_C_CODIGO_LETRA = null;

    public const short COMPROBANTE_DIARIO_CIERRE = 45;
    public const string COMPROBANTE_DIARIO_CIERRE_CODIGO_AFIP = "80";
    public const string COMPROBANTE_DIARIO_CIERRE_CODIGO_LETRA = null;

    public const short TIQUE_FACTURA_A = 46;
    public const string TIQUE_FACTURA_A_CODIGO_AFIP = "81";
    public const string TIQUE_FACTURA_A_CODIGO_LETRA = null;

    public const short TIQUE_FACTURA_B = 47;
    public const string TIQUE_FACTURA_B_CODIGO_AFIP = "82";
    public const string TIQUE_FACTURA_B_CODIGO_LETRA = null;

    public const short TIQUE = 48;
    public const string TIQUE_CODIGO_AFIP = "83";
    public const string TIQUE_CODIGO_LETRA = null;

    public const short COMPROBANTE_FACTURA_SERVICIOS_PUBLICOS = 49;
    public const string COMPROBANTE_FACTURA_SERVICIOS_PUBLICOS_CODIGO_AFIP = "84";
    public const string COMPROBANTE_FACTURA_SERVICIOS_PUBLICOS_CODIGO_LETRA = null;

    public const short NOTA_CREDITO_SERVICIOS_PUBLICOS = 50;
    public const string NOTA_CREDITO_SERVICIOS_PUBLICOS_CODIGO_AFIP = "85";
    public const string NOTA_CREDITO_SERVICIOS_PUBLICOS_CODIGO_LETRA = null;

    public const short NOTA_DEBITO_SERVICIOS_PUBLICOS = 51;
    public const string NOTA_DEBITO_SERVICIOS_PUBLICOS_CODIGO_AFIP = "86";
    public const string NOTA_DEBITO_SERVICIOS_PUBLICOS_CODIGO_LETRA = null;

    public const short OTRO_COMPROBANTE_SERVICIOS_EXTERIOR = 52;
    public const string OTRO_COMPROBANTE_SERVICIOS_EXTERIOR_CODIGO_AFIP = "87";
    public const string OTRO_COMPROBANTE_SERVICIOS_EXTERIOR_CODIGO_LETRA = null;

    public const short OTRO_COMPROBANTE_DOC_EXCEPTUADOS_NOTA_DEBITO = 53;
    public const string OTRO_COMPROBANTE_DOC_EXCEPTUADOS_NOTA_DEBITO_CODIGO_AFIP = "89";
    public const string OTRO_COMPROBANTE_DOC_EXCEPTUADOS_NOTA_DEBITO_CODIGO_LETRA = null;

    public const short OTRO_COMPROBANTE_DOC_EXCEPTUADOS_NOTA_CREDITO = 54;
    public const string OTRO_COMPROBANTE_DOC_EXCEPTUADOS_NOTA_CREDITO_CODIGO_AFIP = "90";
    public const string OTRO_COMPROBANTE_DOC_EXCEPTUADOS_NOTA_CREDITO_CODIGO_LETRA = null;

    public const short AJ_CONTABLES_QUE_INCREMENTAN_DEBITO_FISCAL = 55;
    public const string AJ_CONTABLES_QUE_INCREMENTAN_DEBITO_FISCAL_CODIGO_AFIP = "92";
    public const string AJ_CONTABLES_QUE_INCREMENTAN_DEBITO_FISCAL_CODIGO_LETRA = null;

    public const short AJ_CONTABLES_QUE_DISMINUYEN_DEBITO_FISCAL = 56;
    public const string AJ_CONTABLES_QUE_DISMINUYEN_DEBITO_FISCAL_CODIGO_AFIP = "93";
    public const string AJ_CONTABLES_QUE_DISMINUYEN_DEBITO_FISCAL_CODIGO_LETRA = null;

    public const short AJ_CONTABLES_QUE_INCREMENTAN_CREDITO_FISCAL = 57;
    public const string AJ_CONTABLES_QUE_INCREMENTAN_CREDITO_FISCAL_CODIGO_AFIP = "94";
    public const string AJ_CONTABLES_QUE_INCREMENTAN_CREDITO_FISCAL_CODIGO_LETRA = null;

    public const short AJ_CONTABLES_QUE_DISMINUYEN_CREDITO_FISCAL = 58;
    public const string AJ_CONTABLES_QUE_DISMINUYEN_CREDITO_FISCAL_CODIGO_AFIP = "95";
    public const string AJ_CONTABLES_QUE_DISMINUYEN_CREDITO_FISCAL_CODIGO_LETRA = null;

    public const short NOTA_CREDITO_C = 59;
    public const string NOTA_CREDITO_C_CODIGO_AFIP = "13";
    public const string NOTA_CREDITO_C_CODIGO_LETRA = "C";

    public const short NOTA_DEBITO_C = 60;
    public const string NOTA_DEBITO_C_CODIGO_AFIP = "12";
    public const string NOTA_DEBITO_C_CODIGO_LETRA = "C";

    public const short NOTA_CREDITO_CDD_A = 61;
    public const string NOTA_CREDITO_CDD_A_CODIGO_AFIP = "98";
    public const string NOTA_CREDITO_CDD_A_CODIGO_LETRA = "X";

    public const short NOTA_CREDITO_CDD_B = 62;
    public const string NOTA_CREDITO_CDD_B_CODIGO_AFIP = "99";
    public const string NOTA_CREDITO_CDD_B_CODIGO_LETRA = "X";

    public const short RECIBO = 63;
    public const string RECIBO_CODIGO_AFIP = "199";
    public const string RECIBO_CODIGO_LETRA = "X";

    public const short ACCOR = 64;
    public const string ACCOR_CODIGO_AFIP = "00";
    public const string ACCOR_CODIGO_LETRA = null;

    public const short REMITO = 65;
    public const string REMITO_CODIGO_AFIP = "91";
    public const string REMITO_CODIGO_LETRA = "R";

    public const short FCE_FACTURA_MIPYMES_A = 66;
    public const string FCE_FACTURA_MIPYMES_A_CODIGO_AFIP = "201";
    public const string FCE_FACTURA_MIPYMES_A_CODIGO_LETRA = "A";

    public const short FCE_NOTA_DEBITO_MIPYMES_A = 67;
    public const string FCE_NOTA_DEBITO_MIPYMES_A_CODIGO_AFIP = "202";
    public const string FCE_NOTA_DEBITO_MIPYMES_A_CODIGO_LETRA = "A";

    public const short FCE_NOTA_CREDITO_MIPYMES_A = 68;
    public const string FCE_NOTA_CREDITO_MIPYMES_A_CODIGO_AFIP = "203";
    public const string FCE_NOTA_CREDITO_MIPYMES_A_CODIGO_LETRA = "A";

    public const short FCE_FACTURA_MIPYMES_B = 69;
    public const string FCE_FACTURA_MIPYMES_B_CODIGO_AFIP = "206";
    public const string FCE_FACTURA_MIPYMES_B_CODIGO_LETRA = "B";

    public const short FCE_NOTA_DEBITO_MIPYMES_B = 70;
    public const string FCE_NOTA_DEBITO_MIPYMES_B_CODIGO_AFIP = "207";
    public const string FCE_NOTA_DEBITO_MIPYMES_B_CODIGO_LETRA = "B";

    public const short FCE_NOTA_CREDITO_MIPYMES_B = 71;
    public const string FCE_NOTA_CREDITO_MIPYMES_B_CODIGO_AFIP = "208";
    public const string FCE_NOTA_CREDITO_MIPYMES_B_CODIGO_LETRA = "B";

    public const short FCE_FACTURA_MIPYMES_C = 72;
    public const string FCE_FACTURA_MIPYMES_C_CODIGO_AFIP = "211";
    public const string FCE_FACTURA_MIPYMES_C_CODIGO_LETRA = "C";

    public const short FCE_NOTA_DEBITO_MIPYMES_C = 73;
    public const string FCE_NOTA_DEBITO_MIPYMES_C_CODIGO_AFIP = "212";
    public const string FCE_NOTA_DEBITO_MIPYMES_C_CODIGO_LETRA = "C";

    public const short FCE_NOTA_CREDITO_MIPYMES_C = 74;
    public const string FCE_NOTA_CREDITO_MIPYMES_C_CODIGO_AFIP = "213";
    public const string FCE_NOTA_CREDITO_MIPYMES_C_CODIGO_LETRA = "C";

    public const short DIF_CAMBIO_F_MAS = 75;
    public const string DIF_CAMBIO_F_MAS_CODIGO_AFIP = "501";
    public const string DIF_CAMBIO_F_MAS_CODIGO_LETRA = "X";

    public const short DIF_CAMBIO_F_MENOS = 76;
    public const string DIF_CAMBIO_F_MENOS_CODIGO_AFIP = "502";
    public const string DIF_CAMBIO_F_MENOS_CODIGO_LETRA = "X";

    public const short DIF_CAMBIO_R_MAS = 77;
    public const string DIF_CAMBIO_R_MAS_CODIGO_AFIP = "503";
    public const string DIF_CAMBIO_R_MAS_CODIGO_LETRA = "X";

    public const short DIF_CAMBIO_R_MENOS = 78;
    public const string DIF_CAMBIO_R_MENOS_CODIGO_AFIP = "504";
    public const string DIF_CAMBIO_R_MENOS_CODIGO_LETRA = "X";

    public const short ESTADO_CUENTA = 79;
    public const string ESTADO_CUENTA_CODIGO_AFIP = "601";
    public const string ESTADO_CUENTA_CODIGO_LETRA = "X";

    public const short ORDEN_PAGO = 80;
    public const string ORDEN_PAGO_CODIGO_AFIP = "200";
    public const string ORDEN_PAGO_CODIGO_LETRA = "X";

    public const short CERTIFICADO = 81;
    public const string CERTIFICADO_CODIGO_AFIP = "901";
    public const string CERTIFICADO_CODIGO_LETRA = "X";

    public const short COTIZACION_ETIQUETAS = 82;
    public const string COTIZACION_ETIQUETAS_CODIGO_AFIP = "902";
    public const string COTIZACION_ETIQUETAS_CODIGO_LETRA = "X";
}
