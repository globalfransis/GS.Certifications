<template>
  <div>
    <router-view :key="$route.fullPath">
    </router-view>
  </div>
</template>

<script>
export default {
  name: "companies-crud",
  methods: {
    search: function () {},
  },
  mounted() {
    this.$store.dispatch('loadGrants');
  }
};
</script>
