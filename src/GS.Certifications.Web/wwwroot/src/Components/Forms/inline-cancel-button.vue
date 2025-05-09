<template>
  <button
    type="button"
    :disabled="!enabled"
    title="Cancelar"
    class="btn btn-link text-body"
    v-on:click="onClick()"
  >
    <i class="fas fa-times"></i>
  </button>
</template>

<script>
export default {
  inheritAttrs: false,
  name: "inline-cancel",
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
  color: #dc3545 !important;
  transition: 0.1s;
}

button:active {
  transform: scale(0.9);
  box-shadow: 3px 2px 2px 1px rgba(0, 0, 0, 0.24);
  transition: 0.1s;
}
</style>