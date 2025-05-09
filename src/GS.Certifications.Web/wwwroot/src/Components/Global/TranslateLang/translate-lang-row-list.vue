<template>
<tr :class="stateRowClass">
    <td>{{index+1}}</td>
    <!-- 
    <td>{{translateLang['cultureId']}}</td>
    <td>{{translateLang['cultureCode']}}</td> 
    -->
    <td>
        <culture-select ref="cultureSelect" :cultureSelectedList="cultureSelectedList" :showoption="true" :disabled="mode.isDetail" :cultureSelectedListRestartFlag="cultureSelectedListRestartFlag" :class="errors.get(`${translateName}[${index}].CultureId`) ? 'is-invalid' : ''" class="form-control form-select translateInput" v-model="translateLang['cultureId']" @selected-culture-change="selectedCultureChange" data-toggle="tooltip" :title="errors.get(`${translateName}[${index}].CultureId`)"></culture-select>
    </td>
    <template v-for="(propertyName, index) in propertyNameList">
        <td :key="index">
            <input type="text" :class="errors.get(`${translateName}[${index}].${propertyName.key.charAt(0).toUpperCase() + propertyName.key.slice(1)}`) ? 'is-invalid' : ''" class="form-control translateInput" v-model="translateLang[propertyName.key]" :disabled="mode.isDetail" v-on:change="$emit('on-change-item', translateLang)" data-toggle="tooltip" :title="errors.get(`${translateName}[${index}].${propertyName.key.charAt(0).toUpperCase() + propertyName.key.slice(1)}`)">
        </td>
    </template>
    <td v-if="!mode.isDetail">
        <inline-delete-button v-if="!mode.isDetail" v-on:click="$emit('on-delete-item', translateLang)"></inline-delete-button>
    </td>
</tr>
</template>

<script>
import cultureSelect from "@/selects/culture-select";
import inlineDeleteButton from "@/components/forms/inline-delete-button.vue";
import CrudMode from "@/common/CrudMode";

export default {
    components: {
        cultureSelect,
        inlineDeleteButton,
        CrudMode
    },

    name: "translate-lang-row-list",

    props: {
        index: Number,
        translateLang: Object,
        propertyNameList: Array,
        mode: CrudMode,
        cultureSelectedList: Array,
        cultureSelectedListRestartFlag: Object,
        errors: Object,
        translateName: String,
    },

    data: function () {
        return {};
    },

    mounted() {},

    methods: {
        selectedCultureChange(changed, newCultureId, oldCultureId, newCultureCode) {
            this.$emit("selected-culture-change", newCultureId, oldCultureId);
            if (changed) {
                this.translateLang['cultureCode'] = newCultureCode;
                this.$emit('on-change-item', this.translateLang);
            }
        },
    },

    computed: {
        stateRowClass() {
            if (this.translateLang.new) return 'table-info';
            if (this.translateLang.edited) return 'table-warning';
            if (this.translateLang.deleted) return 'table-danger';
            return '';
        }
    },

    watch: {},
};
</script>

<style>
.translateInput {
    width: 100%;
    padding: 5px;
    margin: 0px;
}
</style>
