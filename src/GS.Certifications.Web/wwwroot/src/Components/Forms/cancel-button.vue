 <template>
    <!-- <a href="#" :disabled="disabled" class="btn btn-link btn-sm" v-on:click="onClick()"> -->
    <a :disabled="disabled" class="btn btn-outline-primary btn-sm" v-on:click="onClick()">
        <slot></slot>
    </a>
</template>

<script>
export default {
  inheritAttrs: false,
  name: "cancel-button",
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