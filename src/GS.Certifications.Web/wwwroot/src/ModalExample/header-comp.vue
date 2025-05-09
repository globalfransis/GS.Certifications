<template>
    <!--Todos los componentes tienen que tener un solo elemento raíz-->
    <div class="root">

        <h1>4. Componente Modal</h1>

        <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#miModal">
            Lanzar Modal (Ya está con Boostrap 5)
        </button>

        <modal-comp idModal="miModal">
            
            <template v-slot:header>
                Este texto lo mando yo
            </template>
            <h2>Este es un body loco</h2>
            <template v-slot:footer>
                <h3>Footer loco</h3>
            </template>

        </modal-comp>

    </div>

</template>

<script>

    import modalComp from "./modal-comp"

    export default {
        name: "header-modal-comp",
        
        components: { modalComp }
    };

</script>