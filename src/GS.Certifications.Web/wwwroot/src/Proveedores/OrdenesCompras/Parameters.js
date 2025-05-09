import BaseParameters from "@/Common/BaseParameters";

const DEFAULT_ORDER_PROPERTY_NAME = "NumeroOrden"; // Settear columna de ordenamiento default
const DEFAULT_ORDER_DIRECTION = "0"; // Settear dirección de ordenamiento default

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
        this.empresaPortalId = null;
        this.fechaDesde = null;
        this.fechaHasta = null;
        this.ordenCompraTipoId = null;
        this.ordenCompraEstadoIdm = null;
    }
}