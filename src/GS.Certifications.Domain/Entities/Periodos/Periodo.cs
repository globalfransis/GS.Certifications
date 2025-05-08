using GSF.Domain.Common;
using GSF.Domain.Entities.Companies;
using System;

namespace GS.Certifications.Domain.Entities.Periodos
{
    public class Periodo : BaseIntEntity
    {
        public int? Año { get; set; }
        public int? NumeroPeriodo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public short? EstadoIdm { get; set; }
        public EstadoPeriodo Estado { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; }
    }

    public class EstadoPeriodo : BaseFixedShortEntity
    {
        public const short CERRADO_IDM = 1;
        public const short ABIERTO_IDM = 2;
        public const short PRESENTADO_IDM = 3;
        public const short NO_VIGENTE_IDM = 4;

        public const string CERRADO_DESCRIPCION = "Cerrado";
        public const string ABIERTO_DESCRIPCION = "Abierto";
        public const string PRESENTADO_DESCRIPCION = "Presentado";
        public const string NO_VIGENTE_DESCRIPCION = "No Vigente";
        public string Descripcion { get; set; }
    }
}
