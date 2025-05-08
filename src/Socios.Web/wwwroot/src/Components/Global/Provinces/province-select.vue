<template>
  <select v-model="selected" class="form-control">
    <option :disabled="!showOption" :value="0"> {{loc['Sin especificar']}} </option>
    <option v-for="option in optionsData" :key="option.id" :value="option.id"> {{ option.code }} </option>
  </select>
</template>
<script>
import loc from "@/common/commonLoc";
export default {
  components: {},
  name: "identificationtype-select",
  props: {
    value: Number,
    options: Array,
    showoption: Boolean,
    id: Number,
  },
  data: function () {
    return {
      optionsData: this.options,
      showOption: null,
      loc,
    };
  },
  computed: {
    selected: {
      get() {
        return this.value;
      },
      set(value) {
        this.onChange(value);
      },
    },
    url() {
      return `/api/Provinces/lookupByCountryId?countryId=${this.id}`;
    },
  },
  methods: {
    onChange(value) {
      if (value === "") {
        value = null;
      } else {
        value = parseInt(value);
      }
      this.$emit("input", value);
    },
    searchProvinces() {
      if (this.options == undefined) {
        fetch(baseUrl + this.url)
          .then((response) => response.json())
          .then((res) => {
            this.optionsData = res;
          })
          .catch((ex) => {
            console.log("Error recuperando provincias: " + ex);
          });
      }
    },
  },
  watch: {
      id(newVal, oldVal) {
          if(newVal != oldVal) {
              this.searchProvinces();
          }
      }
  },
  mounted() {
    this.showOption = this.showoption;
    if (this.options == undefined) {
      fetch(baseUrl + this.url)
        .then((response) => response.json())
        .then((res) => {
          this.optionsData = res;
        })
        .catch((ex) => {
          console.log("Error recuperando provincias: " + ex);
        });
    }
  },
};
</script>