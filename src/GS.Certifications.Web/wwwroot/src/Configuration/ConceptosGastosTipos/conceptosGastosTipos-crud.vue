<template>
    <div>
        <router-view :key="$route.fullPath">
        </router-view>
    </div>
</template>

<script>
import conceptosGastosTiposList from "./conceptosGastosTipos-list.vue";
export default {
    name: "conceptos-gastos-tipos-crud",
    components: {
        conceptosGastosTiposList
    },
    data: function () {
        return {
        };
    },
    methods: {
        async loadGrants() {
            await this.$store.dispatch('loadGrants');
        }
    },
    async mounted() {
        await this.loadGrants();
    }
};
</script>