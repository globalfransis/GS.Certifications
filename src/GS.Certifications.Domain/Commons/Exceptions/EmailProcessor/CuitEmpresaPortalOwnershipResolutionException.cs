namespace GS.Certifications.Domain.Commons.Exceptions.EmailProcessor;

public class CuitEmpresaPortalOwnershipResolutionException : EmailProcessorInvalidOperationException
{
    public override string StatusCode { get; } = "EP_CUIT_OWN";
    public string Cuit { get; }

    public CuitEmpresaPortalOwnershipResolutionException(string cuit)
        : base($"No se pudo determinar univocamente el socio para el cuit {cuit}.")
    {
        Cuit = cuit;
    }

}
