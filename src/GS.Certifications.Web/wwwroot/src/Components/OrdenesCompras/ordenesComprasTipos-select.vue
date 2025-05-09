<template>
    <select v-model="ordenCompraTipoSelected" class="form-select">
        <option :value="null" v-if="nullOption">Sin especificar</option>
        <option v-for="option in ordenesComprasTiposList" :key="option.id" :value="option.id">
            {{ option.nombre }}
        </option>
    </select>
</template>

<script>

import ajax from "@/common/ajaxWrapper";

const API_URL = baseUrl + "/api/proveedores/OrdenesComprasTipos/ListaTiposOrdenesCompras";

export default {
    components: {},
    name: "ordenesComprasTipos-select",
    props: {
        value: Number,
        nullOption: { type: Boolean, required: false, default: () => true },
    },
    data: function () {
        return {
            ordenesComprasTiposList: [],
        };
    },
    computed: {
        ordenCompraTipoSelected: {
            get() {
                return this.value;
            },
            set(value) { this.onChange(value); },

        },
    },
    methods: {
        async getAsync() {
            await new ajax()
                .get(API_URL)
                .then((res) => {
                    this.ordenesComprasTiposList = res;
                })
                .then(() => {
                    const val = this.value;
                    if (!this.ordenesComprasTiposList.some(o => o.id === val)) {
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