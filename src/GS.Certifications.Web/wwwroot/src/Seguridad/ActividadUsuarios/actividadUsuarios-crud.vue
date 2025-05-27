<template>
    <div>
        <router-view :key="$route.fullPath">
        </router-view>
    </div>
</template>

<script>
import actividadUsuariosList from "./actividadUsuarios-list.vue";
export default {
    name: "actividad-usuarios-crud",
    components: {
        actividadUsuariosList
    },
    data: function () {
        return {
        };
    },
    async mounted() {
    }
};
</script>