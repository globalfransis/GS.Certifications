<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 d-flex justify-content-between sticky-header mt-4">
                <div class="col-12 d-grid">
                    <div class="row">
                        <p class="h5 col-6">{{ tipoDoc }} - {{ loc["Solicitud nro."] }} {{ documento.solicitudId }}
                        </p>
                        <div class="col-6 gap-4 d-flex justify-content-end">
                            <documentoEstado-label :value="documento.estadoId" />

                            <div :key="`operationStatus-${documento.operationStatus}`"
                                v-if="documento.operationStatus == this.PROCESSING"
                                class="d-flex align-items-center text-primary gap-1">
                                <div :key="`operationStatus-${documento.operationStatus}`"
                                    v-if="documento.operationStatus == this.PROCESSING"
                                    class=" spinner-border spinner-border-sm text-primary" role="status">
                                    <span class="visually-hidden">{{ loc["Analizando documento..."] }}</span>
                                </div>
                                <span>{{ loc["Analizando documento..."] }}</span>
                            </div>

                            <div :key="`operationStatus-${documento.operationStatus}`"
                                v-if="documento.operationStatus == this.COMPLETED"
                                class="d-flex align-items-center text-success gap-1">
                                <i class="fas fa-check-circle"></i>
                                <span>{{ loc["Documento analizado"] }}</span>
                            </div>

                            <div :key="`operationStatus-${documento.operationStatus}`"
                                v-if="documento.operationStatus == this.FAILED"
                                class="d-flex align-items-center text-danger gap-1">
                                <i class="fas fa-times-circle"></i>
                                <span>{{ loc["Error de análisis"] }}</span>
                            </div>
                            <cancel-button class="ms-2" @click="cancel">{{ loc["Volver"] }}</cancel-button>
                        </div>
                    </div>

                    <div class="btn-group btn-group-sm col-1" role="group"
                        aria-label="Controles de visualización del documento">
                        <button type="button" class="btn btn-light"
                            :class="{ 'active': currentLayoutMode === LayoutMode.Split }"
                            @click="setLayout(LayoutMode.Split)" :title="loc['Mostrar vista dividida']"
                            :aria-label="loc['Mostrar vista dividida']" data-bs-toggle="tooltip"
                            data-bs-placement="bottom">
                            <i class="fas fa-columns" aria-hidden="true"></i>
                        </button>
                        <button type="button" class="btn btn-light"
                            :class="{ 'active': currentLayoutMode === LayoutMode.File }"
                            @click="setLayout(LayoutMode.File)" :title="loc['Mostrar solo el documento']"
                            :aria-label="loc['Mostrar solo el documento']" data-bs-toggle="tooltip"
                            data-bs-placement="bottom">
                            <i class="fas fa-file-alt" aria-hidden="true"></i>
                        </button>
                        <button type="button" class="btn btn-light"
                            :class="{ 'active': currentLayoutMode === LayoutMode.Form }"
                            @click="setLayout(LayoutMode.Form)" :title="loc['Mostrar solo el formulario']"
                            :aria-label="loc['Mostrar solo el formulario']" data-bs-toggle="tooltip"
                            data-bs-placement="bottom">
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
                            <label class="control-label">{{ loc["Importar Documento"] }}</label>
                            <importar-documento idModal="__modal_DocumentoArchivo" ref="importarDocumento"
                                :title="loc['Documento']" :disabled="!updateGrant" :documentoId="documento.id"
                                :solicitudId="documento.solicitudId" :fileName="documento.archivoURL"
                                @archivosUpdated="onDocumentoAnalyzed($event)" />
                            <span class="text-danger field-validation-error">
                                {{ errorBag.get("documentoError") }}
                            </span>
                        </div>
                    </div>

                    <hr>

                    <div class="row">
                        <div :id="documentoArchivoDivId" class="col-6">
                            <iframe v-if="documento.archivoURL"
                                :src="`${currentLocation}/CertificacionesWebRepository/Uploads/Solicitudes/Solicitud_${documento.solicitudGuid}/Doc_${documento.guid}/${documento.archivoURL}`"
                                style="width: 100%; height: 100%; border: none;">
                            </iframe>
                        </div>
                        <div :id="documentoFormularioDivId" class="row col-md-6 col-6">
                            <!-- <div class="form-group col-lg-6 col-sm-12 mb-2 required">
                                <label class="control-label">{{ loc["Fecha Desde"] }}</label>
                                <input type="date" class="form-control" v-model="documento.fechaDesde">
                                <span class="text-danger field-validation-error">
                                    {{ errorBag.get("vigencia") }}
                                </span>
                            </div>
                            <div class="form-group col-lg-6 col-sm-12 mb-2 required">
                                <label class="control-label">{{ loc["Fecha Hasta"] }}</label>
                                <input type="date" class="form-control" v-model="documento.fechaHasta">
                                <span class="text-danger field-validation-error">
                                    {{ errorBag.get("fechaHasta") }}
                                </span>
                            </div>
                            <div class="form-group col-lg-12 col-sm-12 mb-2">
                                <label class="control-label">{{ loc["Observaciones"] }}</label>
                                <textarea class="form-control" cols="50" rows="25"
                                    v-model="documento.observaciones"></textarea>
                                <span class="text-danger field-validation-error">
                                    {{ errorBag.get("observaciones") }}
                                </span>
                            </div>

                            <div v-if="documento.estadoId == DOCUMENTO_RECHAZADO" class="form-group col-lg-12 col-sm-12 mb-4">
                                <label class="control-label">{{ loc["Motivo Rechazo"] }}</label>
                                <textarea id="motivoRechazoTxtArea" disabled class="form-control" enable cols="20"
                                    rows="4" v-model="documento.motivoRechazo"></textarea>
                            </div> -->
                            <div class="form-group col-6 col-lg-6 col-sm-6 mb-3 required">
                                <label class="control-label">{{ loc["Fecha Desde"] }}</label>
                                <input :disabled="documento.operationStatus == PROCESSING || !grants.update" type="date" class="form-control form-control-sm" v-model="documento.fechaDesde">
                                <span class="text-danger field-validation-error small">{{ errorBag.get("fechaDesde") }}</span>
                                <span class="text-danger field-validation-error small">{{ errorBag.get("vigencia") }}</span>
                            </div>
                            <div class="form-group col-6 col-lg-6 col-sm-6 mb-3 required">
                                <label class="control-label">{{ loc["Fecha Hasta"] }}</label>
                                <input :disabled="documento.operationStatus == PROCESSING || !grants.update" type="date" class="form-control form-control-sm" v-model="documento.fechaHasta">
                                <span class="text-danger field-validation-error small">{{ errorBag.get("fechaHasta") }}</span>
                            </div>
                            <div class="col-12 col-lg-12 col-sm-12 mb-3">
                                <label class="control-label">{{ loc["Observaciones"] }}</label>
                                <textarea class="form-control form-control-sm" rows="25" :disabled="!grants.update" v-model="documento.observaciones"></textarea>
                                <span class="text-danger field-validation-error small">{{ errorBag.get("observaciones") }}</span>
                            </div>
                            <div v-if="documento.estadoId == DOCUMENTO_RECHAZADO" class="form-group col-lg-12 col-sm-12 mb-4">
                                <label class="control-label">{{ loc["Motivo Rechazo"] }}</label>
                                <textarea id="motivoRechazoTxtArea" disabled class="form-control" enable cols="20"
                                    rows="4" v-model="documento.motivoRechazo"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12 d-flex justify-content-end gap-2 mb-3 mt-3" v-if="currentLayoutMode != LayoutMode.File">
                <!-- <button @click.prevent="saveAsync"
                    :disabled="documento.operationStatus == PROCESSING || documento.propietarioActualId != BACKOFFICE && documento.estadoId != DOCUMENTO_PENDIENTE"
                    class="btn btn-secondary btn-sm">
                    <i class="fas fa-save"></i>
                    {{loc["Guardar"]}}
                </button> -->

                <accept-button @click="updateAsync"
                    :disabled="documento.operationStatus == PROCESSING || documento.propietarioActualId != BACKOFFICE && documento.estadoId != DOCUMENTO_PRESENTADO"
                    v-if="documento.estadoId == DOCUMENTO_PRESENTADO">
                    {{ loc["Validar"] }}
                </accept-button>

                <a :disabled="documento.propietarioActualId != BACKOFFICE && documento.estadoId != DOCUMENTO_PENDIENTE"
                    v-if="documento.estadoId == DOCUMENTO_PRESENTADO" class="btn btn-outline-danger btn-sm"
                    :title="loc['Rechazar documento']" data-toggle="tooltip" data-bs-toggle="modal"
                    :data-bs-target="`#${documentoRechazoId}-${documento.id}`" style="cursor:pointer">
                    {{ loc["Rechazar"] }}
                </a>

                <cancel-button @click="cancel">{{ loc["Volver"] }}</cancel-button>
            </div>
        </div>

        <documentoRechazo-modal :documento="documento" @documentoRechazado="rejectAsync($event)"
            :idModal="`${documentoRechazoId}-${documento.id}`" />

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

