<template>
    <div>
        <router-view :key="$route.fullPath">
        </router-view>
    </div>
</template>

<script>
import periodosList from "./periodos-list.vue";
export default {
    name: "periodos-crud",
    components: {
        periodosList
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