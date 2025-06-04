<template>
  <button type="button" :disabled="!enabled" :title="loc['Modificar']" class="btn btn-link text-body" @click="onClick">
    <i class="fas fa-edit"></i>
  </button>
</template>

<script>
import loc from '@/common/commonLoc'

export default {
  inheritAttrs: false,
  name: "inline-edit",
  props: {
    enabled: {
      type: Boolean,
      default: true
    }
  },
  methods: {
    onClick(e) {
      this.$emit("click");
    },
  },
  data: function () {
    return {
      loc
    }
  },
  created() {
  }
};
</script>

<style scoped>
button {
  transition: 0.1s;
}

button:hover {
  transform: scale(1.2);
  color: #0d6efd !important;
  transition: 0.1s;
}

button:active {
  transform: scale(0.9);
  box-shadow: 3px 2px 2px 1px rgba(0, 0, 0, 0.24);
  transition: 0.1s;
}
</style>