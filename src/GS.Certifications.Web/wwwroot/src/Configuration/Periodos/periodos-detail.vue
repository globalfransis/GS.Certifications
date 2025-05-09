<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">Detalle del Periodo</p>
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row form-container">
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Año</label>
                            <input type="text" class="form-control" v-model="periodoDto.año" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Numero de periodo</label>
                            <input type="text" class="form-control" v-model="periodoDto.numeroPeriodo" readonly>
                        </div>
                        <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Fecha Inicio</label>
                            <input type="date" class="form-control" v-model="formattedFechaInicio" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Fecha Fin</label>
                            <input type="date" class="form-control" v-model="formattedFechaFin" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Estado de Periodo</label>
                            <input type="text" class="form-control" :value="periodoDto.estado ? periodoDto.estado.descripcion : 'Sin Estado'" readonly/>
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

import PeriodoDto from './PeriodoDto' // Modificar por la clase dto correspondiente

import commonMixin from '@/Common/Mixins/commonMixin';
import detailMixin from '@/Common/Mixins/detailMixin';

export default {
    components: {
        CancelButton
    },
    mixins: [commonMixin, detailMixin],
    name: "periodos-detail",

    data: function () {
        return {
            periodoDto: new PeriodoDto(), // Modificar por la clase dto correspondiente
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
        formattedFechaInicio: {
            get() {
                if(!this.periodoDto.fechaInicio) return;
                return this.periodoDto.fechaInicio.split('T')[0];
            },
            set(newValue) {
                this.periodoDto.fechaInicio = newValue + 'T00:00:00';
            }
        },
        formattedFechaFin: {
            get() {
                if(!this.periodoDto.fechaFin) return;
                return this.periodoDto.fechaFin.split('T')[0];
            },
            set(newValue) {
                this.periodoDto.fechaFin = newValue + 'T00:00:00';
            }
        }
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
                    this.periodoDto = new PeriodoDto(res);
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        seeDetail() {
            // Limpiamos la lista antes de navegar
            this.$store.dispatch("clearList");
            this.$router.push({ name: "detail", params: { id: this.periodoDto.id } });
        },
        async remove() {
            if (
                await this.uiService.confirmActionModal(
                "¿Está usted seguro que desea eliminar a este periodo?",
                "Aceptar",
                "Cancelar"
                )
            ) {
                this.uiService.showSpinner(true)
                await this.$store.dispatch("deleteAsync", this.periodoDto)
                    .then(async () => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            this.goBack();
                        } else {
                            this.uiService.showMessageError("Operación rechazada")
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
            this.$router.push({ name: "edit", query: {desdeDetalle: true}, params: { id: this.periodoDto.id } });
        }
    },
};
</script>