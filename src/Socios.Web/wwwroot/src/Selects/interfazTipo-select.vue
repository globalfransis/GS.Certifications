<template>
    <select v-model="selected" class="form-select" @change="onChange($event.target.value)">
        <option :value="null" v-if="nullOption">Sin especificar</option>
        <option value="69">Exportación</option>
        <option value="73">Importación</option>
    </select>
</template>

<script>
export default {
    components: {},
    name: "interfazTipo-select",
    props: {
        value: String,
        nullOption: { type: Boolean, required: false, default: () => true },
    },
    data: function () {
        return {
            optionsData: [],
        };
    },
    computed: {
        selected: {
            get() {
                return this.value == 0 ? 0 : this.value;
            },
            set(value) { this.onChange(value); },

        },
    },
    methods: {
        onChange(newValue) {
            if (newValue === "") {
                newValue = null;
            }
            this.$emit("input", newValue);
        },
    },
    async mounted() {
    },
    watch: {
    }
};
</script>