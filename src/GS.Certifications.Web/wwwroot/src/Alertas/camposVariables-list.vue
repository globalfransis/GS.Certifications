<template>
    <div class="mt-4">
        <div class="col-12 table-responsive">
            <div class="col input-group">
                <label class="h6 control-label">Campos Variables disponibles para el Tipo de Alerta</label>
                <div data-toggle="tooltip" :title="INFO_CAMPOS_VARIABLES">
                    <i class="ms-2 fas fa-md fa-info-circle col-1"></i>
                </div>
            </div>
            <table :id="`${idTable}`" class="table table-sm table-bordered table-striped table-hover">
                <thead class="table-top">
                    <tr class="text-center align-middle">
                        <th class="text-center">Campo</th>
                        <th class="text-center">Explicación</th>
                        <th class="text-center">Es Obligatorio</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-if="camposVariablesDisponibles.length === 0 || alertaTipoIdm == null"
                        class="no-data text-center">
                        <td class="text-center" colspan="100">{{ resultsMessage }}</td>
                    </tr>

                    <template v-for="cv in camposVariablesDisponibles">
                        <tr :key="cv.idm">
                            <td class="text-right align-middle">
                                {{ cv.descripcionColoquial }}
                            </td>
                            <td class="text-right align-middle">
                                {{ cv.explicacion }}
                            </td>
                            <td class="text-center align-middle">
                                <span v-show="cv.obligatorioEnCuerpo">
                                    <i class="fas fa-check text-success"></i>
                                </span>
                                <span v-show="!cv.obligatorioEnCuerpo">
                                    <i class="fas fa-times text-danger"></i>
                                </span>
                            </td>
                        </tr>
                    </template>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script>
import ajax from "@/common/ajaxWrapper";
import UiService from "@/common/uiService";

const NO_DATA_MESSAGE = "No hay datos";
const SEARCH_RESULTS_MESSAGE = "Se debe seleccionar Tipo de Alerta para ver los Campos Variables disponibles";
const GET_CAMPOS_VARIABLES_URL = baseUrl + "/api/Alertas/GetCamposVariablesByAlertaTipo";

export default {
    name: "camposVariables-list",
    components: {
    },
    props: {
        mode: String,
        grants: Object,
        alertaTipoIdm: Number
    },
    data: function () {
        return {
            camposVariablesDisponibles: [],
            idTable: "alertaCamposVariables",
            resultsMessage: SEARCH_RESULTS_MESSAGE,
            uiService: new UiService(),
            INFO_CAMPOS_VARIABLES: "Se deben indicar entre llaves en el HTML",
            idModalAlertaBody: "alerta_body_modal"
        };
    },
    async mounted() {
    },
    watch: {
        async alertaTipoIdm() {
            if (this.alertaTipoIdm == null) {
                this.clearList();
                this.resultsMessage = SEARCH_RESULTS_MESSAGE;
            }
            else await this.get();
        }
    },
    methods: {
        clearList() {
            this.camposVariablesDisponibles = [];
        },
        async get() {
            this.uiService.showSpinner(true);
            this.clearList();
            return new ajax()
                .get(GET_CAMPOS_VARIABLES_URL, { AlertaTipoIdm: this.alertaTipoIdm })
                .then((res) => {
                    this.camposVariablesDisponibles = res;
                    if (this.camposVariablesDisponibles.length == 0) this.resultsMessage = NO_DATA_MESSAGE
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
    },
};
</script>