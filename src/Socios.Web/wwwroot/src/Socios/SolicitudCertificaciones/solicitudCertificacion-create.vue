<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">{{loc["Nueva Solicitud de Certificación"]}}</p>
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{loc["Descripción"]}}</label>
                            <input type="text" class="form-control" v-model="solicitudCertificacion.descripcion">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12 d-flex justify-content-end gap-2 mb-3 mt-3">
            <accept-button :disabled="!grants.create" @click="createAsync">{{loc["Aceptar"]}}</accept-button>
            <cancel-button @click="cancel">{{loc["Cancelar"]}}</cancel-button>
        </div>
    </div>
</template>

<script>
import AcceptButton from "@/components/forms/accept-button.vue";

import CancelButton from "@/components/forms/cancel-button.vue";
import UiService from "@/common/uiService";

import SolicitudCertificacion from './SolicitudCertificacion'

import commonMixin from '@/Common/Mixins/commonMixin';

import loc from "@/common/commonLoc.js"

export default {
    components: {
        AcceptButton,
        CancelButton
    },
    mixins: [commonMixin],
    name: "solicitudCertificacion-create",

    data: function () {
        return {
            loc,
            solicitudCertificacion: new SolicitudCertificacion(),
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
            // Si no hay permisos de alta, volvemos a la lista
            if (!this.grants.create) this.$router.push({ name: "home" });
        },
        cancel() {
            this.$router.push({ name: "home" });
        },
        goDetail(id) {
            this.$router.push({ name: "detail", params: { id: id } });
        },
        async createAsync() {
            this.uiService.showSpinner(true)
            await this.$store.dispatch("postAsync", this.solicitudCertificacion)
                .then((id) => {
                    if (!this.errorBag.hasErrors()) {
                        this.uiService.showMessageSuccess(loc["Operación confirmada"])
                        this.goDetail(id);
                    } else {
                        this.uiService.showMessageError(loc["Operación rechazada"])
                    }
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        }
    },
};
</script>