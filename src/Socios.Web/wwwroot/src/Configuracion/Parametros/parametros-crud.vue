<template>
        <div>
          <router-view :key="$route.fullPath">
          </router-view>
        </div>
      </template>
      
      <script>
      export default {
        name: "parametros-crud",
        methods: {
          search: function () {},
        },
        async mounted() {
          // await this.$store.dispatch('loadGrants');
        }
      };
      </script>