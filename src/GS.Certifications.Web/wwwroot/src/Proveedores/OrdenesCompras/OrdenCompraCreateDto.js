export default class OrdenCompraCreateDto {
    constructor(dto) {
        this.rowVersion = dto ? dto.rowVersion : null;
        this.numeroOrden = dto ? dto.numeroOrden : null;
        this.fecha = dto ? dto.fecha : null;
        this.empresaPortalId = dto ? dto.empresaPortalId : null;
        this.ordenCompraTipoId = dto ? dto.ordenCompraTipoId : null;
        //this.codigoHES = dto ? dto.codigoHES : null;
        this.codigoQAD = dto ? dto.codigoQAD : null;
        this.ordenCompraEstadoIdm = dto ? dto.ordenCompraEstadoIdm : 1;
        this.importe = dto ? dto.importe : null;
        this.monedaId = dto ? dto.monedaId : null;
        this.observaciones = dto ? dto.observaciones : null;
    }
}