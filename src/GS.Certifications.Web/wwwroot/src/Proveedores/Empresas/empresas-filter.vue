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

                            <!-- Agregar controles de filtro-->
                            <!-- Este es un ejemplo -->
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Razón Social</label>
                                <input type="text" class="form-control" v-model="parameters.razonSocial">
                            </div>
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Nombre Fantasía</label>
                                <input type="text" class="form-control" v-model="parameters.nombreFantasia">
                            </div>
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Identificador Tributario</label>
                                <input type="text" class="form-control" v-model="parameters.identificadorTributario">
                            </div>
                            <!-- <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Gran Contribuyente</label>
                                <input type="text" class="form-control" v-model="parameters.granContribuyente">
                            </div> -->
                            <div class="col-lg-3 col-md-6 col-sm-12 mb-4">
                                <label>Gran Contribuyente</label>
                                <select class="form-select" v-model.number="parameters.granContribuyente">
                                    <option :value="null">Sin Especificar</option>
                                    <option :value="AFIRMATIVO">Si</option>
                                    <option :value="NEGATIVO">No</option>
                                </select>
                            </div>
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Contacto</label>
                                <input type="text" class="form-control" v-model="parameters.contacto">
                            </div>

                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>País</label>
                                <country-select v-model="parameters.paisId" />
                            </div>
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Provincia</label>
                                <province-select :enabled="parameters.paisId != null" v-model="parameters.provinciaId"
                                :paisId="parameters.paisId" />
                            </div>
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Ciudad</label>
                                <city-select
                                :enabled="parameters.provinciaId != null && parameters.paisId != null" v-model="parameters.ciudadId"
                                :provinciaId="parameters.provinciaId" />
                            </div>

                            <div class="col-12 d-flex justify-content-center">
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

import citySelect from "@/Components/Global/Cities/city-select";
import provinceSelect from "@/Components/Global/Provinces/province-select";
import countrySelect from "@/Components/Global/Countries/country-select";

const AFIRMATIVO = true;
const NEGATIVO = false;

export default {
    components: {
        citySelect,
        provinceSelect,
        countrySelect
    },
    name: "empresas-filter",
    props: {
        value: Object
    },
    computed: {
    },
    data: function () {
        return {
            AFIRMATIVO,
            NEGATIVO
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
        }
    },
    watch: {
        'parameters.paisId': function(){
            this.parameters.provinciaId = null;
            this.parameters.ciudadId = null;
        },
        'parameters.provinciaId': function(){
            this.parameters.ciudadId = null;
        }
    }
};
</script>