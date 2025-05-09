export default class TemplateDto {
    constructor(dto) {
        this.id = dto ? dto.id : null;
        this.rowVersion = dto ? dto.rowVersion : null;
        this.descripcion = dto ? dto.descripcion : null;
        // Agregar el resto de los campos del dto
    }
}