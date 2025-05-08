using GS.Certifications.Domain.Entities.Alicuotas;
using GS.Certifications.Domain.Entities.Comprobantes;
using GSF.Domain.Common;
using GSF.Domain.Entities.Companies;
using GSF.Domain.Entities.Geo;

namespace GS.Certifications.Domain.Entities.Impuestos
{
    public class Impuesto : BaseIntEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public short? TipoId { get; set; }
        public ImpuestoTipo Tipo { get; set; }
        public long? ProvinciaId { get; set; }
        public Province Provincia { get; set; }
        public long? CompanyId { get; set; }
        public Company Company { get; set; }
        public short? AlicuotaId { get; set; }
        public Alicuota Alicuota { get; set; }
        public decimal? Valor { get; set; }
    }

    public class ImpuestoDetalle : BaseIntEntity
    {
        public int ComprobanteId { get; set; }
        public Comprobante Comprobante { get; set; }
        public int ImpuestoId { get; set; }
        public Impuesto Impuesto { get; set; }
        public string Descripcion { get; set; }
        public decimal ImporteTotal { get; set; } = default;
        public decimal Alicuota { get; set; } = 0;
    }


    public class ImpuestoTipo : BaseFixedShortEntity
    {
        public const short IVA = 1;
        public const short INTERNO = 2;
        public const short PROVINCIAL = 3;

        public string Valor { get; set; }
        public string Descripcion { get; set; }
        public bool General { get; set; } = false;
    }
}