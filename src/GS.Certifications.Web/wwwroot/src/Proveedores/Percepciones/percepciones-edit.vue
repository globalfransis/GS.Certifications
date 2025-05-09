<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">Edición de Percepción</p> <!-- Por ejemplo: Alta de Usuario -->
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <!-- Agregar campos del formulario de alta -->
                        <!-- Este es un ejemplo -->
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Descripción</label><span class="text-danger">*</span>
                            <input maxlength="500" type="text" class="form-control" v-model="percepcionEditDto.descripcion">
                            <span class="text-danger field-validation-error" data-valmsg-for="Descripcion" data-valmsg-replace="true">
                                {{ errorBag.get("Descripcion") }}
                            </span>
                        </div>
                        <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Provincia</label>
                            <province-select v-model.number="percepcionEditDto.provinciaId" :paisId="1"/>
                            <span class="text-danger field-validation-error" data-valmsg-for="Provincia" data-valmsg-replace="true">
                                {{ errorBag.get("Provincia") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Tipo</label><span class="text-danger">*</span>
                            <tipoPercepcion-select v-model="percepcionEditDto.percepcionTipoId"/>
                            <span class="text-danger field-validation-error" data-valmsg-for="TipoPercepcion" data-valmsg-replace="true">
                                {{ errorBag.get("TipoPercepcion") }}
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
import PercepcionEditDto from './PercepcionEditDto';
import commonMixin from '@/Common/Mixins/commonMixin';

import provinceSelect from "@/Components/Global/Provinces/province-select";
import tipoPercepcionSelect from "@/Components/Percepciones/tipoPercepcion-select"
import alicuotaSelect from "@/Components/Alicuotas/alicuota-select"

export default {
    components: {
        AcceptButton,
        CancelButton,
        tipoPercepcionSelect,
        provinceSelect,
        alicuotaSelect,
    },
    mixins: [commonMixin],
    name: "percepciones-edit",
    data: function () {
        return {
            percepcionEditDto: new PercepcionEditDto(),
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
                    this.percepcionEditDto = new PercepcionEditDto(res);
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        goDetail() {
            this.errorBag.clear();
            this.$router.push({ name: "detail", params: { id: this.percepcionEditDto.id } });
        },
        goBack() {
            this.errorBag.clear();
            this.$router.push({ name: "detail", params: { id: this.percepcionEditDto.id } });
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
                await this.$store.dispatch("putAsync", this.percepcionEditDto)
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
            if(this.percepcionEditDto.descripcion == null ||
             this.percepcionEditDto.descripcion == '' ||
              this.percepcionEditDto.descripcion == ""){
                validacion = false;
                this.errorBag.addError("descripcion", ["El campo 'Descripción' es obligatorio"]);
            }
            if(this.percepcionEditDto.percepcionTipoId == null){
                validacion = false;
                this.errorBag.addError("TipoPercepcion", ["El campo 'Tipo' es obligatorio"]);
            }

            return validacion;
        }
    },
    watch: {
    }
};
</script>