<template>
  <button type="button" :disabled="!enabled" title="Bajar" class="btn btn-link" v-on:click="onClick()">
    <i class="fa fa-arrow-down"></i>
  </button>
</template>

<script>
export default {
  inheritAttrs: false,
  name: "inline-down",
  props: {
    enabled: {
      type: Boolean,
      default: true,
    },
  },
  methods: {
    onClick(e) {
      this.$emit("click");
    },
  },
};
</script>

<style scoped>
button {
  transition: 0.1s;
}

button:hover {
  transform: scale(1.2);
  color: #fd0d0d !important;
  transition: 0.1s;
}

button:active {
  transform: scale(0.9);
  box-shadow: 3px 2px 2px 1px rgba(0, 0, 0, 0.24);
  transition: 0.1s;
}
</style>