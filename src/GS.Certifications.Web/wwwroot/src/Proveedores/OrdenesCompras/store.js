import Vue from 'vue';
import Vuex from 'vuex';
import ajax from "@/common/ajaxWrapper";
import ErrorBag from "@/common/ErrorBag";
import SecurityService from "@/common/sercurityService"
import uiService from "@/common/uiService";
import loc from "@/common/commonLoc";
import CrudMode from "@/common/CrudMode";
import OrdenCompraDto from './OrdenCompraDto';

Vue.use(Vuex);

const RESET_STATE = 'RESET_STATE'
const LOADING = 'LOADING'

const SET_LIST = 'SET_LIST';
const SET_DTO = 'SET_DTO';
const RESET_DTO = 'RESET_DTO';
const SET_NEW_ID = 'SET_NEW_ID';
const SET_SEARCHED = 'SET_SEARCHED';
const SET_RECORDS_TOTAL = 'SET_RECORDS_TOTAL';

const GRANT_NAME = "ordenescompras" // Reemplazar por el nombre del permiso correspondiente. Esto es un ejemplo.

const API_URL = baseUrl + "/api/Proveedores/OrdenesCompras"; // Reemplazar por la URL de la api correspondiente. Esto es un ejemplo.



const listModule = {
    state: {
        list: [],
        recordsTotal: 0,
        searched: false,
    },

    getters: {
        getList: (state) => {
            return state.list;
        },
        getSearched: (state) => {
            return state.searched;
        },
        getRecordsTotal: (state) => {
            return state.recordsTotal;
        },
    },

    mutations: {
        [SET_LIST](state, entitiesList) {
            state.list = entitiesList;
        },
        [SET_RECORDS_TOTAL](state, recordsTotal) {
            state.recordsTotal = recordsTotal;
        },
        [SET_SEARCHED](state, searched) {
            state.searched = searched;
        }
    },

    actions: {
        clearList(ctx) {
            ctx.commit(SET_RECORDS_TOTAL, 0);
            ctx.commit(SET_LIST, []);
            ctx.dispatch("destroyTable");
        },
        setList(ctx, list) {
            ctx.commit(SET_LIST, list)
        },
        async getListAsync(ctx, parameters) {
            ctx.getters.getErrorBag.clear();
            return await new ajax()
                .get(API_URL, parameters, { errorBag: ctx.getters.getErrorBag })
                .then((res) => {
                    if (res.list.length > 0) {
                        ctx.commit(SET_RECORDS_TOTAL, res.recordsTotal)
                        ctx.commit(SET_LIST, res.list);
                    } else {
                        ctx.commit(SET_RECORDS_TOTAL, 0)
                        ctx.commit(SET_LIST, [])
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
                    ctx.commit(SET_SEARCHED, true);
                });
        },
        setSearched(ctx, searched) {
            ctx.commit(SET_SEARCHED, searched);
        },
        destroyTable(ctx) {
            ctx.getters.getUiService.onlyDestroyDataTablesManual(ctx.getters.getIdTable);
        },
    }
}

const formModule = {
    state: {
        ordenCompraDto: new OrdenCompraDto()
    },

    getters: {
        getDto: (state) => {
            return state.ordenCompraDto;
        },
    },

    mutations: {
        [SET_DTO](state, dto) {
            state.ordenCompraDto = dto;
        },
        [RESET_DTO](state) {
            state.ordenCompraDto = new OrdenCompraDto();
        },
        [SET_NEW_ID](state, id) {
            state.ordenCompraDto.id = id;
        }
    },
    actions: {
        async postAsync(ctx, ordenCompraDto) {
            ctx.getters.getErrorBag.clear();
            return await new ajax()
                .post(
                    API_URL,
                    ordenCompraDto,
                    {
                        errorBag: ctx.getters.getErrorBag,
                    })
                .finally(() => {
                });
        },
        async putAsync(ctx, ordenCompraDto) {
            ctx.getters.getErrorBag.clear();
            return await new ajax()
                .put(
                    API_URL,
                    ordenCompraDto,
                    {
                        errorBag: ctx.getters.getErrorBag,
                    })
                .finally(() => {
                });
        },
        async deleteAsync(ctx, ordenCompraDto) {
            ctx.getters.getErrorBag.clear();
            return await new ajax()
                .delete(
                    `${API_URL}/${ordenCompraDto.id}`,
                    { rowVersion: ordenCompraDto.rowVersion },
                    {
                        errorBag: ctx.getters.getErrorBag,
                    })
                .finally(() => {
                });
        },
        async getAsync(ctx, id) {
            return await new ajax()
                .get(`${API_URL}/${id}`)
                .then((res) => {
                    // Agregar alguna lógica de ser necesario
                    return res;
                })
                .finally(() => {
                    // Agregar alguna lógica de ser necesario
                });
        },
        resetDto(ctx) {
            ctx.commit(RESET_DTO);
        }
    }
}

export default new Vuex.Store({
    state: {
        loading: false,
        idTable: `__OrdenCompra_table`, // Reemplazar por un id más adecuado
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
        [RESET_STATE](state) {
            state.textValue = 'State reseted'
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
        resetState(ctx) {
            ctx.commit(RESET_STATE)
        }
    },
    modules: {
        listModule,
        formModule,
    }
})