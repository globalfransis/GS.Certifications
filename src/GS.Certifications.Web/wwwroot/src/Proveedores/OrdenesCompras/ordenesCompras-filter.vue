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
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label class="control-label">Proveedor</label>
                                <empresaPortal-select v-model="parameters.empresaPortalId"/>
                            </div>
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Fecha Desde</label>
                                <input type="date" class="form-control" v-model="parameters.fechaDesde"
                                @change="validateFechaInicio">
                                <span class="text-danger field-validation-error" data-valmsg-for="FechaDesde" data-valmsg-replace="true">
                                    {{ errorBag.get("FechaDesde") }}
                                </span>
                            </div>
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Fecha Hasta</label>
                                <input type="date" class="form-control" v-model="parameters.fechaHasta"
                                @change="validateFechaFin">
                                <span class="text-danger field-validation-error" data-valmsg-for="FechaHasta" data-valmsg-replace="true">
                                    {{ errorBag.get("FechaHasta") }}
                                </span>
                            </div>
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label class="control-label">Tipo</label>
                                <ordenesComprasTipos-select v-model="parameters.ordenCompraTipoId"/>
                            </div>
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label class="control-label">Estado</label>
                                <ordenesComprasEstados-select v-model="parameters.ordenCompraEstadoIdm"/>
                            </div>
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

import empresaPortalSelect from "@/Selects/empresaPortal-select"
import ordenesComprasTiposSelect from "@/Components/OrdenesCompras/ordenesComprasTipos-select"
import ordenesComprasEstadosSelect from "@/Components/OrdenesCompras/ordenesComprasEstados-select"

export default {
    components: {
        empresaPortalSelect,
        ordenesComprasTiposSelect,
        ordenesComprasEstadosSelect
    },
    name: "ordenesCompras-filter",
    props: {
        value: Object
    },
    computed: {
    },
    data: function () {
        return {
        };
    },
    mounted() {
    },
    methods: {
        async search() {
            this.$emit("search");
        },
        clearFilters() {
            this.parameters = new Parameters();
            this.$emit("clear");
        },
        validateFechaInicio() {
            this.errorBag.clear();
            if (this.parameters.fechaHasta && this.parameters.fechaDesde > this.parameters.fechaHasta) {
                this.errorBag.addError("FechaDesde", ["La fecha de inicio no puede ser posterior a la fecha de fin."]);
                this.parameters.fechaDesde = '';
            }
        },
        validateFechaFin() {
            this.errorBag.clear();
            if (this.parameters.fechaDesde && this.parameters.fechaHasta < this.parameters.fechaDesde && this.parameters.fechaHasta != "") {
                this.errorBag.addError("FechaHasta", ["La fecha de fin no puede ser anterior a la fecha de inicio."]);
                this.parameters.fechaHasta = '';
            }
        }
    },
    computed: {
        parameters: {
            get: function () {
                return this.value;
            },
            set: function (newVal) {
                this.$emit("input", newVal);
            }
        },
        errorBag() {
            return this.$store.getters.getErrorBag;
        },
    },
    watch: {
    }
};
</script>