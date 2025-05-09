<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">Edición de Tipo de Documento de Compra {{ this.tipoOrdenCompraDto.nombre }}</p> <!-- Por ejemplo: Alta de Usuario -->
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Nombre</label><span class="text-danger">*</span>
                            <input maxlength="100" type="text" class="form-control" v-model="tipoOrdenCompraDto.nombre">
                            <span class="text-danger field-validation-error" data-valmsg-for="Nombre" data-valmsg-replace="true">
                                {{ errorBag.get("Nombre") }}
                            </span>
                        </div>
                        <div class="form-group col-8 mb-4">
                            <label class="control-label">Descripción</label>
                            <input maxlength="255" type="text" class="form-control" v-model="tipoOrdenCompraDto.descripcion">
                            <span class="text-danger field-validation-error" data-valmsg-for="Descripcion" data-valmsg-replace="true">
                                {{ errorBag.get("Descripcion") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-1 col-sm-12 mb-4">
                            <label class="control-label">Requerida</label>
                            <div>
                                <input type="checkbox" class="form-check-input" v-model="tipoOrdenCompraDto.esRequerida">
                            </div>
                            <span class="text-danger field-validation-error" data-valmsg-for="EsRequerida" data-valmsg-replace="true">
                                {{ errorBag.get("EsRequerida") }}
                            </span>
                        </div>
                    </div>
                    <div class="col-12 d-flex justify-content-between align-items-center mt-2 mb-2">
                        <div>
                            <label class="h5 m-0">Características</label><span class="text-danger">*</span>
                        </div>
                    </div>
                    <span class="text-danger field-validation-error" data-valmsg-for="Caracteristicas" data-valmsg-replace="true">
                            {{ errorBag.get("Caracteristicas") }}
                    </span>
                    <div class="col-12 table-responsive">
                        <table class="mt-4 table table-bordered table-striped table-hover table-white fix-head" 
                        convert-to-datatable-manual no-paging :id="`detalleCaracteristica`">
                            <thead class="table-top">
                                <tr class="text-center">
                                    <th scope="col">Nombre</th>
                                    <th scope="col">Descripción</th>
                                    <th scope="col">Utilización</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(i, index) in listaCaracteristicas" :key="index">
                                    <td class="text-center align-middle">
                                        <label class="form-check-label d-flex justify-content-center" :for="'flexCheckDefault' + index">
                                            {{ i.nombre }}
                                        </label>
                                    </td>
                                    <td class="text-center align-middle">
                                        <label class="form-check-label d-flex justify-content-center" :for="'flexCheckDefault' + index">
                                            {{ i.descripcion }}
                                        </label>
                                    </td>
                                    <td class="text-center align-middle">
                                        <div class="d-flex justify-content-center">
                                            <input class="form-check-input" type="checkbox" v-model="i.valor"
                                            @input="addRemoveModo(i)" :id="'flexCheckDefault' + index">
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">Grupos</p> <!-- Por ejemplo: Alta de Usuario -->
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-12 d-flex justify-content-between align-items-center mt-4 mb-4">
                            <div class="col-lg-6 col-sm-12 d-flex align-items-center">
                                <label class="me-2 mb-0">Seleccionar Grupo:</label>
                                <select class="form-select" v-model="grupoSeleccionadoId">
                                    <option :value="null">Sin especificar</option>
                                    <option v-for="grupo in listadoGrupos" :key="grupo.id" :value="grupo.id">
                                        {{ grupo.name }}
                                    </option>
                                </select>
                            </div>
                            <button type="button" class="btn btn-outline-primary btn-sm" @click="addNuevoGrupo()">
                                <b><i class="fas fa-plus"></i>Agregar</b>
                            </button>
                        </div>
                    </div>
            
                    <div class="col-12 table-responsive">
                        <table class="mt-4 table table-bordered table-striped table-hover table-white fix-head" 
                        convert-to-datatable-manual no-paging :id="`detalleInternosAsociados`">
                            <thead class="table-top">
                                <tr class="text-center">
                                    <th scope="col">Nombre</th>
                                    <th scope="col">Descripcion</th>
                                    <th scope="col">Organizaciones</th>
                                    <th scope="col">Acciones</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-if="gruposAsociados.length === 0" class="no-data text-center">
                                    <td class="text-center" colspan="7">No hay Grupos Asociados</td>
                                </tr>
                                <tr v-for="(item, index) in gruposAsociados" :key="index">
                                    <td class="text-center align-middle">{{ item.name }}</td>
                                    <td class="text-center align-middle">{{ item.description }}</td>
                                    <td class="text-center align-middle">{{ organizaciones(item.organizations) }}</td>
                                    <td class="text-center align-middle">
                                        <div class="d-inline-flex">
                                            <inlineDelete :enabled="grants.deleteUsuario" @click="removeGrupo(item)" />
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12 d-flex justify-content-end mb-3 mt-3">
            <accept-button :disabled="!grants.update" @click="updateAsync" style="margin-right: 10px;">
                Aceptar
            </accept-button>
            <cancel-button @click="cancel">Cancelar</cancel-button>
        </div>
    </div>
</template>

<script>
import AcceptButton from "@/components/forms/accept-button.vue";
import CancelButton from "@/components/forms/cancel-button.vue";
import UiService from "@/common/uiService";
import TipoOrdenCompraEditDto from './TipoOrdenCompraEditDto';

import commonMixin from '@/Common/Mixins/commonMixin';

import ajax from '@/common/ajaxWrapper';

import inlineDelete from "@/components/forms/inline-delete-button.vue";

const API_GRUPOS_URL = baseUrl + "/api/Groups/ListadoGrupos";

export default {
    components: {
        AcceptButton,
        CancelButton,
        inlineDelete
    },
    mixins: [commonMixin],
    name: "tiposOrdenesCompras-edit",
    data: function () {
        return {
            tipoOrdenCompraDto: new TipoOrdenCompraEditDto(),
            uiService: new UiService(),

            listaCaracteristicas: [
                { idm: 1, nombre: "Activa", descripcion: 'La orden puede tener entregas parciales o sin monto tope.', valor: false },
                { idm: 2, nombre: "Recurrente", descripcion: 'El tipo se repite en forma periódica.', valor: false },
                { idm: 3, nombre: "Unica", descripcion: 'Es una orden puntual, de única vez.', valor: false }
            ],

            gruposAsociados: [],
            listadoGrupos: [],
            grupoSeleccionadoId: null
        };
    },
    computed: {
        grants() {
            return this.$store.getters.getGrants;
        },
        errorBag() {
            return this.$store.getters.getErrorBag;
        },
        formattedFecha: {
            get() {
                if(!this.tipoOrdenCompraDto.fecha) return;
                return this.tipoOrdenCompraDto.fecha.split('T')[0];
            },
            set(newValue) {
                this.tipoOrdenCompraDto.fecha = newValue + 'T00:00:00';
            }
        },
    },
    async mounted() {
        await this.obtenerGrupos();
        await this.init();
    },
    methods: {
        async init() {
            this.errorBag.clear();
            if (!this.grants.update) this.$router.push({ name: "home" });
            else {
                if (this.$route.params.id) await this.getAsync(this.$route.params.id);
                else this.goBack();
            }
        },
        async getAsync(id) {
            this.errorBag.clear();
            this.uiService.showSpinner(true);
            await this.$store.dispatch("getAsync", id)
                .then((res) => {
                    this.tipoOrdenCompraDto = new TipoOrdenCompraEditDto(res);
                    this.obtenerValoresCaracteristicas();
                    const idsGruposAsociados = this.tipoOrdenCompraDto.gruposId
                    this.gruposAsociados = this.listadoGrupos.filter(g => idsGruposAsociados.includes(g.id));
                    this.listadoGrupos = this.listadoGrupos.filter(g => !idsGruposAsociados.includes(g.id));
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        goDetail() {
            this.errorBag.clear();
            this.$router.push({ name: "detail", params: { id: this.tipoOrdenCompraDto.id } });
        },
        goBack() {
            this.errorBag.clear();
            this.$router.push({ name: "detail", params: { id: this.tipoOrdenCompraDto.id } });
        },
        cancel() {
            this.errorBag.clear();
            if(this.$route.query.desdeDetalle){
                this.goBack();
            } else {
                this.$router.push({ name: "home", query: { fromDetail: true } });
            }
        },
        async updateAsync() {
            this.errorBag.clear();
            if(this.validacionDeDatos()){
                this.asignarValoresCaracteristicas();
                this.tipoOrdenCompraDto.gruposId = this.gruposAsociados.map(g => g.id);
                this.uiService.showSpinner(true);
                await this.$store.dispatch("putAsync", this.tipoOrdenCompraDto)
                    .then(() => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada");
                            this.goDetail();
                        } else {
                            this.uiService.showMessageError("Operación rechazada");
                        }
                    })
                    .finally(() => {
                        this.uiService.showSpinner(false);
                    });
            }
        },
        validacionDeDatos(){
            var validacion = true;
            if(this.tipoOrdenCompraDto.nombre == null ||
             this.tipoOrdenCompraDto.nombre == '' ||
              this.tipoOrdenCompraDto.nombre == ""){
                validacion = false;
                this.errorBag.addError("Nombre", ["El nombre del tipo es obligatorio"]);
            }
                        
            const caracteristicasSeleccionadas = this.listaCaracteristicas.filter(c => c.valor);
            if (caracteristicasSeleccionadas.length !== 1) {
                validacion = false;
                this.errorBag.addError("Caracteristicas", ["Debe seleccionar una unica característica (Abierta, Recurrente o Única)"]);
            }
            return validacion;
        },
        addRemoveModo(caracteristicaSeleccionada) {
            this.listaCaracteristicas.forEach(caracteristica => {
                caracteristica.valor = caracteristica === caracteristicaSeleccionada;
            });
        },
        asignarValoresCaracteristicas() {
            this.tipoOrdenCompraDto.esAbierta = this.listaCaracteristicas.find(c => c.idm === 1).valor;
            this.tipoOrdenCompraDto.esRecurrente = this.listaCaracteristicas.find(c => c.idm === 2).valor;
            this.tipoOrdenCompraDto.esUnica = this.listaCaracteristicas.find(c => c.idm === 3).valor;
        },
        obtenerValoresCaracteristicas() {
            this.listaCaracteristicas.find(c => c.idm === 1).valor = this.tipoOrdenCompraDto.esAbierta;
            this.listaCaracteristicas.find(c => c.idm === 2).valor = this.tipoOrdenCompraDto.esRecurrente;
            this.listaCaracteristicas.find(c => c.idm === 3).valor = this.tipoOrdenCompraDto.esUnica;
        },
        async obtenerGrupos(){
            return await new ajax()
                .get(API_GRUPOS_URL, null)
                .then((res) => {
                    if(res !== undefined){
                        this.listadoGrupos = res
                        this.listadoGrupos.sort((a, b) => a.name.localeCompare(b.name));
                    }
                })
                .catch((ex) => {
                    throw new Error('Error');
                })
        },
        addNuevoGrupo() {
            const grupo = this.listadoGrupos.find(g => g.id === this.grupoSeleccionadoId);
            
            if (!grupo) {
                this.uiService.showMessageError("Debe seleccionar un grupo válido.");
                return;
            }

            // Validar que no esté ya agregado
            const yaAgregado = this.gruposAsociados.some(g => g.id === grupo.id);
            if (yaAgregado) {
                this.uiService.showMessageError("El grupo ya fue agregado.");
                return;
            }

            this.gruposAsociados.push(grupo);
            this.listadoGrupos = this.listadoGrupos.filter(g => g.id !== grupo.id);
            this.grupoSeleccionadoId = null; // Limpia el select
        },
        removeGrupo(grupo) {
            this.gruposAsociados = this.gruposAsociados.filter(g => g.id !== grupo.id);
            this.listadoGrupos.push(grupo);
            this.listadoGrupos.sort((a, b) => a.name.localeCompare(b.name));
        },
        organizaciones(lista) {
            if (!lista || lista.length === 0) return '';
            return lista.join(', ');
        }
    },
    watch: {
    }
};
</script>