<template>
  <div>
    <div class="modal fade" data-bs-backdrop="static" :id="idModal" tabindex="-1" :ref="idModal" :aria-labelledby="idModal" aria-hidden="true">
      <div class="modal-dialog modal-dialog-scrollable grid-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Clientes</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <div class="row justify-content-md-center">
              <div class="form-group">
                <customers-lookup-filter ref="filter" @on-search="loadData()">
                </customers-lookup-filter>
                <br />
                <table class="table table-bordered table-striped table-hover" :id="`${idTable}`" convert-to-datatable-manual>
                  <thead class="table-top">
                    <tr>
                      <th scope="col">Nombre y apellido</th>
                      <th scope="col">Tipo y nro. de documento</th>
                      <th scope="col">Email</th>
                    </tr>
                  </thead>
                  <tbody>
                    <!-- <tr v-if="list.length === 0">
                      <td class="text-center" colspan="7">No se encontraron datos</td>
                    </tr> -->
                    <tr v-if="list.length === 0" class="no-data">
                      <td colspan="100" class="text-center">
                        No se encontraron datos
                      </td>
                    </tr>
                    <tr :key="index" v-for="(item, index) in list" @click="selectValue(item)" :class="item.selected ? 'table-primary' : ''">
                      <td>{{ item.firstName }} {{ item.lastName }}</td>
                      <td>{{ item.identificationType }} {{ item.idNumber }}</td>
                      <td>{{ item.email }}</td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-primary y mr-2 btn-sm" @click="informSelectedValue()">
              Confirmar
            </button>
            <a href="#" class="btn btn-link btn-sm" data-bs-dismiss="modal" @click="modalClosed">Cerrar</a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import CustomersLookupFilter from "./customers-lookup-filter.vue";
import ajax from "@/common/ajaxWrapper";
import UiService from "@/common/uiService";

const url = `${baseUrl}/api/Customers/Customer`;

export default {
  components: {
    CustomersLookupFilter,
  },
  name: "customer-lookup",
  props: {
    idModal: String,
    customerSelected: Object,
  },
  data: function () {
    return {
      list: [],
      uiService: new UiService(),
      idTable: `__customers_table_${this.idModal}`,
    };
  },
  mounted() {
    this.uiService.setModalEventsCallbacks(
      this.$refs[this.idModal],
      this.loadData,
      this.modalClosed
    );
  },
  methods: {
    modalClosed: function () {
      this.list = [];
      this.$refs.filter.parameters = {};
      this.uiService.eraseDataTablesManual(this.idTable);
    },
    async loadData() {
      let selectedId = null;

      if (this.customerSelected != null) {
        selectedId = this.customerSelected.id;
      }

      await new ajax()
        .get(url, this.$refs.filter.parameters)
        .then((res) => {
          var queryList = [];
          if (res.length) {
            queryList = res.map(function (r) {
              return new Customer(
                selectedId == r.id,
                r.id,
                r.title,
                r.firstName,
                r.lastName,
                r.addressCountryId,
                r.addressCityProvince,
                r.addressStreet,
                r.addressNumber,
                r.addressPostalCode,
                r.natCountryIdm,
                r.identificationTypeId,
                r.identificationType,
                r.idNumber,
                r.email,
                r.phone,
                r.mobilePhone,
                r.customerNumber,
                r.genderId,
                r.remarks
              );
            });
          }
          
          //Must destroy DataTables before updating the values, and reinitialize DataTable after the values have been updated.
          //Its better to call this function here than call it before the ajax call to minimize the time that DataTables is disabled.
          this.uiService.onlyDestroyDataTablesManual(this.idTable);
          
          this.list = Object.assign([], queryList);
        })
        .then(() => {
          this.uiService.transformToDataTablesManual(this.idTable);
        })
        .catch((ex) => {
          let msg = "Error recuperando la lista de clientes.";
          console.log(`${msg}: ${ex}.`);
          showError(msg);
        });
    },
    //Dont delete this method. Until travel-search-list use a store this is needed to work properly.
    getCustomerSelected() {
      var customer = null;
      this.list.forEach((e) => {
        if (e.selected) {
          customer = e;
        }
      });
      return customer;
    },
    selectValue(item) {
      item.selected = !item.selected;
      this.list.forEach((e) => {
        if (e != item) {
          e.selected = false;
        }
      });
    },
    informSelectedValue() {
      var customer = this.getCustomerSelected();

      if (!customer)
        customer = {
          id: null,
          title: "",
          firstName: "",
          lastName: "",
          addressCountryId: null,
          addressCityProvince: "",
          addressStreet: "",
          addressNumber: "",
          addressPostalCode: "",
          natCountryIdm: null,
          identificationTypeId: null,
          identificationType: "",
          idNumber: "",
          email: "",
          phone: "",
          mobilePhone: "",
          customerNumber: "",
          genderId: null,
          remarks: ""
        };

      this.$emit("on-customer-selected", customer);

      //Close modal
      this.modalClosed();
      $("#" + this.idModal).modal("hide");
    },
  },
};

class Customer {
  constructor(
    selected,
    id,
    title,
    firstName,
    lastName,
    addressCountryId,
    addressCityProvince,
    addressStreet,
    addressNumber,
    addressPostalCode,
    natCountryIdm,
    identificationTypeId,
    identificationType,
    idNumber,
    email,
    phone,
    mobilePhone,
    customerNumber,
    genderId,
    remarks
  ) {
    this.selected = selected;
      this.id = id,
     this.title = title,
     this.firstName = firstName,
     this.lastName = lastName,
     this.addressCountryId = addressCountryId,
     this.addressCityProvince = addressCityProvince,
     this.addressStreet = addressStreet,
     this.addressNumber = addressNumber,
     this.addressPostalCode = addressPostalCode,
     this.natCountryIdm = natCountryIdm,
     this.identificationTypeId = identificationTypeId,
     this.identificationType = identificationType,
     this.idNumber = idNumber,
     this.email = email,
     this.phone = phone,
     this.mobilePhone = mobilePhone,
     this.customerNumber = customerNumber,
     this.genderId = genderId,
     this.remarks = remarks
  }
}
</script>

<style lang="scss" scoped>
.grid-dialog {
  max-width: 900px;
  max-height: 700px;
}
</style>
