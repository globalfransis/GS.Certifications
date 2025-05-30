<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">{{loc["Titulo"]}}</p> <!-- Agregar un título, por ejemplo: Detalle del Usuario {userId} -->
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row form-container">
                        <!-- Agregar campos del detalle -->
                        <!-- Este es un ejemplo -->
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{loc["Certificación"]}}</label>
                            <input type="text" class="form-control" v-model="solicitudCertificacion.descripcion">
                        </div>


                        
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12 d-flex justify-content-end gap-2 mb-3 mt-3">
            <accept-button @click="update">{{loc["Editar"]}}</accept-button>
            <cancel-button @click="goBack">{{loc["Volver"]}}</cancel-button>
        </div>
    </div>
</template>

<script>
import AcceptButton from "@/components/forms/accept-button.vue";
import CancelButton from "@/components/forms/cancel-button.vue";
import UiService from "@/common/uiService";

import SolicitudCertificacion from './SolicitudCertificacion' // Modificar por la clase dto correspondiente

import commonMixin from '@/Common/Mixins/commonMixin';
import detailMixin from '@/Common/Mixins/detailMixin';

import loc from "@/common/commonLoc.js"

export default {
    components: {
        AcceptButton,
        CancelButton
    },
    mixins: [commonMixin, detailMixin],
    name: "solicitudCertificacion-detail",

    data: function () {
        return {
            loc,
            solicitudCertificacion: new SolicitudCertificacion(), // Modificar por la clase dto correspondiente
            uiService: new UiService()
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
            if (this.$route.params.id) await this.getAsync(this.$route.params.id);
            else this.goBack();
        },
        async getAsync(id) {
            this.uiService.showSpinner(true);
            await this.$store.dispatch("getAsync", id)
                .then((res) => {
                    this.solicitudCertificacion = new SolicitudCertificacion(res);
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        goBack() {
            this.$router.push({ name: "home", query: { fromDetail: true } });
        },
        update() {
            this.$router.push({ name: "edit", params: { id: this.solicitudCertificacion.id } });
        }
    },
};
</script>