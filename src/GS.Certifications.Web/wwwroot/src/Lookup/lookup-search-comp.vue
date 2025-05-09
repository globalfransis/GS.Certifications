<template>
  <div>
    <div class="modal fade" :id="idModal" tabindex="-1" :ref="idModal" :Flabelledby="idModal" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header lookup-modal-header">
            <h5 class="modal-title">
              <slot name="header">
                <span class="form-label">{{titleHeader}}</span>
              </slot>
            </h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body lookup-modal-body">
            <div class="row justify-content-md-center">
              <table class="table mb-0">
                <thead class="table-top">
                  <tr>
                    <th class="hide" scope="col">Sel</th>
                    <th scope="col">Código</th>
                    <th scope="col">Nombre</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="item in filteredList" :key="item.code" @click="selectValue(item)" role="button" class="selectableRowToRight">
                    <th class="hide" scope="row">
                      <input type="radio" value="item.code" />
                    </th>
                    <td class="">{{ item.code }}</td>
                    <td>{{ item.text || item.name }}</td>
                  </tr>
                </tbody>
              </table>
              <slot>
                <!-- Body Slot -->
              </slot>
            </div>
          </div>
          <div class="modal-footer" v-if="hasFooterSlot">
            <slot name="footer">
              <!-- slot footer -->
            </slot>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import UiService from "@/common/uiService";

export default {
  name: "lookup-search-comp",
  props: {
    idModal: String,
    searchAllUrl: String,
    showUnspecified: Boolean,
    titleHeader: String,
  },
  data: function () {
    return {
      uiService: new UiService(),
      list: [],
      searchText: "",
      selectedId: "",
      selectedCode: "",
      selectedText: "",
      unspecifiedOption: { text: "Sin especificar", id: 0, code: "-" },
      idTable: "lookup-table-" + this.idModal,
    };
  },
  mounted() {
    //Suscribe to boostrap jquery event to inform vue that the modal window is closed.
    $(this.$refs[this.idModal]).on("hidden.bs.modal", this.modalClosed);
    this.loadData();
  },
  computed: {
    filteredList() {
      return this.list.filter(
        (item) =>
          (item.text || item.name + item.code)
            .toLowerCase()
            .indexOf(this.searchText.toLowerCase()) > -1
      );
    },
    //Identify if the specific slot is empty or not.
    hasFooterSlot() {
      return !!this.$slots["footer"];
    },
  },
  watch: {
    searchAllUrl: function (newVal, oldVal) {
      if (newVal !== oldVal) {
        this.loadData();
      }
    },
  },
  methods: {
    modalClosed: function () {
      if (this.selectedId !== "") {
        this.$emit("on-value-selected", this.$data);
      }
    },
    loadData() {
      fetch(baseUrl + this.searchAllUrl)
        .then((response) => response.json())
        .then((res) => {
          if (this.showUnspecified) {
            res = [this.unspecifiedOption].concat(res);
          }
          this.list = res;
        })
        .then((res) => {
          this.uiService.eraseDataTablesManual(this.idTable);
          this.uiService.transformToDataTablesManual(this.idTable);
        })
        .catch(() => {
          this.selectedId = "";
          this.selectedCode = "";
          this.selectedText = "";
        });
    },
    selectValue(item) {
      var modalElement = document.getElementById(this.idModal);
      var modal = bootstrap.Modal.getInstance(modalElement);
      modal.hide();
      this.selectedCode = item.code;
      this.selectedText = item.text || item.name;
      this.selectedId = item.id;
      this.searchText = "";
    },
  },
};
</script>

<style>
.lookup-modal-header {
  padding: 1rem 1rem 0.5rem 1rem !important;
}
/* .lookup-modal-body {
            padding: 0rem 1rem 1rem 1rem !important;
    } */
.dataTables_filter {
  float: right !important;
}

.hide {
  display: none;
}
</style>