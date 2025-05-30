<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">{{ loc["Detalle de la Empresa"] }} {{ this.empresaDto.razonSocial }}</p>
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row form-container">
                        <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Roles"] }}</label>
                            <div>
                                <multiselect @open="loadRoles" v-model="listaRolesAgregados" :options="listaRoles"
                                track-by="id" :custom-label="(item) => {return `${item.descripcion}`}" 
                                :hide-selected="true" :close-on-select="true" open-direction="bottom" :multiple="true" 
                                :placeholder="loc['Seleccionar opción']" :disabled="true" :show-labels="false"
                                :class="{
                                    'multiselect-no-arrow': listaRoles.length > 0,
                                    'multiselect-empty': listaRoles.length === 0
                                }">
                                    <template slot="noResult">{{ loc["No se encontraron resultados"] }}</template>
                                </multiselect>
                            </div>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Número de Socio"] }}</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.codigoProveedor ? empresaDto.codigoProveedor : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Razón Social"] }}</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.razonSocial ? empresaDto.razonSocial : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Nombre Fantasía"] }}</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.nombreFantasia ? empresaDto.nombreFantasia : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Identificador Tributario"] }}</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.identificadorTributario ? empresaDto.identificadorTributario : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Tipo de Socio"] }}</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.granContribuyente ? loc['Adherente'] : loc['Pleno']" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Dirección"] }}</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.direccion ? empresaDto.direccion : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Código Postal"] }}</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.codigoPostal ? empresaDto.codigoPostal : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["País"] }}</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.pais ? empresaDto.pais.name : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Provincia"] }}</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.provincia ? empresaDto.provincia.name : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Ciudad"] }}</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.ciudad ? empresaDto.ciudad.name : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Ciudad Descripción"] }}</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.ciudadDescripcion ? empresaDto.ciudadDescripcion : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Teléfono Principal"] }}</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.telefonoPrincipal ? empresaDto.telefonoPrincipal : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Teléfono Alternativo"] }}</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.telefonoAlternativo ? empresaDto.telefonoAlternativo : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Email Principal"] }}</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.emailPrincipal ? empresaDto.emailPrincipal : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Email Alternativo"] }}</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.emailAlternativo ? empresaDto.emailAlternativo : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Contacto"] }}</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.contacto ? empresaDto.contacto : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Contacto Alternativo"] }}</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.contactoAlternativo ? empresaDto.contactoAlternativo : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Tipo Responsable"] }}</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.tipoResponsable ? loc[empresaDto.tipoResponsable.descripcion] : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Confirmado"] }}</label>
                            <div>
                                <input type="checkbox" class="form-check-input" v-model="empresaDto.confirmado" readonly>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12 d-flex justify-content-end mb-3 mt-3">
            <button :disabled="!grants.updateEmpresa" class="btn btn-primary btn-sm" @click="update" style="margin-right: 10px;">
                {{ loc["Editar"] }}
            </button>
            <button :disabled="!grants.deleteEmpresa" class="btn btn-danger btn-sm" @click="remove" style="margin-right: 10px;">
                {{ loc["Eliminar"] }}
            </button>
            <cancel-button @click="goBack">{{ loc["Volver"] }}</cancel-button>
        </div>
    </div>
</template>

<script>
import CancelButton from "@/components/forms/cancel-button.vue";
import UiService from "@/common/uiService";

import EmpresaDto from './EmpresaDto' // Modificar por la clase dto correspondiente

import commonMixin from '@/Common/Mixins/commonMixin';
import detailMixin from '@/Common/Mixins/detailMixin';

import multiselect from "vue-multiselect";

import monedasEmpresaSelect from "@/Components/Empresas/monedasEmpresa-select";

import EmpresaMoneda from "./EmpresaMoneda"

import empresaMonedaForm from './empresaMoneda-form'

import inlineEdit from "@/components/forms/inline-edit-button.vue";
import inlineDelete from "@/components/forms/inline-delete-button.vue";
import inlineCancel from "@/components/forms/inline-cancel-button.vue";

import loc from "@/common/commonLoc.js"

const AFIRMATIVO = true;
const NEGATIVO = false;

const NO_DATA_MESSAGE = loc["No hay datos"];

