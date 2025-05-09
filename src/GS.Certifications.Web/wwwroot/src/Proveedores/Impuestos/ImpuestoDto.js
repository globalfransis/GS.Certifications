export default class ImpuestoDto {
    constructor(dto) {
        this.id = dto ? dto.id : null;
        this.rowVersion = dto ? dto.rowVersion : null;
        this.nombre = dto ? dto.nombre : null;
        this.descripcion = dto ? dto.descripcion : null;
        this.tipo = dto ? dto.tipo : null;
        this.provincia = dto ? dto.provincia : null;
        this.company = dto ? dto.company : null;
        this.alicuota = dto ? dto.alicuota : null;
        this.valor = dto ? dto.valor : null;
    }
}