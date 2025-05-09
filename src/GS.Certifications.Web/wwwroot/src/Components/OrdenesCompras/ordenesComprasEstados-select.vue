<template>
    <select v-model="ordenCompraEstadoSelected" class="form-select">
        <option :value="null" v-if="nullOption">Sin especificar</option>
        <option v-for="option in filteredOrdenesComprasEstadosList" :key="option.idm" :value="option.idm">
            {{ option.nombre }}
        </option>
    </select>
</template>

<script>

import ajax from "@/common/ajaxWrapper";

const API_URL = baseUrl + "/api/proveedores/OrdenesComprasEstados";

export default {
    components: {},
    name: "ordenesComprasEstados-select",
    props: {
        value: Number,
        nullOption: { type: Boolean, required: false, default: () => true },
        hideAnulada: { type: Boolean, required: false, default: () => false },
    },
    data: function () {
        return {
            ordenesComprasEstadosList: [],
        };
    },
    computed: {
        ordenCompraEstadoSelected: {
            get() {
                return this.value;
            },
            set(value) { this.onChange(value); },
        },
        filteredOrdenesComprasEstadosList() {
            if (this.hideAnulada) {
                return this.ordenesComprasEstadosList.filter(option => option.nombre !== 'Anulada');
            }
            return this.ordenesComprasEstadosList;
        },
    },
    methods: {
        async getAsync() {
            await new ajax()
                .get(API_URL)
                .then((res) => {
                    this.ordenesComprasEstadosList = res;
                })
                .then(() => {
                    const val = this.value;
                    if (!this.ordenesComprasEstadosList.some(o => o.idm === val)) {
                        this.$emit("input", null);
                    } else {
                        this.$emit("input", val);
                    }
                })
                .catch((ex) => {

                });
        },
        onChange(newValue) {
            if (newValue) {
                newValue = parseInt(newValue);
            } else {
                newValue = null;
            }
            this.$emit("input", newValue);
        },
    },
    async mounted() {
        await this.getAsync();
    },
    watch: {
    }
};
</script>