using GSF.Domain.Entities.Security;

namespace Socios.Web.Common.Services.Modules;

public interface IModulesService
{
    public List<Module> GetAll(bool? active = null);
    public Module? GetByRole(int roleId);
    public Module? GetByRole(Role role);
    public List<Module> GetByRoles(List<Role> roles, bool? active = null);
    public List<Module> GetByRoles(List<int> roles, bool? active = null);

    public bool IsActive();
}
