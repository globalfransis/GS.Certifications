export default class UsuarioInternoDto {
    constructor(dto) {
        this.id = dto ? dto.id : null;
        this.empresaPortalId = dto ? dto.empresaPortalId : null;
        this.email = dto ? dto.email : null;
        this.roles = dto ? dto.roles : [];
    }
}