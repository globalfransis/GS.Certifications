export default class UsuarioExternoDto {
    constructor(dto) {
        this.id = dto ? dto.id : null;
        this.empresaPortalId = dto ? dto.empresaPortalId : null;
        this.login = dto ? dto.login : null;
        this.email = dto ? dto.email : null;
        this.firstName = dto ? dto.firstName : null;
        this.lastName = dto ? dto.lastName : null;
        this.idNumber = dto ? dto.idNumber : null;
        this.phoneNumber = dto ? dto.phoneNumber : null;
        this.roles = dto ? dto.roles : [];
    }
}