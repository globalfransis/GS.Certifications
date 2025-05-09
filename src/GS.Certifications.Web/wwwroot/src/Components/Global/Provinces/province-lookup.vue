<template>
  <div>
    <lookup-comp v-on="$listeners" v-bind="$attrs" v-model="id" @input="onInput" :idModal="idModal" :name="lookupName" :titleHeader="lookupName" :codeChars="9" :displaytext="displayText" :searchCode="code" :searchByCodeUrl="`/api/Provinces/lookupByCountryId?countryId={${code}}`" :searchAllUrl="url">
    </lookup-comp>
  </div>
</template>

<script>
import lookupComp from "@/Lookup/lookup-comp";

export default {
  components: { lookupComp },
  name: "province-lookup",
  props: {
    displayMode: String,
            value: '',
            idModal: {
                type: String,
                default: '__provinceModal'
            },
            titleHeader: String,
           displayText: true,
           code: Number
  },
  data: function () {
    return {
      id: this.value,
      lookupName:
        this.titleHeader == undefined ? "Provincias" : this.titleHeader,
    };
  },
  methods: {
    onInput: function (newValue) {
      if(newValue != this.id) {
        this.id = newValue;
      }
      this.$emit("input", this.id);
    },
  },
  computed: {
    url() {
      return `/api/Provinces/lookupByCountryId?countryId=${this.code}`;
    }
  }
};
</script>