<template>
    <select v-model="countrySelected" class="form-select" @change="onChange($event.target.value)">
        <option :value="null">{{loc["Sin especificar"]}}</option>
        <option v-for="(item, index) in countriesList" :key="index" :value="item.id">{{ item.name }}</option>
    </select>
</template>

  <script>

  import ajax from "@/common/ajaxWrapper";

  import loc from "@/common/commonLoc.js"

  const GET_PAISES_SELECT_URL = `${baseUrl}/api/Global/Countries`;

  export default {
    components: {},
    name: "provincia-select",
    props: {
        value: Number,
    },
    data() {
        return {
            loc, 

            countrySelected: null, // Valor predeterminado
            countriesList: []
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
            return await new ajax()
                .get(GET_PAISES_SELECT_URL, null)
                .then((res) => {
                    if(res !== undefined){
                        this.countriesList = res
                    }
                })
                .catch((ex) => {
                    throw new Error('Error');
                })
        }
    },
    mounted() {
        // Llamar a la función fetchDomains cuando el componente se monte
        this.fetchDomains();
    },
    watch: {
        value(newVal) {
            this.countrySelected = newVal;
        }
    }
  };
  </script>