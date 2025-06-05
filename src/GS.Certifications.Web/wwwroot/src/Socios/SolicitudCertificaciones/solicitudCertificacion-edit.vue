<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <div class="row">
                    <div class="col-6">
                        <p class="h5">{{loc["Solicitud nro."]}} {{ solicitudCertificacion.id }}</p>
                        <p class="h6">{{loc["Certificación"]}} {{ solicitudCertificacion.certificacion }} {{ solicitudCertificacion.socio }}</p>
                    </div>
                    <div class="col-6 gap-4 d-flex justify-content-end">
                        <solicitudCertificacionEstado-label v-model="solicitudCertificacion.estadoId" />
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">

                        <div class="col-12 d-flex align-items-center mb-2">
                            <div>
                                <p class="h5">{{loc["Documentos"]}}</p>
                                <span class="text-danger field-validation-error">
                                    {{ errorBag.get("documentos") }}
                                </span>
                            </div>
                            <!-- <button type="button" class="btn btn-outline-primary btn-sm" @click="addNewRow()">
                                    <b><i class="fas fa-plus"></i>Agregar</b>
                                </button> -->
                        </div>
                        <div class="col-12 d-flex align-items-center mb-2">
                            <table :id="`${idTable}`" class="table table-bordered table-hover table-responsive">
                                <thead class="table-top">
                                    <tr class="text-center align-middle">
                                        <th class="w-10" scope="col">{{loc["Tipo"]}}</th>
                                        <th class="w-2" scope="col">{{loc["Versión"]}}</th>
                                        <th class="w-10" scope="col">{{loc["Vigencia"]}}</th>
                                        <th class="w-10" scope="col">{{loc["Estado"]}}</th>
                                        <th class="w-10" scope="col">{{loc["Validado Por"]}}</th>
                                        <th class="w-10" scope="col">{{loc["Fecha Subida"]}}</th>
                                        <th class="w-10" scope="col">{{loc["Archivo"]}}</th>
                                        <th class="w-2" scope="col">{{loc["Acciones"]}}</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-if="solicitudCertificacion.documentosCargados.length === 0" class="no-data">
                                        <td colspan="100" class="text-center">{{ NO_DATA_MESSAGE }}</td>
                                    </tr>
                                    <template v-for="(cd, index) in solicitudCertificacion.documentosCargados">
                                        <tr :class="cd.estadoId == DOCUMENTO_RECHAZADO ? 'table-danger' : ''" :key="index" :title="cd.motivoRechazo">
                                            <td data-toggle="tooltip" class="align-middle">
                                                {{ cd.tipo ? cd.tipo : "-" }}</td>
                                            <td data-toggle="tooltip" class="align-middle">
                                                {{ cd.version ? cd.version : "-" }}</td>
                                            <td data-toggle="tooltip" class="align-middle">
                                                {{ cd.fechaDesde | uidate }} - {{ cd.fechaHasta | uidate }}</td>
                                            <td data-toggle="tooltip" class="align-middle">
                                                {{ cd.estado ? loc[cd.estado] : "-" }}</td>
                                            <td data-toggle="tooltip" class="align-middle">
                                                {{ cd.validadoPor ? cd.validadoPor : "-" }}</td>
                                            <td data-toggle="tooltip" class="align-middle">
                                                {{ cd.fechaSubida | uidate }}</td>
                                            <td data-toggle="tooltip" class="align-middle">
                                                {{ cd.archivoURL ? cd.archivoURL : "-" }}</td>
                                            <td class="text-center align-middle">
                                                <div class="d-inline-flex" v-if="cd.estadoId != DOCUMENTO_RECHAZADO">
                                                    <inlineEdit
                                                        :enabled="grants.update && solicitudCertificacion.propietarioActualId == BACKOFFICE && solicitudCertificacion.estadoId == PRESENTADA"
                                                        @click="update(cd.id)" />
                                                    <!-- <inlineDelete
                                                        :disabled="!grants.update && solicitudCertificacion.propietarioActualId != BACKOFFICE"
                                                        @click="remove(cd)" /> -->
                                                </div>
                                            </td>
                            
                                        </tr>
                                    </template>
                                </tbody>
                            </table>
                        </div>

                        <hr>

                        <div class="col-12 d-flex align-items-center mb-2">
                            <div>
                                <p class="h5">{{loc["Vigencia"]}}</p>
                                <span class="text-danger field-validation-error">
                                    {{ errorBag.get("vigencia") }}
                                </span>
                            </div>
                        </div>

                        <div class="form-group col-lg-6 col-sm-12 mb-2 required">
                            <label class="control-label">{{ loc["Fecha Desde"]}}</label>
                            <input type="date" class="form-control" v-model="solicitudCertificacion.vigenciaDesde">
                            <!-- <span class="text-danger field-validation-error">
                                {{ errorBag.get("vigencia") }}
                            </span> -->
                        </div>
                        <div class="form-group col-lg-6 col-sm-12 mb-4 required">
                            <label class="control-label">{{ loc["Fecha Hasta"]}}</label>
                            <input type="date" class="form-control" v-model="solicitudCertificacion.vigenciaHasta">
                            <span class="text-danger field-validation-error">
                                {{ errorBag.get("fechaHasta") }}
                            </span>
                        </div>

                        <hr>

                        <div class="col-12 d-flex align-items-center mb-2">
                            <div>
                                <p class="h5">{{loc["Observaciones"]}}</p>
                            </div>
                        </div>
                        <div class="form-group col-lg-12 col-sm-12 mb-2">
                            <textarea class="form-control" cols="20" rows="5"
                                v-model="solicitudCertificacion.observaciones"></textarea>
                            <span class="text-danger field-validation-error">
                                {{ errorBag.get("observaciones") }}
                            </span>
                        </div>

                        <div v-if="solicitudCertificacion.estadoId == RECHAZADA"
                            class="form-group col-lg-12 col-sm-12 mb-4">
                            <label class="control-label">{{loc["Motivo Rechazo"]}}</label>
                            <textarea id="motivoRechazoTxtArea" disabled class="form-control" enable cols="20"
                                rows="4" v-model="solicitudCertificacion.motivoRechazo"></textarea>
                        </div>
                        
                        <div v-if="solicitudCertificacion.estadoId == REVISION"
                            class="form-group col-lg-12 col-sm-12 mb-4">
                            <label class="control-label">{{loc["Motivo Revisión"]}}</label>
                            <textarea id="motivoRevisionTxtArea" disabled class="form-control" enable cols="20"
                                rows="4" v-model="solicitudCertificacion.motivoRevision"></textarea>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12 d-flex justify-content-end gap-2 mb-3 mt-3">
            <accept-button
                :disabled="!grants.update || solicitudCertificacion.propietarioActualId != BACKOFFICE"
                v-if="solicitudCertificacion.propietarioActualId == BACKOFFICE && solicitudCertificacion.estadoId == PRESENTADA"
                @click="updateAsync">
                {{ loc["Aprobar"]}}</accept-button>

            <a :disabled="solicitudCertificacion.propietarioActualId != BACKOFFICE && solicitudCertificacion.estadoId == PRESENTADA"
                v-if="solicitudCertificacion.estadoId == PRESENTADA"
                class="btn btn-outline-danger btn-sm" :title="loc['Rechazar solicitud']" data-toggle="tooltip"
                data-bs-toggle="modal" :data-bs-target="`#${solicitudRechazoId}-${solicitudCertificacion.id}`"
                style="cursor:pointer">
                {{ loc["Rechazar"]}}
            </a>

            <a :disabled="solicitudCertificacion.propietarioActualId != BACKOFFICE && solicitudCertificacion.estadoId == PRESENTADA"
            v-if="solicitudCertificacion.estadoId == PRESENTADA" class="btn btn-outline-warning btn-sm" :title="loc['Enviar solicitud a revisión']" data-toggle="tooltip"
                data-bs-toggle="modal" :data-bs-target="`#${solicitudRevisionId}-${solicitudCertificacion.id}`"
                style="cursor:pointer">
                {{ loc["Revisión"]}}
            </a>

            <cancel-button @click="cancel">{{ loc["Volver"]}}</cancel-button>
        </div>

        <solicitudRechazo-modal :solicitud="solicitudCertificacion" @solicitudRechazada="rejectAsync($event)"
                :idModal="`${solicitudRechazoId}-${solicitudCertificacion.id}`" />
        
        <solicitudRevision-modal :solicitud="solicitudCertificacion" @solicitudRevision="sendRevisionAsync($event)"
                :idModal="`${solicitudRevisionId}-${solicitudCertificacion.id}`" />
    </div>
