<template>
  <button :disabled="!enabled" type="button" title="Eliminar" class="btn btn-link text-danger" @click="onClick">
    <i class="fas fa-trash"></i>
  </button>
</template>

<script>
export default {
  inheritAttrs: false,
  name: "inline-delete",
  props: {
    enabled: {
      type: Boolean,
      default: true,
    }
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
  transition: 0.2s;
}

button:active {
  transform: scale(0.9);
  box-shadow: 3px 2px 2px 1px rgba(0, 0, 0, 0.24);
  transition: 0.1s;
}
</style>