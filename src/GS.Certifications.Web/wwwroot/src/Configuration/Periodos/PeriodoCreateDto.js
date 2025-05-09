export default class PeriodoCreateDto {
    constructor(dto) {
        this.rowVersion = dto ? dto.rowVersion : null;
        this.año = dto ? dto.año : null;
        this.numeroPeriodo = dto ? dto.numeroPeriodo : null;
        this.fechaInicio = dto ? dto.fechaInicio : null;
        this.fechaFin = dto ? dto.fechaFin : null;
        this.estadoIdm = dto ? dto.estadoIdm : null;
    }
}