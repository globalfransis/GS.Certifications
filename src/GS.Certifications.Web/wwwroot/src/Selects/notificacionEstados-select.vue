<template>
    <select v-model="selected" class="form-select" @change="onChange($event.target.value)">
        <option :value="null">Sin especificar</option>
        <option v-for="option in optionsData" :key="option.idm" :value="option.idm">
            {{ option.descripcion }}
        </option>
    </select>
</template>

<script>

import ajax from "@/common/ajaxWrapper";

export default {
    components: {},
    name: "notificacionEstados-select",
    props: {
        value: Number,
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
    },
    mounted() {
        new ajax()
            .get(baseUrl + "/api/Notifications/GetNotificacionEstados")
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