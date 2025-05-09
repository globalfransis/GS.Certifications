<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5" style="overflow-wrap: break-word;">Detalle del Impuesto {{ this.impuestoDto.nombre }}</p> <!-- Agregar un título, por ejemplo: Detalle del Usuario {userId} -->
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row form-container">
                        <!-- Agregar campos del detalle -->
                        <!-- Este es un ejemplo -->
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Nombre</label>
                            <input type="text" class="form-control" :value="impuestoDto.nombre" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Descripción</label>
                            <input type="text" class="form-control" :value="impuestoDto.descripcion" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Provincia</label>
                            <input type="text" class="form-control" :value="impuestoDto.provincia ? impuestoDto.provincia.name : 'Sin Provincia'" readonly/>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Tipo</label>
                            <input type="text" class="form-control" :value="impuestoDto.tipo ? impuestoDto.tipo.descripcion : 'Sin Tipo'" readonly/>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Valor Alícuota</label>
                            <input type="text" class="form-control" :value="mostrarValorAlicuota" readonly/>
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

import ImpuestoDto from './ImpuestoDto' // Modificar por la clase dto correspondiente

import commonMixin from '@/Common/Mixins/commonMixin';
import detailMixin from '@/Common/Mixins/detailMixin';

export default {
    components: {
        CancelButton
    },
    mixins: [commonMixin, detailMixin],
    name: "impuestos-detail",

    data: function () {
        return {
            impuestoDto: new ImpuestoDto(), // Modificar por la clase dto correspondiente
            uiService: new UiService(),
            valorAlicuota: null
        };
    },
    computed: {
        grants() {
            return this.$store.getters.getGrants;
        },
        errorBag() {
            return this.$store.getters.getErrorBag;
        },
        mostrarValorAlicuota() {
            if(this.valorAlicuota != null)
                return (this.valorAlicuota).toFixed(2) + '%';
            return 'Sin valor'
        }
    },
    async mounted() {
        await this.init();
    },
    methods: {
        definirValorAlicuota(){
            if(this.impuestoDto.alicuota == null){
                this.valorAlicuota = this.impuestoDto.valor; 
            } else if( this.impuestoDto.valor == null){
                this.valorAlicuota = this.impuestoDto.alicuota.valor; 
            }
        },
        async init() {
            if (this.$route.params.id) await this.getAsync(this.$route.params.id);
            else this.goBack();
        },
        async getAsync(id) {
            this.uiService.showSpinner(true);
            await this.$store.dispatch("getAsync", id)
                .then((res) => {
                    this.impuestoDto = new ImpuestoDto(res);
                    this.definirValorAlicuota();
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        seeDetail() {
            // Limpiamos la lista antes de navegar
            this.$store.dispatch("clearList");
            this.$router.push({ name: "detail", params: { id: this.impuestoDto.id } });
        },
        async remove() {
            if (
                await this.uiService.confirmActionModal(
                "¿Está usted seguro que desea eliminar a este impuesto?",
                "Aceptar",
                "Cancelar"
                )
            ) {
                this.uiService.showSpinner(true)
                await this.$store.dispatch("deleteAsync", this.impuestoDto)
                    .then(async () => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            this.goBack();
                        } else {
                            if(this.errorBag.errors.hasOwnProperty('impuestoDetalle')){
                                this.uiService.showMessageError("Operación rechazada: Existe un comprobante que utiliza el impuesto")
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
            this.$router.push({ name: "edit", query: {desdeDetalle: true}, params: { id: this.impuestoDto.id } });
        }
    },
};
</script>