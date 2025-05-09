<template>
    <!--Todos los componentes tienen que tener un solo elemento raíz-->
    <div class="root">

        <h1>3. Pasaje de parámetros de padre a hijo mediante props</h1>

        <div class="row g-2">
            <div class="col-2">
                <div class="form-floating">
                    <input v-model="newName" type="text" class="form-control" id="newName" placeholder="Nuevo Nombre">
                </div>
            </div>
            <div class="col-4">
                <div class="form-floating">
                    <input v-model="newDescription" type="text" class="form-control" id="newDescription" placeholder="Nueva Descripción">

                </div>
            </div>
            <div class="col-md">
                <div class="form-floating">
                    <button @click="update" type="button" class="btn btn-primary">Actualizar</button>
                </div>
            </div>
        </div>
        <br />
        <div>
            <!--Initial values for 'Name' and 'Description' properties are setted this way: -->
            <detail-comp v-bind:name=childName
                         v-bind:description=childDescription>
            </detail-comp>
        </div>
    </div>
</template>

<script>

    import detailComp from "./detail-comp";

    export default {
        name: "header-component",
        components: { detailComp },
        methods: {
            update: function () {
                this.childName = this.newName;
                this.childDescription = this.newDescription;
                this.newName = "";
                this.newDescription = "";
            }
        },
        data: function () {
            return {
                childName: 'Vel turpis ',
                childDescription: 'Suspendisse potenti nullam ac tortor vitae, mretium fusce id velit ut tortor pretium viverra',
                newName: '',
                newDescription: ''
            }
        }
    };

</script>