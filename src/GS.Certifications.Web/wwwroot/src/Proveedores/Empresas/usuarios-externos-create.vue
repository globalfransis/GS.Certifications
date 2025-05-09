<template>
    <div ref="top">
        <div class="col-12 mt-4">
            <div class="card">
                <div class="card-body">
                    <div class="col-12 mb-4">
                        <p class="h5 m-0">Alta de Usuario Web</p> <!-- Modificar según el caso -->
                    </div>
                    <div class="row">
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Email</label><span class="text-danger">*</span>
                            <input type="text" class="form-control" v-model="usuarioExternoDto.email" @blur="getUsuarioCliente">
                            <span class="text-danger field-validation-error" data-valmsg-for="Email" data-valmsg-replace="true">
                                {{ errorBag.get("Email") }}
                            </span>
                            <span class="text-danger field-validation-error" data-valmsg-for="UsuarioExterno" data-valmsg-replace="true">
                                {{ errorBag.get("UsuarioExterno") }}
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
                        <div class="col-12 mb-4 collapse" :id="usuariosExternosCollapseId">
                            <div class="row">
                                <div class="form-group col-lg-3 col-sm-12 mb-4">
                                    <label class="control-label">Nombre</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" v-model="usuarioExternoDto.firstName" :disabled="!isEditable">
                                    <span class="text-danger field-validation-error" data-valmsg-for="Nombre" data-valmsg-replace="true">
                                        {{ errorBag.get("Nombre") }}
                                    </span>
                                </div>
                                <div class="form-group col-lg-3 col-sm-12 mb-4">
                                    <label class="control-label">Apellido</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" v-model="usuarioExternoDto.lastName" :disabled="!isEditable">
                                    <span class="text-danger field-validation-error" data-valmsg-for="Apellido" data-valmsg-replace="true">
                                        {{ errorBag.get("Apellido") }}
                                    </span>
                                </div>
                                <div class="form-group col-lg-3 col-sm-12 mb-4">
                                    <label class="control-label">Documento</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" v-model="usuarioExternoDto.idNumber" :disabled="!isEditable">
                                    <span class="text-danger field-validation-error" data-valmsg-for="Documento" data-valmsg-replace="true">
                                        {{ errorBag.get("Documento") }}
                                    </span>
                                </div>
                                <div class="form-group col-lg-3 col-sm-12 mb-4">
                                    <label class="control-label">Telefono</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" v-model="usuarioExternoDto.phoneNumber" :disabled="!isEditable">
                                    <span class="text-danger field-validation-error" data-valmsg-for="Telefono" data-valmsg-replace="true">
                                        {{ errorBag.get("Telefono") }}
                                    </span>
                                </div>
                            </div>
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

import UsuarioExternoDto from "./UsuarioExternoDto"
import UiService from "@/common/uiService";

import AcceptButton from "@/components/forms/accept-button.vue";

import CancelButton from "@/components/forms/cancel-button.vue";

import multiselect from "vue-multiselect";

import ajax from "@/common/ajaxWrapper";

const API_ROLES_URL = baseUrl + "/api/Proveedores/RolesTipos";
const API_URL = baseUrl + "/api/Proveedores/Administracion";
const API_USUARIOS_EXTERNOS_URL = baseUrl + "/api/Proveedores/Users/Externo"

