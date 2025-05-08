export default class ReglaDefinicion {
    constructor(dto) {
        this.idm = dto ? dto.idm : null;
        this.interfazIdm = dto ? dto.interfazIdm : null;
        this.interfaz = dto ? dto.interfaz : null;
        this.sistema = dto ? dto.sistema : null;
        this.sistemaIdm = dto ? dto.sistemaIdm : null;
        this.descripcion = dto ? dto.descripcion : null;
        this.consencuencia = dto ? dto.consencuencia : null;
        this.interfazReglaConsecuenciaIdm = dto ? dto.interfazReglaConsecuenciaIdm : null;
    }
}