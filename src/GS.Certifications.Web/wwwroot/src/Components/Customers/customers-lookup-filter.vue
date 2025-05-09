<template>
  <div>
    <div class="col-12 mt-4">
      <p class="h5">Filtro de búsqueda</p>
    </div>
    <!-- Title end -->
    <div class="col-12">
      <!-- Filters card -->
      <div class="card">
        <div class="card-body">
          <form method="get">
            <div class="row justify-content-center">
              <div class="col-lg-4 col-md-2 col-sm-12 mb-4">
                <label>Número</label>
                <input
                  type="number"
                  class="form-control"
                  v-model="parameters.customerNumber"
                />
              </div>
              <div class="col-lg-4 col-md-2 col-sm-12 mb-4">
                <label>Tipo de documento</label>
                <identificationtype-select
                  :showoption="true"
                  v-model="parameters.identificationTypeId"
                ></identificationtype-select>
              </div>
              <div class="col-lg-4 col-md-2 col-sm-12 mb-4">
                <label>Nro. de doc.</label>
                <input
                  type="text"
                  class="form-control"
                  v-model="parameters.idNumber"
                />
              </div>
            </div>
            <div class="row justify-content-center">
              <div class="col-lg-4 col-md-2 col-sm-12 mb-4">
                <label>Nombre</label>
                <input
                  type="text"
                  class="form-control"
                  v-model="parameters.firstName"
                />
              </div>
              <div class="col-lg-4 col-md-2 col-sm-12 mb-4">
                <label>Apellido</label>
                <input
                  type="text"
                  class="form-control"
                  v-model="parameters.lastName"
                />
              </div>
              <div class="col-lg-4 col-md-2 col-sm-12 mb-4">
                <label>Correo</label>
                <input
                  type="text"
                  class="form-control"
                  v-model="parameters.email"
                />
              </div>
              <div class="col-lg-4 col-md-2 col-sm-12 mb-4">
                <label>Cant. registros</label>
                <select
                  v-model="parameters.quantity"
                  class="form-control"
                >
                  <option :value="5"> 
                    <!-- esto es de prueba XD -->
                    5
                  </option>
                  <option :value="10">
                    10
                  </option>
                  <option :value="50">
                    50
                  </option>
                  <option :value="100">
                    100
                  </option>
                  <option :value="200">
                    200
                  </option>
                  <option :value="null">
                    Todos
                  </option>
                </select>
              </div>
              <div class="col-12 d-flex justify-content-center">
                <button
                  v-on:click.prevent="search"
                  class="btn btn-primary btn-sm"
                >
                  <i class="fas fa-search"></i>
                  Buscar
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
import identificationtypeSelect from "@/selects/identificationtype-select";

export default {
  name: "customers-lookup-filter",
  components: {
    identificationtypeSelect,
  },
  props: {},
  data: function () {
    return {
      parameters: new Parameters(),
    };
  },
  mounted() {},
  methods: {
    search() {
      this.$emit("on-search");
    },
  },
};
class Parameters {
  constructor() {
    this.customerNumber = null;
    this.email = null;
    this.idNumber = null;
    this.identificationTypeId = null;
    this.firstName = null;
    this.lastName = null;
    this.quantity = 50;
  }
}
</script>
