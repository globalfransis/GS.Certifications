Vue.component('treeselect', VueTreeselect.Treeselect);

const branchPriority = "BRANCH_PRIORITY";
const leafPriority = "LEAF_PRIORITY";
//const optionType = "Option";
//const grantType = "Grant";

//var optionsData = {
//    selectedValues: [],
//    name: '',
//    description: '',
//    valueConsistsOf: branchPriority,
//    type: undefined,
//    children: [],
//}

new Vue({
    el: "#app",
    props: {
    },
    data() {
        return {
            selectedValues: [],
            name: '',
            description: '',
            groupOwnerName: '',
            valueConsistsOf: branchPriority,
            type: undefined,
            children: [],
        };
    },
    created() {
        this.getDataFromModel();
    },
    methods: {
        getDataFromModel() {
            this.name = $("#Name").val();
            this.description = $("#Description").val();
            this.groupOwnerName = $("#GroupOwnerName").val();
            var jsonTree = $("#TreeOptions").val();
            this.children = JSON.parse(jsonTree);
            let values = $("#SelectedValues").val();
            if (values != null && values != "")
                this.selectedValues = values.split(",");
        },
    }
})