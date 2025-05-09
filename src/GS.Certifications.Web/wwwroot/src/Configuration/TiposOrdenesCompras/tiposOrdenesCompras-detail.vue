<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">Detalle del Tipo de Documento de Compra {{ this.tipoOrdenCompraDto.nombre }}</p> <!-- Agregar un título, por ejemplo: Detalle del Usuario {userId} -->
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row form-container">
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Nombre</label>
                            <input type="text" class="form-control" :value="tipoOrdenCompraDto.nombre" readonly>
                        </div>
                        <div class="form-group col-8 mb-4">
                            <label class="control-label">Descripción</label>
                            <input type="text" class="form-control" :value="tipoOrdenCompraDto.descripcion" readonly>
                        </div>
                        <div class="form-group col-lg-1 col-sm-12 mb-4">
                            <label class="control-label">Requerida</label>
                            <div>
                                <input type="checkbox" class="form-check-input" v-model="tipoOrdenCompraDto.esRequerida" readonly>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 d-flex justify-content-between align-items-center mt-2 mb-2">
                        <label class="h5 m-0">Características</label>
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
                                            @input="addRemoveModo(i)" :id="'flexCheckDefault' + index" disabled>
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
                    <div class="col-12 table-responsive">
                        <table class="mt-4 table table-bordered table-striped table-hover table-white fix-head" 
                        convert-to-datatable-manual no-paging :id="`detalleInternosAsociados`">
                            <thead class="table-top">
                                <tr class="text-center">
                                    <th scope="col">Nombre</th>
                                    <th scope="col">Descripcion</th>
                                    <th scope="col">Organizaciones</th>
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
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12 d-flex justify-content-end mb-3 mt-3">
            <button :disabled="!grants.update" class="btn btn-primary btn-sm" @click="update" style="margin-right: 10px;">
                Editar
            </button>
            <button :disabled="!grants.delete" class="btn btn-danger btn-sm" @click="remove" style="margin-right: 10px;">
                Eliminar
            </button>
            <cancel-button @click="goBack">Volver</cancel-button>
        </div>
    </div>
</template>

<script>
import CancelButton from "@/components/forms/cancel-button.vue";
import UiService from "@/common/uiService";

import TipoOrdenCompraDto from './TipoOrdenCompraDto' // Modificar por la clase dto correspondiente

import commonMixin from '@/Common/Mixins/commonMixin';
import detailMixin from '@/Common/Mixins/detailMixin';

import ajax from '@/common/ajaxWrapper';

const API_GRUPOS_URL = baseUrl + "/api/Groups/ListadoGrupos";

export default {
    components: {
        CancelButton
    },
    mixins: [commonMixin, detailMixin],
    name: "tiposOrdenesCompras-detail",

    data: function () {
        return {
            tipoOrdenCompraDto: new TipoOrdenCompraDto(), // Modificar por la clase dto correspondiente
            uiService: new UiService(),

            listaCaracteristicas: [
                { idm: 1, nombre: "Activa", descripcion: 'La orden puede tener entregas parciales o sin monto tope.', valor: false },
                { idm: 2, nombre: "Recurrente", descripcion: 'El tipo se repite en forma periódica.', valor: false },
                { idm: 3, nombre: "Unica", descripcion: 'Es una orden puntual, de única vez.', valor: false }
            ],
            gruposAsociados: [],
            listadoGrupos: [],
        };
    },
    computed: {
        grants() {
            return this.$store.getters.getGrants;
        },
        errorBag() {
            return this.$store.getters.getErrorBag;
        },
        mostrarValorImporte() {
            if(this.tipoOrdenCompraDto.importe != null)
                return (this.tipoOrdenCompraDto.importe).toFixed(2);
            return 'Sin valor'
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
            if (this.$route.params.id) {await this.getAsync(this.$route.params.id);}
            else this.goBack();
        },
        async getAsync(id) {
            this.uiService.showSpinner(true);
            await this.$store.dispatch("getAsync", id)
                .then((res) => {
                    this.tipoOrdenCompraDto = new TipoOrdenCompraDto(res);
                    this.asignarValoresCaracteristicas();
                    const idsGruposAsociados = this.tipoOrdenCompraDto.grupos.map(g => g.id);
                    this.gruposAsociados = this.listadoGrupos.filter(g => idsGruposAsociados.includes(g.id));
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        seeDetail() {
            // Limpiamos la lista antes de navegar
            this.$store.dispatch("clearList");
            this.$router.push({ name: "detail", params: { id: this.tipoOrdenCompraDto.id } });
        },
        async remove() {
            if (
                await this.uiService.confirmActionModal(
                "¿Está usted seguro que desea eliminar a este tipo de Documento de Compras?",
                "Aceptar",
                "Cancelar"
                )
            ){
                this.uiService.showSpinner(true)
                await this.$store.dispatch("deleteAsync", this.tipoOrdenCompraDto)
                    .then(async () => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            this.goBack();
                        } else {
                            if(this.errorBag.errors.hasOwnProperty('ordenCompra')){
                                this.uiService.showMessageError("Operacion rechazada: Existe un Documento de Compras con el tipo asignado")
                            } else { 
                                this.uiService.showMessageError("Operación rechazada") 
                            }
                        }
                    })
                    .finally(() => {
                        this.uiService.showSpinner(false);
                    });
            }
        },
        goBack() {
            if(this.$route.params.create)
                this.$router.push({ name: "home", query: { fromDetail: false } });
            else
                this.$router.push({ name: "home", query: { fromDetail: true } });
        },
        update() {
            this.$router.push({ name: "edit", query: {desdeDetalle: true}, params: { id: this.tipoOrdenCompraDto.id } });
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
        asignarValoresCaracteristicas() {
            this.listaCaracteristicas.find(c => c.idm === 1).valor = this.tipoOrdenCompraDto.esAbierta;
            this.listaCaracteristicas.find(c => c.idm === 2).valor = this.tipoOrdenCompraDto.esRecurrente;
            this.listaCaracteristicas.find(c => c.idm === 3).valor = this.tipoOrdenCompraDto.esUnica;
        },
        organizaciones(lista) {
            if (!lista || lista.length === 0) return '';
            return lista.join(', ');
        }
    },
};
</script>