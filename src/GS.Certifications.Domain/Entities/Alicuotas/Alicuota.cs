using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Alicuotas
{
    public class Alicuota : BaseFixedShortEntity
    {
        public const short CODIGOTRES_IDM = 1;
        public const short CODIGOCUATRO_IDM = 2;
        public const short CODIGOCINCO_IDM = 3;
        public const short CODIGOSEIS_IDM = 4;
        public const short CODIGOOCHO_IDM = 5;
        public const short CODIGONUEVE_IDM = 6;

        public const string CODIGOTRES_CODIGOAFIP = "0003";
        public const string CODIGOCUATRO_CODIGOAFIP = "0004";
        public const string CODIGOCINCO_CODIGOAFIP = "0005";
        public const string CODIGOSEIS_CODIGOAFIP = "0006";
        public const string CODIGOOCHO_CODIGOAFIP = "0008";
        public const string CODIGONUEVE_CODIGOAFIP = "0009";

        public const decimal CODIGOTRES_VALOR = 0.00m;
        public const decimal CODIGOCUATRO_VALOR = 10.50m;
        public const decimal CODIGOCINCO_VALOR = 21.00m;
        public const decimal CODIGOSEIS_VALOR = 27.00m;
        public const decimal CODIGOOCHO_VALOR = 5.00m;
        public const decimal CODIGONUEVE_VALOR = 2.50m;
        public string CodigoAFIP { get; set; }
        public decimal? Valor { get; set; }
    }
}
