using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Seguridad;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Socios.Web.Common.Services.Empresa.Storage;

public class SessionEmpresaRepository : ICurrentEmpresaPortalRepository
{
    private readonly ISession _session;
    private readonly string _sessionKeyUsuarioEmpresaPortal = "UsuarioEmpresaPortal";
    private readonly string _sessionKeyEmpresaPortalRol = "EmpresaPortalRol";

    public SessionEmpresaRepository(IHttpContextAccessor httpContextAccessor)
    {
        _session = httpContextAccessor.HttpContext.Session;
    }

    public UsuarioEmpresaPortal GetUsuarioEmpresaPortal()
    {
        var usuarioEmpresaJson = _session.GetString(_sessionKeyUsuarioEmpresaPortal);
        if (string.IsNullOrEmpty(usuarioEmpresaJson))
        {
            return null;
        }

        return JsonSerializer.Deserialize<UsuarioEmpresaPortal>(usuarioEmpresaJson);
    }

    public void SetUsuarioEmpresaPortal(UsuarioEmpresaPortal usuarioEmpresaPortal)
    {

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };

        string usuarioEmpresaJson = JsonSerializer.Serialize(usuarioEmpresaPortal, options);
        _session.SetString(_sessionKeyUsuarioEmpresaPortal, usuarioEmpresaJson);
    }

    public RolTipo GetRolTipo()
    {
        var rolJson = _session.GetString(_sessionKeyEmpresaPortalRol);
        if (string.IsNullOrEmpty(rolJson))
        {
            return null;
        }

        return JsonSerializer.Deserialize<RolTipo>(rolJson);
    }

    public void SetRolTipo(RolTipo rol)
    {
        string rolJson = JsonSerializer.Serialize(rol);
        _session.SetString(_sessionKeyEmpresaPortalRol, rolJson);
    }
}