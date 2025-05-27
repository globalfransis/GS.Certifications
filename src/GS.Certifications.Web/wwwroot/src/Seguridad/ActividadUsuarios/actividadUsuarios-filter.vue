<template>
    <div>
        <div class="col-12 mt-4">
            <p class="h5">{{ loc["Filtro de búsqueda"] }}</p>
        </div>
        <!-- Title end -->
        <div class="col-12">
            <!-- Filters card -->
            <div class="card">
                <div class="card-body">
                    <form method="get">
                        <div class="row">

                            <!-- Agregar controles de filtro  -->
                            <!-- Este es un ejemplo -->
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>{{ loc["Email"] }}</label>
                                <input type="text" class="form-control" v-model="parameters.Email">
                            </div>
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>{{loc["Nombre"]}}</label>
                                <input type="text" class="form-control" v-model="parameters.Nombre">
                            </div>
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>{{loc["Apellido"]}}</label>
                                <input type="text" class="form-control" v-model="parameters.Apellido">
                            </div>
                            <!--
                            <div class="col-12 mb-4 form-group required">
                                <label class="control-label">{{ loc["Dominio"] }}</label>
                                <usuariosExternosDomainFs-select v-model="parameters.dominioId" />
                                <span class="text-danger field-validation-error">
                                {{ errorBag.get("dominioId") }}
                                </span>
                            </div>
                            -->
                            <!-- <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Tipo Usuario</label>
                                <input type="text" class="form-control" v-model="parameters.TipoUsuario">
                            </div> -->
                            <div class="col-lg-3 col-md-6 col-sm-12 mb-4">
                                <label class="control-label">{{loc["Tipo Usuario"]}}</label>
                                <userTypesSelect v-model="parameters.TipoUsuario"/>
                            </div>
                            <!-- <div class="col-lg-3 col-sm-12 mb-4">
                                <label>Dominio</label>
                                <input type="text" class="form-control" v-model="parameters.DominioId">
                            </div> -->
                            <div class="col-lg-3 col-md-6 col-sm-12 mb-4">
                                <label class="control-label">{{loc["Dominio"]}}</label>
                                <userDomainFSelect v-model="parameters.DominioId"/>
                            </div>
                            <!-- <div class="col-lg-3 col-sm-12 mb-4">
                                <label class="control-label">{{ Dominio }}</label>
                                <usuariosExternosDomainFs-select v-model="parameters.DominioId" />
                            </div> -->
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>{{loc["Fecha/Hora Desde"]}}</label>
                                <input type="datetime-local" class="form-control" v-model="parameters.FechaDesde">
                            </div>
                            <div class="col-lg-3 col-sm-12 mb-4">
                                <label>{{loc["Fecha/Hora Hasta"]}}</label>
                                <input type="datetime-local" class="form-control" v-model="parameters.FechaHasta">
                            </div>
                            <div class="col-lg-3 col-md-6 col-sm-12 mb-4">
                                <label>{{loc["Actividad"]}}</label>
                                <select class="form-select" v-model.number="parameters.Actividad">
                                    <option :value="null">{{loc["Sin Especificar"]}}</option>
                                    <option :value="ACCESSO_OK">{{loc["ACCESO OK"]}}</option>
                                    <option :value="ACCESSO_FALLIDO">{{loc["ACCESO FALLIDO"]}}</option>
                                </select>
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

const ACCESSO_OK = 1;
const ACCESSO_FALLIDO = 2;

import Parameters from "./Parameters.js"
import userDomainFSelect from "../../Components/DomainF/userDomainF-select.vue"
import userTypesSelect from "../../Components/UserTypes/userTypes-select.vue";

import loc from "@/common/commonLoc.js"

export default {
    components: {
        userDomainFSelect,
        userTypesSelect
    },
    name: "template-filter",
    props: {
        value: Object
    },
    computed: {
    },
    data: function () {
        return {
            ACCESSO_OK,
            ACCESSO_FALLIDO,
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