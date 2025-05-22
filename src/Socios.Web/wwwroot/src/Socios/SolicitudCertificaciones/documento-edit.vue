<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 d-flex justify-content-between sticky-header mt-4">
                <div class="col-12 d-grid">
                    <div class="row">
                        <p class="h5 col-6">Modificación Documento {{ tipoDoc }} - Solicitud nro {{ documento.solicitudId }}
                        </p>
                        <div class="col-6 gap-4 d-flex justify-content-end">
                            <documentoEstado-label :value="documento.estadoId" />
                        
                            <cancel-button class="ms-2" @click="cancel">Volver</cancel-button>
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
                            :class="{ 'active': currentLayoutMode === LayoutMode.File }" @click="setLayout(LayoutMode.File)"
                            title="Mostrar solo el documento" aria-label="Mostrar solo el documento"
                            data-bs-toggle="tooltip" data-bs-placement="bottom">
                            <i class="fas fa-file-alt" aria-hidden="true"></i>
                        </button>
                        <button type="button" class="btn btn-light"
                            :class="{ 'active': currentLayoutMode === LayoutMode.Form }" @click="setLayout(LayoutMode.Form)"
                            title="Mostrar solo el formulario" aria-label="Mostrar solo el formulario"
                            data-bs-toggle="tooltip" data-bs-placement="bottom">
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
                            <label class="control-label">Importar Documento</label>
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
                                <label class="control-label">Fecha Desde</label>
                                <input type="date" class="form-control" v-model="documento.fechaDesde">
                                <span class="text-danger field-validation-error">
                                    {{ errorBag.get("fechaDesde") }}
                                </span>
                            </div>
                            <div class="form-group col-lg-6 col-sm-12 mb-2 required">
                                <label class="control-label">Fecha Hasta</label>
                                <input type="date" class="form-control" v-model="documento.fechaHasta">
                                <span class="text-danger field-validation-error">
                                    {{ errorBag.get("fechaHasta") }}
                                </span>
                            </div>
                            <div class="form-group col-lg-12 col-sm-12 mb-2">
                                <label class="control-label">Observaciones</label>
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
                <button @click.prevent="saveAsync" class="btn btn-secondary btn-sm"
                :disabled="documento.propietarioActualId != SOCIOS && documento.estadoId != DOCUMENTO_PENDIENTE"
                >
                    <i class="fas fa-save"></i>
                    Guardar
                </button>
                <accept-button @click="updateAsync" :disabled="documento.propietarioActualId != SOCIOS && documento.estadoId != DOCUMENTO_PENDIENTE">
                    Aceptar</accept-button>
                <cancel-button @click="cancel">Cancelar</cancel-button>
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

const NO_DATA_MESSAGE = "No hay datos";

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
            documento: new Documento(),
            uiService: new UiService(),
            tipoDoc: '',
            NO_DATA_MESSAGE,
            documentoArchivoDivId: "__documentoArchivo",
            documentoFormularioDivId: "__documentoFormulario",
            LayoutMode,
            currentLayoutMode: LayoutMode.Split,

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
    methods: {
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
                    this.documento = new Documento(res);
                    this.tipoDoc = this.documento.tipo;
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
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
                        this.uiService.showMessageSuccess("Operación confirmada")
                        this.goSolicitudEdit();
                    } else {
                        this.uiService.showMessageError("Operación rechazada")
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