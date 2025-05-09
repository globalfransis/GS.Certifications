import Vue from "vue";
import Vuex from "vuex";
import ajax from "@/common/ajaxWrapper";
import CrudMode from "@/common/CrudMode";
import ErrorBag from "@/common/ErrorBag";
import SecurityService from "@/common/sercurityService";
import uiService from "@/common/uiService";

Vue.use(Vuex);

const SET_COMPANIES_LIST = "SET_COMPANIES_LIST";
const RESET_STATE = "RESET_STATE";
const LOADING = "LOADING";
const SET_SEARCH_FILTER_PARAMS = "SET_SEARCH_FILTER_PARAMS";
const SET_COMPANY = "SET_COMPANY";
const RESET_COMPANY = "RESET_COMPANY";
const SET_COMPANY_CURRENCIES = "SET_COMPANY_CURRENCIES";

const getCompaniesListURL = `${baseUrl}/api/Companies/Companies/GetCompanies`;
const getCompanyURL = `${baseUrl}/api/Companies/Companies/GetCompany`;
const postCompanyURL = `${baseUrl}/api/Companies/Companies/CreateCompany`;
const putCompanyURL = `${baseUrl}/api/Companies/Companies/UpdateCompany`;
const deleteCompanyURL = `${baseUrl}/api/Companies/Companies/DeleteCompany`;
const addNewCurrencyURL = `${baseUrl}/api/Companies/Companies/AddNewCompanyCurrency`;
const deleteCompanyCurrencyUrl = `${baseUrl}/api/Companies/Companies/DeleteCompanyCurrency`;

const listModule = {
  state: {
    companiesList: [],
    searchFilterParams: {
      number: null,
      name: "",
      businessName: "",
      taxId: "",
    },
  },

  getters: {
    searchFilterParams: (state) => {
      return state.searchFilterParams;
    },
    getCompaniesList: (state) => {
      return state.companiesList;
    },
  },

  mutations: {
    [SET_COMPANIES_LIST](state, companiesList) {
      state.companiesList = companiesList;
    },
    [SET_SEARCH_FILTER_PARAMS](state, searchFilterParams) {
      state.searchFilterParams = searchFilterParams;
    },
  },

  actions: {
    async loadCompaniesList(ctx) {
      ctx.commit(LOADING, true);
      this.getters.getUiService.showSpinner(true);
      new ajax()
        .get(getCompaniesListURL, this.getters.searchFilterParams)
        .then((res) => {
          ctx.commit(SET_COMPANIES_LIST, res);
          this.getters.getUiService.onlyDestroyDataTablesManual(
            this.getters.getIdTable
          );
        })
        .then(() => {
          this.getters.getUiService.transformToDataTablesManual(
            this.getters.getIdTable
          );
        })
        .catch((ex) => {
          alert(ex);
        })
        .finally(() => {
          ctx.commit(LOADING, false);
          this.getters.getUiService.showSpinner(false);
        });
    },

    async deleteCompanyById(ctx, id) {
      let currentCompany = this.getters.getCompaniesList.find(
        (s) => s.id == id
      );
      if (currentCompany) {
        await this.dispatch("deleteCompany", currentCompany);
      } else {
        console.log("Error buscando la empresa");
      }
    },

    setSearchFilterParams(ctx, newSearchFilterParams) {
      ctx.commit(SET_SEARCH_FILTER_PARAMS, newSearchFilterParams);
    },
  },
};

