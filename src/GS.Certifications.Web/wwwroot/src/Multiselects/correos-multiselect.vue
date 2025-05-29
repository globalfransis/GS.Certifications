<template>
    <!-- El atributo "taggable" nos permite agregar nuevas opciones al Multiselect.
    Aprovechamos esta función para poder dar de alta nuevos items, de modo que
    al agregar la opción, también agregamos el valor a la lista de correos.
    La idea es que las "options" y los correos siempre sean equivalentes -->
    <multiselect :disabled="!editMode" @open="preventAutocomplete" :hide-selected="true" :searchable="true"
        @keydown.tab="create" @change="setNuevoCorreo" @keydown="onKeydown" :loading="isLoading" :id="multiselectId"
        :showNoResults="false" :internal-search="false" :ref="multiselectId" :close-on-select="false"
        openDirection="top" :taggable="true" tagPosition="bottom" tag-placeholder="Presiona Enter o Tab para agregar"
        v-model="correos" :options="options" :multiple="true" :placeholder="placeHolder"
        deselectLabel="Click o Enter para Remover" :clearOnSelect="true" selectLabel="Click o Enter para Seleccionar"
        selectedLabel="Seleccionado" :showNoOptions="false" label="value" track-by="value" @tag="create"
        @remove="remove">
    </multiselect>
</template>

<script>

import Multiselect from 'vue-multiselect'
import UiService from "@/common/uiService";

import loc from "@/common/commonLoc.js"

export default {
    components: {
        Multiselect
    },
    name: "correos-multiselect",
    props: {
        value: Array,
        editMode: null,
        id: String
    },
    data: function () {
        return {
            loc,
            options: [],
            multiselectId: `correosMultiselect-${this.id}`,
            placeHolder: loc["Ingresar correos"],
            isLoading: false,
            uiService: new UiService(),
            nuevoCorreo: ""
        };
    },
    computed: {
        correos: {
            // Recibimos por v-model un array de strings, pero para mostrar correctamente los valores en este input
            // es necesario que los items sean objetos
            get() {
                this.options = [...this.value.map(correo => { return { value: correo } })]
                return this.value.map(correo => { return { value: correo } });
            },
            set() {
            },
        },
    },
    methods: {
        // Esto es necesario para prevenir el autocompletado del browser
        preventAutocomplete() {
            document.getElementById(this.multiselectId).setAttribute("autocomplete", "off");
        },
        setNuevoCorreo(correo) {
            this.nuevoCorreo = correo;
        },
        // Al eliminar un tag del input, borramos el item de la lista de correos y emitimos el input de la lista como array de strings
        remove(item) {
            const index_options = this.options.indexOf(item);
            this.options = this.options.splice(index_options, 1);

            const index_correos = this.correos.indexOf(item);
            this.correos = this.correos.splice(index_correos, 1);

            this.$emit("input", this.correos.map(correo => correo.value));
        },
        onKeydown(event) {
            this.nuevoCorreo = event.target.value;
            
            var code = (event.keyCode ? event.keyCode : event.which);
            if (code == 9 || code == 13) { // Se presionó tab o enter
                this.setNuevoCorreo("");
            }
        },
        // Ante el evento "tag", si lo que se ingresó es una dirección de correo válida,
        // la misma será agregada a los correos y a las opciones, y se emitirá input
        // como array de strings
        create(input) {
            if (ValidateEmail(input)) {
                if (!this.options.some(correo => correo.value === event.target.value)) {
                    const nuevoCorreo = {
                        value: input,
                    }
                    this.options.push(nuevoCorreo)
                    this.correos.push(nuevoCorreo)
                    this.$emit("input", this.correos.map(correo => correo.value));
                }
            } else {
                this.uiService.showMessageErrorAndFocus("La dirección de correo electrónico ingresada no es válida");
            }
            this.setNuevoCorreo("");
        }
    },
    mounted() {
        if (this.value && this.value.length > 0) {
            this.options = [...this.value]
        }

        // Se define un listener para el text input del multiselect para que
        // al presionar Tab se comporte como el Enter
        const multiselectTextInput = document.getElementById(this.multiselectId);
        multiselectTextInput.addEventListener('keydown', (event) => {
            this.nuevoCorreo = event.target.value;
            if (event.keyCode === 9 || event.keyCode === 13) { // Tab o Enter
                if (event.target.value) {
                    event.preventDefault();
                    this.create(event.target.value);
                    this.$refs[this.multiselectId].search = ""
                }
            }
        });

        multiselectTextInput.addEventListener('focus', (event) => {
            this.$el.querySelector('.multiselect__tags-wrap').classList.add('tags-wrap--focused');
        });

        // Queremos que al hacer blur, si se ingresó un correo en el input, se agregue también a la lista
        multiselectTextInput.addEventListener('blur', (event) => {
            event.preventDefault();
            this.$el.querySelector('.multiselect__tags-wrap').classList.remove('tags-wrap--focused');
            if (!this.options.some(correo => correo.value === event.target.value) && this.nuevoCorreo) {
                this.create(this.nuevoCorreo);
            }
        });

        // Manejamos el evento change para ir guardando en nuevoCorreo el correo ingresado
        // Esto es para que al hacer blur se agregue este valor a la lista
        multiselectTextInput.addEventListener('change', (event) => {
            event.preventDefault();
            this.setNuevoCorreo(event.target.value);
        });

    },
    watch: {
    }
};

function ValidateEmail(mail) {
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(mail)) return true;
    return false;
}

</script>

<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>

<style>
.multiselect__content-wrapper {
    border: 0px !important;
    background-color: transparent !important;
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
</style>