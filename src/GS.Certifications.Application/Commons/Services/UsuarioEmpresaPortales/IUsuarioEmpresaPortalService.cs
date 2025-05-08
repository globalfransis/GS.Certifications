using GS.Certifications.Domain.Entities.Seguridad;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Application.Commons.Services.UsuarioEmpresaPortales;

public interface IUsuarioEmpresaPortalService
{
    public Task<UsuarioEmpresaPortal> GetByIdAsync(long usuarioEmpresasPortalId);
    public Task<IEnumerable<UsuarioEmpresaPortal>> GetAllAsync(GetAllRequestDto filter);
}
