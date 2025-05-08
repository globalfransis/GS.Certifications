using GS.Certifications.Application.CQRS.DbContexts;
using Microsoft.EntityFrameworkCore;
using GS.Certifications.Domain.Entities.Seguridad;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Certifications.Application.Commons.Services.UsuarioEmpresaPortales;

public class UsuarioEmpresaPortalService : IUsuarioEmpresaPortalService
{
    private readonly ICertificationsDbContext _context;

    public UsuarioEmpresaPortalService(ICertificationsDbContext context)
    {
        _context = context;
    }

    private IQueryable<UsuarioEmpresaPortal> GetQueryable()
    {
        IQueryable<UsuarioEmpresaPortal> query = _context.UsuarioEmpresasPortales.AsQueryable()
                                                    .Include(uep => uep.User)
                                                    .Include(uep => uep.Roles)
                                                        .ThenInclude(r => r.RolTipo)
                                                    .Include(uep => uep.EmpresaPortal)
                                                        .ThenInclude(ep => ep.Roles)
                                                            .ThenInclude(r => r.RolTipo)
                                                    .Include(uep => uep.EmpresaPortal)
                                                        .ThenInclude(ep => ep.Company)
                                                    .Include(uep => uep.DomainF);
        return query;
    }

    public async Task<UsuarioEmpresaPortal> GetByIdAsync(long usuarioEmpresasPortalId)
    {
        IQueryable<UsuarioEmpresaPortal> query = GetQueryable();
        UsuarioEmpresaPortal uep = await query
            .Where(u => u.Id == usuarioEmpresasPortalId)
            .FirstOrDefaultAsync();

        return uep;
    }

    public async Task<IEnumerable<UsuarioEmpresaPortal>> GetAllAsync(GetAllRequestDto filter)
    {
        IQueryable<UsuarioEmpresaPortal> query = GetQueryable();
        if (filter.UserId is not null) query = query.Where(u => u.UserId == filter.UserId);
        if (filter.EmpresaPortalId is not null) query = query.Where(u => u.EmpresaPortalId == filter.EmpresaPortalId);
        if (filter.Habilitado is not null) query = query.Where(u => u.Habilitado == filter.Habilitado);
        IEnumerable<UsuarioEmpresaPortal> ueps = await query.ToListAsync();

        return ueps;
    }
}
