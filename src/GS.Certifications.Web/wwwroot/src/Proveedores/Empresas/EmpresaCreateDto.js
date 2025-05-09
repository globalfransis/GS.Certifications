export default class EmpresaCreateDto {
    constructor(dto) {
        this.id = dto ? dto.id : null;
        this.codigoProveedor = dto ? dto.codigoProveedor : null;
        this.razonSocial = dto ? dto.razonSocial : null;
        this.nombreFantasia = dto ? dto.nombreFantasia : null;
        this.identificadorTributario = dto ? dto.identificadorTributario : null;
        this.granContribuyente = dto ? dto.granContribuyente : null;
        this.direccion = dto ? dto.direccion : null;
        this.codigoPostal = dto ? dto.codigoPostal : null;
        this.paisId = dto ? dto.paisId : null;
        this.provinciaId = dto ? dto.provinciaId : null;
        this.ciudadId = dto ? dto.ciudadId : null;
        this.ciudadDescripcion = dto ? dto.ciudadDescripcion : null;
        this.telefonoPrincipal = dto ? dto.telefonoPrincipal : null;
        this.telefonoAlternativo = dto ? dto.telefonoAlternativo : null;
        this.emailPrincipal = dto ? dto.emailPrincipal : null;
        this.emailAlternativo = dto ? dto.emailAlternativo : null;
        this.contacto = dto ? dto.contacto : null;
        this.contactoAlternativo = dto ? dto.contactoAlternativo : null;
        this.tipoResponsableId = dto ? dto.tipoResponsableId : null;
        this.numeroIngresosBrutos = dto ? dto.numeroIngresosBrutos : null;
        this.tipoCuentaId = dto ? dto.tipoCuentaId : null;
        this.cuentaBancaria = dto ? dto.cuentaBancaria : null;
        this.paginaWeb = dto ? dto.paginaWeb : null;
        this.redesSociales = dto ? dto.redesSociales : null;
        this.descripcionEmpresa = dto ? dto.descripcionEmpresa : null;
        this.productosServiciosOfrecidos = dto ? dto.productosServiciosOfrecidos : null;
        this.referenciasComerciales = dto ? dto.referenciasComerciales : null;
        this.confirmado = dto ? dto.confirmado : true;
        this.rolesIdm = dto ? dto.rolesIdm : [];
        this.alicuotasIdm = dto ? dto.alicuotasIdm : [];
        this.ordenesComprasTiposId = dto ? dto.ordenesComprasTiposId : [];
        this.conceptosGastosTiposId = dto ? dto.conceptosGastosTiposId: [];
        this.monedas = dto ? dto.monedas: [];
    }
}