<template>
    <select v-model="citySelected" :disabled="!enabled" class="form-select" @change="onChange($event.target.value)">
        <option :value="null">Sin especificar</option>
        <option v-for="(item, index) in citiesList" :key="index" :value="item.id">{{ item.name }}</option>
    </select>
</template>

<script>
import ajax from "@/common/ajaxWrapper";

const GET_CIUDADES_BY_PROVINCIA_SELECT_URL = `${baseUrl}/api/Global/Cities`;

export default {
    components: {},
    name: "city-select",
    props: {
        provinciaId: Number,
        value: Number,
        enabled: { type: Boolean, required: false, default: () => true },
    },
    data() {
        return {
            citySelected: null, // Valor predeterminado
            citiesList: []
        };
    },
    computed: {
        tipoSelected: {
            get() {
                return this.value == 0 ? 0 : this.value;
            },
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
        async fetchDomains() {
            if (this.provinciaId) {
                return new ajax()
                .get(GET_CIUDADES_BY_PROVINCIA_SELECT_URL, { provinceId: this.provinciaId })
                        .then((res) => {
                        if(res !== undefined){
                            this.citiesList = res
                        }
                    })
                    .catch((ex) => {
                        throw new Error('Error');
                    })
            }
        }
    },
    mounted() {
        // Llamar a la función fetchDomains cuando el componente se monte
        this.fetchDomains(); //ajax().get(GET_CIUDADES_BY_PROVINCIA_SELECT_URL, { provinceId: this.provinciaId })
    },
    watch: {
        value(newVal) {
            this.citySelected = newVal;
        },
        async provinciaId() {
            if(this.provinciaId != null){
                await this.fetchDomains();
            }
            else {
                this.citySelected = null
                this.citiesList = []
            }
        }
    }
};
</script>