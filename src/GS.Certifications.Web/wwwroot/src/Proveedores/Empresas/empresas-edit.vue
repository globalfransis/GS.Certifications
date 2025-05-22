<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">Modificación de la Empresa {{ this.empresaDto.razonSocial }}</p> <!-- Agregar título, por ejemplo: Modificación del Usuario {userId} -->
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <!-- Agregar campos del formulario de alta -->
                        <!-- Este es un ejemplo -->
                        <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Roles</label>
                            <div>
                                <multiselect @open="loadRoles" v-model="listaRolesAgregados" :options="listaRoles"
                                track-by="idm" :custom-label="(item) => {return `${item.descripcion}`}" :hide-selected="true"
                                :close-on-select="true" open-direction="bottom" :multiple="true" placeholder="Seleccionar opción"
                                :show-labels="false">
                                <template slot="noResult"> No se encontraron resultados</template>
                                </multiselect>
                            </div>
                            <span class="text-danger field-validation-error" data-valmsg-for="RolesIdm" data-valmsg-replace="true">
                                {{ errorBag.get("RolesIdm") }}
                            </span>
                        </div>

                        <!-- <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Alicuotas (IVA)</label>
                            <div>
                                <multiselect @open="loadAlicuotas" v-model="listaAlicuotasAgregados" :options="listaAlicuotas"
                                track-by="idm" :custom-label="(item) => {return `${item.valor} %`}" :hide-selected="true"
                                :close-on-select="true" open-direction="bottom" :multiple="true" placeholder="Seleccionar opción"
                                :show-labels="false">
                                <template slot="noResult"> No se encontraron resultados</template>
                                </multiselect>
                            </div>
                            <span class="text-danger field-validation-error" data-valmsg-for="AlicuotasIdm" data-valmsg-replace="true">
                                {{ errorBag.get("AlicuotasIdm") }}
                            </span>
                        </div> -->

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Número de Socio</label><span class="text-danger">*</span>
                            <input maxlength="15" type="text" class="form-control" v-model="empresaDto.codigoProveedor">
                            <span class="text-danger field-validation-error" data-valmsg-for="CodigoProveedor" data-valmsg-replace="true">
                                {{ errorBag.get("CodigoProveedor") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Razón Social</label><span class="text-danger">*</span>
                            <input maxlength="100" type="text" class="form-control" v-model="empresaDto.razonSocial">
                            <span class="text-danger field-validation-error" data-valmsg-for="RazonSocial" data-valmsg-replace="true">
                                {{ errorBag.get("RazonSocial") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Nombre Fantasía</label><span class="text-danger">*</span>
                            <input maxlength="100" type="text" class="form-control" v-model="empresaDto.nombreFantasia">
                            <span class="text-danger field-validation-error" data-valmsg-for="NombreFantasia" data-valmsg-replace="true">
                                {{ errorBag.get("NombreFantasia") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Identificador Tributario</label><span class="text-danger">*</span>
                            <input maxlength="30" type="text" class="form-control" v-model="empresaDto.identificadorTributario">
                            <span class="text-danger field-validation-error" data-valmsg-for="IdentificadorTributario" data-valmsg-replace="true">
                                {{ errorBag.get("IdentificadorTributario") }}
                            </span>
                        </div>
                        <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Tipo de Socio</label><span class="text-danger">*</span>
                            <select class="form-select" v-model="empresaDto.granContribuyente">
                                <option :value="null">Sin Especificar</option>
                                <option :value="AFIRMATIVO">Adherente</option>
                                <option :value="NEGATIVO">Pleno</option>
                            </select>
                            <span class="text-danger field-validation-error" data-valmsg-for="GranContribuyente" data-valmsg-replace="true">
                                {{ errorBag.get("GranContribuyente") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Dirección</label>
                            <input maxlength="120" type="text" class="form-control" v-model="empresaDto.direccion">
                            <span class="text-danger field-validation-error" data-valmsg-for="Direccion" data-valmsg-replace="true">
                                {{ errorBag.get("Direccion") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Código Postal</label>
                            <input maxlength="20" type="text" class="form-control" v-model="empresaDto.codigoPostal">
                            <span class="text-danger field-validation-error" data-valmsg-for="CodigoPostal" data-valmsg-replace="true">
                                {{ errorBag.get("CodigoPostal") }}
                            </span>
                        </div>
                        <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">País</label><!--<span class="text-danger">*</span>-->
                            <country-select v-model.number="empresaDto.paisId" />
                            <span class="text-danger field-validation-error" data-valmsg-for="Pais" data-valmsg-replace="true">
                                {{ errorBag.get("Pais") }}
                            </span>
                        </div>
                        <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Provincia</label>
                            <province-select :enabled="empresaDto.paisId != null" v-model.number="empresaDto.provinciaId"
                            :paisId="empresaDto.paisId" />
                            <span class="text-danger field-validation-error" data-valmsg-for="Provincia" data-valmsg-replace="true">
                                {{ errorBag.get("Provincia") }}
                            </span>
                        </div>
                        <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Ciudad</label>
                            <city-select :enabled="empresaDto.provinciaId != null && empresaDto.paisId != null"
                            v-model.number="empresaDto.ciudadId"
                            :provinciaId="empresaDto.provinciaId" />
                            <span class="text-danger field-validation-error" data-valmsg-for="Ciudad" data-valmsg-replace="true">
                                {{ errorBag.get("Ciudad") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Ciudad Descripción</label>
                            <input maxlength="1000" type="text" class="form-control" v-model="empresaDto.ciudadDescripcion">
                            <span class="text-danger field-validation-error" data-valmsg-for="CiudadDescripcion" data-valmsg-replace="true">
                                {{ errorBag.get("CiudadDescripcion") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Teléfono Principal</label><span class="text-danger">*</span>
                            <input maxlength="25" type="text" class="form-control" v-model="empresaDto.telefonoPrincipal">
                            <span class="text-danger field-validation-error" data-valmsg-for="TelefonoPrincipal" data-valmsg-replace="true">
                                {{ errorBag.get("TelefonoPrincipal") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Teléfono Alternativo</label>
                            <input maxlength="25" type="text" class="form-control" v-model="empresaDto.telefonoAlternativo">
                            <span class="text-danger field-validation-error" data-valmsg-for="TelefonoAlternativo" data-valmsg-replace="true">
                                {{ errorBag.get("TelefonoAlternativo") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Email Principal</label><span class="text-danger">*</span>
                            <input maxlength="150" type="text" class="form-control" v-model="empresaDto.emailPrincipal">
                            <span class="text-danger field-validation-error" data-valmsg-for="EmailPrincipal" data-valmsg-replace="true">
                                {{ errorBag.get("EmailPrincipal") }}
                            </span>
                        </div>

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Email Alternativo</label>
                            <input maxlength="150" type="text" class="form-control" v-model="empresaDto.emailAlternativo">
                            <span class="text-danger field-validation-error" data-valmsg-for="EmailAlternativo" data-valmsg-replace="true">
                                {{ errorBag.get("EmailAlternativo") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Contacto</label><span class="text-danger">*</span>
                            <input maxlength="30" type="text" class="form-control" v-model="empresaDto.contacto">
                            <span class="text-danger field-validation-error" data-valmsg-for="Contacto" data-valmsg-replace="true">
                                {{ errorBag.get("Contacto") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Contacto Alternativo</label>
                            <input maxlength="30" type="text" class="form-control" v-model="empresaDto.contactoAlternativo">
                            <span class="text-danger field-validation-error" data-valmsg-for="ContactoAlternativo" data-valmsg-replace="true">
                                {{ errorBag.get("ContactoAlternativo") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Tipo Responsable</label><!--<span class="text-danger">*</span>-->
                            <categoriasTipos-select v-model.number="empresaDto.tipoResponsableId" />
                            <span class="text-danger field-validation-error" data-valmsg-for="TipoResponsable" data-valmsg-replace="true">
                                {{ errorBag.get("TipoResponsable") }}
                            </span>
                        </div>
                        <!-- <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Número Ingresos Brutos</label>
                            <input maxlength="30" type="text" class="form-control" v-model="empresaDto.numeroIngresosBrutos">
                            <span class="text-danger field-validation-error" data-valmsg-for="NumeroIngresosBrutos" data-valmsg-replace="true">
                                {{ errorBag.get("NumeroIngresosBrutos") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Tipo Cuenta</label>
                            <tiposCuentas-select v-model.number="empresaDto.tipoCuentaId" />
                            <span class="text-danger field-validation-error" data-valmsg-for="TipoCuenta" data-valmsg-replace="true">
                                {{ errorBag.get("TipoCuenta") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Cuenta Bancaria</label>
                            <input maxlength="50" type="text" class="form-control" v-model="empresaDto.cuentaBancaria">
                            <span class="text-danger field-validation-error" data-valmsg-for="CuentaBancaria" data-valmsg-replace="true">
                                {{ errorBag.get("CuentaBancaria") }}
                            </span>
                        </div>

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Página Web</label>
                            <input maxlength="100" type="text" class="form-control" v-model="empresaDto.paginaWeb">
                            <span class="text-danger field-validation-error" data-valmsg-for="PaginaWeb" data-valmsg-replace="true">
                                {{ errorBag.get("PaginaWeb") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Redes Sociales</label>
                            <input maxlength="200" type="text" class="form-control" v-model="empresaDto.redesSociales">
                            <span class="text-danger field-validation-error" data-valmsg-for="RedesSociales" data-valmsg-replace="true">
                                {{ errorBag.get("RedesSociales") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Descripción Empresa</label>
                            <input maxlength="500" type="text" class="form-control" v-model="empresaDto.descripcionEmpresa">
                            <span class="text-danger field-validation-error" data-valmsg-for="DescripcionEmpresa" data-valmsg-replace="true">
                                {{ errorBag.get("DescripcionEmpresa") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Productos Servicios Ofrecidos</label>
                            <input maxlength="500" type="text" class="form-control" v-model="empresaDto.productosServiciosOfrecidos">
                            <span class="text-danger field-validation-error" data-valmsg-for="ProductosServiciosOfrecidos" data-valmsg-replace="true">
                                {{ errorBag.get("ProductosServiciosOfrecidos") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Referencias Comerciales</label>
                            <input maxlength="500" type="text" class="form-control" v-model="empresaDto.referenciasComerciales">
                            <span class="text-danger field-validation-error" data-valmsg-for="ReferenciasComerciales" data-valmsg-replace="true">
                                {{ errorBag.get("ReferenciasComerciales") }}
                            </span>
                        </div> -->
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Confirmado</label>
                            <div>
                                <input type="checkbox" class="form-check-input" v-model="empresaDto.confirmado">
                            </div>
                            <span class="text-danger field-validation-error" data-valmsg-for="Confirmado" data-valmsg-replace="true">
                                {{ errorBag.get("Confirmado") }}
                            </span>
                        </div>

                        <!-- <hr>

                        <div class="col-12 d-flex justify-content-between align-items-center mt-4 mb-4">
                            <div>
                                <label class="control-label h5 m-0">Monedas</label><span class="text-danger">*</span>
                            </div>
                            <button type="button" class="btn btn-outline-primary btn-sm" @click="addNuevaMoneda()">
                                <b><i class="fas fa-plus"></i>Agregar</b>
                            </button>
                        </div>
                        <div>
                            <span class="text-danger field-validation-error">
                                {{ errorBag.get("monedas") }}
                            </span>
                        </div>
                        <div>
                            <table :id="`${idTableEmpresaMonedas}`" :key="empresaMonedasKey" class="table table-bordered table-hover">
                                <thead class="table-top">
                                    <tr class="text-center align-middle">
                                        <th scope="col">Moneda</th>
                                        <th scope="col">Predeterminado</th>
                                        <th scope="col">Acciones</th>
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
                                            <td data-toggle="tooltip" class="text-center align-middle">
                                                <div class="d-inline-flex">
                                                    <inline-edit @click="updateMoneda(i)"
                                                        :enabled="i.mode.isDetail" />
                                                    <inline-delete @click="removeMoneda(i, index)"
                                                        :enabled="i.mode.isDetail" />
                                                </div>
                                            </td>
                                        </tr>
                                        <empresaMoneda-form :ref="`empresaMonedaForm-${index}`" :key="index" :index="index" :empresaMoneda="i"
                                            :listaMonedasUsadas="listaMonedasUsadas"
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
            <accept-button :disabled="!grants.updateEmpresa" @click="updateAsync" style="margin-right: 10px;">
                Aceptar
            </accept-button>
            <cancel-button @click="cancel">Cancelar</cancel-button>
        </div>
    </div>
</template>

<script>
import AcceptButton from "@/components/forms/accept-button.vue";
import CancelButton from "@/components/forms/cancel-button.vue";
import UiService from "@/common/uiService";
import EmpresaEditDto from './EmpresaEditDto' // Modificar por la clase dto que corresponda
import commonMixin from '@/Common/Mixins/commonMixin';

import citySelect from "@/Components/Global/Cities/city-select";
import provinceSelect from "@/Components/Global/Provinces/province-select";
import countrySelect from "@/Components/Global/Countries/country-select";

import categoriasTiposSelect from "@/Components/Empresas/categoriasTipos-select";
import tiposCuentasSelect from "@/Components/Empresas/tiposCuentas-select";
import monedasEmpresaSelect from "@/Components/Empresas/monedasEmpresa-select";

import EmpresaMoneda from "./EmpresaMoneda"

import empresaMonedaForm from './empresaMoneda-form'

import inlineEdit from "@/components/forms/inline-edit-button.vue";
import inlineDelete from "@/components/forms/inline-delete-button.vue";
import inlineCancel from "@/components/forms/inline-cancel-button.vue";

import multiselect from "vue-multiselect";

const AFIRMATIVO = true;
const NEGATIVO = false;

const NO_DATA_MESSAGE = "No hay datos";

export default {
    components: {
        AcceptButton,
        CancelButton,

        citySelect,
        provinceSelect,
        countrySelect,
        categoriasTiposSelect,
        tiposCuentasSelect,
        monedasEmpresaSelect,

        empresaMonedaForm,

        multiselect,
        inlineEdit,
        inlineDelete,
        inlineCancel,
    },
    mixins: [commonMixin],
    name: "empresas-edit",
    data: function () {
        return {
            empresaDto: new EmpresaEditDto(),
            uiService: new UiService(),
            primeraCargaPais: true,
            primeraCargaProvincia: true,

            AFIRMATIVO,
            NEGATIVO,

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
        },
        listaMonedasUsadas() {
            return this.empresaDto.monedas ? 
            this.empresaDto.monedas.filter(m => !m.deleted && !m.mode.isEdit).map(elemento => elemento.currencyId) : []
        }
    },
    async mounted() {
        await this.init();
    },
    methods: {
        addNuevaMoneda(){
            this.errorBag.clear();
            let nuevoItem = new EmpresaMoneda();
            nuevoItem.mode.setCreate();

            this.empresaDto.monedas.push(nuevoItem);
        },
        updateMoneda(i){
            this.errorBag.clear();
            this.uiService.onlyDestroyDataTablesManual(this.idTableEmpresaMonedas);
            i.mode.setUpdate();
            eval("this.$refs." + `empresaMonedaForm-${index}.$forceUpdate()`);
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
            this.errorBag.addError(`Monedas[${index}].modenaPorDefecto`, "");

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
            this.errorBag.clear();
            // Si no hay permisos de modificación, volvemos a la lista
            if (!this.grants.updateEmpresa) this.$router.push({ name: "home" });
            else {
                if (this.$route.params.id) await this.getAsync(this.$route.params.id);
                else this.goBack();
            }
        },
        async getAsync(id) {
            this.errorBag.clear();
            this.uiService.showSpinner(true)
            await this.$store.dispatch("getAsync", id)
                .then((res) => {
                    this.dataListaRolesAgregadas = res.roles
                    this.dataListaAlicuotasAgregadas = res.alicuotas
                    this.empresaDto = new EmpresaEditDto(res);
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        goDetail() {
            this.errorBag.clear();
            this.$router.push({ name: "detail", params: { id: this.empresaDto.id } });
        },
        goBack() {
            this.errorBag.clear();
            this.$router.push({ name: "detail", params: { id: this.empresaDto.id } });
        },
        cancel() {
            this.errorBag.clear();
            if(this.$route.query.desdeDetalle){
                this.goBack()
            }else {
                this.$router.push({ name: "home", query: { fromDetail: true } });
            }
        },
        async updateAsync() {
            this.errorBag.clear();
            if(this.validacionDatosObligatorios()){
                this.uiService.showSpinner(true)
                await this.$store.dispatch("putAsync", this.empresaDto)
                .then(() => {
                    if (!this.errorBag.hasErrors()) {
                        this.uiService.showMessageSuccess("Operación confirmada")
                        this.goDetail();
                    } else {
                        this.uiService.showMessageError("Operación rechazada")
                    }
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
            }
        },
        validacionDatosNumericos(){
            var validacion = true
            if (isNaN(this.empresaDto.identificadorTributario)) {
                validacion = false
                this.errorBag.addError("identificadorTributario", ["El valor ingresado debe ser numerico"]);
            }
            if (isNaN(this.empresaDto.telefonoPrincipal)) {
                validacion = false
                this.errorBag.addError("telefonoPrincipal", ["El valor ingresado debe ser numerico"]);
            }
            if (isNaN(this.empresaDto.telefonoAlternativo)) {
                validacion = false
                this.errorBag.addError("telefonoAlternativo", ["El valor ingresado debe ser numerico"]);
            }
            return validacion
        },
        validacionEmails(){
            var validacion = true
            const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            if(!regex.test(this.empresaDto.emailPrincipal) && (this.empresaDto.emailPrincipal != '' && this.empresaDto.emailPrincipal != "" && this.empresaDto.emailPrincipal != null)){
                validacion = false
                this.errorBag.addError("emailPrincipal", ["El formato de Email es incorrecto"]);
            }
            if(!regex.test(this.empresaDto.emailAlternativo) && (this.empresaDto.emailAlternativo != '' && this.empresaDto.emailAlternativo != "" && this.empresaDto.emailAlternativo != null)){
                validacion = false
                this.errorBag.addError("emailAlternativo", ["El formato de Email es incorrecto"]);
            }
            return validacion
        },
        validacionDatosObligatorios(){
            var validacion = true
            if(!this.validacionEmails()){
                validacion = false
            }
            if(!this.validacionDatosNumericos()){
                validacion = false
            }
            if(this.empresaDto.rolesIdm.length === 0){
                validacion = false;
                this.errorBag.addError("rolesIdm", ["Se debe agregar un rol como minimo"]);
            }
            if(this.empresaDto.codigoProveedor == '' || this.empresaDto.codigoProveedor == "" || this.empresaDto.codigoProveedor == null){
                validacion = false;
                this.errorBag.addError("codigoProveedor", ["El campo 'Código Proveedor' es obligatorio"]);
            }
            if(this.empresaDto.razonSocial == '' || this.empresaDto.razonSocial == "" || this.empresaDto.razonSocial == null){
                validacion = false;
                this.errorBag.addError("razonSocial", ["El campo 'Razón Social' es obligatorio"]);
            }
            if(this.empresaDto.nombreFantasia == '' || this.empresaDto.nombreFantasia == "" || this.empresaDto.nombreFantasia == null){
                validacion = false;
                this.errorBag.addError("nombreFantasia", ["El campo 'Nombre Fantasía' es obligatorio"]);
            }
            if(this.empresaDto.identificadorTributario == '' || this.empresaDto.identificadorTributario == "" || this.empresaDto.identificadorTributario == null){
                validacion = false;
                this.errorBag.addError("identificadorTributario", ["El campo 'Identificador Tributario' es obligatorio"]);
            }
            if(this.empresaDto.granContribuyente == null){
                validacion = false;
                this.errorBag.addError("granContribuyente", ["El campo 'Gran Contribuyente' es obligatorio"]);
            }
            // if(this.empresaDto.paisId == null){
            //     validacion = false;
            //     this.errorBag.addError("Pais", ["El campo 'País' es obligatorio"]);
            // }
            if(this.empresaDto.telefonoPrincipal == '' || this.empresaDto.telefonoPrincipal == "" || this.empresaDto.telefonoPrincipal == null){
                validacion = false;
                this.errorBag.addError("telefonoPrincipal", ["El campo 'Teléfono Principal' es obligatorio"]);
            }
            if(this.empresaDto.emailPrincipal == '' || this.empresaDto.emailPrincipal == "" || this.empresaDto.emailPrincipal == null){
                validacion = false;
                this.errorBag.addError("emailPrincipal", ["El campo 'Email Principal' es obligatorio"]);
            }
            if(this.empresaDto.contacto == '' || this.empresaDto.contacto == "" || this.empresaDto.contacto == null){
                validacion = false;
                this.errorBag.addError("contacto", ["El campo 'Contacto' es obligatorio"]);
            }
            // if(this.empresaDto.tipoResponsableId == null){
            //     validacion = false;
            //     this.errorBag.addError("tipoResponsable", ["El campo 'Tipo Responsable' es obligatorio"]);
            // }

            // var listaNoEliminadas = this.empresaDto.monedas.filter(moneda => !moneda.deleted)

            // if(listaNoEliminadas.length == 0){
            //     validacion = false;
            //     this.errorBag.addError("monedas", ["El campo 'Monedas' es obligatorio"]);
            // }
            // if(listaNoEliminadas.length != 0 && listaNoEliminadas.filter(moneda => moneda.monedaPorDefecto === true).length !== 1){
            //     validacion = false;
            //     this.errorBag.addError("monedas", ["Se debe marcar una unica moneda como predeterminada"]);
            // }
            return validacion
        }
    },
    watch: {
        'empresaDto.paisId': function(){
            if(this.primeraCargaPais){
                this.primeraCargaPais = false;
                return
            }
            this.empresaDto.provinciaId = null;
            this.empresaDto.ciudadId = null;
        },
        'empresaDto.provinciaId': function(){
            if(this.primeraCargaProvincia){
                this.primeraCargaProvincia = false;
                return
            }
            this.empresaDto.ciudadId = null;
        }
    }
};
</script>