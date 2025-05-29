<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">{{loc["Definición de Interfaz"]}} {{ this.definicion.id }}</p>
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{loc["Interfaz"]}}</label>
                            <interfaz-select disabled :sistemaIdm="definicion.sistemaIdm" v-model="definicion.interfazIdm" />
                        </div>

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{loc["Número Orden Campo"]}}</label>
                            <input disabled class="form-control" v-model="definicion.numeroOrdenCampo" />
                        </div>

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{loc["Descripción"]}}</label>
                            <input disabled class="form-control" v-model="definicion.descripcion" />
                        </div>

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{loc["Campo Externo"]}}</label>
                            <input disabled class="form-control" v-model="definicion.campoExterno" />
                        </div>

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{loc["Campo Middleware"]}}</label>
                            <input disabled class="form-control" v-model="definicion.campoMiddleware" />
                        </div>

                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{loc["Explicación"]}}</label>
                            <textarea disabled class="form-control" v-model="definicion.explicacion" />
                        </div>

                    </div>
                </div>
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

import InterfazDefinicion from './InterfazDefinicion'

import commonMixin from '@/Common/Mixins/commonMixin';

import loc from "@/common/commonLoc.js"

const INTERFAZ_CAMPOS_URL = baseUrl + "/api/Interfaces/Campos";

export default {
    components: {
        CancelButton,
        interfazSelect
    },
    name: "definiciones-detail",
    mixins: [commonMixin],
    data: function () {
        return {
            loc,
            definicion: new InterfazDefinicion(),
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
                .get(INTERFAZ_CAMPOS_URL + `/${id}`)
                .then((res) => {
                    this.definicion = new InterfazDefinicion(res);
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