<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">Detalle del Concepto de Gasto {{ this.conceptoGastoTipoDto.nombre }}</p> <!-- Agregar un título, por ejemplo: Detalle del Usuario {userId} -->
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row form-container">
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Nombre</label>
                            <input type="text" class="form-control" :value="conceptoGastoTipoDto.nombre" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Descripción</label>
                            <input type="text" class="form-control" :value="conceptoGastoTipoDto.descripcion" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Nombre de Concepto Contable</label>
                            <input type="text" class="form-control" :value="conceptoGastoTipoDto.conceptoContableNombre" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Valor de Concepto Contable</label>
                            <input type="text" class="form-control" :value="conceptoGastoTipoDto.conceptoContableValor" readonly>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12 d-flex justify-content-end mb-3 mt-3">
            <button :disabled="!grants.update" class="btn btn-primary btn-sm" @click="update" style="margin-right: 10px;">
                Editar
            </button>
            <button :disabled="!grants.delete" class="btn btn-danger btn-sm" @click="remove" style="margin-right: 10px;">
                Eliminar
            </button>
            <cancel-button @click="goBack">Volver</cancel-button>
        </div>
    </div>
</template>

<script>
import CancelButton from "@/components/forms/cancel-button.vue";
import UiService from "@/common/uiService";

import ConceptoGastoTipoDto from './ConceptoGastoTipoDto' // Modificar por la clase dto correspondiente

import commonMixin from '@/Common/Mixins/commonMixin';
import detailMixin from '@/Common/Mixins/detailMixin';

export default {
    components: {
        CancelButton
    },
    mixins: [commonMixin, detailMixin],
    name: "conceptosGastosTipos-detail",

    data: function () {
        return {
            conceptoGastoTipoDto: new ConceptoGastoTipoDto(), // Modificar por la clase dto correspondiente
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
            if (this.$route.params.id) await this.getAsync(this.$route.params.id);
            else this.goBack();
        },
        async getAsync(id) {
            this.uiService.showSpinner(true);
            await this.$store.dispatch("getAsync", id)
                .then((res) => {
                    this.conceptoGastoTipoDto = new ConceptoGastoTipoDto(res);
                    this.asignarValoresCaracteristicas();
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        seeDetail() {
            // Limpiamos la lista antes de navegar
            this.$store.dispatch("clearList");
            this.$router.push({ name: "detail", params: { id: this.conceptoGastoTipoDto.id } });
        },
        async remove() {
            if (
                await this.uiService.confirmActionModal(
                "¿Está usted seguro que desea eliminar a este tipo de Documento de Compras?",
                "Aceptar",
                "Cancelar"
                )
            ){
                this.uiService.showSpinner(true)
                await this.$store.dispatch("deleteAsync", this.conceptoGastoTipoDto)
                    .then(async () => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            this.goBack();
                        } else {
                            if(this.errorBag.errors.hasOwnProperty('ordenCompra')){
                                this.uiService.showMessageError("Operacion rechazada: Existe un Documento de Compras con el tipo asignado")
                            } else { 
                                this.uiService.showMessageError("Operación rechazada") 
                            }
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
            this.$router.push({ name: "edit", query: {desdeDetalle: true}, params: { id: this.conceptoGastoTipoDto.id } });
        },
    },
};
</script>