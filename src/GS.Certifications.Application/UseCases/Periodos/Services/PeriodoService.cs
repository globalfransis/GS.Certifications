using GSF.Application.Services;
using GSF.Application.Helpers.Pagination.Interfaces;
using System.Threading.Tasks;
using System;
using GS.Certifications.Domain.Entities.Periodos;
using Microsoft.EntityFrameworkCore;
using GS.Certifications.Domain.Entities.Impuestos;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Linq;
using GSF.Application.Common.Exceptions;
using System.Collections.Generic;
using GSF.Application.Security.Services.CurrentCompany;
using GS.Certifications.Application.CQRS.DbContexts;

namespace GS.Certifications.Application.UseCases.Periodos.Services;

public class PeriodoService : BaseGSFService, IPeriodoService
{
    private readonly ICertificationsDbContext _context;
    private readonly ICurrentCompanyService _service;
    public PeriodoService(
        ICertificationsDbContext context,
        ICurrentCompanyService service)
    {
        _context = context;
        _service = service;
    }
    public async Task<Periodo> GetAsync(int id)
    {
        Periodo periodo = await _context.Periodos
            .Include(u => u.Estado)
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

        return periodo;
    }

    public async Task<IPaginatedQueryResult<Periodo>> GetManyAsync(IPeriodoQueryParameter request)
    {
        long companyId = (await _service.GetCurrentCompanyAsync()).Id;
        var colection = _context.Periodos
            .Include(u => u.Estado)
            .Where(src => src.CompanyId == companyId && !src.IsDeleted)
            .AsQueryable();

        if (request.EstadoIdm != null)
        {
            colection = colection.Where(u => u.EstadoIdm == request.EstadoIdm);
        }
        if (request.FechaInicio != null)
        {
            colection = colection.Where(u => u.FechaInicio >= request.FechaInicio);
        }
        if (request.FechaFin != null)
        {
            colection = colection.Where(u => u.FechaFin <= request.FechaFin);
        }

        return await Get(colection, request);
    }
    public async Task<Periodo> CreateAsync(IPeriodoCreate c)
    {
        EstadoPeriodo estado = await _context.EstadosPeriodos.FirstOrDefaultAsync(src => src.Idm == c.EstadoIdm);
        if (estado == null)
            throw new ValidationErrorException("EstadoPeriodo", "No existe el estado de periodo");

        // Validación de solapamiento de fechas
        bool fechasSolapadas = await _context.Periodos.Where(u => !u.IsDeleted).AnyAsync(p =>
            c.FechaInicio >= p.FechaInicio && c.FechaInicio <= p.FechaFin ||
            c.FechaFin >= p.FechaInicio && c.FechaFin <= p.FechaFin);

        if (fechasSolapadas)
            throw new ValidationErrorException("Periodo", "Las fechas de inicio o fin se solapan con otro periodo existente");

        Periodo periodo = new()
        {
            Año = c.Año,
            NumeroPeriodo = c.NumeroPeriodo,
            FechaInicio = c.FechaInicio,
            FechaFin = c.FechaFin,
            Estado = estado
        };
        return periodo;
    }

    public async Task UpdateAsync(IPeriodoUpdate e)
    {
        EstadoPeriodo estado = await _context.EstadosPeriodos.FirstOrDefaultAsync(src => src.Idm == e.EstadoIdm);
        if (estado == null)
            throw new ValidationErrorException("EstadoPeriodo", "No existe el estado de periodo");

        bool fechasSolapadas = await _context.Periodos.Where(u => !u.IsDeleted).AnyAsync(p =>
            (e.FechaInicio >= p.FechaInicio && e.FechaInicio <= p.FechaFin ||
            e.FechaFin >= p.FechaInicio && e.FechaFin <= p.FechaFin) && p.Id != e.Id);

        if (fechasSolapadas)
            throw new ValidationErrorException("Periodo", "Las fechas de inicio o fin se solapan con otro periodo existente");

        Periodo periodo = await GetAsync(e.Id);
        periodo.Año = e.Año;
        periodo.NumeroPeriodo = e.NumeroPeriodo;
        periodo.FechaInicio = e.FechaInicio;
        periodo.FechaFin = e.FechaFin;
        periodo.Estado = estado;
    }

    public async Task DeleteAsync(int id)
    {
        Periodo periodo = await GetAsync(id);
        //periodo.IsDeleted = true;
        _context.Periodos.Remove(periodo); //**PARA REVISION
    }

    public async Task<List<EstadoPeriodo>> GetAllEstadosPeriodos()
    {
        return await _context.EstadosPeriodos.ToListAsync();
    }
}