import documentoRechazoModal from './Modal/documentoRechazo-modal'

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
        documentoEstadoLabel,
        documentoRechazoModal
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
            documentoRechazoId: "documentoRechazoModal",
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
        // onDocumentoAnalyzed(e) {
        //     if (!this.errorBag.hasErrors()) {
        //         this.getAsync(e[0]);
        //     }
        // },
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
        // async getAsync(id) {
        //     this.uiService.showSpinner(true)
        //     await this.$store.dispatch("getDocumentoAsync", id)
        //         .then((res) => {
        //             const previousStatus = this.documento.operationStatus;
        //             this.documento = new Documento(res);
        //             this.tipoDoc = this.documento.tipo;

        //             if (this.documento.operationStatus === this.PROCESSING) {
        //                 this.startStatusPolling();
        //             } else if (previousStatus === this.PROCESSING && this.documento.operationStatus !== this.PROCESSING) {
        //                 this.uiService.showMessageSuccess(loc["Análisis de IA completado!"]); // O un mensaje más específico
        //                 this.stopStatusPolling();
        //             }
        //         })
        //         .finally(() => {
        //             this.uiService.showSpinner(false);
        //         });
        // },
        // async getOperationStatusAsync(id) {
        //     await this.$store.dispatch("getDocumentoAsync", id)
        //         .then((res) => {
        //             const previousStatus = this.documento.operationStatus;
        //             this.documento.operationId = res.operationId;
        //             this.documento.operationStatus = res.operationStatus;
        //             this.documento.fechaDesde = res.fechaDesde ? new Date(res.fechaDesde).toISOString().split('T')[0] : null;
        //             this.documento.fechaHasta = res.fechaHasta ? new Date(res.fechaHasta).toISOString().split('T')[0] : null;
        //             this.documento.rowVersion = res.rowVersion;

        //             if (this.documento.operationStatus === this.PROCESSING) {
        //                 this.startStatusPolling();
        //             } else if (previousStatus === this.PROCESSING && this.documento.operationStatus !== this.PROCESSING) {
        //                 this.uiService.showMessageSuccess(loc["Análisis de IA completado!"]);
        //                 this.stopStatusPolling();
        //             }
        //         });
        // },
        async getAsync(id) {
    if (!id && this.documento.id) id = this.documento.id;
    if (!id) return;

    // Mostrar spinner global SOLO si no estamos ya en un polling que lo maneje,
    // o si es una carga inicial que podría estar procesando.
    // Si 'documento.operationStatus' ya es PROCESSING, el spinner ya debería estar activo.
    if (this.documento.operationStatus !== this.PROCESSING) {
        this.uiService.showSpinner(true);
    }

    try {
        const res = await this.$store.dispatch("getDocumentoAsync", id);
        const previousStatus = this.documento.operationStatus;
        this.documento = new Documento(res);
        this.tipoDoc = this.documento.tipo; // Asumiendo que 'tipo' existe en 'res'

        if (this.documento.operationStatus === this.PROCESSING) {
            this.uiService.showSpinner(true); // Asegurar que esté activo
            this.startStatusPolling();
            // No se reduce el iframeHeight aquí, ya que el spinner global "bloquea" la página
        } else { // COMPLETED, FAILED, o cualquier otro estado
            this.stopStatusPolling();
            this.uiService.showSpinner(false); // Ocultar spinner global
            // this.iframeHeight = '120vh'; // Restaurar altura si la habías cambiado

            if (previousStatus === this.PROCESSING && this.documento.operationStatus !== this.PROCESSING) {
                if (this.documento.operationStatus === this.COMPLETED) {
                    this.uiService.showMessageSuccess(this.loc["Documento analizado con éxito"]);
                } else if (this.documento.operationStatus === this.FAILED) {
                    this.uiService.showMessageError(this.loc["Error en el análisis del documento"]);
                }
            }
        }
    } catch (error) {
        console.error(`${this.loc["Error en getAsync para el ID"]} ${id}:`, error);
        this.uiService.showMessageError(this.loc["Error al obtener datos del documento."]);
        this.stopStatusPolling();
        this.uiService.showSpinner(false); // Asegurarse de ocultarlo en caso de error
        // this.iframeHeight = '120vh';
    } finally {
        // Si no está procesando y no hubo error que ya lo ocultó, ocultarlo.
        if (this.documento.operationStatus !== this.PROCESSING && this.uiService.isSpinnerVisible) {
             this.uiService.showSpinner(false);
        }
    }
},

