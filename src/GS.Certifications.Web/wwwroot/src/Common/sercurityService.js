import ajax from '@/common/ajaxWrapper';

export default class SecurityService {


    constructor() {
    }

    async getGrantsByName(values) {
        var getGrantsByNameUrl = baseUrl + "/api/grants/GrantsExists";
        var apiUrl = getGrantsByNameUrl;

        var i = 0;
        while (i < values.length) {
            if (i == 0)
                apiUrl += "?grants=" + values[i];
            else
                apiUrl += "&grants=" + values[i];

            i++;
        }

        return new ajax().get(apiUrl, null)

        //return await new ajax().send(apiUrl, undefined, { method: 'GET' });
        // .then(response => {
        //     result = response;
        // })
        // .catch(error => { throw error });
    }

    async getActualUser(){
        new ajax()
      .get(baseUrl + "",{})
      .then((res) => {

      })
      .catch((ex) => { });
    }

}