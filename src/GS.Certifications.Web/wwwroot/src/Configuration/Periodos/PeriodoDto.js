export default class PeriodoDto {
    constructor(dto) {
        this.id = dto ? dto.id : null;
        this.rowVersion = dto ? dto.rowVersion : null;
        this.año = dto ? dto.año : null;
        this.numeroPeriodo = dto ? dto.numeroPeriodo : null;
        this.fechaInicio = dto ? dto.fechaInicio : null;
        this.fechaFin = dto ? dto.fechaFin : null;
        this.estado = dto ? dto.estado : null;
    }
}