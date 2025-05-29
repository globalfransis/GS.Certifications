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
                                <label>{{loc["Alerta Tipo"]}}</label>
                                <alertaTipos-select v-model="parameters.alertaTipo" />
                            </div>

                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>{{loc["Estado"]}}</label>
                                <div class="col input-group">
                                    <alertaTipoEstados-select :disabled="parameters.alertaTipo.idm == null"
                                        :alertaTipoIdm="parameters.alertaTipo.idm" v-model="parameters.estadoIdm" />
                                    <div data-toggle="tooltip" :title="INFO_ESTADO" class="input-group-text">
                                        <i class="fas fa-lg fa-info-circle col-1"></i>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>{{loc["Ubicación"]}}</label>
                                <div class="col input-group">
                                    <alertaTipoUbicaciones-select :disabled="parameters.alertaTipo.idm == null"
                                        :alertaTipoIdm="parameters.alertaTipo.idm"
                                        v-model="parameters.ubicacionIdm" />
                                    <div data-toggle="tooltip" :title="INFO_UBICACION" class="input-group-text">
                                        <i class="fas fa-lg fa-info-circle col-1"></i>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>{{loc["Horas"]}}</label>
                                <div class="col input-group">
                                    <input class="form-control" type="numeric" v-model="parameters.horas" />
                                    <div data-toggle="tooltip" :title="INFO_HORAS" class="input-group-text">
                                        <i class="fas fa-lg fa-info-circle col-1"></i>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>{{loc["Para"]}}</label>
                                <div class="col input-group">
                                    <input class="form-control" type="text" v-model="parameters.para" />
                                    <div data-toggle="tooltip" :title="INFO_PARA" class="input-group-text">
                                        <i class="fas fa-lg fa-info-circle col-1"></i>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>{{loc["CC"]}}</label>
                                <div class="col input-group">
                                    <input class="form-control" type="text" v-model="parameters.cc" />
                                    <div data-toggle="tooltip" :title="INFO_CC" class="input-group-text">
                                        <i class="fas fa-lg fa-info-circle col-1"></i>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>{{loc["CCO"]}}</label>
                                <div class="col input-group">
                                    <input class="form-control" type="text" v-model="parameters.cco" />
                                    <div data-toggle="tooltip" :title="INFO_CCO" class="input-group-text">
                                        <i class="fas fa-lg fa-info-circle col-1"></i>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>{{loc["Asunto"]}}</label>
                                <div class="col input-group">
                                    <input class="form-control" type="text" v-model="parameters.asunto" />
                                    <div data-toggle="tooltip" :title="INFO_ASUNTO" class="input-group-text">
                                        <i class="fas fa-lg fa-info-circle col-1"></i>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-3 col-md-6 col-sm-12">
                                <div class="col input-group">
                                    <label class="control-label me-2">{{loc["Activa"]}}</label>
                                    <div data-toggle="tooltip" :title="INFO_ACTIVA">
                                        <i class="fas fa-md fa-info-circle col-1"></i>
                                    </div>
                                </div>
                                <div class="btn-group" role="group" aria-label="Basic radio toggle button group">
                                    <input :checked="parameters.activa" type="radio" class="btn-check" name="btnradio"
                                        id="btnActivaFiltro" autocomplete="off" v-on:click="esActiva(true)">
                                    <label class="btn btn-outline-primary" for="btnActivaFiltro">{{loc["Sí"]}}</label>
                                    <input :checked="!parameters.activa" type="radio" class="btn-check" name="btnradio"
                                        id="btnNoEsActivaFiltro" autocomplete="off" v-on:click="esActiva(false)">
                                    <label class="btn btn-outline-secondary" for="btnNoEsActivaFiltro">{{loc["No"]}}</label>
                                </div>
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
import alertaTiposSelect from "@/selects/alertaTipos-select.vue";
import alertaTipoEstadosSelect from "@/selects/alertaTipoEstados-select.vue";
import alertaTipoUbicacionesSelect from "@/selects/alertaTipoUbicaciones-select.vue";
import parametersService from "@/common/parameters";

import loc from "@/common/commonLoc.js"

export default {
    components: { alertaTiposSelect, alertaTipoEstadosSelect, alertaTipoUbicacionesSelect },
    name: "alertas-filter",
    props: {
        mode: String,
    },
    data: function () {
        return {
            loc,
            parameters: new Parameters(),
            parametersStorage: new parametersService(),
            currentPage: 0,
            recordsTotal: 0,
            recordsLength: 25,
            INFO_ESTADO: loc["Estado para Disparar la Notificacion de Alerta. Se debe especificar Alerta Tipo para utilizar este filtro"],
            INFO_UBICACION: loc["Ubicación para Disparar la Notificacion de Alerta. Se debe especificar Alerta Tipo para utilizar este filtro"],
            INFO_HORAS: loc["Horas transcurridas en el Estado, para Disparar la Notificacion de Alerta. Podes indicar cero y esto disparara la Alerta de forma inmediata al llegar al Estado indicado"],
            INFO_PARA: loc["Destinatarios (Para) de la notificacion de Alerta"],
            INFO_CC: loc["Destinatarios (CC) de la notificacion de Alerta"],
            INFO_CCO: loc["Destinatarios (CCO) de la notificacion de Alerta"],
            INFO_ASUNTO: loc["Asunto de la notificacion de Alerta"],
            INFO_ACTIVA: loc["Este marca indica si la Alerta esta siendo disparada o no"]
        };
    },
    mounted() {
        this.parameters = this.getStoredParameters();
    },
    methods: {
        mostrarTooltip(tooltipId) {
            mostrarTooltip(tooltipId)
        },
        async search() {
            this.$emit("search", this.parameters);
        },
        getStoredParameters() {
            return this.parametersStorage.getSearchParameters() ? this.parametersStorage.getSearchParameters() : this.parameters;
        },
        saveCurrentParameters() {
            this.parametersStorage.saveSearchParameters(this.parameters);
        },
        esActiva(status) {
            this.parameters.activa = status;
        },
        clearFilters() {
            this.parameters.alertaTipo = { idm: null, descripcion: '', aceptaDestinatarioVariables: null };
            this.parameters.estadoIdm = null;
            this.parameters.ubicacionIdm = null;
            this.parameters.horas = null;
            this.parameters.para = null;
            this.parameters.cc = null;
            this.parameters.cco = null;
            this.parameters.asunto = null;
            this.parameters.activa = true;
        }
    },
    computed: {
    }
};
class Parameters {
    constructor() {
        this.alertaTipo = { idm: null, descripcion: '', aceptaDestinatarioVariables: null };
        this.estadoIdm = null; // es el estado de la tabla maestra
        this.ubicacionIdm = null;
        this.horas = null;
        this.para = null;
        this.cc = null;
        this.cco = null;
        this.asunto = null;
        this.activa = true;
    }
}
</script>