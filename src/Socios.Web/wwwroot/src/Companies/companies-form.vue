<template>
  <div>
    <div class="col-12">
      <div class="col-12 mt-4">
        <p class="h5">Empresa</p>
      </div>
      <div class="card">
        <div class="card-body">
          <div class="row">
            <div :class="mode.isEdit ? 'form-group required col mb-4' : 'col mb-4'">
              <label class="control-label">Número</label>
              <input class="form-control" :disabled="mode.isDetail" type="number" min="0" v-model="company.number" />
              <span class="text-danger field-validation-error">
                {{ errorBag.get("Number") }}
              </span>
            </div>
            <div :class="mode.isEdit ? 'form-group required col mb-4' : 'col mb-4'">
              <label class="control-label">Nombre</label>
              <input class="form-control" :disabled="mode.isDetail" type="text" maxlength="50" v-model="company.name" />
              <span class="text-danger field-validation-error">
                {{ errorBag.get("Name") }}
              </span>
            </div>
            <!-- seguir desde acá -->
            <div :class="mode.isEdit ? 'form-group required col mb-4' : 'col mb-4'">
              <label class="control-label">Razón social</label>
              <input class="form-control" :disabled="mode.isDetail" type="text" maxlength="70" v-model="company.businessName" />
              <span class="text-danger field-validation-error">
                {{ errorBag.get("BusinessName") }}
              </span>
            </div>
            <div class="col mb-4">
              <label for="valid-date-to">Identificador fiscal</label>
              <input class="form-control" :disabled="mode.isDetail" v-model="company.taxId" type="text" maxlength="40" />
              <span class="text-danger field-validation-error">
                {{ errorBag.get("TaxId") }}
              </span>
            </div>

            <!-- <label for="valid-date-to">Moneda</label>
            <currency-select :disabled="mode.isDetail" v-model="company.newCompanyCurrency.currencyIdm" />
            <button class="btn btn-outline-primary btn-sm form-control"
              :disabled="mode.isDetail && company.newCompanyCurrency.currencyIdm != null" @click="addNewCurrency">
              <b><i class="fas fa-plus"></i>&nbsp;Agregar</b>
            </button>
            <div class="form-check form-switch col-lg-6 col-sm-12 mb-4">
              <label class="form-check-label">Predeterminado</label>
              <input class="form-check-input" :disabled="mode.isDetail"
                v-model="company.newCompanyCurrency.isSalesDefault" type="checkbox" />
            </div> -->

            <div class="card mt-4">
              <div class="card-title mt-4 ml-4">
                <h5 class="fw-bold">Monedas de la empresa</h5>
              </div>
              <div class="card-body">
                <div class="row d-flex" v-if="mode.isEdit">
                  <div class="col mb-4">
                    <label>Moneda</label>
                    <currency-select :disabled="mode.isDetail" v-model="company.newCompanyCurrency.currencyIdm" />
                    <span class="text-danger field-validation-error">
                      {{ errorBag.get("CurrencyIdm") }}
                    </span>
                    <span class="text-danger field-validation-error">
                      {{ errorBag.get("CompanyCurrencies") }}
                    </span>
                  </div>
                  <div class="col d-flex mt-4">
                    <div class="col form-check form-switch">
                      <label class="form-check-label">Predeterminado</label>
                      <input class="form-check-input" :disabled="mode.isDetail" v-model="company.newCompanyCurrency.isSalesDefault" type="checkbox" />
                    </div>
                    <div class="col">
                      <button class="btn btn-outline-primary btn-sm form-control" :disabled="mode.isDetail && company.newCompanyCurrency.currencyIdm != null" @click="addNewCurrency">
                        <b><i class="fas fa-plus"></i>&nbsp;Agregar</b>
                      </button>
                    </div>
                  </div>
                </div>
                <!-- <div>
                  <label>Moneda</label>
                  <currency-select :disabled="mode.isDetail" v-model="company.newCompanyCurrency.currencyIdm" />
                </div> -->

                <table class="
                        table table-bordered table-striped table-hover table-white
                        fix-head 
                      " convert-to-datatable-manual no-paging :id="`Currency`">
                  <thead class="table-top">
                    <tr class="text-center">
                      <th scope="col">Nombre</th>
                      <th class="col">Código</th>
                      <th class="col-1">Predeterminada</th>
                      <th class="col-1" v-if="mode.isEdit">Eliminar</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr v-if="company.companyCurrencies.filter(cc => !cc.removed).length === 0" class="no-data text-center">
                      <td class="text-center" colspan="7">No hay monedas</td>
                    </tr>
                    <tr v-for="(cc, index) in company.companyCurrencies.filter(cc => !cc.removed)" :key="index">
                      <td>{{ cc.name }}</td>
                      <td>{{ cc.code }}</td>
                      <td class="text-center">
                        <div v-show="mode.isEdit" class="form-switch">
                          <input type="checkbox" class="form-check-input" v-model="cc.isSalesDefault" @change="change(cc)">
                        </div>
                        <div v-show="cc.isSalesDefault && mode.isDetail">
                          <i class="fa fa-check" style="color: #5cb85c"></i>
                        </div>
                        <div v-show="!cc.isSalesDefault && mode.isDetail">
                          <i class="fa fa-times" style="color: #d9534f"></i>
                        </div>
                      </td>
                      <td v-if="mode.isEdit">
                        <div class="justify-content-center d-flex no-wrap">
                          <inline-delete :disabled="mode.isDetail" @click="deleteCompanyCurrency(cc)"></inline-delete>
                          <!-- <inline-delete @click="setDelete(serviceEnablement.id)"></inline-delete> -->
                        </div>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="col-12 d-flex justify-content-end mb-3 mt-3">
      <div>
        <accept-button v-if="mode.isEdit" @click="acceptCompany">Aceptar</accept-button>
        <edit-button v-if="mode.isDetail && grants.update" @click="setUpdate">Editar</edit-button>
        <!-- <edit-button
                v-if="mode.isDetail"
                @click="setUpdate"
                >Editar</edit-button
              > -->
        <delete-button v-if="mode.isDetail && grants.delete" @click="setDelete">Eliminar</delete-button>
        <!-- <delete-button
                v-if="mode.isDetail"
                @click="setDelete"
                >Eliminar</delete-button
              > -->
        <cancel-button v-if="mode.isEdit" @click="cancel">Cancelar</cancel-button>
        <cancel-button v-if="mode.isDetail" @click="cancel">Volver</cancel-button>
      </div>
    </div>
  </div>
