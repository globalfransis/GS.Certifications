<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">Detalle del Documento de Compras</p> <!-- Agregar un título, por ejemplo: Detalle del Usuario {userId} -->
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row form-container">
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Numero de Orden</label>
                            <input type="text" class="form-control" :value="ordenCompraDto.numeroOrden" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Fecha</label>
                            <input type="date" class="form-control" v-model="formattedFecha" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Proveedor</label>
                            <input type="text" class="form-control" :value="ordenCompraDto.empresaPortal ? ordenCompraDto.empresaPortal.razonSocial : 'Sin Empresa Portal'" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Tipo</label>
                            <input type="text" class="form-control" :value="ordenCompraDto.ordenCompraTipo ? ordenCompraDto.ordenCompraTipo.nombre : 'Sin Tipo'" readonly>
                        </div>
                        <!-- <div class="form-group col-lg-4 col-sm-12 mb-4">
                            <label class="control-label">Codigo HES</label>
                            <input type="text" class="form-control" :value="ordenCompraDto.codigoHES" readonly>
                        </div> -->
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Código Sistema Externo</label>
                            <input type="text" class="form-control" :value="ordenCompraDto.codigoQAD" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Estado</label>
                            <input type="text" class="form-control" :value="ordenCompraDto.ordenCompraEstado ? ordenCompraDto.ordenCompraEstado.nombre : 'Sin Estado'" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Importe</label>
                            <input type="text" class="form-control" :value="mostrarValorImporte" readonly>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Moneda</label>
                            <input type="text" class="form-control" :value="ordenCompraDto.moneda ? ordenCompraDto.moneda.name : 'Sin Moneda'" readonly>
                        </div>
                        <div class="form-group col-12 mb-4">
                            <label class="control-label">Observaciones</label>
                            <textarea type="text" class="form-control" :value="ordenCompraDto.observaciones" readonly/>
                        </div>
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

import OrdenCompraDto from './OrdenCompraDto' // Modificar por la clase dto correspondiente

import commonMixin from '@/Common/Mixins/commonMixin';
import detailMixin from '@/Common/Mixins/detailMixin';

export default {
    components: {
        CancelButton
    },
    mixins: [commonMixin, detailMixin],
    name: "ordenesCompras-detail",

    data: function () {
        return {
            ordenCompraDto: new OrdenCompraDto(), // Modificar por la clase dto correspondiente
            uiService: new UiService(),
            valorAlicuota: null
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
            if(this.ordenCompraDto.importe != null)
                return this.formatCurrency(this.ordenCompraDto.importe);
            return 'Sin valor'
        },
        formattedFecha: {
            get() {
                if(!this.ordenCompraDto.fecha) return;
                return this.ordenCompraDto.fecha.split('T')[0];
            },
            set(newValue) {
                this.ordenCompraDto.fecha = newValue + 'T00:00:00';
            }
        },
    },
    async mounted() {
        await this.init();
    },
    methods: {
        async init() {
            if (this.$route.params.id) await this.getAsync(this.$route.params.id);
            else this.goBack();
        },
        async getAsync(id) {
            this.uiService.showSpinner(true);
            await this.$store.dispatch("getAsync", id)
                .then((res) => {
                    this.ordenCompraDto = new OrdenCompraDto(res);
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        seeDetail() {
            // Limpiamos la lista antes de navegar
            this.$store.dispatch("clearList");
            this.$router.push({ name: "detail", params: { id: this.ordenCompraDto.id } });
        },
        async remove() {
            if (
                await this.uiService.confirmActionModal(
                "¿Está usted seguro que desea eliminar a este Documento de Compras?",
                "Aceptar",
                "Cancelar"
                )
            ){
                this.uiService.showSpinner(true)
                await this.$store.dispatch("deleteAsync", this.ordenCompraDto)
                    .then(async () => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            this.goBack();
                        } else {
                            this.uiService.showMessageError("Operación rechazada")
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
            this.$router.push({ name: "edit", query: {desdeDetalle: true}, params: { id: this.ordenCompraDto.id } });
        },
        formatCurrency(value) {
            if (value !== null) {
                return `$ ${value.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,')}`;
            }
            return '$ 0.00';
        },
    },
};
</script>