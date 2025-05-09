export default class PercepcionDto {
    constructor(dto) {
        this.id = dto ? dto.id : null;
        this.rowVersion = dto ? dto.rowVersion : null;
        this.descripcion = dto ? dto.descripcion : null;
        this.percepcionTipo = dto ? dto.percepcionTipo : null;
        this.provincia = dto ? dto.provincia : null;
        this.company = dto ? dto.company : null;
    }
}