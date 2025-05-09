<template>
  <div>
    <lookup-comp  @on-item-selected="selected" v-on="$listeners" v-bind="$attrs" v-model="id" @input="onInput" :idModal="idModal" :name="lookupName" :titleHeader="lookupName" :displaytext="displayText" searchByCodeUrl="/api/VehicleTypes/GetVehicleTypes?code={code}" searchAllUrl="/api/VehicleTypes/GetVehicleTypes">
    </lookup-comp>
  </div>
</template>

<script>
import lookupComp from "@/Lookup/lookup-comp";

export default {
  components: { lookupComp },
  name: "vehicle-type-lookup",
  props: {
    value: Number,
    idModal: {
      type: String,
      default: "__VehicleTypeModal",
    },
    titleHeader: String,
    displayText: true,
  },
  data: function () {
    return {
      id: this.value,
      lookupName:
        this.titleHeader == undefined ? "Tipo de vehículo" : this.titleHeader,
    };
  },
  methods: {
    selected(item) {
      this.$emit("selected", item);
    },
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