<template>
    <div>
        <router-view :key="$route.fullPath">
        </router-view>
    </div>
</template>
  
<script>
import SecurityService from "@/common/sercurityService";
import alertasList from "./alertas-list.vue";

export default {
    name: "alertas-crud",
    components: {
        alertasList
    },
    data: function () {
        return {
            grants: { update: undefined },
            securityService: new SecurityService()
        };
    },
    methods: {
        search: function () { },
        async loadGrants() {
            // if (this.grants.update == undefined) {
            //     var grantsName = ["ntfManagement.update"];

            //     await this.securityService
            //         .getGrantsByName(grantsName)
            //         .then((res) => {
            //             this.grants.update = res["ntfManagement.update"];
            //         })
            //         .catch((ex) => {
            //             let msg = "Error recuperando permisos.";
            //             console.log(`${msg}: ${ex}.`);
            //             throw msg;
            //         });
            // }
        }
    },
    async mounted() {
        await this.loadGrants();
    }
};
</script>
  