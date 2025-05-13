<template>
    <div>
        <router-view :key="$route.fullPath">
        </router-view>
    </div>
</template>

<script>
import solicitudCertificacionList from "./solicitudCertificacion-list.vue";
export default {
    name: "solicitudCertificacion-crud",
    components: {
        solicitudCertificacionList
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