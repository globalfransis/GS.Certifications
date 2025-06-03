<template>
    <div class="modal fade" data-bs-backdrop="static" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.2);"
        :id="idModal" tabindex="-1" :ref="idModal" :aria-labelledby="idModal">
        <div class="modal-dialog modal-dialog-centered grid-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">{{loc["Rechazar Documento"]}} {{ documento.id }}</h4>
                    <button type="button" class="btn-close" @click="modalClosed" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <label class="control-label">{{loc["Motivo"]}}</label>
                    <textarea id="motivoRechazoTxtAreaModal" class="form-control" enable cols="20" rows="4"
                        v-model="documento.motivoRechazo"></textarea>
                </div>
                <div class="modal-footer gap-2">
                    <accept-button :disabled="!documento.motivoRechazo" @click="reject">
                        {{loc["Rechazar"]}}</accept-button>
                    <cancel-button type="button" class="btn btn-outline-primary btn-sm" @click="modalClosed">
                        {{loc["Cancelar"]}}</cancel-button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import UiService from "@/common/uiService";
import AcceptButton from "@/components/forms/accept-button.vue";
import CancelButton from "@/components/forms/cancel-button.vue";
import loc from "@/common/commonLoc";

export default {
    components: {
        AcceptButton,
        CancelButton
    },
    name: "documentoRechazo-modal",
    props: {
        idModal: String,
        documento: Object
    },
    data: function () {
        return {
            uiService: new UiService(),
            loc
        };
    },
    computed: {
    },
    async mounted() {
        document.getElementById("motivoRechazoTxtAreaModal").removeAttribute("disabled")
    },
    methods: {
        reject() {
            this.$emit("documentoRechazado", this.documento.motivoRechazo);
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
