<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 d-flex justify-content-between sticky-header mt-4 align-items-start">
                <div class="col-12 d-grid">
                    <div class="row align-items-center">
                        <p class="h5 col-md-6 col-12 mb-md-0 mb-2">{{ tipoDoc }}</p>
                        <div class="col-md-6 col-12 gap-3 d-flex justify-content-md-end align-items-center">
                            <documentoEstado-label :value="documento.estadoId" />

                            <div :key="`operationStatus-${documento.operationStatus}-processing`"
                                v-if="documento.operationStatus == PROCESSING"
                                class="d-flex align-items-center text-primary gap-1">
                                <div class="spinner-border spinner-border-sm text-primary" role="status">
                                    <span class="visually-hidden">{{ loc["Analizando documento..."] }}</span>
                                </div>
                                <span>{{ loc["Analizando documento..."] }}</span>
                            </div>
                            <div :key="`operationStatus-${documento.operationStatus}-completed`"
                                v-if="documento.operationStatus == COMPLETED"
                                class="d-flex align-items-center text-success gap-1">
                                <i class="fas fa-check-circle"></i>
                                <span>{{ loc["Documento analizado"] }}</span>
                            </div>
                            <div :key="`operationStatus-${documento.operationStatus}-failed`"
                                v-if="documento.operationStatus == FAILED"
                                class="d-flex align-items-center text-danger gap-1">
                                <i class="fas fa-times-circle"></i>
                                <span>{{ loc["Error de análisis"] }}</span>
                            </div>
                            <cancel-button class="ms-2" @click="cancel">{{ loc["Volver"] }}</cancel-button>
                        </div>
                    </div>

                    <div class="btn-group btn-group-sm mt-2 col-1" v-if="documento.archivoURL" role="group"
                        aria-label="Controles de visualización del documento">
                        <button type="button" class="btn btn-light"
                            :class="{ 'active': currentLayoutMode === LayoutMode.Split }"
                            @click="setLayout(LayoutMode.Split)" :title="loc['Mostrar vista dividida']"
                            aria-label="Mostrar vista dividida" data-bs-toggle="tooltip" data-bs-placement="bottom">
                            <i class="fas fa-columns" aria-hidden="true"></i>
                        </button>
                        <button type="button" class="btn btn-light"
                            :class="{ 'active': currentLayoutMode === LayoutMode.File }"
                            @click="setLayout(LayoutMode.File)" :title="loc['Mostrar solo el documento']"
                            aria-label="Mostrar solo el documento" data-bs-toggle="tooltip" data-bs-placement="bottom">
                            <i class="fas fa-file-alt" aria-hidden="true"></i>
                        </button>
                        <button type="button" class="btn btn-light"
                            :class="{ 'active': currentLayoutMode === LayoutMode.Form }"
                            @click="setLayout(LayoutMode.Form)" :title="loc['Mostrar solo el formulario']"
                            aria-label="Mostrar solo el formulario" data-bs-toggle="tooltip" data-bs-placement="bottom">
                            <i class="fas fa-list-alt" aria-hidden="true"></i>
                        </button>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <div v-if="!documento.archivoURL && documento.id" class="text-center p-lg-5 p-md-4 p-3">
                        <div class="card shadow-sm bg-light"
                            style="margin: auto; border-radius: 0.5rem;">
                            <div class="card-body">
                                <i class="fas fa-file-upload fa-3x text-primary mb-3"></i>
                                <h5 class="card-title mb-3">{{ loc["Cargar Documento Requerido"] }}</h5>
                                <p class="text-muted">
                                    {{ loc["Para continuar, por favor seleccioná el archivo para"] }} <strong
                                        v-if="tipoDoc">{{ tipoDoc }}</strong><strong v-else>{{ loc['el documento actual'] }}</strong>.
                                    <br>
                                    {{ loc["Una vez cargado, se analizará para extraer información relevante."] }}
                                </p>
                                <div class="mt-4 mb-3">
                                    <importar-documento idModal="__modal_DocumentoArchivo_empty"
                                        ref="importarDocumentoEmpty" :title="loc['Seleccionar archivo...']"
                                        :disabled="!grants.update" :documentoId="documento.id"
                                        :solicitudId="documento.solicitudId" :fileName="documento.archivoURL"
                                        @archivosUpdated="onDocumentoAnalyzedAsync($event)" />
                                    <span class="text-danger field-validation-error d-block mt-2">
                                        {{ errorBag.get("documentoError") }}
                                    </span>
                                </div>
                            </div>
                        </div>
                        <hr class="my-4">
                        <p class="text-muted text-center mb-3 fst-italic">
                            {{ loc["Los campos del formulario se habilitarán y/o completarán después del análisis del documento."] }}
                        </p>
                    </div>

                    <div v-else-if="documento.id">
                        <div class="form-group col-sm-12 mb-4 row">
                            <div class="col-lg-8 col-md-10">
                                <label class="control-label">{{ loc["Cambiar Documento"] }}</label>
                                <importar-documento idModal="__modal_DocumentoArchivo_loaded"
                                    ref="importarDocumentoLoaded" :title="loc['Cambiar archivo...']"
                                    :disabled="!grants.update" :documentoId="documento.id"
                                    :solicitudId="documento.solicitudId" :fileName="documento.archivoURL"
                                    @archivosUpdated="onDocumentoAnalyzedAsync($event)" />
                                <span class="text-danger field-validation-error">
                                    {{ errorBag.get("documentoError") }}
                                </span>
                            </div>
                        </div>
                        <hr>
                    </div>

                    <div class="row" v-if="documento.id">
                        <div :id="documentoArchivoDivId" class="col-md-6" style="height: 120vh;"> <iframe
                                v-if="iframeSrc" :src="iframeSrc" style="width: 100%; height: 100%; border: none;">
                            </iframe>
                            <div v-else-if="documento.archivoURL && !iframeSrc"
                                class="alert alert-warning text-center d-flex align-items-center justify-content-center h-100">
                                <span><i class="fas fa-exclamation-triangle me-2"></i>{{ loc["No se puede mostrar el documento. La URL podría ser inválida o el archivo no está accesible."] }}</span>
                            </div>
                            <div v-else-if="!documento.archivoURL"
                                class="alert alert-light text-center d-flex align-items-center justify-content-center h-100 border">
                                <span><i class="fas fa-eye-slash me-2"></i>{{ loc["No hay documento cargado para visualizar."] }}</span>
                            </div>
                        </div>
                        <div :id="documentoFormularioDivId" class="col-md-6">
                            <div class="form-group col-lg-8 col-sm-12 mb-3 required"> <label
                                    class="control-label">{{ loc["Fecha Desde"] }}</label>
                                <input :disabled="documento.operationStatus == PROCESSING || !grants.update" type="date"
                                    class="form-control" v-model="documento.fechaDesde">
                                <span class="text-danger field-validation-error">{{ errorBag.get("fechaDesde") }}</span>
                            </div>
                            <div class="form-group col-lg-8 col-sm-12 mb-3 required"> <label
                                    class="control-label">{{ loc["Fecha Hasta"] }}</label>
                                <input :disabled="documento.operationStatus == PROCESSING || !grants.update" type="date"
                                    class="form-control" v-model="documento.fechaHasta">
                                <span class="text-danger field-validation-error">{{ errorBag.get("fechaHasta") }}</span>
                            </div>
                            <div class="form-group col-lg-12 col-sm-12 mb-3">
                                <label class="control-label">{{ loc["Observaciones"] }}</label>
                                <textarea class="form-control" cols="50" rows="10" :disabled="!grants.update"
                                    v-model="documento.observaciones"></textarea>
                                <span class="text-danger field-validation-error">{{ errorBag.get("observaciones")
                                    }}</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-12 d-flex justify-content-end gap-2 mb-3 mt-3"
                v-if="currentLayoutMode != LayoutMode.File && documento.id">
                <button @click.prevent="saveAsync" class="btn btn-secondary btn-sm"
                    :disabled="documento.operationStatus == PROCESSING || (!grants.update || documento.propietarioActualId == SOCIOS && documento.estadoId != DOCUMENTO_PRESENTADO) || (!grants.update || documento.propietarioActualId == BACKOFFICE && documento.estadoId == DOCUMENTO_PRESENTADO)">
                    <i class="fas fa-save"></i>
                    {{ loc["Guardar"] }}
                </button>
                <accept-button @click="updateAsync"
                    :disabled="documento.operationStatus == PROCESSING || (!grants.update || documento.propietarioActualId == SOCIOS && documento.estadoId != DOCUMENTO_PRESENTADO) || (!grants.update || documento.propietarioActualId == BACKOFFICE && documento.estadoId == DOCUMENTO_PRESENTADO)">
                    {{loc["Aceptar"]}}</accept-button>
                <cancel-button @click="cancel">{{loc["Volver"]}}</cancel-button>
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
        documentoEstadoLabel
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
        };
    },
    computed: {
        iframeSrc() {
            if (this.documento && this.documento.archivoURL && this.documento.solicitudGuid && this.documento.guid) {
                try {
                    // Validar que la URL es construible y segura si es necesario
                    const url = `${this.currentLocation}/CertificacionesWebRepository/Uploads/Solicitudes/Solicitud_${this.documento.solicitudGuid}/Doc_${this.documento.guid}/${this.documento.archivoURL}`;
                    // new URL(url); // Esto podría usarse para validar la URL si es completa
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
        await this.init();
    },
    beforeDestroy() {
        this.stopStatusPolling();
    },
    methods: {
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
        async getAsync(id) {
            this.uiService.showSpinner(true)
            await this.$store.dispatch("getDocumentoAsync", id)
                .then((res) => {
                    const previousStatus = this.documento.operationStatus;
                    this.documento = new Documento(res);
                    this.tipoDoc = this.documento.tipo;

                    if (this.documento.operationStatus === this.PROCESSING) {
                        this.startStatusPolling();
                    } else if (previousStatus === this.PROCESSING && this.documento.operationStatus !== this.PROCESSING) {
                        this.uiService.showMessageSuccess(loc["Análisis de IA completado!"]); // O un mensaje más específico
                        this.stopStatusPolling();
                    }
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        async getOperationStatusAsync(id) {
            await this.$store.dispatch("getDocumentoAsync", id)
                .then((res) => {
                    const previousStatus = this.documento.operationStatus;
                    this.documento.operationId = res.operationId;
                    this.documento.operationStatus = res.operationStatus;
                    this.documento.fechaDesde = res.fechaDesde ? new Date(res.fechaDesde).toISOString().split('T')[0] : null;
                    this.documento.fechaHasta = res.fechaHasta ? new Date(res.fechaHasta).toISOString().split('T')[0] : null;
                    this.documento.rowVersion = res.rowVersion;

                    if (this.documento.operationStatus === this.PROCESSING) {
                        this.startStatusPolling();
                    } else if (previousStatus === this.PROCESSING && this.documento.operationStatus !== this.PROCESSING) {
                        this.uiService.showMessageSuccess(loc["Análisis de IA completado!"]);
                        this.stopStatusPolling();
                    }
                });
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
</style>