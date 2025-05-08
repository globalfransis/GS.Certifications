using GSF.Domain.Common;
using GSF.Domain.Entities.Companies;
using System.Collections.Generic;

namespace GS.Certifications.Domain.Entities.MailProcessors;

public class IntegracionFacturaPorCorreo : BaseIntEntity
{
    public long CompanyId { get; set; }
    public Company Company { get; set; }
    public string TenantId { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string MailFolder { get; set; }
    public string MailsTo { get; set; }
    public string SubjectKey { get; set; }
    public bool AutoCreateEmpresaPortal { get; set; }
    public bool Actvive { get; set; }
    public List<IntegracionFacturaPorCorreoDetalle> Detalles { get; set; } = new();
}