</template>

<script>
import AcceptButton from "@/components/forms/accept-button.vue";
import CancelButton from "@/components/forms/cancel-button.vue";
import UiService from "@/common/uiService";
import SolicitudCertificacion from './SolicitudCertificacion'
import commonMixin from '@/Common/Mixins/commonMixin';

import solicitudCertificacionEstadoLabel from "@/Selects/solicitudCertificacionEstado-label.vue";

import solicitudRechazoModal from './Modal/solicitudRechazo-modal'
import solicitudRevisionModal from './Modal/solicitudRevision-modal'

import inlineEdit from "@/components/forms/inline-edit-button.vue";
import inlineDelete from "@/components/forms/inline-delete-button.vue";

import loc from "@/common/commonLoc.js"

// Origen de la solicitud
const SOCIOS = 1;
const BACKOFFICE = 2;
const CORREO = 3;

export default {
    components: {
        AcceptButton,
        CancelButton,
        inlineEdit,
        inlineDelete,
        solicitudCertificacionEstadoLabel,
        solicitudRechazoModal,
        solicitudRevisionModal
    },
    mixins: [commonMixin],
    name: "solicitudCertificacion-edit",
    data: function () {
        return {
            loc,
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
            REVISION: SolicitudEstado.REVISION,
            // --- Estados de los documentos de una solicitud
            DOCUMENTO_PENDIENTE: DocumentoEstado.PENDIENTE,
            DOCUMENTO_VALIDADO: DocumentoEstado.VALIDADO,
            DOCUMENTO_RECHAZADO: DocumentoEstado.RECHAZADO,
            DOCUMENTO_VENCIDO: DocumentoEstado.VENCIDO,
            DOCUMENTO_PRESENTADO: DocumentoEstado.PRESENTADO,
            // ---
            solicitudCertificacion: new SolicitudCertificacion(),
            uiService: new UiService(),
            solicitudRechazoId: "solicitudRechazoModal",
            solicitudRevisionId: "solicitudRevisionModal"
        };
    },
    computed: {
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
        async remove(dto) {
            this.uiService.showSpinner(true)
            await this.$store.dispatch("deleteDocumentoAsync", dto)
                .then(async () => {
                    if (!this.errorBag.hasErrors()) {
                        this.uiService.showMessageSuccess(loc["Operación confirmada"])
                        await this.getAsync(this.solicitudCertificacion.id);
                    } else {
                        this.uiService.showMessageError(loc["Operación rechazada"])
                    }
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        update(id) {
            this.$router.push({ name: "document", params: { id: id } });
        },
        async init() {
            // Si no hay permisos de modificación, volvemos a la lista
            if (!this.grants.update) this.$router.push({ name: "home" });
            else {
                if (this.$route.params.id) await this.getAsync(this.$route.params.id);
                else this.cancel();
            }
        },
        async getAsync(id) {
            this.uiService.showSpinner(true)
            await this.$store.dispatch("getAsync", id)
                .then((res) => {
                    this.solicitudCertificacion = new SolicitudCertificacion(res);
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        goDetail() {
            this.$router.push({ name: "detail", params: { id: this.solicitudCertificacion.id } });
        },
        goHome() {
            this.$router.push({ name: "home" });
        },
        cancel() {
            this.$router.push({ name: "home" });
        },
        async updateAsync() {
            this.uiService.showSpinner(true)
            await this.$store.dispatch("putAsync", this.solicitudCertificacion)
                .then(() => {
                    if (!this.errorBag.hasErrors()) {
                        this.uiService.showMessageSuccess(loc["Operación confirmada"])
                        this.goHome();
                    } else {
                        this.uiService.showMessageError(loc["Operación rechazada"])
                    }
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        async rejectAsync() {
            if (
                await this.uiService.confirmActionModal(
                loc["¿Está usted seguro que desea rechazar esta solicitud?"],
                loc["Aceptar"],
                loc["Cancelar"]
                )
            ) {
                this.uiService.showSpinner(true)
                await this.$store.dispatch("rejectAsync", this.solicitudCertificacion)
                    .then(() => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess(loc["Operación confirmada"])
                            this.goHome();
                        } else {
                            this.uiService.showMessageError(loc["Operación rechazada"])
                        }
                    })
                    .finally(() => {
                        this.uiService.showSpinner(false);
                    });
            }
        },
        async sendRevisionAsync() {
            if (
                await this.uiService.confirmActionModal(
                loc["¿Está usted seguro que desea enviar a revisión esta solicitud?"],
                loc["Aceptar"],
                loc["Cancelar"]
                )
            ) {
                this.uiService.showSpinner(true)
                await this.$store.dispatch("sendRevisionAsync", this.solicitudCertificacion)
                    .then(() => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess(loc["Operación confirmada"])
                            this.goHome();
                        } else {
                            this.uiService.showMessageError(loc["Operación rechazada"])
                        }
                    })
                    .finally(() => {
                        this.uiService.showSpinner(false);
                    });
            }
        }
    },
};
</script>