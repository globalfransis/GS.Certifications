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


                            <div class="form-group col-lg-3 col-sm-12 mb-4">
                                <label class="control-label">{{loc["Certificación"]}}</label>
                                <input type="text" disabled class="form-control" value="FONASBA">
                            </div>
                            <!-- <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Nombre</label>
                                <input type="text" class="form-control" v-model="parameters.nombre">
                            </div> -->

                            <div class="form-group col-lg-3 col-sm-12 mb-4">
                                <label class="control-label">{{loc["Estado"]}}</label>
                                <solicitudCertificacionEstado-select v-model="parameters.estadoId" />
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
import Parameters from "./Parameters.js"
import solicitudCertificacionEstadoSelect from "@/Selects/solicitudCertificacionEstado-select.vue";

import loc from "@/common/commonLoc.js"

export default {
    components: { solicitudCertificacionEstadoSelect },
    name: "solicitudCertificacion-filter",
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