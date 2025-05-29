<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">{{loc["Solicitud nro."]}} {{ solicitudCertificacion.id }}</p>
                <p class="h6">{{loc["Certificación"]}} {{ solicitudCertificacion.certificacion }}</p>
                <p class="h6 col-12">{{ solicitudCertificacion.socio }}</p>
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">

                        <div class="col-12 d-flex justify-content-between align-items-center mb-4">
                            <div>
                                <p class="h5 m-0">{{loc["Documentos requeridos"]}}</p>
                                <span class="text-danger field-validation-error">
                                    {{ errorBag.get("documentos") }}
                                </span>
                            </div>
                            <!-- <button type="button" class="btn btn-outline-primary btn-sm" @click="addNewRow()">
                                    <b><i class="fas fa-plus"></i>Agregar</b>
                                </button> -->
                        </div>
                        <table :id="`${idTable}`" class="table table-bordered table-hover">
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
                                    <tr :class="cd.estadoId == DOCUMENTO_RECHAZADO ? 'table-danger' : ''" :key="index">
                                        <td data-toggle="tooltip" class="align-middle">
                                            {{ cd.tipo ? cd.tipo : "-" }}</td>
                                        <td data-toggle="tooltip" class="align-middle">
                                            {{ cd.version ? cd.version : "-" }}</td>
                                        <td data-toggle="tooltip" class="align-middle">
                                            {{ cd.fechaDesde | uidate }} - {{ cd.fechaHasta | uidate }}</td>
                                        <td data-toggle="tooltip" class="align-middle">
                                            {{ cd.estado ? cd.estado : "-" }}</td>
                                        <td data-toggle="tooltip" class="align-middle">
                                            {{ cd.validadoPor ? cd.validadoPor : "-" }}</td>
                                        <td data-toggle="tooltip" class="align-middle">
                                            {{ cd.fechaSubida | uidate }}</td>
                                        <td data-toggle="tooltip" class="align-middle">
                                            {{ cd.archivoURL ? cd.archivoURL : "-" }}</td>
                                        <td class="text-center align-middle">
                                            <div class="d-inline-flex">
                                                <inlineEdit
                                                    :disabled="!grants.update && solicitudCertificacion.propietarioActualId != BACKOFFICE"
                                                    @click="update(cd.id)" />
                                                <inlineDelete
                                                    :disabled="!grants.update && solicitudCertificacion.propietarioActualId != BACKOFFICE"
                                                    @click="remove(cd)" />
                                            </div>
                                        </td>

                                    </tr>
                                </template>
                            </tbody>
                        </table>

                        <hr>

                        <div class="form-group col-lg-6 col-sm-12 mb-2 required">
                            <label class="control-label">{{ loc["Fecha Desde"]}}</label>
                            <input type="date" class="form-control" v-model="solicitudCertificacion.vigenciaDesde">
                            <span class="text-danger field-validation-error">
                                {{ errorBag.get("vigencia") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-6 col-sm-12 mb-2 required">
                            <label class="control-label">{{ loc["Fecha Hasta"]}}</label>
                            <input type="date" class="form-control" v-model="solicitudCertificacion.vigenciaHasta">
                            <span class="text-danger field-validation-error">
                                {{ errorBag.get("fechaHasta") }}
                            </span>
                        </div>

                        <div class="form-group col-lg-12 col-sm-12 mb-2">
                            <label class="control-label">{{ loc["Observaciones"]}}</label>
                            <textarea class="form-control" cols="20" rows="5"
                                v-model="solicitudCertificacion.observaciones"></textarea>
                            <span class="text-danger field-validation-error">
                                {{ errorBag.get("observaciones") }}
                            </span>
                        </div>


                    </div>
                </div>
            </div>
        </div>
        <div class="col-12 d-flex justify-content-end gap-2 mb-3 mt-3">
            <accept-button
                :disabled="!grants.update || solicitudCertificacion.propietarioActualId != BACKOFFICE || !solicitudCertificacion.documentosCargados.every(d => d.estadoId == DOCUMENTO_VALIDADO)"
                v-if="solicitudCertificacion.propietarioActualId == BACKOFFICE && solicitudCertificacion.estadoId == PRESENTADA"
                @click="updateAsync">
                {{ loc["Aprobar"]}}</accept-button>

            <button
                :disabled="solicitudCertificacion.propietarioActualId != BACKOFFICE && solicitudCertificacion.estadoId != BORRADOR"
                v-if="solicitudCertificacion.estadoId == PRESENTADA" class="btn btn-outline-danger btn-sm"
                @click="rejectAsync" title="Rechazar solicitud">
                {{ loc["Rechazar"]}}
            </button>

            <cancel-button @click="cancel">{{ loc["Volver"]}}</cancel-button>
        </div>
    </div>
</template>

<script>
import AcceptButton from "@/components/forms/accept-button.vue";
import CancelButton from "@/components/forms/cancel-button.vue";
import UiService from "@/common/uiService";
import SolicitudCertificacion from './SolicitudCertificacion'
import commonMixin from '@/Common/Mixins/commonMixin';

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
        inlineDelete
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
            // --- Estados de los documentos de una solicitud
            DOCUMENTO_PENDIENTE: DocumentoEstado.PENDIENTE,
            DOCUMENTO_VALIDADO: DocumentoEstado.VALIDADO,
            DOCUMENTO_RECHAZADO: DocumentoEstado.RECHAZADO,
            DOCUMENTO_VENCIDO: DocumentoEstado.VENCIDO,
            DOCUMENTO_PRESENTADO: DocumentoEstado.PRESENTADO,
            // ---
            solicitudCertificacion: new SolicitudCertificacion(),
            uiService: new UiService(),
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
                loc["¿Está usted seguro que desea eliminar esta solicitud?"],
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
        }
    },
};
</script>