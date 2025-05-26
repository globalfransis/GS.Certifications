<template>
    <div>
        <router-view :key="$route.fullPath">
        </router-view>
    </div>
</template>

<script>
import requisitosFQSList from "./requisitosFQS-list.vue";
export default {
    name: "requisitos-crud",
    components: {
        requisitosFQSList
    },
    data: function () {
        return {
        };
    },
    // methods: {
    //     async loadGrants() {
    //         await this.$store.dispatch('loadGrants');
    //     }
    // },
    // async mounted() {
    //     await this.loadGrants();
    // }
};
</script>