async getOperationStatusAsync(id) { // Llamado por el poller
    try {
        const res = await this.$store.dispatch("getDocumentoAsync", id);
        const previousStatus = this.documento.operationStatus;

        this.documento.operationId = res.operationId;
        this.documento.operationStatus = res.operationStatus;
        this.documento.rowVersion = res.rowVersion; // Muy importante

        if (res.operationStatus === this.COMPLETED) {
            this.documento.fechaDesde = res.fechaDesde ? new Date(res.fechaDesde).toISOString().split('T')[0] : null;
            this.documento.fechaHasta = res.fechaHasta ? new Date(res.fechaHasta).toISOString().split('T')[0] : null;
            if (res.archivoURL && !this.documento.archivoURL) {
                this.documento.archivoURL = res.archivoURL; // Actualiza si es la primera vez
            }
        }

        if (this.documento.operationStatus === this.PROCESSING) {
            // El spinner global ya debería estar activo si entramos en polling.
            // No es necesario this.uiService.showSpinner(true) aquí porque el poller es silencioso.
            // this.startStatusPolling(); // El polling ya está corriendo, se auto-gestiona
        } else { // COMPLETED o FAILED detectado por el poller
            this.stopStatusPolling();
            this.uiService.showSpinner(false); // OCULTAR SPINNER GLOBAL
            // this.iframeHeight = '120vh';

            if (previousStatus === this.PROCESSING && this.documento.operationStatus !== this.PROCESSING) {
                if (this.documento.operationStatus === this.COMPLETED) {
                    this.uiService.showMessageSuccess(this.loc["Documento analizado con éxito"]);
                } else if (this.documento.operationStatus === this.FAILED) {
                    this.uiService.showMessageError(this.loc["Error en el análisis del documento"]);
                }
            }
        }
    } catch (error) {
        console.error(`${this.loc["Error en getOperationStatusAsync para el ID"]} ${id}:`, error);
        // En caso de error en el polling, podrías querer detenerlo o reintentar N veces.
        // Por ahora, no ocultamos el spinner global aquí para no dar una falsa impresión de finalización.
        // Si el polling falla repetidamente, el usuario podría quedarse con el spinner global.
        // Considera una lógica de reintentos o un timeout para el polling.
    }
},

