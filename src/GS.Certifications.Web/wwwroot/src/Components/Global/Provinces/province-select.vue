<template>
    <select v-model="provinceSelected" :disabled="!enabled" class="form-select" @change="onChange($event.target.value)">
        <option :value="null">{{loc["Sin especificar"]}}</option>
        <option v-for="(item, index) in provincesList" :key="index" :value="item.id">{{ item.name }}</option>
    </select>
</template>

<script>
import ajax from "@/common/ajaxWrapper";

import loc from "@/common/commonLoc.js"

const GET_PROVINCIAS_BY_PAIS_SELECT_URL = `${baseUrl}/api/Global/Provinces`;

export default {
    components: {},
    name: "provincia-select",
    props: {
        paisId: Number,
        value: Number,
        enabled: { type: Boolean, required: false, default: () => true },
    },
    data() {
        return {
            loc,
            provinceSelected: null, // Valor predeterminado
            provincesList: []
        };
    },
    computed: {
        tipoSelected: {
            get() {
                return this.value == 0 ? 0 : this.value;
            },
        },
    },
    methods: {
        onChange(newValue) {
            if (newValue === "") {
                newValue = null;
            } else {
                newValue = parseInt(newValue);
            }
            this.$emit("input", newValue);
        },
        async fetchDomains() {
            if (this.paisId) {
                return new ajax()
                    .get(GET_PROVINCIAS_BY_PAIS_SELECT_URL, { countryId: this.paisId })
                        .then((res) => {
                        if(res !== undefined){
                            this.provincesList = res
                        }
                    })
                    .catch((ex) => {
                        throw new Error('Error');
                    })
            }
        }
    },
    mounted() {
        // Llamar a la función fetchDomains cuando el componente se monte
        this.fetchDomains();
    },
    watch: {
        value(newVal) {
            this.provinceSelected = newVal;
        },
        async paisId() {
            if(this.paisId != null){
                await this.fetchDomains();
            }
            else {
                this.provinceSelected = null
                this.provincesList = []
            }
        }
    }
};
</script>