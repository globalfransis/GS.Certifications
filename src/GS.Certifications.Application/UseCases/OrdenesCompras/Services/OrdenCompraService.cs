using GSF.Application.Services;
using GSF.Application.Helpers.Pagination.Interfaces;
using System.Threading.Tasks;
using System;
using GS.Certifications.Domain.Entities.OrdenesCompras;
using GSF.Application.Security.Services.CurrentCompany;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using GS.Certifications.Domain.Entities.Empresas;
using GSF.Domain.Entities.Global;
using DocumentFormat.OpenXml.Office2010.Excel;
using GSF.Application.Common.Exceptions;
using System.Collections.Generic;
using static ClosedXML.Excel.XLPredefinedFormat;
using GS.Certifications.Application.Commons.Dtos.OrdenesCompra;
using GS.Certifications.Domain.Entities.Empresas.Roles;
using DocumentFormat.OpenXml.InkML;
using GS.Certifications.Application.CQRS.DbContexts;

namespace GS.Certifications.Application.UseCases.OrdenesCompras.Services;

public class OrdenCompraService : BaseGSFService, IOrdenCompraService
{
    private readonly ICertificationsDbContext _context;
    private readonly ICurrentCompanyService _currentCompanyService;

    private const int ESTADO_GENERADA_IDM = OrdenCompraEstado.ESTADO_GENERADA_IDM;

    private const int CARACTERISTICA_ES_ABIERTA = 1;
    private const int CARACTERISTICA_ES_RECURRENTE = 2;
    private const int CARACTERISTICA_ES_UNICA = 3;

    public OrdenCompraService(ICertificationsDbContext context, ICurrentCompanyService currentCompanyService)
    {
        _context = context;
        _currentCompanyService = currentCompanyService;
    }
    public async Task<OrdenCompra> GetAsync(int id)
    {
        OrdenCompra ordenCompra = await _context.OrdenesCompras
            .Include(u => u.OrdenCompraTipo)
            .Include(u => u.OrdenCompraEstado)
            .Include(u => u.EmpresaPortal)
            .Include(u => u.Moneda)
            .Include(u => u.Company)
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

        return ordenCompra;
    }

    public async Task<IPaginatedQueryResult<OrdenCompra>> GetManyAsync(IOrdenCompraQueryParameter request)
    {
        long companyId = (await _currentCompanyService.GetCurrentCompanyAsync()).Id;
        //long companyId = 39;
        var colection = _context.OrdenesCompras
            .Include(u => u.OrdenCompraTipo)
            .Include(u => u.OrdenCompraEstado)
            .Include(u => u.EmpresaPortal)
            .Include(u => u.Moneda)
            .Include(u => u.Company)
            .Where(src => src.CompanyId == companyId && !src.IsDeleted)
            .AsQueryable();

        if (request.EmpresaPortalId != null)
        {
            colection = colection.Where(u => u.EmpresaPortalId == request.EmpresaPortalId);
        }
        if (request.OrdenCompraTipoId != null)
        {
            colection = colection.Where(u => u.OrdenCompraTipoId == request.OrdenCompraTipoId);
        }
        if (request.OrdenCompraEstadoIdm != null)
        {
            colection = colection.Where(u => u.OrdenCompraEstado.Idm == request.OrdenCompraEstadoIdm);
        }
        if (request.EmpresaPortalId != null)
        {
            colection = colection.Where(u => u.EmpresaPortalId == request.EmpresaPortalId);
        }
        if (request.FechaDesde != null)
        {
            colection = colection.Where(u => u.Fecha >= request.FechaDesde);
        }
        if (request.FechaHasta != null)
        {
            colection = colection.Where(u => u.Fecha <= request.FechaHasta);
        }
        return await Get(colection, request);
    }
    public async Task<OrdenCompra> CreateAsync(IOrdenCompraCreate c)
    {
        EmpresaPortal empresaPortal = null;
        if (c.EmpresaPortalId != null)
        {
            empresaPortal = await _context.EmpresasPortales
                .FirstOrDefaultAsync(src => src.Id == c.EmpresaPortalId);
            if (empresaPortal == null)
                throw new ValidationErrorException("EmpresaPortal", "No existe el Socio");
        }

        OrdenCompraEstado ordenCompraEstado = await _context.OrdenesComprasEstados
        .FirstOrDefaultAsync(src => src.Idm == c.OrdenCompraEstadoIdm);
        if (ordenCompraEstado == null)
            throw new ValidationErrorException("OrdenCompraEstado", "No existe el Estado de Documento de Compra");

        OrdenCompraTipo ordenCompraTipo = await _context.OrdenesComprasTipos
            .FirstOrDefaultAsync(src => src.Id == c.OrdenCompraTipoId);
        if (ordenCompraTipo == null)
            throw new ValidationErrorException("OrdenCompraTipo", "No existe el Tipo de Documento de Compra");

        OrdenCompra ordenCompra = new()
        {
            NumeroOrden = c.NumeroOrden,
            Fecha = c.Fecha,
            EmpresaPortal = empresaPortal,
            OrdenCompraTipo = ordenCompraTipo,
            OrdenCompraEstado = ordenCompraEstado,
            CodigoHES = null,//c.CodigoHES,
            CodigoQAD = c.CodigoQAD,
            Importe = c.Importe,
            MonedaId = c.MonedaId,
            Observaciones = c.Observaciones
        };
        return ordenCompra;
    }

