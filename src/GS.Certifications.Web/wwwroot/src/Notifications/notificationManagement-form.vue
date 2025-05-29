<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">{{loc["Reenviar Notificación"]}}</p>
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">

                        <div class="col mb-4">
                            <label>{{loc["Destinatarios"]}}</label>
                            <correos-multiselect id="destinatarios" :editMode="true" v-model="notificacion.destinatarios" />
                            <span class="text-danger field-validation-error" data-valmsg-for="destinatarios"
                                data-valmsg-replace="true">
                                {{ errorBag.get("destinatarios") }}
                            </span>
                        </div>

                        <div class="col mb-4">
                            <label>{{loc["CC"]}}</label>
                            <correos-multiselect id="cc" :editMode="true" v-model="notificacion.conCopia" />
                            <span class="text-danger field-validation-error" data-valmsg-for="conCopia"
                                data-valmsg-replace="true">
                                {{ errorBag.get("conCopia") }}
                            </span>
                        </div>

                        <div class="col mb-4">
                            <label>{{loc["CCO"]}}</label>
                            <correos-multiselect id="cco" :editMode="true" v-model="notificacion.conCopiaOculta" />
                            <span class="text-danger field-validation-error" data-valmsg-for="conCopiaOculta"
                                data-valmsg-replace="true">
                                {{ errorBag.get("conCopiaOculta") }}
                            </span>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-12 d-flex justify-content-end mb-3 mt-3">
                <div>
                    <accept-button @click="saveNotificacion">{{loc["Reenviar"]}}</accept-button>
                    <cancel-button @click="goBack">{{loc["Cancelar"]}}</cancel-button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import AcceptButton from "@/components/forms/accept-button.vue";
import CancelButton from "@/components/forms/cancel-button.vue";
import ajax from "@/common/ajaxWrapper";
import ErrorBag from "@/common/ErrorBag";
import UiService from "@/common/uiService";
import correosMultiselect from "@/Multiselects/correos-multiselect";

import loc from "@/common/commonLoc.js"

const GET_NOTIFICACION_URL = baseUrl + "/api/Notifications/GetNotificacion";
const SAVE_NOTIFICACION_URL = baseUrl + "/api/Notifications/CreateNotificacionReenvio";

export default {
    components: {
        AcceptButton,
        CancelButton,
        correosMultiselect
    },

    name: "notificationManagement-form",

    data: function () {
        return {
            loc,
            notificacion: {
                id: null,
                destinatarios: [],
                conCopia: [],
                conCopiaOculta: []
            },
            errorBag: new ErrorBag(),
            uiService: new UiService(),
            errorKey: 0
        };
    },
    async mounted() {
        if (this.$route.params.notificacionId) {
            await this.loadNotificacion(this.$route.params.notificacionId);
        }
    },
    methods: {
        async loadNotificacion(id) {
            this.uiService.showSpinner(true);
            return new ajax()
                .get(GET_NOTIFICACION_URL, { Id: id })
                .then((res) => {
                    this.notificacion = res;
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        async saveNotificacion() {
            this.errorBag.clear();
            this.uiService.showSpinner(true)
            return new ajax()
                .post(SAVE_NOTIFICACION_URL,
                    {
                        Id: this.notificacion.id,
                        Destinatarios: this.notificacion.destinatarios,
                        ConCopia: this.notificacion.conCopia,
                        ConCopiaOculta: this.notificacion.conCopiaOculta
                    },
                    { errorBag: this.errorBag })
                .then((res) => {
                    if (!this.errorBag.hasErrors()) {
                        this.uiService.showMessageSuccess("Operación confirmada")
                        this.notificacion = res;
                        this.goBack()
                    } else {
                        this.uiService.showMessageError("Operación rechazada")
                    }
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        goBack() {
            this.$router.push({ name: "list" , query: {fromDetail: true}});
        },
    },
};
</script>