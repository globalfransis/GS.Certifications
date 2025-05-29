<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">{{ loc["Alta de Empresa"] }}</p>
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Roles"] }}</label><span class="text-danger">*</span>
                            <div>
                                <multiselect @open="loadRoles" v-model="listaRolesAgregados" :options="listaRoles"
                                track-by="idm" :custom-label="(item) => {return `${item.descripcion}`}" :hide-selected="true"
                                :close-on-select="true" open-direction="bottom" :multiple="true" :placeholder="loc['Seleccionar opción']"
                                :show-labels="false">
                                <template slot="noResult">{{ loc["No se encontraron resultados"] }}</template>
                                </multiselect>
                            </div>
                            <span class="text-danger field-validation-error" data-valmsg-for="RolesIdm" data-valmsg-replace="true">
                                {{ errorBag.get("RolesIdm") }}
                            </span>
                        </div>        

                        <div v-if="listaDocumentosCompras && listaDocumentosCompras.length > 0" class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Documentos de compras"] }}</label><span class="text-danger">*</span>
                            <div>
                                <multiselect @open="loadDocumentosCompras" v-model="listaDocumentosComprasAgregados" :options="listaDocumentosCompras"
                                track-by="id" :custom-label="(item) => {return `${item.nombre}`}" :hide-selected="true"
                                :close-on-select="true" open-direction="bottom" :multiple="true" :placeholder="loc['Seleccionar opción']"
                                :show-labels="false">
                                <template slot="noResult">{{ loc["No se encontraron resultados"] }}</template>
                                </multiselect>
                            </div>
                            <span class="text-danger field-validation-error" data-valmsg-for="OrdenesComprasTiposId" data-valmsg-replace="true">
                                {{ errorBag.get("OrdenesComprasTiposId") }}
                            </span>
                        </div>
                        
                        <div v-if="listaConceptosGastos && listaConceptosGastos.length > 0" class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Conceptos de Gastos"] }}</label><span class="text-danger">*</span>
                            <div>
                                <multiselect @open="loadConceptosGastos" v-model="listaConceptosGastosAgregados" :options="listaConceptosGastos"
                                track-by="id" :custom-label="(item) => {return `${item.nombre}`}" :hide-selected="true"
                                :close-on-select="true" open-direction="bottom" :multiple="true" :placeholder="loc['Seleccionar opción']"
                                :show-labels="false">
                                <template slot="noResult">{{ loc["No se encontraron resultados"] }}</template>
                                </multiselect>
                            </div>
                            <span class="text-danger field-validation-error" data-valmsg-for="ConceptosGastosTiposId" data-valmsg-replace="true">
                                {{ errorBag.get("ConceptosGastosTiposId") }}
                            </span>
                        </div> 
                        
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Número de Socio"] }}</label><span class="text-danger">*</span>
                            <input maxlength="15" type="text" class="form-control" v-model="empresaDto.codigoProveedor">
                            <span class="text-danger field-validation-error" data-valmsg-for="CodigoProveedor" data-valmsg-replace="true">
                                {{ errorBag.get("CodigoProveedor") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Razón Social"] }}</label><span class="text-danger">*</span>
                            <input maxlength="100" type="text" class="form-control" v-model="empresaDto.razonSocial">
                            <span class="text-danger field-validation-error" data-valmsg-for="RazonSocial" data-valmsg-replace="true">
                                {{ errorBag.get("RazonSocial") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Nombre Fantasía"] }}</label><span class="text-danger">*</span>
                            <input maxlength="100" type="text" class="form-control" v-model="empresaDto.nombreFantasia">
                            <span class="text-danger field-validation-error" data-valmsg-for="NombreFantasia" data-valmsg-replace="true">
                                {{ errorBag.get("NombreFantasia") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Identificador Tributario"] }}</label><span class="text-danger">*</span>
                            <input maxlength="30" type="text" class="form-control" v-model="empresaDto.identificadorTributario">
                            <span class="text-danger field-validation-error" data-valmsg-for="IdentificadorTributario" data-valmsg-replace="true">
                                {{ errorBag.get("IdentificadorTributario") }}
                            </span>
                        </div>
                        <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Tipo de Socio"] }}</label><span class="text-danger">*</span>
                            <select class="form-select" v-model="empresaDto.granContribuyente">
                                <option :value="null">{{ loc["Sin especificar"] }}</option>
                                <option :value="AFIRMATIVO">{{ loc["Adherente"] }}</option>
                                <option :value="NEGATIVO">{{ loc["Pleno"] }}</option>
                            </select>
                            <span class="text-danger field-validation-error" data-valmsg-for="GranContribuyente" data-valmsg-replace="true">
                                {{ errorBag.get("GranContribuyente") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Direccion"] }}</label>
                            <input maxlength="120" type="text" class="form-control" v-model="empresaDto.direccion">
                            <span class="text-danger field-validation-error" data-valmsg-for="Direccion" data-valmsg-replace="true">
                                {{ errorBag.get("Dirección") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Código Postal"] }}</label>
                            <input maxlength="20" type="text" class="form-control" v-model="empresaDto.codigoPostal">
                            <span class="text-danger field-validation-error" data-valmsg-for="CodigoPostal" data-valmsg-replace="true">
                                {{ errorBag.get("CodigoPostal") }}
                            </span>
                        </div>
                        <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["País"] }}</label>
                            <country-select v-model.number="empresaDto.paisId" />
                            <span class="text-danger field-validation-error" data-valmsg-for="Pais" data-valmsg-replace="true">
                                {{ errorBag.get("Pais") }}
                            </span>
                        </div>
                        <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Provincia"] }}</label>
                            <province-select :enabled="empresaDto.paisId != null" v-model.number="empresaDto.provinciaId"
                            :paisId="empresaDto.paisId" />
                            <span class="text-danger field-validation-error" data-valmsg-for="Provincia" data-valmsg-replace="true">
                                {{ errorBag.get("Provincia") }}
                            </span>
                        </div>
                        <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Ciudad"] }}</label>
                            <city-select :enabled="empresaDto.provinciaId != null && empresaDto.paisId != null"
                            v-model.number="empresaDto.ciudadId"
                            :provinciaId="empresaDto.provinciaId" />
                            <span class="text-danger field-validation-error" data-valmsg-for="Ciudad" data-valmsg-replace="true">
                                {{ errorBag.get("Ciudad") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Ciudad Descripción"] }}</label>
                            <input maxlength="1000" type="text" class="form-control" v-model="empresaDto.ciudadDescripcion">
                            <span class="text-danger field-validation-error" data-valmsg-for="CiudadDescripcion" data-valmsg-replace="true">
                                {{ errorBag.get("CiudadDescripcion") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Teléfono Principal"] }}</label><span class="text-danger">*</span>
                            <input maxlength="25" type="text" class="form-control" v-model="empresaDto.telefonoPrincipal">
                            <span class="text-danger field-validation-error" data-valmsg-for="TelefonoPrincipal" data-valmsg-replace="true">
                                {{ errorBag.get("TelefonoPrincipal") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Teléfono Alternativo"] }}</label>
                            <input maxlength="25" type="text" class="form-control" v-model="empresaDto.telefonoAlternativo">
                            <span class="text-danger field-validation-error" data-valmsg-for="TelefonoAlternativo" data-valmsg-replace="true">
                                {{ errorBag.get("TelefonoAlternativo") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Email Principal"] }}</label><span class="text-danger">*</span>
                            <input maxlength="150" type="text" class="form-control" v-model="empresaDto.emailPrincipal">
                            <span class="text-danger field-validation-error" data-valmsg-for="EmailPrincipal" data-valmsg-replace="true">
                                {{ errorBag.get("EmailPrincipal") }}
                            </span>
                        </div>

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Email Alternativo"] }}</label>
                            <input maxlength="150" type="text" class="form-control" v-model="empresaDto.emailAlternativo">
                            <span class="text-danger field-validation-error" data-valmsg-for="EmailAlternativo" data-valmsg-replace="true">
                                {{ errorBag.get("EmailAlternativo") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Contacto"] }}</label><span class="text-danger">*</span>
                            <input maxlength="30" type="text" class="form-control" v-model="empresaDto.contacto">
                            <span class="text-danger field-validation-error" data-valmsg-for="Contacto" data-valmsg-replace="true">
                                {{ errorBag.get("Contacto") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Contacto Alternativo"] }}</label>
                            <input maxlength="30" type="text" class="form-control" v-model="empresaDto.contactoAlternativo">
                            <span class="text-danger field-validation-error" data-valmsg-for="ContactoAlternativo" data-valmsg-replace="true">
                                {{ errorBag.get("ContactoAlternativo") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Tipo Responsable"] }}</label>
                            <categoriasTipos-select v-model.number="empresaDto.tipoResponsableId" />
                            <span class="text-danger field-validation-error" data-valmsg-for="TipoResponsable" data-valmsg-replace="true">
                                {{ errorBag.get("TipoResponsable") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{ loc["Confirmado"] }}</label>
                            <div>
                                <input type="checkbox" class="form-check-input" v-model="empresaDto.confirmado">
                            </div>
                            <span class="text-danger field-validation-error" data-valmsg-for="Confirmado" data-valmsg-replace="true">
                                {{ errorBag.get("Confirmado") }}
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12 d-flex justify-content-end mb-3 mt-3">
            <accept-button :disabled="!grants.createEmpresa" @click="createAsync" style="margin-right: 10px;">
                {{ loc["Aceptar"] }}
            </accept-button>
            <cancel-button @click="cancel">{{ loc["Cancelar"] }}</cancel-button>
        </div>
    </div>
</template>

<script>
import AcceptButton from "@/components/forms/accept-button.vue";

import CancelButton from "@/components/forms/cancel-button.vue";
import UiService from "@/common/uiService";

import EmpresaCreateDto from './EmpresaCreateDto' // Modificar por la clase dto correspondiente

import commonMixin from '@/Common/Mixins/commonMixin';

import citySelect from "@/Components/Global/Cities/city-select";
import provinceSelect from "@/Components/Global/Provinces/province-select";
import countrySelect from "@/Components/Global/Countries/country-select";

import categoriasTiposSelect from "@/Components/Empresas/categoriasTipos-select";
import tiposCuentasSelect from "@/Components/Empresas/tiposCuentas-select";
import monedasEmpresaSelect from "@/Components/Empresas/monedasEmpresa-select";

import EmpresaMoneda from "./EmpresaMoneda"

import empresaMonedaForm from './empresaMoneda-form'

import multiselect from "vue-multiselect";

import inlineEdit from "@/components/forms/inline-edit-button.vue";
import inlineDelete from "@/components/forms/inline-delete-button.vue";
import inlineCancel from "@/components/forms/inline-cancel-button.vue";

const AFIRMATIVO = true;
const NEGATIVO = false;

const NO_DATA_MESSAGE = loc["No hay datos"];

import loc from "@/common/commonLoc.js"

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
    name: "empresas-create", // Modificar

    data: function () {
        return {
            loc, 

            empresaDto: new EmpresaCreateDto(),
            uiService: new UiService(),
            AFIRMATIVO,
            NEGATIVO,

            listaRoles: [],
            dataListaRolesAgregadas : [],
            primerCargaDeRoles: false,

            listaAlicuotas: [],
            dataListaAlicuotasAgregadas : [],
            primerCargaDeAlicuotas: false,

            listaDocumentosCompras: [],
            dataListaDocumentosComprasAgregadas : [],
            primerCargaDeDocumentosCompras: false,

            listaConceptosGastos: [],
            dataListaConceptosGastosAgregadas : [],
            primerCargaDeConceptosGastos: false,

            NO_DATA_MESSAGE,
            idTableEmpresaMonedas: `__empresaMonedas`,
            empresaMonedasKey: 0,
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
        listaDocumentosComprasAgregados: {
            get() {
                return this.dataListaDocumentosComprasAgregadas;
            },
            set(values) {
                this.dataListaDocumentosComprasAgregadas = values;
                this.empresaDto.ordenesComprasTiposId = values.map(elemento => elemento.id);
                this.$emit("input", values);
            }
        },
        listaConceptosGastosAgregados: {
            get() {
                return this.dataListaConceptosGastosAgregadas;
            },
            set(values) {
                this.dataListaConceptosGastosAgregadas = values;
                this.empresaDto.conceptosGastosTiposId = values.map(elemento => elemento.id);
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
        this.loadDocumentosCompras();
        this.loadConceptosGastos();
    },
    methods: {
        addNuevaMoneda(){
            this.errorBag.clear();
            let nuevoItem = new EmpresaMoneda();
            nuevoItem.mode.setCreate();

            this.empresaDto.monedas.push(nuevoItem);
        },
        updateMoneda(i, index){
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
        async loadDocumentosCompras() {
            if (this.primerCargaDeDocumentosCompras) {
                return;
            }
            this.listaDocumentosCompras = await this.$store.dispatch("getDocumentosComprasListAsync");
            this.primerCargaDeDocumentosCompras = true;
        },
        async loadConceptosGastos() {
            if (this.primerCargaDeConceptosGastos) {
                return;
            }
            this.listaConceptosGastos = await this.$store.dispatch("getConceptosGastosListAsync");
            this.primerCargaDeConceptosGastos = true;
        },
        async init() {
            // Si no hay permisos de alta, volvemos a la lista
            this.errorBag.clear();
            if (!this.grants.createEmpresa) this.$router.push({ name: "home" });
        },
        cancel() {
            this.errorBag.clear();
            this.$router.push({ name: "home" });
        },
        goDetail(id) {
            this.errorBag.clear();
            this.$router.push({ name: "detail", params: { id: id, create: true }});
        },
        async createAsync() {
            this.errorBag.clear();
            if(this.validacionDatosObligatorios()){
                this.uiService.showSpinner(true)
                this.empresaDto.monedas = this.empresaDto.monedas.filter(moneda => !moneda.deleted)
                await this.$store.dispatch("postAsync", this.empresaDto)
                .then((id) => {
                    if (!this.errorBag.hasErrors()) {
                        this.uiService.showMessageSuccess(loc["Operación confirmada"])
                        this.goDetail(id);
                    } else {
                        this.uiService.showMessageError(loc["Operación rechazada"])
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
                this.errorBag.addError("identificadorTributario", [loc["El valor ingresado debe ser numerico"]]);
            }
            if (isNaN(this.empresaDto.telefonoPrincipal)) {
                validacion = false
                this.errorBag.addError("telefonoPrincipal", [loc["El valor ingresado debe ser numerico"]]);
            }
            if (isNaN(this.empresaDto.telefonoAlternativo)) {
                validacion = false
                this.errorBag.addError("telefonoAlternativo", [loc["El valor ingresado debe ser numerico"]]);
            }
            return validacion
        },
        validacionEmails(){
            var validacion = true
            const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            if(!regex.test(this.empresaDto.emailPrincipal) && (this.empresaDto.emailPrincipal != '' && this.empresaDto.emailPrincipal != "" && this.empresaDto.emailPrincipal != null)){
                validacion = false
                this.errorBag.addError("emailPrincipal", [loc["El formato de Email es incorrecto"]]);
            }
            if(!regex.test(this.empresaDto.emailAlternativo) && (this.empresaDto.emailAlternativo != '' && this.empresaDto.emailAlternativo != "" && this.empresaDto.emailAlternativo != null)){
                validacion = false
                this.errorBag.addError("emailAlternativo", [loc["El formato de Email es incorrecto"]]);
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
                this.errorBag.addError("rolesIdm", [loc["Se debe agregar un rol como minimo"]]);
            }
            if(this.empresaDto.ordenesComprasTiposId.length === 0 && this.listaDocumentosCompras.length !== 0){
                validacion = false;
                this.errorBag.addError("ordenesComprasTiposId", [loc["Se debe agregar un Documento como minimo"]]);
            }
            if(this.empresaDto.conceptosGastosTiposId.length === 0 && this.listaConceptosGastos.length !== 0){
                validacion = false;
                this.errorBag.addError("conceptosGastosTiposId", [loc["Se debe agregar un Concepto como minimo"]]);
            }
            if(this.empresaDto.codigoProveedor == '' || this.empresaDto.codigoProveedor == "" || this.empresaDto.codigoProveedor == null){
                validacion = false;
                this.errorBag.addError("codigoProveedor", [loc["El campo 'Código Proveedor' es obligatorio"]]);
            }
            if(this.empresaDto.razonSocial == '' || this.empresaDto.razonSocial == "" ||this.empresaDto.razonSocial == null){
                validacion = false;
                this.errorBag.addError("razonSocial", [loc["El campo 'Razón Social' es obligatorio"]]);
            }
            if(this.empresaDto.nombreFantasia == '' || this.empresaDto.nombreFantasia == "" ||this.empresaDto.nombreFantasia == null){
                validacion = false;
                this.errorBag.addError("nombreFantasia", [loc["El campo 'Nombre Fantasía' es obligatorio"]]);
            }
            if(this.empresaDto.identificadorTributario == '' || this.empresaDto.identificadorTributario == "" ||this.empresaDto.identificadorTributario == null){
                validacion = false;
                this.errorBag.addError("identificadorTributario", [loc["El campo 'Identificador Tributario' es obligatorio"]]);
            }
            if(this.empresaDto.granContribuyente == null){
                validacion = false;
                this.errorBag.addError("granContribuyente", [loc["El campo 'Tipo de Socio' es obligatorio"]]);
            }
            if(this.empresaDto.telefonoPrincipal == '' || this.empresaDto.telefonoPrincipal == "" ||this.empresaDto.telefonoPrincipal == null){
                validacion = false;
                this.errorBag.addError("telefonoPrincipal", [loc["El campo 'Teléfono Principal' es obligatorio"]]);
            }
            if(this.empresaDto.emailPrincipal == '' || this.empresaDto.emailPrincipal == "" ||this.empresaDto.emailPrincipal == null){
                validacion = false;
                this.errorBag.addError("emailPrincipal", [loc["El campo 'Email Principal' es obligatorio"]]);
            }
            if(this.empresaDto.contacto == '' || this.empresaDto.contacto == "" ||this.empresaDto.contacto == null){
                validacion = false;
                this.errorBag.addError("contacto", [loc["El campo 'Contacto' es obligatorio"]]);
            }
                
            return validacion
        }
    },
    watch: {
        'empresaDto.paisId': function(){
            this.empresaDto.provinciaId = null;
            this.empresaDto.ciudadId = null;
        },
        'empresaDto.provinciaId': function(){
            this.empresaDto.ciudadId = null;
        }
    }
};
</script>


</script>
<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>
<style>

.multiselect__tag {
    background-color: var(--bs-primary);
    font-size: 01rem;
    color: white;
    border-radius: 50rem;
    padding: 0.2rem;
    padding-left: 0.8rem;
}

.multiselect__tags {
    border: 1px solid #ced4da;
}

.multiselect__tag-icon{
    position: relative !important;
    display: inline-block;
    margin-right: 5px;
}

.multiselect__tag-icon::after{
    transform: scale(1.8);
    font-weight: bold;
    color:white !important;
}
.multiselect__tag-icon:hover{
    transform: scale(1.5);
    transition: 0.1s;
    background: transparent;
    border-radius: 50rem;
}

.multiselect__input{
    width: 100%
}

.multiselect__tags {
    border-radius: 5px 0px 0px 5px;
}

.multiselect__option--disabled {
  background: purple;
  color: rgba(var(--bs-primary), 1);
  font-style: italic;
}

.multiselect__option--highlight {
  background: var(--bs-primary);
  color: white;
  font-size: 1rem;
  height: 1rem;
}

.multiselect__content {
  background: white;
  color: #444;
  font-size: 0.9rem;
}

.multiselect--disabled {
    opacity: 0.8;
}

.multiselect--disabled .multiselect__tag-icon::after {
    content: "";
}

</style>