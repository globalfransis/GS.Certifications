export default class ImpuestoCreateDto {
    constructor(dto) {
        this.id = dto ? dto.id : null;
        this.rowVersion = dto ? dto.rowVersion : null;
        this.descripcion = dto ? dto.descripcion : null;
        this.percepcionTipoId = (dto && dto.percepcionTipo) ? dto.percepcionTipo.idm : null;
        this.provinciaId = (dto && dto.provincia) ? dto.provincia.id : null;
    }
}