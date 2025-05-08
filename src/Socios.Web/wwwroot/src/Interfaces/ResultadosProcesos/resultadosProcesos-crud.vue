<template>
    <div>
        <router-view :key="$route.fullPath">
        </router-view>
    </div>
</template>
  
<script>
import resultadosprocesosList from "./resultadosProcesos-list.vue";

export default {
    name: "resultadosProcesos-crud",
    components: {
        resultadosprocesosList
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
  