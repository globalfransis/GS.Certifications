<template>
    <div class="root">
        <h1>6. Tablas - Custom Vue Editable Grid y Datatable</h1>

        <h4>6.1. Custom Vue Editable Grid</h4>
        <br />
        <div id="app">
            <custom-veg class="grid"
                        ref="grid"
                        id="mygrid"
                        :column-defs="columnDefs"
                        :row-data="rows"
                        :page-count=10
                        row-data-key='id'
                        @cell-updated="cellUpdated"
                        @row-selected="rowSelected"
                        @multiple-selected='multipleSelected'
                        @link-clicked="linkClicked"
                        @context-menu="contextMenu">
                <template v-slot:header>
                    Ejemplo de Custom Vue Editable Grid
                    <a href="#" @click.prevent="removeCurrentRow" v-if="selectedRow" class="ml-1">Remove current row</a>
                </template>
                <template v-slot:header-r>
                    Total rows: {{ rows.length }}
                </template>
            </custom-veg>
        </div>
        <br />

        <h4>6.2. DataTable</h4>
        <br />
        <div class="col-12 table-responsive">
            <table class="table table-bordered table-striped table-hover" width="100%" convert-to-datatable>
                <thead class="table-top">
                    <tr>
                        <th scope="col">Name</th>
                        <th scope="col">Gender</th>
                        <th scope="col">Company</th>
                        <th scope="col">Balance</th>
                        <th scope="col" no-sort-datatable>Id</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item in rows">
                        <td>{{item.name}}</td>
                        <td>{{item.gender}}</td>
                        <td>{{item.company}}</td>
                        <td>{{item.balance}}</td>
                        <td>{{item.id}}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script>
    import CustomVeg from '../CustomVueEditableGrid/components/CustomVeg.vue'

    import data from './data'

    const defaultDateFormat = 'MMM dd, yyyy'
    const defaultDateTimeFormat = 'MMM dd, yyyy h:mm a'

    const numericFormatter = event => {
        if (event.reverse) {
            return event.value && +event.value.replace(' years')
        }
        return `${event.value} years`
    }
    const selectOptions = [
        { value: '', text: 'Select' },
        { value: 'green', text: 'green' },
        { value: 'blue', text: 'blue' },
        { value: 'brown', text: 'brown' }
    ]
    const columnDefinition = [
        { sortable: true, filter: true, field: 'id', headerName: 'Id', editable: true },
        { sortable: true, filter: true, field: 'eyeColor', headerName: 'Eye color', editable: true, type: 'select', selectOptions: selectOptions },
        { sortable: true, filter: true, field: 'name', headerName: 'Name', editable: true, maxlength: 5, className: 'my-custom-class' },
        { sortable: true, filter: true, field: 'gender', headerName: 'Gender', editable: true },
        { sortable: true, filter: true, field: 'company', headerName: 'Company', editable: true },
        { sortable: true, filter: true, field: 'email', headerName: 'Email', editable: true },
        { sortable: true, filter: true, field: 'Phone', headerName: 'Phone', editable: true },
        { sortable: true, filter: true, field: 'registered', headerName: 'registered', type: 'datetime', format: defaultDateFormat, editable: true },
        { sortable: true, filter: true, field: 'registered', headerName: 'registered', type: 'date', format: defaultDateTimeFormat, editable: true, min: '2020-12-01', max: '2021-01-15' },
        { sortable: true, filter: true, field: 'age', headerName: 'Age', type: 'numeric', editable: true, min: 1, max: 100 },
        { sortable: true, filter: true, field: 'age', headerName: 'Age Formatted', type: 'numeric', editable: true, formatter: numericFormatter },
        { sortable: true, filter: true, field: 'balance', headerName: 'Balance', type: 'currency', editable: true },
        { sortable: true, filter: true, field: 'happiness', headerName: 'Happiness percent', type: 'percent', editable: true },
        { sortable: true, filter: true, field: 'isActive', headerName: 'Is active', type: 'boolean', editable: true },
        { sortable: true, filter: false, field: 'picture', headerName: 'Picture', type: 'link', editable: false },
    ]

    export default {
        name: 'example',
        components: {
            CustomVeg
        },
        data() {
            return {
                columnDefs: columnDefinition,
                rows: [],
                selectedRow: null
            }
        },
        created() {
            this.formatData()
        },
        mounted() {
            // you can call methods this.$refs.grid.getFormattedRows()
        },
        methods: {
            formatData() {
                data.forEach(row => {
                    this.formatRow(row)
                })
                this.rows = data
            },
            formatRow(row) {
                const red = '#ffe5e5'
                const green = '#b6f7b6'
                const { happiness } = row
                const priceRateBgColor = happiness > 0.6 ? green : red
                row.$cellStyle = {
                    happiness: priceRateBgColor && { backgroundColor: priceRateBgColor }
                }
                if (row.eyeColor === 'blue') {
                    row.$rowStyle = { color: 'blue' }
                }
            },
            cellUpdated($event) {
                //console.log($event)
            },
            rowSelected($event) {
                this.selectedRow = $event.rowData
            },
            multipleSelected($event) {
                //console.log('Multiple selected', $event)
            },
            linkClicked($event) {
                //console.log($event)
            },
            removeCurrentRow() {
                this.rows = this.rows.filter(row => row.id !== this.selectedRow.id)
            },
            contextMenu($event) {
                //console.log($event)
            }
        }
    }
</script>

<style lang="scss">
#app {
  /*font-family: Avenir, Helvetica, Arial, sans-serif;*/
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
 /* text-align: center;
  color: #2c3e50;
  font-size: 14px;*/
  height: 500px;
}

.grid {
  height: 100%;
}

.ml-1 {
  margin-left: 10px;
}

.my-custom-class
.header-content{
  color: red;

  &::before {
    content: '*';
  }
}
</style>
