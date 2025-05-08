export default class ResultadoProceso {
    constructor(dto) {
        this.id = dto ? dto.id : null;
        this.interfazIdm = dto ? dto.interfazIdm : null;
        this.interfaz = dto ? dto.interfaz : null;
        this.sistema = dto ? dto.sistema : null;
        this.sistemaIdm = dto ? dto.sistemaIdm : null;
        this.tipoDescripcion = dto ? dto.tipoDescripcion : null;
        this.tipo = dto ? dto.tipo : null;
        this.estadoIdm = dto ? dto.estadoIdm : null;
        this.estado = dto ? dto.estado : null;
        this.estadoModificacionFechaHora = dto ? new Date(dto.estadoModificacionFechaHora).toLocaleString() : null;
        this.archivoNombre = dto ? dto.archivoNombre : null;
        this.cantidadRegistrosLeidos = dto ? dto.cantidadRegistrosLeidos : null;
        this.cantidadRegistrosIgnorados = dto ? dto.cantidadRegistrosIgnorados : null;
        this.cantidadRegistrosNoProcesados = dto ? dto.cantidadRegistrosNoProcesados : null;
        this.reglaEnforzadas = dto ? dto.reglaEnforzadas : [];
    }
}