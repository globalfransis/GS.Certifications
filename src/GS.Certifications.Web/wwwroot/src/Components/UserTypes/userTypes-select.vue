<template>
    <select class="form-select" @change="onChange($event.target.value)">
        <option :value="null">Sin especificar</option>
        <option v-for="(item, index) in typesList" :key="index" :value="item.idm">{{ item.description }}</option>
    </select>
</template>

<script>

import ajax from '@/common/ajaxWrapper';
import loc from "@/common/commonLoc.js"

const API_URL = baseUrl + "/api/Types";

export default {
    components: {},
    name: "userTypes-select",
    props: {
        value: Number,
    },
    data() {
        return {
            typesList: [],
            loc
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
                        this.typesList = res
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