<template>
    <select class="form-select" v-model="selected">
        <option :value="null" v-if="nullOption">Sin especificar</option>
        <option v-for="option, index in optionsData" :key="index" :value="option.idm">
            {{ option.descripcion }}
        </option>
    </select>
</template>

<script>

    import ajax from "@/common/ajaxWrapper";

    export default {
        components: {},
        name: "interfazEstado-select",
        props: {
            value: Number,
            interfazId: null,
            nullOption: { type: Boolean, required: false, default: () => true }
        },
        data: function () {
            return {
                optionsData: []
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
            async get() {
                await new ajax()
                .get(baseUrl + `/api/Interfaces/${this.interfazId}/Estados`)
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
            if (this.interfazId != null) {
                await this.get();
            }
        },
        watch: {
            interfazId: async function(newV, oldV) {
                if (this.interfazId != null) {
                    await this.get();
                }
                else {
                    this.optionsData = [];
                    this.$emit("input", null);
                }
            }
        }
    };
</script>