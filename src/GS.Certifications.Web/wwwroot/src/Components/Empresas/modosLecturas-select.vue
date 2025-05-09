<template>
    <select v-model="modoLecturaSelected" class="form-select">
        <option :value="null" v-if="nullOption">Sin especificar</option>
        <option v-for="option in 
        modosLecturasList.filter(m => !modoLecturaEstaUsada(m.idm) || m.idm == value)" :key="option.idm" :value="option.idm">
            {{ option.descripcion }}
        </option>
    </select>
</template>

<script>

import ajax from '@/common/ajaxWrapper';

const API_URL = baseUrl + "/api/proveedores/ModosLecturas";

export default {
    components: {},
    name: "modosLecturas-select",
    props: {
        listaModosLecturasUsadas: {type: Array, required: false, default: () => []},
        value: Number,
        nullOption: { type: Boolean, required: false, default: () => true },
    },
    data() {
        return {
            modosLecturasList: []
        };
    },
    computed: {
        modoLecturaSelected: {
            get() {
                return this.value;
            },
            set(value) { this.onChange(value); },
        },
    },
    methods: {
        modoLecturaEstaUsada(modoLecturaIdm){
            return this.listaModosLecturasUsadas.includes(modoLecturaIdm);
        },
        onChange(newValue) {
            if (newValue) {
                newValue = parseInt(newValue);
            } else {
                newValue = null;
            }
            this.$emit("input", newValue);
            this.$emit("modoLecturaSelected", this.modosLecturasList.find(o => o.idm === newValue));
        },
        async getAsync() {
            await new ajax()
                .get(API_URL)
                .then((res) => {
                    this.modosLecturasList = res
                })
                .then(() => {
                    const val = this.value;
                    if (!this.modosLecturasList.some(o => o.idm === val)) {
                        this.$emit("input", null);
                    } else {
                        this.$emit("input", val);
                    }
                })
                .catch((ex) => {

                });
        },
    },
    mounted() {
        this.getAsync();
    },
};
</script>