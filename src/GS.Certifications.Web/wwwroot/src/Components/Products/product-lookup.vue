<template>
  <div>
    <lookup-comp :error="error" :displaytext="displayText" v-on="$listeners" v-bind="$attrs" v-model="id" @input="onInput" :idModal="idModal" :titleHeader="lookupName" :name="lookupName" searchByCodeUrl="/api/products/lookupByCode?code={code}" searchAllUrl="/api/products/lookupAll"></lookup-comp>
  </div>
</template>

<script>
import lookupComp from "@/Lookup/lookup-comp";

export default {
  components: { lookupComp },
  name: "product-lookup",
  props: {
    value: Number,
    idModal: {
      type: String,
      default: "__productModal",
    },
    titleHeader: String,
   displayText: true,
   error: { type: String },
  },
  data: function () {
    return {
      id: this.value,
      lookupName: this.titleHeader == undefined ? "Producto" : this.titleHeader,
    };
  },
  methods: {
    onInput: function (newValue) {
      this.id = newValue;
      this.$emit("input", this.id);
    },
  },
  watch: {
    value: function (newVal, oldVal) {
      if (newVal !== oldVal) {
        this.id = newVal;
      }
    },
  },
};
</script>