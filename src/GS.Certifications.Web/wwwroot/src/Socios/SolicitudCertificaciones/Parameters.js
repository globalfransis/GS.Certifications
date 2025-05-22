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
        this.nombre = null;
        this.activa = null;
        this.estadoId = null;
        this.certificacionId = 1;
        this.socioId = null;
    }
}