<template>
    <select v-model="selected" class="form-select" @change="onChange($event.target.value)">
        <option :value="null">{{loc["Sin especificar"]}}</option>
        <option v-for="option in optionsData" :key="option.ubicacionId" :value="option.ubicacionId">
            {{ option.ubicacionDescripcion }}
        </option>
    </select>
</template>

<script>

import ajax from "@/common/ajaxWrapper";

import loc from "@/common/commonLoc.js"

export default {
    components: {},
    name: "alertaTipoUbicaciones-select",
    props: {
        value: Number,
        alertaTipoIdm: Number
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
        getData() {
            if (this.alertaTipoIdm != null) {
                new ajax()
                    .get(baseUrl + "/api/Alertas/GetAlertaTipoUbicaciones", { AlertaTipoIdm: this.alertaTipoIdm })
                    .then((res) => {
                        this.optionsData = res;
                    })
                    .catch((ex) => {

                    });
            }
        }
    },
    mounted() {
        this.getData();
    },
    watch: {
        alertaTipoIdm() {
            this.getData()
        }
    }
};
</script>