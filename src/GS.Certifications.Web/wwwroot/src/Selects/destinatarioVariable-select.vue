<template>
    <select v-model="selected" class="form-select" @change="onChange($event.target.value)">
        <option :value="null">{{loc["Sin especificar"]}}</option>
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
    name: "destinatarioVariable-select",
    props: {
        value: Number,
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
            if (newValue === "") {
                newValue = null;
            } else {
                newValue = parseInt(newValue);
            }
            this.$emit("input", newValue);
        },
    },
    mounted() {
        new ajax()
            .get(baseUrl + "/api/Alertas/GetDestinatarioVariables")
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