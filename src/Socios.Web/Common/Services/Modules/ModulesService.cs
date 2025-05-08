using GSF.Domain.Entities.Security;

namespace Socios.Web.Common.Services.Modules;

public class ModulesService : IModulesService
{
    private readonly ModulesConfiguration _modulesConfiguration;

    private List<Module> _modules;
    public ModulesService(ModulesConfiguration modulesConfiguration)
    {
        _modulesConfiguration = modulesConfiguration;
    }

    public List<Module> GetAll(bool? active = null)
    {
        if (_modules == null) _modules = _modulesConfiguration.Modules.Where(m => active is null || m.Active == active).ToList();
        return _modules;
    }
    public Module? GetByRole(int roleId)
    {
        if (_modules == null) _modules = _modulesConfiguration.Modules;
        return _modules.FirstOrDefault(m => m.RoleId == roleId);
    }

    public Module? GetByRole(Role role)
    {
        if (_modules == null) _modules = _modulesConfiguration.Modules;
        return _modules.FirstOrDefault(m => m.RoleId == role.Id);
    }

    public List<Module> GetByRoles(List<Role> roles, bool? active = null)
    {
        if (_modules == null) _modules = _modulesConfiguration.Modules;
        return _modules.Where(m => roles.Any(r => m.RoleId == r.Id && (active is null || m.Active == active))).ToList();
    }

    public List<Module> GetByRoles(List<int> roles, bool? active = null)
    {
        if (_modules == null) _modules = _modulesConfiguration.Modules;
        return _modules.Where(m => roles.Any(r => m.RoleId == r && (active is null || m.Active == active))).ToList();
    }

    public bool IsActive()
    {
        return _modulesConfiguration.Active;
    }
}
