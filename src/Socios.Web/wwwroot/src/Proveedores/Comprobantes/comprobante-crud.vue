<template>
    <div>
        <router-view :key="$route.fullPath">
        </router-view>
    </div>
</template>

<script>
import comprobanteList from "./comprobante-list.vue";
export default {
    name: "comprobante-crud",
    components: {
        comprobanteList
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