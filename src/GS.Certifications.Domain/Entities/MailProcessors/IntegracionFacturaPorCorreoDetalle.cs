using GS.Certifications.Domain.Entities.Empresas;
using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.MailProcessors;

public class IntegracionFacturaPorCorreoDetalle : BaseIntEntity
{
    public int IntegracionFacturaPorCorreoId { get; set; }
    public IntegracionFacturaPorCorreo IntegracionFacturaPorCorreo { get; set; }
    public int EmpresaPortalId { get; set; }
    public EmpresaPortal EmpresaPortal { get; set; }
    public string MailsFrom { get; set; }
    public string SubjectKey { get; set; }
    public bool Actvive { get; set; }
}
