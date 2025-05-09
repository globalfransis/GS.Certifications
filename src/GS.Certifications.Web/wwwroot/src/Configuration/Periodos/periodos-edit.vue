<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">Edicion de Periodo</p> <!-- Por ejemplo: Alta de Usuario -->
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <!-- Agregar campos del formulario de alta -->
                        <!-- Este es un ejemplo -->
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Año</label><span class="text-danger">*</span>
                            <input type="number" class="form-control" v-model="periodoEditDto.año">
                            <span class="text-danger field-validation-error" data-valmsg-for="Año" data-valmsg-replace="true">
                                {{ errorBag.get("Año") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Numero de periodo</label><span class="text-danger">*</span>
                            <input type="number" class="form-control" v-model="periodoEditDto.numeroPeriodo">
                            <span class="text-danger field-validation-error" data-valmsg-for="NumerioPeriodo" data-valmsg-replace="true">
                                {{ errorBag.get("NumerioPeriodo") }}
                            </span>
                        </div>
                        <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Fecha Inicio</label><span class="text-danger">*</span>
                            <input type="date" class="form-control" v-model="formattedFechaInicio" @change="validateFechaInicio">
                            <span class="text-danger field-validation-error" data-valmsg-for="FechaInicio" data-valmsg-replace="true">
                                {{ errorBag.get("FechaInicio") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Fecha Fin</label><span class="text-danger">*</span>
                            <input type="date" class="form-control" v-model="formattedFechaFin" @change="validateFechaFin">
                            <span class="text-danger field-validation-error" data-valmsg-for="FechaFin" data-valmsg-replace="true">
                                {{ errorBag.get("FechaFin") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Estado de Periodo</label><span class="text-danger">*</span>
                            <estadoPeriodo-select v-model.number="periodoEditDto.estadoIdm" :estadoDeshabilitado="estadoDeshabilitado"
                             :deshabilitarOpcion4="deshabilitarOpcion4" />
                            <span class="text-danger field-validation-error" data-valmsg-for="EstadoPeriodo" data-valmsg-replace="true">
                                {{ errorBag.get("EstadoPeriodo") }}
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
import PeriodoEditDto from './PeriodoEditDto';
import commonMixin from '@/Common/Mixins/commonMixin';

import estadoPeriodoSelect from "@/Components/Periodos/estadoPeriodo-select"

export default {
    components: {
        AcceptButton,
        CancelButton,
        estadoPeriodoSelect
    },
    mixins: [commonMixin],
    name: "periodos-edit",
    data: function () {
        return {
            periodoEditDto: new PeriodoEditDto(),
            uiService: new UiService(),
            estadoDeshabilitado: true,
            deshabilitarOpcion4: false,  // Nueva variable para bloquear la opción 4
            estadoIdmInicial: null
        };
    },
    computed: {
        grants() {
            return this.$store.getters.getGrants;
        },
        errorBag() {
            return this.$store.getters.getErrorBag;
        },
        formattedFechaInicio: {
            get() {
                if(!this.periodoEditDto.fechaInicio) return;
                return this.periodoEditDto.fechaInicio.split('T')[0];
            },
            set(newValue) {
                this.periodoEditDto.fechaInicio = newValue + 'T00:00:00';
            }
        },
        formattedFechaFin: {
            get() {
                if(!this.periodoEditDto.fechaFin) return;
                return this.periodoEditDto.fechaFin.split('T')[0];
            },
            set(newValue) {
                this.periodoEditDto.fechaFin = newValue + 'T00:00:00';
            }
        }
    },
    async mounted() {
        await this.init();
        this.validarEstadoPeriodo();
        this.periodoEditDto.estadoIdm = this.estadoIdmInicial;
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
                    this.periodoEditDto = new PeriodoEditDto(res);
                    this.estadoIdmInicial = this.periodoEditDto.estadoIdm;
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        goDetail() {
            this.errorBag.clear();
            this.$router.push({ name: "detail", params: { id: this.periodoEditDto.id } });
        },
        goBack() {
            this.errorBag.clear();
            this.$router.push({ name: "detail", params: { id: this.periodoEditDto.id } });
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
                await this.$store.dispatch("putAsync", this.periodoEditDto)
                    .then(() => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            this.goDetail();
                        } else {
                            if(this.errorBag.errors.hasOwnProperty('periodo')){
                                this.uiService.showMessageError("Operación rechazada: Las fechas de inicio y fin ya estan contenidas en otro periodo existente")
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
        validacionDeDatos(){
            var validacion = true;
            if(this.periodoEditDto.año == null || this.periodoEditDto.año == ''){
                validacion = false;
                this.errorBag.addError("Año", ["El campo 'Año' es obligatorio"]);
            }
            if(this.periodoEditDto.numeroPeriodo == null || this.periodoEditDto.numeroPeriodo == ''){
                validacion = false;
                this.errorBag.addError("NumerioPeriodo", ["El campo 'Numero de Periodo' es obligatorio"]);
            }
            if(this.periodoEditDto.fechaInicio == null || this.periodoEditDto.fechaInicio == '' || this.periodoEditDto.fechaInicio == "T00:00:00"){
                validacion = false;
                this.errorBag.addError("FechaInicio", ["El campo 'Fecha de Inicio' es obligatorio"]);
            }
            if(this.periodoEditDto.fechaFin == null || this.periodoEditDto.fechaFin == '' || this.periodoEditDto.fechaFin == "T00:00:00"){
                validacion = false;
                this.errorBag.addError("FechaFin", ["El campo 'Fecha de Fin' es obligatorio"]);
            }
            if(this.periodoEditDto.estadoIdm == null){
                validacion = false;
                this.errorBag.addError("EstadoPeriodo", ["El campo 'Estado de Periodo' es obligatorio"]);
            }
            return validacion;
        },
        validarEstadoPeriodo() {
            if((this.periodoEditDto.fechaInicio == null || this.periodoEditDto.fechaInicio == '') ||  (this.periodoEditDto.fechaFin == null || this.periodoEditDto.fechaFin == '') ){
                this.periodoEditDto.estadoIdm = null;
                this.estadoDeshabilitado = true;
                return;
            }
            const fechaInicio = new Date(this.periodoEditDto.fechaInicio);
            const fechaFin = new Date(this.periodoEditDto.fechaFin);
            const fechaActual = new Date();

            const mesActual = fechaActual.getMonth() + 1; // Meses en JavaScript van de 0 a 11
            const mesFin = fechaFin.getMonth() + 1;
            const mesInicio = fechaInicio.getMonth() + 1;

            // Reglas de validación
            if (fechaActual >= fechaInicio && fechaActual <= fechaFin) {
                this.periodoEditDto.estadoIdm = 2;
                this.estadoDeshabilitado = true;
            } else if (mesActual > mesFin + 1) {
                this.periodoEditDto.estadoIdm = 3;
                this.estadoDeshabilitado = true;
            } else if(mesActual > mesFin && mesActual == mesFin + 1){ 
                // Deshabilitar opción 4 si el mes actual es mayor que el mes de fecha fin
                this.deshabilitarOpcion4 = true
                this.periodoEditDto.estadoIdm = null;
                this.estadoDeshabilitado = false;
            } else if (fechaActual < fechaInicio ) {
                this.periodoEditDto.estadoIdm = 4;
                this.estadoDeshabilitado = true;
            }
        },
        validateFechaInicio() {
            this.errorBag.clear();
            if(this.periodoEditDto.fechaInicio == '' || this.periodoEditDto.fechaInicio == null || this.periodoEditDto.fechaInicio == "T00:00:00"){
                this.periodoEditDto.fechaInicio = '';
                return
            }
            if (this.periodoEditDto.fechaFin && this.periodoEditDto.fechaInicio > this.periodoEditDto.fechaFin && this.periodoEditDto.fechaInicio != "T00:00:00") {
                this.errorBag.addError("FechaInicio", ["La fecha de inicio no puede ser posterior a la fecha de fin."]);
                this.periodoEditDto.fechaInicio = '';
            }
        },
        validateFechaFin() {
            this.errorBag.clear();
            if(this.periodoEditDto.fechaFin == '' || this.periodoEditDto.fechaFin == null || this.periodoEditDto.fechaFin == "T00:00:00"){
                this.periodoEditDto.fechaFin = '';
                return
            }
            if (this.periodoEditDto.fechaInicio && this.periodoEditDto.fechaFin < this.periodoEditDto.fechaInicio && this.periodoEditDto.fechaFin != "T00:00:00") {
                this.errorBag.addError("FechaFin", ["La fecha de fin no puede ser anterior a la fecha de inicio."]);
                this.periodoEditDto.fechaFin = '';
            }
        }
    },
    watch: {
        "periodoEditDto.fechaInicio": "validarEstadoPeriodo",
        "periodoEditDto.fechaFin": "validarEstadoPeriodo"
    },
};
</script>