import Vue from 'vue';
import Vuex from 'vuex';
import ajax from "@/common/ajaxWrapper";
import ErrorBag from "@/common/ErrorBag";
import SecurityService from "@/common/sercurityService"
import uiService from "@/common/uiService";
import loc from "@/common/commonLoc";
import CrudMode from "@/common/CrudMode";
import EmpresaDto from './EmpresaDto';

Vue.use(Vuex);

const RESET_STATE = 'RESET_STATE'
const LOADING = 'LOADING'

const SET_LIST = 'SET_LIST';
const SET_DTO = 'SET_DTO';
const RESET_DTO = 'RESET_DTO';
const SET_NEW_ID = 'SET_NEW_ID';
const SET_SEARCHED = 'SET_SEARCHED';
const SET_RECORDS_TOTAL = 'SET_RECORDS_TOTAL';

const GRANT_EMPRESA_NAME = "empresasportales" // Reemplazar por el nombre del permiso correspondiente. Esto es un ejemplo.
const GRANT_USUARIO_NAME = "usuariosempresas"

const API_URL = baseUrl + "/api/Proveedores/Administracion"; // Reemplazar por la URL de la api correspondiente. Esto es un ejemplo.
const API_ROLES_URL = baseUrl + "/api/Proveedores/RolesTipos";
const API_ALICUOTAS_URL = baseUrl + "/api/Alicuotas/ObtenerAlicuotas";
const API_DOCUMENTOS_COMPRAS_URL = baseUrl + "/api/proveedores/OrdenesComprasTipos";
const API_CONCEPTOS_GASTOS_URL = baseUrl + "/api/Configuration/ConceptosGastosTipos";
const API_MONEDAS_URL = baseUrl + "/api/Proveedores/Monedas";
const API_USUARIOS_EXTERNOS_URL = baseUrl + "/api/Proveedores/UsuariosExternos"

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
        async getRolesListAsync(ctx){
            var listaADevolver = []
            await new ajax()
                .get(API_ROLES_URL, { errorBag: ctx.getters.getErrorBag })
                .catch((ex) => {
                    console.log(`${ex.msg}: ${ex}.`);
                    showError(ex.msg);
                })
                .then((res) => {
                    var queryList
                    if (res.list.length > 0) {
                        queryList = res.list;
                        queryList = Object.assign([], queryList);
                        listaADevolver = queryList;
                    }
                })
            return listaADevolver;
        },
        async getAlicuotasListAsync(ctx){
            var listaADevolver = []
            await new ajax()
                .get(API_ALICUOTAS_URL, { errorBag: ctx.getters.getErrorBag })
                .catch((ex) => {
                    console.log(`${ex.msg}: ${ex}.`);
                    showError(ex.msg);
                })
                .then((res) => {
                    var queryList
                    if (res.list.length > 0) {
                        queryList = res.list;
                        queryList = Object.assign([], queryList);
                        listaADevolver = queryList;
                    }
                })
            return listaADevolver;
        },
        async getDocumentosComprasListAsync(ctx){
            var listaADevolver = []
            await new ajax()
                .get(`${API_DOCUMENTOS_COMPRAS_URL}/ListaTiposOrdenesCompras`, { errorBag: ctx.getters.getErrorBag })
                .catch((ex) => {
                    console.log(`${ex.msg}: ${ex}.`);
                    showError(ex.msg);
                })
                .then((res) => {
                    if (res.length > 0) {
                        listaADevolver = res;
                    }
                })
            return listaADevolver;
        },
        async getConceptosGastosListAsync(ctx){
            var listaADevolver = []
            await new ajax()
                .get(`${API_CONCEPTOS_GASTOS_URL}/ListaConceptos`, { errorBag: ctx.getters.getErrorBag })
                .catch((ex) => {
                    console.log(`${ex.msg}: ${ex}.`);
                    showError(ex.msg);
                })
                .then((res) => {
                    if (res.length > 0) {
                        listaADevolver = res;
                    }
                })
            return listaADevolver;
        },
        async getEmpresaRolesListAsync(ctx, id){
            var listaADevolver = []
            await new ajax()
                .get(`${API_ROLES_URL}/EmpresaRoles/${id}`, { errorBag: ctx.getters.getErrorBag })
                .catch((ex) => {
                    console.log(`${ex.msg}: ${ex}.`);
                    showError(ex.msg);
                })
                .then((res) => {
                    var queryList
                    if (res.list.length > 0) {
                        queryList = res.list;
                        queryList = Object.assign([], queryList);
                        listaADevolver = queryList;
                    }
                })
            return listaADevolver;
        },
        async getMonedasListAsync(ctx){
            var listaADevolver = []
            await new ajax()
                .get(API_MONEDAS_URL, { errorBag: ctx.getters.getErrorBag })
                .catch((ex) => {
                    console.log(`${ex.msg}: ${ex}.`);
                    showError(ex.msg);
                })
                .then((res) => {
                    var queryList
                    if (res.list.length > 0) {
                        queryList = res.list;
                        queryList = Object.assign([], queryList);
                        listaADevolver = queryList;
                    }
                })
            return listaADevolver;
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
        // Copia del dto en el store. Esto es por si en ciertos casos necesitamos guardar una copia de los datos persistidos en el store.
        // Reemplazar el nombre "templateDto" y la clase por los que correspondan.
        empresaDto: new EmpresaDto()
    },

    getters: {
        getDto: (state) => {
            return state.empresaDto;
        },
    },

    mutations: {
        [SET_DTO](state, dto) {
            state.empresaDto = dto;
        },
        [RESET_DTO](state) {
            state.empresaDto = new EmpresaDto();
        },
        [SET_NEW_ID](state, id) {
            state.empresaDto.id = id;
        }
    },
    actions: {
        async postAsync(ctx, empresaDto) {
            ctx.getters.getErrorBag.clear();
            return await new ajax()
                .post(
                    API_URL,
                    empresaDto,
                    {
                        errorBag: ctx.getters.getErrorBag,
                    })
                .finally(() => {
                });
        },
        async putAsync(ctx, empresaDto) {
            ctx.getters.getErrorBag.clear();
            return await new ajax()
                .put(
                    `${API_URL}/${empresaDto.id}`,
                    empresaDto,
                    {
                        errorBag: ctx.getters.getErrorBag,
                    })
                .finally(() => {
                });
        },
        async deleteAsync(ctx, empresaDto) {
            ctx.getters.getErrorBag.clear();
            return await new ajax()
                .delete(
                    `${API_URL}/${empresaDto.id}`,
                    { rowVersion: empresaDto.rowVersion },
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
        async getUsuarioExterno(ctx, email) {
            return await new ajax()
            .get(`${API_USUARIOS_EXTERNOS_URL}/${email}/Exists`)
            .then((res) => {
                // Agregar alguna lógica de ser necesario
                return res;
            })
            .finally(() => {
                // Agregar alguna lógica de ser necesario
            });
        },
        async añadirUsuarioExterno(ctx, empresaDto) {
            ctx.getters.getErrorBag.clear();
            return await new ajax()
                .post(
                    `${API_URL}/AñadirUsuarioExterno`,
                    empresaDto,
                    {
                        errorBag: ctx.getters.getErrorBag,
                    })
                .finally(() => {
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
        idTable: `__empresas_table`, // Reemplazar por un id más adecuado
        mode: new CrudMode(),
        errorBag: new ErrorBag(),
        grants: 
        { 
            createEmpresa: false, updateEmpresa: false, deleteEmpresa: false,
            createUsuario: false, updateUsuario: false, deleteUsuario: false,
        },
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
                `${GRANT_EMPRESA_NAME}.create`,
                `${GRANT_EMPRESA_NAME}.update`,
                `${GRANT_EMPRESA_NAME}.delete`,

                `${GRANT_USUARIO_NAME}.create`,
                `${GRANT_USUARIO_NAME}.update`,
                `${GRANT_USUARIO_NAME}.delete`
            ];

            await this.getters.getSecurityService
                .getGrantsByName(grantsName)
                .then((newValue) => {
                    this.getters.getGrants.createEmpresa = newValue[`${GRANT_EMPRESA_NAME}.create`];
                    this.getters.getGrants.updateEmpresa = newValue[`${GRANT_EMPRESA_NAME}.update`];
                    this.getters.getGrants.deleteEmpresa = newValue[`${GRANT_EMPRESA_NAME}.delete`];

                    this.getters.getGrants.createUsuario = newValue[`${GRANT_USUARIO_NAME}.create`];
                    this.getters.getGrants.updateUsuario = newValue[`${GRANT_USUARIO_NAME}.update`];
                    this.getters.getGrants.deleteUsuario = newValue[`${GRANT_USUARIO_NAME}.delete`];
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