<template>
    <select v-model="selected" class="form-select">
        <option :value="null" v-if="nullOption">{{loc["Sin especificar"]}}</option>
        <option v-for="option in optionsData" :key="option.idm" :value="option.idm">
            {{ loc[option.descripcion] }}
        </option>
    </select>
</template>

<script>

import ajax from "@/common/ajaxWrapper";

import loc from "@/common/commonLoc.js"

export default {
    components: {},
    name: "solicitudCertificacionEstado-select",
    props: {
        value: Number,
        nullOption: { type: Boolean, required: false, default: () => true },
    },
    data: function () {
        return {
            loc,
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
                .get(baseUrl + "/api/Certificaciones/Solicitudes/Estados")
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