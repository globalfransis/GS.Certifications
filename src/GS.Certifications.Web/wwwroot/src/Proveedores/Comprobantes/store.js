import Vue from 'vue';
import Vuex from 'vuex';
import ajax from "@/common/ajaxWrapper";
import ErrorBag from "@/common/ErrorBag";
import SecurityService from "@/common/sercurityService"
import uiService from "@/common/uiService";
import loc from "@/common/commonLoc";
import CrudMode from "@/common/CrudMode";
import Comprobante from './Comprobante';

Vue.use(Vuex);

const RESET_STATE = 'RESET_STATE'
const LOADING = 'LOADING'

const SET_LIST = 'SET_LIST';
const SET_DTO = 'SET_DTO';
const RESET_DTO = 'RESET_DTO';
const SET_NEW_ID = 'SET_NEW_ID';
const SET_SEARCHED = 'SET_SEARCHED';
const SET_RECORDS_TOTAL = 'SET_RECORDS_TOTAL';

const GRANT_NAME = "comprobantes" // Reemplazar por el nombre del permiso correspondiente. Esto es un ejemplo.

const API_URL = baseUrl + "/api/Proveedores/Comprobantes";

const API_EMPRESAS_URL = baseUrl + "/api/Proveedores/Administracion";

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
        async saveAsync(ctx, comprobante) {
            ctx.getters.getErrorBag.clear();
            return await new ajax()
                .post(
                    API_URL + "/Borrador",
                    comprobante,
                    {
                        errorBag: ctx.getters.getErrorBag,
                    })
                .finally(() => {
                });
        },
        async updateDraftAsync(ctx, comprobante) {
            ctx.getters.getErrorBag.clear();
            return await new ajax()
                .put(
                    `${API_URL}/Borrador/${comprobante.id}`,
                    comprobante,
                    {
                        errorBag: ctx.getters.getErrorBag,
                    })
                .finally(() => {
                });
        },
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
                    if (ctx.getters.getErrorBag.hasErrors()) {
                        ctx.commit(SET_RECORDS_TOTAL, 0);
                        ctx.commit(SET_LIST, []);
                        ctx.dispatch("destroyTable");
                        return;
                    }
                    if (res.list.length > 0) {
                        ctx.commit(SET_RECORDS_TOTAL, res.recordsTotal);
                        ctx.commit(SET_LIST, res.list);
                    } else {
                        ctx.commit(SET_RECORDS_TOTAL, 0);
                        ctx.commit(SET_LIST, []);
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
                    if (!ctx.getters.getErrorBag.hasErrors())
                        ctx.commit(SET_SEARCHED, true);
                    else
                        ctx.commit(SET_SEARCHED, false);
                });
        },
        async getDatosProveedorAsync(ctx, id) {
            return await new ajax()
                .get(`${API_EMPRESAS_URL}/${id}`)
                .then((res) => {
                    // Agregar alguna lógica de ser necesario
                    return res;
                })
                .finally(() => {
                    // Agregar alguna lógica de ser necesario
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
        comprobante: new Comprobante()
    },

    getters: {
        getDto: (state) => {
            return state.comprobante;
        },
    },

    mutations: {
        [SET_DTO](state, dto) {
            state.comprobante = dto;
        },
        [RESET_DTO](state) {
            state.comprobante = new Comprobante();
        },
        [SET_NEW_ID](state, id) {
            state.comprobante.id = id;
        }
    },
    actions: {
        setComprobante(ctx, comprobante) {
            ctx.commit(SET_DTO, comprobante);
        },
        async postAsync(ctx, comprobante) {
            ctx.getters.getErrorBag.clear();
            return await new ajax()
                .post(
                    API_URL,
                    comprobante,
                    {
                        errorBag: ctx.getters.getErrorBag,
                    })
                .finally(() => {
                });
        },
        async rejectAsync(ctx, comprobante) {
            ctx.getters.getErrorBag.clear();
            return await new ajax()
                .put(
                    `${API_URL}/${comprobante.id}/Rechazar`,
                    comprobante,
                    {
                        errorBag: ctx.getters.getErrorBag,
                    })
                .finally(() => {
                });
        },
        async approveAsync(ctx, comprobante) {
            ctx.getters.getErrorBag.clear();
            return await new ajax()
                .put(
                    `${API_URL}/${comprobante.id}/Aprobar`,
                    comprobante,
                    {
                        errorBag: ctx.getters.getErrorBag,
                    })
                .finally(() => {
                });
        },
        async updatePeriodoAsync(ctx, comprobante) {
            ctx.getters.getErrorBag.clear();
            return await new ajax()
                .put(
                    `${API_URL}/${comprobante.id}/Periodos`,
                    comprobante,
                    {
                        errorBag: ctx.getters.getErrorBag,
                    })
                .finally(() => {
                });
        },
        async confirmAsync(ctx, comprobante) {
            ctx.getters.getErrorBag.clear();
            return await new ajax()
                .put(
                    `${API_URL}/${comprobante.id}/Confirmar`,
                    comprobante,
                    {
                        errorBag: ctx.getters.getErrorBag,
                    })
                .finally(() => {
                });
        },
        async authorizeAsync(ctx, comprobante) {
            ctx.getters.getErrorBag.clear();
            return await new ajax()
                .put(
                    `${API_URL}/${comprobante.id}/Autorizar`,
                    comprobante,
                    {
                        errorBag: ctx.getters.getErrorBag,
                    })
                .finally(() => {
                });
        },
        async verifyAsync(ctx, comprobante) {
            ctx.getters.getErrorBag.clear();
            return await new ajax()
                .put(
                    `${API_URL}/${comprobante.id}/Constatar`,
                    comprobante,
                    {
                        errorBag: ctx.getters.getErrorBag,
                    })
                .finally(() => {
                });
        },
        async putAsync(ctx, comprobante) {
            ctx.getters.getErrorBag.clear();
            return await new ajax()
                .put(
                    `${API_URL}/${comprobante.id}`,
                    comprobante,
                    {
                        errorBag: ctx.getters.getErrorBag,
                    })
                .finally(() => {
                });
        },
        async deleteAsync(ctx, comprobante) {
            ctx.getters.getErrorBag.clear();
            return await new ajax()
                .delete(
                    `${API_URL}/${comprobante.id}?RowVersion=${comprobante.rowVersion}`,
                    { rowVersion: comprobante.rowVersion },
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
        idTable: `__Comprobantes_table`,
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