 <template>
  <button :disabled="!enabled" class="btn btn-danger y mr-2 btn-sm" v-on:click="onClick()">
    <slot></slot>
  </button>
</template>

<script>
export default {
  inheritAttrs: false,
  name: "delete-button",
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