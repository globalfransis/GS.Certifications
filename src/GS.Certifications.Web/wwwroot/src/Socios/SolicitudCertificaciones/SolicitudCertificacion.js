export default class SolicitudCertificacion {
    constructor(dto) {
        this.id = dto ? dto.id : null;
        this.socioId = dto ? dto.socioId : null;
        this.certificacionId = dto ? dto.certificacionId : 1;
        this.estadoId = dto ? dto.estadoId : null;
        this.socio = dto ? dto.socio : null;
        this.certificacion = dto ? dto.certificacion : null;
        this.estado = dto ? dto.estado : null;
        this.cantidadAprobaciones = dto ? dto.cantidadAprobaciones : 0;
        this.observaciones = dto ? dto.observaciones : null;
        this.motivoRechazo = dto ? dto.motivoRechazo : null;
        this.motivoRevision = dto ? dto.motivoRevision : null;
        this.rowVersion = dto ? dto.rowVersion : null;
        this.descripcion = dto ? dto.descripcion : null;
        this.guid = dto ? dto.guid : null;

        this.fechaSolicitud = dto ? dto.fechaSolicitud : null;
        this.ultimaModificacionEstado = dto ? dto.ultimaModificacionEstado : null;
        this.vigenciaDesde = dto ? dto.vigenciaDesde ? new Date(dto.vigenciaDesde).toISOString().split('T')[0] : null : null;
        this.vigenciaHasta = dto ? dto.vigenciaHasta ? new Date(dto.vigenciaHasta).toISOString().split('T')[0] : null : null;
        this.propietarioActualId = dto ? dto.propietarioActualId : null;
        this.origenId = dto ? dto.origenId : null;

        this.documentosCargados = dto ? dto.documentosCargados : []

    }
}