<template>
    <select v-model="selectedValue" class="form-control" @input="$emit('input', $event.target.selectedOptions[0]._value)">
        <option v-bind:disabled="!showOption" :value="{id:0,text:''}"> {{loc['Sin especificar']}}</option>
        <option v-for="option in optionsData" :key="option.id" v-bind:value="{ id: option.id, text: option.cultureCode }"> {{ option.cultureCode }} </option>
    </select>
</template>
<script>
    import loc from "@/common/commonLoc";
    export default {
        components: {},
        name: "cultureorganization-select",
        props: {
            value: {},
            options: Array,
            showoption: Boolean
        },
        data: function () {
            return {
                optionsData: this.options,
                showOption: null,
                selectedValue: Object.assign({}, this.value),
                loc
            };
        },
        // computed: {
        //     selected() {
        //         return this.selectedValue;
        //     },
        // },
        methods: {
            async findAndEmitSelected() {
                let sel;
                if (this.value.id != null || (this.value.text != null && this.value.text != "")) {
                    sel = this.optionsData.find (
                        c => c.id == this.value.id || c.cultureCode == this.value.text)
                } else {
                    sel = this.optionsData.find(
                        c => c.selected
                    );
                }
                if (sel) {
                    this.selectedValue = { id: sel.id, text: sel.cultureCode };
                    this.$emit("input", this.selectedValue);
                }
            }
        },
        watch: {},
        mounted() {
            this.showOption = this.showoption;
            if (this.options == undefined) {
                fetch(baseUrl + "/api/cultures/culture/GetCultureOrganizations")
                    .then((response) => response.json())
                    .then(res => {
                        this.optionsData = res;
                        // let sel = this.optionsData.find(
                        //     c => c.selected
                        // );

                        this.findAndEmitSelected();
                    })
                    .catch((ex) => {
                        console.log("Error recuperando culturas: " + ex);
                    });
            }
        },
    };
</script>