</template>

<script>
import AcceptButton from "@/components/forms/accept-button.vue";
import CancelButton from "@/components/forms/cancel-button.vue";
import EditButton from "@/components/forms/edit-button.vue";
import DeleteButton from "@/components/forms/delete-button.vue";
import UiService from "@/common/uiService";
import CurrencySelect from "@/Selects/currency-select.vue";
import inlineDelete from "@/components/forms/inline-delete-button.vue";

export default {
  name: "companies-form",

  components: {
    AcceptButton,
    CancelButton,
    EditButton,
    DeleteButton,
    CurrencySelect,
    inlineDelete,
  },

  data: function () {
    return {
      uiService: new UiService(),
    };
  },

  computed: {
    mode() {
      return this.$store.getters.getMode;
    },
    errorBag() {
      return this.$store.getters.getErrorBag;
    },
    company: {
      get() {
        return this.$store.getters.getCompany;
      },
      set(company) {
        this.$store.dispatch("setCompany", company);
      },
    },
    grants() {
      return this.$store.getters.getGrants;
    },
  },

  mounted() {
    switch (this.$router.currentRoute.name) {
      case "create":
        this.$store.dispatch("resetCompany").then((x) => {
          this.mode.setCreate();
        });
        break;
      case "detail":
        this.$store
          .dispatch("loadCompany", this.$router.currentRoute.params.id)
          .then((x) => {
            this.mode.setDetail();
          });
        break;
      case "edit":
        this.$store
          .dispatch("loadCompany", this.$router.currentRoute.params.id)
          .then((x) => {
            this.mode.setUpdate();
          });
        break;
      default:
        this.uiService.showMessageMainErrorAndLock("Modo inválido");
        this.$router.push("/list");
        break;
    }
  },
  methods: {
    async deleteCompanyCurrency(cc) {
      await this.$store.dispatch("deleteCompanyCurrency", cc).then(() => {
        if (this.errorBag.hasErrors()) {
          this.uiService.showMessageMainErrorAndLock(
            `Se han producido errores: ${this.errorBag.get("")}`
          );
        } else {
          this.uiService.showMessageSuccess(`Moneda eliminada`);
        }
      });
    },
    change(cc) {
      // cc.isSalesDefault = !cc.isSalesDefault;
      this.company.companyCurrencies.forEach((e) => {
        if (cc.isSalesDefault && e.isSalesDefault && cc.code != e.code) {
          e.isSalesDefault = false;
        }
      });
    },
    addNewCurrency() {
      this.$store.dispatch("addNewCurrency");
    },
    setUpdate() {
      this.mode.setUpdate();
      this.$router.push({
        name: "edit",
        params: { id: this.company.id },
      });
    },
    cancel() {
      if (this.mode.isDetail || this.mode.isCreate) {
        // botón de Volver
        this.$store.dispatch("resetCompany");
        this.$router.push("/list");
      } else {
        // botón de Cancelar
        this.mode.setDetail();
        this.$router.push({
          name: "detail",
          params: { id: this.company.id },
        });
      }
      this.errorBag.clear();
    },
    async acceptCompany() {
      await this.saveCompany();
    },
    async confirmCompanyCreation() {
      if (!this.errorBag.hasErrors()) {
        this.uiService.showMessageSuccess(`Nueva empresa confirmada`);
        this.mode.setDetail();
        this.$router.push({
          name: "detail",
          params: { id: this.acceptCompany.id },
        });
      } else {
        this.uiService.showMessageErrorAndFocus("Operación rechazada.");
      }

      if (this.errorBag.hasGlobalErrors()) {
        this.uiService.showMessageMainErrorAndLock(
          `Hubo un error al tratar de crear la empresa: ${this.errorBag.get(
            ""
          )}`
        );
      }
    },
    async saveCompany() {
      if (
        await this.uiService.confirmActionModal(
          `¿Está usted seguro que desea guardar la empresa?`,
          "Aceptar",
          "Cancelar"
        )
      ) {
        // let currentCompany = this.company;

        // this.$store.dispatch("setCompany", currentCompany);

        await this.$store.dispatch("saveCompany").then((id) => {
          if (!this.errorBag.hasErrors()) {
            if (this.mode.isUpdate) {
              this.uiService.showMessageSuccess(`Empresa confirmada`);
              this.loadCompanyById(this.company.id);
            } else {
              this.uiService.showMessageSuccess(`Nueva Empresa confirmada`);
              this.loadCompanyById(id);
            }
          } else {
            this.uiService.showMessageErrorAndFocus("Operación rechazada.");
          }
        });
        // this.confirmCompanyCreation();
      }
    },
    async loadCompanyById(id) {
      await this.$store.dispatch("loadCompany", id).then((pe) => {
        this.mode.setDetail();
        this.$router.push({
          name: "detail",
          params: { id: id },
        });
      });
    },
    upsert() {},
    async setDelete() {
      if (
        await this.uiService.confirmActionModal(
          `¿Está usted seguro que desea eliminar la empresa?`,
          "Aceptar",
          "Cancelar"
        )
      ) {
        await this.$store.dispatch("deleteCurrentCompany").then(() => {
          if (this.errorBag.hasErrors()) {
            this.uiService.showMessageMainErrorAndLock(
              `Se han producido errores: ${this.errorBag.get("")}`
            );
          } else {
            this.uiService.showMessageSuccess(`Se ha eliminado la empresa`);
            this.$router.push("/list");
            // this.mode.setNone();
            // await this.$store.dispatch("loadCompanyList");
          }
        });
      }
    },
  },
};
</script>
