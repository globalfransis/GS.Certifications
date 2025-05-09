<template>
    <div>
        <router-view :key="$route.fullPath">
        </router-view>
    </div>
</template>

<script>
import tiposOrdenesComprasList from "./tiposOrdenesCompras-list.vue";
export default {
    name: "tipos-ordenes-compras-crud",
    components: {
        tiposOrdenesComprasList
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