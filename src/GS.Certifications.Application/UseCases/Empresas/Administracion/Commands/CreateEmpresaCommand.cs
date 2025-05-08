using DocumentFormat.OpenXml.InkML;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Global.Currencies.Queries;
using GSF.Application.Security.Services;
using GSF.Application.Security.Services.CurrentCompany;
using GSF.Domain.Entities.Companies;
using MediatR;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Impuestos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Commands
{
    public class CreateEmpresaCommand : IRequest<int> // Adjust TResponse properly
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
        public string IdMoneda { get; set; }
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
        public List<EmpresaCurrencyCreate> Monedas { get; set; }
    }

    public class CreateEmpresaCommandHandler : BaseRequestHandler<int, CreateEmpresaCommand, int> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext Context;
        private readonly IEmpresaPortalService EmpresasService;
        private readonly ICurrentCompanyService CompanyService;
        // Add services

        public CreateEmpresaCommandHandler(ICertificationsDbContext context, IEmpresaPortalService empresasService, ICurrentCompanyService companyService)
        {
            Context = context;
            EmpresasService = empresasService;
            CompanyService = companyService;
            // Inject dependencies
        }

        protected override async Task<int> HandleRequestAsync(CreateEmpresaCommand request, CancellationToken cancellationToken)
        {
            EmpresasCreate command = new EmpresasCreate
            {
                CodigoProveedor = request.CodigoProveedor,
                RazonSocial = request.RazonSocial,
                NombreFantasia = request.NombreFantasia,
                IdentificadorTributario = request.IdentificadorTributario,
                GranContribuyente = request.GranContribuyente,
                Direccion = request.Direccion,
                CodigoPostal = request.CodigoPostal,
                PaisId = request.PaisId,
                ProvinciaId = request.ProvinciaId,
                CiudadId = request.CiudadId,
                CiudadDescripcion = request.CiudadDescripcion,
                TelefonoPrincipal = request.TelefonoPrincipal,
                TelefonoAlternativo = request.TelefonoAlternativo,
                EmailPrincipal = request.EmailPrincipal,
                EmailAlternativo = request.EmailAlternativo,
                Contacto = request.Contacto,
                ContactoAlternativo = request.ContactoAlternativo,
                TipoResponsableId = request.TipoResponsableId,
                NumeroIngresosBrutos = request.NumeroIngresosBrutos,
                TipoCuentaId = request.TipoCuentaId,
                CuentaBancaria = request.CuentaBancaria,
                PaginaWeb = request.PaginaWeb,
                RedesSociales = request.RedesSociales,
                DescripcionEmpresa = request.DescripcionEmpresa,
                ProductosServiciosOfrecidos = request.ProductosServiciosOfrecidos,
                ReferenciasComerciales = request.ReferenciasComerciales,
                Confirmado = request.Confirmado,
                RolesIdm = request.RolesIdm,
                AlicuotasIdm = request.AlicuotasIdm,
                OrdenesComprasTiposId = request.OrdenesComprasTiposId,
                ConceptosGastosTiposId = request.ConceptosGastosTiposId,
                Monedas = new List<IEmpresaCurrencyCreate>()
            };

            if (request.Monedas != null)
            {
                foreach (EmpresaCurrencyCreate empresaCurrency in request.Monedas)
                {
                    command.Monedas.Add(empresaCurrency);
                }
            }

            EmpresaPortal empresa = await EmpresasService.CreateAsync(command);
            empresa.CompanyId = (await CompanyService.GetCurrentCompanyAsync()).Id;
            empresa.OrganizationId = (await CompanyService.GetCurrentCompanyOrganizationAsync()).Id;
            Context.EmpresasPortales.Add(empresa);
            await Context.SaveChangesAsync(cancellationToken);
            return empresa.Id;
        }
    }
}
