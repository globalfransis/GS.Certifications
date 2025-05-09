<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">Alta de Periodo</p> <!-- Por ejemplo: Alta de Usuario -->
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <!-- Agregar campos del formulario de alta -->
                        <!-- Este es un ejemplo -->
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Año</label><span class="text-danger">*</span>
                            <input type="number" class="form-control" v-model="periodoCreateDto.año">
                            <span class="text-danger field-validation-error" data-valmsg-for="Año" data-valmsg-replace="true">
                                {{ errorBag.get("Año") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Numero de periodo</label><span class="text-danger">*</span>
                            <input type="number" class="form-control" v-model="periodoCreateDto.numeroPeriodo">
                            <span class="text-danger field-validation-error" data-valmsg-for="NumerioPeriodo" data-valmsg-replace="true">
                                {{ errorBag.get("NumerioPeriodo") }}
                            </span>
                        </div>
                        <div class="col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Fecha Inicio</label><span class="text-danger">*</span>
                            <input type="date" class="form-control" v-model="periodoCreateDto.fechaInicio"
                            @change="validateFechaInicio">
                            <span class="text-danger field-validation-error" data-valmsg-for="FechaInicio" data-valmsg-replace="true">
                                {{ errorBag.get("FechaInicio") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Fecha Fin</label><span class="text-danger">*</span>
                            <input type="date" class="form-control" v-model="periodoCreateDto.fechaFin"
                            @change="validateFechaFin">
                            <span class="text-danger field-validation-error" data-valmsg-for="FechaFin" data-valmsg-replace="true">
                                {{ errorBag.get("FechaFin") }}
                            </span>
                        </div>
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Estado de Periodo</label><span class="text-danger">*</span>
                            <estadoPeriodo-select v-model.number="periodoCreateDto.estadoIdm" :estadoDeshabilitado="estadoDeshabilitado"
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
            <accept-button :disabled="!grants.create" @click="createAsync" style="margin-right: 10px;">
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

import PeriodoCreateDto from './PeriodoCreateDto' // Modificar por la clase dto correspondiente

import commonMixin from '@/Common/Mixins/commonMixin';

import estadoPeriodoSelect from "@/Components/Periodos/estadoPeriodo-select"

export default {
    components: {
        AcceptButton,
        CancelButton,
        estadoPeriodoSelect
    },
    mixins: [commonMixin],
    name: "periodos-create", // Modificar

    data: function () {
        return {
            periodoCreateDto: new PeriodoCreateDto(),
            uiService: new UiService(),
            estadoDeshabilitado: true,
            deshabilitarOpcion4: false,  // Nueva variable para bloquear la opción 4
        };
    },
    computed: {
        grants() {
            return this.$store.getters.getGrants;
        },
        errorBag() {
            return this.$store.getters.getErrorBag;
        }
    },
    async mounted() {
        await this.init();
    },
    methods: {
        async init() {
            this.errorBag.clear();
            // Si no hay permisos de alta, volvemos a la lista
            if (!this.grants.create) this.$router.push({ name: "home" });
        },
        cancel() {
            this.errorBag.clear();
            this.$router.push({ name: "home" });
        },
        goDetail(id) {
            this.errorBag.clear();
            this.$router.push({ name: "detail", params: { id: id, create: true }});
        },
        async createAsync() {
            this.errorBag.clear();
            if(this.validacionDeDatos() && !this.errorBag.hasErrors()){
                this.uiService.showSpinner(true)
                await this.$store.dispatch("postAsync", this.periodoCreateDto)
                    .then((id) => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            this.goDetail(id);
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
            if(this.periodoCreateDto.año == null || this.periodoCreateDto.año == ''){
                validacion = false;
                this.errorBag.addError("Año", ["El campo 'Año' es obligatorio"]);
            }
            if(this.periodoCreateDto.numeroPeriodo == null || this.periodoCreateDto.numeroPeriodo == ''){
                validacion = false;
                this.errorBag.addError("NumerioPeriodo", ["El campo 'Numero de Periodo' es obligatorio"]);
            }
            if(this.periodoCreateDto.fechaInicio == null || this.periodoCreateDto.fechaInicio == '' || this.periodoCreateDto.fechaInicio == "T00:00:00"){
                validacion = false;
                this.errorBag.addError("FechaInicio", ["El campo 'Fecha de Inicio' es obligatorio"]);
            }
            if(this.periodoCreateDto.fechaFin == null || this.periodoCreateDto.fechaFin == '' || this.periodoCreateDto.fechaFin == "T00:00:00"){
                validacion = false;
                this.errorBag.addError("FechaFin", ["El campo 'Fecha de Fin' es obligatorio"]);
            }
            if(this.periodoCreateDto.estadoIdm == null){
                validacion = false;
                this.errorBag.addError("EstadoPeriodo", ["El campo 'Estado de Periodo' es obligatorio"]);
            }
            return validacion;

        },
        validarEstadoPeriodo() {
            if((this.periodoCreateDto.fechaInicio == null || this.periodoCreateDto.fechaInicio == '') ||  (this.periodoCreateDto.fechaFin == null || this.periodoCreateDto.fechaFin == '') ){
                this.periodoCreateDto.estadoIdm = null;
                this.estadoDeshabilitado = true;
                return;
            }
            const fechaInicio = new Date(this.periodoCreateDto.fechaInicio + "T00:00:00");
            const fechaFin = new Date(this.periodoCreateDto.fechaFin + "T00:00:00");
            const fechaActual = new Date();

            const mesActual = fechaActual.getMonth() + 1; // Meses en JavaScript van de 0 a 11
            const mesFin = fechaFin.getMonth() + 1;
            const mesInicio = fechaInicio.getMonth() + 1;

            // Reglas de validación
            if (fechaActual >= fechaInicio && fechaActual <= fechaFin) {
                this.periodoCreateDto.estadoIdm = 2;
                this.estadoDeshabilitado = true;
            } else if (mesActual > mesFin + 1) {
                this.periodoCreateDto.estadoIdm = 3;
                this.estadoDeshabilitado = true;
            } else if(mesActual > mesFin && mesActual == mesFin + 1){ 
                // Deshabilitar opción 4 si el mes actual es mayor que el mes de fecha fin
                this.deshabilitarOpcion4 = true
                this.periodoCreateDto.estadoIdm = null;
                this.estadoDeshabilitado = false;
            } else if (fechaActual < fechaInicio ) {
                this.periodoCreateDto.estadoIdm = 4;
                this.estadoDeshabilitado = true;
            }
        },
        validateFechaInicio() {
            this.errorBag.clear();
            if(this.periodoCreateDto.fechaInicio == '' || this.periodoCreateDto.fechaInicio == null || this.periodoCreateDto.fechaInicio == "T00:00:00"){
                this.periodoCreateDto.fechaInicio = '';
                return
            }
            if (this.periodoCreateDto.fechaFin && this.periodoCreateDto.fechaInicio > this.periodoCreateDto.fechaFin) {
                this.errorBag.addError("FechaInicio", ["La fecha de inicio no puede ser posterior a la fecha de fin."]);
                this.periodoCreateDto.fechaInicio = '';
            }
        },
        validateFechaFin() {
            this.errorBag.clear();
            if(this.periodoCreateDto.fechaFin == '' || this.periodoCreateDto.fechaFin == null || this.periodoCreateDto.fechaFin == "T00:00:00"){
                this.periodoCreateDto.fechaFin = '';
                return
            }
            if (this.periodoCreateDto.fechaInicio && this.periodoCreateDto.fechaFin < this.periodoCreateDto.fechaInicio) {
                this.errorBag.addError("FechaFin", ["La fecha de fin no puede ser anterior a la fecha de inicio."]);
                this.periodoCreateDto.fechaFin = '';
            }
        },
    },
    watch: {
        "periodoCreateDto.fechaInicio": "validarEstadoPeriodo",
        "periodoCreateDto.fechaFin": "validarEstadoPeriodo"
    }
};
</script>