<template>
  <div>
    <lookup-comp v-on="$listeners" v-bind="$attrs" v-model="id" @input="onInput" :idModal="idModal" :name="lookupName" :titleHeader="lookupName" :displaytext="displayText" searchByCodeUrl="/api/productvariant/lookupByCode?code={code}" searchAllUrl="/api/productvariant/GetProductVariants">
    </lookup-comp>
  </div>
</template>

<script>
import lookupComp from "@/Lookup/lookup-comp";

export default {
  components: { lookupComp },
  name: "productvariant-lookup",
  props: {
    value: Number,
    idModal: {
      type: String,
      default: "__productvariantModal",
    },
    titleHeader: String,
   displayText: true,
  },
  data: function () {
    return {
      id: this.value,
      lookupName:
        this.titleHeader == undefined
          ? "Variación de producto"
          : this.titleHeader,
    };
  },
  methods: {
    onInput: function (newValue) {
      this.id = newValue;
      this.$emit("input", this.id);
    },
  },
};
</script>