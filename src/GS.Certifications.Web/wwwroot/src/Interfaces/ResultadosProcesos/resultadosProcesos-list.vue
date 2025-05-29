<template>
    <div>

        <resultadosProcesos-filter v-model="parameters" @clear="onClear()" @search="onSearch" />

        <br />

        <div class="col-12 d-flex justify-content-between align-items-center mt-2 mb-2">
            <p class="h5 m-0">{{loc["Listado de Resultados"]}}</p>
        </div>

        <div class="col-12 table-responsive">
            <datatableRecordsLength v-model="recordsLength" v-if="list.length > 0" :id="`${idTable}-recordsLength`"
                @changed="onRecordsLengthChanged" />
            <table :id="`${idTable}`" convert-to-datatable-manual server-side-paging
                class="table table-sm table-bordered table-striped table-hover">
                <thead class="table-top">
                    <tr class="text-center align-middle">
                        <th default-sort-datatable="desc" no-sort-datatable data-column="Id" class="text-center">{{loc["Id"]}}</th>
                        <th class="text-center" no-sort-datatable >{{loc["Sistema"]}}</th>
                        <th class="text-center" no-sort-datatable >{{loc["Interfaz"]}}</th>
                        <th class="text-center" no-sort-datatable >{{loc["Tipo"]}}</th>
                        <th class="text-center" no-sort-datatable >{{loc["Estado"]}}</th>
                        <th class="text-center" datatable-datetime no-sort-datatable>{{loc["Últ. Modif. Estado"]}}</th>
                        <th class="text-center" no-sort-datatable >{{loc["Archivo"]}}</th>
                        <th class="text-center" no-sort-datatable >{{loc["Leídos"]}}</th>
                        <th class="text-center" no-sort-datatable >{{loc["Ignorados"]}}</th>
                        <th class="text-center" no-sort-datatable >{{loc["No Procesados"]}}</th>
                        <th class="text-center" no-sort-datatable>{{loc["Acciones"]}}</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-if="list.length === 0" class="no-data text-center">
                        <td class="text-center" colspan="100">{{ resultsMessage }}</td>
                    </tr>
                    <template v-for="item in list">
                        <tr :key="item.id">

                            <td class="text-right align-middle">
                                <a type="button" class="btn-link" @click="setDetail(item.id)">
                                    {{ item.id }}
                                </a>
                            </td>

                            <td class="text-right align-middle">
                                {{ item.sistema }}
                            </td>

                            <td class="text-right align-middle">
                                {{ item.interfaz }}
                            </td>

                            <td class="text-right align-middle">
                                {{ item.tipoDescripcion }}
                            </td>

                            <td class="text-right align-middle">
                                {{ item.estado }}
                            </td>

                            <td class="text-right align-middle">
                                {{ item.estadoModificacionFechaHora | uidatetime }}
                            </td>

                            <td class="text-right align-middle">
                                {{ item.archivoNombre }}
                            </td>

                            <td class="text-right align-middle">
                                {{ item.cantidadRegistrosLeidos }}
                            </td>

                            <td class="text-right align-middle">
                                {{ item.cantidadRegistrosIgnorados }}
                            </td>

                            <td class="text-right align-middle">
                                {{ item.cantidadRegistrosNoProcesados }}
                            </td>

                            <td class="text-center align-middle">
                                <button type="button" title="Descargar Archivo" class="col btn btn-link text-body"
                                    id="print-button" :disabled="!item.archivoNombre"
                                    @click="descargarArchivo(item.id)">
                                    <i class="fas fa-download"></i>
                                </button>
                            </td>
                        </tr>
                    </template>
                </tbody>
            </table>
            <datatablePagination v-if="list.length > 0" v-model="currentPage" :id="`${idTable}-datatable`"
                @page-changed="onPageChanged" :recordsTotal="recordsTotal" :recordsLength="recordsLength"
                :maxVisiblePages="5" />
        </div>
    </div>
