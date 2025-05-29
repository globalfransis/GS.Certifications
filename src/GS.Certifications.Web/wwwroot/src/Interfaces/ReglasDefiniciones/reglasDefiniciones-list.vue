<template>
    <div>

        <reglasDefiniciones-filter v-model="parameters" @clear="onClear()" @search="onSearch" />

        <br />

        <div class="col-12 d-flex justify-content-between align-items-center mt-2 mb-2">
            <p class="h5 m-0">{{loc["Listado de Definiciones de Reglas"]}}</p>
        </div>

        <div class="col-12 table-responsive">
            <div v-if="list.length > 0">
                <datatableRecordsLength v-model="recordsLength" :id="`${idTable}-recordsLength`"
                    @changed="onRecordsLengthChanged" />
            </div>
            <table :id="`${idTable}`" convert-to-datatable-manual server-side-paging
                class="table table-sm table-bordered table-striped table-hover">
                <thead class="table-top">
                    <tr class="text-center align-middle">
                        <th data-column="Idm" default-sort-datatable="desc" class="text-center">{{loc["Id"]}}</th>
                        <th data-column="Interfaz.Sistema.Descripcion" class="text-center">{{loc["Sistema"]}}</th>
                        <th data-column="Interfaz.DescripcionMiddleware" class="text-center">{{loc["Interfaz"]}}</th>
                        <th data-column="InterfazReglaConsecuencia.Descripcion" class="text-center">{{loc["Consecuencia"]}}</th>
                        <th data-column="Descripcion" class="text-center">{{loc["Descripción"]}}</th>
                        <!-- <th class="text-center" no-sort-datatable>Acciones</th> -->
                    </tr>
                </thead>
                <tbody>
                    <tr v-if="list.length === 0" class="no-data text-center">
                        <td class="text-center" colspan="100">{{ resultsMessage }}</td>
                    </tr>
                    <template v-for="item in list">
                        <tr :key="item.idm">

                            <td class="text-right align-middle">
                                <!-- <a type="button" class="btn-link" @click="setDetail(item.idm)">
                                    {{ item.idm }}
                                </a> -->
                                {{ item.idm }}
                            </td>

                            <td class="text-right align-middle">
                                {{ item.sistema }}
                            </td>

                            <td class="text-right align-middle">
                                {{ item.interfaz }}
                            </td>

                            <td class="text-right align-middle">
                                {{ item.consecuenci }}
                            </td>

                            <td class="text-right align-middle">
                                {{ item.descripcion }}
                            </td>

                            <!-- <td class="text-center align-middle">
                            </td> -->
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
import reglasDefinicionesFilter from "./reglasDefiniciones-filter.vue";
import SessionParametersService from "@/Common/SessionParametersService";
import datatablePagination from "@/components/pagination/datatablePagination.vue";
import datatableRecordsLength from "@/components/pagination/datatableRecordsLength.vue";
import Parameters from "./Parameters";
import columnSortedMixin from '@/Common/Mixins/columnSortedMixin';

import loc from "@/common/commonLoc.js"

const NO_DATA_MESSAGE = loc["No hay datos"];
const SEARCH_RESULTS_MESSAGE = loc["Click en 'Buscar' para traer resultados"];
const INTERFAZ_REGLAS_URL = baseUrl + "/api/Interfaces/Reglas";

export default {
    name: "reglasDefiniciones-list",
    mixins: [columnSortedMixin],
    components: {
        inlineEdit,
        reglasDefinicionesFilter,
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
            idTable: "reglasDefiniciones",
            resultsMessage: SEARCH_RESULTS_MESSAGE,
            parameters: {},
            currentPage: '0',
            recordsLength: 25,
            uiService: new UiService(),
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
                this.parameters.orderPropertyName = 'Idm';
                this.parameters.orderDirection = '1';
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
            this.uiService.showSpinner(true);
            this.saveParameters();
            return await new ajax()
                .get(INTERFAZ_REGLAS_URL, this.parameters)
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
        setDetail(id) {
            this.$router.push({ name: "detail", params: { id: id } });
        }
    },
};
</script>