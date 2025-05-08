using System.Collections.Generic;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services;

public class AnalizeRequestDto : IComprobanteAnalysisParameter
{
    public long CompanyId { get; set; }
    public ICollection<ModoAnalisis> Modos { get; set; } = new List<ModoAnalisis>() { ModoAnalisis.QR, ModoAnalisis.OCR_DETALLE, ModoAnalisis.OCR_IMPUESTOS, ModoAnalisis.OCR_CABECERA, ModoAnalisis.MANUAL };
    public int? EmpresaId { get; set; } = null;
}
