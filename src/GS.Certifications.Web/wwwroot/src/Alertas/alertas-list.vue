<template>
    <div>

        <alertas-filter @search="setParameters($event)" />

        <br />

        <div class="col-12 d-flex justify-content-between align-items-center mt-2 mb-2">
            <p class="h5 m-0">{{loc["Listado de Alertas"]}}</p>
            <button type="button" class="btn btn-outline-primary btn-sm" @click="setCreate">
                <b><i class="fas fa-plus"></i>
                    {{loc["Agregar"]}}</b>
            </button>
        </div>

        <div class="col-12 table-responsive">
            <div v-if="alertas.length > 0">
                <datatableRecordsLength v-model="recordsLength" :id="`${idTable}-recordsLength`"
                    @changed="onRecordsLengthChanged"></datatableRecordsLength>
            </div>
            <table :id="`${idTable}`" convert-to-datatable-manual no-paging
                class="table table-sm table-bordered table-striped table-hover">
                <thead class="table-top">
                    <tr class="text-center align-middle">
                        <th class="text-center">{{loc["Id"]}}</th>
                        <th class="text-center">{{loc["Alerta Tipo"]}}</th>
                        <th class="text-center">{{loc["Estado"]}}</th>
                        <th class="text-center">{{loc["Ubicacion"]}}</th>
                        <th class="text-center">{{loc["Horas"]}}</th>
                        <th class="text-center">{{loc["Fecha / Hora Desde"]}}</th>
                        <th class="text-center">{{loc["Fecha / Hora Hasta"]}}</th>
                        <th class="text-center">{{loc["Reglas Adicionales"]}}</th>
                        <th class="text-center">{{loc["Para"]}}</th>
                        <th class="text-center">{{loc["Para Variable"]}}</th>
                        <th class="text-center">{{loc["CC"]}}</th>
                        <th class="text-center">{{loc["CCO"]}}</th>
                        <th class="text-center">{{loc["Asunto"]}}</th>
                        <th class="text-center">{{loc["Ver Cuerpo"]}}</th>
                        <th class="text-center">{{loc["Activa"]}}</th>
                        <th class="text-center">{{loc["Usuario Últ. Modif."]}}</th>
                        <th class="text-center" datatable-datetime>{{loc["Fecha / Hora Últ. Modif."]}}</th>
                        <th class="text-center" no-sort-datatable>{{loc["Acciones"]}}</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-if="alertas.length === 0" class="no-data text-center">
                        <td class="text-center" colspan="100">{{ resultsMessage }}</td>
                    </tr>
                    <template v-for="alerta in alertas">
                        <tr :key="alerta.id">

                            <td class="text-right align-middle">
                                <a type="button" class="btn-link" @click="setDetail(alerta.id)">
                                    {{ alerta.id }}
                                </a>
                            </td>

                            <td class="text-right align-middle">
                                {{ alerta.alertaTipo.descripcion }}
                            </td>

                            <td class="text-right align-middle">
                                {{ alerta.estadoDescripcion != null ? alerta.estadoDescripcion : "-" }}
                            </td>

                            <td class="text-right align-middle">
                                {{ alerta.ubicacionDescripcion != null ? alerta.ubicacionDescripcion : "-" }}
                            </td>

                            <td class="text-right align-middle">
                                {{ alerta.horas }}
                            </td>

                            <td class="text-right align-middle">
                                <template v-if="alerta.fechaHoraDesde">
                                    {{ alerta.fechaHoraDesde | uidatetime }}
                                </template>
                                <template v-else>
                                    -
                                </template>
                            </td>

                            <td class="text-right align-middle">
                                <template v-if="alerta.fechaHoraHasta">
                                    {{ alerta.fechaHoraHasta | uidatetime }}
                                </template>
                                <template v-else>
                                    -
                                </template>
                            </td>


                            <td class="text-right align-middle">
                                {{ alerta.reglasAdicionales.length === 0 ? "-" : alerta.reglasAdicionales.map(r =>
                                    r.descripcion).join(', ') }}
                            </td>

                            <td class="text-right align-middle">
                                {{ alerta.para }}
                            </td>

                            <td class="text-right align-middle">
                                {{ alerta.paraVariable ? alerta.paraVariable : "-" }}
                            </td>

                            <td class="text-right align-middle">
                                {{ alerta.cc ? alerta.cc : "-" }}
                            </td>

                            <td class="text-right align-middle">
                                {{ alerta.cco ? alerta.cco : "-" }}
                            </td>

                            <td class="text-right align-middle">
                                {{ alerta.asunto }}
                            </td>

                            <td class="text-center align-middle">
                                <div class="d-flex justify-content-between m-2">
                                    <a :title="INFO_BODY_RENDER" data-toggle="tooltip" :id="`bodyTooltip-${alerta.id}`"
                                        @touchstart="mostrarTooltip(`bodyTooltip-${alerta.id}`)" data-bs-toggle="modal"
                                        :data-bs-target="`#${idModalAlertaBodyRender}-${alerta.id}`"
                                        style="cursor:pointer">
                                        <i class="fas fa-solid fa-envelope-open-text"></i>
                                    </a>

                                    <alertasBody-modal :render="true" :alertaCuerpo="alerta.cuerpo"
                                        :alertaAsunto="alerta.asunto"
                                        :idModal="`${idModalAlertaBodyRender}-${alerta.id}`" />

                                    <a :title="INFO_BODY_PLAIN_TEXT" data-toggle="tooltip"
                                        :id="`bodyTooltip-${alerta.id}`"
                                        @touchstart="mostrarTooltip(`bodyTooltip-${alerta.id}`)" data-bs-toggle="modal"
                                        :data-bs-target="`#${idModalAlertaBodyPlainText}-${alerta.id}`"
                                        style="cursor:pointer">
                                        <i class="fas fa-solid fa-code"></i>
                                    </a>

                                    <alertasBody-modal :render="false" :alertaCuerpo="alerta.cuerpo"
                                        :alertaAsunto="alerta.asunto"
                                        :idModal="`${idModalAlertaBodyPlainText}-${alerta.id}`" />
                                </div>

                            </td>

                            <td class="text-right align-middle">
                                <div class="text-center align-middle">
                                    <span v-show="alerta.activa">
                                        <i class="fas fa-check text-success"></i>
                                    </span>
                                    <span v-show="!alerta.activa">
                                        <i class="fas fa-times text-danger"></i>
                                    </span>
                                </div>
                            </td>

                            <td class="text-right align-middle">
                                {{ alerta.usuarioUltimaModificacion ? alerta.usuarioUltimaModificacion :
                                    alerta.usuarioAlta }}
                            </td>

                            <td class="text-right align-middle">
                                {{ getAlertaDate(alerta) | uidatetime }}
                            </td>

                            <td class="text-center align-middle">
                                <!-- Agregar grants -->
                                <inline-edit @click="setUpdate(alerta.id)"></inline-edit>
                            </td>
                        </tr>
                    </template>
                </tbody>
            </table>
            <datatablePagination v-if="alertas.length > 0" v-model="currentPage" :id="`${idTable}-datatable`"
                @page-changed="loadAlertas" :recordsTotal="recordsTotal" :recordsLength="recordsLength"
                :maxVisiblePages="5">
            </datatablePagination>
        </div>
    </div>
