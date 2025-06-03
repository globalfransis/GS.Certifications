<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 d-flex justify-content-between mt-4 align-items-start">
                <div class="col-12 d-grid">
                    <div class="row align-items-center">
                        <p class="h5 col-md-7 col-12 mb-md-0 mb-2">{{ tipoDoc }}</p>
                        <div class="col-md-5 col-12 gap-3 d-flex justify-content-md-end align-items-center">
                            <documentoEstado-label :value="documento.estadoId" />
                            <cancel-button class="ms-2" @click="cancel">{{ loc["Volver"] }}</cancel-button>
                        </div>
                    </div>
                    <div class="btn-group btn-group-sm mt-2 col-1" v-if="documento.archivoURL && documento.operationStatus != PROCESSING" role="group"
                        aria-label="Controles de visualización del documento">
                        <button type="button" class="btn btn-light" :class="{ 'active': currentLayoutMode === LayoutMode.Split }" @click="setLayout(LayoutMode.Split)" :title="loc['Mostrar vista dividida']"><i class="fas fa-columns"></i></button>
                        <button type="button" class="btn btn-light" :class="{ 'active': currentLayoutMode === LayoutMode.File }" @click="setLayout(LayoutMode.File)" :title="loc['Mostrar solo el documento']"><i class="fas fa-file-alt"></i></button>
                        <button type="button" class="btn btn-light" :class="{ 'active': currentLayoutMode === LayoutMode.Form }" @click="setLayout(LayoutMode.Form)" :title="loc['Mostrar solo el formulario']"><i class="fas fa-list-alt"></i></button>
                    </div>
                </div>
            </div>

            <div class="card mt-3">
                <div class="card-body">

                    <div v-if="!documento.archivoURL && documento.id && documento.operationStatus != PROCESSING && documento.operationStatus != FAILED" 
                         class="text-center p-lg-4 p-3 mb-4">
                        <div class="card shadow-sm bg-light" style="margin: auto; border-radius: 0.5rem">
                            <div class="card-body p-4">
                                <i class="fas fa-file-upload fa-3x text-primary mb-3"></i>
                                <h5 class="card-title mb-3">{{ loc["Cargar Documento Requerido"] }}</h5>
                                <p class="text-muted small">
                                    {{ loc["Para continuar, por favor seleccioná el archivo para"] }} <strong v-if="tipoDoc">{{ tipoDoc }}</strong><strong v-else>{{ loc['el documento actual'] }}</strong>.
                                    <br> {{ loc["Una vez cargado, se analizará para extraer información relevante."] }}
                                </p>
                                <div class="mt-4">
                                    <importar-documento idModal="__modal_DocumentoArchivo_empty" ref="importarDocumentoEmpty"
                                        :title="loc['Seleccionar archivo...']" :disabled="!grants.update" 
                                        :documentoId="documento.id" :solicitudId="documento.solicitudId" :fileName="documento.archivoURL"
                                        @archivosUpdated="onDocumentoAnalyzedAsync($event)" />
                                    <span class="text-danger field-validation-error d-block mt-2 small">{{ errorBag.get("documentoError") }}</span>
                                </div>
                            </div>
                        </div>
                        <p class="text-muted text-center mt-3 mb-0 fst-italic small">
                            {{ loc["Los campos del formulario se habilitarán y/o completarán después del análisis del documento."] }}
                        </p>
                         <hr class="my-4">
                    </div>

                    <div v-if="documento.archivoURL && documento.id" class="form-group col-sm-12 mb-3 row">
                        <div class="col-lg-8 col-md-10">
                            <label class="control-label">{{ loc["Cambiar Documento"] }}</label>
                            <importar-documento idModal="__modal_DocumentoArchivo_loaded" ref="importarDocumentoLoaded"
                                :title="loc['Cambiar archivo...']"
                                :disabled="!grants.update || documento.operationStatus == PROCESSING"
                                :documentoId="documento.id" :solicitudId="documento.solicitudId"
                                :fileName="documento.archivoURL"
                                @archivosUpdated="onDocumentoAnalyzedAsync($event)" />
                            <span class="text-danger field-validation-error d-block mt-1 small">{{ errorBag.get("documentoError") }}</span>
                        </div>
                    </div>
                    
                    <div v-if="documento.id && (documento.operationStatus == PROCESSING || (documento.operationStatus == FAILED && previousOperationStatusForMessage == PROCESSING) || (documento.operationStatus == COMPLETED && previousOperationStatusForMessage == PROCESSING) || (documento.operationStatus == FAILED && !documento.archivoURL) )"
                         class="mt-3 mb-4 p-3 text-center d-flex flex-column align-items-center justify-content-center">
                        
                        <div :key="documento.operationStatus" v-if="documento.operationStatus == PROCESSING">
                            <div class="ai-animated-icon-container mb-3"> <sparkles-icon :animated="true" size="2.5rem"/> </div>
                            <h5 class="text-primary mb-2">{{ loc["Procesando Documento con IA..."] }}</h5>
                            <p class="text-muted small" style="min-height: 1.5em;">{{ currentAiMessage }}</p>
                            <div class="progress mt-2" style="height: 6px;">
                                <div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" style="width: 100%"></div>
                            </div>
                        </div>

                        <div v-else-if="documento.operationStatus == COMPLETED && previousOperationStatusForMessage == PROCESSING"
                             class="alert alert-success d-flex align-items-center justify-content-center shadow-sm w-75">
                            <i class="fas fa-check-circle fa-lg me-2"></i> {{ loc["Documento analizado con éxito"] }}
                        </div>

                        <div v-else-if="documento.operationStatus == FAILED"
                             class="alert alert-danger d-flex flex-column align-items-center justify-content-center shadow-sm w-75 p-4">
                            <i class="fas fa-exclamation-triangle fa-2x text-danger mb-3"></i>
                            <h5 class="text-danger mb-2">{{ loc["Error en el Análisis del Documento"] }}</h5>
                            <p v-if="!documento.archivoURL" class="text-muted mb-3 small">{{ loc["El análisis previo falló. Puedes intentar cargar el archivo de nuevo o contactar a soporte."] }}</p>
                            <importar-documento v-if="!documento.archivoURL"
                                idModal="__modal_DocumentoArchivo_retry_failed" ref="importarDocumentoRetryFailed"
                                :title="loc['Reintentar Carga']" :disabled="!grants.update" :documentoId="documento.id"
                                :solicitudId="documento.solicitudId" :fileName="documento.archivoURL"
                                @archivosUpdated="onDocumentoAnalyzedAsync($event)" />
                        </div>
                    </div>
                    <hr v-if="documento.id && (documento.operationStatus == PROCESSING || ((documento.operationStatus == COMPLETED || documento.operationStatus == FAILED) && previousOperationStatusForMessage == PROCESSING) || (documento.operationStatus == FAILED && !documento.archivoURL))">

                    <div class="row mt-2" v-if="documento.id && (documento.archivoURL || (!documento.archivoURL && documento.operationStatus != PROCESSING && documento.operationStatus != FAILED))">
                        <div :id="documentoArchivoDivId" class="col-md-6" :style="{ height: iframeHeight }">
                            <iframe v-if="iframeSrc" :src="iframeSrc" style="width: 100%; height: 100%; border: none;"></iframe>
                            <div v-else-if="documento.archivoURL && !iframeSrc"
                                 class="alert alert-warning text-center d-flex align-items-center justify-content-center h-100">
                                <span><i class="fas fa-exclamation-triangle me-2"></i>{{ loc["No se puede mostrar el documento. La URL podría ser inválida o el archivo no está accesible."] }}</span>
                            </div>
                            <div v-else-if="!documento.archivoURL"
                                class="alert alert-light text-center d-flex align-items-center justify-content-center h-100 border">
                                <span><i class="fas fa-eye-slash me-2"></i>{{ loc["No hay documento cargado para visualizar."] }}</span>
                            </div>
                        </div>
                        <div :id="documentoFormularioDivId" class="col-md-6 col-6">
                            <div class="form-group col-lg-10 col-sm-12 mb-3 required">
                                <label class="control-label">{{ loc["Fecha Desde"] }}</label>
                                <input :disabled="documento.operationStatus == PROCESSING || !grants.update" type="date" class="form-control form-control-sm" v-model="documento.fechaDesde">
                                <span class="text-danger field-validation-error small">{{ errorBag.get("fechaDesde") }}</span>
                                <span class="text-danger field-validation-error small">{{ errorBag.get("vigencia") }}</span>
                            </div>
                            <div class="form-group col-lg-10 col-sm-12 mb-3 required">
                                <label class="control-label">{{ loc["Fecha Hasta"] }}</label>
                                <input :disabled="documento.operationStatus == PROCESSING || !grants.update" type="date" class="form-control form-control-sm" v-model="documento.fechaHasta">
                                <span class="text-danger field-validation-error small">{{ errorBag.get("fechaHasta") }}</span>
                            </div>
                            <div class="form-group col-lg-12 col-sm-12 mb-3">
                                <label class="control-label">{{ loc["Observaciones"] }}</label>
                                <textarea class="form-control form-control-sm" cols="50" rows="6" :disabled="!grants.update" v-model="documento.observaciones"></textarea>
                                <span class="text-danger field-validation-error small">{{ errorBag.get("observaciones") }}</span>
                            </div>
                            <div v-if="documento.estadoId == DOCUMENTO_RECHAZADO" class="form-group col-lg-12 col-sm-12 mb-4">
                                <label class="control-label">{{ loc["Motivo Rechazo"] }}</label>
                                <textarea id="motivoRechazoTxtArea" disabled class="form-control" enable cols="20"
                                    rows="4" v-model="documento.motivoRechazo"></textarea>
                            </div>
                        </div>
                    </div>
                </div> </div> <div class="col-12 d-flex justify-content-end gap-2 mb-3 mt-3"
                v-if="currentLayoutMode != LayoutMode.File && documento.id">
                <!-- <button @click.prevent="saveAsync" class="btn btn-secondary btn-sm"
                    :disabled="documento.operationStatus == PROCESSING || (!grants.update || documento.propietarioActualId == SOCIOS && documento.estadoId != DOCUMENTO_PRESENTADO) || (!grants.update || documento.propietarioActualId == BACKOFFICE && documento.estadoId == DOCUMENTO_PRESENTADO)">
                    <i class="fas fa-save"></i>
                    {{ loc["Guardar"] }}
                </button> -->
                <accept-button @click="updateAsync"
                    :disabled="documento.operationStatus == PROCESSING || (!grants.update || documento.propietarioActualId == SOCIOS && documento.estadoId != DOCUMENTO_PRESENTADO) || (!grants.update || documento.propietarioActualId == BACKOFFICE && documento.estadoId == DOCUMENTO_PRESENTADO)">
                    {{loc["Aceptar"]}}</accept-button>
                <cancel-button @click="cancel">{{ loc["Volver"] }}</cancel-button>
            </div>
        </div>
    </div>
