<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">{{loc["Proceso"]}} {{ this.proceso.id }}</p>
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{loc["Interfaz"]}}</label>
                            <interfaz-select disabled :sistemaIdm="proceso.sistemaIdm" v-model="proceso.interfazIdm" />
                        </div>

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{loc["Tipo"]}}</label>
                            <interfazTipo-select disabled v-model="proceso.tipo" />
                        </div>

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{loc["Estado"]}}</label>
                            <interfazEstado-select :interfazId="proceso.interfazIdm" disabled
                                v-model="proceso.estadoIdm" />
                        </div>

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{loc["Última Modif. Estado"]}}</label>
                            <input disabled class="form-control"
                                v-model="proceso.estadoModificacionFechaHora" />
                        </div>

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{loc["Nombre Archivo"]}}</label>
                            <input disabled class="form-control" v-model="proceso.archivoNombre" />
                        </div>

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{loc["Cant. Leídos"]}}</label>
                            <input disabled class="form-control" v-model="proceso.cantidadRegistrosLeidos" />
                        </div>

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{loc["Cant. Ignorados"]}}</label>
                            <input disabled class="form-control" v-model="proceso.cantidadRegistrosIgnorados" />
                        </div>

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{loc["Cant. No Procesados"]}}</label>
                            <input disabled class="form-control" v-model="proceso.cantidadRegistrosNoProcesados" />
                        </div>


                    </div>
                </div>
            </div>
            <div class="col-12 mb-3 mt-3">

                <div class="card-title d-flex justify-content-between align-items-center mt-2 mb-2">
                    <h5 class="fw-bold">{{loc["Reglas Enforzadas"]}}</h5>
                </div>
                        <table class="
                        table table-bordered table-striped table-hover table-white
                        fix-head 
                      " convert-to-datatable-manual no-paging :id="`reglasEnforzadas-proceso${proceso.id}`">
                            <thead class="table-top">
                                <tr class="text-center">
                                    <th scope="col">{{loc["Nro. Registro"]}}</th>
                                    <th class="col">{{loc["Campo"]}}</th>
                                    <th class="col">{{loc["Regla"]}}</th>
                                    <th class="col">{{loc["Consecuencia"]}}</th>
                                    <th class="col">{{loc["Valor"]}}</th>
                                    <th class="col">{{loc["Valor Relacionado"]}}</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-if="proceso.reglaEnforzadas.length === 0" class="no-data text-center">
                                    <td class="text-center" colspan="7">{{loc["No hay datos"]}}</td>
                                </tr>
                                <tr v-for="(re, index) in proceso.reglaEnforzadas" :key="index">
                                    <td>{{ re.registroNumero }}</td>
                                    <td>{{ re.campo ? re.campo : "-" }}</td>
                                    <td>{{ re.regla }}</td>
                                    <td>{{ re.consecuencia }}</td>
                                    <td>{{ re.campoValor }}</td>
                                    <td>{{ re.campoValorRelacionado }}</td>
                                    <!-- TODO: Adicionales?  -->
                                </tr>
                            </tbody>
                        </table>
            </div>
        </div>
        <div class="col-12 d-flex justify-content-end mb-3 mt-3">
            <cancel-button @click="goBack">{{loc["Volver"]}}</cancel-button>
        </div>
    </div>
</template>

<script>
import CancelButton from "@/components/forms/cancel-button.vue";
import ajax from "@/common/ajaxWrapper";
import ErrorBag from "@/common/ErrorBag";
import UiService from "@/common/uiService";
import interfazSelect from "@/selects/interfaz-select.vue";
import interfazEstadoSelect from "@/selects/interfazEstado-select.vue";
import interfazTipoSelect from "@/selects/interfazTipo-select.vue";

import ResultadoProceso from './ResultadoProceso'

import commonMixin from '@/Common/Mixins/commonMixin';

import loc from "@/common/commonLoc.js"

const RESULTADOS_PROCESOS_URL = baseUrl + "/api/Interfaces/ResultadosProcesos";

export default {
    components: {
        CancelButton,
        interfazSelect,
        interfazEstadoSelect,
        interfazTipoSelect
    },
    mixins: [commonMixin],
    name: "resultadosProcesos-detail",

    data: function () {
        return {
            loc,
            proceso: new ResultadoProceso(),
            errorBag: new ErrorBag(),
            uiService: new UiService()
        };
    },
    async mounted() {
        if (this.$route.params.id) await this.getAsync(this.$route.params.id);
        else this.goBack();
    },
    methods: {
        async getAsync(id) {
            this.uiService.showSpinner(true)
            return new ajax()
                .get(RESULTADOS_PROCESOS_URL + `/${id}`)
                .then((res) => {
                    this.proceso = new ResultadoProceso(res);
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        goBack() {
            this.$router.push({ name: "home", query: {fromDetail: true} });
        }
    },
};
</script>