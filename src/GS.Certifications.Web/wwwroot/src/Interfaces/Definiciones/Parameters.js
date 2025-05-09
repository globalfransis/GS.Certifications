import BaseParameters from "@/Common/BaseParameters";

const DEFAULT_ORDER_PROPERTY_NAME = "Idm";
const DEFAULT_ORDER_DIRECTION = "1";

export default class Parameters extends BaseParameters {
    constructor(orderPropertyName, orderDirection) {
        if (orderPropertyName == undefined) {
            orderPropertyName = DEFAULT_ORDER_PROPERTY_NAME;
        }
        if (orderDirection == undefined) {
            orderDirection = DEFAULT_ORDER_DIRECTION;
        }
        super(orderPropertyName, orderDirection);
        this.sistemaIdm = null;
        this.interfazId = null;
    }
}