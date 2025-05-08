<template>
  <select v-model="selected" class="form-select">
    <option :value="null" v-if="showOption">{{loc['Sin especificar']}}</option>
    <option :value="true">{{loc['Sí']}}</option>
    <option :value="false">{{loc['No']}}</option>
  </select>
</template>
<script>
  import loc from "@/common/commonLoc";
  export default {
    components: {},
    name: "boolean-select",
    props: {
      value: {},
      showOption: {
        type: Boolean,
        default: function () { return true }
      }
    },
    data: function () {
      return {
        loc
      };
    },
    computed: {
      selected: {
        get() { return this.value; },
        set(value) { this.onChange(value); },
      },
    },
    methods: {
      onChange(value) {
        if (value != undefined) {
          this.$emit("input", value);
        }
      },
    },
    mounted() {},
  };
</script>