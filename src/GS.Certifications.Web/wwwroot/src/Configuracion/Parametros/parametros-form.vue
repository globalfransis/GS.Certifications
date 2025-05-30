<template>
  <div>
    <div class="col-12">
      <div class="col-12 mt-4">
        <p class="h5">{{loc["Datos del Parametro"]}}</p>
      </div>
      <div class="card">
        <div class="card-body">
          <div class="row">

            <div class='form-group required col-lg-6 col-md-6 col-sm-12 mb-4'>
              <label class="control-label">{{loc["Clave"]}}</label>
              <input :disabled="!mode.isEdit" maxlength="100" class="form-control" v-model="parametro.clave" />
              <span class="text-danger field-validation-error" data-valmsg-for="clave" data-valmsg-replace="true">
                {{ errorBag.get("clave") }}
              </span>
            </div>

            <div class='form-group required col-lg-6 col-md-6 col-sm-12 mb-4'>
              <label class="control-label">{{loc["Valor"]}}</label>
              <input :disabled="!mode.isEdit" maxlength="250" class="form-control" v-model="parametro.valor" />
              <span class="text-danger field-validation-error" data-valmsg-for="valor" data-valmsg-replace="true">
                {{ errorBag.get("valor") }}
              </span>
            </div>

            <div class='form-group required col-lg-6 col-md-6 col-sm-12 mb-4'>
              <label class="control-label">{{loc["Descripcion"]}}</label>
              <input :disabled="!mode.isEdit" maxlength="250" class="form-control" v-model="parametro.descripcion" />
              <span class="text-danger field-validation-error" data-valmsg-for="descripcion" data-valmsg-replace="true">
                {{ errorBag.get("descripcion") }}
              </span>
            </div>

          </div>
        </div>
      </div>
      <div class="col-12 d-flex justify-content-end mb-3 mt-3">
        <div>
          <accept-button v-if="mode.isCreate"  @click="save">Aceptar</accept-button>
          <accept-button v-if="mode.isUpdate"  @click="update">Actualizar</accept-button>
          <edit-button v-if="mode.isDetail" @click="setUpdate">Editar</edit-button>
          <!-- hacer seedings de permisos!!! -->
          <cancel-button v-on:click="cancel()">
            {{ loc[`${mode.isEdit ? 'Cancelar' : 'Volver'}`] }}
          </cancel-button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import AcceptButton from "@/components/forms/accept-button.vue";
import CancelButton from "@/components/forms/cancel-button.vue";
import EditButton from "@/components/forms/edit-button.vue";
import loc from "@/common/commonLoc";

export default {
  name: "parametros-create",

  components: {
    AcceptButton,
    CancelButton,
    EditButton,
  },

  data: function () {
    return {
      loc,
      esDetalle: false,
    };
  },

  computed: {
    parametro(){
      return this.$store.getters.getParametro;
    },

    mode() {
      return this.$store.getters.getMode;
    },

    grants() {
      return this.$store.getters.getGrants;
    },

    errorBag() {
      return this.$store.getters.getErrorBag;
    },
    list() {
      return this.$store.getters.getParametrosLista;
    },

  },

  async mounted() {
    switch (this.$router.currentRoute.name) {
      case "create":
        this.esDetalle = false;
        this.$store.dispatch("resetParametro");
        this.mode.setCreate();
        break;
      case "edit":
        this.esDetalle = true;
        await this.$store
          .dispatch("getParametro", this.$router.currentRoute.params.id)
          .then(() => {
            this.mode.setUpdate();
          });
        break;
      case "detail":
        this.esDetalle = true;
        await this.$store
          .dispatch("getParametro", this.$router.currentRoute.params.id)
          .then(() => {
            this.mode.setDetail();
          });
        break;
      default:
        this.$router.push("/list");
        break;
    }
  },

  methods: {

    async save() {
      await this.$store.dispatch("saveParametro").then((id) => {
        if (!this.errorBag.hasErrors()) {
          this.$store.getters.getUiService.showMessageSuccess(
            `Operación confirmada.`
          );
          this.$router.push({
            name: "detail",
            params: {
              id: id,
            },
          });
        } else {
          this.$store.getters.getUiService.showMessageError("Operación rechazada.");
        }
      })
      .catch((ex) => {
        this.$store.getters.getUiService.showMessageError("Operación rechazada.");
      });
    },

    async update() {
      await this.$store.dispatch("updateParametro").then((id) => {
        if (!this.errorBag.hasErrors()) {
          this.$store.getters.getUiService.showMessageSuccess(
            `Operación confirmada.`
          );
          this.$router.push({
            name: "detail",
            params: {
              id: this.parametro.id,
            },
          });
        }
      });
    },

    async cancel() {
      this.errorBag.clear();
      if(this.esDetalle){
        this.$router.push({ name: "home", query: {fromDetail: true} });
      } else {
        this.$store.commit("SET_HAS_BEEN_SERCHED",false)
        this.$router.push("/list");
      }
    },

    async setUpdate(){
      this.$router.push({
        name: "edit",
        params: {
          id: this.parametro.id,
        },
      });
    }
  },
};
</script>