export default {
    components: {
        CancelButton,
        multiselect,

        monedasEmpresaSelect,

        empresaMonedaForm,
        inlineEdit,
        inlineDelete,
        inlineCancel,
    },
    mixins: [commonMixin, detailMixin],
    name: "empresas-detail",

    data: function () {
        return {
            loc,

            empresaDto: new EmpresaDto(), // Modificar por la clase dto correspondiente
            uiService: new UiService(),

            listaRoles: [],
            dataListaRolesAgregadas : [],
            primerCargaDeRoles: false,

            listaAlicuotas: [],
            dataListaAlicuotasAgregadas : [],
            primerCargaDeAlicuotas: false,

            NO_DATA_MESSAGE,
            idTableEmpresaMonedas: `__empresaMonedas`,
            empresaMonedasKey: 0
        };
    },
    computed: {
        grants() {
            return this.$store.getters.getGrants;
        },
        errorBag() {
            return this.$store.getters.getErrorBag;
        },
        listaRolesAgregados: {
            get() {
                return this.dataListaRolesAgregadas;
            },
            set(values) {
                this.dataListaRolesAgregadas = values;
                this.empresaDto.rolesIdm = values.map(elemento => elemento.idm);
                this.$emit("input", values);
            }
        },
        listaAlicuotasAgregados: {
            get() {
                return this.dataListaAlicuotasAgregadas;
            },
            set(values) {
                this.dataListaAlicuotasAgregadas = values;
                this.empresaDto.alicuotasIdm = values.map(elemento => elemento.idm);
                this.$emit("input", values);
            }
        }
    },
    async mounted() {
        await this.init();
    },
    methods: {
        addNuevaMoneda(){
            let nuevoItem = new EmpresaMoneda();
            nuevoItem.mode.setCreate();

            this.empresaDto.monedas.push(nuevoItem);
        },
        updateMoneda(i){
            this.uiService.onlyDestroyDataTablesManual(this.idTableEmpresaMonedas);
            i.mode.setUpdate();
        },
        async removeMoneda(cd, index) {
            if (
                await this.uiService.confirmActionModal(
                    `¿Está seguro que desea eliminar la Moneda?`, //${cd.name}
                    "Aceptar",
                    "Cancelar"
                )
            ) {
                cd.deleted = true;
                this.removeRowMoneda(index);
                this.uiService.showMessageSuccess(`Se ha eliminado la Moneda ${cd.name}`);
            }
        },
        cancelEditMoneda(key) {
            clearMessage();
            if (this.empresaDto.monedas[key].mode.isUpdate) {
                this.empresaDto.monedas[key].mode.setDetail();
            } else {
                this.removeRowMoneda(key);
            }
        },
        removeRowMoneda(index) {
            this.uiService.onlyDestroyDataTablesManual(this.idTableEmpresaMonedas);

            if (this.empresaDto.monedas[index].mode.isCreate) {
                this.empresaDto.monedas.splice(index, 1);
            }
            this.errorBag.addError(`Monedas[${index}].currencyId`, "");
            this.errorBag.addError(`Monedas[${index}].monedaPorDefecto`, "");

            this.empresaMonedasKey += 1;

            this.$nextTick(() => {
                this.uiService.transformToDataTablesManual(this.idTableEmpresaMonedas);
            });
        },
        editFinishedMoneda(detalle, index) {
            this.uiService.onlyDestroyDataTablesManual(this.idTableEmpresaMonedas);
            this.empresaDto.monedas[index] = Object.assign({}, detalle);
            this.empresaDto.monedas[index].mode.setDetail();

            this.$nextTick(() => {
                this.uiService.transformToDataTablesManual(this.idTableEmpresaMonedas);
            });

        },
        async loadRoles() {
            if (this.primerCargaDeRoles) {
                return;
            }
            this.listaRoles = await this.$store.dispatch("getRolesListAsync");
            this.primerCargaDeRoles = true;
        },
        async loadAlicuotas() {
            if (this.primerCargaDeAlicuotas) {
                return;
            }
            this.listaAlicuotas = await this.$store.dispatch("getAlicuotasListAsync");
            this.primerCargaDeAlicuotas = true;
        },
        async init() {
            if (this.$route.params.id) await this.getAsync(this.$route.params.id);
            else this.goBack();
        },
        async getAsync(id) {
            this.uiService.showSpinner(true);
            await this.$store.dispatch("getAsync", id)
                .then((res) => {
                    this.dataListaAlicuotasAgregadas = res.alicuotas
                    this.dataListaRolesAgregadas = res.roles
                    this.empresaDto = new EmpresaDto(res);
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        seeDetail() {
            // Limpiamos la lista antes de navegar
            this.$store.dispatch("clearList");
            this.$router.push({ name: "detail", params: { id: this.empresaDto.id } });
        },
        async remove() {
            if (
                await this.uiService.confirmActionModal(
                loc["¿Está usted seguro que desea eliminar a esta empresa portal?"],
                loc["Aceptar"],
                loc["Cancelar"]
                )
            ){
                this.uiService.showSpinner(true)
                await this.$store.dispatch("deleteAsync", this.empresaDto)
                    .then(async () => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess(loc["Operación confirmada"])
                            this.goBack();
                        } else {
                            this.uiService.showMessageError(loc["Operación rechazada"])
                        }
                    })
                    .finally(() => {
                        this.uiService.showSpinner(false);
                    });
            }
        },
        goBack() {
            if(this.$route.params.create)
                this.$router.push({ name: "home", query: { fromDetail: false } });
            else
                this.$router.push({ name: "home", query: { fromDetail: true } });
        },
        update() {
            this.$router.push({ name: "edit", query: {desdeDetalle: true}, params: { id: this.empresaDto.id } });
        }
    },
};
</script>

<style>
.multiselect-no-arrow .multiselect__select {
    display: none;
}

.multiselect-empty .multiselect__tags,
.multiselect-no-arrow .multiselect__tags {
    background-color: #e9ecef;
    color: black;
}

.multiselect-empty .multiselect__single,
.multiselect-empty .multiselect__placeholder,
.multiselect-no-arrow .multiselect__placeholder {
    color: black;
}

.multiselect-empty .multiselect__input,
.multiselect-no-arrow .multiselect__input {
    background-color: #e9ecef;
    color: black;
}
</style>