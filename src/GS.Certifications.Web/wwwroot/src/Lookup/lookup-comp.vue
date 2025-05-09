<template>
  <div @keyup.prevent.f2="showSearch" :title="error">
    <input type="hidden" v-model="id" :name="name" @input="handleInput" />
    <div class="input-group">
      <input data-placement="bottom" data-toggle="tooltip" :title="text" type="text" class="form-control" v-uppercase v-bind:class="[codeClasses, { 'invalid': error }]" v-bind="$attrs" placeholder="" v-on:blur="searchByCode" v-bind:maxlength="codeChars" v-model="code" Rgth aria-label="Código" />

      <span v-if="displayText" class="form-control no-wrap " v-bind="$attrs" v-bind:class="{ 'is-invalid': error }" tabindex="-1">
        {{ text }}
      </span>

      <button class="input-group-text dropdown-toggle text-secondary" @click.prevent="null;" tabindex="-1" v-bind="$attrs" v-bind:class="{'invalid': error}" id="filtersButton" data-bs-toggle="modal" aria-expanded="false" :data-bs-target="`#${idModal}`">
        <i class="fas fa-search"></i>

      </button>

    </div>

    <span class="text-danger" v-if="displayMode==='full'">{{error}}</span>

    <lookup-search-comp :idModal="idModal" :titleHeader="titleHeader" :showUnspecified="showUnspecified" :searchAllUrl="searchAllUrl" @on-value-selected="updateSelected" :ref="`${modalName}`">
      <template #filterSlot>
        <slot name="lkpfilter" :filter="filter">
          <button class="btn btn-primary" @click="filter">Filtrar</button>
        </slot>
      </template>
      <slot>
        <!--Slot for modal footer-->
      </slot>
    </lookup-search-comp>
  </div>
</template>

<script>
import lookupSearchComp from "./lookup-search-comp.vue";
import "@/common/common-filters";

