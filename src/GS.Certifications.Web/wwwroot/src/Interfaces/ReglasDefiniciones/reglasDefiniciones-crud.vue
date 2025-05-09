<template>
    <div>
        <router-view :key="$route.fullPath">
        </router-view>
    </div>
</template>
  
<script>
import reglasdefinicionesList from "./reglasDefiniciones-list.vue";

export default {
    name: "reglasDefiniciones-crud",
    components: {
        reglasdefinicionesList
    },
    data: function () {
        return {
        };
    },
    methods: {
    },
    async mounted() {
    }
};
</script>
  