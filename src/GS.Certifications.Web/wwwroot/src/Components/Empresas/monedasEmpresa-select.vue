<template>
    <select v-model="monedaSelected" class="form-select">
        <option :value="null" v-if="nullOption">Sin especificar</option>
        <option v-for="option in 
        monedasList.filter(m => !monedaEstaUsada(m.idm) || m.idm == value)" :key="option.idm" :value="option.idm">
            {{ option.name }}
        </option>
    </select>
</template>

<script>

import ajax from '@/common/ajaxWrapper';

const API_URL = baseUrl + "/api/proveedores/Monedas";

export default {
    components: {},
    name: "monedasEmpresa-select",
    props: {
        listaMonedasUsadas: {type: Array, required: false, default: () => []},
        value: Number,
        nullOption: { type: Boolean, required: false, default: () => true },
    },
    data() {
        return {
            monedasList: []
        };
    },
    computed: {
        monedaSelected: {
            get() {
                return this.value;
            },
            set(value) { this.onChange(value); },
        },
    },
    methods: {
        monedaEstaUsada(currencyId){
            return this.listaMonedasUsadas.includes(currencyId);
        },
        onChange(newValue) {
            if (newValue) {
                newValue = parseInt(newValue);
            } else {
                newValue = null;
            }
            this.$emit("input", newValue);
            this.$emit("monedaSelected", this.monedasList.find(o => o.idm === newValue));
        },
        async getAsync() {
            await new ajax()
                .get(API_URL)
                .then((res) => {
                    this.monedasList = res
                })
                .then(() => {
                    const val = this.value;
                    if (!this.monedasList.some(o => o.idm === val)) {
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