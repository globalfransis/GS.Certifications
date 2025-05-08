import CrudMode from "@/common/CrudMode";

export default class ComprobantePercepcion {
    constructor(dto) {
        this.id = dto ? dto.id ? dto.id : 0 : 0;
        this.rowVersion = dto ? dto.rowVersion : null;
        this.comprobanteId = dto ? dto.comprobanteId : null;
        this.percepcionId = dto ? dto.percepcionId : null;
        this.descripcion = dto ? dto.descripcion : null;
        this.alicuota = dto ? dto.alicuota : 0;
        this.importeTotal = dto ? dto.importeTotal : 0;
        this.percepcion = dto ? dto.percepcion : "-";
        this.mode = new CrudMode().setDetail();
        this.deleted = false;
    }
}