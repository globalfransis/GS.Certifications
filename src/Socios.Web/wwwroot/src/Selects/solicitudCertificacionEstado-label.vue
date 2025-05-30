<template>
    <div class="d-flex align-items-center" :class="estadoClase">
        <i :class="estadoIcono" class="me-1"></i>
        <span>{{ estadoDescripcion }}</span>
    </div>
</template>

<script>
import loc from "@/common/commonLoc.js"

export default {
    name: "solicitudCertificacionEstado-label",
    props: {
        value: Number, // ID del estado
    },
    data: function () {return {loc}},
    computed: {
        estado() {
            const estados = [
                { idm: SolicitudEstado.PENDIENTE, descripcion: this.loc["Pendiente"], color: "text-warning", icono: "fas fa-circle" }, 
                { idm: SolicitudEstado.PRESENTADA, descripcion: this.loc["Presentada"], color: "text-primary", icono: "fas fa-circle" },
                { idm: SolicitudEstado.APROBADA, descripcion: this.loc["Aprobada"], color: "text-success", icono: "fas fa-check-circle" },
                { idm: SolicitudEstado.RECHAZADA, descripcion: this.loc["Rechazada"], color: "text-danger", icono: "fas fa-times-circle" }, 
                { idm: SolicitudEstado.BORRADOR, descripcion: this.loc["Borrador"], color: "text-secondary", icono: "fas fa-circle" } 
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
