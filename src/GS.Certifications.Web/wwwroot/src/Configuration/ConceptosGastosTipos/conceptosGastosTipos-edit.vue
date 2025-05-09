<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">Edición de Concepto de Gasto {{ this.conceptoGastoTipoDto.nombre }}</p> <!-- Por ejemplo: Alta de Usuario -->
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
import ConceptoGastoTipoDto from './ConceptoGastoTipoDto';

import commonMixin from '@/Common/Mixins/commonMixin';

export default {
    components: {
        AcceptButton,
        CancelButton,
    },
    mixins: [commonMixin],
    name: "conceptosGastosTipos-edit",
    data: function () {
        return {
            conceptoGastoTipoDto: new ConceptoGastoTipoDto(),
            uiService: new UiService(),
        };
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
                    this.conceptoGastoTipoDto = new ConceptoGastoTipoDto(res);
                    this.obtenerValoresCaracteristicas();
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        goDetail() {
            this.errorBag.clear();
            this.$router.push({ name: "detail", params: { id: this.conceptoGastoTipoDto.id } });
        },
        goBack() {
            this.errorBag.clear();
            this.$router.push({ name: "detail", params: { id: this.conceptoGastoTipoDto.id } });
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
                await this.$store.dispatch("putAsync", this.conceptoGastoTipoDto)
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