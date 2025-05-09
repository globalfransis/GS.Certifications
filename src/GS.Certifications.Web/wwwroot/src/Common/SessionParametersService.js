
export default class SessionParametersService {
    static get() {

        let dir = getUrl();
        let data =  getDataFromLocalStorage(dir);

        if(!data)
            return null;

        return Object.assign({}, data);
    }

    static set(parameters) {

        let dir = getUrl();

        let data = parameters;

        saveDataInLocalStorage(dir, data)
    }
}

