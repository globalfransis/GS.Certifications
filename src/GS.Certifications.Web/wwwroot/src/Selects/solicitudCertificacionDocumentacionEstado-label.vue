<template>
    <div class="d-flex align-items-center" :class="estadoClase">
        <i :class="estadoIcono" class="me-1"></i>
        <span>{{ estadoDescripcion }}</span>
    </div>
</template>

<script>

const POR_VENCER_DIAS_LIMITE = 30; // TODO: esto más adelante podria ser una prop, y el valor tendria que ser alguna configuracion a nivel company

export default {
    name: "solicitudCertificacionDocumentacionEstado-label",
    props: {
        value: Object, // ID del estado
    },
    computed: {
        estado() {
            if (!this.value || typeof this.value.estadoId === 'undefined') {
                return { descripcion: "Inválida", color: "text-muted", icono: "fas fa-question-circle" };
            }

            const hoy = new Date();
            hoy.setHours(0, 0, 0, 0);

            const posiblesEstados = [
                {
                    condition: this.esDocumentacionRechazada,
                    descripcion: "Documentación Rechazada",
                    color: "text-danger",
                    icono: "fas fa-times-circle"
                },
                {
                    condition: this.esDocumentacionPendienteVerificacion,
                    descripcion: "Doc. Pend. Verificación",
                    color: "text-danger",
                    icono: "fas fa-hourglass-half"
                },
                {
                    condition: this.esDocumentacionVencida,
                    descripcion: "Documentación Vencida",
                    color: "text-danger",
                    icono: "fas fa-exclamation-circle"
                },
                {
                    condition: this.esDocumentacionPorVencer,
                    descripcion: "Documentación por Vencer",
                    color: "text-warning",
                    icono: "fas fa-exclamation-triangle"
                },
                {
                    condition: this.esDocumentacionOk,
                    descripcion: "OK",
                    color: "text-success",
                    icono: "fas fa-check-circle"
                },
                {
                    condition: this.esDocumentacionFaltante,
                    descripcion: "Documentación Faltante",
                    color: "text-danger",
                    icono: "fas fa-pencil-alt"
                },
                // otros estados
                {
                    condition: (value) => value.estadoId === SolicitudEstado.APROBADA,
                    descripcion: "Aprobada",
                    color: "text-success",
                    icono: "fas fa-check-circle"
                },
                {
                    condition: (value) => value.estadoId === SolicitudEstado.PRESENTADA,
                    descripcion: "Presentada",
                    color: "text-primary",
                    icono: "fas fa-file-import"
                },
                {
                    condition: (value) => value.estadoId === SolicitudEstado.RECHAZADA,
                    descripcion: "Rechazada",
                    color: "text-danger",
                    icono: "fas fa-ban"
                },
            ];

            const estadoEncontrado = posiblesEstados.find(estadoDef => estadoDef.condition(this.value, hoy));

            if (estadoEncontrado) {
                return estadoEncontrado;
            }

            // fallback si ninguna condición específica se cumple
            return { descripcion: "Estado Desconocido", color: "text-muted", icono: "fas fa-question-circle" };
        },
        estadoDescripcion() {
            return this.estado.descripcion;
        },
        estadoClase() {
            return this.estado.color;
        },
        estadoIcono() {
            return this.estado.icono;
        },

        
    },
    async mounted() {
    },
    methods: {
        // Callbacks de verificación de condiciones
        esDocumentacionFaltante(solicitud) {
            return solicitud.estadoId === SolicitudEstado.BORRADOR &&
                (!solicitud.documentosCargados ||
                    solicitud.documentosCargados.some(d => d.estadoId === DocumentoEstado.PENDIENTE));
        },
        esDocumentacionVencida(solicitud, fechaHoy) {
            if (solicitud.estadoId !== SolicitudEstado.APROBADA) return false;
            if (!solicitud.documentosCargados || solicitud.documentosCargados.length === 0) return false;

            return solicitud.documentosCargados.some(d => {
                if (!d.fechaHasta) return false; // Si no hay fechaHasta, no puede estar vencido
                const fechaHastaDoc = new Date(d.fechaHasta);
                fechaHastaDoc.setHours(0, 0, 0, 0);
                return fechaHastaDoc < fechaHoy;
            });
        },
        esDocumentacionPorVencer(solicitud, fechaHoy) {
            if (solicitud.estadoId !== SolicitudEstado.APROBADA) return false;
            if (!solicitud.documentosCargados || solicitud.documentosCargados.length === 0) return false;

            // no debe estar ya vencida para ser "por vencer"
            if (this.esDocumentacionVencida(solicitud, fechaHoy)) return false;

            const fechaLimite = new Date(fechaHoy);
            fechaLimite.setDate(fechaHoy.getDate() + POR_VENCER_DIAS_LIMITE);

            return solicitud.documentosCargados.some(d => {
                if (!d.fechaHasta) return false;
                const fechaHastaDoc = new Date(d.fechaHasta);
                fechaHastaDoc.setHours(0, 0, 0, 0);
                // está vigente hoy Y antes de la fecha limite de "por vencer"
                return fechaHastaDoc >= fechaHoy && fechaHastaDoc < fechaLimite;
            });
        },
        esDocumentacionOk(solicitud, fechaHoy) {
            if (solicitud.estadoId !== SolicitudEstado.APROBADA) return false;

            // si está vencida o por vencer, no está "OK" en este contexto
            if (this.esDocumentacionVencida(solicitud, fechaHoy)) return false;
            if (this.esDocumentacionPorVencer(solicitud, fechaHoy, POR_VENCER_DIAS_LIMITE)) return false;

            // si no hay documentos cargados, y la solicitud está aprobada, se considera OK
            if (!solicitud.documentosCargados || solicitud.documentosCargados.length === 0) return true;

            // todos los documentos deben estar validados y dentro del rango de fechas
            return solicitud.documentosCargados.every(d => {
                if (d.estadoId !== DocumentoEstado.VALIDADO) return false;
                if (!d.fechaDesde || !d.fechaHasta) return false; // si no hay fechas, no se puede validar el rango

                const fechaDesdeDoc = new Date(d.fechaDesde);
                const fechaHastaDoc = new Date(d.fechaHasta);
                fechaDesdeDoc.setHours(0, 0, 0, 0);
                fechaHastaDoc.setHours(0, 0, 0, 0);

                return fechaDesdeDoc <= fechaHoy && fechaHastaDoc >= fechaHoy;
            });
        },
        esDocumentacionPendienteVerificacion(solicitud) {
            if (solicitud.estadoId !== SolicitudEstado.PRESENTADA) return false;
            if (!solicitud.documentosCargados || solicitud.documentosCargados.length === 0) return false;

            if (this.esDocumentacionRechazada(solicitud)) return false;

            return solicitud.documentosCargados.some(
                d => d.estadoId !== DocumentoEstado.VALIDADO && d.estadoId !== DocumentoEstado.RECHAZADO
            );
        },
        esDocumentacionRechazada(solicitud) {
            if (solicitud.estadoId !== SolicitudEstado.PRESENTADA) return false;
            if (!solicitud.documentosCargados || solicitud.documentosCargados.length === 0) return false;

            return solicitud.documentosCargados.some(d => d.estadoId === DocumentoEstado.RECHAZADO);
        }
    }
};
</script>
