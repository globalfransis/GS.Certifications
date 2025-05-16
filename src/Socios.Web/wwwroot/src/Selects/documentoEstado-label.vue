<template>
    <div class="d-flex align-items-center" :class="estadoClase">
        <i :class="estadoIcono" class="me-1"></i>
        <span>{{ estadoDescripcion }}</span>
    </div>
</template>

<script>
export default {
    name: "DocumentoEstadoLabel",
    props: {
        value: Number, // ID del estado
    },
    computed: {
        estado() {
            const estados = [
                { idm: 1, descripcion: "Pendiente", color: "text-secondary", icono: "fas fa-circle" }, 
                { idm: 2, descripcion: "Validado", color: "text-success", icono: "fas fa-check-double" },
                { idm: 3, descripcion: "Rechazado", color: "text-danger", icono: "fas fa-times-circle" },
                { idm: 4, descripcion: "Vencido", color: "text-warning", icono: "fas fa-check-circle" }, 
                { idm: 5, descripcion: "Presentado", color: "text-success", icono: "fas fa-circle" } 
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
