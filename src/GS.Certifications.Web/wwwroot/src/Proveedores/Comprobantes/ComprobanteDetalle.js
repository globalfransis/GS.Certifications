import CrudMode from "@/common/CrudMode";

const UNIDAD_IDM = 13;

export default class ComprobanteDetalle {
    constructor(dto) {
        this.id = dto ? dto.id ? dto.id : 0 : 0;
        this.rowVersion = dto ? dto.rowVersion : null;
        this.comprobanteId = dto ? dto.comprobanteId : null;
        this.unidadMedidaId = dto ? dto.unidadMedidaId ? dto.unidadMedidaId : UNIDAD_IDM : UNIDAD_IDM;
        this.unidadMedida = dto ? dto.unidadMedida : "-";
        this.cantidad = dto ? dto.cantidad ? dto.cantidad : 1 : 1;
        this.precioUnitario = dto ? dto.precioUnitario ? dto.precioUnitario : 0 : 0;
        this.importeBonificacion = dto ? dto.importeBonificacion ? dto.importeBonificacion : 0 : 0;
        this.subtotal = dto ? dto.subtotal ? dto.subtotal : 0 : 0;
        this.importeIVA = dto ? dto.importeIVA ? dto.importeIVA : 0 : 0;
        this.detalle = dto ? dto.detalle : null;
        this.alicuota = dto ? dto.alicuota : null;
        this.mode = new CrudMode().setDetail();
        this.deleted = false;
    }
}