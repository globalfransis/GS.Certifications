using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Certificaciones.Documentos;

public class DocumentoRequerido : BaseIntEntity
{
    public int CertificacionId { get; set; }
    public Certificacion Certificacion { get; set; }
    public short TipoId { get; set; }
    public TipoDocumento Tipo { get; set; }
    public bool Obligatorio { get; set; }
    public bool SobreescribeVigencia { get; set; }
    public int? VigenciaDiasCustom { get; set; }
}
