String.prototype.toPascalCase = function () {
    return this.substr(0, 1).toUpperCase() + this.substr(1);
}

String.prototype.toCamelCase = function () {
    return this.substr(0, 1).toLowerCase() + this.substr(1);
}