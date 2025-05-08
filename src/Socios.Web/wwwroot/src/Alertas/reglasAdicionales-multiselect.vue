<template>
    <!-- Algunas aclaraciones:
    1) track-by indica la propiedad por la cual se diferencia cada item
    2) multiple es para permitir selección múltiple
    3) label es la propiedad de cada item que se va mostrar como display de las opciones -->
    <multiselect :hide-selected="true" :searchable="true" :loading="isLoading" :id="multiselectId" :disabled="!enabled"
        :internal-search="false" :ref="multiselectId" :close-on-select="false" tag-position="bottom" openDirection="top"
        @close="clearSearch" v-model="selectedValues" :options="options" :multiple="true" :placeholder="placeHolder"
        deselectLabel="Click o Enter para Remover" selectLabel="Click o Enter para Seleccionar"
        selectedLabel="Seleccionado" :showNoOptions="false" label="descripcionDetalle" track-by="idm" @search-change="asyncGet">
        <template slot="noResult">{{NO_RESULTS_MESSAGE}}</template>
    </multiselect>
    <!-- Puse "clearSearch" porque quiero que se borre la búsqueda al cerrar la lista de opciones.
    Esto es para evitar que pueda seleccionar alarmaZonas que fueron eliminadas
    Y es algo particular de este caso de uso -->
</template>

<script>

import ajax from "@/common/ajaxWrapper";
import Multiselect from 'vue-multiselect'

const NO_RESULTS_MESSAGE = "No se encontraron resultados"
const SELECT_REGLAS_PLACEHOLDER = "Seleccionar reglas adicionales"

export default {
    components: {
        Multiselect
    },
    name: "reglasAdicionales-multiselect",
    props: {
        value: Array,
        alertaTipoIdm: Number,
        enabled:{ type: Boolean, required: false, default: () => true },
    },
    data: function () {
        return {
            options: [],
            multiselectId: "reglasAdicionalesMultiselect",
            placeHolder: SELECT_REGLAS_PLACEHOLDER,
            isLoading: false,
            NO_RESULTS_MESSAGE: NO_RESULTS_MESSAGE
        };
    },
    computed: {
        selectedValues: {
            get() {
                return this.value;
            },
            set(values) {
                // si quisieramos que el componente devuelva, por ejemplo, una lista de ids
                // el emit tiene que ser de value con un map que seleccione los ids de cada item
                // de modo que devuelva una lista de ids
                this.$emit("input", values);
            },
        },
    },
    methods: {
        clearSearch() {
            this.options = [];
        },
        async asyncGet(query) {
            this.isLoading = true
            return await new ajax()
                .get(baseUrl + "/api/Alertas/GetReglasAdicionalesByAlertaTipo", { alertaTipoIdm: this.alertaTipoIdm, search: query })
                .then((res) => {
                    this.options = res;
                })
                .catch((ex) => {
                    console.log(ex)
                })
                .finally(() => {
                    this.isLoading = false
                })
                ;
        },
    },
    async mounted() {
        if (this.value && this.value.length > 0) {
            this.options = [...this.value]
        }

        const multiselectTextInput = document.getElementById(this.multiselectId);

        multiselectTextInput.addEventListener('focus', async (event) => {
            this.$el.querySelector('.multiselect__tags-wrap').classList.add('tags-wrap--focused');
            await this.asyncGet();
        });

        multiselectTextInput.addEventListener('blur', (event) => {
            this.$el.querySelector('.multiselect__tags-wrap').classList.remove('tags-wrap--focused');
        });

    },
    // watch: {
    //     async alertaTipoIdm() {
    //         else this.placeHolder = SELECT_REGLAS_PLACEHOLDER
    //     }
    // },
};
</script>
<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>
<style>
.multiselect__content-wrapper {
    border: 0px !important;
    /* position: relative; */
}

.tags-wrap--focused {
    position: absolute;
    top: calc(100% + 5px);
    left: 0;
    width: 100%;
    /* top: calc(100% + 8px);
  left: 0;
  right: 0;
  z-index: 1; */
}

.multiselect__content {
  background: rgb(255, 255, 255);
  color: #444;
  font-size: 0.9rem;
  border: 1px solid lightgray;
}
</style>
