<template>
    <div>

        <comprobante-filter v-model="parameters" @clear="onClear()" @search="onSearch" />

        <br />

        <div class="col-12 d-flex justify-content-between align-items-center mt-2 mb-2">
            <p class="h5 m-0">Listado de Comprobantes</p> <!-- Modificar según el caso -->
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
                        <th class="w-10" data-column="Numero">Comprobante</th>
                        <th style="width: 5.5% !important;" data-column="FechaEmision" datatable-datetime>Fecha Emisión</th>
                        <th class="w-5" data-column="Moneda.Code">Moneda</th>
                        <th class="w-10" data-column="ImporteTotal">Total</th>
                        <th class="w-10" data-column="ModifiedBy">Último Usuario</th>
                        <th style="width: 5.5% !important;" data-column="FechaRegistracion">Fecha Carga</th>
                        <th style="width: 7% !important;" data-column="ComprobanteEstado.Descripcion">Estado</th>
                        <th class="w-5" no-sort-datatable>Propietario</th>
                        <th class="w-10" no-sort-datatable>Lectura QR</th>
                        <th class="w-10" no-sort-datatable>Validación QR</th>
                        <th class="w-10" no-sort-datatable>Validación Cmp.</th>
                        <th class="w-10" no-sort-datatable>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-if="list.length === 0" class="no-data text-center">
                        <td class="text-center" colspan="100">{{ resultsMessage }}</td>
                    </tr>
                    <template v-for="item in list">
                        <tr :key="item.id">
                            <td class="align-middle">
                                <a type="button" class="btn-link" @click="seeDetail(item.id)">
                                    {{ item.comprobante }}
                                </a>
                            </td>

                            <td class="align-middle text-center">
                                {{ item.fechaEmision | uidate }}
                            </td>
                            <td class="align-middle text-center">
                                {{ item.moneda }}
                            </td>
                            <td class="text-end align-middle">
                                {{ item.importeTotal | currency }}
                            </td>
                            <td class="align-middle">
                                {{ item.modifiedBy }}
                            </td>
                            <td class="align-middle text-center">
                                {{ item.fechaRegistracion | uidate }}
                            </td>
                            <td class="text-center">
                                <comprobanteEstado-label :value="item.comprobanteEstadoId" />
                            </td>
                            <td class="text-center">
                                <div v-if="item.propietarioActualId == PROPIETARIO_ACTUAL" class="text-success">
                                    <i class="fas fa-check-circle"></i>
                                </div>

                                <div v-else class="text-danger">
                                    <i class="fas fa-times-circle"></i>
                                </div>
                            </td>
                            <td class="text-center">
                                <div v-if="item.validacionQR" class="text-success"
                                title="QR leído"
                                data-toggle="tooltip">
                                    <i class="fas fa-check-circle"></i>
                                    <!-- <span>QR leído</span> -->
                                </div>

                                <div v-else class="text-danger"
                                title="QR No leído"
                                data-toggle="tooltip">
                                    <i class="fas fa-times-circle"></i>
                                    <!-- <span>QR No leído</span> -->
                                </div>
                            </td>

                            <td class="text-center">
                                <div v-if="item.estadoValidacionARCAQRId == NO_VALIDADA"
                                    class="text-warning-darker" title="QR sin constatar ARCA"
                                    data-toggle="tooltip">
                                    <i class="fas fa-question-circle"></i>
                                    <!-- <span>QR sin constatar ARCA</span> -->
                                </div>

                                <div v-if="item.estadoValidacionARCAQRId == RECHAZADA"
                                    class="text-danger" title="QR inválido ARCA"
                                    data-toggle="tooltip">
                                    <i class="fas fa-times-circle"></i>
                                    <!-- <span>QR inválido ARCA</span> -->
                                </div>

                                <div v-if="item.estadoValidacionARCAQRId == ERROR_VALIDACION"
                                    class="text-danger" title="QR error comunicación ARCA"
                                    data-toggle="tooltip">
                                    <i class="fas fa-times-circle"></i>
                                    <!-- <span>QR error comunicación ARCA</span> -->
                                </div>

                                <div v-if="item.estadoValidacionARCAQRId == VALIDADA"
                                    class="text-success" title="QR constatado ARCA"
                                    data-toggle="tooltip">
                                    <i class="fas fa-check-circle"></i>
                                    <!-- <span>QR constatado ARCA</span> -->
                                </div>
                            </td>

                            <td class="text-center">
                                <div v-if="item.estadoValidacionARCAId == NO_VALIDADA"
                                    class="text-warning-darker" title="Comp. sin constatar ARCA"
                                    data-toggle="tooltip">
                                    <i class="fas fa-question-circle"></i>
                                    <!-- <span>Comp. sin constatar ARCA</span> -->
                                </div>

                                <div v-if="item.estadoValidacionARCAId == RECHAZADA"
                                    class="text-danger" title="Comp. sin constatar ARCA"
                                    data-toggle="tooltip">
                                    <i class="fas fa-times-circle"></i>
                                    <!-- <span>Comp. inválido ARCA</span> -->
                                </div>

                                <div v-if="item.estadoValidacionARCAId == ERROR_VALIDACION"
                                    class="text-danger" title="Comp. error comunicación ARCA"
                                    data-toggle="tooltip">
                                    <i class="fas fa-times-circle"></i>
                                    <!-- <span>Comp. error comunicación ARCA</span> -->
                                </div>

                                <div v-if="item.estadoValidacionARCAId == VALIDADA"
                                    class="text-success" title="Comp. constatado ARCA"
                                    data-toggle="tooltip">
                                    <i class="fas fa-check-circle"></i>
                                    <!-- <span>Comp. constatado ARCA</span> -->
                                </div>
                            </td>
                            <td class="text-center align-middle">
                                <div class="d-inline-flex">
                                    <inlineEdit :enabled="grants.update" @click="update(item.id)" />
                                    <inlineDelete :enabled="grants.delete" @click="remove(item)" />
                                    <a :href="`${currentLocation}/SupplierWebRepository/Comprobantes/cmp_${item.guid}/${item.nombreArchivo}/${item.nombreArchivo}`"
                                        :download="item.nombreArchivo || 'comprobante'" title="Descargar comprobante"
                                        data-toggle="tooltip" class="btn btn-link text-body" target="_blank"
                                        rel="noopener noreferrer" v-if="item.guid && item.nombreArchivo">
                                        <i class="fas fa-download" aria-hidden="true"></i>
                                    </a>
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
import comprobanteFilter from './comprobante-filter.vue';
import comprobanteEstadoLabel from "@/Selects/comprobanteEstado-label.vue";

