import EmpresaMoneda from "./EmpresaMoneda.js"

export default class EmpresaDto {
    constructor(dto) {
        this.id = dto ? dto.id : null;
        this.codigoProveedor = dto ? dto.codigoProveedor : null;
        this.razonSocial = dto ? dto.razonSocial : null;
        this.nombreFantasia = dto ? dto.nombreFantasia : null;
        this.identificadorTributario = dto ? dto.identificadorTributario : null;
        this.granContribuyente = dto ? dto.granContribuyente : null;
        this.direccion = dto ? dto.direccion : null;
        this.codigoPostal = dto ? dto.codigoPostal : null;
        this.pais = dto ? dto.pais : null;
        this.provincia = dto ? dto.provincia : null;
        this.ciudad = dto ? dto.ciudad : null;
        this.ciudadDescripcion = dto ? dto.ciudadDescripcion : null;
        this.telefonoPrincipal = dto ? dto.telefonoPrincipal : null;
        this.telefonoAlternativo = dto ? dto.telefonoAlternativo : null;
        this.emailPrincipal = dto ? dto.emailPrincipal : null;
        this.emailAlternativo = dto ? dto.emailAlternativo : null;
        this.contacto = dto ? dto.contacto : null;
        this.contactoAlternativo = dto ? dto.contactoAlternativo : null;
        this.tipoResponsable = dto ? dto.tipoResponsable : null;
        this.numeroIngresosBrutos = dto ? dto.numeroIngresosBrutos : null;
        this.tipoCuenta = dto ? dto.tipoCuenta : null;
        this.cuentaBancaria = dto ? dto.cuentaBancaria : null;
        this.idMoneda = dto ? dto.idMoneda : null;
        this.paginaWeb = dto ? dto.paginaWeb : null;
        this.redesSociales = dto ? dto.redesSociales : null;
        this.descripcionEmpresa = dto ? dto.descripcionEmpresa : null;
        this.productosServiciosOfrecidos = dto ? dto.productosServiciosOfrecidos : null;
        this.referenciasComerciales = dto ? dto.referenciasComerciales : null;
        this.confirmado = dto ? dto.confirmado : true;
        this.roles = dto ? dto.roles : [];
        this.alicuotas = dto ? dto.alicuotas : [];
        this.monedas = [];
        if(dto && dto.monedas){
            dto.monedas.forEach(m => {
                this.monedas.push(new EmpresaMoneda(m))
            })
        }
    }
}