<template>
    <select v-model="selected" class="form-select">
        <option :disabled="!showOption" :value="null"> {{ loc['Sin especificar'] }} </option>
        <option v-for="option in optionsData.filter(option => option.id == selected || !cultureSelectedList.includes(option.id))" :key="option.id" :value="option.id">{{option.code}}</option>
    </select>
</template>
<script>
    import loc from "@/common/commonLoc";
    export default {
        components: {},
        name: "culture-select",
        props: {
            value: Number,
            showoption: Boolean,
            cultureSelectedList: {
                type: Array,
                default: function () { return [] }
            },
            cultureSelectedListRestartFlag: Object
        },
        data: function () {
            return {
                optionsData: [],
                showOption: true,
                oldValue: 0,
                loc,
            };
        },
        computed: {
            selected: {
                get() { return this.value; },
                set(value) { this.onChange(value); },
            },
        },
        watch: {
            cultureSelectedListRestartFlag: function (newVal, oldVal) {
                this.restartCultureSelectedList()
            }
        },
        methods: {
            onChange(value, target) {
                let code = "";
                if (value === "") {
                    value = null;
                } else {
                    value = parseInt(value);
                    code = this.optionsData.find(c => c.id == value).code;
                    //code = target.options[target.selectedIndex].text;
                }
                // this.$emit("input", value);          
                // this.$emit("codeChange", code, value, this.oldValue);
                this.selectedCultureChange(true, value, this.oldValue, code);
            },

            selectedCultureChange(changed, newValue, oldValue, newCode) {
                this.$emit("input", newValue);
                this.$emit("selected-culture-change", changed, newValue, oldValue, newCode);
                this.oldValue = newValue;
            },
            restartCultureSelectedList() {
                this.selectedCultureChange(false, this.value);
            }
        },

        mounted() {
            this.showOption = this.showoption;
            this.selectedCultureChange(false, this.value);
            if (this.options == undefined) {
                fetch(baseUrl + "/api/cultures/culture/GetCultureOrganizations")
                    .then((response) => response.json())
                    .then((res) => {
                        res.forEach(element => {
                            element.value = element.cultureId
                            element.code = element.cultureCode
                        });
                        this.optionsData = res;
                    })
                    .catch((ex) => {
                        console.log("Error recuperardo idiomas: " + ex);
                    });
            }
        },
    };
</script>