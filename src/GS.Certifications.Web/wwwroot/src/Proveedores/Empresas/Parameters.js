import BaseParameters from "@/Common/BaseParameters";

const DEFAULT_ORDER_PROPERTY_NAME = "Created"; // Settear columna de ordenamiento default
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
        // Definir e inicializar parámetros de busqueda con sus valores default
        this.id = null;
        this.razonSocial = null;
        this.nombreFantasia = null;
        this.identificadorTributario = null;
        this.granContribuyente = null;
        this.contacto = null;
        this.paisId = null;
        this.provinciaId = null;
        this.ciudadId = null;
    }
}