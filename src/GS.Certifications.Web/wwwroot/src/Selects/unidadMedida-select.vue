<template>
    <select v-if="!disabled" v-model="selected" class="form-select">
        <option :value="null" v-if="nullOption">Sin especificar</option>
        <option v-for="option in optionsData" :key="option.idm" :value="option.idm">
            {{ option.codigoAFIP }}
        </option>
    </select>
    <div v-else>{{optionsData.find(o => o.idm == selected) ? optionsData.find(o => o.idm == selected).descripcion : "-"}}</div>
</template>

<script>

import ajax from "@/common/ajaxWrapper";

export default {
    components: {},
    name: "unidadMedida-select",
    props: {
        value: Number,
        disabled: { type: Boolean, required: false, default: () => false },
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
                .get(baseUrl + "/api/UnidadMedidas")
                .then((res) => {
                    this.optionsData = res;
                })
                .then(() => {
                    const val = this.value;
                    if (!this.optionsData.some(o => o.idm === val)) {
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