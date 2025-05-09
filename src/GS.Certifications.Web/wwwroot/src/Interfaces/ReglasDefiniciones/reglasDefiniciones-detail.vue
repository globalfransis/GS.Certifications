<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">Definición de Regla de Interfaz {{ this.reglaDefinicion.id }}</p>
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Interfaz</label>
                            <interfaz-select disabled :sistemaIdm="reglaDefinicion.sistemaIdm" v-model="reglaDefinicion.interfazIdm" />
                        </div>

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Consecuencia</label>
                            <input disabled class="form-control" v-model="reglaDefinicion.consecuencia" />
                        </div>

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Descripción</label>
                            <input disabled class="form-control" v-model="reglaDefinicion.descripción" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12 d-flex justify-content-end mb-3 mt-3">
            <cancel-button @click="goBack">Volver</cancel-button>
        </div>
    </div>
</template>

<script>
import CancelButton from "@/components/forms/cancel-button.vue";
import ajax from "@/common/ajaxWrapper";
import ErrorBag from "@/common/ErrorBag";
import UiService from "@/common/uiService";
import interfazSelect from "@/selects/interfaz-select.vue";

import ReglaDefinicion from './ReglaDefinicion'

import commonMixin from '@/Common/Mixins/commonMixin';

const INTERFAZ_REGLAS_URL = baseUrl + "/api/Interfaces/Reglas";

export default {
    components: {
        CancelButton,
        interfazSelect
    },
    name: "reglasDefiniciones-detail",
    mixins: [commonMixin],
    data: function () {
        return {
            reglaDefinicion: new ReglaDefinicion(),
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
                .get(INTERFAZ_REGLAS_URL + `/${id}`)
                .then((res) => {
                    this.definicion = new ReglaDefinicion(res);
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