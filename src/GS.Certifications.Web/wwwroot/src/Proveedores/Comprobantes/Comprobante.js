import ComprobanteDetalle from "./ComprobanteDetalle";
import ComprobantePercepcion from "./ComprobantePercepcion";
import ComprobanteImpuesto from "./ComprobanteImpuesto";

const NO_VALIDADA = 4;

export default class Comprobante {
    constructor(dto) {
        this.id = dto ? dto.id : null;
        this.guid = dto ? dto.guid : null;
        this.rowVersion = dto ? dto.rowVersion : null;
        this.nroIdentificacionFiscalPro = dto ? dto.nroIdentificacionFiscalPro : null;
        this.domicilioPro = dto ? dto.domicilioPro : null;
        this.categoriaTipoEmisorId = dto ? dto.categoriaTipoEmisorId : null;
        this.comprobanteTipoId = dto ? dto.comprobanteTipoId : null;
        this.categoriaTipoReceptorId = dto ? dto.categoriaTipoReceptorId : null;
        this.nroIdentificacionFiscalCompany = dto ? dto.nroIdentificacionFiscalCompany : null;
        this.puntoVenta = dto ? dto.puntoVenta : null;
        this.letra = dto ? dto.letra : null;
        this.numero = dto ? dto.numero : null;
        this.fechaEmision = dto ? dto.fechaEmision ? new Date(dto.fechaEmision).toISOString().split('T')[0] : null : null;
        this.fechaRegistracion = dto ? dto.fechaRegistracion : null;
        this.tipoCodigoAutorizacionId = dto ? dto.tipoCodigoAutorizacionId : null;
        this.codigoAutorizacion = dto ? dto.codigoAutorizacion : null;
        this.monedaId = dto ? dto.monedaId : null;
        this.importeNeto = dto ? dto.importeNeto ? dto.importeNeto : 0 : 0;
        this.importeTotal = dto ? dto.importeTotal ? dto.importeTotal : 0 : 0;
        this.importeIVA = dto ? dto.importeIVA ? dto.importeIVA : 0 : 0;
        this.importeBonificacion = dto ? dto.importeBonificacion ? dto.importeBonificacion : 0 : 0;
        this.iva21 = dto ? dto.iva21 ? dto.iva21 : 0 : 0;
        this.iva105 = dto ? dto.iva105 ? dto.iva105 : 0 : 0;
        this.empresaId = dto ? dto.empresaId : null;
        this.companyId = dto ? dto.companyId : null;
        this.codigoHES = dto ? dto.codigoHES : null;
        this.comprobanteEstadoId = dto ? dto.comprobanteEstadoId : null;
        this.comentarios = dto ? dto.comentarios : null;

        this.importeImpuestosInternos = dto ? dto.importeImpuestosInternos : 0;
        this.importeOtrosTributosProv = dto ? dto.importeOtrosTributosProv : 0;
        this.importePercepcionIVA = dto ? dto.importePercepcionIVA : 0;
        this.importePercepcionIIBB = dto ? dto.importePercepcionIIBB : 0;

        this.motivoRechazo = dto ? dto.motivoRechazo ? dto.motivoRechazo : "" : "";
        this.proveedor = dto ? dto.proveedor ? dto.proveedor : "" : "";

        this.detalles = [];
        this.impuestos = [];
        this.percepciones = [];

        this.validacionQR = dto ? dto.validacionQR : false;
        this.qrValor = dto ? dto.qrValor : null;
        this.periodoId = dto ? dto.periodoId : null;

        this.estadoConstatacion = dto ? dto.estadoConstatacion : NO_VALIDADA;
        this.estadoValidacionARCAId = dto ? dto.estadoValidacionARCAId : null;
        this.estadoValidacionARCAQRId = dto ? dto.estadoValidacionARCAQRId : null;
        this.observacionesARCA = dto ? dto.observacionesARCA : null;

        this.condicionVentaId = dto ? dto.condicionVentaId : null;
        this.nroRemito = dto ? dto.nroRemito : null;
        this.nroOrdenCompra = dto ? dto.nroOrdenCompra : null;
        this.cotizacion = dto ? dto.cotizacion : 1;

        this.propietarioActualId = dto ? dto.propietarioActualId : null;

        this.nombreArchivo = dto ? dto.nombreArchivo : "";

        this.comprobante = dto ? dto.comprobante : "";
        
        this.fechaVencimiento = dto ? dto.fechaVencimiento ? new Date(dto.fechaVencimiento).toISOString().split('T')[0] : null : null;
        this.fechaVencimientoCodigoAutorizacion = dto ? dto.fechaVencimientoCodigoAutorizacion ? new Date(dto.fechaVencimientoCodigoAutorizacion).toISOString().split('T')[0] : null : null;

        if (dto && dto.detalles && dto.detalles.length) {
            dto.detalles.forEach(element => {
                this.detalles.push(new ComprobanteDetalle(element));
            });
        }

        if (dto && dto.impuestos && dto.impuestos.length) {
            dto.impuestos.forEach(element => {
                this.impuestos.push(new ComprobanteImpuesto(element));
            });
        }

        if (dto && dto.percepciones && dto.percepciones.length) {
            dto.percepciones.forEach(element => {
                this.percepciones.push(new ComprobantePercepcion(element));
            });
        }
    }
}