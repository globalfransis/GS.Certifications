<template>

    <!--Todos los componentes tienen que tener un solo elemento raíz-->
    <div class="root">

        <week-days v-model="val" v-bind:error="''"></week-days>
        <h1>1. Mínimo componente simple</h1>

    </div>
</template>

<script>
    import weekDays from "@/components/weekdays/week-days";
    export default {
        components: {weekDays},
        name: "simple-component",
        data: function () {
            return {
                val: "560"
            }
        }
    };

</script>