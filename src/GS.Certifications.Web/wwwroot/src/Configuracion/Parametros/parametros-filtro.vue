<template>
  <div>
    <div class="col-12 mt-4">
      <p class="h5">{{ loc["Filtro de búsqueda"] }}</p>
    </div>
    <!-- Title end -->
    <div class="col-12">
      <!-- Filters card -->
      <div class="card">
        <div class="card-body">
          <form method="get">
            <div class="row justify-content-center">
              <div class="col-lg-3 col-md-6 col-sm-12 mb-4">
                <label>{{ loc["Id"] }}</label>
                <input type="text" class="form-control" v-model="parameters.Id" />
                <span class="text-danger field-validation-error" data-valmsg-replace="true">
                {{ errorBag.get("id") }}
              </span>
              </div>
              <div class="col-lg-3 col-md-6 col-sm-12 mb-4">
                <label>{{ loc["Clave"] }}</label>
                <input type="text" class="form-control" v-model="parameters.Clave" />
              </div>
              <div class="col-lg-3 col-md-6 col-sm-12 mb-4">
                <label>{{ loc["Valor"] }}</label>
                <input type="text" class="form-control" v-model="parameters.Valor" />
              </div>
              <div class="col-lg-3 col-md-6 col-sm-12 mb-4">
                <label>{{ loc["Descripción"] }}</label>
                <input type="text" class="form-control" v-model="parameters.Descripcion" />
              </div>

              <div class="col-12 d-flex justify-content-center">
                <button v-on:click.prevent="search" class="btn btn-primary btn-sm">
                  <i class="fas fa-search"></i>
                  {{ loc["Buscar"] }}
                </button>
                <button tabindex="12" @click.prevent="clearFilters" class="btn btn-secondary btn-sm ms-2">
                  <i class="fas fa-eraser"></i>
                  {{ loc["Limpiar"] }}
                </button>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

    <script>
import loc from "@/common/commonLoc";
import UiService from "@/common/uiService";
//import parameters from "@/common/parameters";

import Parameters from "./Parameters";

export default {
  name: "parametros-filtro",

  components: {
  },

  props: {
    value: Object
  },

  data: function () {
    return {
      loc,
      uiService: new UiService(),
      idModalLookupAgente: "agente_lookup",
      idModalLookupInstalacion: "instalacion_lookup",
    };
  },
  computed: {
    parameters: {
      get: function() {
          return this.value;
      },
      set: function(newValue) {
          this.$emit('input', newValue);
      }
    },

    errorBag() {
      return this.$store.getters.getErrorBag;
    },

    mode() {
      return this.$store.getters.getMode;
    },
  },

  watch: {},

  mounted() {
    //this.recoverParametersFromParametersService();
  },

  methods: {

    /*
    clearFilters() {
      this.parameters = {
        id: null,
        clave: null,
        valor: null,
        descripcion: null,
      };
      this.parametersService.saveSearchParameters(null); // limpiamos los parametros guardados en sesión
    },
    */

    recoverParametersFromParametersService() {
      let storageParams = this.parametersService.getSearchParameters();
      if (storageParams != null) {
        this.$store.dispatch("setSearchFilterParams", storageParams);
      }
    },

    saveParametersOnParametersService() {
      this.parametersService.saveSearchParameters(this.parameters);
    },


    search() {
        clearMessage();
        this.$emit('search')
    },

    clearFilters() {
        this.parameters = new Parameters();
        this.$emit('clear')
    },

    /*
    async search() {
      this.saveParametersOnParametersService();
      clearMessage();
      await this.$store.dispatch("loadEntitiesList")
      .then(searchSuccessful => {
        if (!searchSuccessful) {
          this.uiService.showMessageError("Operación rechazada.");
        }
      })

      .catch((ex) => {
              this.uiService.showMessageMainError(ex.message);
        });
    },
    */
  },
};
</script>