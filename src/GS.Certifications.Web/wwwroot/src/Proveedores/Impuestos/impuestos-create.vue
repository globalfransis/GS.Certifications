<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">Alta de Impuesto</p> <!-- Por ejemplo: Alta de Usuario -->
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <!-- Agregar campos del formulario de alta -->
                        <!-- Este es un ejemplo -->
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Nombre</label><span class="text-danger">*</span>
                            <input maxlength="250" type="text" class="form-control" v-model="impuestoCreateDto.nombre">
                            <span class="text-danger field-validation-error" data-valmsg-for="Nombre" data-valmsg-replace="true">
                                {{ errorBag.get("Nombre") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Descripción</label><span class="text-danger">*</span>
                            <input maxlength="500" type="text" class="form-control" v-model="impuestoCreateDto.descripcion">
                            <span class="text-danger field-validation-error" data-valmsg-for="Descripcion" data-valmsg-replace="true">
                                {{ errorBag.get("Descripcion") }}
                            </span>
                        </div>
                        <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Provincia</label>
                            <province-select v-model.number="impuestoCreateDto.provinciaId" :paisId="1"/>
                            <span class="text-danger field-validation-error" data-valmsg-for="Provincia" data-valmsg-replace="true">
                                {{ errorBag.get("Provincia") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Tipo</label><span class="text-danger">*</span>
                            <tipoImpuesto-select v-model="impuestoCreateDto.tipoId"/>
                            <span class="text-danger field-validation-error" data-valmsg-for="TipoImpuesto" data-valmsg-replace="true">
                                {{ errorBag.get("TipoImpuesto") }}
                            </span>
                        </div>

                        <div class="col-lg-3 col-sm-12 mb-4" v-if="impuestoCreateDto.tipoId === 1">
                            <label class="control-label">Alícuota</label><span class="text-danger">*</span>
                            <alicuota-select v-model.number="impuestoCreateDto.alicuotaId" />
                            <span class="text-danger field-validation-error" data-valmsg-for="Alicuota" data-valmsg-replace="true">
                                {{ errorBag.get("Alicuota") }}
                            </span>
                        </div>

                        <div class="form-group col-lg-3 col-sm-12 mb-4" v-else>
                            <label class="control-label">Valor</label><span class="text-danger">*</span>
                            <input type="number" class="form-control" v-model="impuestoCreateDto.valor">
                            <span class="text-danger field-validation-error" data-valmsg-for="Valor" data-valmsg-replace="true">
                                {{ errorBag.get("Valor") }}
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12 d-flex justify-content-end mb-3 mt-3">
            <accept-button :disabled="!grants.create" @click="createAsync" style="margin-right: 10px;">
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

import ImpuestoCreateDto from './ImpuestoCreateDto' // Modificar por la clase dto correspondiente

import commonMixin from '@/Common/Mixins/commonMixin';

import provinceSelect from "@/Components/Global/Provinces/province-select";
import tipoImpuestoSelect from "@/Components/Impuestos/tipoImpuesto-select"
import alicuotaSelect from "@/Components/Alicuotas/alicuota-select"

export default {
    components: {
        AcceptButton,
        CancelButton,
        tipoImpuestoSelect,
        provinceSelect,
        alicuotaSelect,
    },
    mixins: [commonMixin],
    name: "impuestos-create", // Modificar

    data: function () {
        return {
            impuestoCreateDto: new ImpuestoCreateDto(),
            uiService: new UiService(),
            rolElegido: null,
            listaRoles: []
        };
    },
    computed: {
        grants() {
            return this.$store.getters.getGrants;
        },
        errorBag() {
            return this.$store.getters.getErrorBag;
        }
    },
    async mounted() {
        await this.init();
    },
    methods: {
        async init() {
            this.errorBag.clear();
            // Si no hay permisos de alta, volvemos a la lista
            if (!this.grants.create) this.$router.push({ name: "home" });
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
            if(this.validacionDeDatos()){
                this.uiService.showSpinner(true)
                await this.$store.dispatch("postAsync", this.impuestoCreateDto)
                    .then((id) => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            this.goDetail(id);
                        } else {
                            this.uiService.showMessageError("Operación rechazada")
                        }
                    })
                    .finally(() => {
                        this.uiService.showSpinner(false);
                    });
            }
        },
        validacionDeDatos(){
            var validacion = true;
            if(this.impuestoCreateDto.nombre == null ||
             this.impuestoCreateDto.nombre == '' ||
              this.impuestoCreateDto.nombre == ""){
                validacion = false;
                this.errorBag.addError("nombre", ["El campo 'Nombre' es obligatorio"]);
            }
            if(this.impuestoCreateDto.descripcion == null ||
             this.impuestoCreateDto.descripcion == '' ||
              this.impuestoCreateDto.descripcion == ""){
                validacion = false;
                this.errorBag.addError("descripcion", ["El campo 'Descripción' es obligatorio"]);
            }
            if(this.impuestoCreateDto.tipoId == null){
                validacion = false;
                this.errorBag.addError("TipoImpuesto", ["El campo 'Tipo' es obligatorio"]);
            }
            if(this.impuestoCreateDto.alicuotaId == null && this.impuestoCreateDto.tipoId == 1){
                validacion = false;
                this.errorBag.addError("Alicuota", ["El campo 'Alicuota' es obligatorio"]);
            }
            if((this.impuestoCreateDto.valor == null || this.impuestoCreateDto.valor == 0) && this.impuestoCreateDto.tipoId != 1){
                validacion = false;
                this.errorBag.addError("valor", ["El campo 'Valor' es obligatorio"]);
            } else if (isNaN(this.impuestoCreateDto.valor)) {
                validacion = false
                this.errorBag.addError("valor", ["El valor ingresado debe ser numerico"]);
            }
            return validacion;
        }
    },
    watch: {
        'impuestoCreateDto.tipoId'(newVal, oldVal) {
            if (newVal === 1) {
                this.impuestoCreateDto.valor = null;
            } else {
                this.impuestoCreateDto.alicuotaId = null;
            }
        },
        'impuestoCreateDto.valor': function(newValue) {
            const isValidNumber = /^-?\d+([.,]\d+)?$/.test(newValue);

            if (!isValidNumber || newValue < 0) {
                this.impuestoCreateDto.valor = 0;
            } else if (newValue > 100) {
                this.impuestoCreateDto.valor = 100;
            } else {
                // Reemplazar coma por punto para convertir a número
                const numericValue = Number(newValue.toString().replace(',', '.'));
                
                // Verificar si tiene más de dos decimales
                const decimalPart = newValue.toString().split(/[.,]/)[1];
                if (decimalPart && decimalPart.length > 2) {
                    this.$nextTick(() => {
                        this.impuestoCreateDto.valor = newValue.slice(0, newValue.length - 1);
                    });
                    return;
                }
                
                this.impuestoCreateDto.valor = numericValue;
            }
        }
    }
};
</script>