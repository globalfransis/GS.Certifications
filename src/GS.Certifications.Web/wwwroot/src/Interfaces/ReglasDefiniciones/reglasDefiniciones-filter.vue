<template>
    <div>
        <div class="col-12 mt-4">
            <p class="h5">{{loc["Filtro de búsqueda"]}}</p>
        </div>
        <!-- Title end -->
        <div class="col-12">
            <!-- Filters card -->
            <div class="card">
                <div class="card-body">
                    <form method="get">
                        <div class="row">

                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>{{loc["Sistema"]}}</label>
                                <sistema-select v-model="parameters.sistemaIdm" />
                            </div>

                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>{{loc["Interfaz"]}}</label>
                                <interfaz-select :sistemaIdm="parameters.sistemaIdm" v-model="parameters.interfazId" />
                            </div>

                            <div class="col-12 d-flex justify-content-center">
                                <div class="d-flex justify-content-between">
                                    <button v-on:click.prevent="search" class="btn btn-primary btn-sm">
                                        <i class="fas fa-search"></i>
                                        {{loc["Buscar"]}}
                                    </button>
                                    <button tabindex="12" @click.prevent="clearFilters"
                                        class="btn btn-secondary btn-sm ms-2">
                                        <i class="fas fa-eraser"></i>
                                        {{loc["Limpiar"]}}
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
import interfazSelect from "@/selects/interfaz-select.vue";
import sistemaSelect from "@/selects/sistema-select.vue";

import Parameters from "./Parameters.js"

import loc from "@/common/commonLoc.js"

export default {
    components: { interfazSelect, sistemaSelect },
    name: "reglasDefiniciones-filter",
    props: {
        value: Object
    },
    computed: {
    },
    data: function () {
        return {
            loc
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
            get: function() {
                return this.value;
            },
            set: function(newVal) {
                this.$emit("input", newVal);
            }
        }
    },
    watch: {
    }
};
</script>