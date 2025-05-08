<template>
  <button :disabled="disabled" class="btn btn-primary y pr-2 btn-sm" type="button" v-on:click="onClick()">
    <slot></slot>
  </button>
</template>

<script>
export default {
  inheritAttrs: false,
  name: "accept-button",
  props: {
    disabled: false,
  },
  methods: {
    onClick(e) {
      this.$emit("click");
    },
  },
};
</script>