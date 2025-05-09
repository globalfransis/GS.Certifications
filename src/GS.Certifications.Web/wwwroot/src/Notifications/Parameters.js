import BaseParameters from "@/Common/BaseParameters";

const DEFAULT_ORDER_PROPERTY_NAME = "FechaEncolado";
const DEFAULT_ORDER_DIRECTION = "1";

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
        this.FechaEncolado = null;
        this.FechaEnviado = null;
        this.Destinatarios = null;
        //this.tipo = null;
        this.ConCopia = null;
        this.ConCopiaOculta = null;
        this.Asunto = null;
        this.EstadoIdm = null;
    }
}

/*
 this.fechaEncolado = null;
        this.fechaEnviado = null;
        this.tipo = null;
        this.destinatarios = null;
        this.conCopia = null;
        this.conCopiaOculta = null;
        this.asunto = null;
        this.estadoIdm = null;
*/
