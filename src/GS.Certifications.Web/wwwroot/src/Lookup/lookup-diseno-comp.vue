<template>
    <div>
        <h4>{{ id }}</h4>
        <input type="hidden" :name="name" @input="handleInput" />
        <span class="form-label">País</span>
        <div class="" @keyup="showSearch" tabindex="0">
            <div class="flex-column">
                <div class="d-inline-flex">
                    <div class="input-group lookup" style="padding: 0">
                        <input type="text"
                               class="form-control"
                               placeholder="Código"
                               v-on:blur="searchByCode"
                               v-model="code" />

                        <div class="dropdown">
                            <ul class="dropdown-menu" :show="filteredData">
                                <li class="dropdown-item"
                                    :data-id="item.id"
                                    v-for="item in filteredData"
                                    :key="item.id">
                                    {{ item.name }}
                                </li>
                            </ul>
                        </div>
                        <span class="form-control" tabindex="-1">{{ text }}</span>
                        <!-------------------------------->
                        <button class="btn btn-outline-secondary dropdown-toggle" type="button"
                                id="filtersButton" data-bs-toggle="dropdown" aria-expanded="false">
                            <i class="fas fa-search"></i>
                        </button>
                        <div class="dropdown-menu lookup-dropdown p-2" aria-labelledby="filtersButton">
                            <div class="d-flex justify-content-between">
                                <div class="dropdown">
                                    <select>
                                        <option>Ale</option>
                                        <option>Ale2</option>
                                        <option>Ale3</option>
                                    </select>
                                    <button class="btn btn-secondary dropdown-toggle btn-sm" type="button"
                                            id="dropdownMenuButton" data-bs-toggle="dropdown" aria-expanded="false">
                                        Continente
                                    </button>
                                    <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                                        <li><a class="dropdown-item" href="#">Action</a></li>
                                        <li><a class="dropdown-item" href="#">Another action</a></li>
                                        <li><a class="dropdown-item" href="#">Something else here</a></li>
                                    </ul>
                                </div>
                                <div class="">
                                    <button type="button" class="btn btn-primary btn-sm">Buscar</button>
                                </div>
                            </div>
                            <div class="dropdown-divider"></div>

                            <table class="table table-borderless caption-top">
                                <!-- <caption class="d-inline-block mx-3">Codigo</caption>
            <caption class="d-inline-block mx-5">Pais</caption> -->
                                <tbody>
                                    <tr>
                                        <th scope="row">
                                            <div class="form-check">
                                                <input class="form-check-input" type="radio" name="flexRadioDefault"
                                                       id="flexRadioDefault1">ARG
                                            </div>
                                        </th>
                                        <td>ARGENTINA</td>
                                    </tr>
                                    <tr>
                                        <th scope="row">
                                            <div class="form-check">
                                                <input class="form-check-input" type="radio" name="flexRadioDefault"
                                                       id="flexRadioDefault1">BRA
                                            </div>
                                        </th>
                                        <td>BRASIL</td>
                                    </tr>
                                    <tr>
                                        <th scope="row">
                                            <div class="form-check">
                                                <input class="form-check-input" type="radio" name="flexRadioDefault"
                                                       id="flexRadioDefault1">URY
                                            </div>
                                        </th>
                                        <td colspan="2">URUGUAY</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <!------------------------------------------------>
                        <!--<span class="input-group-text"
          data-bs-toggle="modal"
          :data-bs-target="`#${idModal}`">
        <i class="oi oi-magnifying-glass" accesskey="s"></i>
    </span>-->
                    </div>
                </div>
            </div>
        </div>
       
        <lookup-search-comp :idModal="idModal"
                            @on-value-selected="updateSelected"
                            ref="modalmodal">
            <template #filterSlot>
                <slot name="lkpfilter" :filter="filter">
                    <button class="btn btn-primary" @click="filter">Filtrar</button>
                </slot>
            </template>
            <slot>
                <h1>Te piso la tablita</h1>
            </slot>
            <!-- <input class="form-control" type="text" /> -->
        </lookup-search-comp>
    </div>
</template>

<script>
    import lookupSearchComp from "./lookup-search-comp.vue";

    export default {
        components: { lookupSearchComp },
        name: "lookup-diseno-comp",
        mounted() {
            this.loadAllData();
        },
        props: {
            name: String,
            idModal: String,
        },
        data: function () {
            return {
                id: null,
                code: "",
                text: "",
                allData: [],
            };
        },
        computed: {
            filteredList() {
                return this.allData.filter((item) => {
                    return item.name.toLowerCase().indexOf(this.code.toLowerCase()) > -1;
                });
            },
        },
        methods: {
            searchByCode: function () {
                var i = 0;
                fetch(
                    "https://restcountries.eu/rest/v2/alpha/" +
                    this.code +
                    "?fields=alpha2Code;alpha3Code;name;translations;currencies"
                )
                    .then((response) => response.json())
                    .then((res) => {
                        this.id = i++;
                        this.code = this.code.toUpperCase();
                        this.text = `${res.translations["es"]} (${res.currencies[0].code})`;
                    })
                    .catch(() => {
                        this.code = "";
                        this.text = "";
                    });
            },
            handleInput(e) {
                this.$emit("input", this.id);
            },
            process: function () {
                alert("procesado");
                this.showModal = false;
            },
            updateSelected(item) {
                this.id = item.selectedId;
                this.code = item.selectedCode;
                this.text = item.selectedText;
            },
            showSearch(e) {
                if (e.key === "F4") {
                    var modalElement = document.getElementById(this.idModal);
                    var modal = new bootstrap.Modal(modalElement);
                    modal.show();
                }
            },
            filterModal(searchString) {
                this.$refs.modalmodal.filterByText(searchString);
            },
            loadAllData() {
                var i = 0;
                fetch(
                    "https://restcountries.eu/rest/v2/all?fields=alpha2Code;alpha3Code;name;translations;currencies"
                )
                    .then((response) => response.json())
                    .then((res) => {
                        this.allData = res.map(function (r) {
                            return {
                                id: i++,
                                code: r.alpha2Code,
                                name: r.translations["es"] || r.name,
                            };
                        });
                    })
                    .catch(() => {
                        alert("catch");
                    });
            },
        },
    };
</script>

<style>

    .lookup-dropdown {
        width: 500px;
        transform: none !important;
    }
    /*.lookup {
        width: 600px;
    }*/
    /*
    .lookup-code {
    }

    .lookup-text {
    } */
</style>