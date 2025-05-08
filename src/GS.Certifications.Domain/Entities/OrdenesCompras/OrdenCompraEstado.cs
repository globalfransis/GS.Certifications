using GSF.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Domain.Entities.OrdenesCompras
{
    public class OrdenCompraEstado : BaseFixedShortEntity
    {
        public const short ESTADO_GENERADA_IDM = 1;
        public const short ESTADO_APROBADA_IDM = 2;
        public const short ESTADO_ANULADA_IDM = 3;

        public const int ESTADO_GENERADA_VALOR = 1;
        public const int ESTADO_APROBADA_VALOR = 2;
        public const int ESTADO_ANULADA_VALOR = 3;

        public const string ESTADO_GENERADA_DESCRIPCION = "Generada";
        public const string ESTADO_APROBADA_DESCRIPCION = "Aprobada";
        public const string ESTADO_ANULADA_DESCRIPCION = "Anulada";

        public const string ESTADO_GENERADA_NOMBRE = "Generada";
        public const string ESTADO_APROBADA_NOMBRE = "Aprobada";
        public const string ESTADO_ANULADA_NOMBRE = "Anulada";
        public int Valor { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
