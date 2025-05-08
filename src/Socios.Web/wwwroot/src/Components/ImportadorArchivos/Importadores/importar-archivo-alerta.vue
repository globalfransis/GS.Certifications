<template>
    <div>
        <importador-archivos ref="importadorArchivos" :showDetail="true" :acceptedExtensions="acceptedExtensions"
            :title="null" :target="target" @updateArchivos="onUpdateArchivos($event)"
            :showMessage="showMessageImportFile" :infoTextProp="mensajeParaImportadorMasivo" :maxFileSize="10"
            :targetURL="'Alerta'" :importarMultiplesArchivos="false" />
        <span class="text-danger field-validation-error" v-show="errorBag.hasErrors()">
            {{ errorBag }}
        </span>
    </div>
</template>


<script>
import loc from "@/common/commonLoc";
import AcceptButton from "@/components/forms/accept-button.vue";
import CrudMode from "@/common/CrudMode";
import importadorArchivos from "@/Components/ImportadorArchivos/importador-archivos.vue";
import inlineDelete from "@/components/forms/inline-delete-button.vue";
import ErrorBag from "@/common/ErrorBag";
import ajax from "@/common/ajaxWrapper";

const discardTargetArchivosURL = `${baseUrl}/api/archivos/discardTargetArchivos`;
const stringAcceptedExtension = ".html";

export default {
    name: "importar-archivo-alerta",

    components: {
        AcceptButton,
        importadorArchivos,
        inlineDelete,
    },
    props: {
    },
    data: function () {
        return {
            loc,
            errorBag: new ErrorBag(),
            mode: new CrudMode(),
            target: "Tarjetas",
            baseUrl,
            acceptedExtensions: stringAcceptedExtension,
            showMessageImportFile: false,
        };
    },
    watch: {
    },
    async mounted() {
        window.addEventListener("beforeunload", async () => {
            await this.clearTempFolder();
        });
    },
    methods: {
        onUpdateArchivos(listaArchivos) {
            this.$emit("archivosImportados", listaArchivos);
        },

        async clearTempFolder() {
            return new ajax()
                .delete(discardTargetArchivosURL, {
                    TargetId: this.targetId,
                    TargetPath: this.target,
                })
        },

        clearFiles() {
            this.$refs.importadorArchivos.resetDetalleArchivos();
        }
    },
};
</script>