<template>
  <div>
    <div data-placement="bottom" data-toggle="tooltip" class="input-group mb-0">
      <input maxlength="15" :disabled="!enabled" type="search" @input="onChange" v-model="search" @keydown.down="onArrowDown" @keydown.up="onArrowUp" @keydown.enter="onEnter" @keydown.esc="clear" class="form-control" :class="error ? 'is-invalid' : ''"  :placeholder="placeholdertext" />
      <span class="input-group-text border-0">
        <i class="fas fa-search"></i>
      </span>
      <table :id="`table-${name}`" class=" tablita table table-light table-bordered table-hover shadow" v-show="isOpen">
        <thead class="table-top">
          <tr>
            <th v-for="(header,index) in headers" :key="`head-${name}-${index}`" class="text-center d-none text-primary d-sm-block d-md-table-cell">{{header}}</th>
          </tr>
        </thead>
        <tbody>
          <tr v-if="items.length === 0" class="text-center">
            <td class="text-center" :colspan="headers.length">No hay datos</td>
          </tr>
          <tr v-for="(result, i) in items" :key="`row-${name}-${i}`" @click="setResult(result)" class="" :class="i === arrowCounter ? 'table-active': ''">
            <td v-for="(prop,index) in displayproperties" :key="index" class="text-center">
              {{result[prop]}}
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
export default {
  name: "search-autocomplete",
  props: {
    list: {
      type: Array,
      required: false,
      default: () => [],
    },
    name: {
      type: String,
      required: false,
      default: () => "",
    },
    placeholdertext: {
      type: String,
      required: false,
      default: () => "",
    },
    displayinfo: {
      type: Boolean,
      required: false,
      default: () => false,
    },
    isAsync: {
      type: Boolean,
      required: false,
      default: false,
    },
    headers: {
      type: Array,
      required: false,
      default: () => [],
    },
    displayproperties: {
      type: Array,
      required: false,
      default: () => [],
    },
    value: {
      type: Number,
      required: false,
      default: () => null,
    },
    isDetail: {
      type: Boolean,
      required: false,
      default: () => false,
    },
    enabled: {
      type: Boolean,
      required: false,
      default: () => true,
    },
    get: {
      type: Function,
      required: false,
    },
    error: String
  },
  data() {
    return {
      isOpen: false,
      selectedItem: {},
      search: "",
      id: null,
      isLoading: false,
      arrowCounter: -1,
      selectedId: null,
      items: [],
    };
  },
  watch: {
    list: function (value, oldValue) {
      if (value.length !== oldValue.length) {
        this.items = value;
      }
    },
    search: function (value, old) {
      if (value === "") {
        this.clear();
      }
    },
  },
  async mounted() {
    console.log(this.value)
    if (this.value != null) {
      let result = await this.get();
      this.setResult(result[0]);
      this.isOpen = this.displayinfo;
      this.items.push(result[0]);
    }
    document.addEventListener("click", this.handleClickOutside);
  },
  destroyed() {
    document.removeEventListener("click", this.handleClickOutside);
  },
  methods: {
    setResult(result) {
      this.search = this.isDetail ? result.textInDetail : result.text;
      this.selectedItem = result;
      this.id = result.value;
      this.isOpen = false;
      this.emit();
    },
    emit() {
      this.$emit("selected", this.selectedItem);
      this.$emit("input", this.id);
    },
    filterResults() {
      // this.results = this.items.filter((item) => {
      //   return item.name.toLowerCase().indexOf(this.search.toLowerCase()) > -1;
      // });
    },
    onChange(val) {
      this.$emit("search", this.search);
      // this.$emit("input", val);

      // this.filterResults();
      this.isOpen = true;
    },
    handleClickOutside(event) {
      if (!this.$el.contains(event.target)) {
        this.isOpen = false;
        this.arrowCounter = -1;
      }
    },
    clear() {
      this.items = [];
      this.selectedItem = {};
      this.search = "";
      this.id = null;
      this.isOpen = false;
      this.emit();
    },
    onArrowDown() {
      if (this.arrowCounter < this.items.length - 1) {
        this.arrowCounter = this.arrowCounter + 1;
      } else {
        this.arrowCounter = 0;
      }
    },
    onArrowUp() {
      if (this.arrowCounter >= 1) {
        this.arrowCounter = this.arrowCounter - 1;
      } else {
        this.arrowCounter = this.items.length - 1;
      }
    },
    onEnter() {
      this.selectedItem = this.items[this.arrowCounter];
      this.search = this.items[this.arrowCounter].text;
      this.id = this.items[this.arrowCounter].value;
      this.isOpen = false;
      this.arrowCounter = -1;
      this.emit();
    },
  },
};
</script>

<style scoped>
.tablita {
  position: absolute !important;
  margin-top: 2rem;
  border-bottom-left-radius: 0.25rem;
  border-bottom-right-radius: 0.25rem;
}

input[type="search"] {
  margin: 0;
  padding: 7px 8px;
  font-size: 14px;
  color: inherit;
}

input.search {
  padding: 9px 4px 9px 40px;
  background: transparent
    url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='16' height='16' class='bi bi-search' viewBox='0 0 16 16'%3E%3Cpath d='M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001c.03.04.062.078.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1.007 1.007 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0z'%3E%3C/path%3E%3C/svg%3E")
    no-repeat 13px center;
}

input[disabled] {
  background-color: #e9ecef;
}
</style>