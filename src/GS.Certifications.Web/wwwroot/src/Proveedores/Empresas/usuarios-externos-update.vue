<template>
    <div ref="top">
        <div class="col-12 mt-4">
            <div class="card">
                <div class="card-body">
                    <div class="col-12 mb-4">
                        <p class="h5 m-0">Modificación del Usuario {{ this.usuarioExternoDto.email }}</p> <!-- Modificar según el caso -->
                    </div>
                    <div class="row">
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Email</label><span class="text-danger">*</span>
                            <input disabled type="text" class="form-control" v-model="usuarioExternoDto.email">
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Roles</label><span class="text-danger">*</span>
                            <div>
                                <multiselect @open="loadRoles" v-model="listaRolesAgregados" :options="listaRoles"
                                track-by="idm" :custom-label="(item) => {return `${item.descripcion}`}" :hide-selected="true"
                                :close-on-select="true" open-direction="bottom" :multiple="true" placeholder="Seleccionar opción"
                                :show-labels="false">
                                <template slot="noResult"> No se encontraron resultados</template>
                                </multiselect>
                            </div>
                            <span class="text-danger field-validation-error" data-valmsg-for="Roles" data-valmsg-replace="true">
                                {{ errorBag.get("Roles") }}
                            </span>
                        </div> 
                    </div>
                    <div class="col-12 d-flex justify-content-end mb-3 mt-3">
                        <div>
                        <accept-button :disabled="!grants.updateUsuario" @click="save">Aceptar</accept-button>
                        <cancel-button @click="cancel">Cancelar</cancel-button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>

import UsuarioExternoDto from "./UsuarioExternoDto"
import UiService from "@/common/uiService";

import AcceptButton from "@/components/forms/accept-button.vue";

import CancelButton from "@/components/forms/cancel-button.vue";

import multiselect from "vue-multiselect";

import ajax from "@/common/ajaxWrapper";

const API_ROLES_URL = baseUrl + "/api/Proveedores/RolesTipos";
const API_URL = baseUrl + "/api/Proveedores/Administracion";

export default {
    name: "usuarios-Externos-update",
    components: {
        AcceptButton,
        CancelButton,
        multiselect
    },
    data: function(){
        return {
            usuarioExternoDto: new UsuarioExternoDto(),
            uiService: new UiService(),
            existeUsuario: true,

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
        listaRolesAgregados: {
            get() {
                return this.dataListaRolesAgregadas;
            },
            set(values) {
                // this.dataListaRolesAgregadas = values
                // this.usuarioExternoDto.roles = values.map(elemento => elemento.rolTipo);
                this.usuarioExternoDto.roles = values
                this.$emit("input", values);
            }
        },
    },
    async mounted(){
        await this.getAsync();
    },
    methods: {
        async getAsync(){
            var usuarioId = this.$route.params.usuarioId
            return await new ajax()
                .get(
                    `${API_URL}/ObtenerUsuario/${usuarioId}`,
                    { errorBag: this.errorBag, }
                )
                .then((res) => {
                    this.usuarioExternoDto = new UsuarioExternoDto(res);
                    this.dataListaRolesAgregadas = res.roles;
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        async save(){
            this.usuarioExternoDto.empresaPortalId = this.$route.params.id;
            await this.modificarUsuarioExterno(this.usuarioExternoDto);
        },
        async modificarUsuarioExterno(usuarioExternoDto) {
            this.errorBag.clear();
            if(this.validacionDatosObligatorios()){
                this.uiService.showSpinner(true)
                return await new ajax()
                    .put(
                        `${API_URL}/ModificarUsuario/${this.$route.params.usuarioId}`,
                        usuarioExternoDto,
                        {
                            errorBag: this.errorBag,
                        })
                    .then(() => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            this.cancel()
                        } else {
                            this.uiService.showMessageError("Operación rechazada")
                        }
                    })
                    .finally(() => {
                        this.uiService.showSpinner(false);
                    });
            }
        },
        cancel(){
            this.$router.push({ name: "detail", 
                                params: { id: this.$route.params.id },
                                query: { tab: "usuariosExternosForm" } });
        },
        async loadRoles() {
            if (this.primerCargaDeRoles) {
                return;
            }
            this.listaRoles = await this.getEmpresaRolesListAsync(this.$route.params.id);
            this.primerCargaDeRoles = true;
        },
        async getEmpresaRolesListAsync(id){
            var listaADevolver = []
            await new ajax()
                .get(`${API_ROLES_URL}/EmpresaRoles/${id}`, { errorBag: this.errorBag })
                .catch((ex) => {
                    console.log(`${ex.msg}: ${ex}.`);
                    showError(ex.msg);
                })
                .then((res) => {
                    var queryList
                    if (res.list.length > 0) {
                        queryList = res.list;
                        queryList = Object.assign([], queryList);
                        listaADevolver = queryList.map(function(empresaRol) {
                            return empresaRol.rolTipo;
                        });
                    }
                })
            return listaADevolver;
        },
        goBack() {
            if(this.$route.params.create)
                this.$router.push({ name: "home", query: { fromDetail: false } });
            else
                this.$router.push({ name: "home", query: { fromDetail: true } });
        },
        validacionDatosObligatorios(){
            var validacion = true;
            if(this.usuarioExternoDto.roles.length === 0){
                validacion = false;
                this.errorBag.addError("roles", ["Se debe agregar un rol como minimo"]);
            }
            return validacion
        }
    }
}
</script>