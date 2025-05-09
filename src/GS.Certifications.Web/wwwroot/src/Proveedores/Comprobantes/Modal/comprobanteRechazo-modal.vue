<template>
    <div class="modal fade" data-bs-backdrop="static" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.2);"
        :id="idModal" tabindex="-1" :ref="idModal" :aria-labelledby="idModal">
        <div class="modal-dialog modal-dialog-centered grid-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Rechazar Comprobante {{ comprobante.numero | nroComprobante }}</h4>
                    <button type="button" class="btn-close" @click="modalClosed" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <label class="control-label">Motivo</label>
                    <textarea id="motivoRechazoTxtAreaModal" class="form-control" enable cols="20" rows="4"
                        v-model="comprobante.motivoRechazo"></textarea>
                </div>
                <div class="modal-footer gap-2">
                    <accept-button @click="reject">
                        Rechazar</accept-button>
                    <cancel-button type="button" class="btn btn-outline-primary btn-sm" @click="modalClosed">
                        Cancelar</cancel-button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import UiService from "@/common/uiService";
import AcceptButton from "@/components/forms/accept-button.vue";
import CancelButton from "@/components/forms/cancel-button.vue";

export default {
    components: {
        AcceptButton,
        CancelButton
    },
    name: "comprobanteRechazo-modal",
    props: {
        idModal: String,
        comprobante: Object
    },
    data: function () {
        return {
            uiService: new UiService(),
        };
    },
    computed: {
    },
    async mounted() {
        document.getElementById("motivoRechazoTxtAreaModal").removeAttribute("disabled")
    },
    methods: {
        reject() {
            this.$emit("comprobanteRechazado", this.comprobante.motivoRechazo);
            this.modalClosed();
        },
        modalClosed: function () {
            $(`#${this.idModal}`).modal("toggle");
        },
    },
};
</script>

<style lang="scss" scoped>
.grid-dialog {
    max-width: 1000px;
    max-height: 700px;
    // position:fixed;
}
</style>
