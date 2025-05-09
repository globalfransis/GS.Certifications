<template>
    <div class="d-flex align-items-center" :class="estadoClase">
        <i :class="estadoIcono" class="me-1"></i>
        <span>{{ estadoDescripcion }}</span>
    </div>
</template>

<script>

import ajax from "@/common/ajaxWrapper";

export default {
    name: "ComprobanteEstadoLabel",
    props: {
        value: Number, // ID del estado
    },
    computed: {
        estado() {
            const estados = [
                { idm: 8, descripcion: "Borrador", color: "text-secondary", icono: "fas fa-circle" },  // Gris
                { idm: 4, descripcion: "Confirmado", color: "text-success", icono: "fas fa-check-circle" }, // Amarillo
                { idm: 7, descripcion: "Rechazado", color: "text-danger", icono: "fas fa-times-circle" }, // Rojo
                { idm: 6, descripcion: "Aprobado", color: "text-primary", icono: "fas fa-check-circle" }, // Azul
                { idm: 9, descripcion: "Autorizado", color: "text-success", icono: "fas fa-check-circle" } // Verde
            ];
            return estados.find(e => e.idm === this.value) || estados[0]; // Default: Borrador
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
        await this.getEstados();
    },
    methods: {
        async getEstados() {
            try {
                const res = await new ajax().get(baseUrl + "/api/Proveedores/Comprobantes/Estados");
                console.log()
                this.optionsData = res;
            } catch (error) {
                console.error("Error obteniendo estados:", error);
            }
        },
        getDescripcionBorrador() {
            // Descripción por defecto (Borrador) si no se encuentra en los estados
            return "Borrador";
        }
    }
};
</script>
