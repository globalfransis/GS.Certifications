<template>
    <select v-model="selected" class="form-select">
        <option :value="null" v-if="nullOption">Sin especificar</option>
        <option v-for="option in optionsData" :key="option.idm" :value="option.alicuota">
            {{ option.alicuota }}%
        </option>
    </select>
</template>

<script>

import ajax from "@/common/ajaxWrapper";

export default {
    components: {},
    name: "impuestoAlicuota-select",
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
            await new ajax()
                .get(baseUrl + "/api/Impuestos?IVA=true")
                .then((res) => {
                    this.optionsData = res;
                })
                .then(() => {
                    const val = this.value;
                    if (!this.optionsData.some(o => o.alicuota === val)) {
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
                newValue = parseFloat(newValue);
            } else {
                newValue = null;
            }
            this.$emit("input", newValue);
            this.$emit("selected", this.optionsData.find(o => o.idm === newValue));
        },
    },
    async mounted() {
        await this.getAsync();
    },
    watch: {
    }
};
</script>