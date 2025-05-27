<template>
    <select class="form-select" @change="onChange($event.target.value)">
        <option :value="null">Sin especificar</option>
        <option v-for="(item, index) in domainsList" :key="index" :value="item.idm">{{ item.name }}</option>
    </select>
</template>

<script>

import ajax from '@/common/ajaxWrapper';

const API_URL = baseUrl + "/api/Domains";

export default {
    components: {},
    name: "userDomainF-select",
    props: {
        value: Number,
    },
    data() {
        return {
            domainsList: []
        };
    },
    computed: {
        
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
                        this.domainsList = res
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
            this.selected = newVal;
        }
    }
};
</script>