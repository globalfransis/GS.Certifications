
export default class Parameters {
    constructor() { }

    getSearchParameters() {

        let dir = getUrl();
        let data =  getDataFromLocalStorage(dir);

        if(!data)
            return null;

        return Object.assign({}, data);
    }

    saveSearchParameters(parameters) {

        let dir = getUrl();

        let data = parameters;

        saveDataInLocalStorage(dir, data)
    }
}