</template>

<script>
import ajax from "@/common/ajaxWrapper";
import UiService from "@/common/uiService";
import inlineEdit from "@/components/forms/inline-edit-button.vue";
import resultadosProcesosFilter from "./resultadosProcesos-filter.vue";
import SessionParametersService from "@/Common/SessionParametersService";
import datatablePagination from "@/components/pagination/datatablePagination.vue";
import datatableRecordsLength from "@/components/pagination/datatableRecordsLength.vue";
import Parameters from "./Parameters";
import columnSortedMixin from '@/Common/Mixins/columnSortedMixin';

import loc from "@/common/commonLoc.js"

const NO_DATA_MESSAGE = loc["No hay datos"];
const SEARCH_RESULTS_MESSAGE = loc["Click en 'Buscar' para traer resultados"];
const RESULTADOS_PROCESOS_URL = baseUrl + "/api/Interfaces/ResultadosProcesos";

export default {
    name: "resultadosProcesos-list",
    mixins: [columnSortedMixin],
    components: {
        inlineEdit,
        resultadosProcesosFilter,
        datatablePagination,
        datatableRecordsLength
    },
    props: {
        mode: String,
        grants: Object,
    },
    computed: {
    },
    data: function () {
        return {
            loc,
            list: [],
            recordsTotal: 0,
            idTable: "resultadosProcesos",
            resultsMessage: SEARCH_RESULTS_MESSAGE,
            parameters: {},
            currentPage: '0',
            recordsLength: 25,
            uiService: new UiService()
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
        async descargarArchivo(procesoId) {
            this.uiService.showSpinner(true);
            return await new ajax()
                .getFile(RESULTADOS_PROCESOS_URL + `/${procesoId}/Archivos`, { responseType: 'blob', observe: 'response' })
                .then(async (res) => {
                    let valorHeaderContentDisposition = res.headers.get('content-disposition').split(";")[1]
                    let nombreArchivo = valorHeaderContentDisposition.split("=")[1]
                    nombreArchivo = nombreArchivo.replaceAll("\"", '');
                    var blob = await res.blob();
                    const link = document.createElement('a');
                    link.href = URL.createObjectURL(blob);
                    link.download = nombreArchivo;
                    link.click();
                    URL.revokeObjectURL(link.href);
                })
                .catch(error => {
                    this.uiService.showMessageError(error.message);
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        onClear() {
        },
        async onSearch() {
            this.currentPage = '0'; // reiniciamos la página actual para la nueva búsqueda
            await this.getAsync();
        },
        async handleColumnSorting(columnSortData) {
            // Obtenemos los parámetros de la última búsqueda realizada ya que no queremos hacer el get con los parámetros que se hayan setteado sin haber cliqueado en Buscar
            this.getStoredParameters();
            this.setColumnSortData(columnSortData);
            await this.getAsync();
        },
        setColumnSortData(data) {
            if (data) {
                this.parameters.orderPropertyName = data.orderPropertyName;
                this.parameters.orderDirection = data.orderDirection;
            } else {
                this.parameters.orderPropertyName = "EstadoModificacionFechaHora";
                this.parameters.orderDirection = "1";
            }
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
            this.saveParameters();
            this.uiService.showSpinner(true);
            return await new ajax()
                .get(RESULTADOS_PROCESOS_URL, this.parameters)
                .then((res) => {
                    this.recordsTotal = res.recordsTotal;
                    this.list = res.list;
                    if (this.list.length == 0) this.resultsMessage = NO_DATA_MESSAGE

                    this.uiService.onlyDestroyDataTablesManual(this.idTable);
                })
                .then(() => {
                    if (this.list.length > 0) this.uiService.transformToDataTablesManual(this.idTable);
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
            this.$router.push({ name: "detail", params: { id: id } });
        },
        setUpdate(id) {
            this.$router.push({ name: "edit", params: { id: id } });
        }
    },
};
</script>