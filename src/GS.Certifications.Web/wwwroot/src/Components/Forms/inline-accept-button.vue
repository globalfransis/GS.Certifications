﻿<template>
  <button type="button" :disabled="!enabled" :title="loc['Aceptar']" class="btn btn-link text-body" v-on:click="onClick()">
    <i class="fas fa-check"></i>
  </button>
</template>

<script>
import loc from '@/common/commonLoc'

export default {
  inheritAttrs: false,
  name: "inline-accept",
  props: {
    enabled: {
      type: Boolean,
      default: true,
    },
  },
  data: function () {
    return {
      loc
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
  color: #0d6efd !important;
  transition: 0.1s;
}

button:active {
  transform: scale(0.9);
  box-shadow: 3px 2px 2px 1px rgba(0, 0, 0, 0.24);
  transition: 0.1s;
}
</style>