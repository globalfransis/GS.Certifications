using GS.Certifications.Domain.Entities.Empresas;

namespace GS.Certifications.Domain.Commons.Exceptions.EmailProcessor;

public class EmpresaPortalNotAllowedForIntegracionFacturaPorCorreoException : EmailProcessorInvalidOperationException
{
    public override string StatusCode { get; } = "EP_N_ALW";
    public EmpresaPortal EmpresaPortal { get; }
    public EmpresaPortalNotAllowedForIntegracionFacturaPorCorreoException(EmpresaPortal empresaPortal)
        : base($"La empresa portal: {empresaPortal.Guid}, no está habilitada para la integración de facturas por correo.")
    {
        EmpresaPortal = empresaPortal;
    }

}
