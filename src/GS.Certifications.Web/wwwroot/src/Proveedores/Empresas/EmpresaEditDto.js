import EmpresaMoneda from "./EmpresaMoneda.js"

export default class EmpresaEditDto {
    constructor(dto) {
        this.id = dto ? dto.id : null;
        this.codigoProveedor = dto ? dto.codigoProveedor : null;
        this.razonSocial = dto ? dto.razonSocial : null;
        this.nombreFantasia = dto ? dto.nombreFantasia : null;
        this.identificadorTributario = dto ? dto.identificadorTributario : null;
        this.granContribuyente = dto ? dto.granContribuyente : null;
        this.direccion = dto ? dto.direccion : null;
        this.codigoPostal = dto ? dto.codigoPostal : null;
        this.paisId = dto ? (dto.pais ? dto.pais.id : null) : null;
        this.provinciaId = dto ? (dto.provincia ? dto.provincia.id : null) : null;
        this.ciudadId = dto ? (dto.ciudad ? dto.ciudad.id : null) : null;
        this.ciudadDescripcion = dto ? dto.ciudadDescripcion : null;
        this.telefonoPrincipal = dto ? dto.telefonoPrincipal : null;
        this.telefonoAlternativo = dto ? dto.telefonoAlternativo : null;
        this.emailPrincipal = dto ? dto.emailPrincipal : null;
        this.emailAlternativo = dto ? dto.emailAlternativo : null;
        this.contacto = dto ? dto.contacto : null;
        this.contactoAlternativo = dto ? dto.contactoAlternativo : null;
        this.tipoResponsableId = dto ? (dto.tipoResponsable ? dto.tipoResponsable.idm : null) : null;
        this.numeroIngresosBrutos = dto ? dto.numeroIngresosBrutos : null;
        this.tipoCuentaId = dto ? (dto.tipoCuenta ? dto.tipoCuenta.idm : null) : null;
        this.cuentaBancaria = dto ? dto.cuentaBancaria : null;
        this.idMoneda = dto ? dto.idMoneda : null;
        this.paginaWeb = dto ? dto.paginaWeb : null;
        this.redesSociales = dto ? dto.redesSociales : null;
        this.descripcionEmpresa = dto ? dto.descripcionEmpresa : null;
        this.productosServiciosOfrecidos = dto ? dto.productosServiciosOfrecidos : null;
        this.referenciasComerciales = dto ? dto.referenciasComerciales : null;
        this.confirmado = dto ? dto.confirmado : true;
        this.rolesIdm = dto ? dto.roles.map(r => r.idm) : [];
        this.alicuotasIdm = dto ? dto.alicuotas.map(r => r.idm) : [];
        this.monedas = [];
        if(dto && dto.monedas){
            dto.monedas.forEach(m => {
                this.monedas.push(new EmpresaMoneda(m))
            })
        }
    }
}