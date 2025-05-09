<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5" style="overflow-wrap: break-word;">Detalle del Impuesto {{ this.impuestoEditDto.nombre }}</p> <!-- Agregar un título, por ejemplo: Detalle del Usuario {userId} -->
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <!-- Agregar campos del formulario de alta -->
                        <!-- Este es un ejemplo -->
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Nombre</label><span class="text-danger">*</span>
                            <input maxlength="250" type="text" class="form-control" v-model="impuestoEditDto.nombre">
                            <span class="text-danger field-validation-error" data-valmsg-for="Nombre" data-valmsg-replace="true">
                                {{ errorBag.get("Nombre") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Descripción</label><span class="text-danger">*</span>
                            <input maxlength="500" type="text" class="form-control" v-model="impuestoEditDto.descripcion">
                            <span class="text-danger field-validation-error" data-valmsg-for="Descripcion" data-valmsg-replace="true">
                                {{ errorBag.get("Descripcion") }}
                            </span>
                        </div>
                        <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Provincia</label>
                            <province-select v-model.number="impuestoEditDto.provinciaId" :paisId="1"/>
                            <span class="text-danger field-validation-error" data-valmsg-for="Provincia" data-valmsg-replace="true">
                                {{ errorBag.get("Provincia") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Tipo</label><span class="text-danger">*</span>
                            <tipoImpuesto-select v-model="impuestoEditDto.tipoId"/>
                            <span class="text-danger field-validation-error" data-valmsg-for="TipoImpuesto" data-valmsg-replace="true">
                                {{ errorBag.get("TipoImpuesto") }}
                            </span>
                        </div>

                        <div class="col-lg-3 col-sm-12 mb-4" v-if="impuestoEditDto.tipoId === 1">
                            <label class="control-label">Alícuota</label><span class="text-danger">*</span>
                            <alicuota-select v-model.number="impuestoEditDto.alicuotaId" />
                            <span class="text-danger field-validation-error" data-valmsg-for="Alicuota" data-valmsg-replace="true">
                                {{ errorBag.get("Alicuota") }}
                            </span>
                        </div>

                        <div class="form-group col-lg-3 col-sm-12 mb-4" v-else>
                            <label class="control-label">Valor</label><span class="text-danger">*</span>
                            <input type="number" class="form-control" v-model="impuestoEditDto.valor">
                            <span class="text-danger field-validation-error" data-valmsg-for="Valor" data-valmsg-replace="true">
                                {{ errorBag.get("Valor") }}
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12 d-flex justify-content-end mb-3 mt-3">
            <accept-button :disabled="!grants.update" @click="updateAsync" style="margin-right: 10px;">
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
import ImpuestoEditDto from './ImpuestoEditDto';
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
    name: "impuestos-edit",
    data: function () {
        return {
            impuestoEditDto: new ImpuestoEditDto(),
            uiService: new UiService(),
            rolElegido: null,
            mensajeRolActual: null,
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
            if (!this.grants.update) this.$router.push({ name: "home" });
            else {
                if (this.$route.params.id) await this.getAsync(this.$route.params.id);
                else this.goBack();
            }
        },
        async getAsync(id) {
            this.errorBag.clear();
            this.uiService.showSpinner(true);
            await this.$store.dispatch("getAsync", id)
                .then((res) => {
                    this.impuestoEditDto = new ImpuestoEditDto(res);
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        goDetail() {
            this.errorBag.clear();
            this.$router.push({ name: "detail", params: { id: this.impuestoEditDto.id } });
        },
        goBack() {
            this.errorBag.clear();
            this.$router.push({ name: "detail", params: { id: this.impuestoEditDto.id } });
        },
        cancel() {
            this.errorBag.clear();
            if(this.$route.query.desdeDetalle){
                this.goBack();
            } else {
                this.$router.push({ name: "home", query: { fromDetail: true } });
            }
        },
        async updateAsync() {
            this.errorBag.clear();
            if(this.validacionDeDatos()){
                this.uiService.showSpinner(true);
                await this.$store.dispatch("putAsync", this.impuestoEditDto)
                    .then(() => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada");
                            this.goDetail();
                        } else {
                            this.uiService.showMessageError("Operación rechazada");
                        }
                    })
                    .finally(() => {
                        this.uiService.showSpinner(false);
                    });
            }
        },
        validacionDeDatos(){
            var validacion = true;
            if(this.impuestoEditDto.nombre == null ||
             this.impuestoEditDto.nombre == '' ||
              this.impuestoEditDto.nombre == ""){
                validacion = false;
                this.errorBag.addError("nombre", ["El campo 'Nombre' es obligatorio"]);
            }
            if(this.impuestoEditDto.descripcion == null ||
             this.impuestoEditDto.descripcion == '' ||
              this.impuestoEditDto.descripcion == ""){
                validacion = false;
                this.errorBag.addError("descripcion", ["El campo 'Descripción' es obligatorio"]);
            }
            if(this.impuestoEditDto.tipoId == null){
                validacion = false;
                this.errorBag.addError("TipoImpuesto", ["El campo 'Tipo' es obligatorio"]);
            }
            if(this.impuestoEditDto.alicuotaId == null && this.impuestoEditDto.tipoId == 1){
                validacion = false;
                this.errorBag.addError("Alicuota", ["El campo 'Alicuota' es obligatorio"]);
            }
            if((this.impuestoEditDto.valor == null || this.impuestoEditDto.valor == 0) && this.impuestoEditDto.tipoId != 1){
                validacion = false;
                this.errorBag.addError("valor", ["El campo 'Valor' es obligatorio"]);
            } else if (isNaN(this.impuestoEditDto.valor)) {
                validacion = false
                this.errorBag.addError("valor", ["El valor ingresado debe ser numerico"]);
            }

            return validacion;
        }
    },
    watch: {
        'impuestoEditDto.tipoId'(newVal, oldVal) {
            if (newVal === 1) {
                this.impuestoEditDto.valor = null;
            } else {
                this.impuestoEditDto.alicuotaId = null;
            }
        },
        'impuestoEditDto.valor': function(newValue) {
            const isValidNumber = /^-?\d+([.,]\d+)?$/.test(newValue);

            if (!isValidNumber || newValue < 0) {
                this.impuestoEditDto.valor = 0;
            } else if (newValue > 100) {
                this.impuestoEditDto.valor = 100;
            } else {
                // Reemplazar coma por punto para convertir a número
                const numericValue = Number(newValue.toString().replace(',', '.'));
                
                // Verificar si tiene más de dos decimales
                const decimalPart = newValue.toString().split(/[.,]/)[1];
                if (decimalPart && decimalPart.length > 2) {
                    this.$nextTick(() => {
                        this.impuestoEditDto.valor = newValue.slice(0, newValue.length - 1);
                    });
                    return;
                }
                
                this.impuestoEditDto.valor = numericValue;
            }
        }
    }
};
</script>