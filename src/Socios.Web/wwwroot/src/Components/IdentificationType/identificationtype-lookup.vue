<template>
  <div>
    <lookup-comp v-on="$listeners" v-bind="$attrs" v-model="id" @input="onInput" :idModal="idModal" :name="lookupName" :titleHeader="lookupName" :displaytext="displayText" @on-item-selected="selected" searchByCodeUrl="/api/IdentificationType/GetIdentificationTypesByOrganization?code={code}" searchAllUrl="/api/IdentificationType/GetIdentificationTypesByOrganization">
    </lookup-comp>
  </div>
</template>

<script>
import lookupComp from "@/Lookup/lookup-comp";

export default {
  components: { lookupComp },
  name: "identificationtype-lookup",
  props: {
    value: Number,
    idModal: {
      type: String,
      default: "__identificationtypeModal",
    },
    titleHeader: String,
    displayText: true,
  },
  data: function () {
    return {
      id: this.value,
      lookupName:
        this.titleHeader == undefined ? "Tipo de documento" : this.titleHeader,
    };
  },
  methods: {
    selected(item) {
      this.$emit("selected", item);
    },
    onInput: function (newValue) {
      this.id = newValue;
      // Its not neccesary, its binded by v-model, so the 'input' is already emited.
      // this.$emit("input", this.id);
    },
  },
  watch: {
    value: function (value) {
      this.id = value;
    }
  }
};
</script>