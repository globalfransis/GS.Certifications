<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">Alta de Percepción</p> <!-- Por ejemplo: Alta de Usuario -->
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <!-- Agregar campos del formulario de alta -->
                        <!-- Este es un ejemplo -->
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Descripción</label><span class="text-danger">*</span>
                            <input maxlength="500" type="text" class="form-control" v-model="percepcionCreateDto.descripcion">
                            <span class="text-danger field-validation-error" data-valmsg-for="Descripcion" data-valmsg-replace="true">
                                {{ errorBag.get("Descripcion") }}
                            </span>
                        </div>
                        <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Provincia</label>
                            <province-select v-model.number="percepcionCreateDto.provinciaId" :paisId="1"/>
                            <span class="text-danger field-validation-error" data-valmsg-for="Provincia" data-valmsg-replace="true">
                                {{ errorBag.get("Provincia") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Tipo</label><span class="text-danger">*</span>
                            <tipoPercepcion-select v-model="percepcionCreateDto.percepcionTipoId"/>
                            <span class="text-danger field-validation-error" data-valmsg-for="TipoPercepcion" data-valmsg-replace="true">
                                {{ errorBag.get("TipoPercepcion") }}
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

import PercepcionCreateDto from './PercepcionCreateDto' // Modificar por la clase dto correspondiente

import commonMixin from '@/Common/Mixins/commonMixin';

import provinceSelect from "@/Components/Global/Provinces/province-select";
import tipoPercepcionSelect from "@/Components/Percepciones/tipoPercepcion-select"

export default {
    components: {
        AcceptButton,
        CancelButton,
        tipoPercepcionSelect,
        provinceSelect,
    },
    mixins: [commonMixin],
    name: "percepciones-create", // Modificar

    data: function () {
        return {
            percepcionCreateDto: new PercepcionCreateDto(),
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
                await this.$store.dispatch("postAsync", this.percepcionCreateDto)
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
            if(this.percepcionCreateDto.descripcion == null ||
             this.percepcionCreateDto.descripcion == '' ||
              this.percepcionCreateDto.descripcion == ""){
                validacion = false;
                this.errorBag.addError("descripcion", ["El campo 'Descripción' es obligatorio"]);
            }
            if(this.percepcionCreateDto.percepcionTipoId == null){
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