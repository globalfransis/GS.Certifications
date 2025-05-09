<template>
  <select v-model="selected" :disabled="readonly" class="form-select" @change="onChange($event.target.value)">
    <!-- {{ this.showOption ? loc['Sin especificar'] : loc['Seleccionar'] }}  -->
    <option :readonly="readonly" :value="null"> {{loc['Sin especificar'] }} </option>
    <option :readonly="readonly" v-for="option in list" :key="option.id" :value="option.id"> {{ option.name }} </option>
  </select>
</template>
<script>
import ajax from "@/common/ajaxWrapper";
import loc from "@/common/commonLoc";
export default {
  components: {},
  name: "gender-select",
  props: {
    value: Number,
    readonly: Boolean
  },
  data: function () {
    return {
      list: [],
      loc,
    };
  },
  computed: {
    selected() {
      return this.value == 0 ? 0 : this.value;
    },
  },
  methods: {
    onChange(newValue) {
      if (newValue === "") {
        newValue = null;
      } else {
        newValue = parseInt(newValue);
      }
      this.$emit("input", newValue);
    },
  },
 mounted() {
    new ajax()
      .get(baseUrl + "/api/Global/Genders/GetGenders")
      .then((res) => {
        this.list = res;
      })
      .catch((ex) => {

      });
  },
  watch: {
    value: function (value) {
      this.id = value;
    }
  }
};
</script>