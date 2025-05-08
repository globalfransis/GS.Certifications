using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Impuestos;
using GS.Certifications.Domain.Entities.Percepciones;
using GS.Certifications.Domain.Entities.Periodos;
using GSF.Domain.Common;
using GSF.Domain.Entities.Companies;
using GSF.Domain.Entities.Global;
using System;
using System.Collections.Generic;

namespace GS.Certifications.Domain.Entities.Comprobantes;

public class Comprobante : BaseIntEntity
{
    public Guid Guid { get; private set; }
    public string NroIdentificacionFiscalPro { get; set; }
    public string DomicilioPro { get; set; }
    public short? CategoriaTipoEmisorId { get; set; }
    public CategoriaTipo CategoriaTipoEmisor { get; set; }
    public short? ComprobanteTipoId { get; set; }
    public ComprobanteTipo ComprobanteTipo { get; set; }
    // TODO: no es nullable
    public short? CategoriaTipoReceptorId { get; set; }
    public CategoriaTipo CategoriaTipoReceptor { get; set; }
    public string NroIdentificacionFiscalCompany { get; set; }
    public int PuntoVenta { get; set; }
    public string Letra { get; set; }
    public int Numero { get; set; }
    public DateTime? FechaEmision { get; set; }
    public DateTime? FechaVencimiento { get; set; }
    public DateTime? FechaVencimientoCodigoAutorizacion { get; set; }
    public DateTime FechaRegistracion { get; set; }
    public short? TipoCodigoAutorizacionId { get; set; }
    public CodigoAutorizacionTipo TipoCodigoAutorizacion { get; set; }
    public string CodigoAutorizacion { get; set; }
    public long? MonedaId { get; set; }
    public Currency Moneda { get; set; }
    public decimal ImporteNeto { get; set; } = default;
    public decimal ImporteTotal { get; set; } = default;
    public decimal ImporteIVA { get; set; } = default;
    public decimal ImporteBonificacion { get; set; } = default;
    public decimal ImportePercepcionIVA { get; set; } = default;
    public decimal ImportePercepcionIIBB { get; set; } = default;
    public decimal ImportePercepcionMunicipal { get; set; } = default;
    public decimal ImporteImpuestosInternos { get; set; } = default;
    public decimal ImporteOtrosTributosProv { get; set; } = default;
    // TODO: no es nullable
    public int? EmpresaId { get; set; }
    public EmpresaPortal Empresa { get; set; }
    // TODO: no es nullable
    public long? CompanyId { get; set; }
    public Company Company { get; set; }
    public string CodigoHES { get; set; }
    public int ComprobanteEstadoId { get; set; } = ComprobanteEstado.BORRADOR;
    public ComprobanteEstado ComprobanteEstado { get; set; }
    public string Comentarios { get; set; }
    public string MotivoRechazo { get; set; }
    public DateTime? FechaRechazo { get; set; }
    public string UsuarioRechazo { get; set; }
    public List<ComprobanteDetalle> Detalles { get; set; } = new();
    public List<ImpuestoDetalle> Impuestos { get; set; } = new();
    public List<PercepcionDetalle> Percepciones { get; set; } = new();

    public bool? ValidacionQR { get; set; } = false;
    public string QRValor { get; set; }

    public short? EstadoValidacionARCAQRId { get; set; } = EstadoValidacionARCA.NO_VALIDADA;
    public EstadoValidacionARCA EstadoValidacionARCAQR { get; set; }

    public short? EstadoValidacionARCAId { get; set; } = EstadoValidacionARCA.NO_VALIDADA;
    public EstadoValidacionARCA EstadoValidacionARCA { get; set; }
    public string ObservacionesARCA { get; set; }
    public string ObservacionesARCAQR { get; set; }

    public Periodo Periodo { get; set; }
    public int? PeriodoId { get; set; }

    public short? OrigenId { get; set; }
    public Origen Origen { get; set; }
    public short? PropietarioActualId { get; set; }
    public Origen PropietarioActual { get; set; }
    public short? CondicionVentaId { get; set; } = CondicionVenta.OTRO;
    public CondicionVenta CondicionVenta { get; set; }
    public string NroRemito { get; set; }
    public string NroOrdenCompra { get; set; }
    public decimal? Cotizacion { get; set; }
    public string NombreArchivo { get; set; }

    public Comprobante()
    {
        Guid = Guid.NewGuid();
    }
}


public class ComprobanteDetalle : BaseIntEntity
{
    public int ComprobanteId { get; set; }
    public Comprobante Comprobante { get; set; }
    public short? UnidadMedidaId { get; set; }
    public UnidadMedida UnidadMedida { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; } = default;
    public decimal ImporteBonificacion { get; set; } = default;
    public decimal Subtotal { get; set; } = default;
    public decimal ImporteIVA { get; set; } = default;
    public bool Exento { get; set; }
    public decimal Alicuota { get; set; } = default;
    public int? OrdenCompraId { get; set; }
    public string Detalle { get; set; }
}