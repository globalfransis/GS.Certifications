<template>
  <div>
    <lookup-comp v-on="$listeners" :showUnespecified="showUnespecified" v-bind="$attrs" v-model="id" @input="onInput" :idModal="idModal" :name="lookupName" :titleHeader="lookupName" :displaytext="displayText" searchByCodeUrl="/api/Customers/Customergroup?code={code}" searchAllUrl="/api/Customers/Customergroup">
    </lookup-comp>
  </div>
</template>

<script>
import lookupComp from "@/Lookup/lookup-comp";

export default {
  components: { lookupComp },
  name: "customer-group-lookup",
  props: {
    value: Number,
    idModal: {
      type: String,
      default: "__customergroupModal",
    },
    showUnspecified: {
      type: Boolean,
      default: false,
    },
    titleHeader: String,
    displayText: true,
  },
  data: function () {
    return {
      id: this.value,
      lookupName:
        this.titleHeader == undefined ? "Grupos de clientes" : this.titleHeader,
    };
  },
  methods: {
    onInput: function (newValue) {
      this.id = newValue;
      this.$emit("input", this.id);
    },
  },
  watch: {
    value: function (value) {
      this.id = value;
    }
  }
};
</script>