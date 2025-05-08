using GS.Certifications.Application.Commons.Dtos.ConceptosGastosTipos;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.Commons.Dtos.ModosLecturas;
using GS.Certifications.Application.Commons.Dtos.OrdenesCompra;
using GS.Certifications.Application.CQRS.DbContexts;
using GSF.Application.Common.Exceptions;
using GSF.Application.Global.Currencies.Queries;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Security.Services.CurrentCompany;
using GSF.Application.Services;
using GSF.Domain.Entities.Global;
using GSF.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;
using GS.Certifications.Domain.Entities.Alicuotas;
using GS.Certifications.Domain.Entities.Comprobantes;
using GS.Certifications.Domain.Entities.ConceptosGastosTipos;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Empresas.Alicuotas;
using GS.Certifications.Domain.Entities.Empresas.ConceptosGastosTipos;
using GS.Certifications.Domain.Entities.Empresas.ModosLecturas;
using GS.Certifications.Domain.Entities.Empresas.OrdenesCompras;
using GS.Certifications.Domain.Entities.Empresas.Roles;
using GS.Certifications.Domain.Entities.ModosLecturas;
using GS.Certifications.Domain.Entities.OrdenesCompras;
using GS.Certifications.Domain.Entities.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Services
{
    public class EmpresaPortalService : BaseGSFService, IEmpresaPortalService
    {
        private readonly ICertificationsDbContext context;
        private readonly ICurrentCompanyService service;

        public EmpresaPortalService(ICertificationsDbContext context, ICurrentCompanyService service)
        {
            this.context = context;
            this.service = service;
        }
        public async Task<EmpresaPortal> GetAsync(long id)
        {
            EmpresaPortal empresa = await context.EmpresasPortales
                .Include(u => u.Company)
                .Include(u => u.Organization)
                .Include(u => u.Pais)
                .Include(u => u.Ciudad)
                .Include(u => u.Provincia)
                .Include(u => u.TipoResponsable)
                .Include(u => u.TipoCuenta)
                .Include(u => u.Roles)
                    .ThenInclude(u => u.RolTipo)
                .Include(u => u.Monedas)
                .Include(u => u.Alicuotas)
                    .ThenInclude(u => u.Alicuota)
                .Include(u => u.ModosLecturas)
                    .ThenInclude(u => u.ModoLectura)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            return empresa;
        }

        public async Task<IPaginatedQueryResult<EmpresaPortal>> GetManyAsync(IEmpresasQueryParameter request)
        {

            long companyId = (await service.GetCurrentCompanyAsync()).Id;
            var colection = context.EmpresasPortales
                .Include(u => u.Company)
                .Include(u => u.Organization)
                .Include(u => u.Pais)
                .Include(u => u.Ciudad)
                .Include(u => u.Provincia)
                .Include(u => u.TipoResponsable)
                .Include(u => u.TipoCuenta)
                .Include(u => u.Roles)
                    .ThenInclude(u => u.RolTipo)
                .Include(u => u.Monedas)
                .Include(u => u.Alicuotas)
                    .ThenInclude(u => u.Alicuota)
                .Include(u => u.ModosLecturas)
                    .ThenInclude(u => u.ModoLectura)
                .Where(src => src.CompanyId == companyId && !src.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.RazonSocial))
            {
                colection = colection.Where(a => a.RazonSocial.Contains(request.RazonSocial));
            }
            if (!string.IsNullOrEmpty(request.NombreFantasia))
            {
                colection = colection.Where(a => a.NombreFantasia.Contains(request.NombreFantasia));
            }
            if (!string.IsNullOrEmpty(request.IdentificadorTributario))
            {
                colection = colection.Where(a => a.IdentificadorTributario.Contains(request.IdentificadorTributario));
            }
            if (request.GranContribuyente != null)
            {
                colection = colection.Where(a => a.GranContribuyente == request.GranContribuyente);
            }
            if (!string.IsNullOrEmpty(request.Contacto))
            {
                colection = colection.Where(a => a.Contacto.Contains(request.Contacto));
            }
            if (request.PaisId != null)
            {
                colection = colection.Where(a => a.PaisId == request.PaisId);
            }
            if (request.ProvinciaId != null)
            {
                colection = colection.Where(a => a.ProvinciaId == request.ProvinciaId);
            }
            if (request.CiudadId != null)
            {
                colection = colection.Where(a => a.CiudadId == request.CiudadId);
            }

            return await Get(colection, request);
        }
        public async Task<EmpresaPortal> CreateAsync(IEmpresasCreate c)
        {
            EmpresaPortal empresa = new EmpresaPortal
            {
                CodigoProveedor = c.CodigoProveedor,
                RazonSocial = c.RazonSocial,
                NombreFantasia = c.NombreFantasia,
                IdentificadorTributario = c.IdentificadorTributario,
                GranContribuyente = c.GranContribuyente,
                Direccion = c.Direccion,
                CodigoPostal = c.CodigoPostal,
                PaisId = c.PaisId,
                ProvinciaId = c.ProvinciaId,
                CiudadId = c.CiudadId,
                CiudadDescripcion = c.CiudadDescripcion,
                TelefonoPrincipal = c.TelefonoPrincipal,
                TelefonoAlternativo = c.TelefonoAlternativo,
                EmailPrincipal = c.EmailPrincipal,
                EmailAlternativo = c.EmailAlternativo,
                Contacto = c.Contacto,
                ContactoAlternativo = c.ContactoAlternativo,
                TipoResponsableId = c.TipoResponsableId,
                NumeroIngresosBrutos = c.NumeroIngresosBrutos,
                TipoCuentaId = c.TipoCuentaId,
                CuentaBancaria = c.CuentaBancaria,
                PaginaWeb = c.PaginaWeb,
                RedesSociales = c.RedesSociales,
                DescripcionEmpresa = c.DescripcionEmpresa,
                ProductosServiciosOfrecidos = c.DescripcionEmpresa,
                ReferenciasComerciales = c.ReferenciasComerciales,
                Confirmado = c.Confirmado,
            };

            List<EmpresaRol> listaRoles = new List<EmpresaRol>();

            foreach (short idm in c.RolesIdm)
            {
                EmpresaRol empresaRol = new EmpresaRol()
                {
                    RolTipoId = idm,
                };
                listaRoles.Add(empresaRol);
            }
            empresa.Roles = listaRoles;

            List<EmpresaAlicuota> listaAlicuotas = new List<EmpresaAlicuota>();

            foreach (short idm in c.AlicuotasIdm)
            {
                Alicuota alicuota = await context.Alicuotas.FirstOrDefaultAsync(src => src.Idm == idm);
                EmpresaAlicuota empresaAlicuota = new EmpresaAlicuota()
                {
                    AlicuotaIdm = idm,
                    //Alicuota = alicuota
                };
                listaAlicuotas.Add(empresaAlicuota);
            }
            empresa.Alicuotas = listaAlicuotas;

            List<EmpresaOrdenCompraTipo> listaOrdenesComprasTipos = new List<EmpresaOrdenCompraTipo>();

            foreach (short id in c.OrdenesComprasTiposId)
            {
                OrdenCompraTipo ordenCompraTipo = await context.OrdenesComprasTipos.FirstOrDefaultAsync(src => src.Id == id);
                EmpresaOrdenCompraTipo empresaOrdenCompraTipo = new EmpresaOrdenCompraTipo()
                {
                    OrdenCompraTipoId = id,
                    //Alicuota = alicuota
                };
                listaOrdenesComprasTipos.Add(empresaOrdenCompraTipo);
            }
            empresa.OrdenesComprasTipos = listaOrdenesComprasTipos;

            List<EmpresaConceptoGastoTipo> listaConceptosGastosTipos = new List<EmpresaConceptoGastoTipo>();

            foreach (short id in c.ConceptosGastosTiposId)
            {
                ConceptoGastoTipo conceptoGastoTipo = await context.ConceptosGastosTipos.FirstOrDefaultAsync(src => src.Id == id);
                EmpresaConceptoGastoTipo empresaConceptoGastoTipo = new EmpresaConceptoGastoTipo()
                {
                    ConceptoGastoTipoId = id,
                    //Alicuota = alicuota
                };
                listaConceptosGastosTipos.Add(empresaConceptoGastoTipo);
            }
            empresa.ConceptosGastosTipos = listaConceptosGastosTipos;

            List<EmpresaModoLectura> listaEmpresaModosLectura = new List<EmpresaModoLectura>();
            List<ModoLectura> listaModosLectura = await GetAllModosLecturas();

            foreach (ModoLectura modoLectura in listaModosLectura)
            {
                if (modoLectura.Idm != ModoLectura.MANUAL_IDM)
                {
                    EmpresaModoLectura empresaAlicuota = new EmpresaModoLectura()
                    {
                        ModoLectura = modoLectura,
                        ModoLecturaIdm = modoLectura.Idm
                    };
                    listaEmpresaModosLectura.Add(empresaAlicuota);
                }
            }
            empresa.ModosLecturas = listaEmpresaModosLectura;

            List<EmpresaCurrency> listaCurrencies = new List<EmpresaCurrency>();

            if (c.Monedas != null)
            {
                foreach (EmpresaCurrencyCreate currency in c.Monedas)
                {
                    EmpresaCurrency empresaCurrency = new EmpresaCurrency
                    {
                        CurrencyId = currency.CurrencyId,
                        MonedaPorDefecto = currency.MonedaPorDefecto
                    };
                    listaCurrencies.Add(empresaCurrency);
                }
                empresa.Monedas = listaCurrencies;
            }
            return empresa;
        }

        public async Task UpdateAsync(IEmpresasUpdate e)
        {
            EmpresaPortal empresa = await GetAsync(e.Id);

            List<EmpresaRol> listaRoles = empresa.Roles.ToList();
            List<EmpresaAlicuota> listaAlicuotas = empresa.Alicuotas.ToList();
            List<EmpresaCurrency> listaCurrencies = empresa.Monedas.ToList();

            List<IEmpresaCurrencyUpdate> listaCurrenciesNuevas = e.Monedas.Where(src => !src.Deleted && src.Id == 0).ToList();
            List<IEmpresaCurrencyUpdate> listaCurrenciesEliminadas = e.Monedas.Where(src => src.Deleted && src.Id != 0).ToList();
            List<IEmpresaCurrencyUpdate> listaCurrenciesModificadas = e.Monedas.Where(src => !src.Deleted && src.Id != 0).ToList();

            foreach (EmpresaCurrencyUpdate empresaCurrency in listaCurrenciesEliminadas)
            {
                EmpresaCurrency moneda = listaCurrencies.Where(src => src.CurrencyId == empresaCurrency.CurrencyId).FirstOrDefault();
                context.EmpresasCurrencies.Remove(moneda);
                listaCurrencies.Remove(moneda);
            }

            foreach (EmpresaCurrencyUpdate empresaCurrency in listaCurrenciesNuevas)
            {
                EmpresaCurrency moneda = new EmpresaCurrency
                {
                    CurrencyId = empresaCurrency.CurrencyId,
                    MonedaPorDefecto = empresaCurrency.MonedaPorDefecto
                };
                listaCurrencies.Add(moneda);
            }

            foreach (EmpresaCurrencyUpdate empresaCurrency in listaCurrenciesModificadas)
            {
                EmpresaCurrency moneda = listaCurrencies.Where(src => src.Id == empresaCurrency.Id).FirstOrDefault();

                if (moneda != null)
                {
                    moneda.CurrencyId = empresaCurrency.CurrencyId;
                    moneda.MonedaPorDefecto = empresaCurrency.MonedaPorDefecto;
                }
            }

            if (e.RolesIdm.Count != 0)
            {
                foreach (short idm in e.RolesIdm)
                {
                    if (!empresa.Roles.Any(r => r.RolTipoId == idm))
                    {
                        EmpresaRol empresaRol = new EmpresaRol()
                        {
                            RolTipoId = idm,
                        };
                        listaRoles.Add(empresaRol);
                    }
                }
            }

            foreach (EmpresaRol empresaRol in empresa.Roles)
            {
                if (!e.RolesIdm.Any(r => r == empresaRol.RolTipoId))
                {
                    context.EmpresasRoles.Remove(empresaRol);
                    listaRoles.Remove(empresaRol);
                }
            }

            if (e.AlicuotasIdm.Count != 0)
            {
                foreach (short idm in e.AlicuotasIdm)
                {
                    Alicuota alicuota = await context.Alicuotas.FirstOrDefaultAsync(src => src.Idm == idm);
                    if (!empresa.Alicuotas.Any(r => r.AlicuotaIdm == idm))
                    {
                        EmpresaAlicuota empresaAlicuota = new EmpresaAlicuota()
                        {
                            AlicuotaIdm = idm,
                            Alicuota = alicuota
                        };
                        listaAlicuotas.Add(empresaAlicuota);
                    }
                }
            }

            foreach (EmpresaAlicuota empresaAlicuota in empresa.Alicuotas)
            {
                if (!e.AlicuotasIdm.Any(r => r == empresaAlicuota.AlicuotaIdm))
                {
                    context.EmpresasAlicuotas.Remove(empresaAlicuota);
                    listaAlicuotas.Remove(empresaAlicuota);
                }
            }

            empresa.Roles = listaRoles;
            empresa.Alicuotas = listaAlicuotas;
            empresa.Monedas = listaCurrencies;
            empresa.CodigoProveedor = e.CodigoProveedor;
            empresa.RazonSocial = e.RazonSocial;
            empresa.NombreFantasia = e.NombreFantasia;
            empresa.IdentificadorTributario = e.IdentificadorTributario;
            empresa.GranContribuyente = e.GranContribuyente;
            empresa.Direccion = e.Direccion;
            empresa.CodigoPostal = e.CodigoPostal;
            empresa.PaisId = e.PaisId;
            empresa.ProvinciaId = e.ProvinciaId;
            empresa.CiudadId = e.CiudadId;
            empresa.CiudadDescripcion = e.CiudadDescripcion;
            empresa.TelefonoPrincipal = e.TelefonoPrincipal;
            empresa.TelefonoAlternativo = e.TelefonoAlternativo;
            empresa.EmailPrincipal = e.EmailPrincipal;
            empresa.EmailAlternativo = e.EmailAlternativo;
            empresa.Contacto = e.Contacto;
            empresa.ContactoAlternativo = e.ContactoAlternativo;
            empresa.TipoResponsableId = e.TipoResponsableId;
            empresa.NumeroIngresosBrutos = e.NumeroIngresosBrutos;
            empresa.TipoCuentaId = e.TipoCuentaId;
            empresa.CuentaBancaria = e.CuentaBancaria;
            empresa.PaginaWeb = e.PaginaWeb;
            empresa.RedesSociales = e.RedesSociales;
            empresa.DescripcionEmpresa = e.DescripcionEmpresa;
            empresa.ProductosServiciosOfrecidos = e.DescripcionEmpresa;
            empresa.ReferenciasComerciales = e.ReferenciasComerciales;
            empresa.Confirmado = e.Confirmado;
        }

        public async Task DeleteAsync(int id)
        {
            EmpresaPortal empresa = await context.EmpresasPortales
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(c => c.Id == id);

            IQueryable<EmpresaRol> roles = context.EmpresasRoles
                .Where(src => src.EmpresaPortalId == empresa.Id);

            var rolesList = roles.ToList();

            foreach (var rol in rolesList)
            {
                //rol.IsDeleted = true;
                context.EmpresasRoles.Remove(rol);
            }

            IQueryable<EmpresaAlicuota> alicuotas = context.EmpresasAlicuotas
                .Where(src => src.EmpresaPortalId == empresa.Id);

            var alicuotasList = alicuotas.ToList();

            foreach (var alicuota in alicuotasList)
            {
                //alicuota.IsDeleted = true;
                context.EmpresasAlicuotas.Remove(alicuota);
            }

            IQueryable<EmpresaOrdenCompraTipo> ordenesComprasTipos = context.EmpresasOrdenesComprasTipos
            .Where(src => src.EmpresaPortalId == empresa.Id);

            var ordenesComprasTiposList = ordenesComprasTipos.ToList();

            foreach (var ordenCompraTipo in ordenesComprasTiposList)
            {
                //ordenCompraTipo.IsDeleted = true;
                context.EmpresasOrdenesComprasTipos.Remove(ordenCompraTipo);
            }

            IQueryable<EmpresaConceptoGastoTipo> conceptosGastosTipos = context.EmpresasConceptosGastosTipos
                .Where(src => src.EmpresaPortalId == empresa.Id);

            var conceptosGastosTiposList = conceptosGastosTipos.ToList();

            foreach (var conceptoGastoTipo in conceptosGastosTiposList)
            {
                //ordenCompraTipo.IsDeleted = true;
                context.EmpresasConceptosGastosTipos.Remove(conceptoGastoTipo);
            }

            IQueryable<EmpresaCurrency> monedas = context.EmpresasCurrencies
                .Where(src => src.EmpresaPortalId == empresa.Id);

            var monedasList = monedas.ToList();

            foreach (var moneda in monedasList)
            {
                //moneda.IsDeleted = true;
                context.EmpresasCurrencies.Remove(moneda);
            }

            IQueryable<EmpresaModoLectura> modosLecturas = context.EmpresasModosLecturas
                .Where(src => src.EmpresaPortalId == empresa.Id);

            var modosLecturasList = modosLecturas.ToList();

            foreach (var modoLectura in modosLecturasList)
            {
                //modoLectura.IsDeleted = true;
                context.EmpresasModosLecturas.Remove(modoLectura);
            }

            //empresa.IsDeleted = true;
            context.EmpresasPortales.Remove(empresa);
        }

        public Task<IPaginatedQueryResult<RolTipo>> GetManyRolesTiposAsync(IRolesTiposQueryParameter request)
        {
            var colection = context.RolesTipos.AsQueryable();

            if (!string.IsNullOrEmpty(request.Descripcion))
            {
                colection = colection.Where(a => a.Descripcion.Contains(request.Descripcion));
            }
            if (!string.IsNullOrEmpty(request.Codigo))
            {
                colection = colection.Where(a => a.Codigo.Contains(request.Codigo));
            }

            return Get(colection, request);
        }
        public async Task<IPaginatedQueryResult<EmpresaRol>> GetEmpresasRolesManyAsync(IEmpresasRolesQueryParameter request)
        {
            var colection = context.EmpresasRoles
                .Include(u => u.RolTipo)
                .AsQueryable();

            if (request.EmpresaPortalId != null)
            {
                colection = colection.Where(a => a.EmpresaPortalId == request.EmpresaPortalId);
            }

            return await Get(colection, request);
        }

        public async Task<List<Currency>> GetAllCurrencies()
        {
            var colection = await context.Currencies.ToListAsync();
            return colection;
        }

        public async Task<List<ModoLectura>> GetAllModosLecturas()
        {
            var colection = await context.ModosLecturas.ToListAsync();
            return colection;
        }

        public async Task<List<EmpresaModoLectura>> GetAllEmpresasModosLecturas(long? empresaPortalId)
        {
            var colection = await context.EmpresasModosLecturas
                .Include(u => u.ModoLectura)
                .Where(src => src.EmpresaPortalId == empresaPortalId && !src.IsDeleted).ToListAsync();
            return colection;
        }
        public async Task ModificarModosLectura(int empresaPortalId, List<ModoLecturaDto> modosLecturasUpdated)
        {
            EmpresaPortal empresaPortal = await context.EmpresasPortales
                .Include(e => e.ModosLecturas)
                .FirstOrDefaultAsync(e => e.Id == empresaPortalId);

            if (empresaPortal == null)
                throw new Exception("Empresa no encontrada");

            var idsActuales = empresaPortal.ModosLecturas.Select(x => x.ModoLecturaIdm).ToList();
            var idsNuevos = modosLecturasUpdated.Select(x => x.Idm).ToList();

            // Agregar nuevos
            var aAgregar = idsNuevos.Except(idsActuales).ToList();
            foreach (var idm in aAgregar)
            {
                empresaPortal.ModosLecturas.Add(new EmpresaModoLectura
                {
                    ModoLecturaIdm = idm,
                    EmpresaPortalId = empresaPortalId
                });
            }

            // Eliminar los que ya no están
            var aEliminar = empresaPortal.ModosLecturas
                .Where(x => !idsNuevos.Contains(x.ModoLecturaIdm))
                .ToList();

            foreach (var item in aEliminar)
            {
                context.EmpresasModosLecturas.Remove(item);
            }
        }
        public async Task<List<EmpresaOrdenCompraTipo>> GetAllEmpresasOrdenesComprasTipos(long? empresaPortalId)
        {
            var colection = await context.EmpresasOrdenesComprasTipos
                .Include(u => u.OrdenCompraTipo)
                .Where(src => src.EmpresaPortalId == empresaPortalId && !src.IsDeleted).ToListAsync();
            return colection;
        }
        public async Task ModificarOrdenesComprasTipos(int empresaPortalId, List<OrdenCompraTipoDto> ordenesComprasTipos)
        {
            EmpresaPortal empresaPortal = await context.EmpresasPortales
                .Include(e => e.OrdenesComprasTipos)
                .FirstOrDefaultAsync(e => e.Id == empresaPortalId);

            if (empresaPortal == null)
                throw new Exception("Empresa no encontrada");

            var idsActuales = empresaPortal.OrdenesComprasTipos.Select(x => x.OrdenCompraTipoId).ToList();
            var idsNuevos = ordenesComprasTipos.Select(x => x.Id).ToList();

            // Agregar nuevos
            var aAgregar = idsNuevos.Except(idsActuales).ToList();
            foreach (var id in aAgregar)
            {
                empresaPortal.OrdenesComprasTipos.Add(new EmpresaOrdenCompraTipo
                {
                    OrdenCompraTipoId = id,
                    EmpresaPortalId = empresaPortalId
                });
            }

            // Eliminar los que ya no están
            var aEliminar = empresaPortal.OrdenesComprasTipos
                .Where(x => !idsNuevos.Contains(x.OrdenCompraTipoId))
                .ToList();

            foreach (var item in aEliminar)
            {
                context.EmpresasOrdenesComprasTipos.Remove(item);
            }
        }

        public async Task<List<EmpresaConceptoGastoTipo>> GetAllEmpresasConceptosGastosTipos(long? empresaPortalId)
        {
            var colection = await context.EmpresasConceptosGastosTipos
                .Include(u => u.ConceptoGastoTipo)
                .Where(src => src.EmpresaPortalId == empresaPortalId && !src.IsDeleted).ToListAsync();
            return colection;
        }
        public async Task ModificarConceptosGastosTipos(int empresaPortalId, List<ConceptoGastoTipoDto> conceptosGastosTipos)
        {
            EmpresaPortal empresaPortal = await context.EmpresasPortales
                .Include(e => e.ConceptosGastosTipos)
                .FirstOrDefaultAsync(e => e.Id == empresaPortalId);

            if (empresaPortal == null)
                throw new Exception("Empresa no encontrada");

            var idsActuales = empresaPortal.ConceptosGastosTipos.Select(x => x.ConceptoGastoTipoId).ToList();
            var idsNuevos = conceptosGastosTipos.Select(x => x.Id).ToList();

            // Agregar nuevos
            var aAgregar = idsNuevos.Except(idsActuales).ToList();
            foreach (var id in aAgregar)
            {
                empresaPortal.ConceptosGastosTipos.Add(new EmpresaConceptoGastoTipo
                {
                    ConceptoGastoTipoId = id,
                    EmpresaPortalId = empresaPortalId
                });
            }

            // Eliminar los que ya no están
            var aEliminar = empresaPortal.ConceptosGastosTipos
                .Where(x => !idsNuevos.Contains(x.ConceptoGastoTipoId))
                .ToList();

            foreach (var item in aEliminar)
            {
                context.EmpresasConceptosGastosTipos.Remove(item);
            }
        }
        public async Task<List<TipoCuenta>> GetAllTiposCuentas()
        {
            var colection = await context.TiposCuentas.ToListAsync();
            return colection;
        }
        public async Task<List<CategoriaTipo>> GetAllCategoriasTipos()
        {
            var colection = await context.CategoriasTipos.ToListAsync();
            return colection;
        }
        public async Task<User> GetUsuarioExternoAsync(string email)
        {
            User usuarioExterno = null;

            long currentCompanyId = (await service.GetCurrentCompanyAsync()).Id;

            usuarioExterno = await context.Users
                .Include(u => u.UserDomains)
                    .ThenInclude(ud => ud.DomainF)
                .Include(u => u.CompaniesUsersGroups)
                    .ThenInclude(ud => ud.Company)
                //.Where(u => u.CompaniesUsersGroups.Any(u => u.Company.Id == currentCompanyId))
                .FirstOrDefaultAsync(u => u.UserTypeIdm == 1001 && u.Email == email);

            return usuarioExterno;
        }

        public async Task<UsuarioEmpresaPortal> CreateUsuarioEmpresaAsync(IUsuarioEmpresaCreate t)
        {
            User usuarioExterno = null;
            usuarioExterno = await GetUsuarioExternoAsync(t.Email);

            long companyId = (await service.GetCurrentCompanyGroupAsync()).Id;

            if (usuarioExterno == null)
            {
                usuarioExterno = new User
                {
                    Email = t.Email,
                    Login = t.Login,
                    FirstName = t.FirstName,
                    IdNumber = t.IdNumber,
                    PhoneNumber = t.PhoneNumber,
                    GroupOwnerId = companyId,
                    Blocked = false,
                    Activated = false,
                    UserTypeIdm = 1001,
                };
            }
            else
            {
                if (context.UsuarioEmpresasPortales.Any(u => u.UserId == usuarioExterno.Id))
                {
                    throw new ValidationErrorException
                        ("UsuarioExterno", "El usuario ya esta relacionado con la empresa portal");
                }

                if (!usuarioExterno.CompaniesUsersGroups.Any(u => u.CompanyId == companyId))
                {

                }
            }

            UsuarioEmpresaPortal usuarioEmpresa = new UsuarioEmpresaPortal()
            {
                EmpresaPortalId = t.EmpresaPortalId,
                User = usuarioExterno
            };

            List<UsuarioEmpresaPortalRol> listaRoles = new List<UsuarioEmpresaPortalRol>();

            foreach (RolTipoDto rol in t.Roles)
            {
                RolTipo rolTipo = context.RolesTipos
                    .Where(u => u.Idm == rol.Idm).FirstOrDefault();

                UsuarioEmpresaPortalRol usuarioEmpresaPortalRol = new UsuarioEmpresaPortalRol
                {
                    RolTipo = rolTipo,
                    Habilitado = false
                };

                listaRoles.Add(usuarioEmpresaPortalRol);
            }
            usuarioEmpresa.Roles = listaRoles;

            return usuarioEmpresa;
        }
    }

    public class EmpresasCreate : IEmpresasCreate
    {
        public string CodigoProveedor { get; set; }
        public string RazonSocial { get; set; }
        public string NombreFantasia { get; set; }
        public string IdentificadorTributario { get; set; }
        public bool GranContribuyente { get; set; }
        public string Direccion { get; set; }
        public string CodigoPostal { get; set; }
        public long PaisId { get; set; }
        public long? ProvinciaId { get; set; }
        public long? CiudadId { get; set; }
        public string CiudadDescripcion { get; set; }
        public string TelefonoPrincipal { get; set; }
        public string TelefonoAlternativo { get; set; }
        public string EmailPrincipal { get; set; }
        public string EmailAlternativo { get; set; }
        public string Contacto { get; set; }
        public string ContactoAlternativo { get; set; }
        public short TipoResponsableId { get; set; }
        public string NumeroIngresosBrutos { get; set; }
        public short? TipoCuentaId { get; set; }
        public string CuentaBancaria { get; set; }
        public string PaginaWeb { get; set; }
        public string RedesSociales { get; set; }
        public string DescripcionEmpresa { get; set; }
        public string ProductosServiciosOfrecidos { get; set; }
        public string ReferenciasComerciales { get; set; }
        public bool Confirmado { get; set; }
        public List<short> RolesIdm { get; set; }
        public List<short> AlicuotasIdm { get; set; }
        public List<short> OrdenesComprasTiposId { get; set; }
        public List<short> ConceptosGastosTiposId { get; set; }
        public List<IEmpresaCurrencyCreate> Monedas { get; set; }
    }
    public class EmpresaCurrencyCreate : IEmpresaCurrencyCreate
    {
        public int? Id { get; set; }
        public byte?[] RowVersion { get; set; }
        public int? EmpresaPortalId { get; set; }
        public CurrencyDto Currency { get; set; }
        public long CurrencyId { get; set; }
        public bool MonedaPorDefecto { get; set; }
        public bool Deleted { get; set; }
    }
    public class EmpresasUpdate : IEmpresasUpdate
    {
        public long Id { get; set; } // Adjust Id type properly
        public byte[] RowVersion { get; set; }
        public string CodigoProveedor { get; set; }
        public string RazonSocial { get; set; }
        public string NombreFantasia { get; set; }
        public string IdentificadorTributario { get; set; }
        public bool GranContribuyente { get; set; }
        public string Direccion { get; set; }
        public string CodigoPostal { get; set; }
        public long PaisId { get; set; }
        public long? ProvinciaId { get; set; }
        public long? CiudadId { get; set; }
        public string CiudadDescripcion { get; set; }
        public string TelefonoPrincipal { get; set; }
        public string TelefonoAlternativo { get; set; }
        public string EmailPrincipal { get; set; }
        public string EmailAlternativo { get; set; }
        public string Contacto { get; set; }
        public string ContactoAlternativo { get; set; }
        public short TipoResponsableId { get; set; }
        public string NumeroIngresosBrutos { get; set; }
        public short? TipoCuentaId { get; set; }
        public string CuentaBancaria { get; set; }
        public string PaginaWeb { get; set; }
        public string RedesSociales { get; set; }
        public string DescripcionEmpresa { get; set; }
        public string ProductosServiciosOfrecidos { get; set; }
        public string ReferenciasComerciales { get; set; }
        public bool Confirmado { get; set; }
        public List<short> RolesIdm { get; set; }
        public List<short> AlicuotasIdm { get; set; }
        public List<IEmpresaCurrencyUpdate> Monedas { get; set; }
    }
    public class EmpresaCurrencyUpdate : IEmpresaCurrencyUpdate
    {
        public int? Id { get; set; }
        public byte?[] RowVersion { get; set; }
        public int? EmpresaPortalId { get; set; }
        public CurrencyDto Currency { get; set; }
        public long CurrencyId { get; set; }
        public bool MonedaPorDefecto { get; set; }
        public bool Deleted { get; set; }
    }
}
