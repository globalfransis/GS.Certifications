using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.ModosLecturas
{
    public class ModoLectura : BaseFixedShortEntity
    {
        public const short QR_IDM = 1;
        public const short OCR_DETALLE_IDM = 2;
        public const short OCR_IMPUESTOS_IDM = 3;
        public const short OCR_CABECERA_IDM = 4;
        public const short MANUAL_IDM = 5;

        public const string QR_DESCRIPCION = "QR - Cabecera";
        public const string OCR_DETALLE_DESCRIPCION = "OCR - Detalle";
        public const string OCR_IMPUESTOS_DESCRIPCION = "OCR - Impuestos";
        public const string OCR_CABECERA_DESCRIPCION = "OCR - Cabecera";
        public const string MANUAL_DESCRIPCION = "Manual (no se usa IA)";

        public const string QR_CODIGO = "QRC";
        public const string OCR_DETALLE_CODIGO = "OCRD";
        public const string OCR_IMPUESTOS_CODIGO = "OCRI";
        public const string OCR_CABECERA_CODIGO = "OCRC";
        public const string MANUAL_CODIGO = "MAN";

        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }
}