export default {
    name: "usuarios-externos-create",
    components: {
        AcceptButton,
        CancelButton,
        multiselect
    },
    data: function(){
        return {
            usuarioExternoDto: new UsuarioExternoDto(),
            uiService: new UiService(),
            existeUsuario: false,
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
        isEditable() {
            return this.usuarioExternoDto.email && !this.existeUsuario;
        },
        listaRolesAgregados: {
            get() {
                return this.dataListaRolesAgregadas;
            },
            set(values) {
                this.dataListaRolesAgregadas = values
                this.usuarioExternoDto.roles = values.map(elemento => elemento.rolTipo);
                this.$emit("input", values);
            }
        },
    },
    mounted(){
        //await this.init();
    },
    methods: {
        async save(){
            this.usuarioExternoDto.empresaPortalId = this.$route.params.id;
            await this.añadirUsuarioExterno(this.usuarioExternoDto);
        },
        async añadirUsuarioExterno(usuarioExternoDto) {
            this.errorBag.clear();
            if(this.validacionDatosObligatorios()){
                this.uiService.showSpinner(true)
                return await new ajax()
                    .post(
                        `${API_URL}/AñadirUsuarioExterno`,
                        usuarioExternoDto,
                        {
                            errorBag: this.errorBag,
                        })
                    .then((id) => {
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
                                query: { tab: "usuariosExternosForm" } });
        },
        async getUsuarioCliente() {
            this.errorBag.clear();
            if(this.usuarioExternoDto.email == '' || this.usuarioExternoDto.email == "" || this.usuarioExternoDto.email == null){
                this.mostrarCollapse(this.usuariosExternosCollapseId, false);
                this.errorBag.addError("email", ["El campo 'Email' es obligatorio"]);
                return
            }
            var usuarioExternoExiste = await this.getUsuarioExterno(this.usuarioExternoDto.email);
            if (usuarioExternoExiste) {      
                this.mostrarCollapse(this.usuariosExternosCollapseId, false);
                this.usuarioExternoDto.login = null
                this.usuarioExternoDto.firstName = null
                this.usuarioExternoDto.lastName = null
                this.usuarioExternoDto.idNumber = null
                this.usuarioExternoDto.phoneNumber = null
                this.existeUsuario = true;
            } else {
                this.mostrarCollapse(this.usuariosExternosCollapseId, true);
                this.usuarioExternoDto.login = this.usuarioExternoDto.email ? this.usuarioExternoDto.email.toString().replace("@", "#") : null
                this.usuarioExternoDto.firstName = null
                this.usuarioExternoDto.lastName = null
                this.usuarioExternoDto.idNumber = null
                this.usuarioExternoDto.phoneNumber = null
                this.existeUsuario = false;
            }
        },
        async getUsuarioExterno(email) {
            var resultado = null;
            await new ajax()
                .get(`${API_USUARIOS_EXTERNOS_URL}/${email}/Exists`)
                .then((res) => {
                    // Agregar alguna lógica de ser necesario
                    resultado = res;
                })
            return resultado;
        },
        mostrarCollapse(idCollapse, estado){
            let element = document.querySelector(`#${idCollapse}`);
            let bsCollapse = bootstrap.Collapse.getInstance(element);
            // if the instance is not yet initialized then create new collapse
            if (bsCollapse === null) {
                bsCollapse = new bootstrap.Collapse(element, {
                    toggle: false   // this parameter is important!
                })
            }
            if (estado){
                bsCollapse.show();
            }
            else {
                bsCollapse.hide();
            }
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
            if(this.usuarioExternoDto.roles.length === 0){
                validacion = false;
                this.errorBag.addError("roles", ["Se debe agregar un rol como minimo"]);
            }
            if(!this.existeUsuario){
                if(this.usuarioExternoDto.firstName == '' || this.usuarioExternoDto.firstName == "" || this.usuarioExternoDto.firstName == null){
                    validacion = false;
                    this.errorBag.addError("nombre", ["El campo 'Nombre' es obligatorio"]);
                }
                if(this.usuarioExternoDto.lastName == '' || this.usuarioExternoDto.lastName == "" || this.usuarioExternoDto.lastName == null){
                    validacion = false;
                    this.errorBag.addError("apellido", ["El campo 'Apellido' es obligatorio"]);
                }
                if(this.usuarioExternoDto.idNumber == '' || this.usuarioExternoDto.idNumber == "" || this.usuarioExternoDto.idNumber == null){
                    validacion = false;
                    this.errorBag.addError("documento", ["El campo 'Documento' es obligatorio"]);
                }
                if(this.usuarioExternoDto.phoneNumber == '' || this.usuarioExternoDto.phoneNumber == "" || this.usuarioExternoDto.phoneNumber == null){
                    validacion = false;
                    this.errorBag.addError("telefono", ["El campo 'Telefono' es obligatorio"]);
                }
                if(this.usuarioExternoDto.email == '' || this.usuarioExternoDto.email == "" || this.usuarioExternoDto.email == null){
                    validacion = false;
                    this.errorBag.addError("email", ["El campo 'Email' es obligatorio"]);
                } else {
                    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                    if(!regex.test(this.usuarioExternoDto.email) && 
                    (this.usuarioExternoDto.email != '' || this.usuarioExternoDto.email != "" || this.usuarioExternoDto.email != null)){
                        validacion = false
                        this.errorBag.addError("email", ["El formato de Email es incorrecto"]);
                    }
                }
            }
            return validacion;
        }
    }
}
</script>