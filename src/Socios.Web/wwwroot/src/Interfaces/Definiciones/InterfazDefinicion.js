export default class ResultadoProceso {
    constructor(dto) {
        this.idm = dto ? dto.idm : null;
        this.interfazIdm = dto ? dto.interfazIdm : null;
        this.interfaz = dto ? dto.interfaz : null;
        this.sistema = dto ? dto.sistema : null;
        this.sistemaIdm = dto ? dto.sistemaIdm : null;
        this.descripcion = dto ? dto.descripcion : null;
        this.numeroOrdenCampo = dto ? dto.numeroOrdenCampo : null;
        this.explicacion = dto ? dto.explicacion : null;
        this.campoExterno = dto ? dto.campoExterno : null;
        this.campoMiddleware = dto ? dto.campoMiddleware : null;
    }
}