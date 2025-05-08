using GSF.Application.Services;
using GSF.Application.Helpers.Pagination.Interfaces;
using System.Threading.Tasks;
using System;
using GS.Certifications.Domain.Entities.ConceptosGastosTipos;
using GSF.Application.Security.Services.CurrentCompany;
using GSF.Application.Common.Exceptions;
using System.Linq;
using GS.Certifications.Domain.Entities.OrdenesCompras;
using GS.Certifications.Application.Commons.Dtos.OrdenesCompra;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using GS.Certifications.Application.CQRS.DbContexts;

namespace GS.Certifications.Application.UseCases.ConceptosGastosTipos.Services;

public class ConceptoGastoTipoService : BaseGSFService, IConceptoGastoTipoService
{
    private readonly ICertificationsDbContext _context;
    private readonly ICurrentCompanyService _currentCompanyService;
    public ConceptoGastoTipoService(ICertificationsDbContext context, ICurrentCompanyService currentCompanyService)
    {
        _context = context;
        _currentCompanyService = currentCompanyService;
    }
    public async Task<ConceptoGastoTipo> GetAsync(int id)
    {
        return await _context.ConceptosGastosTipos
             .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
    }

    public async Task<IPaginatedQueryResult<ConceptoGastoTipo>> GetManyAsync(IConceptoGastoTipoQueryParameter request)
    {
        long companyId = (await _currentCompanyService.GetCurrentCompanyAsync()).Id;
        //long companyId = 39;
        var colection = _context.ConceptosGastosTipos
            .Include(u => u.Company)
            .Where(src => src.CompanyId == companyId && !src.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrEmpty(request.Nombre))
        {
            colection = colection.Where(u => u.Nombre.Contains(request.Nombre));
        }

        return await Get(colection, request);
    }
    public async Task<ConceptoGastoTipo> CreateAsync(IConceptoGastoTipoCreate c)
    {
        if (_context.ConceptosGastosTipos.Any(src => src.Nombre == c.Nombre && !src.IsDeleted))
            throw new ValidationErrorException("Nombre", "Existe un tipo de Concepto de Gasto con el mismo nombre");

        ConceptoGastoTipo conceptoGastoTipo = new()
        {
            Nombre = c.Nombre,
            Descripcion = c.Descripcion,
            ConceptoContableNombre = c.ConceptoContableNombre,
            ConceptoContableValor = c.ConceptoContableValor
        };
        return conceptoGastoTipo;
    }

    public async Task UpdateAsync(IConceptoGastoTipoUpdate e)
    {
        ConceptoGastoTipo conceptoGastoTipo = await GetAsync(e.Id);

        if (_context.ConceptosGastosTipos.Any(src => src.Nombre == e.Nombre && src.Id != e.Id && !src.IsDeleted))
            throw new ValidationErrorException("Nombre", "Existe un tipo de Concepto de Gasto con el mismo nombre");

        conceptoGastoTipo.Nombre = e.Nombre;
        conceptoGastoTipo.Descripcion = e.Descripcion;
        conceptoGastoTipo.ConceptoContableNombre = e.ConceptoContableNombre;
        conceptoGastoTipo.ConceptoContableValor = e.ConceptoContableValor;
    }

    public async Task DeleteAsync(int id)
    {

        ConceptoGastoTipo conceptoGastoTipo = await GetAsync(id);

        //ordenCompraTpo.IsDeleted = true;
        _context.ConceptosGastosTipos.Remove(conceptoGastoTipo); //**PARA REVISION
    }

    public async Task<List<ConceptoGastoTipo>> GetAllConceptosGastosTipos()
    {
        long companyId = (await _currentCompanyService.GetCurrentCompanyAsync()).Id;
        return await _context.ConceptosGastosTipos
            .Include(u => u.Company)
            .Where(src => src.CompanyId == companyId && !src.IsDeleted).ToListAsync();
    }
}
