<template>
    <div class="mt-4">

        <!--<notificationManagement-filter @search="setParameters($event)" />-->
        <notificationManagement-filter v-model="parameters" @search="onSearch" @clear="onClear" />

        <br />

        <div class="col-12 d-flex justify-content-between align-items-center mt-5 mb-3">
            <p class="h5 m-0">{{loc["Listado de Notificaciones"]}}</p>
        </div>

        <div class="col-12 table-responsive">
            <datatableRecordsLength v-if="notificaciones.length > 0" v-model="recordsLength"
                :id="`${idTable}-recordsLength`" @changed="onRecordsLengthChanged">
            </datatableRecordsLength>
            <table :id="`${idTable}`" convert-to-datatable-manual class="table table-bordered table-striped table-hover"
                server-side-paging>
                <thead class="table-top">
                    <tr class="text-center align-middle">
                        <th data-column="FechaEncolado" class="text-center" datatable-datetime
                            default-sort-datatable="desc">{{loc["Fecha / Hora Registración"]}}</th>
                        <th data-column="FechaEnviado" class="text-center" datatable-datetime>{{loc["Fecha / Hora Notificación"]}}</th>
                        <th data-column="EstadoIdm" class="text-center">{{loc["Tipo"]}}</th>
                        <th data-column="Destinatarios" class="text-center">{{loc["Destinatarios"]}}</th>
                        <th data-column="ConCopia" class="text-center">{{loc["CC"]}}</th>
                        <th data-column="ConCopiaOculta" class="text-center">{{loc["CCO"]}}</th>
                        <th data-column="ConfiguracionNotificacion.Subject" class="text-center">{{loc["Asunto"]}}</th>
                        <!-- <th no-sort-datatable class="text-center">Eventos</th> -->
                        <th no-sort-datatable>{{loc["Acciones"]}}</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-if="notificaciones.length === 0" class="no-data text-center">
                        <td class="text-center" colspan="100">{{ resultsMessage }}</td>
                    </tr>
                    <template v-for="notificacion in notificaciones">
                        <tr :key="notificacion.id">
                            <td class="text-right align-middle">
                                {{ notificacion.fechaEncolado | uidatetime }}
                            </td>
                            <td class="text-right align-middle">
                                <template v-if="notificacion.fechaEnviado">
                                    {{ notificacion.fechaEnviado | uidatetime }}
                                </template>
                                <template v-else>
                                    -
                                </template>
                            </td>
                            <td class="text-right align-middle">
                                <!-- RM6051 no existe la tabla tipo, hardcodeo Correo {{ notificacion.tipo }} -->
                                {{loc["Correo"]}}
                            </td>
                            <td class="text-right align-middle">
                                {{ notificacion.destinatarios ? notificacion.destinatarios : "-" }}
                            </td>
                            <td class="text-right align-middle">
                                {{ notificacion.conCopia ? notificacion.conCopia : "-" }}
                            </td>
                            <td class="text-right align-middle">
                                {{ notificacion.conCopiaOculta ? notificacion.conCopiaOculta : "-" }}
                            </td>
                            <td class="text-right align-middle">
                                {{ notificacion.asunto ? notificacion.asunto : "-" }}
                            </td>
                            <!-- <td class="text-center">
                                <a data-bs-toggle="modal"
                                    :data-bs-target="`#${idModalNotificationEvents}-${notificacion.id}`" style="cursor:pointer">
                                    <i class="fas fa-search"></i>
                                </a>
                                <notificationEvents-modal :notificacionId="notificacion.id" :idModal="`${idModalNotificationEvents}-${notificacion.id}`"/>
                            </td> -->
                            <td class="text-center align-middle">
                                <div class="d-flex noflex justify-content-between">
                                    <a class="me-2" title="Eventos" data-toggle="tooltip" data-bs-toggle="modal"
                                        :data-bs-target="`#${idModalNotificationEvents}-${notificacion.id}`"
                                        style="cursor:pointer">
                                        <i class="fas fa-search"></i>
                                    </a>
                                    <a class="me-2" :title="INFO_BODY" data-toggle="tooltip"
                                        :id="`bodyTooltip-${notificacion.id}`"
                                        @touchstart="mostrarTooltip(`bodyTooltip-${notificacion.id}`)"
                                        data-bs-toggle="modal"
                                        :data-bs-target="`#${idModalNotificationBody}-${notificacion.id}`"
                                        style="cursor:pointer">
                                        <i class="fas fa-solid fa-envelope-open-text"></i>
                                    </a>
                                    <a :title="INFO_REENVIAR" data-toggle="tooltip"
                                        :id="`reenviarTooltip-${notificacion.id}`"
                                        @touchstart="mostrarTooltip(`reenviarTooltip-${notificacion.id}`)"
                                        @click="navigateToForm(notificacion.id)" style="cursor:pointer"><i
                                            class="fas fa-solid fa-share"></i></a>
                                    <notificationBody-modal :notificacionId="notificacion.id"
                                        :idModal="`${idModalNotificationBody}-${notificacion.id}`" />
                                    <notificationEvents-modal :notificacionId="notificacion.id"
                                        :idModal="`${idModalNotificationEvents}-${notificacion.id}`" />
                                </div>
                            </td>
                        </tr>
                    </template>
                </tbody>
            </table>
            <datatablePagination v-if="notificaciones.length > 0" v-model="currentPage" :id="`${idTable}-datatable`"
                @page-changed="onPageChanged" :recordsTotal="recordsTotal" :recordsLength="recordsLength"
                :maxVisiblePages="5">
            </datatablePagination>
        </div>
    </div>
