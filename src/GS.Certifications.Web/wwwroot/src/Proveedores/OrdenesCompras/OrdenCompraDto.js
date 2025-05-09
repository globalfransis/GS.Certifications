export default class OrdenCompraDto {
    constructor(dto) {
        this.id = dto ? dto.id : null;
        this.rowVersion = dto ? dto.rowVersion : null;
        this.numeroOrden = dto ? dto.numeroOrden : null;
        this.fecha = dto ? dto.fecha : null;
        this.empresaPortal = dto ? dto.empresaPortal : null;
        this.ordenCompraTipo = dto ? dto.ordenCompraTipo : null;
        //this.codigoHES = dto ? dto.codigoHES : null;
        this.codigoQAD = dto ? dto.codigoQAD : null;
        this.ordenCompraEstado = dto ? dto.ordenCompraEstado : null;
        this.importe = dto ? dto.importe : null;
        this.moneda = dto ? dto.moneda : null;
        this.observaciones = dto ? dto.observaciones : null;
    }
}