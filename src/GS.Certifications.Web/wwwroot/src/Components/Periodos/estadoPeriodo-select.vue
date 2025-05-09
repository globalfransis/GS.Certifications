<template>
    <select v-model="selected" class="form-select" :disabled="estadoDeshabilitado">
        <option :value="null" v-if="nullOption">Sin especificar</option>
        <option 
            v-for="option in optionsData" 
            :key="option.idm" 
            :value="option.idm"
            :disabled="option.idm === 4 && deshabilitarOpcion4" 
        >
            {{ option.descripcion }}
        </option>
    </select>
</template>

<script>
import ajax from "@/common/ajaxWrapper";

export default {
    name: "estadoPeriodo-select",
    props: {
        value: Number,
        nullOption: { type: Boolean, required: false, default: () => true },
        estadoDeshabilitado: Boolean, // Para deshabilitar el select completo
        deshabilitarOpcion4: Boolean  // Nueva prop para bloquear opción 4
    },
    data() {
        return {
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
            },
        },
    },
    methods: {
        async getAsync() {
            await new ajax()
                .get(baseUrl + "/api/Configuration/Periodos/EstadosPeriodos")
                .then((res) => {
                    this.optionsData = res;
                })
                .then(() => {
                    const val = this.value;
                    if (!this.optionsData.some(o => o.idm === val)) {
                        this.$emit("input", null);
                    } else {
                        this.$emit("input", val);
                    }
                })
                .catch((ex) => {
                    console.error("Error al cargar los estados de periodo", ex);
                });
        },
        onChange(newValue) {
            if (newValue) {
                newValue = parseInt(newValue);
            } else {
                newValue = null;
            }
            this.$emit("input", newValue);
        },
    },
    async mounted() {
        await this.getAsync();
    }
};
</script>