using GS.Certifications.Application.Commons.Dtos.EmailProcessor;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.GSFExtensions.GSFWebFilteTransferService;
using GS.Certifications.Application.Interfaces.EmailProcessor;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services;
using GSF.Domain.Entities.Companies;
using GSFWebFileTransferService.Abstractions.Builder;
using GSFWebFileTransferService.Abstractions.DefaultValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GS.Certifications.Domain.Commons.Exceptions.EmailProcessor;
using GS.Certifications.Domain.Entities.Comprobantes;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Empresas.ModosLecturas;
using GS.Certifications.Domain.Entities.MailProcessors;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.EmailProcessor.Commands;

public class EmailProcessorCommand : IRequest<EmailProcessorResultDto>
{
    public int ProcesoId { get; set; }
    public int CompanyId { get; set; }
    public class EmailProcessorCommandHandler : IRequestHandler<EmailProcessorCommand, EmailProcessorResultDto>
    {
        private readonly ICertificationsDbContext _dbContext;
        private readonly IEmailInvoiceService _emailInvoiceService;
        private readonly IComprobanteService _comprobanteService;
        private readonly IEmpresaPortalService _empresasPortalService;
        private readonly IWebFileTransferServiceBuilder<StorageTypeGSFWFTS, BasicFileConfigurationTypeGSFWFTS> _fileTransferServiceBuilder;
        private readonly ILogger<EmailProcessorCommandHandler> _logger;

        public EmailProcessorCommandHandler(
            ICertificationsDbContext dbContext,
            //IEmailInvoiceService emailInvoiceService,
            IComprobanteService comprobanteService,
            IEmpresaPortalService empresaPortalService,
            IWebFileTransferServiceBuilder<StorageTypeGSFWFTS,
            BasicFileConfigurationTypeGSFWFTS> fileTransferServiceBuilder,
            ILogger<EmailProcessorCommandHandler> logger)
        {
            _dbContext = dbContext;
            //_emailInvoiceService = emailInvoiceService;
            _comprobanteService = comprobanteService;
            _empresasPortalService = empresaPortalService;
            _fileTransferServiceBuilder = fileTransferServiceBuilder;
            _logger = logger;
        }

