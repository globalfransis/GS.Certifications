using GSFWebFileTransferService.Abstractions.DefaultValueObjects;

namespace GS.Certifications.Application.GSFExtensions.GSFWebFilteTransferService;

public class EmailProcessorFileConfiguration : BasicFileConfigurationTypeGSFWFTS
{
    private const string EmailAdjuntoFileConfig = "EmailAdjuntoFileConfig";

    public static EmailProcessorFileConfiguration Config => FromString(EmailAdjuntoFileConfig);

    public override string[] __ALLOWED_TYPES => new string[] {
                                        EmailAdjuntoFileConfig
                                  };

    protected EmailProcessorFileConfiguration(string configType) : base(configType)
    {
    }

    public new static EmailProcessorFileConfiguration FromString(string configType)
    {
        return new EmailProcessorFileConfiguration(configType);
    }
}