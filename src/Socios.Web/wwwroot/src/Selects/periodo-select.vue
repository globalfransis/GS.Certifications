<template>
    <select v-model="selected" class="form-select">
        <option :value="null" v-if="nullOption">Sin especificar</option>
        <option v-for="option in optionsData" :key="option.id" :value="option.id">
            {{ option.vigencia }}
        </option>
    </select>
</template>

<script>

import ajax from "@/common/ajaxWrapper";

export default {
    components: {},
    name: "periodo-select",
    props: {
        value: Number,
        nullOption: { type: Boolean, required: false, default: () => true },
    },
    data: function () {
        return {
            optionsData: [],
        };
    },
    computed: {
        selected: {
            get() {
                return this.value;
            },
            set(value) { this.onChange(value); },

        },
    },
    methods: {
        async getAsync() {

            const sortOrders = [
                { orderPropertyName: "Estado.Descripcion", orderDirection: "0" },
                { orderPropertyName: "FechaFin", orderDirection: "1" }
            ];

            const jsonString = JSON.stringify(sortOrders);
            const encodedData = encodeURIComponent(jsonString);

            await new ajax()
                .get(baseUrl + `/api/Configuration/Periodos?SortOrders=${encodedData}`)
                .then((res) => {
                    this.optionsData = res ? res.list : [];
                })
                .then(() => {
                    const val = this.value;
                    if (!this.optionsData.some(o => o.id === val)) {
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
            this.$emit("selected", this.optionsData.find(o => o.id === newValue));
        },
    },
    async mounted() {
        await this.getAsync();
    },
    watch: {
    }
};
</script>