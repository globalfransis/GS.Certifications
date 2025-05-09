export default class ImpuestoCreateDto {
    constructor(dto) {
        this.id = dto ? dto.id : null;
        this.rowVersion = dto ? dto.rowVersion : null;
        this.nombre = dto ? dto.nombre : null;
        this.descripcion = dto ? dto.descripcion : null;
        this.tipoId = dto ? dto.tipo.idm : null;
        this.provinciaId = (dto && dto.provincia) ? dto.provincia.id : null;
        this.alicuotaId = (dto && dto.alicuota) ? dto.alicuota.idm : null;
        this.valor = dto ? dto.valor : null;
    }
}