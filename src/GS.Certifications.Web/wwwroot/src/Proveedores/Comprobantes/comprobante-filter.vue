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

                            <div class="form-group col-lg-3 col-sm-12 mb-4">
                                <label class="control-label">Proveedor</label>
                                <empresaPortal-select v-model="parameters.empresaId" />
                                <span class="text-danger field-validation-error">
                                    {{ errorBag.get("empresaId") }}
                                </span>
                            </div>

                            <div class="form-group col-lg-3 col-sm-12 mb-4">
                                <label class="control-label">Número</label>
                                <input type="text" class="form-control" v-model="parameters.numero">
                            </div>

                            <div class="form-group col-lg-3 col-sm-12 mb-4">
                                <label class="control-label">Tipo Comprobante</label>
                                <comprobanteTipo-select v-model="parameters.comprobanteTipoId" />
                            </div>

                            
                            <div class="form-group col-lg-3 col-sm-12 mb-4">
                                <label class="control-label">Estado</label>
                                <comprobanteEstado-select v-model="parameters.comprobanteEstadoId" />
                            </div>

                            <div class="form-group col-lg-3 col-sm-12 mb-4">
                                <label class="control-label">Tipo Código Autorización</label>
                                <codigoAutorizacionTipo-select v-model="parameters.tipoCodigoAutorizacionId" />
                            </div>

                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Fecha Emisión Desde</label>
                                <input class="form-control" type="date" v-model="parameters.fechaEmisionDesde" />
                            </div>

                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Fecha Emisión Hasta</label>
                                <input class="form-control" type="date" v-model="parameters.fechaEmisionHasta" />
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
import comprobanteTipoSelect from "@/Selects/comprobanteTipo-select.vue";
import comprobanteEstadoSelect from "@/Selects/comprobanteEstado-select.vue";
import empresaPortalSelect from "@/Selects/empresaPortal-select.vue";
import codigoAutorizacionTipoSelect from "@/Selects/codigoAutorizacionTipo-select.vue";

export default {
    components: { comprobanteTipoSelect, codigoAutorizacionTipoSelect, empresaPortalSelect, comprobanteEstadoSelect },
    name: "comprobante-filter",
    props: {
        value: Object
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
        }
    },
    computed: {
        errorBag() {
            return this.$store.getters.getErrorBag;
        },
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