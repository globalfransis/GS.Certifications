<template>
  <div>
    <lookup-comp v-on="$listeners" v-bind="$attrs" v-model="id" @input="onInput" :idModal="idModal" :name="lookupName" :titleHeader="lookupName" :displaytext="displayText" :codeChars="3" searchByCodeUrl="/api/Countries/lookupByCode?code={code}" searchAllUrl="/api/Countries/lookUpAll">
    </lookup-comp>
  </div>
</template>

<script>
import lookupComp from "@/Lookup/lookup-comp";

export default {
  components: { lookupComp },
  name: "country-lookup",
  props: {
    displayMode: String,
    value: "",
    idModal: {
      type: String,
      default: "__countryModal",
    },
    titleHeader: String,
    displayText: true,
  },
  data: function () {
    return {
      id: this.value,
      lookupName: this.titleHeader == undefined ? "Países" : this.titleHeader,
    };
  },
  methods: {
    onInput (newValue) {
      this.id = newValue;
      // Its not neccesary, its binded by v-model, so the 'input' is already emited.
      // this.$emit("input", this.id);
    },
  },
  watch: {
    value: function (value) {
      this.id = value;
    },
  },
};
</script>