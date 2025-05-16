<template>
    <div>
        <importador-archivos ref="importadorArchivos" :showDetail="true" :acceptedExtensions="acceptedExtensions"
            idModal="__modal_documentos" :archivoNombre="fileName" :target="target" @updateArchivos="onUpdateArchivos($event)"
            :showMessage="showMessageImportFile" :maxFileSize="2" :targetURL="target"
            :analyzeFile="true"
            :apiURL="apiURL"
            :eb="errorBag"
            :importarMultiplesArchivos="false" />
    </div>
</template>


<script>
import loc from "@/common/commonLoc";
import AcceptButton from "@/components/forms/accept-button.vue";
import CrudMode from "@/common/CrudMode";
import importadorArchivos from "@/Components/ImportadorArchivos/importador-archivos.vue";
import inlineDelete from "@/components/forms/inline-delete-button.vue";
import ajax from "@/common/ajaxWrapper";

const API_URL = `${baseUrl}/api/Certificaciones/Solicitudes/Documentos/Analisis/`;
const ACCEPTED_EXTENSIONS = ".png, .jpg, .jpeg, image/png, image/jpeg, .pdf, application/pdf";

export default {
    name: "importar-documento",

    components: {
        AcceptButton,
        importadorArchivos,
        inlineDelete,
    },
    computed: {
        errorBag() {
            return this.$store.getters.getErrorBag;
        },
        apiURL() {
            return `${baseUrl}/api/Certificaciones/Solicitudes/${this.solicitudId}/Documentos/${this.documentoId}/Analisis/`
        }
    },
    props: {
        documentoId: Number,
        solicitudId: Number,
        fileName: String
    },
    data: function () {
        return {
            loc,
            // errorBag: new ErrorBag(),
            mode: new CrudMode(),
            target: "Documento",
            baseUrl,
            acceptedExtensions: ACCEPTED_EXTENSIONS,
            archivos: [],
            showMessageImportFile: false,
            API_URL
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
            this.archivos = listaArchivos;
            this.$emit("archivosUpdated", this.archivos);
        },

        async clearTempFolder() {
            if (this.archivos.length) {
                return await new ajax()
                    .delete(API_URL, {
                        TargetPath: this.archivos[0].folderPath
                    })
            }
        },

        clearFiles() {
            this.$refs.importadorArchivos.resetDetalleArchivos();
        },

        async cancel() {
            await this.$refs.importadorArchivos.cancel();
        },

        loadArchivo(archivos) {
            this.archivos = archivos;
            this.$refs.importadorArchivos.archivos = this.archivos
            if (this.archivos.length > 0) this.$refs.importadorArchivos.mapArchivos();
        }
    },
};
</script>