using GS.Certifications.Domain.Entities.Comprobantes;

namespace GS.Certifications.Domain.Commons.Exceptions.EmailProcessor;

public class DuplicateComprobanteException : EmailProcessorInvalidOperationException
{
    public override string StatusCode { get; } = "CMP_DUP";
    public Comprobante Comprobante { get; }
    public DuplicateComprobanteException(Comprobante comprobante)
        : base("Comprobante duplicado.")
    {
        Comprobante = comprobante;
    }
}
