<template>
    <div class="modal fade" data-bs-backdrop="static" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.2);"
        :id="idModal" tabindex="-1" :ref="idModal" :aria-labelledby="idModal">
        <div class="modal-dialog modal-dialog-centered grid-dialog">
            <div class="modal-content">

                <div class="modal-body">
                    <label class="control-label">Observaciones ARCA</label>
                    <textarea id="motivoRechazoTxtAreaModal" disabled class="form-control" enable cols="20" rows="4"
                        v-model="observaciones"></textarea>
                </div>
                
                <div class="modal-footer gap-2">
                    <cancel-button type="button" class="btn btn-outline-primary btn-sm" @click="modalClosed">
                        Cerrar</cancel-button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import UiService from "@/common/uiService";
import CancelButton from "@/components/forms/cancel-button.vue";

export default {
    components: {
        CancelButton
    },
    name: "observacionesArca-modal",
    props: {
        idModal: String,
        observaciones: String
    },
    data: function () {
        return {
            uiService: new UiService(),
        };
    },
    computed: {
    },
    async mounted() {
    },
    methods: {
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
