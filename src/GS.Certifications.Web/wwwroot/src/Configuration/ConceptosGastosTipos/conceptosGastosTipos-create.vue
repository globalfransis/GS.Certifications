<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">Alta del Concepto de Gasto</p> <!-- Por ejemplo: Alta de Usuario -->
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Nombre</label><span class="text-danger">*</span>
                            <input maxlength="100" type="text" class="form-control" v-model="conceptoGastoTipoDto.nombre">
                            <span class="text-danger field-validation-error" data-valmsg-for="Nombre" data-valmsg-replace="true">
                                {{ errorBag.get("Nombre") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Descripción</label><span class="text-danger">*</span>
                            <input maxlength="255" type="text" class="form-control" v-model="conceptoGastoTipoDto.descripcion">
                            <span class="text-danger field-validation-error" data-valmsg-for="Descripcion" data-valmsg-replace="true">
                                {{ errorBag.get("Descripcion") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Nombre de Concepto Contable</label>
                            <input maxlength="100" type="text" class="form-control" v-model="conceptoGastoTipoDto.conceptoContableNombre">
                            <span class="text-danger field-validation-error" data-valmsg-for="ConceptoContableNombre" data-valmsg-replace="true">
                                {{ errorBag.get("ConceptoContableNombre") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Valor de Concepto Contable</label>
                            <input maxlength="255" type="text" class="form-control" v-model="conceptoGastoTipoDto.conceptoContableValor">
                            <span class="text-danger field-validation-error" data-valmsg-for="ConceptoContableValor" data-valmsg-replace="true">
                                {{ errorBag.get("ConceptoContableValor") }}
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

import ConceptoGastoTipoDto from './ConceptoGastoTipoDto' // Modificar por la clase dto correspondiente

import commonMixin from '@/Common/Mixins/commonMixin';

export default {
    components: {
        AcceptButton,
        CancelButton,
    },
    mixins: [commonMixin],
    name: "conceptosGastosTipos-create", // Modificar

    data: function () {
        return {
            uiService: new UiService(),
            conceptoGastoTipoDto: new ConceptoGastoTipoDto(),
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
                await this.$store.dispatch("postAsync", this.conceptoGastoTipoDto)
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
            if(this.conceptoGastoTipoDto.nombre == null ||
             this.conceptoGastoTipoDto.nombre == '' ||
              this.conceptoGastoTipoDto.nombre == ""){
                validacion = false;
                this.errorBag.addError("Nombre", ["El campo 'Nombre' es obligatorio"]);
            }
            if(this.conceptoGastoTipoDto.descripcion == null ||
             this.conceptoGastoTipoDto.descripcion == '' ||
              this.conceptoGastoTipoDto.descripcion == ""){
                validacion = false;
                this.errorBag.addError("Descripcion", ["El campo 'Descripcion' es obligatorio"]);
            }
            return validacion;
        },
    },
    watch: {
    }
};
</script>