const formModule = {
  state: {
    company: {
      id: null,
      number: null,
      name: "",
      businessName: "",
      taxId: "",
      companyCurrencies: [],
      rowVersion: null,

      newCompanyCurrency: { currencyIdm: 0, isSalesDefault: false, removed: false },
    },
  },

  getters: {
    getCompany: (state) => {
      return state.company;
    },
  },

  mutations: {
    [SET_COMPANY](state, newValue) {
      state.company.id = newValue.id;
      state.company.number = newValue.number;
      state.company.name = newValue.name;
      state.company.businessName = newValue.businessName;
      state.company.taxId = newValue.taxId;
      state.company.companyCurrencies = newValue.currencies;
      state.company.rowVersion = newValue.rowVersion;

      state.company.newCompanyCurrency.currencyIdm = 0;
      state.company.newCompanyCurrency.isSalesDefault = false;
      state.company.newCompanyCurrency.removed = false;
    },
    [RESET_COMPANY](state) {
      state.company.id = 0;
      state.company.number = null;
      state.company.name = "";
      state.company.businessName = "";
      state.company.taxId = "";
      state.company.companyCurrencies = [];
      state.company.rowVersion = null;

      state.company.newCompanyCurrency.currencyIdm = 0;
      state.company.newCompanyCurrency.isSalesDefault = false;
      state.company.newCompanyCurrency.removed = false;
    },
    [SET_COMPANY_CURRENCIES](state, newValue) {
      state.company.companyCurrencies = newValue;
    },
  },

  actions: {
    async deleteCompanyCurrency(ctx, cc) {
      ctx.commit(LOADING, true);
    
      this.getters.getErrorBag.clear();
      this.getters.getUiService.showSpinner(true);
      return new ajax()
        .delete(
            deleteCompanyCurrencyUrl,
          {
            Remove: cc,
            CompanyCurrencies: this.getters.getCompany.companyCurrencies,
          },
          { errorBag: this.getters.getErrorBag }
        )
        .then((res) => {
          if (this.getters.getErrorBag.hasErrors()) {
            this.getters.getUiService.showMessageErrorAndFocus(
              `Operación rechazada.`
            );
        } else {
            ctx.commit(SET_COMPANY_CURRENCIES, res);
            this.getters.getCompany.newCompanyCurrency.currencyIdm = 0;
            this.getters.getCompany.newCompanyCurrency.isSalesDefault = false;
            this.getters.getCompany.newCompanyCurrency.removed = false;
          }
        })
        .catch((ex) => {
          alert(ex);
        })
        .finally(() => {
          ctx.commit(LOADING, false);

          this.getters.getUiService.showSpinner(false);
        });
    },
    async addNewCurrency(ctx) {
      ctx.commit(LOADING, true);

      this.getters.getErrorBag.clear();
      this.getters.getUiService.showSpinner(true);
      new ajax()
        .post(
          addNewCurrencyURL,
          {
            CurrencyIdm: this.getters.getCompany.newCompanyCurrency.currencyIdm,
            isSalesDefault:
              this.getters.getCompany.newCompanyCurrency.isSalesDefault,
            CompanyCurrencies: this.getters.getCompany.companyCurrencies,
          },
          { errorBag: this.getters.getErrorBag }
        )
        .then((res) => {
          if (this.getters.getErrorBag.hasErrors()) {
            this.getters.getUiService.showMessageErrorAndFocus(
              `Operación rechazada.`
            );
        } else {
            ctx.commit(SET_COMPANY_CURRENCIES, res);
            this.getters.getCompany.newCompanyCurrency.currencyIdm = 0;
            this.getters.getCompany.newCompanyCurrency.isSalesDefault = false;
            this.getters.getCompany.newCompanyCurrency.removed = false;
          }
        })
        .catch((ex) => {
          alert(ex);
        })
        .finally(() => {
          ctx.commit(LOADING, false);

          this.getters.getUiService.showSpinner(false);
        });
    },
    async loadCompany(ctx, id) {
      ctx.commit(LOADING, true);
      await new ajax()
        .get(getCompanyURL, { Id: id })
        .then((res) => {
          ctx.commit(SET_COMPANY, res);
        })
        // .catch((ex) => {
        //     alert(ex)
        // })
        .finally(function () {
          ctx.commit(LOADING, false);
        });
    },

    async deleteCurrentCompany() {
      return await this.dispatch("deleteCompany", this.getters.getCompany);
    },

    async saveCompany(ctx) {
      ctx.commit(LOADING, true);
      let tempCompany = this.getters.getCompany;
      this.getters.getErrorBag.clear();
      this.getters.getUiService.showSpinner(true);
      if (tempCompany.id == 0) {
        return await new ajax()
          .post(
            postCompanyURL,
            {
              Number: tempCompany.number,
              Name: tempCompany.name,
              BusinessName: tempCompany.businessName,
              TaxId: tempCompany.taxId,
              CompanyCurrencies: tempCompany.companyCurrencies,
            },
            {
              errorBag: this.getters.getErrorBag,
            }
          )
          .finally(() => {
            ctx.commit(LOADING, false);
            this.getters.getUiService.showSpinner(false);
          });
      } else {
        return await new ajax()
          .put(
            putCompanyURL,
            {
              Id: tempCompany.id,
              Number: tempCompany.number,
              Name: tempCompany.name,
              BusinessName: tempCompany.businessName,
              TaxId: tempCompany.taxId,
              CompanyCurrencies: tempCompany.companyCurrencies,
            },
            {
              errorBag: this.getters.getErrorBag,
            }
          )
          .finally(() => {
            ctx.commit(LOADING, false);
            this.getters.getUiService.showSpinner(false);
          });
      }
    },
    setCompany(ctx, company) {
      ctx.commit(SET_COMPANY, company);
    },
    resetCompany(ctx) {
      ctx.commit(RESET_COMPANY);
      this.getters.getErrorBag.clear();
    },
  },
};