        public async Task<EmailProcessorResultDto> Handle(EmailProcessorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("--------------------------------------------------------------------------------------");
            _logger.LogInformation($"Iniciando tarea de procesamiento de correos para la compania:{request.CompanyId}");
            Stopwatch sw = Stopwatch.StartNew();

            _logger.LogInformation($"Obteniendo datos de la compania:{request.CompanyId}");
            Company company = await _dbContext.Companies.Where(c => c.Id == request.CompanyId).SingleOrDefaultAsync(cancellationToken);

            _logger.LogInformation($"Obteniendo datos extras de la compania:{request.CompanyId}");
            CompanyExtra companyExtra = await _dbContext.CompanyExtras.Where(c => c.CompanyId == company.Id).SingleOrDefaultAsync(cancellationToken);

            _logger.LogInformation($"Obteniendo datos de integracion de la compania:{request.CompanyId}");
            IntegracionFacturaPorCorreo integracion = await _dbContext.IntegracionesFacturaPorCorreo.SingleOrDefaultAsync(i => i.Company == company && i.Actvive);
            if (integracion is null) throw new CompanyNotAllowedForIntegracionFacturaPorCorreoException(company);

            _logger.LogInformation($"Obteniendo correos de la compania:{request.CompanyId}");
            List<EmailInvoiceAttachmentDto> facturasEnCorreoAdjuntas = await _emailInvoiceService.GetEmailInvoiceAttachmentsAsync(company.Id);

            _logger.LogInformation($"Documentos en correos de la compania {request.CompanyId} encontrados: {facturasEnCorreoAdjuntas.Count}");
            _logger.LogInformation($"Procesando documentos en correos de la compania:{request.CompanyId}");
            ConcurrentBag<Comprobante> comprobantesAgregados = new();
            foreach (var facturaEnCorreoAdjunta in facturasEnCorreoAdjuntas.OrderBy(m => m.EmailReceivedDateTime))
            {
                Comprobante comprobante = null;
                StringBuilder sbObservaciones = new();
                string statusCode = "OK";

                var ejecutarFinally = false;
                try
                {
                    BinaryData binData = ObtenerContenidoAdjunto(facturaEnCorreoAdjunta);
                    await GuardarArchivoEmailAdjuntoAsync(facturaEnCorreoAdjunta);
                    IComprobanteAnalysisResult iAAnalisis = await AnalizarIAAdjuntoAsync(company, binData);
                    comprobante = await GenerarComprobanteAsync(company, companyExtra, integracion, facturaEnCorreoAdjunta, iAAnalisis, sbObservaciones);
                    comprobante = await ProcesarComprobanteAsync(comprobante, comprobantesAgregados, sbObservaciones);
                    comprobante = await GuardarArchivoComprobanteAsync(comprobante, facturaEnCorreoAdjunta);
                    ejecutarFinally = true;
                }
                catch (EmailProcessorInvalidOperationException ex)
                {
                    _logger.LogWarning(ex.ToString());
                    comprobante = null;
                    sbObservaciones.Append(ex.Message);
                    statusCode = ex.StatusCode;
                    ejecutarFinally = true;
                }
                finally
                {
                    if (ejecutarFinally)
                    {
                        FinalizarProcesamientoComprobante(facturaEnCorreoAdjunta.ComprobanteEMailExtension, request.ProcesoId, company, comprobante, sbObservaciones, statusCode);
                        _logger.LogInformation($"Persistiendo datos para la compania:{request.CompanyId}");
                        await _dbContext.SaveChangesAsync(cancellationToken);
                    }
                }
            }

            sw.Stop();
            _logger.LogInformation($"Finalizando tarea de procesamiento de correos para la compania:{request.CompanyId}");
            _logger.LogInformation("--------------------------------------------------------------------------------------");

            return new EmailProcessorResultDto()
            {
                CantidadProcesado = facturasEnCorreoAdjuntas.Count,
                DuracionMins = sw.Elapsed.TotalMinutes
            };
        }

        private BinaryData ObtenerContenidoAdjunto(EmailInvoiceAttachmentDto invoice)
        {
            byte[] bytes = invoice.FileContentBytes;
            BinaryData binData = new(bytes);
            return binData;
        }

        private async Task<IComprobanteAnalysisResult> AnalizarIAAdjuntoAsync(Company company, BinaryData dataAdjunta)
        {
            return await _comprobanteService.AnalyzeAsync(dataAdjunta, new AnalizeRequestDto() { CompanyId = company.Id });
        }

        private async Task<Comprobante> GenerarComprobanteAsync(Company company, CompanyExtra companyExtra, IntegracionFacturaPorCorreo integracion, EmailInvoiceAttachmentDto facturaEnCorreoAdjunta, IComprobanteAnalysisResult analisis, StringBuilder sbObservaciones)
        {
            EmpresaPortal empresaPortal = null;
            string analisisCuitCompany = analisis.NroIdentificacionFiscalCompany.Trim();
            string analisisCuitEmpresaPortal = analisis.NroIdentificacionFiscalPro.Trim();

            if (!companyExtra.IdentificadorTributario.Trim().Equals(analisisCuitCompany)) throw new CuitCompanyConflictException(company, analisisCuitCompany);

            List<EmpresaPortal> empresasPortalesAsigandas = facturaEnCorreoAdjunta.EmpresasPortalesAsignadas.Where(ep => !string.IsNullOrWhiteSpace(ep.IdentificadorTributario) && ep.IdentificadorTributario.Equals(analisisCuitEmpresaPortal)).ToList();
            if (empresasPortalesAsigandas.Count > 1) throw new CuitEmpresaPortalOwnershipResolutionException(analisisCuitEmpresaPortal);

            if (empresasPortalesAsigandas.Any())
            {
                empresaPortal = empresasPortalesAsigandas.Single();
            }
            else
            {
                if (!integracion.AutoCreateEmpresaPortal) throw new CuitEmpresaPortalNotFoundException(analisisCuitEmpresaPortal);
                if (string.IsNullOrWhiteSpace(analisis.NroIdentificacionFiscalPro)) throw new CuitEmpresaPortalNotReadedException();

                EmpresaPortal epExistente = await _dbContext.EmpresasPortales.FirstOrDefaultAsync(ep => ep.IdentificadorTributario.Trim().Equals(analisisCuitEmpresaPortal.Trim()));
                if (epExistente != null) throw new EmpresaPortalNotAllowedForIntegracionFacturaPorCorreoException(epExistente);

                empresaPortal = await GenerarNuevaEmpresaPortal(company, analisis, facturaEnCorreoAdjunta, integracion, sbObservaciones);
            }

            Comprobante comprobante = await _comprobanteService.GenerateDraftAsync(analisis, empresaPortal);

            comprobante = await FiltrarDatosComprobantes(comprobante);

            return comprobante;
        }