const NO_DATA_MESSAGE = "No hay datos";
const SEARCH_RESULTS_MESSAGE = "Click en 'Buscar' para traer resultados";

// Estados ARCA
const VALIDADA = 1;
const RECHAZADA = 2;
const ERROR_VALIDACION = 3;
const NO_VALIDADA = 4;

//Propietario actual 
const PROPIETARIO_ACTUAL = 1;

export default {
    name: "comprobante-list",
    mixins: [columnSortedMixin],
    components: {
        inlineEdit,
        inlineDelete,
        comprobanteFilter,
        datatablePagination,
        datatableRecordsLength,
        comprobanteEstadoLabel
    },
    props: {
    },
    computed: {
        currentLocation() {
            return window.location.origin;
        },
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
            uiService: new UiService(),

            // Estados validacion en ARCA
            VALIDADA,
            RECHAZADA,
            ERROR_VALIDACION,
            NO_VALIDADA,

            //Propietario actual 
            PROPIETARIO_ACTUAL
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
        setUpdate(cd) {
            cd.mode.setUpdate();
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
                "¿Está usted seguro que desea eliminar este comprobante?",
                "Aceptar",
                "Cancelar"
                )
            ){
                this.uiService.showSpinner(true)
                await this.$store.dispatch("deleteAsync", dto)
                    .then(async () => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            await this.getAsync();
                        } else {
                            this.uiService.showMessageError("Operación rechazada")
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
                this.parameters.orderPropertyName = ""; // Settear columna de ordenamiento por defecto
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
td {
  width:fit-content !important;
  max-width: 75px !important;
  vertical-align: middle !important;
  word-break: break-word !important;
}

th {
  width: fit-content !important;
  max-width: 100% !important;
  vertical-align: middle !important;
}
</style>