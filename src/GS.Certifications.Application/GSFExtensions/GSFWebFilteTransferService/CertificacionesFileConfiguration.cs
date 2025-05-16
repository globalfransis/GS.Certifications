using GSFWebFileTransferService.Abstractions.DefaultValueObjects;

namespace GS.Certifications.Application.GSFExtensions.GSFWebFilteTransferService;

public class CertificacionesFileConfiguration : BasicFileConfigurationTypeGSFWFTS
{
    private const string DocumentoSolicitudCertificacionValue = "DocumentoSolicitudCertificacionFileConfig";

    public static CertificacionesFileConfiguration DocumentoSolicitudCertificacion => FromString(DocumentoSolicitudCertificacionValue);

    public override string[] __ALLOWED_TYPES => new string[] {
                                        DocumentoSolicitudCertificacionValue
                                  };

    protected CertificacionesFileConfiguration(string configType) : base(configType)
    {
    }

    public new static CertificacionesFileConfiguration FromString(string configType)
    {
        return new CertificacionesFileConfiguration(configType);
    }
}