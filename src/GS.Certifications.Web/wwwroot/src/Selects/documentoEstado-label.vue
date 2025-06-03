<template>
    <div class="d-flex align-items-center" :class="estadoClase">
        <i :class="estadoIcono" class="me-1"></i>
        <span>{{ estadoDescripcion }}</span>
    </div>
</template>

<script>
import loc from "@/common/commonLoc.js"

export default {
    name: "DocumentoEstadoLabel",
    props: {
        value: Number, // ID del estado
    },
    data: function () {return {loc}},
    computed: {
        estado() {
            const estados = [
                { idm: 1, descripcion: this.loc["Pendiente"], color: "text-secondary", icono: "fas fa-circle" }, 
                { idm: 2, descripcion: this.loc["Validado"], color: "text-success", icono: "fas fa-check-double" },
                { idm: 3, descripcion: this.loc["Rechazado"], color: "text-danger", icono: "fas fa-times-circle" },
                { idm: 4, descripcion: this.loc["Vencido"], color: "text-warning-darker", icono: "fas fa-check-circle" }, 
                { idm: 5, descripcion: this.loc["Presentado"], color: "text-success", icono: "fas fa-circle" } 
            ];
            return estados.find(e => e.idm === this.value) || estados[0];
        },
        estadoDescripcion() {
            return this.estado.descripcion;
        },
        estadoClase() {
            return this.estado.color;
        },
        estadoIcono() {
            return this.estado.icono;
        }
    },
    async mounted() {
        // await this.getEstados();
    },
    methods: {
    }
};
</script>
