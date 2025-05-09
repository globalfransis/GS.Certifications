import Vue from 'vue';
import Vuex from 'vuex';
import ajax from "@/common/ajaxWrapper";
import ErrorBag from "@/common/ErrorBag";
import SecurityService from "@/common/sercurityService"
import uiService from "@/common/uiService";
import loc from "@/common/commonLoc";
import CrudMode from "@/common/CrudMode";

Vue.use(Vuex);

const newValueET_STATE = 'newValueET_STATE'
const LOADING = 'LOADING'

const SET_PARAMETROS_LIST = 'SET_PARAMETROS_LIST';
const SET_PARAMETRO = 'SET_PARAMETRO';
const RESET_PARAMETRO = 'RESET_PARAMETRO';
const SET_HAS_BEEN_SERCHED = 'SET_HAS_BEEN_SERCHED';
const SET_RECORDS_TOTAL = 'SET_RECORDS_TOTAL';


const GET_PARAMETROS_URL = `${baseUrl}/api/Parametros/GetParametros`;
const GET_PARAMETRO_URL = `${baseUrl}/api/Parametros/GetParametro`;
const CHANGE_STATUS_URL = `${baseUrl}/api/Suscripciones/Suscripcion/ChangeStatus`;
const POST_URL = `${baseUrl}/api/Parametros/CreateParametro`;
const UPDATE_URL = `${baseUrl}/api/Parametros/UpdateParametro`;


const listModule = {
    state: {
        parametrosLista: [],
        recordsTotal: 0,
        hasBeenSearched: false,
    },

    getters: {
        getParametrosLista: (state) => {
            return state.parametrosLista;
        },
        getHasBeenSearched: (state) => {
            return state.hasBeenSearched;
        },
        getRecordsTotal: (state) => {
            return state.recordsTotal;
        },
    },

    mutations: {
        [SET_PARAMETROS_LIST](state, entitiesList) {
            state.parametrosLista = entitiesList;
        },
        [SET_HAS_BEEN_SERCHED](state, hasBeenSearched) {
            state.hasBeenSearched = hasBeenSearched;
        },
        [SET_RECORDS_TOTAL](state, recordsTotal) {
            state.recordsTotal = recordsTotal;
        },
    },

    actions: {
        setSearchFilterParams(ctx, newSearchFilterParams) {
            ctx.commit(SET_SEARCH_FILTER_PARAMS, newSearchFilterParams)
        },
        async changeStatus(ctx) {
            return await new ajax()
                .put(CHANGE_STATUS_URL)
                .then((newValue) => {
                    console.log(newValue)
                    this.getters.getUiService.onlyDestroyDataTablesManual(this.getters.getIdTable);
                })
                .then(() => {
                    this.getters.getUiService.transformToDataTablesManual(this.getters.getIdTable);
                })
                .catch((ex) => {
                    throw new Error(loc['Error al buscar: '] + ex.message);
                })
                .finally(() => {
                    ctx.commit(SET_HAS_BEEN_SERCHED, true)
                    ctx.commit(LOADING, false)
                    this.getters.getUiService.showSpinner(false)
                });
        },
        setList(ctx, list) {
            ctx.commit(SET_PARAMETROS_LIST, list)
        },
        async loadEntitiesList(ctx, parameters) {
            ctx.getters.getErrorBag.clear();
            ctx.getters.getUiService.showSpinner(true)
            return await new ajax()
                .get(GET_PARAMETROS_URL, parameters,{errorBag: ctx.getters.getErrorBag,})
                .then((res) => {
                    if(res.list.length > 0) {
                        ctx.commit(SET_RECORDS_TOTAL, res.recordsTotal)
                        ctx.commit(SET_PARAMETROS_LIST, res.list);
                    } else {
                        ctx.commit(SET_RECORDS_TOTAL, 0)
                        ctx.commit(SET_PARAMETROS_LIST, [])
                    }
                    ctx.dispatch("destroyTable");
                })
                .then(() => {
                    ctx.getters.getUiService.transformToDataTablesManual(ctx.getters.getIdTable);
                })
                .catch((ex) => {
                    throw new Error(loc['Error al buscar: '] + ex.message);
                })
                .finally(() => {
                    ctx.commit(SET_HAS_BEEN_SERCHED, true)
                    ctx.commit(LOADING, false)
                    ctx.getters.getUiService.showSpinner(false)
                });
        },
        destroyTable(ctx) {
            ctx.getters.getUiService.onlyDestroyDataTablesManual(ctx.getters.getIdTable);
        },
    }
}

