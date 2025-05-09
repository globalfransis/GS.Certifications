<script>
    // import { StockRangeItem, StockRangeTree, Transport } from "./model-object-classes.vue";
    import { StockRangeTree, Transport } from "./model-object-classes.vue";
    const folderEmoji = "📒";
    const errorMessageFromAPICall = window.APICallErrorText;

    /**
     * Gets the StockRange list and returns it a as array of nodes (StockRangeTree).
     * @return {StockRangeTree[]}
     */
    export const getStockRangesTreeByAPI = async () => {
        const GetStockRangesByOrganizationUrl = baseUrl + '/api/stockranges/GetStockRangesByOrganization';
        var stockRanges = [];

        await fetch(GetStockRangesByOrganizationUrl)
            .then((response) => response.json())
            .then((res) => {
                //var t = res.forEach(element => {
                //    if (element.parentStockRangeId == null)

                //})
                //console.log(res);
                res.forEach(r => {
                    if (r.parentStockRangeId == null)
                        stockRanges.push(new StockRangeTree(r.id, folderEmoji + ' ' + r.code + ' - ' + r.name, r.code, r.name, r.parentStockRangeId));
                });
                stockRanges.forEach(element => {
                    element.children = createStockRangesChildren(element, res);
                })
            })
            .catch((error) => {
                //console.log(error)
                alert(errorMessageFromAPICall)
            });
        return stockRanges;
    };

    /**
     * Creates recursively the children nodes for a StockRangeTree.
     * @param { StockRangeTree } element A StockRangeTree with no children setted on wich the childrens will be inserted recursively.
     * @param { [] } source Source of data, should be the response of an API call.
     * @return {StockRangeTree[]}
     */
    function createStockRangesChildren(element, source) {
        source.forEach(sr => {
            if (element.id == sr.parentStockRangeId)
                element.children.push(new StockRangeTree(sr.id, folderEmoji + ' ' + sr.code + ' - ' + sr.name, sr.code, sr.name, sr.parentStockRangeId));
        });
        element.children.forEach(child => {
            if (child.children != null)
                child.children = createStockRangesChildren(child, source);
        });

        if (element.children.length == 0)
            delete element.children;

        return element.children;
    };

    /**
     * Search for a particular node in an array of StockRangeTree by its id.
     * @param { StockRangeTree[] } tree Array of StockRangeTree to be searched.
     * @param { int } value Id of the StockRangeTree to seach.
     * @return {StockRangeTree}
     */
    export const searchCodeInStockRangeTree = (tree, value) => {
        //tree should be a StockRangeTree object.
        var result = null;

        for (var i = 0; i < tree.length && result == null; i++) {
            if (tree[i].id == value)
                result = tree[i];
            else if (tree[i].children != null)
                result = searchCodeInStockRangeTree(tree[i].children, value)
        }
        return result;
    };

    /**
     * Gets the Transport list and returns it a as array Transport.
     * @return {Transport[]}
     */
    export const getTransportsByAPI = async () => {
        const getTransportsByOrganizationUrl = baseUrl + '/api/transports/GetTransportsByOrganization';
        var queryResult;

        await fetch(getTransportsByOrganizationUrl)
            .then((response) => response.json())
            .then((res) => {
                var queryList = [];
                for (var i = 0; i < res.length; i++) {
                    var t = res[i];
                    queryList.push(new Transport(t.id, t.name, t.transportTypeName, t.transportModeId, t.allowsCargos, t.allowsPassengers, t.allowsStaterooms, t.allowsVehicles, t.singleUnit));
                }
                // if (transportModeId != null) {
                //     // queryResult = queryList.filter(function (itm) {
                //     //     return !selectedValues.some(f => {
                //     //         return f.stockRangeId == itm.id;
                //     //     })
                //     // });
                //     queryResult = queryList.filter(function (itm) {
                //         return itm.transportModeId == transportModeId;
                //     });
                // } else {
                //     queryResult = queryList;
                // }
                queryResult = queryList;
            })
            .catch((error) => {
                alert(errorMessageFromAPICall);
            });
        return queryResult;
    };

    /**
     * Returns an empty Transport.
     * @param { string } UnspecifiedOptionsText Value for the transportName.
     * @return {Transport}
     */
    export function createEmpyTransport(UnspecifiedOptionsText) {
        return new Transport(0, UnspecifiedOptionsText);
    };

    export const EmptyTimeHelper = "00:00";

    export const productTypes = {
        PASSENGER: 1,
        VEHICLE: 2,
        CARGO: 3,
        STATEROOM: 4,
        HOTEL: 5,
        TRANSPORT: 6,
        SERVICE: 7,
        TOUR: 8,
        TICKET: 9,
        DISCOUNT: 10,
    };

    /**
     * Returns a List of ProductTypesId supported by the properties of a Transport.
     * @param { Transport } transport
     */
    export function getProductTypesIdsByTransport(transport) {
        //Cambiar la logica, se deberian recuperar todos por llamada Ajax y sacar los que no tenga permitido.
        var productTypesId = [];
        if (transport.allowsPassengers)
            productTypesId.push(productTypes.PASSENGER);
        if (transport.allowsVehicles)
            productTypesId.push(productTypes.VEHICLE);
        if (transport.allowsCargos)
            productTypesId.push(productTypes.CARGO);
        if (transport.allowsStaterooms)
            productTypesId.push(productTypes.STATEROOM);

        productTypesId.push(productTypes.HOTEL);
        productTypesId.push(productTypes.TRANSPORT);
        productTypesId.push(productTypes.SERVICE);
        productTypesId.push(productTypes.TOUR);
        productTypesId.push(productTypes.TICKET);
        productTypesId.push(productTypes.DISCOUNT);
        
        return productTypesId;
    };

    export const CrudModes = {
        CREATE: 'Create',
        UPDATE: 'Update',
        DETAIL: 'Detail',
        DELETE: 'Delete',
    };

</script>