<template>
    <div>
        <div class="col-12 mt-4">
            <p class="h5">Filtro de búsqueda</p>
        </div>
        <!-- Title end -->
        <div class="col-12">
            <!-- Filters card -->
            <div class="card">
                <div class="card-body">
                    <form method="get">
                        <div class="row">
                            <!-- Agregar controles de filtro -->
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label class="control-label">Estado de Periodo</label>
                                <estadoPeriodo-select v-model.number="parameters.estadoIdm" />
                            </div>
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Fecha Desde</label>
                                <input type="date" class="form-control" v-model="parameters.fechaInicio"
                                @change="validateFechaInicio">
                                <span class="text-danger field-validation-error" data-valmsg-for="FechaInicio" data-valmsg-replace="true">
                                {{ errorBag.get("FechaInicio") }}
                            </span>
                            </div>
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Fecha Hasta</label>
                                <input type="date" class="form-control" v-model="parameters.fechaFin"
                                @change="validateFechaFin">
                                <span class="text-danger field-validation-error" data-valmsg-for="FechaFin" data-valmsg-replace="true">
                                {{ errorBag.get("FechaFin") }}
                            </span>
                            </div>
                            <!-- Botones separados -->
                            <div class="col-12 d-flex justify-content-center mt-4">
                                <div class="d-flex justify-content-between">
                                    <button v-on:click.prevent="search" class="btn btn-primary btn-sm">
                                        <i class="fas fa-search"></i>
                                        Buscar
                                    </button>
                                    <button tabindex="12" @click.prevent="clearFilters"
                                        class="btn btn-secondary btn-sm ms-2">
                                        <i class="fas fa-eraser"></i>
                                        Limpiar
                                    </button>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import Parameters from "./Parameters.js"
import estadoPeriodoSelect from "@/Components/Periodos/estadoPeriodo-select"

export default {
    components: {
        estadoPeriodoSelect
    },
    name: "periodos-filter",
    props: {
        value: Object
    },
    computed: {
        errorBag() {
            return this.$store.getters.getErrorBag;
        },
        parameters: {
            get() {
                return this.value;
            },
            set(newVal) {
                this.$emit("input", newVal);
            }
        }
    },
    methods: {
        search() {
            if (!this.errorBag.hasErrors()) {
                this.errorBag.clear();
                this.$emit("search");
            }
        },
        clearFilters() {
            this.errorBag.clear();
            this.parameters = new Parameters();
            this.$emit("clear");
        },
        validateFechaInicio() {
            this.errorBag.clear();
            if (this.parameters.fechaFin && this.parameters.fechaInicio > this.parameters.fechaFin) {
                this.errorBag.addError("FechaInicio", ["La fecha de inicio no puede ser posterior a la fecha de fin."]);
                this.parameters.fechaInicio = '';
            }
        },
        validateFechaFin() {
            this.errorBag.clear();
            if (this.parameters.fechaInicio && this.parameters.fechaFin < this.parameters.fechaInicio) {
                this.errorBag.addError("FechaFin", ["La fecha de fin no puede ser anterior a la fecha de inicio."]);
                this.parameters.fechaFin = '';
            }
        }
    }
};
</script>