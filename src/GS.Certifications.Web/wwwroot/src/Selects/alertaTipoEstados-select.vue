<template>
    <select v-model="selected" class="form-select" @change="onChange($event.target.value)">
        <option :value="null">Sin especificar</option>
        <option v-for="option in optionsData" :key="option.estadoId" :value="option.estadoId">
            {{ option.estadoDescripcion }}
        </option>
    </select>
</template>

<script>

import ajax from "@/common/ajaxWrapper";

export default {
    components: {},
    name: "alertaTipoEstados-select",
    props: {
        value: Number,
        alertaTipoIdm: Number
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
                    .get(baseUrl + "/api/Alertas/GetAlertaTipoEstados", { AlertaTipoIdm: this.alertaTipoIdm })
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