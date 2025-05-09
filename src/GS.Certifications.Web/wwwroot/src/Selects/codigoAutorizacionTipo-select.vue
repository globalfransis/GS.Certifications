<template>
    <select v-model="selected" class="form-select">
        <option :value="null" v-if="nullOption">Sin especificar</option>
        <option :value="1">CAE</option>
        <option :value="2">CAEA</option>
        <option :value="3">CAI</option>
    </select>
</template>

<script>

// import ajax from "@/common/ajaxWrapper";

export default {
    components: {},
    name: "codigoAutorizacionTipo-select",
    props: {
        value: Number,
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
                return this.value;
            },
            set(value) { this.onChange(value); },

        },
    },
    methods: {
        // async getAsync() {
        //     await new ajax()
        //         .get(baseUrl + "/api/CodigoAutorizaciones/Tipos")
        //         .then((res) => {
        //             this.optionsData = res;
        //         })
        //         .then(() => {
        //             const val = this.value;
        //             if (!this.optionsData.some(o => o.idm === val)) {
        //                 this.$emit("input", null);
        //             } else {
        //                 this.$emit("input", val);
        //             }
        //         })
        //         .catch((ex) => {

        //         });
        // },
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
        // await this.getAsync();
    },
    watch: {
    }
};
</script>