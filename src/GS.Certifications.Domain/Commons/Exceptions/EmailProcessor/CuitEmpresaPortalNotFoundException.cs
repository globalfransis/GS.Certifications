namespace GS.Certifications.Domain.Commons.Exceptions.EmailProcessor;

public class CuitEmpresaPortalNotFoundException : EmailProcessorInvalidOperationException
{
    public override string StatusCode { get; } = "EP_CUIT_N_FND";
    public string Cuit { get; }
    public CuitEmpresaPortalNotFoundException(string cuit)
        : base($"No se pudo determinar la empresa portal para el cuit {cuit}.")
    {
        Cuit = cuit;
    }
}
