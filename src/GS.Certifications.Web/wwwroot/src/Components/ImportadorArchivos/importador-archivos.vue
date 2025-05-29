<template>
    <div class="row">
        <div v-if="title != null">
            <p class="h6">{{ loc[title] }}</p>
            <hr>
        </div>
        <span v-if="errorBag.hasGSFWebFileTransferServiceException()" class="text-danger field-validation-error"
            :data-valmsg-for="`formFile-${target}`" data-valmsg-replace="true">
            {{ errorBag.getGSFWebFileTransferServiceException() }}
        </span>
        <br>
        <!-- <span v-if="showMessageAfterUpload" class="text-danger field-validation-error" :data-valmsg-for="`formFile-${target}`" data-valmsg-replace="true">
          {{ MESSAGE_AFTER_UPLOAD }}
        </span> -->
        <span v-if="showMessageAfterAction && showMessage" class="text-danger field-validation-error"
            :data-valmsg-for="`formFile-${target}`" data-valmsg-replace="true">
            {{ MESSAGE_AFTER_ACTION }}
        </span>


        <div class="row col-8">
            <div class="col-6 ms-0 me-0" v-if="showDetail">
                <input class="form-control form-control-sm col-12 w-100" :value="archivoNombre ? archivoNombre : archivosImportadosDetalle" readonly />
            </div>
            <label class="col-auto ms-3 px-1 py-0 me-0 btn btn-outline-primary btn-sm" style="float:right">
                <i class="mt-2 fas fa-file-import"></i>
                <input type="file" :accept="extensionsTypes" :multiple="importarMultiplesArchivos"
                    :name="`formFile-${target}`" :id="`formFile-${target}`" @input="submitFile" @change="fileSelected"
                    style="display: none;" />
                {{ loc["Importar"] }}
            </label>
            <div class="col-1 mt-1 ms-1 me-0" data-toggle="tooltip" id="archivosTooltip"
                v-if="toolTipVisible"
                @touchstart="mostrarTooltip('archivosTooltip')" :title="infoText">
                <i class="fas fa-lg fa-info-circle"></i>
            </div>
        </div>

    </div>
</template>


<script>
import loc from "@/common/commonLoc";
import AcceptButton from "@/components/forms/accept-button.vue";
import UiService from "@/common/uiService";
import CrudMode from "@/common/CrudMode";
import inlineDelete from "@/components/forms/inline-delete-button.vue";
import ajax from "@/common/ajaxWrapper";
import ErrorBag from "@/common/ErrorBag";

const downloadTargetArchivoURL =
    baseUrl + "/api/archivos/DownloadTargetArchivos";
const deleteTargetArchivoURL =
    baseUrl + "/api/archivos/DeleteTargetArchivoFromTemp";
const saveTargetArchivosURL = baseUrl + "/api/archivos/SaveTargetArchivos";
const getTargetArchivosURL = baseUrl + "/api/archivos/GetTargetArchivos";
const discardTargetArchivosURL =
    baseUrl + "/api/archivos/discardTargetArchivos";
const importarTargetArchivosURL =
    baseUrl + "/api/archivos/ImportarTargetArchivos";
const defaultFileSizeLimit = 2;
const FILESIZEERROR = loc["Error al importar archivo. El o los archivos que se intentan importar superan el limite permitido."];
const ARCHIVOS_IMPORTADOS_DETALLE = loc["Seleccione los archivos que desea importar"];
const ARCHIVO_IMPORTADO_DETALLE = loc["Seleccione el archivo que desea importar"];

