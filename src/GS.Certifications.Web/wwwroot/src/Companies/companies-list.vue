<template>
  <div class="mt-4">
    <companies-filter> </companies-filter>
    <br />
    <div
      class="col-12 d-flex justify-content-between align-items-center mt-5 mb-3"
    >
      <p class="h5 m-0">{{loc['Listado de empresas']}}</p>
      <button
        type="button"
        v-if="grants.create"
        class="btn btn-outline-primary btn-sm"
        @click="setCreate"
      >
        <b><i class="fas fa-plus"></i>
        {{loc['Agregar']}}</b>
      </button>
    </div>
    <table
      :id="`${idTable}`"
      convert-to-datatable-manual
      class="table table-bordered table-striped table-hover mt-4"
    >
      <thead class="table-top">
        <tr class="text-center align-middle">
          <th class="text-center d-none d-sm-block d-md-table-cell">
            {{loc['Número']}}
          </th>
          <th class="text-center d-none d-md-table-cell">
            {{loc['Identificador fiscal']}}
          </th>
          <th class="text-center">{{loc['Nombre']}}</th>
          <th class="text-center">{{loc['Razón social']}}</th>
          <th class="text-center" no-sort-datatable>{{loc['Acciones']}}</th>
        </tr>
      </thead>
      <tbody>
        <tr v-if="companiesList.length === 0" class="no-data text-center">
          <td class="text-center" colspan="7">{{loc['No hay datos']}}</td>
        </tr>
        <template v-for="company in companiesList">
          <tr :key="company.id">
            <td class="text-right align-middle">
              <a type="button" class="btn-link" @click="setDetail(company.id)">
                {{ company.number }}
              </a>
            </td>
            <td class="text-right align-middle">
              {{ company.taxId ? company.taxId : "-" }}
            </td>

            <td class="text-right align-middle">
              {{ company.name ?  company.name : "-" }}
            </td>

            <td class="text-right align-middle">
              {{ company.businessName ? company.businessName : "-" }}
            </td>

            <td class="text-center">
              <inline-edit v-if="grants.update" @click="setUpdate(company.id)"></inline-edit>
              <!-- <inline-edit @click="setUpdate(company.id)"></inline-edit> -->
              <inline-delete v-if="grants.delete" @click="setDelete(company.id)"></inline-delete>
              <!-- <inline-delete @click="setDelete(company.id)"></inline-delete> -->
            </td>
          </tr>
        </template>
      </tbody>
    </table>
  </div>
</template>

<script>
import inlineEdit from "@/components/forms/inline-edit-button.vue";
import inlineDelete from "@/components/forms/inline-delete-button.vue";
import companiesFilter from "./companies-filter.vue";
import loc from "@/common/commonLoc";
import companiesForm from './companies-form.vue';

export default {
  name: "companies-list",
  components: {
    inlineEdit,
    inlineDelete,
    companiesFilter,
    companiesForm
  },
  data: function () {
    return {
      loc
    };
  },
  mounted() {
    this.$store.dispatch("loadCompaniesList");
  },
  computed: {
    mode() {
      return this.$store.getters.getMode;
    },
    companiesList() {
      return this.$store.getters.getCompaniesList;
    },
    errorBag() {
      return this.$store.getters.getErrorBag;
    },
    idTable() {
      return this.$store.getters.getIdTable;
    },
    grants() {
      return this.$store.getters.getGrants;
    },
    uiService() {
      return this.$store.getters.getUiService;
    }
  },
  methods: {
    setUpdate(id) {
      clearMessage();
      this.mode.setUpdate();
      this.$router.push({ name: "edit", params: { id: id } });
    },
    setDetail(id) {
      clearMessage();
      this.mode.setDetail();
      this.$router.push({ name: "detail", params: { id: id } });
    },
    setCreate() {
      clearMessage();
      this.mode.setCreate();
      this.$router.push({ name: "create" });
    },
    async setDelete(id) {
      if (
        await this.uiService.confirmActionModal(
          loc[`¿Está usted seguro que desea eliminar esta empresa?`],
          loc["Aceptar"],
          loc["Cancelar"]
        )
      ) {
        await this.$store.dispatch("deleteCompanyById", id);
        if(!this.errorBag.hasErrors()) {
            await this.$store.dispatch("loadCompaniesList")
            this.uiService.showMessageSuccess(`Se ha eliminado la empresa`);
        }
      }
    },
  },
};
</script>