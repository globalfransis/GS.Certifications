<template>
    <div>
        <div class="modal fade" data-bs-backdrop="static" :id="idModal" tabindex="-1" :ref="idModal" :aria-labelledby="idModal" aria-hidden="true">
            <div class="modal-dialog modal-dialog-scrollable grid-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">
                            {{modalText.ModalTitleText}}
                            <span v-if="selectedTransport != null">
                                - {{selectedTransport.name}}
                            </span>
                        </h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row justify-content-md-center">
                            <div class="form-group">
                                <div class="row d-flex justify-content-around">

                                    <div class="w-50">
                                        <h4>{{modalText.AvailablesText}}</h4>
                                        <table class="table table-bordered table-striped table-hover table-white fix-head">
                                            <thead class="table-top">
                                                <tr>
                                                    <th scope="col" class="col-1">{{modalText.CodeText}}</th>
                                                    <th scope="col" class="col-3">{{modalText.NameText}}</th>
                                                    <th scope="col" class="col-2">{{modalText.ProductTypeText}}</th>
                                                    <th scope="col" class="col-2">{{modalText.MeasureUnitText}}</th>
                                                    <th scope="col" class="col-2"></th>
                                                    <th scope="col" class="col-1"></th>
                                                </tr>
                                            </thead>
                                            <draggable v-model="availableItemsList" tag="tbody" group="people" :sort="false">
                                                <tr v-for="item in availableItemsList" :key="item.id" role="move">
                                                    <td>{{ item.stockGroupCode }}</td>
                                                    <td>{{ item.stockGroupName }}</td>
                                                    <td>{{ item.productTypeName }}</td>
                                                    <td>{{ item.measureUnitName }}</td>
                                                    <td><input class="form-control form-control-sm invisible" type="number" disabled /></td>
                                                    <td class="text-center">
                                                        <button type="button" @click="addToSelectedList(item)" class="transparent-buttons">
                                                            <i class="fas fa-arrow-right"></i>
                                                        </button>
                                                    </td>
                                                </tr>
                                            </draggable>
                                        </table>
                                    </div>

                                    <div class="w-50">
                                        <h4>{{modalText.SelectedText}}</h4>
                                        <table class="table table-bordered table-striped table-hover table-white fix-head">
                                            <thead class="table-top">
                                                <tr>
                                                    <th scope="col" class="col-1"></th>
                                                    <th scope="col" class="col-1">{{modalText.CodeText}}</th>
                                                    <th scope="col" class="col-3">{{modalText.NameText}}</th>
                                                    <th scope="col" class="col-2">{{modalText.ProductTypeText}}</th>
                                                    <th scope="col" class="col-2">{{modalText.MeasureUnitText}}</th>
                                                    <th scope="col" class="col-2">{{modalText.StockMaxText}}</th>
                                                </tr>
                                            </thead>
                                            <draggable v-model="selectedItemsList" tag="tbody" group="people" :sort="false">
                                                <tr v-for="item in selectedItemsList" :key="item.id" role="move">
                                                    <td class="text-center">
                                                        <button type="button" @click="removeFromSelectedList(item)" class="transparent-buttons">
                                                            <i class="fas fa-arrow-left"></i>
                                                        </button>
                                                    </td>
                                                    <td>{{ item.stockGroupCode }}</td>
                                                    <td>{{ item.stockGroupName }}</td>
                                                    <td>{{ item.productTypeName }}</td>
                                                    <td>{{ item.measureUnitName }}</td>
                                                    <td>
                                                        <input class="form-control form-control-sm" type="number" min="0"
                                                               v-model="item.capacity"
                                                               v-on:keydown="checkStockMaxValues($event)"
                                                               v-on:keydown.enter="$event.preventDefault()"
                                                               v-on:blur="parseInput(item, item.capacity)" />
                                                    </td>
                                                </tr>
                                            </draggable>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary y mr-2 btn-sm" @click="informSelectedValues()">
                            {{modalText.ConfirmText}}
                        </button>
                        <a href="#" class="btn btn-link btn-sm" data-bs-dismiss="modal">{{modalText.CloseText}}</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import draggable from "@/../lib/vue-draggable/dist/vuedraggable.umd";
    import { getProductTypesIdsByTransport } from "./helpers.vue";
    import UiService from "@/common/uiService";

    const GetStockGroupsByOrganizationUrl = baseUrl + "/api/stockgroups/GetStockGroupsByOrganization";

    export default {
        components: { draggable },
        name: "stock-groups-modal-comp",
        props: {
            idModal: String,
            getSelectedData: Function, //Need a function to return some data. Because boostrap modals are used (not Vue modals), we need to set the values and open de modal in the same event. The props dont update that way, so the function is needed.
            mode: String,
            frequency: Object
        },
        data: function () {
            return {
                uiService: new UiService(),
                availableItemsList: [],
                selectedItemsList: [],
                idTable: `__stock_groups_table_${this.idModal}`,
                modalText: "",
                pageMode: null,
            };
        },
        created() {
            this.modalText = window.stockGroupsModalStrings;
        },
        mounted() {
            //Suscribe to boostrap jquery event to inform vue that the modal window is closed.
            this.uiService.setModalEventsCallbacks(
                this.$refs[this.idModal],
                this.loadData,
                this.modalClosed
            );
            this.pageMode = this.mode;
        },
        methods: {
            modalClosed: function () {
                this.availableItemsList = [];
                this.selectedItemsList = [];
            },
            async loadData() {
                //this.selectedFrequency = this.getSelectedData();
                //this.selectedTransport = this.selectedFrequency.transport; //FrequencyModel
                //if (this.pageMode == "profile") {
                //    if (this.selectedFrequencyModel.transportProfileCapacities != null) {
                //        //Shallow Clone the array
                //        this.selectedFrequencyModelCapacities = [
                //            ...this.selectedFrequency.transportProfileCapacities,
                //        ]; //FrequencyModel
                //    } else {
                //        this.selectedFrequencyCapacities = null;
                //    }
                //} else {
                //if (this.selectedFrequency.frequencyCapacities != null) {
                //    //Shallow Clone the array
                //    this.selectedFrequencyCapacities = [
                //        ...this.selectedFrequencyModel.frequencyModelCapacities,
                //    ]; //FrequencyModel
                //} else {
                //    this.selectedFrequencyModelCapacities = null;
                //}
                //}
                //var selectedF = this.selectedFrequency;

                var allowedProductTypesIds = getProductTypesIdsByTransport(this.frequency.transport);

                await fetch(GetStockGroupsByOrganizationUrl)
                    .then((response) => response.json())
                    .then((res) => {
                        var queryList = res
                            .filter(fc => allowedProductTypesIds.includes(fc.productTypeIdm))


                        // If there are frequencyCapacities the queryList has to be filtered, to avoid repeated elements.
                        //If not all items are availables.
                        if (this.frequency.frequencyCapacities != null) {
                            this.selectedItemsList = this.frequency.frequencyCapacities.map(fc => ({
                                id: fc.stockGroupId,
                                capacity: fc.capacity,
                                stockGroup: {
                                    id: fc.stockGroupId,
                                    productTypeIdm: fc.stockGroupProductTypeIdm,
                                    productTypeName: fc.productTypeName,
                                    measureUnitIdm: fc.measureUnitIdm,
                                    measureUnitName: fc.measureUnitName,
                                    code: fc.stockGroupCode,
                                    name: fc.stockGroupName
                                }
                            }));
                            this.availableItemsList = queryList.filter(function (itm) {
                                return !this.selectedItemsList.some((f) => {
                                    return f.id === itm.id;
                                }).map(function (sg) {
                                    return {
                                        id: null,
                                        capacity: 0,
                                        stockGroup: {
                                            id: sg.id,
                                            productTypeIdm: sg.productTypeIdm,
                                            productTypeName: sg.productTypeName,
                                            measureUnitIdm: sg.measureUnitIdm,
                                            measureUnitName: sg.measureUnitName,
                                            code: sg.code,
                                            name: sg.name
                                        }
                                    }
                                });
                            });
                        } else {
                            this.availableItemsList = queryList;
                        }
                    })
                    .catch((error) => {
                        showError(this.modalText.APICallErrorText);
                    });
            },
            addToSelectedList(item) {
                this.selectedItemsList.push(item);
                this.availableItemsList.splice(this.availableItemsList.indexOf(item), 1);
            },
            removeFromSelectedList(item) {
                this.availableItemsList.push(item);
                this.selectedItemsList.splice(this.selectedItemsList.indexOf(item), 1);
            },
            informSelectedValues() {
                this.$emit(
                    "on-stock-groups-selected",
                    this.selectedFrequencyCapacities
                );

                //Close modal
                this.uiService.closeModalManual(this.idModal);
            },
            checkStockMaxValues(event) {
                this.uiService.preventNoIntegerNumbers(event);
            },
            parseInput(item, inputMax) {
                var value = parseFloat(inputMax);
                if (!isNaN(value)) item.capacity = value;
                else item.capacity = 0;
            },
        },
        computed: {
            dragOptions() {
                return {
                    animation: 0,
                    group: "description",
                    disabled: false,
                    ghostClass: "ghost",
                };
            },
        },
    };
</script>

<style lang="scss" scoped>
    .grid-dialog {
        max-width: 1400px;
        max-height: 700px;
    }

    .button {
        margin-top: 35px;
    }

    .flip-list-move {
        transition: transform 0.5s;
    }

    .no-move {
        transition: transform 0s;
    }

    .ghost {
        opacity: 0.5;
        background: #c8ebfb;
    }

    .list-group {
        min-height: 20px;
    }

    .list-group-item {
        cursor: move;
    }

    .list-group-item i {
        cursor: pointer;
    }
</style>