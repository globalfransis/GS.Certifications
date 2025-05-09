<template>
    <select v-model="tipoCuentaSelected" class="form-select" @change="onChange($event.target.value)">
        <option :value="null">Sin especificar</option>
        <option v-for="(item, index) in tiposCuentasList" :key="index" :value="item.idm">{{ item.nombre }}</option>
    </select>
</template>

<script>

import ajax from '@/common/ajaxWrapper';

const API_URL = baseUrl + "/api/proveedores/TiposCuentas";

export default {
    components: {},
    name: "tiposCuentas-select",
    props: {
        value: Number,
    },
    data() {
        return {
            tiposCuentasList: []
        };
    },
    computed: {
        tipoCuentaSelected: {
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
                        this.tiposCuentasList = res
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
            this.tipoCuentaSelected = newVal;
        }
    }
};
</script>