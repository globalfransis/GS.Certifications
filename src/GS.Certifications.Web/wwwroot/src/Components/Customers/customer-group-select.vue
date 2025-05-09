<template>
  <select v-model="selected" class="form-select">
    <option :disabled="!showOption" :value="null"> {{loc['Sin especificar'] }} </option>
    <option v-for="(item, index) in list" :key="index" :value="item.id"> {{ item.code + " - " + item.name }} </option>
  </select>
</template>
<script>
import ajax from "@/common/ajaxWrapper";
import loc from "@/common/commonLoc";
export default {
  components: {},
  name: "customer-group-select",
  props: {
    value: Number,
  },
  data: function () {
    return {
      list: [],
      loc,
      showOption: true,
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
    loadData() {
      new ajax()
        .get(
          baseUrl +
            "/api/Customers/CustomerGroup"
        )
        .then((res) => {
          this.list = res;
        })
        .catch((ex) => {
          console.log(
            "Error recuperando grupos de clientes " +
              ex +
              JSON.stringify(this.params)
          );
        });
    },
  },
  mounted() {
    this.loadData();
  },
  watch: {},
};
</script>