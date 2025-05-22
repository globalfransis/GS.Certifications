<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">Detalle de la Empresa {{ this.empresaDto.razonSocial }}</p> <!-- Agregar un título, por ejemplo: Detalle del Usuario {userId} -->
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row form-container">
                        <!-- Agregar campos del detalle -->
                        <!-- Este es un ejemplo -->
                        <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Roles</label>
                            <div>
                                <multiselect @open="loadRoles" v-model="listaRolesAgregados" :options="listaRoles"
                                track-by="id" :custom-label="(item) => {return `${item.descripcion}`}" 
                                :hide-selected="true" :close-on-select="true" open-direction="bottom" :multiple="true" 
                                placeholder="Seleccionar opción" :disabled="true" :show-labels="false"
                                :class="{
                                    'multiselect-no-arrow': listaRoles.length > 0,
                                    'multiselect-empty': listaRoles.length === 0
                                }">
                                    <template slot="noResult"> No se encontraron resultados</template>
                                </multiselect>
                            </div>
                        </div>

                        <!-- <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Alicuotas (IVA)</label>
                            <div>
                                <multiselect @open="loadAlicuotas" v-model="listaAlicuotasAgregados" :options="listaAlicuotas"
                                track-by="id" :custom-label="(item) => {return `${item.valor} %`}" 
                                :hide-selected="true" :close-on-select="true" open-direction="bottom" :multiple="true" 
                                placeholder="Seleccionar opción" :disabled="true" :show-labels="false"
                                :class="{
                                    'multiselect-no-arrow': listaAlicuotas.length > 0,
                                    'multiselect-empty': listaAlicuotas.length === 0
                                }">
                                    <template slot="noResult"> No se encontraron resultados</template>
                                </multiselect>
                            </div>
                        </div> -->

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Número de Socio</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.codigoProveedor ? empresaDto.codigoProveedor : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Razón Social</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.razonSocial ? empresaDto.razonSocial : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Nombre Fantasía</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.nombreFantasia ? empresaDto.nombreFantasia : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Identificador Tributario</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.identificadorTributario ? empresaDto.identificadorTributario : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Tipo de Socio</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.granContribuyente ? 'Adherente' : 'Pleno'" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Dirección</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.direccion ? empresaDto.direccion : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Código Postal</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.codigoPostal ? empresaDto.codigoPostal : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">País</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.pais ? empresaDto.pais.name : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Provincia</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.provincia ? empresaDto.provincia.name : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Ciudad</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.ciudad ? empresaDto.ciudad.name : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Ciudad Descripción</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.ciudadDescripcion ? empresaDto.ciudadDescripcion : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Teléfono Principal</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.telefonoPrincipal ? empresaDto.telefonoPrincipal : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Teléfono Alternativo</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.telefonoAlternativo ? empresaDto.telefonoAlternativo : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Email Principal</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.emailPrincipal ? empresaDto.emailPrincipal : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Email Alternativo</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.emailAlternativo ? empresaDto.emailAlternativo : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Contacto</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.contacto ? empresaDto.contacto : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Contacto Alternativo</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.contactoAlternativo ? empresaDto.contactoAlternativo : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Tipo Responsable</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.tipoResponsable ? empresaDto.tipoResponsable.descripcion : null" readonly>
                        </div>
                        <!-- <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Número Ingresos Brutos</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.numeroIngresosBrutos ? empresaDto.numeroIngresosBrutos : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Tipo Cuenta</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.tipoCuenta ? empresaDto.tipoCuenta.nombre : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Cuenta Bancaria</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.cuentaBancaria ? empresaDto.cuentaBancaria : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Página Web</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.paginaWeb ? empresaDto.paginaWeb : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Redes Sociales</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.redesSociales ? empresaDto.redesSociales : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Descripción Empresa</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.descripcionEmpresa ? empresaDto.descripcionEmpresa : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Productos Servicios Ofrecidos</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.productosServiciosOfrecidos ? empresaDto.productosServiciosOfrecidos : null" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Referencias Comerciales</label>
                            <input type="text" class="form-control" 
                            :value="empresaDto.referenciasComerciales ? empresaDto.referenciasComerciales : null" readonly>
                        </div> -->
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Confirmado</label>
                            <div>
                                <input type="checkbox" class="form-check-input" v-model="empresaDto.confirmado" readonly>
                            </div>
                        </div>

                        <!-- <hr>

                        <div class="col-12 d-flex justify-content-between align-items-center mt-4 mb-4">
                            <div>
                                <p class="h5 m-0">Monedas</p>
                                <span class="text-danger field-validation-error">
                                    {{ errorBag.get("monedas") }}
                                </span>
                            </div>
                        </div>
                        <div>
                            <table :id="`${idTableEmpresaMonedas}`" :key="empresaMonedasKey" class="table table-bordered table-hover">
                                <thead class="table-top">
                                    <tr class="text-center align-middle">
                                        <th scope="col">Moneda</th>
                                        <th scope="col">Predeterminado</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-if="empresaDto.monedas.filter(i => !i.deleted).length === 0"
                                        class="no-data">
                                        <td colspan="100" class="text-center">{{ NO_DATA_MESSAGE }}</td>
                                    </tr>
                                    <template v-for="(i, index) in empresaDto.monedas">
                                        <tr v-if="i.mode.isDetail && !i.deleted" :key="index">
                                            <td data-toggle="tooltip">
                                                <monedasEmpresa-select disabled v-model="i.currencyId"/>
                                            </td>
                                            <td data-toggle="tooltip" class="text-center align-middle">
                                                <input disabled placeholder="Moneda predeterminada"
                                                 id="monedaPorDefecto" type="checkbox"
                                                  class="form-check-input" v-model="i.monedaPorDefecto"/>
                                            </td>
                                        </tr>
                                        <empresaMoneda-form :key="index" :index="index" :empresaMoneda="i"
                                            @cancel-edit="cancelEditMoneda(index)" v-if="i.mode.isEdit"
                                            @edit-finished="editFinishedMoneda($event, index)" />
                                    </template>
                                </tbody>
                            </table>
                        </div> -->
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12 d-flex justify-content-end mb-3 mt-3">
            <button :disabled="!grants.updateEmpresa" class="btn btn-primary btn-sm" @click="update" style="margin-right: 10px;">
                Editar
            </button>
            <button :disabled="!grants.deleteEmpresa" class="btn btn-danger btn-sm" @click="remove" style="margin-right: 10px;">
                Eliminar
            </button>
            <cancel-button @click="goBack">Volver</cancel-button>
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

const AFIRMATIVO = true;
const NEGATIVO = false;

const NO_DATA_MESSAGE = "No hay datos";

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
                "¿Está usted seguro que desea eliminar a esta empresa portal?",
                "Aceptar",
                "Cancelar"
                )
            ){
                this.uiService.showSpinner(true)
                await this.$store.dispatch("deleteAsync", this.empresaDto)
                    .then(async () => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            this.goBack();
                        } else {
                            this.uiService.showMessageError("Operación rechazada")
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