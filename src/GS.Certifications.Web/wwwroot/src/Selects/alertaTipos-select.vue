<template>
    <select v-model="selected" class="form-select">
        <option :value="{ idm: null, descripcion: '', aceptaDestinatarioVariables: null }">{{loc["Sin especificar"]}}</option>
        <option v-for="option in optionsData" :key="option.idm" :value="option">
            {{ option.descripcion }}
        </option>
    </select>
</template>

<script>
import ajax from "@/common/ajaxWrapper";

import loc from "@/common/commonLoc.js"

export default {
    name: "alertaTipos-select",
    props: {
        value: {
            type: Object,
            default: () => ({ idm: null, descripcion: '', aceptaDestinatarioVariables: null })
        }
    },
    data() {
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
            set(value) {
                this.onChange(value);
            }
        }
    },
    methods: {
        onChange(newValue) {
            if (newValue === "") {
                newValue = null;
            }
            console.log(typeof newValue)
            console.log(newValue)
            this.$emit("input", newValue);
        }
    },
    mounted() {
        new ajax()
            .get(baseUrl + "/api/Alertas/GetAlertaTipos")
            .then((res) => {
                this.optionsData = res;
            })
            .catch((ex) => {
                // Manejo de errores
                console.error(ex);
            });
    }
};
</script>
