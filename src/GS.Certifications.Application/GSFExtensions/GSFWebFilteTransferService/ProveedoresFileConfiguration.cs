using GSFWebFileTransferService.Abstractions.DefaultValueObjects;

namespace GS.Certifications.Application.GSFExtensions.GSFWebFilteTransferService;

public class ProveedoresFileConfiguration : BasicFileConfigurationTypeGSFWFTS
{
    private const string ComprobantesValue = "ComprobantesFileConfig";

    public static ProveedoresFileConfiguration Comprobantes => FromString(ComprobantesValue);

    public override string[] __ALLOWED_TYPES => new string[] {
                                        ComprobantesValue
                                  };

    protected ProveedoresFileConfiguration(string configType) : base(configType)
    {
    }

    public new static ProveedoresFileConfiguration FromString(string configType)
    {
        return new ProveedoresFileConfiguration(configType);
    }
}