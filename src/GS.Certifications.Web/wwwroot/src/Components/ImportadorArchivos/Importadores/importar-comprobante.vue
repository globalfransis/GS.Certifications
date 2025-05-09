<template>
    <div>
        <importador-archivos ref="importadorArchivos" :showDetail="true" :acceptedExtensions="acceptedExtensions"
            idModal="__modal_comprobantes" :title="null" :target="target" @updateArchivos="onUpdateArchivos($event)"
            :showMessage="showMessageImportFile" :maxFileSize="2" :targetURL="target" :analyzeFile="true"
            :eb="errorBag"
            :apiURL="apiURLComputed" :importarMultiplesArchivos="false" />
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

const API_URL = `${baseUrl}/api/Proveedores/Comprobantes/Analisis`;
const ACCEPTED_EXTENSIONS = ".png, .jpg, .jpeg, image/png, image/jpeg, .pdf, application/pdf";

export default {
    name: "importar-comprobante",

    components: {
        AcceptButton,
        importadorArchivos,
        inlineDelete,
    },
    props: {
        empresaPortalId: {
            type: Number,
            required: false,
            default: () => null
        }
    },
    data: function () {
        return {
            loc,
            // errorBag: new ErrorBag(),
            mode: new CrudMode(),
            target: "Comprobante",
            baseUrl,
            acceptedExtensions: ACCEPTED_EXTENSIONS,
            archivos: [],
            showMessageImportFile: false,
            API_URL
        };
    },
    computed: {
        apiURLComputed() {
            return `${API_URL}?EmpresaId=${this.empresaPortalId}`
        },
        errorBag() {
            return this.$store.getters.getErrorBag;
        }
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