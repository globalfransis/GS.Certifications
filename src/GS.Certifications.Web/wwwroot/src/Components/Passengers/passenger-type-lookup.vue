<template>
  <div>
    <lookup-comp :error="error" v-on="$listeners" v-bind="$attrs" v-model="id" @input="onInput" :idModal="idModal" :name="lookupName" :titleHeader="lookupName" :displaytext="displayText" searchByCodeUrl="/api/PassengerType/GetPassengerTypes?code={code}" searchAllUrl="/api/PassengerType/GetPassengerTypes">
    </lookup-comp>
  </div>
</template>

<script>
import lookupComp from "@/Lookup/lookup-comp";

export default {
  components: { lookupComp },
  name: "passenger-type-lookup",
  props: {
    value: Number,
    idModal: {
      type: String,
      default: "__PassengerTypeModal",
    },
    titleHeader: String,
    displayText: true,
    error: { type: String },
  },
  data: function () {
    return {
      id: this.value,
      lookupName:
        this.titleHeader == undefined ? "Tipo de pasajero" : this.titleHeader,
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