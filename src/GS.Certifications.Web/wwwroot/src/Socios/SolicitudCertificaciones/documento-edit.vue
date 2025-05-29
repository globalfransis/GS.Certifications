<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 d-flex justify-content-between sticky-header mt-4">
                <div class="col-12 d-grid">
                    <div class="row">
                        <p class="h5 col-6">{{ tipoDoc }} - {{loc["Solicitud nro."]}} {{ documento.solicitudId }}
                        </p>
                        <div class="col-6 gap-4 d-flex justify-content-end">
                            <documentoEstado-label :value="documento.estadoId" />

                            <div :key="`operationStatus-${documento.operationStatus}`"
                                v-if="documento.operationStatus == this.PROCESSING" class="d-flex align-items-center text-primary gap-1">
                                <div :key="`operationStatus-${documento.operationStatus}`"
                                    v-if="documento.operationStatus == this.PROCESSING"
                                    class=" spinner-border spinner-border-sm text-primary" role="status">
                                    <span class="visually-hidden">Analizando documento...</span>
                                </div>
                                <span>Analizando documento...</span>
                            </div>

                            <div :key="`operationStatus-${documento.operationStatus}`"
                                v-if="documento.operationStatus == this.COMPLETED"
                                class="d-flex align-items-center text-success gap-1">
                                <i class="fas fa-check-circle"></i>
                                <span>Documento analizado</span>
                            </div>

                            <div :key="`operationStatus-${documento.operationStatus}`"
                                v-if="documento.operationStatus == this.FAILED"
                                class="d-flex align-items-center text-danger gap-1">
                                <i class="fas fa-times-circle"></i>
                                <span>Error de análisis</span>
                            </div>
                            <cancel-button class="ms-2" @click="cancel">{{loc["Volver"]}}</cancel-button>
                        </div>
                    </div>

                    <div class="btn-group btn-group-sm col-1" role="group"
                        aria-label="Controles de visualización del documento">
                        <button type="button" class="btn btn-light"
                            :class="{ 'active': currentLayoutMode === LayoutMode.Split }"
                            @click="setLayout(LayoutMode.Split)" title="Mostrar vista dividida"
                            aria-label="Mostrar vista dividida" data-bs-toggle="tooltip" data-bs-placement="bottom">
                            <i class="fas fa-columns" aria-hidden="true"></i>
                        </button>
                        <button type="button" class="btn btn-light"
                            :class="{ 'active': currentLayoutMode === LayoutMode.File }"
                            @click="setLayout(LayoutMode.File)" title="Mostrar solo el documento"
                            aria-label="Mostrar solo el documento" data-bs-toggle="tooltip" data-bs-placement="bottom">
                            <i class="fas fa-file-alt" aria-hidden="true"></i>
                        </button>
                        <button type="button" class="btn btn-light"
                            :class="{ 'active': currentLayoutMode === LayoutMode.Form }"
                            @click="setLayout(LayoutMode.Form)" title="Mostrar solo el formulario"
                            aria-label="Mostrar solo el formulario" data-bs-toggle="tooltip" data-bs-placement="bottom">
                            <i class="fas fa-list-alt" aria-hidden="true"></i>
                        </button>
                    </div>
                    <!-- 
                    <div>
                        <a id="splitLayoutButton" @click="setLayout(LayoutMode.Split)" title="Mostrar vista dividida"
                            data-toggle="tooltip" class="ms-2 btn btn-primary px-2 py-1">
                            <i class="fas fa-columns" aria-hidden="true"></i>
                        </a>
                        <a id="fileLayoutButton" @click="setLayout(LayoutMode.File)" title="Mostrar solo el documento"
                            data-toggle="tooltip" class="btn btn-primary px-2 py-1">
                            <i class="fas fa-file-alt" aria-hidden="true"></i>
                        </a>
                        <a id="formLayoutButton" @click="setLayout(LayoutMode.Form)" title="Mostrar solo el formulario"
                            data-toggle="tooltip" class="btn btn-primary px-2 py-1">
                            <i class="fas fa-list-alt" aria-hidden="true"></i>
                        </a>
                    </div> -->
                </div>
            </div>
            <div class="card mt-2">
                <div class="card-body">

                    <div class="form-group col-sm-12 mb-4 row">
                        <div class="col-8">
                            <label class="control-label">{{loc["Importar Documento"]}}</label>
                            <importar-documento idModal="__modal_DocumentoArchivo" ref="importarDocumento"
                                title="Documento" :disabled="!updateGrant" :documentoId="documento.id"
                                :solicitudId="documento.solicitudId" :fileName="documento.archivoURL"
                                @archivosUpdated="onDocumentoAnalyzed($event)" />
                            <span class="text-danger field-validation-error">
                                {{ errorBag.get("documentoError") }}
                            </span>
                        </div>
                    </div>

                    <hr>

                    <div class="row">
                        <div :id="documentoArchivoDivId" class="col-6" style="height: 150vh;">
                            <iframe v-if="documento.archivoURL"
                                :src="`${currentLocation}/CertificacionesWebRepository/Uploads/Solicitudes/Solicitud_${documento.solicitudGuid}/Doc_${documento.guid}/${documento.archivoURL}`"
                                style="width: 100%; height: 100%; border: none;">
                            </iframe>
                        </div>
                        <div :id="documentoFormularioDivId" class="col-6">
                            <div class="form-group col-lg-6 col-sm-12 mb-2 required">
                                <label class="control-label">{{loc["Fecha Desde"]}}</label>
                                <input type="date" class="form-control" v-model="documento.fechaDesde">
                                <span class="text-danger field-validation-error">
                                    {{ errorBag.get("vigencia") }}
                                </span>
                            </div>
                            <div class="form-group col-lg-6 col-sm-12 mb-2 required">
                                <label class="control-label">{{loc["Fecha Hasta"]}}</label>
                                <input type="date" class="form-control" v-model="documento.fechaHasta">
                                <span class="text-danger field-validation-error">
                                    {{ errorBag.get("fechaHasta") }}
                                </span>
                            </div>
                            <div class="form-group col-lg-12 col-sm-12 mb-2">
                                <label class="control-label">{{loc["Observaciones"]}}</label>
                                <textarea class="form-control" cols="50" rows="25"
                                    v-model="documento.observaciones"></textarea>
                                <span class="text-danger field-validation-error">
                                    {{ errorBag.get("observaciones") }}
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12 d-flex justify-content-end gap-2 mb-3 mt-3" v-if="currentLayoutMode != LayoutMode.File">
                <button @click.prevent="saveAsync"
                    :disabled="documento.operationStatus == PROCESSING || documento.propietarioActualId != BACKOFFICE && documento.estadoId != DOCUMENTO_PENDIENTE"
                    class="btn btn-secondary btn-sm">
                    <i class="fas fa-save"></i>
                    {{loc["Guardar"]}}
                </button>

                <accept-button @click="updateAsync"
                    :disabled="documento.operationStatus == PROCESSING || documento.propietarioActualId != BACKOFFICE && documento.estadoId != DOCUMENTO_PRESENTADO"
                    v-if="documento.estadoId == DOCUMENTO_PRESENTADO">
                    {{loc["Validar"]}}
                </accept-button>

                <button
                    :disabled="documento.propietarioActualId != BACKOFFICE && documento.estadoId != DOCUMENTO_PENDIENTE"
                    v-if="documento.estadoId == DOCUMENTO_PRESENTADO" class="btn btn-outline-danger btn-sm"
                    @click="rejectAsync" title="Rechazar solicitud">
                    {{loc["Rechazar"]}}
                </button>

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
            pollingDelay: 3000, // Tiempo en milisegundos para el polling 

        };
    },
    computed: {
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
                console.error("Error durante el polling del estado del documento:", error);
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
        onDocumentoAnalyzed(e) {
            if (!this.errorBag.hasErrors()) {
                this.getAsync(e[0]);
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
                        this.uiService.showMessageSuccess("Análisis de IA completado!"); // O un mensaje más específico
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
                        this.uiService.showMessageSuccess("Análisis de IA completado!");
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
            if (
                await this.uiService.confirmActionModal(
                    "¿Está usted seguro que desea aprobar esta solicitud?",
                    "Aceptar",
                    "Cancelar"
                )
            ) {
                this.errorBag.clear();
                this.uiService.showSpinner(true)
                return await this.$store.dispatch("putDocumentoAsync", this.documento)
                    .then(() => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            this.goSolicitudEdit();
                        } else {
                            this.uiService.showMessageError("Operación rechazada")
                        }
                    })
                    .finally(() => {
                        this.uiService.showSpinner(false);
                    });
            }
        },
        async rejectAsync() {
            if (
                await this.uiService.confirmActionModal(
                    "¿Está usted seguro que desea rechazar esta solicitud?",
                    "Aceptar",
                    "Cancelar"
                )
            ) {
                this.errorBag.clear();
                this.uiService.showSpinner(true)
                return await this.$store.dispatch("rejectDocumentoAsync", this.documento)
                    .then(() => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            this.goSolicitudEdit();
                        } else {
                            this.uiService.showMessageError("Operación rechazada")
                        }
                    })
                    .finally(() => {
                        this.uiService.showSpinner(false);
                    });
            }
        },
        async saveAsync() {
            this.errorBag.clear();
            this.uiService.showSpinner(true);
            await this.$store.dispatch("updateDocumentoDraftAsync", this.documento)
                .then(() => {
                    if (!this.errorBag.hasErrors()) {
                        this.uiService.showMessageSuccess("Operación confirmada")
                        this.goSolicitudEdit();
                    } else {
                        this.uiService.showMessageError("Operación rechazada")
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