async onDocumentoAnalyzed(e) { // Cambiado de onDocumentoAnalyzedAsync para seguir tu código
    if (this.errorBag.hasErrors() || !e || (Array.isArray(e) && e.length === 0)) {
        if (!this.errorBag.hasErrors()) {
            this.uiService.showMessageError(this.loc["Error al procesar el archivo cargado."]);
        }
        return;
    }

    const eventData = Array.isArray(e) ? e[0] : e; // e[0] si es un array, o e directamente

    // ASUMCIÓN: eventData (o eventData.id) es el ID del DocumentoCargado
    // O que el backend ya sabe qué documento analizar basado en la subida de 'importar-documento'
    // y que el siguiente getAsync recuperará el estado PROCESSING.

    this.uiService.showSpinner(true); // Activar spinner global INMEDIATAMENTE
    // this.iframeHeight = '30vh'; // Opcional: reducir iframe mientras "procesa"

    // Forzar una actualización del estado del documento que debería estar en PROCESSING
    // El backend debe haber iniciado la operación de IA y marcado el estado como PROCESSING
    // como resultado de la acción del componente 'importar-documento'.
    // La siguiente llamada a getAsync debería recoger ese estado.
    // Si `eventData` es el objeto documento completo y actualizado por el importador:
    if (eventData && eventData.id) {
         await this.getAsync(eventData.id);
    } else if (this.documento.id) {
         await this.getAsync(this.documento.id); // Re-obtener el actual
    } else {
        console.error("No hay ID de documento para iniciar el análisis/polling.");
        this.uiService.showSpinner(false);
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
            if (
                await this.uiService.confirmActionModal(
                    loc["¿Está usted seguro que desea aprobar este documento?"],
                    loc["Aceptar"],
                    loc["Cancelar"]
                )
            ) {
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
            }
        },
        async rejectAsync() {
            if (
                await this.uiService.confirmActionModal(
                    loc["¿Está usted seguro que desea rechazar este documento?"],
                    loc["Aceptar"],
                    loc["Cancelar"]
                )
            ) {
                this.errorBag.clear();
                this.uiService.showSpinner(true)
                return await this.$store.dispatch("rejectDocumentoAsync", this.documento)
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