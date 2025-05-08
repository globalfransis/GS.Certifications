<template>
    <div class="modal" data-bs-backdrop="static" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.2);"
        :id="idModal" tabindex="-1" :ref="idModal" :aria-labelledby="idModal">
        <div class="modal-dialog modal-dialog-scrollable grid-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Eventos</h4>
                    <button type="button" class="btn-close" @click="modalClosed" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <ul class="nav nav-tabs mt-4" role="tablist">
                        <li class="nav-item" v-for="(e, index) in emails" :id="`notif-event-${e.replace('@', '')}`"
                            role="presentation">
                            <button :class="index == 0 ? 'nav-link active' : 'nav-link'" @click="showEventsFor(e)"
                                :id="`notif-event-${e.replace('@', '')}-tab`" data-bs-toggle="tab"
                                :data-bs-target="`#notif-event-${e.replace('@', '')}`" type="button" role="tab"
                                :aria-controls="`notif-event-${e.replace('@', '')}`" :aria-selected="index == 0">{{ e
                                }}</button>
                        </li>
                    </ul>
                    <div class="tab-content" id="eventTabContent">
                        <div class="tab-pane fade show active" :id="`notif-event-${selected}`" role="tabpanel"
                            :aria-labelledby="`notif-event-${selected}-tab`">

                            <table :id="`${idTable}`" class="table table-bordered table-striped table-hover mt-4">
                                <thead class="table-top">
                                    <tr class="text-center align-middle">
                                        <th>
                                            Fecha
                                        </th>
                                        <th>
                                            Estado
                                        </th>
                                        <th>Motivo</th>
                                        <th>Intentos</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-if="selectedEventList.length === 0 && !loading" class="no-data text-center">
                                        <td class="text-center" colspan="100">{{ resultsMessage }}</td>
                                    </tr>
                                    <template v-for="ev in selectedEventList">
                                        <tr :key="ev.id">
                                            <td class="text-start align-middle">
                                                {{ ev.fecha | uidatetime }}
                                            </td>
                                            <td class="text-start align-middle">
                                                {{ ev.estado }}
                                            </td>
                                            <td class="text-start align-middle">
                                                {{ ev.razon ? ev.razon : "-" }}
                                            </td>
                                            <td class="text-end align-middle">
                                                {{ ev.intentos }}
                                            </td>
                                        </tr>
                                    </template>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <cancel-button type="button" class="btn btn-outline-primary btn-sm" @click="modalClosed">
                        Cerrar</cancel-button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import UiService from "@/common/uiService";
import CancelButton from "@/components/forms/cancel-button.vue";
import ajax from "@/common/ajaxWrapper";

const API_URL = baseUrl + "/api/Notifications";

const NO_DATA_MESSAGE = "No hay datos";

export default {
    components: {
        CancelButton
    },
    name: "notificationEvents-modal",
    props: {
        idModal: String,
        notificacionId: Number
    },
    data: function () {
        return {
            uiService: new UiService(),
            resultsMessage: NO_DATA_MESSAGE,
            eventsByEmail: {},
            loading: false,
            selectedEventList: []
        };
    },
    computed: {
        emails() {
            return Object.keys(this.eventsByEmail);
        },

    },
    async mounted() {
        $(`#${this.idModal}`).on('show.bs.modal', await this.getAsync);
        $(`#${this.idModal}`).on('hide.bs.modal', this.clear);
    },
    methods: {
        async getAsync() {
            this.uiService.showSpinner(true);
            this.loading = true;
            return await new ajax()
                .get(API_URL + `/${this.notificacionId}/Events`)
                .then((res) => {
                    this.eventsByEmail = res;
                    this.selectedEventList = Object.keys(this.eventsByEmail).length > 0 ? this.eventsByEmail[this.emails[0]] : [];
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                    this.loading = false;
                })
        },
        showEventsFor(email) {
            this.selectedEventList = this.eventsByEmail[email];
        },
        clear() {
            this.eventsByEmail = {};
            this.selectedEventList = [];
        },
        modalClosed: function () {
            $(`#${this.idModal}`).modal("toggle");
            this.clear();
        },
    },
};
</script>

<style lang="scss" scoped>
.grid-dialog {
    max-width: 1000px;
    max-height: 700px;
    // position:fixed;
}
</style>