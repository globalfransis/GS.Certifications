export default class OrdenCompraEditDto {
    constructor(dto) {
        this.id = dto ? dto.id : null;
        this.rowVersion = dto ? dto.rowVersion : null;
        this.numeroOrden = dto ? dto.numeroOrden : null;
        this.fecha = dto ? dto.fecha : null;
        this.empresaPortalId = (dto && dto.empresaPortal) ? dto.empresaPortal.id : null;
        this.ordenCompraTipoId = (dto && dto.ordenCompraTipo) ? dto.ordenCompraTipo.id : null;
        //this.codigoHES = dto ? dto.codigoHES : null;
        this.codigoQAD = dto ? dto.codigoQAD : null;
        this.ordenCompraEstadoIdm = (dto && dto.ordenCompraEstado) ? dto.ordenCompraEstado.idm : null;
        this.importe = dto ? dto.importe : null;
        this.monedaId = (dto && dto.moneda) ? dto.moneda.idm : null;
        this.observaciones = dto ? dto.observaciones : null;
    }
}