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
        this.rowVersion = dto ? dto.rowVersion : null;
        this.descripcion = dto ? dto.descripcion : null;

        this.documentosCargados = dto ? dto.documentosCargados : []

    }
}