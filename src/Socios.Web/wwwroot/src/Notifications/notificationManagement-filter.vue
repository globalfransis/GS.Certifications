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
                                <label>Fecha Registración</label>
                                <input class="form-control" type="date" v-model="parameters.FechaEncolado" />
                                <!-- <span class="text-danger field-validation-error" data-valmsg-for="fechaRegistracion"
                                    data-valmsg-replace="true">
                                    {{ errorBag.get("DATE_RANGE_EXCEDEED_ERROR") }}
                                </span> -->
                            </div>

                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Fecha Notificación</label>
                                <input class="form-control" type="date" v-model="parameters.FechaEnviado" />
                                <!-- <span class="text-danger field-validation-error" data-valmsg-for="fechaNotificacion"
                                    data-valmsg-replace="true">
                                    {{ errorBag.get("DATE_RANGE_EXCEDEED_ERROR") }}
                                </span> -->
                            </div>


                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Tipo</label>
                                <input class="form-control" disabled value="Correo">
                                <!-- select de tipo de notificacion -->
                            </div>

                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Destinatarios</label>
                                <input class="form-control" type="text" v-model="parameters.Destinatarios" />
                            </div>

                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>CC</label>
                                <input class="form-control" type="text" v-model="parameters.ConCopia" />
                            </div>

                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>CCO</label>
                                <input class="form-control" type="text" v-model="parameters.ConCopiaOculta" />
                            </div>

                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Asunto</label>
                                <input class="form-control" type="text" v-model="parameters.Asunto" />
                            </div>

                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Estado</label>
                                <notificacionEstados-select v-model="parameters.EstadoIdm" />
                            </div>

                            <div class="col-12 d-flex justify-content-center">
                                <div class="justify-content-between">
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
import notificacionEstadosSelect from "@/selects/notificacionEstados-select.vue";

import Parameters from "./Parameters";

export default {
    components: { notificacionEstadosSelect },
    name: "notificationManagement-filter",
    props: {
        mode: String,
        value: Object,
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
        async clearFilters() {
            this.parameters = new Parameters();
            this.$emit('clear')
        },
    },
    computed: {
        parameters: {
            get: function() {
                return this.value;
            },
            set: function(newValue) {
                this.$emit('input', newValue);
            }
        },
    }
};
</script>