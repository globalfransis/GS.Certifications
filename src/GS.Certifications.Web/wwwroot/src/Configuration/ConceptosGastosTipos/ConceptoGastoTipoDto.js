export default class ConceptoGastoTipoDto {
    constructor(dto) {
        this.id = dto ? dto.id : null;
        this.rowVersion = dto ? dto.rowVersion : null;
        this.nombre = dto ? dto.nombre : null;
        this.descripcion = dto ? dto.descripcion : null;
        this.conceptoContableNombre = dto ? dto.conceptoContableNombre : null;
        this.conceptoContableValor = dto ? dto.conceptoContableValor : null;
    }
}