</template>
<script>
import AcceptButton from "@/components/forms/accept-button.vue";
import CancelButton from "@/components/forms/cancel-button.vue";
import UiService from "@/common/uiService";
import commonMixin from '@/Common/Mixins/commonMixin';
import importarDocumento from '@/Components/ImportadorArchivos/Importadores/importar-documento'
import inlineEdit from "@/components/forms/inline-edit-button.vue";
import inlineDelete from "@/components/forms/inline-delete-button.vue";
import inlineCancel from "@/components/forms/inline-cancel-button.vue";
import commonMixin from '@/Common/Mixins/commonMixin';
import documentoEstadoLabel from "@/Selects/documentoEstado-label.vue";
import LayoutMode from "./LayoutMode";

import Documento from "./Documento";

import loc from "@/common/commonLoc.js"

import SparklesIcon from '@/common/ui/SparklesIcon.vue';

const NO_DATA_MESSAGE = loc["No hay datos"];
// Origen de la solicitud
const SOCIOS = 1;
const BACKOFFICE = 2;
const CORREO = 3;

export default {
    components: {
        AcceptButton,
        CancelButton,
        importarDocumento,
        inlineEdit,
        inlineDelete,
        inlineCancel,
        documentoEstadoLabel,
        SparklesIcon
    },
    mixins: [commonMixin],
    name: "documento-edit",
    data: function () {
        return {
            loc,
            documento: new Documento(),
            uiService: new UiService(),
            tipoDoc: '',
            NO_DATA_MESSAGE,
            documentoArchivoDivId: "__documentoArchivo",
            documentoFormularioDivId: "__documentoFormulario",
            LayoutMode,
            currentLayoutMode: LayoutMode.Split,
            // --- Origen de la solicitud ---
            SOCIOS,
            BACKOFFICE,
            CORREO,
            // --- Estados de la solicitud ---
            PENDIENTE: SolicitudEstado.PENDIENTE,
            PRESENTADA: SolicitudEstado.PRESENTADA,
            APROBADA: SolicitudEstado.APROBADA,
            RECHAZADA: SolicitudEstado.RECHAZADA,
            BORRADOR: SolicitudEstado.BORRADOR,
            // --- Estados de los documentos de una solicitud
            DOCUMENTO_PENDIENTE: DocumentoEstado.PENDIENTE,
            DOCUMENTO_VALIDADO: DocumentoEstado.VALIDADO,
            DOCUMENTO_RECHAZADO: DocumentoEstado.RECHAZADO,
            DOCUMENTO_VENCIDO: DocumentoEstado.VENCIDO,
            DOCUMENTO_PRESENTADO: DocumentoEstado.PRESENTADO,
            // --- Estados de operacion
            PROCESSING: OperationStatus.PROCESSING,
            COMPLETED: OperationStatus.COMPLETED,
            FAILED: OperationStatus.FAILED,
            // ---
            pollingIntervalId: null, // Para guardar el ID del intervalo del polling
            isCurrentlyPolling: false, // Para evitar ejecuciones solapadas del poll
            pollingDelay: 1500, // Tiempo en milisegundos para el polling
            aiProcessingMessages: [], // Se poblará en mounted o created
            currentAiMessage: '',
            aiMessageIntervalId: null,
            currentAiMessageIndex: 0,
            iframeHeight: '120vh',
            previousOperationStatusForMessage: null, // Para mostrar mensajes de COMPLETED/FAILED solo una vez
        };
    },
    computed: {
        iframeSrc() {
            if (this.documento && this.documento.archivoURL && this.documento.solicitudGuid && this.documento.guid) {
                try {
                    const url = `${this.currentLocation}/CertificacionesWebRepository/Uploads/Solicitudes/Solicitud_${this.documento.solicitudGuid}/Doc_${this.documento.guid}/${this.documento.archivoURL}`;
                    return url;
                } catch (e) {
                    console.error("Error construyendo iframeSrc:", e);
                    return null;
                }
            }
            return null;
        },
        currentLocation() {
            return window.location.origin;
        },
        grants() {
            return this.$store.getters.getGrants;
        },
        errorBag() {
            return this.$store.getters.getErrorBag;
        }
    },
    async mounted() {
        this.aiProcessingMessages = [
            this.loc["Analizando documento..."],
            this.loc["Leyendo estructura del documento..."],
            this.loc["Extrayendo campos clave..."],
            this.loc["Verificando información..."],
            this.loc["Finalizando análisis..."]
        ];
        this.currentAiMessage = this.aiProcessingMessages[0];
        await this.init();
    },
    beforeDestroy() {
        this.stopStatusPolling();
        this.stopAiMessageCycling();
    },
    methods: {
        startAiMessageCycling() {
            this.stopAiMessageCycling(); // detenemos  cualquier ciclo anterior

            if (this.documento.operationStatus === this.PROCESSING && this.aiProcessingMessages.length > 0) {
                this.currentAiMessageIndex = 0;
                this.currentAiMessage = this.aiProcessingMessages[this.currentAiMessageIndex];

                if (this.currentAiMessageIndex === this.aiProcessingMessages.length - 1) {
                    return;
                }

                this.aiMessageIntervalId = setInterval(() => {
                    if (this.documento.operationStatus !== this.PROCESSING) {
                        this.stopAiMessageCycling();
                        return;
                    }
                    this.currentAiMessageIndex++;
                    if (this.currentAiMessageIndex >= this.aiProcessingMessages.length - 1) {
                        this.currentAiMessageIndex = this.aiProcessingMessages.length - 1;
                        this.currentAiMessage = this.aiProcessingMessages[this.currentAiMessageIndex];
                        this.stopAiMessageCycling();
                    } else {
                        this.currentAiMessage = this.aiProcessingMessages[this.currentAiMessageIndex];
                    }
                }, 1500);
            } else if (this.documento.operationStatus === this.PROCESSING) {
                this.currentAiMessage = this.loc["Analizando documento..."];
            }
        },
        stopAiMessageCycling() {
            if (this.aiMessageIntervalId) {
                clearInterval(this.aiMessageIntervalId);
                this.aiMessageIntervalId = null;
            }
        },
        async pollDocumentStatus() {
            // si ya no está procesando, o si otra llamada de poll está en curso, no hacemos nada
            if (this.documento.operationStatus !== this.PROCESSING || this.isCurrentlyPolling) {
                if (this.documento.operationStatus !== this.PROCESSING) {
                    this.stopStatusPolling();
                }
                return;
            }

            this.isCurrentlyPolling = true;
            try {
                await this.getOperationStatusAsync(this.documento.id);
            } catch (error) {
                console.error(loc["Error durante el polling del estado del documento:"], error);
                this.stopStatusPolling(); // detenemos el polling en caso de error
            } finally {
                this.isCurrentlyPolling = false;
            }
        },
        startStatusPolling() {
            // detenemos cualquier polling anterior para evitar múltiples intervalos
            this.stopStatusPolling();

            if (this.documento.operationStatus === this.PROCESSING && this.documento.id) {
                this.pollingIntervalId = setInterval(() => {
                    if (this.documento.operationStatus === this.PROCESSING) {
                        this.pollDocumentStatus();
                    } else {
                        this.stopStatusPolling();
                    }
                }, this.pollingDelay);
            }
        },

        stopStatusPolling() {
            if (this.pollingIntervalId) {
                clearInterval(this.pollingIntervalId);
                this.pollingIntervalId = null;
            }
        },
        async onDocumentoAnalyzedAsync(e) {
            if (!this.errorBag.hasErrors()) {
                await this.getAsync(e[0]);
            }
        },
        setLayout(mode) {
            this.currentLayoutMode = mode;
            this.currentLayoutMode.apply(this.documentoArchivoDivId, this.documentoFormularioDivId);
        },
        async init() {
            // Si no hay permisos de modificación, volvemos a la lista
            if (!this.grants.update) this.$router.push({ name: "home" });
            else {
                if (this.$route.params.id) await this.getAsync(this.$route.params.id);
                else this.cancel();

                if (this.$route.query.fromAnalysis) {
                    await this.updateAsync().then(() => {
                        const query = Object.assign({}, this.$route.query);
                        delete query.fromAnalysis;
                        this.$router.replace({ query });
                    });
                }
            }
        },
        async getAsync(id) { // Llamado en init y después de @archivosUpdated
    if (!id && this.documento.id) id = this.documento.id;
    if (!id) return;

    this.uiService.showSpinner(true);
    try {
        const res = await this.$store.dispatch("getDocumentoAsync", id);
        const previousStatus = this.documento.operationStatus;
        
        this.documento = new Documento(res);
        this.tipoDoc = this.documento.tipo;

        if (this.documento.operationStatus === this.PROCESSING) {
            if (previousStatus !== this.PROCESSING) {
                this.startAiMessageCycling();
                this.previousOperationStatusForMessage = this.PROCESSING;
            }
            this.startStatusPolling();
            this.iframeHeight = '30vh';
        } else { // COMPLETED, FAILED, o cualquier otro estado que no sea PROCESSING
            this.stopAiMessageCycling();
            this.stopStatusPolling();
            this.iframeHeight = '120vh';
            if (previousStatus === this.PROCESSING) { //
                if (this.documento.operationStatus === this.COMPLETED) {
                    this.uiService.showMessageSuccess(this.loc["Documento analizado con éxito"]);
                } else if (this.documento.operationStatus === this.FAILED) {
                    this.uiService.showMessageError(this.loc["Error en el análisis del documento"]);
                }
            } else {
                 this.previousOperationStatusForMessage = null;
            }
        }
    } catch (error) {
        console.error(`${this.loc["Error en getAsync para el ID"]} ${id}:`, error);
        this.uiService.showMessageError(this.loc["Error al obtener datos del documento."]);
        this.stopStatusPolling();
        this.stopAiMessageCycling();
        this.iframeHeight = '120vh';
        this.previousOperationStatusForMessage = null;
    } finally {
        this.uiService.showSpinner(false);
    }
},

async getOperationStatusAsync(id) { // Llamado por el poller
    try {
        const res = await this.$store.dispatch("getDocumentoAsync", id);
        const previousPollingStatus = this.documento.operationStatus;

        // Solo actualizamos los campos que el polling necesita para evitar sobreescribir datos del formulario
        this.documento.operationId = res.operationId;
        this.documento.operationStatus = res.operationStatus;
        this.documento.rowVersion = res.rowVersion;

        if (res.operationStatus === this.COMPLETED) {
            this.documento.fechaDesde = res.fechaDesde ? new Date(res.fechaDesde).toISOString().split('T')[0] : this.documento.fechaDesde; // No sobreescribir si ya tiene valor
            this.documento.fechaHasta = res.fechaHasta ? new Date(res.fechaHasta).toISOString().split('T')[0] : this.documento.fechaHasta;
            if (res.archivoURL && !this.documento.archivoURL) {
                this.documento.archivoURL = res.archivoURL;
            }
        }

        if (this.documento.operationStatus === this.PROCESSING) {
            if (previousPollingStatus !== this.PROCESSING) {
                this.startAiMessageCycling();
                this.previousOperationStatusForMessage = this.PROCESSING;
            }
            this.iframeHeight = '30vh';
        } else { // COMPLETED o FAILED
            this.stopAiMessageCycling();
            this.stopStatusPolling();
            this.iframeHeight = '120vh';
            if (previousPollingStatus === this.PROCESSING) {
                if (this.documento.operationStatus === this.COMPLETED) {
                    this.uiService.showMessageSuccess(this.loc["Documento analizado con éxito"]);
                } else if (this.documento.operationStatus === this.FAILED) {
                    this.uiService.showMessageError(this.loc["Error en el análisis del documento"]);
                }
            } else {
                 this.previousOperationStatusForMessage = null;
            }
        }
    } catch (error) {
        console.error(`${this.loc["Error en getOperationStatusAsync para el ID"]} ${id}:`, error);
    }
},
        // completarFormularioDocumento(e) {
        //     this.errorBag.clear();

        //     this.documento = new Documento(e[0]);

        // },
        // goDetail() {
        //     this.$router.push({ name: "detail", params: { id: this.comprobante.id } });
        // },
        cancel() {
            this.$router.push({ name: "edit", params: { id: this.documento.solicitudId } });
        },
        goSolicitudEdit() {
            this.$router.push({ name: "edit", params: { id: this.documento.solicitudId } });
        },
        async updateAsync() {
            this.errorBag.clear();
            this.uiService.showSpinner(true)
            return await this.$store.dispatch("putDocumentoAsync", this.documento)
                .then(() => {
                    if (!this.errorBag.hasErrors()) {
                        this.uiService.showMessageSuccess(loc["Operación confirmada"])
                        this.goSolicitudEdit();
                    } else {
                        this.uiService.showMessageError(loc["Operación rechazada"])
                    }
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        async saveAsync() {
            this.errorBag.clear();
            this.uiService.showSpinner(true);
            await this.$store.dispatch("updateDocumentoDraftAsync", this.documento)
                .then(() => {
                    if (!this.errorBag.hasErrors()) {
                        this.uiService.showMessageSuccess(loc["Operación confirmada"])
                        this.goSolicitudEdit();
                    } else {
                        this.uiService.showMessageError(loc["Operación rechazada"])
                    }
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        }
    }
};
</script>
<!-- 
<style>
#__comprobanteDetalles td.border-danger-gs {
    border: 3px solid #dc3545 !important;
}

#__comprobanteImpuestos td.border-danger-gs {
    border: 3px solid #dc3545 !important;
}

#__comprobantePercepciones td.border-danger-gs {
    border: 3px solid #dc3545 !important;
}
</style> -->

<style>
.btn-group .btn.btn-light.active {
    background-color: #0d8b8b;
    color: white;
    border-color: #0d8b8b;
}

.btn-group .btn.btn-light:not(.active):hover {
    background-color: #e9ecef;
}

.btn-group .btn.btn-light:not(.active):focus {
    box-shadow: 0 0 0 0.2rem rgba(#0d8b8b, 0.25);
}

.ai-animated-icon-container {
    display: flex;
    justify-content: center;
    align-items: center;
    min-height: 60px;
    /* Espacio para el icono y su animación */
    /* width: 100%; si necesitas que ocupe todo el ancho */
}

/* Estilos para el icono animado */
.ai-animated-icon {
    font-size: 3.0rem;
    /* Ajusta el tamaño del icono aquí */
    display: inline-block;
    line-height: 1;
    animation: aiIconAnimate 2.5s infinite ease-in-out;
    /* El color inicial lo tomará del primer paso del keyframe */
}

@keyframes aiIconAnimate {
    0% {
        transform: scale(0.9);
        opacity: 0.7;
        color: #FFD700;
        /* Gold - Color inicial */
        text-shadow: 0 0 6px rgba(255, 215, 0, 0.5);
        /* Sombra/brillo dorado */
    }

    25% {
        transform: scale(1.15);
        opacity: 1;
        color: #00FFFF;
        /* Aqua/Cyan - Segundo color */
        text-shadow: 0 0 10px rgba(0, 255, 255, 0.6);
    }

    50% {
        transform: scale(0.95);
        opacity: 0.8;
        color: #FF00FF;
        /* Fuchsia/Magenta - Tercer color */
        text-shadow: 0 0 8px rgba(255, 0, 255, 0.5);
    }

    75% {
        transform: scale(1.1);
        opacity: 1;
        color: #9370DB;
        /* MediumPurple - Cuarto color */
        text-shadow: 0 0 10px rgba(147, 112, 219, 0.6);
    }

    100% {
        transform: scale(0.9);
        opacity: 0.7;
        color: #FFD700;
        /* Gold - De vuelta al color inicial */
        text-shadow: 0 0 6px rgba(255, 215, 0, 0.5);
    }
}

.sticky-header {
    position: sticky;
    top: 0;
    background-color: white;
    z-index: 1020;
    padding-bottom: 0.5rem;
    border-bottom: 1px solid #eee;
}

.col-md-6[style*="height"] {
    transition: height 0.3s ease-in-out;
}
</style>