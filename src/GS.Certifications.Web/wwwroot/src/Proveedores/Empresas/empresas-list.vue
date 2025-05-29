<template>
    <div>

        <empresas-filter v-model="parameters" @clear="onClear()" @search="onSearch" />

        <br />

        <div class="col-12 d-flex justify-content-between align-items-center mt-2 mb-2">
            <p class="h5 m-0">{{ loc["Listado de Empresas"]}}</p> <!-- Modificar según el caso -->
            <button :disabled="!grants.createEmpresa" type="button" class="btn btn-outline-primary btn-sm" @click="create">
                <b><i class="fas fa-plus"></i>{{ loc["Agregar"]}}</b>
            </button>
        </div>

        <div class="col-12 table-responsive">
            <datatableRecordsLength v-model="recordsLength" v-if="list.length > 0" :id="`${idTable}-recordsLength`"
                @changed="onRecordsLengthChanged" />
            <table :id="`${idTable}`" convert-to-datatable-manual server-side-paging
                class="table table-sm table-bordered table-striped table-hover">
                <thead class="table-top">
                    <tr class="text-center align-middle">
                        <th data-column="RazonSocial" class="text-center">{{ loc["Razón Social"]}}</th>
                        <th data-column="IdentificadorTributario" class="text-center w-15">{{ loc["Identificador Tributario"]}}</th>
                        <th data-column="CodigoProveedor" class="text-center w-10">{{ loc["Codigo Proveedor"]}}</th>
                        <th data-column="Contacto" class="text-center w-20">{{ loc["Contacto"]}}</th>
                        <th data-column="Confirmado" style="width: 7% !important;">{{ loc["Confirmado"]}}</th>
                        <th class="text-center w-10" no-sort-datatable>{{ loc["Acciones"]}}</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-if="list.length === 0" class="no-data text-center">
                        <td class="text-center" colspan="100">{{ resultsMessage }}</td>
                    </tr>
                    <template v-for="item in list">
                        <tr :key="item.id">
                            <td class="text-right align-middle">
                                <a type="button" class="btn-link" @click="seeDetail(item.id)">
                                    {{ item.razonSocial }}
                                </a>
                            </td>
                            <td class="text-right align-middle">{{ item.identificadorTributario }}</td>
                            <td class="text-right align-middle">{{ item.codigoProveedor }}</td>
                            <td class="text-right align-middle">{{ item.contacto }}</td>
                            <!-- <td class="text-right align-middle">{{ item.confirmado }}</td> -->
                            <td class="text-center align-middle">
                                <div v-if="item.confirmado == true" class="text-success">
                                    <i class="fas fa-check-circle"></i>
                                </div>

                                <div v-else class="text-danger">
                                    <i class="fas fa-times-circle"></i>
                                </div>
                            </td>
                            <td class="text-center align-middle">
                                <div class="d-inline-flex">
                                    <inlineEdit :enabled="grants.updateEmpresa" @click="update(item.id)" />
                                    <!-- Habilitar este botón si aplica -->
                                    <inlineDelete :enabled="grants.deleteEmpresa" @click="remove(item)" />
                                </div>
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
import UiService from "@/common/uiService";
import inlineEdit from "@/components/forms/inline-edit-button.vue";
import inlineDelete from "@/components/forms/inline-delete-button.vue";
import SessionParametersService from "@/Common/SessionParametersService";
import datatablePagination from "@/components/pagination/datatablePagination.vue";
import datatableRecordsLength from "@/components/pagination/datatableRecordsLength.vue";
import Parameters from "./Parameters";
import columnSortedMixin from '@/Common/Mixins/columnSortedMixin';
import empresasFilter from './empresas-filter.vue';

import loc from "@/common/commonLoc.js"

const NO_DATA_MESSAGE = loc["No hay datos"];
const SEARCH_RESULTS_MESSAGE = loc["Click en 'Buscar' para traer resultados"];

export default {
    name: "empresas-list",
    mixins: [columnSortedMixin],
    components: {
        inlineEdit,
        inlineDelete,
        empresasFilter,
        datatablePagination,
        datatableRecordsLength
    },
    props: {
    },
    computed: {
        idTable() {
            return this.$store.getters.getIdTable;
        },
        grants() {
            return this.$store.getters.getGrants;
        },
        searched() {
            return this.$store.getters.getSearched;
        },
        resultsMessage() {
            return this.searched ? NO_DATA_MESSAGE : SEARCH_RESULTS_MESSAGE;
        },
        errorBag() {
            return this.$store.getters.getErrorBag;
        },
        list() {
            return this.$store.getters.getList;
        },
        recordsTotal() {
            return this.$store.getters.getRecordsTotal;
        },
    },
    data: function () {
        return {
            loc,
            parameters: {},
            currentPage: '0',
            recordsLength: 100,
            uiService: new UiService()
        };
    },
    async mounted() {
        await this.init();
    },
    methods: {
        async init() {
            this.$store.dispatch("setSearched", false);

            this.getStoredParameters();

            // Al obtener los parámetros guardados en sesión, actualizamos currentPage y recordsLength
            this.currentPage = this.parameters.start;
            this.recordsLength = this.parameters.length;

            if (this.$route.query.fromDetail) {
                await this.getAsync();
            }
        },
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
        async remove(dto) {
            if (
                await this.uiService.confirmActionModal(
                loc["¿Está usted seguro que desea eliminar a esta empresa portal?"],
                loc["Aceptar"],
                loc["Cancelar"]
                )
            ){
                this.uiService.showSpinner(true)
                await this.$store.dispatch("deleteAsync", dto)
                    .then(async () => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess(loc["Operación confirmada"])
                            await this.getAsync();
                        } else {
                            this.uiService.showMessageError(loc["Operación rechazada"])
                        }
                    })
                    .finally(() => {
                        this.uiService.showSpinner(false);
                    });
            }
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
                this.parameters.orderPropertyName = "Created"; // Settear columna de ordenamiento por defecto
                this.parameters.orderDirection = "1"; // Settear dirección de ordenamiento por defecto
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
            await this.$store.dispatch("getListAsync", this.parameters)
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        create() {
            // Limpiamos la lista antes de navegar
            this.errorBag.clear();
            this.$store.dispatch("clearList");
            this.$router.push({ name: "create" });
        },
        seeDetail(id) {
            // Limpiamos la lista antes de navegar
            this.errorBag.clear();
            this.$store.dispatch("clearList");
            this.$router.push({ name: "detail", params: { id: id } });
        },
        update(id) {
            // Limpiamos la lista antes de navegar
            this.errorBag.clear();
            this.$store.dispatch("clearList");
            this.$router.push({ name: "edit", params: { id: id } });
        }
    },
};
</script>