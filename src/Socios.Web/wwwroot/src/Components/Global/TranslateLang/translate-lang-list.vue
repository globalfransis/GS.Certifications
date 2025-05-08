<template>
<div class="card">
    <div class="card-body">
        <div class="d-flex justify-content-between align-items-center mb-3">
            <p class="h5 m-0">Traducciones</p>
            <button type='button' v-if="!mode.isDetail" class="btn btn-outline-primary btn-sm" @click.prevent="addItem()"><b><i class="fas fa-plus"></i> Agregar</b></button>
        </div>
        <div class="form-group">
            <div>
                <!-- Table -->
                <table no-paging id="fixed-table" class="table table-bordered table-striped table-hover table-white">
                    <thead class="table-top table-top-translate text-center align-middle">
                        <tr class="text-center align-middle">
                        <th class="col-1 text-center align-middle">#</th>
                        <th class="col-2 text-center align-middle">Idioma</th>
                        <template v-for="(propertyName, index) in propertyNameList">
                            <th :key="index" class="text-center align-middle">{{propertyName.value}}</th>
                        </template>
                        <th class="text-center align-middle col-1" v-if="!mode.isDetail">Eliminar</th>
                        </tr>
                    </thead>
                    <tbody class="text-center align-middle">
                        <tr v-if="copyTranslateLangList.length == 0" class="no-data">
                            <td colspan="100">No hay datos</td>
                        </tr>
                        
                        <template v-for="(translateLang, index) in copyTranslateLangList.filter(item => !item.deleted)">
                            <translate-lang-row-list :key="translateLang.id" :index="index" :translateLang="translateLang" :propertyNameList="propertyNameList" :mode="mode" :cultureSelectedList="cultureSelectedList" :cultureSelectedListRestartFlag="cultureSelectedListRestartFlag" :errors="errorBag" :translateName="translateName" @on-delete-item="deleteItem" @on-change-item="changeItem" @selected-culture-change="selectedCultureChange" />
                        </template>
                    </tbody>
                </table>

            </div>
        </div>
    </div>
</div>
</template>

<script>
import CrudMode from "@/common/CrudMode";
import translateLangRowList from "./translate-lang-row-list.vue";
import ErrorBag from "@/common/ErrorBag";

export default {
    components: {
        translateLangRowList
    },

    name: "translate-lang-list",

    props: {
        mode: CrudMode,
        translateLangList: Array,
        propertyNameList: Array,
        errorBag: ErrorBag,
        translateName: String,
        disableAutoRestart: Boolean
    },

    data: function () {
        return {
            copyTranslateLangList: [],
            AvailableId: Number,
            cultureSelectedList: [],
            cultureSelectedListRestartFlag: {}
        };
    },

    computed: {},

    watch: {
        mode: function (newVal, oldVal) {
            if (!this.disableAutoRestart) {this.initList();}
        },

        translateLangList: function (newVal, oldVal) {
            if (!this.disableAutoRestart) {this.initList();}
            this.$emit("on-change", this.copyTranslateLangList);
        }
    },

    methods: {
        addItem() {
            let newObject = {
                id: this.AvailableId,
                cultureId: 0,
                cultureCode: "",
                new: true,
                deleted: false,
                edited: false
            }
            this.propertyNameList.forEach(element => {
                newObject[`${element.key}`] = "";
            });
            this.copyTranslateLangList.push(newObject);
            this.AvailableId -= 1;
            this.$emit("on-change", this.copyTranslateLangList);
        },

        deleteItem(item) {
            this.removeCultureSelectedToList(item.cultureId);
            if (item.new) {
                this.copyTranslateLangList.splice(this.copyTranslateLangList.indexOf(item), 1)
                //this.copyTranslateLangList = this.copyTranslateLangList.filter(value => value.id != item.id);
            } else {
                item.deleted = true;
            }
            this.$emit("on-change", this.copyTranslateLangList);
        },

        changeItem(item) {
            if (!item.new) item.edited = true;
            this.$emit("on-change", this.copyTranslateLangList);
        },

        initList() {
            this.AvailableId = 0;
            this.cultureSelectedList = [];
            this.cultureSelectedListRestartFlag = {};
            //Copia temporal de la lista que voy a utlizar.
            //Modifico la misma antes de asignarla a la lista definitiva para que diparen los hooks de VUE con las nuevas propiedades.

            // let tempCopyTranslateLangList = this.translateLangList.slice();
            // tempCopyTranslateLangList.forEach(element => {
                //     element['new'] = false;
            //     element['deleted'] = false;
            //     element['edited'] = false;
            // });

            let tempCopyTranslateLangList = [];
            this.translateLangList.forEach(element => {
                let copyTrad = {};
                Object.assign(copyTrad, element);
                copyTrad['new'] = false;
                copyTrad['deleted'] = false;
                copyTrad['edited'] = false;
                tempCopyTranslateLangList.push(copyTrad);
            });
            this.copyTranslateLangList = tempCopyTranslateLangList;
        },

        getChangedList() {
            return this.copyTranslateLangList;
        },

        addCultureSelectedToList(cultureId) {
            if (cultureId) this.cultureSelectedList.push(cultureId);
        },

        removeCultureSelectedToList(cultureId) {
            if (cultureId) this.cultureSelectedList.splice(this.cultureSelectedList.indexOf(cultureId), 1);
        },

        selectedCultureChange(newCultureId, oldcultureId) {
            this.removeCultureSelectedToList(oldcultureId);
            this.addCultureSelectedToList(newCultureId);
        },
    },

    mounted() {
        this.initList();
    },
};
</script>

<style>
.table-top-translate th {
    background-color: #0059A3 !important;
    color: white;
    padding: 5px;
}
</style>
