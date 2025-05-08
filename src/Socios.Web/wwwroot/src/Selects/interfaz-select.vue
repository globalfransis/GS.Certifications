<template>
    <select v-model="selected" class="form-select">
        <option :value="null" v-if="nullOption">Sin especificar</option>
        <option v-for="option in optionsData" :key="option.idm" :value="option.idm">
            {{ option.descripcionMiddleware }}
        </option>
    </select>
</template>

<script>

import ajax from "@/common/ajaxWrapper";

export default {
    components: {},
    name: "interfaz-select",
    props: {
        value: Number,
        sistemaIdm: null,
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
                .get(baseUrl + "/api/Interfaces", { sistemaIdm: this.sistemaIdm })
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
        if (this.sistemaIdm != null) {
            await this.getAsync();
        }
    },
    watch: {
        sistemaIdm: async function (newV, oldV) {
            if (this.sistemaIdm != null) {
                await this.getAsync();
            }
            else {
                this.optionsData = [];
                this.$emit("input", null);
            }
        }
    }
};
</script>