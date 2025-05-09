import CrudMode from "@/common/CrudMode";

export default class ComprobanteImpuesto {
    constructor(dto) {
        this.id = dto ? dto.id ? dto.id : 0 : 0;
        this.rowVersion = dto ? dto.rowVersion : null;
        this.comprobanteId = dto ? dto.comprobanteId : null;
        this.impuestoId = dto ? dto.impuestoId : null;
        this.descripcion = dto ? dto.descripcion : null;
        this.impuesto = dto ? dto.impuesto : "-";
        this.importeTotal = dto ? dto.importeTotal : 0;
        this.mode = new CrudMode().setDetail();
        this.deleted = false;
    }
}