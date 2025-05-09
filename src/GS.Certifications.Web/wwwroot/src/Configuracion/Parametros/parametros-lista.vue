<template>
  <div class="mb-3">
    <parametros-filtro v-model="parameters" @search="onSearch" @clear="onClear" />
    <br />
    <div class="col-12 d-flex justify-content-between align-items-center mt-5 mb-1">
      <p class="h5 m-0">{{loc[`Listado de ${titleName}`]}}</p>
      <button @click="setCreate" type="button" class="btn btn-outline-primary btn-sm">
        <b><i class="fas fa-plus"></i>
          {{ loc['Agregar'] }}</b>
      </button>
    </div>
    <div class="col-12 table-responsive">
      <datatableRecordsLength v-if="parametrosLista.length > 0" v-model="recordsLength" :id="`${idTable}-recordsLength`"
        @changed="onRecordsLengthChanged"></datatableRecordsLength>
      <table :id="`${idTable}`" convert-to-datatable-manual class="table table-bordered table-striped table-hover"
      server-side-paging>
        <thead class="table-top">
          <tr class="text-center align-middle">
            <th data-column="Id" default-sort-datatable="asc" class="text-center">{{loc["Id"]}}</th>
            <th data-column="Clave" class="text-center">{{loc["Clave"]}}</th>
            <th data-column="Valor" class="text-center">{{loc["Valor"]}}</th>
            <th data-column="Descripcion" class="text-center">{{loc["Descripción"]}}</th>
            <th no-sort-datatable class="text-center">{{loc["Editar"]}}</th>
          </tr>
        </thead>
        <tbody>
          <tr v-if="parametrosLista.length === 0" class="no-data text-center">
            <td class="text-center" colspan="100">{{loc[`${emptyListMessage}`]}}</td>
          </tr>
          <template v-if="parametrosLista.length > 0">
            <tr v-for="parametro in parametrosLista" :key="parametro.id">
              <td class="text-right align-middle">
                <a class="btn btn-link" @click="setDetail(parametro.id)" > {{ parametro.id }} </a>
              </td>
              <td class="text-right align-middle">
                {{ parametro.clave }}
              </td>
              <td class="text-right align-middle">
                {{ parametro.valor }}
              </td>
              <td class="text-right align-middle">
                {{ parametro.descripcion }}
              </td>
              <td class="text-center">
                <inline-edit @click="setUpdate(parametro.id)"></inline-edit>
              </td>
            </tr>
          </template>
        </tbody>
      </table>
      <datatablePagination v-if="parametrosLista.length > 0" v-model="currentPage" :id="`${idTable}-datatable`"
        @page-changed="onPageChanged" :recordsTotal="recordsTotal" :recordsLength="recordsLength" :maxVisiblePages="5">
      </datatablePagination>
    </div>
  </div>
</template>

<script>
import inlineEdit from "@/components/forms/inline-edit-button.vue";
import inlineDelete from "@/components/forms/inline-delete-button.vue";
import loc from "@/common/commonLoc";
import UiService from "@/common/uiService";
import parametrosFiltro from "./parametros-filtro.vue";

import columnSortedMixin from '@/Common/Mixins/columnSortedMixin';

import datatablePagination from "@/components/pagination/datatablePagination.vue";
import datatableRecordsLength from "@/components/pagination/datatableRecordsLength.vue";

import Parameters from "./Parameters";

import SessionParametersService from '@/Common/SessionParametersService';

const EMPTY_LIST_MESSAGE = "No hay datos";
const FIRST_LIST_MESSAGE = "Click en 'Buscar' para traer resultados";
const DEFAULT_NO_DATA_MSG = "Click en 'Buscar' para traer resultados";
export default {
  name: "parametros-list",
  mixins: [columnSortedMixin],
  components: {
    parametrosFiltro,
    inlineEdit,
    inlineDelete,
    datatablePagination,
    datatableRecordsLength
  },

  data: function () {
    return {
      loc,
      uiService: new UiService(),
      titleName: "Parametros",
      idModalEdit: "editModal",
      idModalHistoria: "historiaModal",
      loadHistorial: false,
      modalKey: 0,
      reload: 0,
      noDataMsg: DEFAULT_NO_DATA_MSG,
      parameters: {},
      currentPage: '0',
      recordsLength: 25,
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
    async getAsync() {
      this.saveParameters()
      return await this.$store.dispatch("loadEntitiesList", this.parameters);
    },
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
            this.parametros.orderPropertyName = 'Id';
            this.parametros.orderDirection = '0';
        }
    },
    onClear() {

    },
    async onSearch() {
      this.currentPage = '0'; // reiniciamos la página actual para la nueva búsqueda
      await this.getAsync();
      console.log(this.parameters);
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

    setCreate() {
      this.$store.dispatch("setList", []);
      this.$store.dispatch("destroyTable");

      this.$router.push({name: "create"});
    },
    setUpdate(id) {
      this.$router.push({name: "edit", params: {id: id, } });
    },
    setDetail(id) {
      this.$router.push({name: "detail", params: {id: id, } });
    },
  },

  computed: {
    recordsTotal() {
      return this.$store.getters.getRecordsTotal;
    },
    mode() {
      return this.$store.getters.getMode;
    },
    errorBag() {
      return this.$store.getters.getErrorBag;
    },
    idTable() {
      return this.$store.getters.getIdTable;
    },

    parametrosLista : {
      get() {
        return this.$store.getters.getParametrosLista;
      },
      set(list) {
        this.$store.dispatch("setList", list);
      }
    },

    grants() {
      return this.$store.getters.getGrants;
    },
    emptyListMessage() {
      return this.$store.getters.getHasBeenSearched
        ? EMPTY_LIST_MESSAGE
        : FIRST_LIST_MESSAGE;
    },
  },

};
</script>