<template>
    <div ref="top">
        <div class="col-12">
            <div class="card">
                <div class="card-body">
                    <div class="col-12 d-flex justify-content-between align-items-center mt-2 mb-2">
                        <p class="h5 m-0">Conceptos de Gastos</p> <!-- Modificar según el caso -->
                        <div v-if="modoDetalle">
                            <button :disabled="!grants.createUsuario" type="button" class="btn btn-outline-primary btn-sm" @click="modificarModo">
                                <b>Modificar</b>
                            </button>
                        </div>
                    </div>
                    <div class="col-12 table-responsive">
                        <table class="mt-4 table table-bordered table-striped table-hover table-white fix-head" 
                        convert-to-datatable-manual no-paging :id="`detalleConceptosGastosTipos`">
                            <thead class="table-top">
                                <tr class="text-center">
                                    <th class="w-30" scope="col">Concepto de Gasto</th>
                                    <th scope="col">Descripcion</th>
                                    <th class="w-15" scope="col">Utilización</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(i, index) in listaConceptosGastosTipos">
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
                                            <input class="form-check-input" type="checkbox" v-model="listaEmpresaConceptosGastosTipos"
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
const API_CONCEPTOS_GASTOS_URL = baseUrl + "/api/Configuration/ConceptosGastosTipos";
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
    name: "conceptos-gastos-form",

    data: function () {
        return {
            uiService: new UiService(),
            modoDetalle: true,
            listaEmpresaConceptosGastosTipos: [],
            listaConceptosGastosTipos: [],
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
            await this.getEmpresaConceptosGastosTiposAsync();
            await this.getConceptosGastosTiposAsync();
        },
        async getEmpresaConceptosGastosTiposAsync() {            
            this.uiService.showSpinner(true);
            return await new ajax()
                .get(`${API_URL}/ObtenerConceptosGastosTipos`, {empresaPortalId: this.$route.params.id}, { errorBag: this.errorBag })
                .catch((ex) => {
                    console.log(`${ex.msg}: ${ex}.`);
                    showError(ex.msg);
                })
                .then((res) => {
                    this.listaEmpresaConceptosGastosTipos = res.map(em => em.conceptoGastoTipo);
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        async getConceptosGastosTiposAsync() {            
            this.uiService.showSpinner(true);
            return await new ajax()
                .get(`${API_CONCEPTOS_GASTOS_URL}/ListaConceptos`, { errorBag: this.errorBag })
                .catch((ex) => {
                    console.log(`${ex.msg}: ${ex}.`);
                    showError(ex.msg);
                })
                .then((res) => {
                    this.listaConceptosGastosTipos = res;
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        addRemoveModo(conceptoGastoTipo) {
            const index = this.listaEmpresaConceptosGastosTipos.findIndex(m => m.id === conceptoGastoTipo.id);

            if (index >= 0) {
                this.listaEmpresaConceptosGastosTipos.splice(index, 1);
            } else {
                this.listaEmpresaConceptosGastosTipos.push(conceptoGastoTipo);
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
                    .put(`${API_URL}/ModificarConceptosGastosTipos`,
                    {
                        empresaPortalId: this.$route.params.id,
                        conceptosGastosTipos: this.listaEmpresaConceptosGastosTipos
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
            if(this.listaEmpresaConceptosGastosTipos.length === 0){
                this.uiService.showMessageError("Operación rechazada: Debe haber minimo un concepto de gasto")
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