export default {
    name: "importador-archivos",

    components: {
        AcceptButton,
        inlineDelete,
    },
    props: {
        idModal: String,
        target: String,
        targetId: Number,
        title: String,
        archivoNombre: {
            type: String,
            required: false,
            default: () => null
        },
        disabled: false,
        mostrarAcciones: Boolean,
        acceptedExtensions: String,
        maxFileSize: Number,
        importarMultiplesArchivos: {
            type: Boolean,
            required: false,
            default: () => true
        },
        eb: {
            type: Object,
            required: false,
            default: () => null
        },
        analyzeFile: {
            type: Boolean,
            required: false,
            default: () => false
        },
        showMessage: {
            type: Boolean,
            required: false,
            default: () => true,
        },
        showDetail: {
            type: Boolean,
            required: false,
            default: () => false,
        },
        infoTextProp: {
            type: String,
            required: false,
            default: () => null,
        },
        targetURL: {
            type: String,
            required: false,
            default: () => "target",
        },
        apiURL: {
            type: String,
            required: false,
            default: () => "",
        },
        toolTipVisible: {
            type:Boolean,
            required: false,
            default: () => true,
        }
    },
    data: function () {
        return {
            uiService: new UiService(),
            loc,
            mode: new CrudMode(),
            archivos: [],
            currentArchivos: [],
            errorBag: new ErrorBag(),
            baseUrl,
            extensionsTypes: this.acceptedExtensions == null ? "*" : this.acceptedExtensions,
            showMessageAfterUpload: false,
            showMessageAfterAction: false,
            MESSAGE_AFTER_UPLOAD: loc["Para confirmar la subida del archivo agregado, debes presionar Aceptar"],
            MESSAGE_AFTER_ACTION: loc["Para confirmar la subida o eliminación del archivo, debes presionar Aceptar"],
            fileSizeLimit: this.maxFileSize == null ? defaultFileSizeLimit : this.maxFileSize,
            archivosImportadosDetalle: this.importarMultiplesArchivos ? ARCHIVOS_IMPORTADOS_DETALLE : ARCHIVO_IMPORTADO_DETALLE,
        };
    },
    computed: {
        fileInput() {
            return document.getElementById(`formFile-${this.target}`);
        },
        infoText() {
            return this.infoTextProp ? this.infoTextProp :  
            `${loc['Las extensiones soportadas son']} ${this.extensionsTypes} ${loc['con un máximo de']} ${this.fileSizeLimit} ${loc['megabytes']}`
        },
        urlImportarArchivos() {
            if (this.apiURL) return this.apiURL
            return baseUrl + `/api/archivos/Importar${this.targetURL}Archivos`;
        }
    },
    watch: {
        async targetId() {
            if (this.targetId) {
                await this.clearTempFolder();
                await this.loadArchivos();
            }
        },
        archivos() {
            if (this.archivos) {
                if (this.archivos.filter(a => a.id == 0 && !a.eliminado).length > 0) {
                    this.showMessageAfterUpload = true;
                } else {
                    this.showMessageAfterUpload = false;
                }

                if (this.archivos.filter(a => a.id == 0 || a.eliminado).length > 0) {
                    this.showMessageAfterAction = true;
                } else {
                    this.showMessageAfterAction = false;
                }

                if (JSON.stringify(this.archivos) === JSON.stringify(this.currentArchivos)) this.$emit('editing', false)
                else this.$emit('editing', true)
            }
        }
    },
    async mounted() {
        window.addEventListener("beforeunload", async () => {
            await this.clearTempFolder();
        });
        if (this.targetId) {
            await this.loadArchivos()
        }
    },
    async destroyed() {
        await this.clearTempFolder();
    },
    methods: {
        mostrarTooltip(tooltipId) {
            mostrarTooltip(tooltipId)
        },
        async clearTempFolder() {
            if (this.targetId)
                return new ajax()
                    .delete(discardTargetArchivosURL, {
                        TargetId: this.targetId,
                        TargetPath: this.target,
                    })
        },
        async validateFileSize() {
            //Esta promise existe solo para que se espere la promesa, haciendo que los eventos como @change sucedan.
            //Sin esto el evento @change y el computed se no corren en el orden adecuado.
            await new Promise(r => setTimeout(r, 0));
            for (let i = 0; i < document.getElementById(`formFile-${this.target}`).files.length; i++) {
                if (Math.round((document.getElementById(`formFile-${this.target}`).files[i].size / 1024 / 1000)).toFixed(2) > this.fileSizeLimit)
                    throw new Error(FILESIZEERROR);
            }
        },
        mapArchivos() {
            if (this.archivos)
                this.archivosImportadosDetalle = this.archivos.filter(archivo => !archivo.eliminado)
                    .map(archivo => archivo.fileName)
                    .join(", ");
        },

        async submitFile() {
            await this.validateFileSize()
                .then(async () => {
                    if (this.eb) {
                        this.eb.clear();
                    } else {
                        this.errorBag.clear();
                    }
                    var form = new FormData();
                    for (let i = 0; i < document.getElementById(`formFile-${this.target}`).files.length; i++) {
                        form.append("formFile", document.getElementById(`formFile-${this.target}`).files[i]);
                    }
                    form.append("targetId", this.targetId);
                    form.append("targetPath", this.target);
                    this.uiService.showSpinner(true);
                    return await new ajax()
                        .postFile(this.urlImportarArchivos, form, { errorBag: this.eb ? this.eb : this.errorBag })
                        .then((res) => {
                            if (!this.errorBag.hasErrors()) {
                                this.agregarArchivos(res);
                                this.$emit("updateArchivos", this.archivos);
                                this.mapArchivos();
                            }
                        })
                        .catch(error => {
                            this.uiService.showMessageError(loc[error.message]);
                        })
                        .finally(() => {
                            this.uiService.showSpinner(false);
                            this.clearFiles();
                        });
                })
                .catch(error => {
                    this.uiService.showMessageError(loc[error.message]);
                    this.clearFiles();
                })
        },
        clearFiles() {
            this.fileInput.value = null;
            this.fileInput.removeAttribute("disabled");
        },
        resetDetalleArchivos() {
            this.archivos = []
            this.archivosImportadosDetalle = this.importarMultiplesArchivos ? ARCHIVOS_IMPORTADOS_DETALLE : ARCHIVO_IMPORTADO_DETALLE
        },
        fileSelected() {
            this.fileInput.setAttribute("disabled", true);
        },
        cancel() {
            this.uiService.showSpinner(true);
            this.errorBag.clear();
            return new ajax()
                .delete(discardTargetArchivosURL, {
                    TargetId: this.targetId,
                    TargetPath: this.target,
                })
                .then(() => {
                    this.loadArchivos();
                    this.mode.setDetail();
                    this.resetDetalleArchivos();
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        agregarArchivos(files) {
            if (this.analyzeFile) {
                this.archivos = [files];
                return;
            }
            if (!this.importarMultiplesArchivos) this.archivos = files;
            else this.archivos = this.archivos.concat(files);
        },
        async downloadFile(a) {
            this.uiService.showSpinner(true);
            return new ajax()
                .getFile(downloadTargetArchivoURL, {
                    path: a.url,
                    fileName: a.fileName,
                }, { responseType: 'blob', observe: 'response' })
                .then(async (res) => {
                    let valorHeaderContentDisposition = res.headers.get('content-disposition').split(";")[1]
                    let nombreArchivo = valorHeaderContentDisposition.split("=")[1]
                    nombreArchivo = nombreArchivo.replaceAll("\"", '');
                    let body = res.body;

                    var blob = await res.blob();
                    const link = document.createElement('a');
                    link.href = URL.createObjectURL(blob);
                    link.download = nombreArchivo;
                    link.click();
                    URL.revokeObjectURL(link.href);
                })
                .catch(error => {
                    this.uiService.showMessageError(loc[error.message]);
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        async deleteArchivo(a) {
            this.uiService.showSpinner(true);
            return await new ajax()
                .delete(deleteTargetArchivoURL, {
                    ArchivoEliminar: a,
                    Archivos: this.archivos,
                    TargetPath: this.target,
                })
                .then((res) => {
                    // res es la lista de archivos con a.eliminado = true
                    // no se borra nada del file system
                    this.archivos = res;
                    this.$emit("updateArchivos", this.archivos);
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        async saveArchivos() {
            this.uiService.showSpinner(true);
            return await new ajax()
                .post(saveTargetArchivosURL, {
                    TargetId: this.targetId,
                    TargetPath: this.target,
                    Archivos: this.archivos,
                }, { errorBag: this.eb ? this.eb : this.errorBag })
                .then(async () => {
                    if (this.errorBag.hasErrors()) {
                        if (this.errorBag.get("FORMULARIO_DIGITAL")) {
                            this.uiService.showMessageErrorAndFocus(this.errorBag.get("FORMULARIO_DIGITAL"));
                        } else {
                            this.uiService.showMessageError(loc["Operación rechazada"])
                        }
                    } else {
                        this.uiService.showMessageSuccess(loc["Operación confirmada"])
                        this.$emit('created');
                        await this.loadArchivos();
                    }
                })
                .catch(error => {
                    this.uiService.showMessageError(loc[error.message]);
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        async loadArchivos() {
            this.uiService.showSpinner(true);
            return new ajax()
                .get(getTargetArchivosURL, {
                    TargetId: this.targetId,
                    TargetPath: this.target,
                })
                .then((res) => {
                    this.archivos = res;
                    this.currentArchivos = res;
                    this.$emit("updateArchivos", this.archivos);
                    this.mode.setDetail();
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        clearArchivosList() {
            this.archivos = [];
        },
        limpiar(){
            this.clearArchivosList();
            this.resetDetalleArchivos();
        }
    },
};
</script>
