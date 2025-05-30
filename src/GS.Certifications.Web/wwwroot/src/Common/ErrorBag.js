import '@/common/ui-extensions'
import loc from "@/common/commonLoc.js"

const GSFWEBFILETRANSFERSERVICEEXCEPTION = "GSFWebFileTransferServiceException";

export default class ErrorBag {
    constructor() {
        const DBUPDATECONCURRENCYEEXEPTION = "DbUpdateConcurrencyException";
        this.errors = {}
    }

    get(field) {
        //console.log('error for ' + field)
        if (field) {
            let err = this.errors[field.toCamelCase()]

            if (err === undefined) {
                err = this.errors[field.toPascalCase()]
            }

            return (err !== undefined && err.length > 0) ? loc[err[0]] : ''

        }
        else {
            return loc[this.errors['']] || ''
        }
    }

    /**
     * Returns the messages of all the errors in a single string in differents lines.
     * @returns A multi line string with the messages of all errors.
     */
     getAll() {
        var message = "";
        for (const [key, value] of Object.entries(this.errors)) {
            message = "\n" + value;
        }
        return loc[message];
    }

    getGSFWebFileTransferServiceException() {
        if (GSFWEBFILETRANSFERSERVICEEXCEPTION) {
            let err = this.errors[GSFWEBFILETRANSFERSERVICEEXCEPTION.toCamelCase()]

            if (err === undefined) {
                err = this.errors[GSFWEBFILETRANSFERSERVICEEXCEPTION.toPascalCase()]
            }

            return (err !== undefined && err.length > 0) ? err[0] : ''

        }
        else {
            return this.errors[''] || ''
        }
    }

    add(errorList) {
        this.errors = errorList
    }

    addError(key, message) {
        this.errors[key] = message
    }

    clear() {
        clearMessage()
        this.errors = {}
    }

    hasGSFWebFileTransferServiceException() {
        return this.get(GSFWEBFILETRANSFERSERVICEEXCEPTION).length > 0;
    }

    hasErrors() {
        return Object.keys(this.errors).length > 0
    }

    hasGlobalErrors() {
        return this.get('').length > 0
    }
}