export default {
  components: { lookupSearchComp },
  inheritAttrs: false,
  name: "lookup-comp",
  props: {
    value: Number,
    name: String,
    idModal: String,
    getByIdUrl: String,
    displayMode: { default: "full" }, //inline
    searchAllUrl: String,
    searchByCodeUrl: String,
    error: { type: String },
    codeChars: {
      type: Number,
      default: 3,
    },
    showUnspecified: {
      type: Boolean,
      default: true,
    },
    displaytext: Boolean,
    tooltipText: String,
    titleHeader: String,
  },
  data: function () {
    return {
      id: this.value,
      code: "",
      text: "",
      allData: [],
      selectedItem: {},
      modalName: `${this.idModal}_modal`,
      displayText: true,
      emitUserChange: false,
    };
  },
  async mounted() {
    await this.setById();
    if (this.displaytext != undefined) {
      this.displayText = this.displaytext;
    }
  },
  computed: {
    filteredList() {
      return this.allData.filter((item) => {
        return item.text.toLowerCase().indexOf(this.code.toLowerCase()) > -1;
      });
    },
    codeClasses() {
      return [`code-${this.codeChars}`];
    },
  },
  methods: {
    searchByCode: function () {
      if (!this.code) {
        this.id = null;
        this.code = "";
        this.text = "";
        this.handleInput();
      } else {
        fetch(baseUrl + this.searchByCodeUrl.replace("{code}", this.code))
          .then((response) => response.json())
          .then((res) => {
            this.emitUserChange = true;

            if (!res || res.length === 0) {
              this.clearValues();
            } else {
              var item = res;
              if (Array.isArray(res) && res.length > 0) {
                item = res[0];
              }

              this.selectedItem = item;
              this.code = this.code.toUpperCase();
              this.text = item.text || item.name;
              this.id = item.id;
            }
            // this.handleInput();
          })
          .catch(() => {
            this.clearValues();
            // this.handleInput();
          });
      }
    },
    clearValues() {
      this.id = null;
      this.code = "";
      this.text = "";
      this.selectedItem = null;
      // this.handleInput();
    },
    handleInput(e) {
      // this.$emit("input", this.id);
      // this.$emit("on-item-selected", this.selectedItem);
    },
    // process: function () {
    //   alert("procesado");
    //   this.showModal = false;
    // },
    updateSelected(item) {
      this.emitUserChange = true;
      this.selectedItem = item.originalData;
      this.code = item.selectedCode;
      this.text = item.selectedText;
      this.id = item.selectedId;
      // this.handleInput();
    },
    showSearch(e) {
      var modalElement = document.getElementById(this.idModal);
      var modal = new bootstrap.Modal(modalElement);
      modal.show();
    },
    filterModal(searchString) {
      this.$refs[this.modalName].filterByText(searchString);
    },
    getAttrs(prefix) {
      return Object.keys(this.$attrs)
        .filter((key) => key.indexOf(prefix) === 0)
        .reduce(function (output, key) {
          output[key.replace(prefix, "")] = attrs[key];
          return output;
        }, {});
    },
    async loadAllData() {
      return fetch(baseUrl + this.searchAllUrl)
        .then((response) => response.json())
        .then((res) => {
          this.allData = res.map(function (r) {
            return {
              id: r.id,
              code: r.code,
              text: r.name || r.text,
              originalData: r,
            };
          });
        })
        .catch((ex) => {
          alert(ex);
        });
    },
    async setById() {
      var self = this;
      if (this.id > 0) {
        await this.loadAllData().then(function () {
          console.log(self)
          console.log(self.name)
          var selectedObject = self.allData.find((i) => i.id === self.id);
          self.code = selectedObject.code;
          self.text = selectedObject.text;
          self.selectedItem = selectedObject.originalData;
          // self.handleInput();
        });
      }
    },
  },
  watch: {
    id: async function (newVal, oldVal) {
      if (newVal !== oldVal && !(newVal === null && oldVal === undefined)) {
        this.id = newVal;
        await this.setById();
        this.$emit("input", this.id);
        this.$emit("on-item-selected", this.selectedItem);

        if (this.emitUserChange) {
          this.emitUserChange = false;
          this.$emit("user-change", this.id);
        }
      }
    },
    //This watch serves to bind the prop data.
    value: async function (newVal, oldVal) {
      if (newVal !== oldVal) {
        if (this.id !== newVal) {
          this.emitUserChange = false;
          this.id = newVal;
          await this.setById();
        }
      }
    },
    searchAllUrl:async function (newVal, oldVal) {
      if (newVal !== oldVal) {
        this.id = 0;
        await this.setById();
        this.code = "";
        this.text = "";
        this.selectedItem = {};
        if (this.displaytext != undefined) {
          this.displayText = this.displaytext;
        }
      }
    },
  },
};
</script>

<style scoped>
.open-search-button:hover {
  background: #dde;
}

.open-search-button:active {
  background: #ccd;
}

.code-1 {
  max-width: 5ch;
}

.code-2 {
  max-width: 6ch;
}

.code-3 {
  max-width: 7ch;
}

.code-4 {
  max-width: 8ch;
}

.code-5 {
  max-width: 9ch;
}

.code-6 {
  max-width: 11ch;
}

.code-7 {
  max-width: 13ch;
}

.code-8 {
  max-width: 15ch;
}

button[disabled] {
  background-color: #e9ecef;
  pointer-events: none;
}

span[disabled] {
  background-color: #e9ecef;
  pointer-events: none;
}

.no-wrap {
  white-space: nowrap;
  overflow: hidden;
}

input.invalid {
  border-left-color: #dc3545;
  border-top-color: #dc3545;
  border-bottom-color: #dc3545;
}

span.is-invalid {
  border-left-color: #ffcccc;
  border-right-color: #ffcccc !important;
}

button.invalid {
  border-right-color: #dc3545 !important;
  border-top-color: #dc3545;
  border-bottom-color: #dc3545;
  background-color: #ffe8e8;
}
</style>