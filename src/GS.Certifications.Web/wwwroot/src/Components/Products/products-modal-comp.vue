<template>
    <div>
        <div class="modal fade"
             :id="idModal"
             tabindex="-1"
             :ref="idModal"
             :aria-labelledby="idModal"
             aria-hidden="true">
            <div class="modal-dialog modal-dialog-scrollable grid-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">{{modalText.ModalTitleText}}</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row justify-content-md-center">
                            <div class="form-group">

                                <table class="table table-bordered table-striped table-hover table-white fix-head" convert-to-datatable-manual no-paging :id="`${idTable}`">
                                    <thead class="table-top">
                                        <tr>
                                            <th scope="col" no-sort-datatable></th>
                                            <th scope="col">{{modalText.NameText}}</th>
                                            <th scope="col">{{modalText.CodeText}}</th>
                                            <th scope="col">{{modalText.ProductTypeText}}</th>
                                            <th scope="col">{{modalText.StockTypeText}}</th>
                                            <th scope="col">{{modalText.MeasureUnitText}}</th>
                                            <th scope="col">{{modalText.ProductGroupText}}</th>
                                            <th scope="col">{{modalText.ProductComboText}}</th>
                                            <th scope="col">{{modalText.EntireRoute}}</th>
                                            <th scope="col">{{modalText.StockFree}}</th>
                                        </tr>
                                    </thead>
                                    <tbody selectable-row v-if="availableItemsList.length > 0">
                                        <tr v-for="item in availableItemsList" :key="item.id" @click="selectRow(item)" role="button">
                                            <td scope="row" class="d-table-cell justify-content-center">
                                                <!--<input type="radio" name="selectedProduct" class="form-check-input " :value="item.selected" @click="selectValue(item)" />-->
                                                <!-- <input type="checkbox" name="selectedProduct" class="form-check-input check-radio-table" :checked="selectedProduct.id == item.id ? true : false" /> -->
                                                <input type="checkbox" name="selectedProduct" class="form-check-input check-radio-table" v-model="item.selected" />
                                            </td>
                                            <td>{{ item.name }}</td>
                                            <td>{{ item.code }}</td>
                                            <td>{{ item.productTypeName }}</td>
                                            <td>{{ item.stockTypeName }}</td>
                                            <td>{{ item.measureUnitName }}</td>
                                            <td>{{ item.productGroupName }}</td>
                                            <td><i :class="item.productCombo ? 'fa fa-check' : 'fa fa-times'"></i></td>
                                            <td><i :class="item.isEntireRoute ? 'fa fa-check' : 'fa fa-times'"></i></td>
                                            <td><i :class="item.isStockFree ? 'fa fa-check' : 'fa fa-times'"></i></td>
                                        </tr>
                                    </tbody>
                                </table>
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

    import UiService from '@/common/uiService';

    const getTravelProductsByOrganizationUrl = baseUrl + '/api/products/GetTravelProductsByOrganization';

    export default {
        name: "products-modal-comp",
        props: {
            idModal: String,
            existingProducts: Array,
            productsToRemove: {
                type: Array,
                default: []
            },
        },
        data: function () {
            return {
                uiService: new UiService(),
                availableItemsList: [],
                idTable: `__products_table_${this.idModal}`,
                modalText: '',
            };
        },
        created() {
            this.modalText = window.productsModalStrings;
        },
        mounted() {
            //Subscribe to boostrap jquery event to inform vue that the modal window is closed.
            this.uiService.setModalEventsCallbacks(this.$refs[this.idModal], this.loadData, this.modalClosed);
        },
        methods: {
            modalClosed: function () {
                this.availableItemsList = [];
                this.uiService.eraseDataTablesManual(this.idTable);
            },
            async loadData() {
                await fetch(getTravelProductsByOrganizationUrl)
                    .then((response) => response.json())
                    .then((res) => {
                        
                        for (var i = 0; i < res.length; i++) {
                            var p = res[i];
                            if (!this.productsToRemove.includes(p.id)) {
                                let product = new Products(p.id, p.name, p.code, p.marketable, p.productCombo, p.measureUnitIdm, p.measureUnitName,
                                    p.productTypeIdm, p.productTypeName, p.productGroupId, p.productGroupName, p.stockTypeIdm,
                                    p.stockTypeName, p.isEntireRoute, p.isStockFree)
                                product.selected = this.existingProducts.includes(p.id)
                                this.availableItemsList.push(product)
                            }
                        }
                    })
                    .then((res) => {
                        // if (this.availableItemsList.length > 0)
                        this.uiService.eraseDataTablesManual(this.idTable);
                        this.uiService.transformToDataTablesManual(this.idTable);
                    })
                    .catch((error) => {
                        showError(this.modalText.APICallErrorText);
                    });

            },
            selectRow(item) {
                item.selected = !item.selected;
            },
            informSelectedValues() {
                var selectedValues = [];

                for (var i = 0; i < this.availableItemsList.length; i++) {
                    if (this.availableItemsList[i].selected)
                        selectedValues.push(this.availableItemsList[i])
                }
                if (selectedValues.length > 0)
                    this.$emit("on-products-selected", selectedValues);
                //Close modal
                this.uiService.closeModalManual(this.idModal);
            }
        },
    };
    class Products {
        constructor(id, name, code, marketable, productCombo, measureUnitIdm, measureUnitName, productTypeIdm, productTypeName, productGroupId, productGroupName,
            stockTypeIdm, stockTypeName, isEntireRoute, isStockFree) {
            this.id = id;
            this.name = name;
            this.code = code;
            this.marketable = marketable;
            this.productCombo = productCombo;
            this.measureUnitIdm = measureUnitIdm;
            this.measureUnitName = measureUnitName;
            this.productTypeIdm = productTypeIdm;
            this.productTypeName = productTypeName;
            this.productGroupId = productGroupId;
            this.productGroupName = productGroupName;
            this.stockTypeIdm = stockTypeIdm;
            this.stockTypeName = stockTypeName;
            this.isEntireRoute = isEntireRoute;
            this.isStockFree = isStockFree;
            this.selected = false;
        }
    };
</script>

<style lang="scss" scoped>
    .grid-dialog {
        max-width: 1050px;
        max-height: 700px;
    }
</style>