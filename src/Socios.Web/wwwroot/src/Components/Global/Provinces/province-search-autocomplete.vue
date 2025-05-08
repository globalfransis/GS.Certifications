<template>
  <div>
    <!-- <label for="form-label">Provincia</label> -->
    <search-autocomplete :displayinfo="displayinfo" :get="loadSelected" :list="list" :enabled="enabled" @search="search = $event" :placeholdertext="placeholdertext" :name="autocompleteName" :displayproperties="displayproperties" :headers="headers" @selected="selectedItem = $event" v-model="selectedValue" />
  </div>
</template>

<script>
import SearchAutocomplete from "@/components/forms/search-autocomplete.vue";
const GETPROVINCES_URL = `${baseUrl}/api/Provinces/ProvincesAutocomplete`;

import ajax from "@/common/ajaxWrapper";

export default {
  name: "search-autocomplete-provinces",
  components: {
    SearchAutocomplete,
  },
  props: {
    value: { type: Number, required: false, default: () => null },
    columns: { type: Number, required: false, default: () => 1 },
    mincharacters: { type: Number, required: false, default: () => 1 },
    enabled: { type: Boolean, required: false, default: () => false },
    country: Number,
  },

  data: function () {
    return {
      selectedItem: null,
      displayproperties: ["text"],
      headers: ["Nombre"],
      list: [],
      parameters: { search: "", idm: null, countryIdm: this.country },
      search: "",
      displayinfo: false,
      placeholdertext: "Buscar provincias",
      autocompleteName: "province",
    };
  },
  computed: {
    selectedValue: {
      get() {
        return this.value;
      },
      set(value) {
        this.$emit("input", value);
        this.$emit("selected", this.selectedItem);
      },
    },
  },
  mounted() {},
  watch: {
    search: function (value) {
      this.onChange();
    },
  },
  methods: {
    onChange() {
      this.parameters.idm = null;
      if (this.search.length >= this.mincharacters) {
        this.parameters.search = this.search;
        this.parameters.countryIdm = this.country;
        this.loadList();
      }

      if (this.search.length <= this.mincharacters) {
        this.list = [];
      }
    },
    loadList() {
      new ajax()
        .get(GETPROVINCES_URL, this.parameters)
        .then((res) => {
          this.list = res;
        })
        .catch((ex) => {
          this.uiService.showMessageMainErrorAndLock(msg);
        });
    },
    async loadSelected() {
      this.parameters.idm = this.value;
      let response = [];
      await new ajax()
        .get(GETPROVINCES_URL, this.parameters)

        .then((res) => {
          response = res;
        })
        .catch((ex) => {
          this.uiService.showMessageMainErrorAndLock(msg);
        });
      return response;
    },
  },
};
</script>
