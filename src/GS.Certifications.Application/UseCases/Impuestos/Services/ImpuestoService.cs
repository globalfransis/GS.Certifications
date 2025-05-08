using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using GS.Certifications.Application.CQRS.DbContexts;
using GSF.Application.Common.Exceptions;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Security.Services.CurrentCompany;
using GSF.Application.Services;
using Microsoft.EntityFrameworkCore;
using GS.Certifications.Domain.Entities.Alicuotas;
using GS.Certifications.Domain.Entities.Impuestos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Impuestos.Services;

public class ImpuestoService : BaseGSFService, IImpuestoService
{
    private readonly ICertificationsDbContext _context;
    private readonly ICurrentCompanyService _currentCompanyService;

    public ImpuestoService(ICertificationsDbContext context, ICurrentCompanyService currentCompanyService)
    {
        _context = context;
        _currentCompanyService = currentCompanyService;
    }
    public async Task<Impuesto> GetAsync(int id)
    {
        Impuesto impuesto = await _context.Impuestos
            .Include(u => u.Alicuota)
            .Include(u => u.Company)
            .Include(u => u.Provincia)
            .Include(u => u.Tipo)
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

        return impuesto;
    }

    public async Task<IPaginatedQueryResult<Impuesto>> GetManyAsync(IImpuestoQueryParameter request)
    {
        long companyId = (await _currentCompanyService.GetCurrentCompanyAsync()).Id;
        var colection = _context.Impuestos
            .Include(u => u.Alicuota)
            .Include(u => u.Company)
            .Include(u => u.Provincia)
            .Include(u => u.Tipo)
            .Where(u => u.CompanyId == companyId && !u.IsDeleted).AsQueryable();

        if (!string.IsNullOrEmpty(request.CodigoAFIP))
        {
            colection = colection.Where(u =>
            u.Alicuota != null && u.Alicuota.CodigoAFIP.Contains(request.CodigoAFIP));
        }
        if (request.Valor != null)
        {
            colection = colection.Where(u =>
            u.Alicuota == null && u.Valor == request.Valor ||
            u.Alicuota != null && u.Alicuota.Valor == request.Valor);
        }
        if (request.ProvinciaId != null)
        {
            colection = colection.Where(u => u.ProvinciaId == request.ProvinciaId);
        }
        if (request.TipoId != null)
        {
            colection = colection.Where(u => u.TipoId == request.TipoId);
        }

        return await Get(colection, request);

    }
    public async Task<Impuesto> CreateAsync(IImpuestoCreate c)
    {
        if (!_context.Alicuotas.Any(src => src.Idm == c.AlicuotaId) && c.Valor == null)
            throw new ValidationErrorException("Alicuota", "No existe la Alicuota");
        if (!_context.ImpuestosTipos.Any(src => src.Idm == c.TipoId))
            throw new ValidationErrorException("TipoImpuesto", "No existe el tipo de impuesto");

        Impuesto impuesto = new()
        {
            Nombre = c.Nombre,
            Descripcion = c.Descripcion,
            TipoId = c.TipoId,
            ProvinciaId = c.ProvinciaId,
            AlicuotaId = c.AlicuotaId,
            Valor = c.Valor
        };
        return impuesto;
    }
    public async Task UpdateAsync(IImpuestoUpdate e)
    {
        if (!_context.Alicuotas.Any(src => src.Idm == e.AlicuotaId) && e.Valor == null)
            throw new ValidationErrorException("Alicuota", "No existe la Alicuota");
        if (!_context.ImpuestosTipos.Any(src => src.Idm == e.TipoId))
            throw new ValidationErrorException("TipoImpuesto", "No existe el tipo de impuesto");

        Impuesto impuesto = await GetAsync(e.Id);
        impuesto.Nombre = e.Nombre;
        impuesto.Descripcion = e.Descripcion;
        impuesto.TipoId = e.TipoId;
        impuesto.ProvinciaId = e.ProvinciaId;
        impuesto.AlicuotaId = e.AlicuotaId;
        impuesto.Valor = e.Valor;
    }

    public async Task DeleteAsync(int id)
    {
        if (_context.ImpuestoDetalles.Any(src => src.ImpuestoId == id && !src.IsDeleted))
            throw new ValidationErrorException("ImpuestoDetalle", "Existe un comprobante que utiliza el impuesto");

        Impuesto impuesto = await GetAsync(id);
        //impuesto.IsDeleted = true;
        _context.Impuestos.Remove(impuesto); //**PARA REVISION
    }

    public async Task<List<ImpuestoTipo>> GetAllImpuestosTiposCuentas()
    {
        return await _context.ImpuestosTipos.ToListAsync();
    }

    public async Task<List<Alicuota>> GetAllAlicuotasCuentas()
    {
        return await _context.Alicuotas.ToListAsync();
    }

    public async Task<IPaginatedQueryResult<Alicuota>> GetManyAlicuotasAsync(IAlicuotasQueryParameter request)
    {
        var colection = _context.Alicuotas.AsQueryable();

        if (!string.IsNullOrEmpty(request.CodigoAFIP))
        {
            colection = colection.Where(a => a.CodigoAFIP.Contains(request.CodigoAFIP));
        }
        if (request.Valor != null)
        {
            colection = colection.Where(a => a.Valor == a.Valor);
        }

        return await Get(colection, request);
    }
}
