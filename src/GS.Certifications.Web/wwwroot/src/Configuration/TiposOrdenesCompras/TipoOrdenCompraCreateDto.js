export default class TipoOrdenCompraCreateDto {
    constructor(dto) {
        this.id = dto ? dto.id : null;
        this.rowVersion = dto ? dto.rowVersion : null;
        this.nombre = dto ? dto.nombre : null;
        this.descripcion = dto ? dto.descripcion : null;
        this.esRequerida = dto ? dto.esRequerida : false;
        this.esAbierta = dto ? dto.esAbierta : null;
        this.esRecurrente = dto ? dto.esRecurrente : null;
        this.esUnica = dto ? dto.esUnica : null;
        this.gruposId = dto ? dto.gruposId : [];
    }
}