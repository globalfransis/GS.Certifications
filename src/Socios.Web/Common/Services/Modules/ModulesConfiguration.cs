namespace Socios.Web.Common.Services.Modules;

public record ModulesConfiguration
{
    public List<Module> Modules { get; set; }
    public bool Active { get; set; }

}

public record Module
{
    public int RoleId { get; set; }
    public string ModuleName { get; set; }
    public string ApiKey { get; set; }
    public string HandShakeUrl { get; set; }
    public string LandingUrl { get; set; }
    public bool Active { get; set; }
}
