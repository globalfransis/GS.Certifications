<template>
    <select v-model="selected" class="form-select">
        <option :value="null" v-if="nullOption">{{loc["Sin especificar"]}}</option>
        <option v-for="option in optionsData" :key="option.idm" :value="option.idm">
            {{ option.descripcion }}
        </option>
    </select>
</template>

<script>

import ajax from "@/common/ajaxWrapper";

import loc from "@/common/commonLoc.js"

export default {
    components: {},
    name: "sistema-select",
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
        onChange(newValue) {
            if (newValue) {
                newValue = parseInt(newValue);
            }
            this.$emit("input", newValue);
        },
    },
    async mounted() {
        await new ajax()
            .get(baseUrl + "/api/Sistemas")
            .then((res) => {
                this.optionsData = res;
            })
            .catch((ex) => {

            });
    },
    watch: {
    }
};
</script>