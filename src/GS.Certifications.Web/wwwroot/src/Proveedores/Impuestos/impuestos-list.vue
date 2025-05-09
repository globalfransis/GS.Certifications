<template>
    <div>

        <impuestos-filter v-model="parameters" @clear="onClear()" @search="onSearch" />

        <br />

        <div class="col-12 d-flex justify-content-between align-items-center mt-2 mb-2">
            <p class="h5 m-0">Listado de Impuestos</p> <!-- Modificar según el caso -->
            <button :disabled="!grants.create" type="button" class="btn btn-outline-primary btn-sm" @click="create">
                <b><i class="fas fa-plus"></i>Agregar</b>
            </button>
        </div>

        <div class="col-12 table-responsive">
            <datatableRecordsLength v-model="recordsLength" v-if="list.length > 0" :id="`${idTable}-recordsLength`"
                @changed="onRecordsLengthChanged" />
            <table :id="`${idTable}`" convert-to-datatable-manual server-side-paging
                class="table table-sm table-bordered table-striped table-hover">
                <thead class="table-top">
                    <tr class="text-center align-middle">
                        <th data-column="Nombre" class="text-center">Nombre</th>
                        <th data-column="Alicuota.CodigoAFIP" class="text-center">Código AFIP</th>
                        <th data-column="Provincia.Name" class="text-center">Provincia</th>
                        <th no-sort-datatable class="text-center">Valor</th>
                        <th class="text-center" no-sort-datatable>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-if="list.length === 0" class="no-data text-center">
                        <td class="text-center" colspan="100">{{ resultsMessage }}</td>
                    </tr>
                    <template v-for="item in list">
                        <tr :key="item.id">
                            <!--
                            <td class="text-right align-middle">
                                <a type="button" class="btn-link" @click="seeDetail(item.id)">
                                    {{ item.razonSocial }}
                                </a>
                            </td>
                            -->
                            <td class="text-right align-middle wrap-text">
                                <a type="button" class="btn-link" @click="seeDetail(item.id)">
                                    {{ item.nombre }}
                                </a>
                            </td>
                            <td class="text-right align-middle">{{ item.codigoAFIP ? item.codigoAFIP : ' - '}}</td>
                            <td class="text-right align-middle">{{ item.provincia ? item.provincia : ' - ' }}</td>
                            <td class="text-end align-middle">{{ item.valor.toFixed(2) + '%' }}</td>
                            <td class="text-center align-middle">
                                <div class="d-inline-flex">
                                    <button
                                    type="button"
                                    title="Historial de Estados"
                                    @click="seeDetail(item.id)"
                                        class="col btn btn-link">
                                        <i class="fas fa-search"></i>
                                    </button>
                                    <inlineEdit :enabled="grants.update" @click="update(item.id)" />
                                    <inlineDelete :enabled="grants.delete" @click="remove(item)" />
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
import impuestosFilter from './impuestos-filter.vue';

const NO_DATA_MESSAGE = "No hay datos";
const SEARCH_RESULTS_MESSAGE = "Click en 'Buscar' para traer resultados";

export default {
    name: "impuestos-list",
    mixins: [columnSortedMixin],
    components: {
        inlineEdit,
        inlineDelete,
        impuestosFilter,
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
                "¿Está usted seguro que desea eliminar a este impuesto?",
                "Aceptar",
                "Cancelar"
                )
            ) {
                this.uiService.showSpinner(true)
                await this.$store.dispatch("deleteAsync", dto)
                    .then(async () => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            await this.getAsync();
                        } else {
                            if(this.errorBag.errors.hasOwnProperty('impuestoDetalle')){
                                this.uiService.showMessageError("Operación rechazada: Existe un comprobante que utiliza el impuesto")
                            } else { 
                                this.uiService.showMessageError("Operación rechazada") 
                            }
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
                this.parameters.orderPropertyName = "Id"; // Settear columna de ordenamiento por defecto
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
            this.$store.dispatch("clearList");
            this.$router.push({ name: "create" });
        },
        seeDetail(id) {
            // Limpiamos la lista antes de navegar
            this.$store.dispatch("clearList");
            this.$router.push({ name: "detail", params: { id: id } });
        },
        update(id) {
            // Limpiamos la lista antes de navegar
            this.$store.dispatch("clearList");
            this.$router.push({ name: "edit", params: { id: id } });
        }
    },
};
</script>

<style>
.wrap-text {
    word-break: break-word; /* Rompe la palabra si es necesario */
    max-width: 500px; /* Ajusta según el tamaño deseado */
    white-space: normal; /* Permite el salto de línea */
}
</style>