export default class ImpuestoCreateDto {
    constructor(dto) {
        this.rowVersion = dto ? dto.rowVersion : null;
        this.nombre = dto ? dto.nombre : null;
        this.descripcion = dto ? dto.descripcion : null;
        this.tipoId = dto ? dto.tipoId : null;
        this.provinciaId = dto ? dto.provinciaId : null;
        this.alicuotaId = dto ? dto.alicuotaId : null;
        this.valor = dto ? dto.valor : null;
    }
}