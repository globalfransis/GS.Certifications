<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">Titulo</p> <!-- Agregar título, por ejemplo: Modificación del Usuario {userId} -->
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <!-- Agregar campos del formulario de edición -->
                        <!-- Este es un ejemplo -->
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Descripción</label>
                            <input type="text" class="form-control" v-model="solicitudCertificacion.descripcion">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12 d-flex justify-content-end gap-2 mb-3 mt-3">
            <accept-button :disabled="!grants.update" @click="updateAsync">
                Aceptar</accept-button>
            <cancel-button @click="cancel">Cancelar</cancel-button>
        </div>
    </div>
</template>

<script>
import AcceptButton from "@/components/forms/accept-button.vue";
import CancelButton from "@/components/forms/cancel-button.vue";
import UiService from "@/common/uiService";
import SolicitudCertificacion from './SolicitudCertificacion' // Modificar por la clase dto que corresponda
import commonMixin from '@/Common/Mixins/commonMixin';

export default {
    components: {
        AcceptButton,
        CancelButton
    },
    mixins: [commonMixin],
    name: "solicitudCertificacion-edit",
    data: function () {
        return {
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
            // Si no hay permisos de modificación, volvemos a la lista
            if (!this.grants.update) this.$router.push({ name: "home" });
            else {
                if (this.$route.params.id) await this.getAsync(this.$route.params.id);
                else this.cancel();
            }
        },
        async getAsync(id) {
            this.uiService.showSpinner(true)
            await this.$store.dispatch("getAsync", id)
                .then((res) => {
                    this.solicitudCertificacion = new SolicitudCertificacion(res);
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        goDetail() {
            this.$router.push({ name: "detail", params: { id: this.solicitudCertificacion.id } });
        },
        cancel() {
            this.$router.push({ name: "detail", params: { id: this.solicitudCertificacion.id } });
        },
        async updateAsync() {
            this.uiService.showSpinner(true)
            await this.$store.dispatch("putAsync", this.solicitudCertificacion)
                .then(() => {
                    if (!this.errorBag.hasErrors()) {
                        this.uiService.showMessageSuccess("Operación confirmada")
                        this.goDetail();
                    } else {
                        this.uiService.showMessageError("Operación rechazada")
                    }
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        }
    },
};
</script>