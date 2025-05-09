<template>
    <div ref="top">
        <div class="col-12">
            <div class="card">
                <div class="card-body">
                    <div class="col-12 d-flex justify-content-between align-items-center mt-2 mb-2">
                        <p class="h5 m-0">Documentos de Compras</p> <!-- Modificar según el caso -->
                        <div v-if="modoDetalle">
                            <button :disabled="!grants.createUsuario" type="button" class="btn btn-outline-primary btn-sm" @click="modificarModo">
                                <b>Modificar</b>
                            </button>
                        </div>
                    </div>
                    <div class="col-12 table-responsive">
                        <table class="mt-4 table table-bordered table-striped table-hover table-white fix-head" 
                        convert-to-datatable-manual no-paging :id="`detalleDocumentosCompras`">
                            <thead class="table-top">
                                <tr class="text-center">
                                    <th class="w-30" scope="col">Documento de compra</th>
                                    <th scope="col">Descripcion</th>
                                    <th class="w-15" scope="col">Utilización</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(i, index) in listaDocumentosCompras">
                                    <td class="text-center align-middle">
                                        <label class="form-check-label d-flex justify-content-center" :for="'flexCheckDefault' + index">
                                            {{ i.nombre }}
                                        </label>
                                    </td>
                                    <td class="text-center align-middle">
                                        <label class="form-check-label d-flex justify-content-center" :for="'flexCheckDefault' + index">
                                            {{ i.descripcion ? i.descripcion : ' - '}}
                                        </label>
                                    </td>
                                    <td class="text-center align-middle">
                                        <div class="d-flex justify-content-center">
                                            <input class="form-check-input" type="checkbox" v-model="listaEmpresaDocumentosCompras"
                                            :value="i" @input="addRemoveModo(i)" :id="'flexCheckDefault' + index" :disabled="modoDetalle" >
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div v-if="!modoDetalle" class="col-12 d-flex justify-content-end mb-3 mt-3">
                        <div>
                        <accept-button :disabled="!grants.createUsuario" @click="save">Aceptar</accept-button>
                        <cancel-button @click="cancel">Cancelar</cancel-button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12 d-flex justify-content-end mb-3 mt-3">
                <cancel-button @click="goBack">Volver</cancel-button>
            </div>
        </div>
    </div>
</template>

<script>
import AcceptButton from "@/components/forms/accept-button.vue";
import CancelButton from "@/components/forms/cancel-button.vue";
import UiService from "@/common/uiService";
import commonMixin from '@/Common/Mixins/commonMixin';
import multiselect from "vue-multiselect";
import inlineEdit from "@/components/forms/inline-edit-button.vue";
import inlineDelete from "@/components/forms/inline-delete-button.vue";
import inlineCancel from "@/components/forms/inline-cancel-button.vue";
import ajax from "@/common/ajaxWrapper";

const API_URL = baseUrl + "/api/Proveedores/Administracion";
const API_DOCUMENTOS_COMPRAS_URL = baseUrl + "/api/proveedores/OrdenesComprasTipos";
const NO_DATA_MESSAGE = "No hay datos";

export default {
    components: {
        AcceptButton,
        CancelButton,
        multiselect,
        inlineEdit,
        inlineDelete,
        inlineCancel,
    },
    mixins: [commonMixin],
    name: "documentos-compras-form",

    data: function () {
        return {
            uiService: new UiService(),
            modoDetalle: true,
            listaEmpresaDocumentosCompras: [],
            listaDocumentosCompras: [],
        }
    },
    computed: {
        grants() {
            return this.$store.getters.getGrants;
        },
        errorBag() {
            return this.$store.getters.getErrorBag;
        },
    },
    async mounted() {
        await this.init()
    },
    methods: {
        modificarModo(){
            this.modoDetalle = !this.modoDetalle
        },
        async init(){
            await this.getEmpresaDocumentosComprasAsync();
            await this.getDocumentosComprasAsync();
        },
        async getEmpresaDocumentosComprasAsync() {            
            this.uiService.showSpinner(true);
            return await new ajax()
                .get(`${API_URL}/ObtenerOrdenesComprasTipos`, {empresaPortalId: this.$route.params.id}, { errorBag: this.errorBag })
                .catch((ex) => {
                    console.log(`${ex.msg}: ${ex}.`);
                    showError(ex.msg);
                })
                .then((res) => {
                    this.listaEmpresaDocumentosCompras = res.map(em => em.ordenCompraTipo);
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        async getDocumentosComprasAsync() {            
            this.uiService.showSpinner(true);
            return await new ajax()
                .get(`${API_DOCUMENTOS_COMPRAS_URL}/ListaTiposOrdenesCompras`, { errorBag: this.errorBag })
                .catch((ex) => {
                    console.log(`${ex.msg}: ${ex}.`);
                    showError(ex.msg);
                })
                .then((res) => {
                    this.listaDocumentosCompras = res;
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        addRemoveModo(ordenCompraTipo) {
            const index = this.listaEmpresaDocumentosCompras.findIndex(m => m.id === ordenCompraTipo.id);

            if (index >= 0) {
                this.listaEmpresaDocumentosCompras.splice(index, 1);
            } else {
                this.listaEmpresaDocumentosCompras.push(ordenCompraTipo);
            }
        },
        goBack() {
            this.errorBag.clear();
            if(this.$route.params.create)
                this.$router.push({ name: "home", query: { fromDetail: false } });
            else
                this.$router.push({ name: "home", query: { fromDetail: true } });
        },
        async cancel(){
            this.modificarModo()
            await this.init()
        },
        async save(){
            if(this.validacionListaEmpresas()){
                this.uiService.showSpinner(true);
                return await new ajax()
                    .put(`${API_URL}/ModificarOrdenesComprasTipos`,
                    {
                        empresaPortalId: this.$route.params.id,
                        ordenesComprasTipos: this.listaEmpresaDocumentosCompras
                    },
                    { errorBag: this.errorBag })
                    .catch((ex) => {
                        console.log(`${ex.msg}: ${ex}.`);
                        showError(ex.msg);
                    })
                    .then(async () => {
                        this.modificarModo()
                        await this.init()
                    })
                    .finally(() => {
                        this.uiService.showMessageSuccess("Operación confirmada")
                        this.uiService.showSpinner(false);
                    });
            }
            this.uiService.showSpinner(false); 
        },
        validacionListaEmpresas(){
            if(this.listaEmpresaDocumentosCompras.length === 0){
                this.uiService.showMessageError("Operación rechazada: Debe haber minimo un Documento de compra")
                //this.errorBag.addError("rolesIdm", ["Se debe agregar un rol como minimo"]);
                return false;
            }
            return true;
        }
    },
    watch: {
    }
}
</script>