
namespace HostCore.Dtos.Session;

public record SessionDataDto
{
    public long UserId { get; set; }
    public long CompanyId { get; set; }
    public int EmpresaPortalId { get; set; }
    public long RoleId { get; set; }
}
