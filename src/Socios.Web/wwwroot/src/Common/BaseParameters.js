export default class BaseParameters {
    constructor(orderPropertyName, orderDirection) {
        this.start = '0';
        this.length = 25;
        this.orderPropertyName = orderPropertyName;
        this.orderDirection = orderDirection; // "0" -> ASC, "1" -> DESC
    }
}