    public async Task UpdateAsync(IOrdenCompraUpdate e)
    {
        OrdenCompra ordenCompra = await GetAsync(e.Id);

        EmpresaPortal empresaPortal = ordenCompra.EmpresaPortal;
        if (e.EmpresaPortalId != null)
        {
            empresaPortal = await _context.EmpresasPortales
            .FirstOrDefaultAsync(src => src.Id == e.EmpresaPortalId);
            if (empresaPortal == null)
                throw new ValidationErrorException("EmpresaPortal", "No existe el Socio");
        }

        OrdenCompraEstado ordenCompraEstado = await _context.OrdenesComprasEstados
            .FirstOrDefaultAsync(src => src.Idm == e.OrdenCompraEstadoIdm);
        if (ordenCompraEstado == null)
            throw new ValidationErrorException("OrdenCompraEstado", "No existe el Estado de Documento de Compra");

        OrdenCompraTipo ordenCompraTipo = await _context.OrdenesComprasTipos
            .FirstOrDefaultAsync(src => src.Id == e.OrdenCompraTipoId);
        if (ordenCompraTipo == null)
            throw new ValidationErrorException("OrdenCompraTipo", "No existe el Tipo de Documento de Compra");

        ordenCompra.NumeroOrden = e.NumeroOrden;
        ordenCompra.Fecha = e.Fecha;
        ordenCompra.EmpresaPortal = empresaPortal;
        ordenCompra.OrdenCompraTipo = ordenCompraTipo;
        //ordenCompra.CodigoHES = e.CodigoHES;
        ordenCompra.CodigoQAD = e.CodigoQAD;
        ordenCompra.OrdenCompraEstado = ordenCompraEstado;
        ordenCompra.Importe = e.Importe;
        ordenCompra.MonedaId = e.MonedaId;
        ordenCompra.Observaciones = e.Observaciones;
    }

    public async Task DeleteAsync(int id)
    {

        OrdenCompra ordenCompra = await GetAsync(id);

        //ordenCompra.IsDeleted = true;
        _context.OrdenesCompras.Remove(ordenCompra); //*PARA REVISION
    }