        private async Task<Comprobante> FiltrarDatosComprobantes(Comprobante comprobante)
        {
            int? idEmpresaPortal = comprobante.EmpresaId;
            if (idEmpresaPortal.HasValue)
            {
                List<EmpresaModoLectura> modoLecturas = await _dbContext.EmpresasModosLecturas.Where(emd => emd.EmpresaPortalId == idEmpresaPortal).ToListAsync();

                if (!modoLecturas.Any(eml => eml.ModoLecturaIdm == 1))
                {
                    comprobante = FiltrarDatosCabeceraQR(comprobante);
                }

                if (!modoLecturas.Any(eml => eml.ModoLecturaIdm == 4))
                {
                    comprobante = FiltrarDatosCabeceraOCR(comprobante);
                }

                if (!modoLecturas.Any(eml => eml.ModoLecturaIdm == 1 || eml.ModoLecturaIdm == 4))
                {
                    comprobante = FiltrarDatosCabecera(comprobante);
                }

                if (!modoLecturas.Any(eml => eml.ModoLecturaIdm == 2))
                {
                    comprobante = FiltrarDetalles(comprobante);
                }

                if (!modoLecturas.Any(eml => eml.ModoLecturaIdm == 3))
                {
                    comprobante = FiltrarImpuestos(comprobante);
                }
            }
            return comprobante;
        }

        private Comprobante FiltrarDatosCabeceraQR(Comprobante comprobante)
        {
            comprobante.Cotizacion = default;
            comprobante.ImporteTotal = default;

            return comprobante;
        }

        private Comprobante FiltrarDatosCabeceraOCR(Comprobante comprobante)
        {
            comprobante.NroRemito = default;
            comprobante.NroOrdenCompra = default;
            comprobante.CondicionVenta = default;
            comprobante.CondicionVentaId = default;
            comprobante.FechaVencimiento = default;
            comprobante.FechaVencimientoCodigoAutorizacion = default;

            return comprobante;
        }

        private Comprobante FiltrarDatosCabecera(Comprobante comprobante)
        {
            comprobante.ComprobanteTipo = default;
            comprobante.ComprobanteTipoId = default;
            comprobante.Numero = default;
            comprobante.PuntoVenta = default;
            comprobante.FechaEmision = default;
            comprobante.TipoCodigoAutorizacion = default;
            comprobante.TipoCodigoAutorizacionId = default;
            comprobante.CodigoAutorizacion = default;
            comprobante.Moneda = default;
            comprobante.MonedaId = default;
            comprobante.ImporteTotal = default;

            return comprobante;
        }

        private Comprobante FiltrarDetalles(Comprobante comprobante)
        {
            comprobante.Detalles = new();

            comprobante.ImporteBonificacion = default;
            comprobante.ImporteNeto = default;

            return comprobante;
        }