const formModule = {
    state: {
        nuevoParametro: {
            id: 0,
            clave: "",
            valor: "",
            descripcion: "",
        },
    },

    getters:{
        getParametro: (state) => {
            return state.nuevoParametro;
        },
    },

    mutations: {
        [SET_PARAMETRO](state, newValue) {
            state.nuevoParametro.id = newValue.id;
            state.nuevoParametro.clave= newValue.clave;
            state.nuevoParametro.valor = newValue.valor;
            state.nuevoParametro.descripcion = newValue.descripcion;
        },

        [RESET_PARAMETRO](state) {
            state.nuevoParametro.id = 0;
            state.nuevoParametro.clave = "";
            state.nuevoParametro.valor = "";
            state.nuevoParametro.descripcion = "";
        },
    },

    actions: {
        async saveParametro(ctx){
            ctx.getters.getErrorBag.clear();
            ctx.getters.getUiService.showSpinner(true);
            return await new ajax()
              .post(
                POST_URL,
                ctx.state.nuevoParametro,
                {
                  errorBag: ctx.getters.getErrorBag,
                })
            .finally(() => {
                ctx.getters.getUiService.showSpinner(false)
            });
        },

        async updateParametro(ctx){
            ctx.getters.getErrorBag.clear();
            ctx.getters.getUiService.showSpinner(true);
            return await new ajax()
                .put(
                    UPDATE_URL,
                    ctx.state.nuevoParametro,
                    {
                     errorBag: ctx.getters.getErrorBag,
                    })
                .finally(() => {
                    ctx.getters.getUiService.showSpinner(false)
            });
        },

        async getParametro(ctx, id){
            ctx.getters.getUiService.showSpinner(true);
            return await new ajax()
                .get(GET_PARAMETRO_URL, { Id: id})
                .then((res) => {
                    ctx.commit("SET_PARAMETRO", res)
                })
                .finally(() => {
                    ctx.getters.getUiService.showSpinner(false)
            });
        },


        resetParametro(ctx){
            ctx.commit("RESET_PARAMETRO")
        }
    }
}

export default new Vuex.Store({
    state: {
        loading: false,
        idTable: `__Parametros_table`,
        mode: new CrudMode(),
        errorBag: new ErrorBag(),
        grants: { create: false, update: false, delete: false },
        securityService: new SecurityService(),
        uiService: new uiService()
    },

    getters: {
        loading: (state) => {
            return state.loading
        },
        getMode: (state) => {
            return state.mode;
        },
        getErrorBag: (state) => {
            return state.errorBag;
        },
        getGrants: (state) => {
            return state.grants
        },
        getSecurityService: (state) => {
            return state.securityService
        },
        getIdTable: (state) => {
            return state.idTable;
        },
        getUiService: (state) => {
            return state.uiService
        }
    },

    mutations: {
        [LOADING](state, value) {
            state.loading = value
        },
        [newValueET_STATE](state) {
            state.textValue = 'State newValueeted'
        },
    },

    actions: {

        async loadGrants() {
            var grantsName = [
                `${GRANT_NAME}.create`,
                `${GRANT_NAME}.update`,
                `${GRANT_NAME}.delete`
            ];

            await this.getters.getSecurityService
                .getGrantsByName(grantsName)
                .then((newValue) => {
                    this.getters.getGrants.create = newValue[`${GRANT_NAME}.create`];
                    this.getters.getGrants.update = newValue[`${GRANT_NAME}.update`];
                    this.getters.getGrants.delete = newValue[`${GRANT_NAME}.delete`];
                })
                .catch((ex) => {
                    throw new Error(loc['Error recuperando permisos: '] + ex.message);
                });
        },

        newValueetState(ctx) {
            ctx.commit(newValueET_STATE)
        }
    },

    modules: {
        listModule,
        formModule,
    }
})