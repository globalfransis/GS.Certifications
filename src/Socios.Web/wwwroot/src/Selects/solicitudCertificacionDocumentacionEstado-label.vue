<template>
    <div class="d-flex align-items-center" :class="estadoClase">
        <i :class="estadoIcono" class="me-1"></i>
        <span>{{ estadoDescripcion }}</span>
    </div>
</template>

<script>
import loc from "@/common/commonLoc.js"

const POR_VENCER_DIAS_LIMITE = 30; // TODO: esto más adelante podria ser una prop, y el valor tendria que ser alguna configuracion a nivel company

export default {
    name: "solicitudCertificacionDocumentacionEstado-label",
    props: {
        value: Object, // ID del estado
    },
    data: function () { return { loc } },
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
                    descripcion: this.loc["Documentación Rechazada"],
                    color: "text-danger",
                    icono: "fas fa-times-circle"
                },
                {
                    condition: this.esDocumentacionPendienteVerificacion,
                    descripcion: this.loc["Doc. Pend. Verificación"],
                    color: "text-danger",
                    icono: "fas fa-hourglass-half"
                },
                {
                    condition: this.esDocumentacionEnRevision,
                    descripcion: this.loc["Doc. para Revisión"],
                    color: "text-warning",
                    icono: "fas fa-exclamation-circle"
                },
                {
                    condition: this.esDocumentacionVencida,
                    descripcion: this.loc["Documentación Vencida"],
                    color: "text-danger",
                    icono: "fas fa-exclamation-circle"
                },
                {
                    condition: this.esDocumentacionPorVencer,
                    descripcion: this.loc["Documentación por Vencer"],
                    color: "text-warning",
                    icono: "fas fa-exclamation-triangle"
                },
                {
                    condition: this.esDocumentacionOk,
                    descripcion: this.loc["OK"],
                    color: "text-success",
                    icono: "fas fa-check-circle"
                },
                {
                    condition: this.esDocumentacionFaltante,
                    descripcion: this.loc["Documentación Faltante"],
                    color: "text-danger",
                    icono: "fas fa-pencil-alt"
                },
                // otros estados
                {
                    condition: (value) => value.estadoId === SolicitudEstado.APROBADA,
                    descripcion: this.loc["Aprobada"],
                    color: "text-success",
                    icono: "fas fa-check-circle"
                },
                {
                    condition: (value) => value.estadoId === SolicitudEstado.PRESENTADA,
                    descripcion: this.loc["Presentada"],
                    color: "text-primary",
                    icono: "fas fa-file-import"
                },
                {
                    condition: (value) => value.estadoId === SolicitudEstado.RECHAZADA,
                    descripcion: this.loc["Rechazada"],
                    color: "text-danger",
                    icono: "fas fa-ban"
                },
            ];

            const estadoEncontrado = posiblesEstados.find(estadoDef => estadoDef.condition(this.value, hoy));

            if (estadoEncontrado) {
                return estadoEncontrado;
            }

            // fallback si ninguna condición específica se cumple
            return { descripcion: this.loc["Estado Desconocido"], color: "text-muted", icono: "fas fa-question-circle" };
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
        getUltimasVersionesDocumentosCargados(documentosCargados) {
            if (!documentosCargados || documentosCargados.length === 0) {
                return [];
            }
            const agrupados = documentosCargados.reduce((acc, doc) => {
                const key = doc.documentoRequeridoId;
                if (!acc[key] || (doc.version && acc[key].version && doc.version > acc[key].version) || !acc[key].version) {
                    acc[key] = doc;
                }
                return acc;
            }, {});
            return Object.values(agrupados);
        },
        // Callbacks de verificación de condiciones
        esDocumentacionFaltante(solicitud) {
            if (solicitud.estadoId !== SolicitudEstado.BORRADOR) {
                return false;
            }
            const ultimasVersiones = this.getUltimasVersionesDocumentosCargados(solicitud.documentosCargados);
            if (!ultimasVersiones || ultimasVersiones.length === 0 && solicitud.certificacion && solicitud.certificacion.documentosRequeridos && solicitud.certificacion.documentosRequeridos.length > 0) {
                return true;
            }
            return ultimasVersiones.some(d => d.estadoId === DocumentoEstado.PENDIENTE || !d.archivoURL);
        },

        esDocumentacionVencida(solicitud, fechaHoy) {
            // Solo relevante si la solicitud está APROBADA
            if (solicitud.estadoId !== SolicitudEstado.APROBADA) return false;
            const ultimasVersiones = this.getUltimasVersionesDocumentosCargados(solicitud.documentosCargados);
            if (!ultimasVersiones || ultimasVersiones.length === 0) return false;

            return ultimasVersiones.some(d => {
                if (d.estadoId !== DocumentoEstado.VALIDADO) return false;
                if (!d.fechaHasta) return true; // si está validado pero no tiene fechaHasta, se considera problemático/vencido por defecto
                const fechaHastaDoc = new Date(d.fechaHasta);
                fechaHastaDoc.setHours(0, 0, 0, 0);
                return fechaHastaDoc < fechaHoy;
            });
        },

        esDocumentacionPorVencer(solicitud, fechaHoy) {
            if (solicitud.estadoId !== SolicitudEstado.APROBADA) return false;
            const ultimasVersiones = this.getUltimasVersionesDocumentosCargados(solicitud.documentosCargados);
            if (!ultimasVersiones || ultimasVersiones.length === 0) return false;

            // No puede estar "por vencer" si ya está "vencida"
            if (this.esDocumentacionVencida(solicitud, fechaHoy)) return false;

            const fechaLimite = new Date(fechaHoy);
            fechaLimite.setDate(fechaHoy.getDate() + POR_VENCER_DIAS_LIMITE);

            return ultimasVersiones.some(d => {
                if (d.estadoId !== DocumentoEstado.VALIDADO) return false;
                if (!d.fechaHasta) return false;
                const fechaHastaDoc = new Date(d.fechaHasta);
                fechaHastaDoc.setHours(0, 0, 0, 0);
                return fechaHastaDoc >= fechaHoy && fechaHastaDoc < fechaLimite;
            });
        },

        esDocumentacionOk(solicitud, fechaHoy) {
            // Solo tiene sentido si la solicitud está APROBADA
            if (solicitud.estadoId !== SolicitudEstado.APROBADA) return false;

            // Si ya determinamos que está Vencida o Por Vencer, entonces no está OK
            if (this.esDocumentacionVencida(solicitud, fechaHoy)) return false;
            if (this.esDocumentacionPorVencer(solicitud, fechaHoy)) return false;

            const ultimasVersiones = this.getUltimasVersionesDocumentosCargados(solicitud.documentosCargados);

            return true;
        },

        esDocumentacionPendienteVerificacion(solicitud) {
            // Relevante si la solicitud está PRESENTADA
            if (solicitud.estadoId !== SolicitudEstado.PRESENTADA) return false;
            const ultimasVersiones = this.getUltimasVersionesDocumentosCargados(solicitud.documentosCargados);

            if (!ultimasVersiones || ultimasVersiones.length === 0) return false;

            if (ultimasVersiones.some(d => d.estadoId === DocumentoEstado.RECHAZADO)) return false;

            return ultimasVersiones.some(d => d.estadoId !== DocumentoEstado.VALIDADO);
        },

        esDocumentacionRechazada(solicitud) {
            const ultimasVersiones = this.getUltimasVersionesDocumentosCargados(solicitud.documentosCargados);
            if (!ultimasVersiones || ultimasVersiones.length === 0) return false;
            // Es "Rechazada" si la ÚLTIMA versión de AL MENOS UN documento está RECHAZADA
            return ultimasVersiones.some(d => d.estadoId === DocumentoEstado.RECHAZADO);
        },
        esDocumentacionEnRevision(solicitud) {
            return solicitud.estadoId === SolicitudEstado.REVISION;
        }
    }
};
</script>
