<template>
    <div ref="top">
        <div class="col-12 mt-4">
            <div class="card">
                <div class="card-body">
                    <div class="col-12 mb-4">
                        <p class="h5 m-0">Alta de Usuario Backoffice</p> <!-- Modificar según el caso -->
                    </div>
                    <div class="row">
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Email</label><span class="text-danger">*</span>
                            <input type="text" class="form-control" v-model="usuarioInternoDto.email" @blur="getUsuarioCliente">
                            <span class="text-danger field-validation-error" data-valmsg-for="Email" data-valmsg-replace="true">
                                {{ errorBag.get("Email") }}
                            </span>
                            <span class="text-danger field-validation-error" data-valmsg-for="UsuarioInterno" data-valmsg-replace="true">
                                {{ errorBag.get("UsuarioInterno") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Roles</label><span class="text-danger">*</span>
                            <div>
                                <multiselect @open="loadRoles" v-model="listaRolesAgregados" :options="listaRoles"
                                track-by="rolTipoId" :custom-label="(item) => {return `${item.rolTipo.descripcion}`}" :hide-selected="true"
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
                        <accept-button :disabled="!grants.createUsuario" @click="save">Aceptar</accept-button>
                        <cancel-button @click="cancel">Cancelar</cancel-button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>

import UsuarioInternoDto from "./UsuarioInternoDto"
import UiService from "@/common/uiService";

import AcceptButton from "@/components/forms/accept-button.vue";

import CancelButton from "@/components/forms/cancel-button.vue";

import multiselect from "vue-multiselect";

import ajax from "@/common/ajaxWrapper";

const API_ROLES_URL = baseUrl + "/api/Proveedores/RolesTipos";
const API_URL = baseUrl + "/api/Proveedores/Administracion";
const API_USUARIOS_INTERNOS_URL = baseUrl + "/api/Proveedores/Users/Interno"

export default {
    name: "usuarios-internos-create",
    components: {
        AcceptButton,
        CancelButton,
        multiselect
    },
    data: function(){
        return {
            usuarioInternoDto: new UsuarioInternoDto(),
            uiService: new UiService(),
            existeUsuario: false,
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
        isEditable() {
            return this.usuarioInternoDto.email && !this.existeUsuario;
        },
        listaRolesAgregados: {
            get() {
                return this.dataListaRolesAgregadas;
            },
            set(values) {
                this.dataListaRolesAgregadas = values
                this.usuarioInternoDto.roles = values.map(elemento => elemento.rolTipo);
                this.$emit("input", values);
            }
        },
    },
    mounted(){
        //await this.init();
    },
    methods: {
        async save(){
            this.usuarioInternoDto.empresaPortalId = this.$route.params.id;
            await this.añadirUsuarioInterno(this.usuarioInternoDto);
        },
        async añadirUsuarioInterno(usuarioInternoDto) {
            this.errorBag.clear();
            if(this.validacionDatosObligatorios()){
                this.uiService.showSpinner(true)
                return await new ajax()
                    .post(
                        `${API_URL}/AñadirUsuarioInterno`,
                        usuarioInternoDto,
                        {
                            errorBag: this.errorBag,
                        })
                    .then(() => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            this.cancel();
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
            this.errorBag.clear();
            this.$router.push({ name: "detail", 
                                params: { id: this.$route.params.id },
                                query: { tab: "usuariosInternosForm" } });
        },
        async getUsuarioCliente() {
            if(this.usuarioInternoDto.email == null){
                this.errorBag.addError("email", ["El campo 'Email' es obligatorio"]);
                return
            }
            var usuarioInternoExiste = await this.getUsuarioInterno(this.usuarioInternoDto.email);
            if (!usuarioInternoExiste) { 
                this.existeUsuario = false;
            } else {
                this.existeUsuario = true;
            }
        },
        async getUsuarioInterno(email) {
            var resultado = null;
            await new ajax()
                .get(`${API_USUARIOS_INTERNOS_URL}/${email}/Exists`)
                .then((res) => {
                    // Agregar alguna lógica de ser necesario
                    resultado = res;
                })
            return resultado;
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
                        listaADevolver = queryList;
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
            if(this.usuarioInternoDto.roles.length === 0){
                validacion = false;
                this.errorBag.addError("roles", ["Se debe agregar un rol como minimo"]);
            }
            if(this.usuarioInternoDto.email == '' || this.usuarioInternoDto.email == "" || this.usuarioInternoDto.email == null){
                validacion = false;
                this.errorBag.addError("email", ["El campo 'Email' es obligatorio"]);
            } else if(!this.existeUsuario){
                validacion = false;
                this.errorBag.addError("email", ["No existe un Usuario Backoffice con email " + this.usuarioInternoDto.email]);
            }
            return validacion;
        }
    }
}
</script>