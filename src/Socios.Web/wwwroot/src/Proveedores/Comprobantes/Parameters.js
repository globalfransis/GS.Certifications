import BaseParameters from "@/Common/BaseParameters";

const DEFAULT_ORDER_PROPERTY_NAME = "Id"; // Settear columna de ordenamiento default
const DEFAULT_ORDER_DIRECTION = "1"; // Settear dirección de ordenamiento default

export default class Parameters extends BaseParameters {
    constructor(orderPropertyName, orderDirection) 
    {
        if (orderPropertyName == undefined) {
            orderPropertyName = DEFAULT_ORDER_PROPERTY_NAME;
        }
        if (orderDirection == undefined) {
            orderDirection = DEFAULT_ORDER_DIRECTION;
        }
        super(orderPropertyName, orderDirection);
        this.comprobanteTipoId = null;
        this.numero = null;
        this.fechaEmisionDesde = null;
        this.fechaEmisionHasta = null;
        this.tipoCodigoAutorizacionId = null;
        this.comprobanteEstadoId = null;
    }
}