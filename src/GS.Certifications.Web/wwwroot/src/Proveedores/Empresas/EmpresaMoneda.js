import CrudMode from "@/common/CrudMode";

export default class EmpresaMoneda {
    constructor(dto) {
        this.id = dto ? dto.id : 0;
        this.rowVersion = dto ? dto.rowVersion : null;
        this.empresaPortalId = dto ? dto.empresaPortalId : null;
        this.currency = dto ? dto.currency : null;
        this.currencyId = dto ? dto.currencyId : null;
        this.monedaPorDefecto = dto ? dto.monedaPorDefecto : false;
        this.mode = new CrudMode().setDetail();
        this.deleted = false;
    }
}