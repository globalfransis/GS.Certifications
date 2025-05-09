<template>
    <div>
        <div class="modal" data-bs-backdrop="static" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.2);"
            :id="idModal" tabindex="-1" :ref="idModal" :aria-labelledby="idModal" aria-hidden="true">
            <div class="modal-dialog modal-dialog-scrollable grid-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">{{ alertaAsunto ? alertaAsunto : "" }}</h4>
                        <button type="button" class="btn-close" @click="modalClosed" aria-label="Close"></button>
                    </div>
                    <div class="modal-body text-start">
                        <!-- investigar como hacer que un elemento no sea focuseable para que no se pueda acceder a los links por keyboard -->
                        <div v-if="render" class="non-clickable" v-html="alertaCuerpo ? alertaCuerpo : ''">
                        </div>
                        <div v-if="!render" class="non-clickable">
                            {{ alertaCuerpo }}
                        </div>
                    </div>
                    <div class="modal-footer">
                        <cancel-button type="button" class="btn btn-outline-primary btn-sm" @click="modalClosed">
                            Cerrar</cancel-button>
                    </div>
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
    name: "alertasBody-modal",
    props: {
        idModal: String,
        alertaCuerpo: String,
        alertaAsunto: String,
        render: Boolean
    },
    data: function () {
        return {
            uiService: new UiService(),
            alertaArchivoData: {
                asunto: "",
                cuerpo: ""
            }
        };
    },
    mounted() {
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