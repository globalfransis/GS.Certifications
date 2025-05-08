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
              <div class="col mb-4">
                <label>{{ loc["Número"] }}</label>
                <input
                  class="form-control"
                  type="number"
                  v-model="parameters.number"
                />
              </div>
              <div class="col mb-4">
                <label>{{ loc["Nombre"] }}</label>
                <input
                  class="form-control"
                  type="text"
                  v-model="parameters.name"
                />
              </div>
              <div class="col mb-4">
                <label>{{ loc["Razón social"] }}</label>
                <input
                  class="form-control"
                  type="text"
                  v-model="parameters.businessName"
                />
              </div>
              <div class="col mb-4">
                <label>{{ loc["Identificador fiscal"] }}</label>
                <input
                  class="form-control"
                  type="text"
                  v-model="parameters.taxId"
                />
              </div>
              <div class="col-12 d-flex justify-content-center">
                <button
                  v-on:click.prevent="search"
                  class="btn btn-primary btn-sm"
                >
                  <i class="fas fa-search"></i>
                  {{ loc["Buscar"] }}
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

export default {
  components: {
  },
  name: "companies-filter",
  props: {},
  data: function () {
    return {
      loc,
    };
  },
  mounted() {
  },
  methods: {
    search() {
      clearMessage();
      this.$store.dispatch("loadCompaniesList");
    },
  },
  computed: {
    parameters: {
      get() {
        return this.$store.getters.searchFilterParams;
      },
      set(value) {
        this.$store.dispatch("setSearchFilterParams", value);
      },
    },
    mode() {
      return this.$store.getters.getMode;
    },
  },
};
</script>