        private Comprobante FiltrarImpuestos(Comprobante comprobante)
        {
            comprobante.Impuestos = new();
            comprobante.Percepciones = new();

            comprobante.ImporteIVA = default;
            comprobante.ImporteOtrosTributosProv = default;
            comprobante.ImporteImpuestosInternos = default;
            comprobante.ImportePercepcionIIBB = default;
            comprobante.ImportePercepcionIVA = default;
            comprobante.ImportePercepcionMunicipal = default;

            return comprobante;
        }

        private async Task<EmpresaPortal> GenerarNuevaEmpresaPortal(Company company, IComprobanteAnalysisResult analisis, EmailInvoiceAttachmentDto facturaEnCorreoAdjunta, IntegracionFacturaPorCorreo integracion, StringBuilder sbObservaciones)
        {
            string razonSocial = string.IsNullOrWhiteSpace(analisis.RazonSocialPro) ? analisis.NroIdentificacionFiscalPro : analisis.RazonSocialPro;

            EmpresaCurrencyCreate moneda = new()
            {
                CurrencyId = 1,
                MonedaPorDefecto = true,
                Deleted = false
            };
            var monedas = new List<IEmpresaCurrencyCreate>() { moneda };

            EmpresasCreate command = new EmpresasCreate
            {
                CodigoProveedor = "-",
                RazonSocial = razonSocial,
                NombreFantasia = analisis.NroIdentificacionFiscalPro,
                IdentificadorTributario = analisis.NroIdentificacionFiscalPro,
                GranContribuyente = false,
                RolesIdm = new() { RolTipo.PROV_IDM },
                AlicuotasIdm = new(),
                PaisId = 1, //Argentina | No hay una constante para esto.
                TelefonoPrincipal = "-",
                EmailPrincipal = facturaEnCorreoAdjunta.EmailFrom,
                Contacto = "-",
                TipoResponsableId = analisis.CategoriaTipoReceptorId ?? CategoriaTipo.RESPONSABLE_INSCRIPTO,
                Confirmado = false,
                Monedas = monedas
            };

            EmpresaPortal empresaPortal = await _empresasPortalService.CreateAsync(command);
            empresaPortal.Company = company;
            empresaPortal.OrganizationId = company.OrganizationId;

            IntegracionFacturaPorCorreoDetalle integracionFacturaPorCorreoDetalle = new()
            {
                IntegracionFacturaPorCorreo = integracion,
                EmpresaPortal = empresaPortal,
                Actvive = true
            };

            _dbContext.EmpresasPortales.Add(empresaPortal);
            _dbContext.IntegracionFacturaPorCorreoDetalles.Add(integracionFacturaPorCorreoDetalle);

            AgregarObservacion(sbObservaciones, $"Se registro la EmpresaPortal: {empresaPortal.Guid}.");

            return empresaPortal;
        }

        private async Task<Comprobante> ProcesarComprobanteAsync(Comprobante comprobante, ConcurrentBag<Comprobante> comprobantesAgregados, StringBuilder sbObservaciones)
        {
            var comprobanteAgregado = BuscarEnAgregados(comprobante, comprobantesAgregados);
            if (comprobanteAgregado is not null)
            {
                AgregarObservacion(sbObservaciones, $"Se actualiza el comprobante nuevo: {comprobanteAgregado.Guid}.");
                return _comprobanteService.Update(comprobanteAgregado, comprobante);
            }

            var comprobanteExistente = await BuscarEnBaseDeDatosAsync(comprobante);
            if (comprobanteExistente is not null)
            {
                if (comprobanteExistente.ComprobanteEstadoId != ComprobanteEstado.BORRADOR) throw new DuplicateComprobanteException(comprobante);

                AgregarObservacion(sbObservaciones, $"Se actualiza el comprobante: {comprobanteExistente.Id}.");
                return await _comprobanteService.UpdateAsync(comprobanteExistente.Id, comprobante);
            }

            comprobantesAgregados.Add(comprobante);
            _dbContext.Comprobantes.Add(comprobante);
            AgregarObservacion(sbObservaciones, "Procesado correctamente.");
            return comprobante;
        }

