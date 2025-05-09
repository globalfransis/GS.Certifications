<template>
    <select v-model="companySelected" class="form-select" @change="onChange($event.target.value)">
        <option :value="null">Sin especificar</option>
        <option v-for="(item, index) in companiesList" :key="index" :value="item.id">{{ item.name }}</option>
    </select>
</template>

<script>

import ajax from '@/common/ajaxWrapper';

const API_URL = baseUrl + "/api/proveedores/Companies";

export default {
    components: {},
    name: "userTypes-select",
    props: {
        value: Number,
    },
    data() {
        return {
            companySelected: null, // Valor predeterminado
            companiesList: []
        };
    },
    computed: {
        companySelected: {
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
            return await new ajax()
                .get(API_URL, null)
                .then((res) => {
                    if(res !== undefined){
                        this.companiesList = res
                    }
                })
                .catch((ex) => {
                    throw new Error('Error');
                })
        }
    },
    mounted() {
        // Llamar a la función fetchDomains cuando el componente se monte
        this.fetchDomains();
    },
    watch: {
        value(newVal) {
            this.companySelected = newVal;
        }
    }
};
</script>