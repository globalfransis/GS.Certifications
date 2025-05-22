<template>
    <div class="d-flex align-items-center" :class="estadoClase">
        <i :class="estadoIcono" class="me-1"></i>
        <span>{{ estadoDescripcion }}</span>
    </div>
</template>

<script>
export default {
    name: "solicitudCertificacionEstado-label",
    props: {
        value: Number, // ID del estado
    },
    computed: {
        estado() {
            const estados = [
                { idm: SolicitudEstado.PENDIENTE, descripcion: "Pendiente", color: "text-warning", icono: "fas fa-circle" }, 
                { idm: SolicitudEstado.PRESENTADA, descripcion: "Presentada", color: "text-primary", icono: "fas fa-circle" },
                { idm: SolicitudEstado.APROBADA, descripcion: "Aprobada", color: "text-success", icono: "fas fa-check-circle" },
                { idm: SolicitudEstado.RECHAZADA, descripcion: "Rechazada", color: "text-danger", icono: "fas fa-times-circle" }, 
                { idm: SolicitudEstado.BORRADOR, descripcion: "Borrador", color: "text-secondary", icono: "fas fa-circle" } 
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
