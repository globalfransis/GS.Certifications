<template>
    <div ref="top">
        <div class="col-12">
            <div class="card">
                <div class="card-body">
                    <div class="col-12 d-flex justify-content-between align-items-center mt-2 mb-2">
                        <p class="h5 m-0">Listado de Usuarios Backoffice</p> <!-- Modificar según el caso -->
                        <button :disabled="!grants.createUsuario" type="button" class="btn btn-outline-primary btn-sm" @click="addInterno">
                            <b><i class="fas fa-plus"></i>Agregar</b>
                        </button>
                    </div>
                    <div class="col-12 table-responsive">
                        <table class="mt-4 table table-bordered table-striped table-hover table-white fix-head" 
                        convert-to-datatable-manual no-paging :id="`detalleInternosAsociados`">
                            <thead class="table-top">
                                <tr class="text-center">
                                    <th scope="col">Email</th>
                                    <th scope="col">Roles</th>
                                    <th scope="col">Acciones</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-if="internosAsociados.length === 0" class="no-data text-center">
                                    <td class="text-center" colspan="7">No hay Usuarios Backoffice asociados a este proveedor</td>
                                </tr>
                                <tr v-for="(item, index) in internosAsociados" :key="index">
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
                <cancel-button @click="goBack">Volver</cancel-button>
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

const API_URL = baseUrl + "/api/Proveedores/Administracion";

export default {
    name: "usuariosInternos-form",
    components: {
        inlineDelete,
        inlineEdit,
        CancelButton
    },
    data: function(){
        return {
            uiService: new UiService(),
            esCreacion: false,
            internosAsociados: [],
            existeUsuario: true,
            usuariosInternosCollapseId: "collapseUsuariosCompras",

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
        addInterno(){
            this.errorBag.clear();
            this.$router.push({ name: "usuariosInternosCreate", params: { id: this.$route.params.id } });
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
                .get(`${API_URL}/ObtenerUsuariosInternos`, {empresaPortalId: this.$route.params.id}, { errorBag: this.errorBag })
                .catch((ex) => {
                    console.log(`${ex.msg}: ${ex}.`);
                    showError(ex.msg);
                })
                .then((res) => {
                    var queryList
                    if (res.length > 0) {
                        queryList = res;
                        queryList = Object.assign([], queryList);
                        this.internosAsociados = queryList;
                    } else {
                        this.internosAsociados = [];
                    }
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        update(usuarioId){
            this.errorBag.clear();
            this.$store.dispatch("clearList");
            this.$router.push({ name: "usuariosInternosUpdate", params: { usuarioId: usuarioId } });
        },
        async remove(usuario){
            if (
                await this.uiService.confirmActionModal(
                "¿Está usted seguro que desea eliminar a este usuario backoffice?",
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