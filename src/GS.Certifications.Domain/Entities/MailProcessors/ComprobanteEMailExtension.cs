using GSF.Domain.Common;
using System;

namespace GS.Certifications.Domain.Entities.MailProcessors;

public class ComprobanteEMailExtension : BaseIntEntity
{
    public long ProcesoId { get; set; }
    public long CompanyId { get; set; }
    public string EmpresaPortalGuid { get; set; }
    public string ComprobanteGuid { get; set; }
    public string EmailId { get; set; }
    public string EmailSubject { get; set; }
    public string EmailFrom { get; set; }
    public DateTimeOffset? EmailReceivedDateTime { get; set; }
    public string FileName { get; set; }
    public string FileId { get; set; }
    public string Observaciones { get; set; }
    public string StatusCode { get; set; }
    public string NombreArchivo { get; set; }
}
