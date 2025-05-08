using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands
{
    public class SaveComprobanteDraftCommand : IRequest<int>
    {
        public string NroIdentificacionFiscalPro { get; set; }
        //public string DomicilioPro { get; set; }
        public short? CategoriaTipoEmisorId { get; set; }
        public short? ComprobanteTipoId { get; set; }
        public short? CategoriaTipoReceptorId { get; set; }
        public string NroIdentificacionFiscalCompany { get; set; }
        public int? PuntoVenta { get; set; }
        public string Letra { get; set; }
        public int? Numero { get; set; }
        public DateTime? FechaEmision { get; set; }
        public short? TipoCodigoAutorizacionId { get; set; }
        public string CodigoAutorizacion { get; set; }
        public long? MonedaId { get; set; }
        public decimal? ImporteNeto { get; set; }
        public decimal? ImporteTotal { get; set; }
        public decimal? ImporteIVA { get; set; } = 0;
        public decimal? ImporteBonificacion { get; set; }
        public decimal? ImportePercepcionIVA { get; set; } = 0;
        public decimal? ImportePercepcionIIBB { get; set; } = 0;
        public decimal? ImportePercepcionMunicipal { get; set; } = 0;
        public decimal? ImporteImpuestosInternos { get; set; } = 0;
        public decimal? ImporteOtrosTributosProv { get; set; } = 0;
        public int? EmpresaId { get; set; }
        public long? CompanyId { get; set; }
        public string CodigoHES { get; set; }
        public int? ComprobanteEstadoId { get; set; }
        public string Comentarios { get; set; }
        public List<ComprobanteDetalleCreate> Detalles { get; set; }
        public List<ComprobanteImpuestoCreate> Impuestos { get; set; }
        public List<ComprobantePercepcionCreate> Percepciones { get; set; }
        public bool? ValidacionQR { get; set; } = false;
        public string QRValor { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaVencimientoCodigoAutorizacion { get; set; }
    }

    public class SaveComprobanteDraftCommandHandler : BaseRequestHandler<int, SaveComprobanteDraftCommand, int>
    {
        private readonly ICertificationsDbContext Context;
        private readonly IComprobanteService ComprobanteService;

        public SaveComprobanteDraftCommandHandler(ICertificationsDbContext context, IComprobanteService comprobanteService)
        {
            Context = context;
            ComprobanteService = comprobanteService;
        }

        protected override async Task<int> HandleRequestAsync(SaveComprobanteDraftCommand request, CancellationToken cancellationToken)
        {
            var companyExtra = await Context.CompanyExtras.FirstOrDefaultAsync(c => c.CompanyId == request.CompanyId) ?? throw new Exception("No existen datos extra de la Company.");

            var comprobanteCreateArgs = new ComprobanteCreate()
            {
                NroIdentificacionFiscalPro = request.NroIdentificacionFiscalPro,
                //DomicilioPro = request.DomicilioPro,
                CategoriaTipoEmisorId = (short)request.CategoriaTipoEmisorId,
                ComprobanteTipoId = (short)request.ComprobanteTipoId,
                PuntoVenta = (int)request.PuntoVenta,
                Numero = (int)request.Numero,
                FechaEmision = (DateTime)request.FechaEmision,
                TipoCodigoAutorizacionId = (short)request.TipoCodigoAutorizacionId,
                CodigoAutorizacion = request.CodigoAutorizacion,
                MonedaId = (long)request.MonedaId,
                ImporteNeto = request.ImporteNeto ?? 0,
                ImporteTotal = request.ImporteTotal ?? 0,
                ImporteBonificacion = request.ImporteBonificacion ?? 0,
                Comentarios = request.Comentarios,
                Detalles = new(request.Detalles),
                Impuestos = new(request.Impuestos),
                Percepciones = new(request.Percepciones),
                EmpresaId = request.EmpresaId,

                ImporteIVA = request.ImporteIVA ?? 0,
                ImporteImpuestosInternos = (decimal)request.ImporteImpuestosInternos,
                ImporteOtrosTributosProv = (decimal)request.ImporteOtrosTributosProv,
                ImportePercepcionIIBB = (decimal)request.ImportePercepcionIIBB,
                ImportePercepcionIVA = (decimal)request.ImportePercepcionIVA,
                ImportePercepcionMunicipal = (decimal)request.ImportePercepcionMunicipal,

                ValidacionQR = request.ValidacionQR,
                QRValor = request.QRValor,

                FechaVencimiento = request.FechaVencimiento,
                FechaVencimientoCodigoAutorizacion = request.FechaVencimientoCodigoAutorizacion,

                // Company
                CategoriaTipoReceptorId = companyExtra.TipoResponsableId,
                NroIdentificacionFiscalCompany = companyExtra.IdentificadorTributario,
                CompanyId = request.CompanyId
            };

            var borrador = await ComprobanteService.SaveDraftAsync(comprobanteCreateArgs);
            await Context.SaveChangesAsync(cancellationToken);
            return borrador.Id;
        }
    }
}
