<template>
    <div>
        <router-view :key="$route.fullPath">
        </router-view>
    </div>
</template>
  
<script>
import definicionesList from "./definiciones-list.vue";

export default {
    name: "definiciones-crud",
    components: {
        definicionesList
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
  