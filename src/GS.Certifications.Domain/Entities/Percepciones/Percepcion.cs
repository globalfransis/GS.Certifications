using GS.Certifications.Domain.Entities.Comprobantes;
using GSF.Domain.Common;
using GSF.Domain.Entities.Companies;
using GSF.Domain.Entities.Geo;

namespace GS.Certifications.Domain.Entities.Percepciones;

public class Percepcion : BaseIntEntity
{
    public short? PercepcionTipoId { get; set; }
    public PercepcionTipo PercepcionTipo { get; set; }
    public long? ProvinciaId { get; set; }
    public Province Provincia { get; set; }
    public string Descripcion { get; set; }
    public long? CompanyId { get; set; }
    public Company Company { get; set; }
}

public class PercepcionDetalle : BaseIntEntity
{
    public int ComprobanteId { get; set; }
    public Comprobante Comprobante { get; set; }
    public int PercepcionId { get; set; }
    public Percepcion Percepcion { get; set; }
    public string Descripcion { get; set; }
    public decimal Alicuota { get; set; } = default;
    public decimal ImporteTotal { get; set; } = default;
}