</template>

<script>
import ajax from "@/common/ajaxWrapper";
import UiService from "@/common/uiService";
import inlineEdit from "@/components/forms/inline-edit-button.vue";
import alertasFilter from "./alertas-filter.vue";
import parametersService from "@/common/parameters";
import datatablePagination from "@/components/pagination/datatablePagination.vue";
import datatableRecordsLength from "@/components/pagination/datatableRecordsLength.vue";
import alertasBodyModal from "./alertasBody-modal.vue";

import loc from "@/common/commonLoc.js"

const NO_DATA_MESSAGE = loc["No hay datos"];
const SEARCH_RESULTS_MESSAGE = loc["Click en 'Buscar' para traer resultados"];
const GET_ALERTAS_URL = baseUrl + "/api/Alertas/GetAlertas";

export default {
    name: "alertas-list",
    components: {
        inlineEdit,
        alertasFilter,
        datatablePagination,
        datatableRecordsLength,
        alertasBodyModal
    },
    props: {
        mode: String,
        grants: Object,
    },
    data: function () {
        return {
            loc,
            alertas: [],
            recordsTotal: 0,
            recordsLength: 25,
            currentPage: 0,
            idTable: "alertas",
            resultsMessage: SEARCH_RESULTS_MESSAGE,
            parameters: {},
            uiService: new UiService(),
            parametersStorage: new parametersService(),
            INFO_BODY_RENDER: loc['Ver cuerpo de la Alerta renderizado'],
            INFO_BODY_PLAIN_TEXT: loc['Ver cuerpo de la Alerta en texto plano'],
            idModalAlertaBodyRender: "alerta_body_modal_render",
            idModalAlertaBodyPlainText: "alerta_body_modal_plain_text"
        };
    },
    async mounted() {
    },
    methods: {
        async setParameters(params) {
            this.parameters = params
            await this.loadAlertas()
        },
        saveCurrentParameters(parameters) {
            this.parametersStorage.saveSearchParameters(parameters);
        },
        async onRecordsLengthChanged() {
            this.currentPage = 0;
            await this.loadAlertas();
        },
        async loadAlertas() {
            this.uiService.showSpinner(true);
            this.parameters.start = this.currentPage ? this.currentPage : "0"; // 0 llega null por ajax, por eso se envia como string
            this.parameters.length = this.recordsLength;
            this.saveCurrentParameters(this.parameters)
            return new ajax()
                .get(GET_ALERTAS_URL, this.parameters)
                .then((res) => {
                    this.recordsTotal = res.recordsTotal;
                    this.alertas = res.list;
                    if (this.alertas.length == 0) this.resultsMessage = NO_DATA_MESSAGE
                    this.uiService.onlyDestroyDataTablesManual(this.idTable)
                })
                .then(() => {
                    if (this.alertas.length > 0) this.uiService.transformToDataTablesManual(this.idTable);
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        mostrarTooltip(tooltipId) {
            mostrarTooltip(tooltipId)
        },
        setCreate() {
            this.$router.push({ name: "create" });
        },
        setDetail(id) {
            this.$router.push({ name: "detail", params: { alertaId: id } });
        },
        setUpdate(id) {
            this.$router.push({ name: "edit", params: { alertaId: id } });
        },
        getAlertaDate(alerta) {
            if (alerta.fechaUltimaModificacion != null) return alerta.fechaUltimaModificacion
            return alerta.fechaAlta
        }
    },
};
</script>