namespace GS.Certifications.Domain.Commons.Exceptions.EmailProcessor;

public class CuitEmpresaPortalNotReadedException : EmailProcessorInvalidOperationException
{
    public override string StatusCode { get; } = "EP_CUIT_N_RD";
    public CuitEmpresaPortalNotReadedException() : base($"No se pudo leer el cuit.") { }
}
