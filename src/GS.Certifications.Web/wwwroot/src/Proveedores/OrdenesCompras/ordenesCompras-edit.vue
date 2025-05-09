<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">Edición de Documento de Compras</p> <!-- Por ejemplo: Alta de Usuario -->
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Numero de Orden</label><span class="text-danger">*</span>
                            <input maxlength="50" type="number" class="form-control" v-model="ordenCompraDto.numeroOrden">
                            <span class="text-danger field-validation-error" data-valmsg-for="NumeroOrden" data-valmsg-replace="true">
                                {{ errorBag.get("NumeroOrden") }}
                            </span>
                        </div>
                        <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Fecha</label><span class="text-danger">*</span>
                            <input type="date" class="form-control" v-model="formattedFecha">
                            <span class="text-danger field-validation-error" data-valmsg-for="Fecha" data-valmsg-replace="true">
                                {{ errorBag.get("Fecha") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Proveedor</label>
                            <empresaPortal-select v-model="ordenCompraDto.empresaPortalId" />
                            <span class="text-danger field-validation-error" data-valmsg-for="EmpresaPortal" data-valmsg-replace="true">
                                {{ errorBag.get("EmpresaPortal") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Tipo</label><span class="text-danger">*</span>
                            <ordenesComprasTipos-select v-model="ordenCompraDto.ordenCompraTipoId" />
                            <span class="text-danger field-validation-error" data-valmsg-for="OrdenCompraTipo" data-valmsg-replace="true">
                                {{ errorBag.get("OrdenCompraTipo") }}
                            </span>
                        </div>
                        <!-- <div class="form-group col-lg-4 col-sm-12 mb-4">
                            <label class="control-label">Codigo HES</label>
                            <input maxlength="50" type="text" class="form-control" v-model="ordenCompraDto.codigoHES">
                            <span class="text-danger field-validation-error" data-valmsg-for="CodigoHES" data-valmsg-replace="true">
                                {{ errorBag.get("CodigoHES") }}
                            </span>
                        </div> -->
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Código Sistema Externo</label>
                            <input maxlength="50" type="text" class="form-control" v-model="ordenCompraDto.codigoQAD">
                            <span class="text-danger field-validation-error" data-valmsg-for="CodigoQAD" data-valmsg-replace="true">
                                {{ errorBag.get("CodigoQAD") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Estado</label><span class="text-danger">*</span>
                            <ordenesComprasEstados-select :nullOption="false" v-model="ordenCompraDto.ordenCompraEstadoIdm" />
                            <span class="text-danger field-validation-error" data-valmsg-for="OrdenCompraEstado" data-valmsg-replace="true">
                                {{ errorBag.get("OrdenCompraEstado") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Importe</label><span class="text-danger">*</span>
                            <input type="number" class="form-control" v-model="ordenCompraDto.importe">
                            <span class="text-danger field-validation-error" data-valmsg-for="Importe" data-valmsg-replace="true">
                                {{ errorBag.get("Importe") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Moneda</label><span class="text-danger">*</span>
                            <monedasEmpresa-select v-model="ordenCompraDto.monedaId" />
                            <span class="text-danger field-validation-error" data-valmsg-for="Moneda" data-valmsg-replace="true">
                                {{ errorBag.get("Moneda") }}
                            </span>
                        </div>
                        <div class="form-group col-12 mb-4">
                            <label class="control-label">Observaciones</label>
                            <textarea class="form-control" v-model="ordenCompraDto.observaciones"></textarea>
                            <span class="text-danger field-validation-error" data-valmsg-for="Observaciones" data-valmsg-replace="true">
                                {{ errorBag.get("Observaciones") }}
                            </span>
                        </div>
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
import OrdenCompraEditDto from './OrdenCompraEditDto';

import commonMixin from '@/Common/Mixins/commonMixin';

import empresaPortalSelect from "@/Selects/empresaPortal-select"
import ordenesComprasTiposSelect from "@/Components/OrdenesCompras/ordenesComprasTipos-select"
import ordenesComprasEstadosSelect from "@/Components/OrdenesCompras/ordenesComprasEstados-select"
import monedasEmpresaSelect from "@/Components/Empresas/monedasEmpresa-select";

export default {
    components: {
        AcceptButton,
        CancelButton,
        empresaPortalSelect,
        ordenesComprasTiposSelect,
        ordenesComprasEstadosSelect,
        monedasEmpresaSelect
    },
    mixins: [commonMixin],
    name: "ordenesCompras-edit",
    data: function () {
        return {
            ordenCompraDto: new OrdenCompraEditDto(),
            uiService: new UiService(),
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
                    this.ordenCompraDto = new OrdenCompraEditDto(res);
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        goDetail() {
            this.errorBag.clear();
            this.$router.push({ name: "detail", params: { id: this.ordenCompraDto.id } });
        },
        goBack() {
            this.errorBag.clear();
            this.$router.push({ name: "detail", params: { id: this.ordenCompraDto.id } });
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
                this.uiService.showSpinner(true);
                await this.$store.dispatch("putAsync", this.ordenCompraDto)
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
            if(this.ordenCompraDto.numeroOrden == null ||
             this.ordenCompraDto.numeroOrden == '' ||
              this.ordenCompraDto.numeroOrden == ""){
                validacion = false;
                this.errorBag.addError("NumeroOrden", ["El campo 'Numero de Orden' es obligatorio"]);
            } else if (isNaN(this.ordenCompraDto.numeroOrden)) {
                validacion = false
                this.errorBag.addError("NumeroOrden", ["El Numero de Orden ingresado debe ser numerico"]);
            }
            if(this.ordenCompraDto.fecha == null || this.ordenCompraDto.fecha == '' || this.ordenCompraDto.fecha == "T00:00:00"){
                validacion = false;
                this.errorBag.addError("Fecha", ["El campo 'Fecha' es obligatorio"]);
            }
            if(this.ordenCompraDto.ordenCompraTipoId == null){
                validacion = false;
                this.errorBag.addError("OrdenCompraTipo", ["El campo 'Tipo' es obligatorio"]);
            }
            if(this.ordenCompraDto.ordenCompraEstadoIdm == null){
                validacion = false;
                this.errorBag.addError("OrdenCompraEstado", ["El campo 'Estado' es obligatorio"]);
            }
            if(this.ordenCompraDto.importe == null ||
             this.ordenCompraDto.importe == '' ||
              this.ordenCompraDto.importe == ""){
                validacion = false;
                this.errorBag.addError("Importe", ["El campo 'Importe' es obligatorio"]);
            } else if (isNaN(this.ordenCompraDto.importe)) {
                validacion = false
                this.errorBag.addError("Importe", ["El Importe ingresado debe ser numerico"]);
            }
            if(this.ordenCompraDto.monedaId == null){
                validacion = false;
                this.errorBag.addError("Moneda", ["El campo 'Moneda' es obligatorio"]);
            }
            return validacion;
        }
    },
    watch: {
        'ordenCompraDto.importe': function(newValue) {
            const isValidNumber = /^-?\d+([.,]\d+)?$/.test(newValue);

             if (!isValidNumber || newValue < 0) {
                this.ordenCompraDto.importe = 0;
            } else if (newValue > 10000000000) {
                this.ordenCompraDto.importe = 10000000000;
            } else {
                // Reemplazar coma por punto para convertir a número
                const numericValue = Number(newValue.toString().replace(',', '.'));

                // Verificar si tiene más de dos decimales
                const decimalPart = newValue.toString().split(/[.,]/)[1];
                if (decimalPart && decimalPart.length > 2) {
                this.$nextTick(() => {
                    this.ordenCompraDto.importe = newValue.slice(0, newValue.length - 1);
                    });
                return;
                }

                this.ordenCompraDto.importe = numericValue;
            }
        },
        'ordenCompraDto.numeroOrden': function(newValue){
            if (newValue === '') {
                this.ordenCompraDto.numeroOrden = 0;
                return
            }
        }
    }
};
</script>