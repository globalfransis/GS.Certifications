using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Application.Interfaces.EmailProcessor;

public interface IEmailInvoiceService
{
    public Task<List<EmailInvoiceAttachmentDto>> GetEmailInvoiceAttachmentsAsync(long companyId);
}