export default new Vuex.Store({
  state: {
    loading: false,
    mode: new CrudMode(),
    idTable: `__companies_table`,
    errorBag: new ErrorBag(),
    grants: { create: false, update: false, delete: false },
    securityService: new SecurityService(),
    uiService: new uiService(),
  },

  getters: {
    loading: (state) => {
      return state.loading;
    },
    getMode: (state) => {
      return state.mode;
    },
    getErrorBag: (state) => {
      return state.errorBag;
    },
    getGrants: (state) => {
      return state.grants;
    },
    getSecurityService: (state) => {
      return state.securityService;
    },
    getIdTable: (state) => {
      return state.idTable;
    },
    getUiService: (state) => {
      return state.uiService;
    },
  },

  mutations: {
    [LOADING](state, value) {
      state.loading = value;
    },
    [RESET_STATE](state) {
      state.textValue = "State Reseted";
    },
  },

  actions: {
    async loadGrants() {
      var grantsName = ["companies.create", "companies.update", "companies.delete"];

      await this.getters.getSecurityService
        .getGrantsByName(grantsName)
        .then((res) => {
          this.getters.getGrants.create = res["companies.create"];
          this.getters.getGrants.update = res["companies.update"];
          this.getters.getGrants.delete = res["companies.delete"];
        })
        .catch((ex) => {
          let msg = "Error recuperando permisos.";
          console.log(`${msg}: ${ex}.`);
          throw msg;
        });
    },
    resetState(ctx) {
      ctx.commit(RESET_STATE);
    },
    async deleteCompany(ctx, company) {
      ctx.commit(LOADING, true);
      this.getters.getUiService.showSpinner(true);
      this.getters.getErrorBag.clear();
      return await new ajax()
        .delete(
          deleteCompanyURL,
          {
            Id: company.id,
            RowVersion: company.rowVersion,
          },
          { errorBag: this.getters.getErrorBag }
        )
        .then(() => {
          if (this.getters.getErrorBag.hasGlobalErrors()) {
            throw "Error al eliminar";
          }
        })
        .catch((ex) => {
          throw "Error al eliminar";
        })
        .finally(() => {
          ctx.commit(LOADING, false);
          this.getters.getUiService.showSpinner(false);
        });
    },
  },

  modules: {
    listModule,
    formModule,
  },
});
