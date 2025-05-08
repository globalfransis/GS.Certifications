using GS.Certifications.Application.CQRS.DbContexts;
using GSF.Application.Common.Exceptions;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Security.Services.CurrentCompany;
using GSF.Application.Services;
using Microsoft.EntityFrameworkCore;
using GS.Certifications.Domain.Entities.Percepciones;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Percepciones.Services;

public class PercepcionService : BaseGSFService, IPercepcionService
{
    private readonly ICertificationsDbContext _context;
    private readonly ICurrentCompanyService _currentCompanyService;

    public PercepcionService(ICertificationsDbContext context, ICurrentCompanyService currentCompanyService)
    {
        _context = context;
        _currentCompanyService = currentCompanyService;
    }
    public async Task<Percepcion> GetAsync(int id)
    {
        Percepcion percepcion = await _context.Percepciones
            .Include(u => u.Company)
            .Include(u => u.Provincia)
            .Include(u => u.PercepcionTipo)
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

        return percepcion;
    }

    public async Task<IPaginatedQueryResult<Percepcion>>
        GetManyAsync(IPercepcionQueryParameter request)
    {
        long companyId = (await _currentCompanyService.GetCurrentCompanyAsync()).Id;
        var colection = _context.Percepciones
            .Include(u => u.Company)
            .Include(u => u.Provincia)
            .Include(u => u.PercepcionTipo)
            .Where(u => u.CompanyId == companyId && !u.IsDeleted).AsQueryable();

        if (!string.IsNullOrEmpty(request.Descripcion))
        {
            colection = colection.Where(u => u.Descripcion.Contains(request.Descripcion));
        }
        if (request.ProvinciaId != null)
        {
            colection = colection.Where(u => u.ProvinciaId == request.ProvinciaId);
        }
        if (request.PercepcionTipoId != null)
        {
            colection = colection.Where(u => u.PercepcionTipoId == request.PercepcionTipoId);
        }

        return await Get(colection, request);
    }
    public async Task<Percepcion> CreateAsync(IPercepcionCreate c)
    {
        if (!_context.ImpuestosTipos.Any(src => src.Idm == c.PercepcionTipoId))
            throw new ValidationErrorException("PercepcionTipo", "No existe el tipo de percepcion");

        Percepcion percepcion = new()
        {
            Descripcion = c.Descripcion,
            PercepcionTipoId = c.PercepcionTipoId,
            ProvinciaId = c.ProvinciaId,
        };
        return percepcion;
    }

    public async Task UpdateAsync(IPercepcionUpdate e)
    {
        if (!_context.ImpuestosTipos.Any(src => src.Idm == e.PercepcionTipoId))
            throw new ValidationErrorException("PercepcionTipo", "No existe el tipo de percepcion");

        Percepcion percepcion = await GetAsync(e.Id);
        percepcion.Descripcion = e.Descripcion;
        percepcion.PercepcionTipoId = e.PercepcionTipoId;
        percepcion.ProvinciaId = e.ProvinciaId;
    }

    public async Task DeleteAsync(int id)
    {
        if (_context.PercepcionDetalles.Any(src => src.PercepcionId == id && !src.IsDeleted))
            throw new ValidationErrorException("PercepcionDetalle", "Existe un comprobante que utiliza la percepcion");

        Percepcion percepcion = await GetAsync(id);
        //percepcion.IsDeleted = true;
        _context.Percepciones.Remove(percepcion); //**PARA REVISION
    }

    public async Task<List<PercepcionTipo>> GetAllPercepcionesTiposCuentas()
    {
        return await _context.PercepcionesTipos.ToListAsync();
    }
}
