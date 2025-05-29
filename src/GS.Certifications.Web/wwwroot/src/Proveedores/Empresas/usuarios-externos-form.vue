<template>
    <div ref="top">
        <div class="col-12">
            <div class="card">
                <div class="card-body">
                    <div class="col-12 d-flex justify-content-between align-items-center mt-2 mb-2">
                        <p class="h5 m-0">{{loc["Listado de Usuarios Web"]}}</p> <!-- Modificar según el caso -->
                        <button :disabled="!grants.createUsuario" type="button" class="btn btn-outline-primary btn-sm" @click="addExterno">
                            <b><i class="fas fa-plus"></i>{{loc["Agregar"]}}</b>
                        </button>
                    </div>
                    <div class="col-12 table-responsive">
                        <table class="mt-4 table table-bordered table-striped table-hover table-white fix-head" 
                        convert-to-datatable-manual no-paging :id="`detalleExternosAsociados`">
                            <thead class="table-top">
                                <tr class="text-center">
                                    <th scope="col">{{loc["Email"]}}</th>
                                    <th scope="col">{{loc["Roles"]}}</th>
                                    <th scope="col">{{loc["Acciones"]}}</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-if="externosAsociados.length === 0" class="no-data text-center">
                                    <td class="text-center" colspan="7">{{loc["No hay Usuarios Web asociados a este proveedor"]}}</td>
                                </tr>
                                <tr v-for="(item, index) in externosAsociados" :key="index">
                                    <td class="text-center align-middle">{{ item.email }}</td>
                                    <td class="text-center align-middle">{{ generarRoles(item) }}</td>
                                    <td class="text-center align-middle">
                                        <div class="d-inline-flex">
                                            <inlineEdit :enabled="grants.updateUsuario" @click="update(item.id)" />
                                            <!-- Habilitar este botón si aplica -->
                                            <inlineDelete :enabled="grants.deleteUsuario" @click="remove(item)" />
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="col-12 d-flex justify-content-end mb-3 mt-3">
                <cancel-button @click="goBack">{{loc["Volver"]}}</cancel-button>
            </div>
        </div>
    </div>
</template>

<script>

import UiService from "@/common/uiService";

import UiService from "@/common/uiService";
import inlineEdit from "@/components/forms/inline-edit-button.vue";
import inlineDelete from "@/components/forms/inline-delete-button.vue";
import CancelButton from "@/components/forms/cancel-button.vue";

import ajax from "@/common/ajaxWrapper";

import loc from "@/common/commonLoc.js"

const API_URL = baseUrl + "/api/Proveedores/Administracion";

export default {
    name: "usuarios-externos-form",
    components: {
        inlineDelete,
        inlineEdit,
        CancelButton
    },
    data: function(){
        return {
            loc,
            
            uiService: new UiService(),
            esCreacion: false,
            externosAsociados: [],
            existeUsuario: true,
            usuariosExternosCollapseId: "collapseUsuariosCompras",

            listaRoles: [],
            dataListaRolesAgregadas: [],
            primerCargaDeRoles: false,
        }
    },
    computed: {
        grants() {
            return this.$store.getters.getGrants;
        },
        errorBag() {
            return this.$store.getters.getErrorBag;
        },
    },
    async mounted(){
        await this.getAsync();
    },
    methods: {
        addExterno(){
            this.errorBag.clear();
            this.$router.push({ name: "usuariosExternosCreate", params: { id: this.$route.params.id } });
        },
        goBack() {
            this.errorBag.clear();
            if(this.$route.params.create)
                this.$router.push({ name: "home", query: { fromDetail: false } });
            else
                this.$router.push({ name: "home", query: { fromDetail: true } });
        },
        generarRoles(item) {
            if (item.roles.length === 0) return "-";
            let roles = item.roles
                .map(r => r.descripcion);

            return roles.join(', ');
        },
        async getAsync() {            
            this.uiService.showSpinner(true);
            return await new ajax()
                .get(`${API_URL}/ObtenerUsuariosExternos`, {empresaPortalId: this.$route.params.id}, { errorBag: this.errorBag })
                .catch((ex) => {
                    console.log(`${ex.msg}: ${ex}.`);
                    showError(ex.msg);
                })
                .then((res) => {
                    var queryList
                    if (res.length > 0) {
                        queryList = res;
                        queryList = Object.assign([], queryList);
                        this.externosAsociados = queryList;
                    } else {
                        this.externosAsociados = [];
                    }
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        update(usuarioId){
            this.errorBag.clear();
            this.$store.dispatch("clearList");
            this.$router.push({ name: "usuariosExternosUpdate", params: { usuarioId: usuarioId } });
        },
        async remove(usuario){
            if (
                await this.uiService.confirmActionModal(
                "¿Está usted seguro que desea eliminar a este usuario web?",
                "Aceptar",
                "Cancelar"
                )
            ){
                this.uiService.showSpinner(true)
                await new ajax()
                    .delete(
                        `${API_URL}/EliminarUsuario/${usuario.id}`,
                        { rowVersion: usuario.rowVersion },
                        { errorBag: this.errorBag })
                    .then(async () => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            await this.getAsync();
                        } else {
                            this.uiService.showMessageError("Operación rechazada")
                        }
                    })
                    .finally(() => {
                        this.uiService.showSpinner(false);
                    });
            }
        }
    }
}

</script>