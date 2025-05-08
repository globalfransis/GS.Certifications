<template>
    <!--Todos los componentes tienen que tener un solo elemento raíz-->
    <div class="root">
        <h1>2. Componentes anidados</h1>
        <div>
            <detail-comp>
                <h3 class="slot-content">Este texto está escrito en el componente padre, pero se renderiza en el componente hijo mediante slots</h3>
            </detail-comp>
        </div>
    </div>

</template>

<style>
    .slot-content {
        border: 1px dashed gray;
        margin: 10px;
        padding: 5px;
    }
</style>

<script>

    import detailComp from "./detail-comp";

    export default {
        name: "header-component",
        components: { detailComp },
    };

</script>
