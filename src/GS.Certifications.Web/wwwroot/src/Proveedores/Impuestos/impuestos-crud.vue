<template>
    <div>
        <router-view :key="$route.fullPath">
        </router-view>
    </div>
</template>

<script>
import impuestosList from "./impuestos-list.vue";
export default {
    name: "impuestos-crud",
    components: {
        impuestosList
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