    public async Task<OrdenCompraTipo> GetTipoAsync(int id)
    {
        OrdenCompraTipo ordenCompraTipo = await _context.OrdenesComprasTipos
            .Include(u => u.Company)
            .Include(u => u.Grupos)
                .ThenInclude(u => u.Grupo)
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

        return ordenCompraTipo;
    }
    public async Task<IPaginatedQueryResult<OrdenCompraTipo>> GetManyTiposAsync(IOrdenCompraTipoQueryParameter request)
    {
        long companyId = (await _currentCompanyService.GetCurrentCompanyAsync()).Id;
        var colection = _context.OrdenesComprasTipos
            .Include(u => u.Company)
            .Where(src => src.CompanyId == companyId && !src.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrEmpty(request.Nombre))
        {
            colection = colection.Where(u => u.Nombre.Contains(request.Nombre));
        }
        if (request.Caracteristica != null)
        {
            if (request.Caracteristica == CARACTERISTICA_ES_ABIERTA)
                colection = colection.Where(u => u.EsAbierta);
            else if (request.Caracteristica == CARACTERISTICA_ES_RECURRENTE)
                colection = colection.Where(u => u.EsRecurrente);
            else if (request.Caracteristica == CARACTERISTICA_ES_UNICA)
                colection = colection.Where(u => u.EsUnica);
        }
        return await Get(colection, request);
    }
    public async Task<OrdenCompraTipo> CreateTipoAsync(IOrdenCompraTipoCreate t)
    {
        long companyId = (await _currentCompanyService.GetCurrentCompanyAsync()).Id;

        if (t.Nombre == null)
            throw new ValidationErrorException("Nombre", "El campo 'Nombre' es obligatorio.");

        if (_context.OrdenesComprasTipos.Any(src => src.Nombre == t.Nombre && src.CompanyId == companyId && !src.IsDeleted))
            throw new ValidationErrorException("Nombre", "Existe un tipo de Documento con el mismo nombre");

        if (!t.EsAbierta && !t.EsRecurrente && !t.EsUnica)
            throw new ValidationErrorException("Caracteristica", "Debe seleccionar al menos una característica (Abierta, Recurrente o Única).");

        OrdenCompraTipo ordenCompra = new()
        {
            Nombre = t.Nombre,
            Descripcion = t.Descripcion,
            EsRequerida = t.EsRequerida,
            EsAbierta = t.EsAbierta,
            EsRecurrente = t.EsRecurrente,
            EsUnica = t.EsUnica
        };

        List<GrupoOrdenCompraTipo> listaGrupos = new List<GrupoOrdenCompraTipo>();

        foreach (short id in t.GruposId)
        {
            GrupoOrdenCompraTipo empresaRol = new()
            {
                GrupoId = id,
            };
            listaGrupos.Add(empresaRol);
        }
        ordenCompra.Grupos = listaGrupos;

        return ordenCompra;
    }
    public async Task UpdateTipoAsync(IOrdenCompraTipoUpdate p)
    {
        OrdenCompraTipo ordenCompraTipo = await GetTipoAsync(p.Id);

        if (p.Nombre == null)
            throw new ValidationErrorException("Nombre", "El campo 'nombre' es obligatorio.");

        if (_context.OrdenesComprasTipos.Any
            (src => src.Nombre == p.Nombre && src.CompanyId == ordenCompraTipo.CompanyId && src.Id != p.Id && !src.IsDeleted))
            throw new ValidationErrorException("Nombre", "Existe un tipo de Documento con el mismo nombre");

        if (!p.EsAbierta && !p.EsRecurrente && !p.EsUnica)
            throw new ValidationErrorException("Caracteristica", "Debe seleccionar al menos una característica (Abierta, Recurrente o Única).");

        List<GrupoOrdenCompraTipo> listaGrupos = ordenCompraTipo.Grupos.ToList();
        if (p.GruposId.Count != 0)
        {
            foreach (short id in p.GruposId)
            {
                if (!ordenCompraTipo.Grupos.Any(r => r.GrupoId == id))
                {
                    GrupoOrdenCompraTipo grupoOrdenCompraTipo = new()
                    {
                        GrupoId = id,
                    };
                    listaGrupos.Add(grupoOrdenCompraTipo);
                }
            }
        }

        foreach (GrupoOrdenCompraTipo grupoOrdenCompraTipo in ordenCompraTipo.Grupos)
        {
            if (!p.GruposId.Any(r => r == grupoOrdenCompraTipo.GrupoId))
            {
                _context.GrupoOrdenesComprasTipos.Remove(grupoOrdenCompraTipo);
                listaGrupos.Remove(grupoOrdenCompraTipo);
            }
        }

        ordenCompraTipo.Nombre = p.Nombre;
        ordenCompraTipo.Descripcion = p.Descripcion;
        ordenCompraTipo.EsRequerida = p.EsRequerida;
        ordenCompraTipo.EsAbierta = p.EsAbierta;
        ordenCompraTipo.EsRecurrente = p.EsRecurrente;
        ordenCompraTipo.EsUnica = p.EsUnica;
    }
    public async Task DeleteTipoAsync(int id)
    {

        OrdenCompraTipo ordenCompraTpo = await GetTipoAsync(id);

        if (_context.OrdenesCompras.Any(src => src.OrdenCompraTipoId == ordenCompraTpo.Id))
            throw new ValidationErrorException("OrdenCompra", "Operacion rechazada: Existe un Documento de compra con el tipo asignado");

        if (_context.EmpresasOrdenesComprasTipos.Any(src => src.OrdenCompraTipoId == ordenCompraTpo.Id))
            throw new ValidationErrorException("EmpresaOrdenCompra", "Operacion rechazada: Existe un Socio asociado al tipo seleccionado");

        IQueryable<GrupoOrdenCompraTipo> grupos = _context.GrupoOrdenesComprasTipos
            .Where(src => src.OrdenCompraTipoId == ordenCompraTpo.Id);

        var gruposList = grupos.ToList();

        foreach (var grupo in gruposList)
        {
            //rol.IsDeleted = true;
            _context.GrupoOrdenesComprasTipos.Remove(grupo);
        }

        //ordenCompraTpo.IsDeleted = true;
        _context.OrdenesComprasTipos.Remove(ordenCompraTpo); //**PARA REVISION
    }
    public async Task<List<OrdenCompraEstado>> GetAllOrdenesComprasEstados()
    {
        return await _context.OrdenesComprasEstados.ToListAsync();
    }

    public async Task<List<OrdenCompraTipo>> GetAllOrdenesComprasTipos()
    {
        long companyId = (await _currentCompanyService.GetCurrentCompanyAsync()).Id;
        return await _context.OrdenesComprasTipos
            .Include(u => u.Company)
            .Where(src => src.CompanyId == companyId && !src.IsDeleted).ToListAsync();
    }
}
