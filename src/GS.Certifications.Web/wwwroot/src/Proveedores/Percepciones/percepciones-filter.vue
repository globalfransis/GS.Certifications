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
                                <label class="control-label">Descripción</label>
                                <input type="text" class="form-control" v-model="parameters.descripcion">
                            </div>
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label class="control-label">Tipo</label>
                                <tipoPercepcion-select v-model="parameters.percepcionTipoId"/>
                            </div>
                            <div class="col-lg-3 col-md-6 col-sm-12 mb-4">
                                <label class="control-label">Provincia</label>
                                <province-select v-model="parameters.provinciaId" :paisId="1"/>
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

import provinceSelect from "@/Components/Global/Provinces/province-select";
import tipoPercepcionSelect from "@/Components/Percepciones/tipoPercepcion-select"

export default {
    components: {
        provinceSelect,
        tipoPercepcionSelect
    },
    name: "percepciones-filter",
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
    }
};
</script>