        private Comprobante BuscarEnAgregados(Comprobante comprobante, ConcurrentBag<Comprobante> agregados)
        {
            return agregados.FirstOrDefault(c =>
                c.CompanyId == comprobante.CompanyId &&
                c.Empresa == comprobante.Empresa &&
                c.ComprobanteTipoId == comprobante.ComprobanteTipoId &&
                c.PuntoVenta == comprobante.PuntoVenta &&
                c.Numero == comprobante.Numero);
        }

        private async Task<Comprobante> BuscarEnBaseDeDatosAsync(Comprobante comprobante)
        {
            return await _dbContext.Comprobantes.FirstOrDefaultAsync(c =>
                c.CompanyId == comprobante.CompanyId &&
                c.Empresa == comprobante.Empresa &&
                c.ComprobanteTipoId == comprobante.ComprobanteTipoId &&
                c.PuntoVenta == comprobante.PuntoVenta &&
                c.Numero == comprobante.Numero);
        }

        private void AgregarObservacion(StringBuilder sb, string mensaje)
        {
            if (sb.Length > 0) sb.Append(" | ");
            sb.Append(mensaje);
        }

        private async Task<string> SubirArchivoAsync(byte[] fileContentBytes, string originalFileName, string finalPathId, BasicFileConfigurationTypeGSFWFTS config)
        {
            var fileTransferService = _fileTransferServiceBuilder.GetIWebFileTransferService(StorageTypeGSFWFTS.FileSystemStorage, config);

            var stream = new MemoryStream(fileContentBytes);
            string fileName = $"{Guid.NewGuid():N}_{originalFileName}";
            IFormFile file = new FormFile(stream, 0, fileContentBytes.Length, "", fileName);

            // Subir a carpeta temporal
            var tempFolder = $"Comprobante_{DateTime.Now:yyyyMMddHHmmssfff}";
            await fileTransferService.UploadTempFile(file, tempFolder);

            // Subir a carpeta final
            var finalPath = Path.Combine(finalPathId, fileName);
            await fileTransferService.UploadFinalDirectoryFile(file, finalPath);

            return fileName;
        }

        private async Task GuardarArchivoEmailAdjuntoAsync(EmailInvoiceAttachmentDto email)
        {
            var pathId = $"Cmp_{DateTime.Now:yyyyMMdd}";
            var fileName = await SubirArchivoAsync(email.FileContentBytes, email.FileName, pathId, EmailProcessorFileConfiguration.Config);

            email.ComprobanteEMailExtension.NombreArchivo = fileName;
        }

        private async Task<Comprobante> GuardarArchivoComprobanteAsync(Comprobante comprobante, EmailInvoiceAttachmentDto email)
        {
            var pathId = $"Cmp_{comprobante.Guid}";
            var fileName = await SubirArchivoAsync(email.FileContentBytes, email.FileName, pathId, ProveedoresFileConfiguration.Comprobantes);

            comprobante.NombreArchivo = fileName;
            return comprobante;
        }

        private void FinalizarProcesamientoComprobante(ComprobanteEMailExtension comprobanteEMailExtension, int procesoId, Company company, Comprobante comprobante, StringBuilder sbObservaciones, string statusCode)
        {
            comprobanteEMailExtension.ProcesoId = procesoId;
            comprobanteEMailExtension.CompanyId = company.Id;
            comprobanteEMailExtension.EmpresaPortalGuid = comprobante?.Empresa?.Guid.ToString().ToUpper();
            comprobanteEMailExtension.ComprobanteGuid = comprobante?.Guid.ToString().ToUpper();
            comprobanteEMailExtension.Observaciones = sbObservaciones.ToString();
            comprobanteEMailExtension.StatusCode = statusCode;
        }
    }
}