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
                                <label class="control-label">Nombre</label>
                                <input type="text" class="form-control" v-model="parameters.nombre">
                            </div>          
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label class="control-label">Característica</label>
                                <select class="form-select" v-model="parameters.caracteristica">
                                    <option :value="null">Sin especificar</option>
                                    <option v-for="caracteristica in caracteristicas" :key="caracteristica.idm" :value="caracteristica.idm">
                                        {{ caracteristica.nombre }}
                                    </option>
                                </select>
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

export default {
    components: {
    },
    name: "tiposOrdenesCompras-filter",
    props: {
        value: Object
    },
    computed: {
    },
    data: function () {
        return {
            caracteristicas: [
                { idm: 1, nombre: 'Abierta' },
                { idm: 2, nombre: 'Recurrente' },
                { idm: 3, nombre: 'Unica' }
            ]
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
        },
        errorBag() {
            return this.$store.getters.getErrorBag;
        },
    },
    watch: {
    }
};
</script>