</template>

<script>
import ajax from "@/common/ajaxWrapper";
import UiService from "@/common/uiService";
import inlineEdit from "@/components/forms/inline-edit-button.vue";
import notificationManagementFilter from "./notificationManagement-filter.vue";
import notificationBodyModal from "./notificationBody-modal.vue";
import notificationEventsModal from "./notificationEvents-modal.vue";

import columnSortedMixin from '@/Common/Mixins/columnSortedMixin';

import datatablePagination from "@/components/pagination/datatablePagination.vue";
import datatableRecordsLength from "@/components/pagination/datatableRecordsLength.vue";

import Parameters from "./Parameters";

import SessionParametersService from '@/Common/SessionParametersService';

import loc from "@/common/commonLoc.js"

const NO_DATA_MESSAGE = "No hay datos";
const SEARCH_RESULTS_MESSAGE = "Click en 'Buscar' para traer resultados";
const GET_NOTIFICACIONES_URL = baseUrl + "/api/Notifications/GetNotificaciones";

export default {
    name: "notificationManagement-list",
    mixins: [columnSortedMixin],
    components: {
        inlineEdit,
        notificationManagementFilter,
        datatablePagination,
        datatableRecordsLength,
        notificationBodyModal,
        notificationEventsModal
    },
    props: {
        mode: String,
        grants: Object,
    },
    data: function () {
        return {
            loc,

            notificaciones: [],
            recordsTotal: 0,
            recordsLength: 25,
            currentPage: 0,
            idTable: "notificacionesManagement",
            resultsMessage: SEARCH_RESULTS_MESSAGE,
            parameters: {},
            uiService: new UiService(),
            INFO_REENVIAR: 'Reenviar',
            INFO_BODY: 'Ver cuerpo de la Notificación',
            idModalNotificationBody: "notification_body_modal",
            idModalNotificationEvents: "notification_events_modal",
        };
    },
    async mounted() {
        this.getStoredParameters();
        // Al obtener los parámetros guardados en sesión, actualizamos currentPage y recordsLength
        this.currentPage = this.parameters.start;
        this.recordsLength = this.parameters.length;
        if (this.$route.query.fromDetail) {
            await this.getAsync();
        }
    },
    methods: {
        // Guarda los parámetros en local storage (sesión)
        saveParameters() {
            // Setteamos a los parámetros de búsqueda la pág. actual y la cant. de registros por pág.
            this.parameters.start = this.currentPage;
            this.parameters.length = this.recordsLength;
            SessionParametersService.set(this.parameters);
            // Ajustamos en el HTML los atributos correspondientes al ordenamiento especificado
            this.fixColumnSortingHTML(this.parameters);
        },
        // Obtiene los parámetros guardados en local storage (sesión)
        getStoredParameters() {
            this.parameters = SessionParametersService.get() ? SessionParametersService.get() : new Parameters();
        },
        async handleColumnSorting(columnSortData) {
            this.getStoredParameters();
            this.setColumnSortData(columnSortData);
            await this.getAsync();
        },
        setColumnSortData(data) {
            if (data) {
                this.parameters.orderPropertyName = data.orderPropertyName;
                this.parameters.orderDirection = data.orderDirection;
            } else {
                this.parametros.orderPropertyName = 'FechaEncolado';
                this.parametros.orderDirection = '1';
            }
        },
        onClear() {

        },
        async onSearch() {
            this.currentPage = '0'; // reiniciamos la página actual para la nueva búsqueda
            await this.getAsync();
        },
        async onPageChanged() {
            // Obtenemos los parámetros de la última búsqueda realizada ya que no queremos hacer el get con los parámetros que se hayan setteado sin haber cliqueado en Buscar
            this.getStoredParameters();
            await this.getAsync();
        },
        async onRecordsLengthChanged() {
            // Obtenemos los parámetros de la última búsqueda realizada ya que no queremos hacer el get con los parámetros que se hayan setteado sin haber cliqueado en Buscar
            this.getStoredParameters();
            // Al haber cambiado la cantidad de elementos por página, reiniciamos el start
            this.currentPage = '0';
            await this.getAsync();
        },

        async getAsync() {
            this.uiService.showSpinner(true);
            this.saveParameters()
            return new ajax()
                .get(GET_NOTIFICACIONES_URL, this.parameters)
                .then((res) => {
                    if (res.list.length > 0) {
                        this.recordsTotal = res.recordsTotal
                        this.notificaciones = res.list
                    } else {
                        this.resultsMessage = NO_DATA_MESSAGE
                        this.recordsTotal = 0
                        this.notificaciones = []
                    }
                    this.destroyTable();
                })
                .then(() => {
                    if (this.notificaciones.length > 0) this.uiService.transformToDataTablesManual(this.idTable);
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        destroyTable() {
            this.uiService.onlyDestroyDataTablesManual(this.idTable);
        },
        mostrarTooltip(tooltipId) {
            mostrarTooltip(tooltipId)
        },
        navigateToForm(notificacionId) {
            this.$router.push({ name: "edit", params: { notificacionId: notificacionId } });
        }
    },
};
</script>