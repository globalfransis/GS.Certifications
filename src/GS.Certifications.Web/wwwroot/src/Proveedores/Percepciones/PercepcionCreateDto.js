export default class PercepcionCreateDto {
    constructor(dto) {
        this.rowVersion = dto ? dto.rowVersion : null;
        this.descripcion = dto ? dto.descripcion : null;
        this.percepcionTipoId = dto ? dto.percepcionTipoId : null;
        this.provinciaId = dto ? dto.provinciaId : null;
    }
}