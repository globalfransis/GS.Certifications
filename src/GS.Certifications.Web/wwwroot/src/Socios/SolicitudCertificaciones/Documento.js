export default class Documento {
    constructor(dto) {
        this.id = dto ? dto.id : null;
        this.rowVersion = dto ? dto.rowVersion : null;
        this.fechaDesde = dto ? dto.fechaDesde ? new Date(dto.fechaDesde).toISOString().split('T')[0] : null : null;
        this.fechaHasta = dto ? dto.fechaHasta ? new Date(dto.fechaHasta).toISOString().split('T')[0] : null : null;
        this.solicitudId = dto ? dto.solicitudId : null;
        this.documentoRequeridoId = dto ? dto.documentoRequeridoId : null;
        this.archivoURL = dto ? dto.archivoURL : null;
        this.version = dto ? dto.version : null;
        this.estadoId = dto ? dto.estadoId : null;
        this.validadoPorId = dto ? dto.validadoPorId : null;
        this.validadoPor = dto ? dto.validadoPor : null;
        this.fechaSubida = dto ? dto.fechaSubida : null;
        this.guid = dto ? dto.guid : null;
        this.solicitudGuid = dto ? dto.solicitudGuid : null;
        this.propietarioActualId = dto ? dto.propietarioActualId : null;
        this.observaciones = dto ? dto.observaciones : null;
        this.motivoRechazo = dto ? dto.motivoRechazo : null;
        this.tipo = dto ? dto.tipo : null;
        this.operationId = dto ? dto.operationId : null;
        this.operationStatus = dto ? dto.operationStatus : null;
    }
}