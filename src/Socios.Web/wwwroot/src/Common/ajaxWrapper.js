const ERROR413MESSAGE = "Error al importar archivo. El o los archivos que se intentan importar superan el limite permitido.";//Otra opcion es importar aca loc y usarlo al momento de devolver el mensaje.

class defaultOptions {
    constructor() {
        this.errorBag = null
        this.sendJson = true
        this.headers = { 'Content-Type': 'application/json; charset=utf-8' }
    }

    errorCallback(response) {
        if (this.errorBag != null) {

            this.errorBag.add(response.errors)
        }
    }
}

class defaultFileOptions {
    constructor() {
        this.errorBag = null
        this.sendJson = true
    }
    errorCallback(response) {
        if (this.errorBag != null) {
    
            this.errorBag.add(response.errors)
        }
    }
}

export default class Ajax {
    async get(url, parameters, options) {
        let queryParams = new URLSearchParams()

        options = Object.assign(options || {}, { method: 'GET' })

        if (parameters && Object.keys(parameters).length > 0) {
            let params = Object.assign({}, parameters)
            this.sanitizeValues(params)
            for (const p in params) {
                if (Object.prototype.toString.call(params[p]) === '[object Array]') {
                    params[p].forEach(pp => {
                        queryParams.append(p, pp)
                    })
                } else {
                    queryParams.append(p, params[p])
                }
            }
            
            url = `${url}?${queryParams}`
        }

        return this.send(url, undefined, options)
    }

    async getFile(url, parameters, options) {
        let queryParams = new URLSearchParams()

        options = Object.assign(options || {}, { method: 'GET' })

        if (parameters && Object.keys(parameters).length > 0) {
            let params = Object.assign({}, parameters)
            this.sanitizeValues(params)
            for (const p in params) {
                if (Object.prototype.toString.call(params[p]) === '[object Array]') {
                    params[p].forEach(pp => {
                        queryParams.append(p, pp)
                    })
                } else {
                    queryParams.append(p, params[p])
                }
            }

            url = `${url}?${queryParams}`
        }

        return this.fetchFile(url, undefined, options)
    }

    async post(url, params, options) {
        options = Object.assign(options || {}, { method: 'POST' })
        return this.send(url, params, options)
    }

    async postFile(url, params, options) {
        options = Object.assign(options || {}, { method: 'POST' })
        return this.sendFile(url, params, options)
    }

    async put(url, params, options) {
        options = Object.assign(options || {}, { method: 'PUT' })
        return this.send(url, params, options)
    }

    async delete(url, params, options) {
        options = Object.assign(options || {}, { method: 'DELETE' })
        return this.send(url, params, options)
    }

    //#region Methods to be considered as Private Methods

    sanitizeValues(params) {
        //replace null and undefined with ''
        for (const p in params) { if (params[p] == null || params[p] === undefined || params[p] === 0) { params[p] = '' } }
    }

    async sendFile(url, params, options) {

        var opt = new defaultFileOptions()

        Object.assign(opt, options)

        if (params) {

            opt.body = params

        }

        return fetch(url, opt)
            .then(async res => {
                if (res.status === 413) { //Payload to large
                    throw new Error(ERROR413MESSAGE)
                }

                if (res.status === 204) return;

                if (res.status >= 500) {
                    return res.json()
                        .then(data => {
                            // let parsedData = JSON.parse(data);
                            throw new Error(data.errors)
                        });
                    // throw Error(res);
                }

                if (res.status == 401) { //Anauthorized
                    var loginUrl = baseUrl + '/Security/Login?SessionExpired=True';
                    try {
                        return res.json()
                            .then(data => {
                                if (data.gsfSessionExpiredMessage) {
                                    window.location.href = loginUrl;
                                    //return;
                                } else {
                                    //opt.errorCallback(data);
                                    throw Error(data);
                                    //return;
                                }
                            });
                    } catch (e) {
                        throw Error(res);
                    }
                }

                if (res.status == 404)
                    throw Error(res);

                const contentType = res.headers.get("content-type");
                if (!contentType) { return; }

                if (res.status == 400)
                    return res.json()
                        .then(data => {
                            if (contentType && contentType.indexOf("application/problem+json") !== -1) {
                                // if (opt.errorCallback()) {
                                //     opt.errorCallback(data);
                                // } else {
                                //     throw new Error(data.errors);
                                // }
                                opt.errorCallback(data);
                                //throw Error(res.statusText)
                            }
                            else {
                                // if (data.errors != undefined)
                                //     throw new Error(data.errors)
                                // else
                                //     throw new Error(res.text())

                                throw new Error(data.errors)
                            }
                        });

                return res.json()
                    .then(data => {
                        if (!res.ok) {
                            if (contentType && contentType.indexOf("application/problem+json") !== -1) {
                                // if (opt.errorCallback()) {
                                //     opt.errorCallback(data);
                                // } else {
                                //     throw new Error(data.errors);
                                // }
                                opt.errorCallback(data);
                                //throw Error(res.statusText)
                            }
                            else {
                                // if (data.errors != undefined)
                                //     throw new Error(data.errors)
                                // else
                                //     throw new Error(res.text())

                                throw new Error(data.errors)
                            }

                        }
                        else {
                            return data;
                        }
                    })
            })
    }

    async send(url, params, options) {

        var opt = new defaultOptions()

        Object.assign(opt, options)

        if (params) {

            var bodyParams = JSON.stringify(params);
            opt.body = bodyParams
            
        }

        return fetch(url, opt)
            .then(async res => {
                if (res.status === 204) return;

                if (res.status >= 500) {
                    return res.json()
                        .then(data => {
                            // let parsedData = JSON.parse(data);
                            throw new Error(data.errors)
                        });
                    // throw Error(res);
                }

                if (res.status == 401) { //Anauthorized
                    var loginUrl = baseUrl + '/Security/Login?SessionExpired=True';
                    try {
                        return res.json()
                            .then(data => {
                                if (data.gsfSessionExpiredMessage) {
                                    window.location.href = loginUrl;
                                    //return;
                                } else {
                                    //opt.errorCallback(data);
                                    throw Error(data);
                                    //return;
                                }
                            });
                    } catch (e) {
                        throw Error(res);
                    }
                }

                if (res.status == 404) //Not Found
                    throw Error(res);

                const contentType = res.headers.get("content-type");
                if (!contentType) { return; }

                return res.json()
                    .then(data => {
                        if (!res.ok) {
                            if (contentType && contentType.indexOf("application/problem+json") !== -1) {
                                // if (opt.errorCallback()) {
                                //     opt.errorCallback(data);
                                // } else {
                                //     throw new Error(data.errors);
                                // }
                                opt.errorCallback(data);
                                //throw Error(res.statusText)
                            }
                            else {
                                // if (data.errors != undefined)
                                //     throw new Error(data.errors)
                                // else
                                //     throw new Error(res.text())
                                
                                throw new Error(data.errors)
                            }

                        }
                        else {
                            return data;
                        }
                    })
            })
    }

    async fetchFile(url, params, options) {

        var opt = new defaultOptions()

        Object.assign(opt, options)

        if (params) {

            var bodyParams = JSON.stringify(params);
            opt.body = bodyParams

        }

        return fetch(url, opt)
            .then(async res => {
                if (res.status === 204) return;

                if (res.status >= 500) {
                    throw Error("No se encontró el archivo.");
                }

                if (res.status == 404)
                    throw Error(res);

                return res;
            })
    }

    //#endregion
}