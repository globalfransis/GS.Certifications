import BaseParameters from "@/Common/BaseParameters";

const DEFAULT_ORDER_PROPERTY_NAME = "Id"//"UserActivity.UltimoAcceso"; // Settear columna de ordenamiento default
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
        this.Email = null;
        this.Nombre = null;
        this.Apellido = null;
        this.TipoUsuario = null;
        this.DominioId = null;
        this.FechaDesde = null;
        this.FechaHasta = null;
        this.Actividad = null;
    }
}