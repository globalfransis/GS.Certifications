<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p v-if="mode.isCreate" class="h5">{{loc["Crear Alerta"]}}</p>
                <p v-if="mode.isUpdate" class="h5">{{loc["Modificar Alerta"]}} {{ this.alerta.id }}</p>
                <p v-if="mode.isDetail" class="h5">{{loc["Alerta"]}} {{ this.alerta.id }}</p>
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">

                        <div class="form-group required col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">{{loc["Alerta Tipo"]}}</label>
                            <alertaTipos-select :disabled="!mode.isCreate" v-model="alerta.alertaTipo" />
                            <span class="text-danger field-validation-error">
                                {{ errorBag.get("alertaTipoIdm") }}
                            </span>
                        </div>

                        <div class="col-lg-3 col-sm-12 mb-4">
                            <div class="form-group required col input-group">
                                <label class="control-label">{{loc["Estado"]}}</label>
                                <div data-toggle="tooltip" :title="INFO_ESTADO">
                                    <i class="ms-2 fas fa-md fa-info-circle col-1"></i>
                                </div>
                            </div>
                            <alertaTipoEstados-select :disabled="!mode.isCreate || alerta.alertaTipo.idm == null"
                                :alertaTipoIdm="alerta.alertaTipo.idm" v-model="alerta.estadoIdm" />
                            <span class="text-danger field-validation-error">
                                {{ errorBag.get("AlertaTipoEstadoIdm") }}
                                {{ errorBag.get("EstadoUbicacion") }}
                            </span>
                        </div>

                        <div v-if="alerta.alertaTipo.idm == ALERTA_TIPO_TARJETA_IDM && mode.isCreate"
                            class="col-lg-3 col-sm-12 mb-4">
                            <div class="form-group required col input-group">
                                <label class="control-label">{{loc["Ubicacion"]}}</label>
                                <div data-toggle="tooltip" :title="INFO_UBICACION">
                                    <i class="ms-2 fas fa-md fa-info-circle col-1"></i>
                                </div>
                            </div>
                            <alertaTipoUbicaciones-select :disabled="!mode.isCreate || alerta.alertaTipo.idm == null"
                                :alertaTipoIdm="alerta.alertaTipo.idm" v-model="alerta.ubicacionIdm" />
                            <span class="text-danger field-validation-error">
                                {{ errorBag.get("AlertaTipoUbicacionIdm") }}
                            </span>
                        </div>

                        <div class="col-lg-3 col-sm-12 mb-4">
                            <div class="form-group required col input-group">
                                <label class="control-label">{{loc["Horas"]}}</label>
                                <div data-toggle="tooltip" :title="INFO_HORAS">
                                    <i class="ms-2 fas fa-md fa-info-circle col-1"></i>
                                </div>
                            </div>
                            <input :disabled="!mode.isCreate" class="form-control" type="number"
                                @keydown="validateNumericValues($event)" v-model="alerta.horas">
                            <span class="text-danger field-validation-error">
                                {{ errorBag.get("horas") }}
                            </span>
                        </div>

                        <div class="col-lg-3 col-sm-12 mb-4">
                            <div class="form-group required col input-group">
                                <label class="control-label">{{loc["Fecha / Hora Desde"]}}</label>
                                <div data-toggle="tooltip" :title="INFO_FECHA_HORA_DESDE">
                                    <i class="ms-2 fas fa-md fa-info-circle col-1"></i>
                                </div>
                            </div>
                            <input :disabled="!mode.isCreate" id="fechaHoraDesde" class="form-control"
                                type="datetime-local" v-model="alerta.fechaHoraDesde">
                            <span class="text-danger field-validation-error">
                                {{ errorBag.get("fechaHoraDesde") }}
                            </span>
                        </div>

                        <div class="col-lg-3 col-sm-12 mb-4">
                            <div class="form-group col input-group">
                                <label class="control-label">{{loc["Fecha / Hora Hasta"]}}</label>
                                <div data-toggle="tooltip" :title="INFO_FECHA_HORA_HASTA">
                                    <i class="ms-2 fas fa-md fa-info-circle col-1"></i>
                                </div>
                            </div>
                            <input :disabled="!mode.isCreate" class="form-control" type="datetime-local"
                                v-model="alerta.fechaHoraHasta">
                            <span class="text-danger field-validation-error">
                                {{ errorBag.get("fechaHoraHasta") }}
                            </span>
                        </div>

                        <div class="col-lg-3 mb-4">
                            <div class="form-group required col input-group">
                                <label class="control-label">{{loc["Para"]}}</label>
                                <div data-toggle="tooltip" :title="INFO_PARA">
                                    <i class="ms-2 fas fa-md fa-info-circle col-1"></i>
                                </div>
                            </div>
                            <correos-multiselect :editMode="mode.isCreate" id="para" v-model="alerta.para" />
                            <span class="text-danger field-validation-error" data-valmsg-for="para"
                                data-valmsg-replace="true">
                                {{ errorBag.get("destinatarios") }}
                            </span>
                        </div>

                        <div v-if="alerta.alertaTipo.aceptaDestinatarioVariables" class="col-lg-3 col-sm-12 mb-4">
                            <div class="form-group col input-group">
                                <label class="control-label">{{loc["Para Variable"]}}</label>
                                <div data-toggle="tooltip" :title="INFO_PARA_VARIABLE">
                                    <i class="ms-2 fas fa-md fa-info-circle col-1"></i>
                                </div>
                            </div>
                            <destinatarioVariable-select :disabled="!mode.isCreate"
                                v-model="alerta.destinatarioVariableIdm" />
                            <span class="text-danger field-validation-error">
                                {{ errorBag.get("destinatarioVariables") }}
                            </span>
                        </div>

                        <div class="col-lg-3 mb-4">
                            <div class="col input-group">
                                <label>{{loc["CC"]}}</label>
                                <div data-toggle="tooltip" :title="INFO_CC">
                                    <i class="ms-2 fas fa-md fa-info-circle col-1"></i>
                                </div>
                            </div>
                            <correos-multiselect id="cc" :editMode="mode.isCreate" v-model="alerta.cc" />
                            <span class="text-danger field-validation-error" data-valmsg-for="cc"
                                data-valmsg-replace="true">
                                {{ errorBag.get("cc") }}
                            </span>
                        </div>

                        <div class="col-lg-3 mb-4">
                            <div class="col input-group">
                                <label>{{loc["CCO"]}}</label>
                                <div data-toggle="tooltip" :title="INFO_CCO">
                                    <i class="ms-2 fas fa-md fa-info-circle col-1"></i>
                                </div>
                            </div>
                            <correos-multiselect id="cco" :editMode="mode.isCreate" v-model="alerta.cco" />
                            <span class="text-danger field-validation-error" data-valmsg-for="cco"
                                data-valmsg-replace="true">
                                {{ errorBag.get("cco") }}
                            </span>
                        </div>

                        <div class="col-12 mb-4">
                            <div class="form-group required col input-group">
                                <label class="control-label">{{loc["Asunto"]}}</label>
                                <div data-toggle="tooltip" :title="INFO_ASUNTO">
                                    <i class="ms-2 fas fa-md fa-info-circle col-1"></i>
                                </div>
                            </div>
                            <input :disabled="!mode.isCreate" class="form-control" type="text" v-model="alerta.asunto">
                            <span class="text-danger field-validation-error">
                                {{ errorBag.get("asunto") }}
                            </span>
                        </div>

                        <div v-if="alerta.alertaTipo.idm == ALERTA_TIPO_TARJETA_IDM && mode.isCreate"
                            class="col-12 mb-4">
                            <div class="col input-group">
                                <label class="control-label">{{loc["Reglas Adicionales"]}}</label>
                                <div data-toggle="tooltip" :title="INFO_REGLAS">
                                    <i class="ms-2 fas fa-md fa-info-circle col-1"></i>
                                </div>
                            </div>
                            <reglasAdicionales-multiselect :enabled="mode.isCreate && alerta.alertaTipo.idm != null"
                                :alertaTipoIdm="alerta.alertaTipo.idm" v-model="alerta.reglasAdicionales" />
                            <span class="text-danger field-validation-error">
                                {{ errorBag.get("reglasAdicionales") }}
                            </span>
                        </div>

                        <div v-if="!mode.isCreate" class="col-lg-3 col-md-6 col-sm-12">
                            <div class="col input-group">
                                <label class="control-label me-2">{{loc["Activa"]}}</label>
                                <div data-toggle="tooltip" :title="INFO_ACTIVA">
                                    <i class="fas fa-md fa-info-circle col-1"></i>
                                </div>
                            </div>
                            <div class="btn-group" role="group" aria-label="Basic radio toggle button group">
                                <input :disabled="mode.isDetail" :checked="alerta.activa" type="radio" class="btn-check"
                                    name="btnradio" id="btnActivaFiltro" autocomplete="off" v-on:click="esActiva(true)">
                                <label class="btn btn-outline-primary" for="btnActivaFiltro">{{loc["Sí"]}}</label>
                                <input :disabled="mode.isDetail" :checked="!alerta.activa" type="radio"
                                    class="btn-check" name="btnradio" id="btnNoEsActivaFiltro" autocomplete="off"
                                    v-on:click="esActiva(false)">
                                <label class="btn btn-outline-secondary" for="btnNoEsActivaFiltro">{{loc["No"]}}</label>
                            </div>
                        </div>

                        <div v-if="mode.isCreate">
                            <hr>
                            <div class="form-group required">
                                <label class="control-label">{{loc["Cuerpo"]}}</label>
                                <importar-archivo-alerta ref="importarAlerta" :mostrarAcciones="true"
                                    :target="'alertas'" :targetId="null"
                                    @archivosImportados="onArchivosImportados($event)" />
                                <span class="text-danger field-validation-error">
                                    {{ errorBag.get("archivo") }}
                                    {{ errorBag.get("CampoVariableObligatorio") }}
                                </span>
                            </div>
                            <camposVariables-list :alertaTipoIdm="alerta.alertaTipo.idm"></camposVariables-list>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-12 d-flex justify-content-end mb-3 mt-3">
                <div v-if="mode.isCreate || mode.isEdit">
                    <accept-button @click="save">{{loc["Aceptar"]}}</accept-button>
                    <cancel-button @click="goBack">{{loc["Cancelar"]}}</cancel-button>
                </div>
                <div v-else>
                    <accept-button @click="setEdit">{{loc["Editar"]}}</accept-button>
                    <cancel-button @click="goBack">{{loc["Volver"]}}</cancel-button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import destinatarioVariableSelect from "@/selects/destinatarioVariable-select.vue";
import alertaTiposSelect from "@/selects/alertaTipos-select.vue";
import alertaTipoEstadosSelect from "@/selects/alertaTipoEstados-select.vue";
import alertaTipoUbicacionesSelect from "@/selects/alertaTipoUbicaciones-select.vue";
import AcceptButton from "@/components/forms/accept-button.vue";
import CancelButton from "@/components/forms/cancel-button.vue";
import ajax from "@/common/ajaxWrapper";
import ErrorBag from "@/common/ErrorBag";
import UiService from "@/common/uiService";
import correosMultiselect from "@/Multiselects/correos-multiselect";
import ImportarArchivoAlerta from "@/Components/ImportadorArchivos/Importadores/importar-archivo-alerta.vue";
import reglasAdicionalesMultiselect from "./reglasAdicionales-multiselect.vue";
import camposVariablesList from "./camposVariables-list.vue";
import CrudMode from "@/common/CrudMode";

import loc from "@/common/commonLoc.js"

const CREATE_ALERTA_URL = baseUrl + "/api/Alertas/CreateAlerta";
const GET_ALERTA_URL = baseUrl + "/api/Alertas/GetAlerta";
const UPDATE_ALERTA_URL = baseUrl + "/api/Alertas/UpdateAlerta";

const MAX_HORAS_PERMITIDAS = 2160;

export default {
    components: {
        AcceptButton,
        CancelButton,
        correosMultiselect,
        destinatarioVariableSelect,
        alertaTiposSelect,
        alertaTipoEstadosSelect,
        alertaTipoUbicacionesSelect,
        ImportarArchivoAlerta,
        reglasAdicionalesMultiselect,
        camposVariablesList
    },

    name: "alertas-form",

    data: function () {
        return {
            loc,
            alerta: {
                id: null,
                alertaTipo: {
                    idm: null,
                    descripcion: "",
                    aceptaDestinatarioVariables: null
                },
                estadoIdm: null,
                ubicacionIdm: null,
                reglasAdicionales: [],
                horas: null,
                fechaHoraDesde: null,
                fechaHoraHasta: null,
                para: [],
                destinatarioVariableIdm: null,
                cc: [],
                cco: [],
                asunto: "",
                archivo: ""
            },
            errorBag: new ErrorBag(),
            uiService: new UiService(),
            INFO_ESTADO: loc["Estado para Disparar la Notificacion de Alerta. Se debe especificar Alerta Tipo para seleccionar este campo"],
            INFO_UBICACION: loc["Ubicación para Disparar la Notificacion de Alerta. Se debe especificar Alerta Tipo para seleccionar este campo"],
            INFO_HORAS: loc["Horas transcurridas en el Estado, para Disparar la Notificacion de Alerta. Podes indicar cero y esto disparara la Alerta de forma inmediata al llegar al Estado indicado"],
            INFO_PARA: loc["Destinatarios (Para) de la notificacion de Alerta"],
            INFO_PARA_VARIABLE: loc["Destinatarios (Para Variables) de la notificacion de Alerta"],
            INFO_CC: loc["Destinatarios (CC) de la notificacion de Alerta"],
            INFO_CCO: loc["Destinatarios (CCO) de la notificacion de Alerta"],
            INFO_ASUNTO: loc["Asunto de la notificacion de Alerta"],
            INFO_REGLAS: 
                loc["Tener en cuenta que el sistema no valida colisiones entre Reglas Adicionales."] + "\n" +
                loc["Esto significa que debes tener cuidado y no configurar Reglas Adicionales que colisionen entre si."] + "\n" +
                loc["Esto significa, no configurar reglas que por ejemplo indiquen:"] + "\n" +
                "'" + loc["Validar X dato = Si"] + "'" + "\n" +
                loc["y otra que indique"] + "\n" +
                "'" + loc["Validar mismo X dato = No"] + "'",
            INFO_FECHA_HORA_DESDE: loc["Fecha/Hora Desde de la modificacion de estado de la entidad para tomar a partir de esta"],
            INFO_FECHA_HORA_HASTA: loc["Fecha/Hora Hasta de la modificacion de estado de la entidad para no tomar mas alla de esta"],
            INFO_ACTIVA: loc["Este marca indica si la Alerta esta siendo disparada o no"],
            ALERTA_TIPO_TARJETA_IDM: 501,
            mode: new CrudMode()
        };
    },
    async mounted() {
        // Validar grants cuando existan
        switch (this.$route.name) {
            case 'detail':
                this.mode.setDetail();
                break;
            case 'create':
                this.mode.setCreate();
                break;
            case 'edit':
                this.mode.setUpdate();
                break;
            default:
                this.mode.setDetail();
                break;
        }

        if (this.$route.params.alertaId) this.getAlerta(this.$route.params.alertaId);
    },
    computed: {
        horas() {
            return this.alerta.horas;
        },
        fechaHoraDesde() {
            return this.alerta.fechaHoraDesde
        },
        fechaHoraMinima() {
            if (this.horas != null && this.horas !== "") {
                const fechaActual = new Date();
                fechaActual.setHours(fechaActual.getHours() - parseInt(this.horas) - 1);

                const fechaHoraMinima = new Date(fechaActual.getTime() - fechaActual.getTimezoneOffset() * 60000)
                    .toISOString()
                    .slice(0, 16);

                return fechaHoraMinima;
            }
        }
    },
    watch: {
        fechaHoraDesde() {
            if (this.mode.isCreate) {
                const valorIngresado = this.alerta.fechaHoraDesde;
                const fechaHoraMinima = this.getFechaHoraMinima();

                // Si el valor ingresado es menor al mínimo, ajustamos al mínimo
                if (valorIngresado && fechaHoraMinima && valorIngresado < fechaHoraMinima) {
                    this.uiService.showMessageError(`${loc['Fecha / Hora Desde no puede ser menor a']} ${fechaHoraMinima.replace('T', ' ')}.`)
                    this.alerta.fechaHoraDesde = fechaHoraMinima;
                }
            }
        },
        horas() {
            if (this.mode.isCreate) {
                const fechaDesdeInput = document.getElementById('fechaHoraDesde');
                if (this.horas != null) {

                    this.alerta.fechaHoraDesde = this.fechaHoraMinima;
                    fechaDesdeInput.setAttribute('min', this.fechaHoraMinima);
                }
                if (this.horas == "" || this.horas == null) {
                    fechaDesdeInput.removeAttribute('min');
                }
            }
        }
    },
    methods: {
        getFechaHoraMinima() {
            if (this.horas != null && this.horas !== "") {
                const fechaActual = new Date();
                fechaActual.setHours(fechaActual.getHours() - parseInt(this.horas) - 1);

                const fechaHoraMinima = new Date(fechaActual.getTime() - fechaActual.getTimezoneOffset() * 60000)
                    .toISOString()
                    .slice(0, 16);

                return fechaHoraMinima;
            }
            return null;
        },
        setEdit() {
            this.$router.push({ name: "edit", params: { alertaId: this.alerta.id } });
        },
        mostrarTooltip(tooltipId) {
            mostrarTooltip(tooltipId)
        },
        esActiva(activa) {
            this.alerta.activa = activa;
        },
        async save() {
            if (this.mode.isCreate) await this.create();
            else if (this.mode.isUpdate) await this.update();
        },
        async update() {
            this.errorBag.clear();
            this.uiService.showSpinner(true)
            return new ajax()
                .put(UPDATE_ALERTA_URL,
                    {
                        Id: this.alerta.id,
                        Activa: this.alerta.activa
                    },
                    { errorBag: this.errorBag })
                .then((res) => {
                    if (!this.errorBag.hasErrors()) {
                        this.uiService.showMessageSuccess(loc["Operación confirmada"])
                        this.goBack()
                    } else {
                        this.uiService.showMessageError(loc["Operación rechazada"])
                    }
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        async create() {
            this.errorBag.clear();
            this.uiService.showSpinner(true)
            return new ajax()
                .post(CREATE_ALERTA_URL,
                    {
                        AlertaTipoIdm: this.alerta.alertaTipo.idm,
                        EstadoIdm: this.alerta.estadoIdm,
                        UbicacionIdm: this.alerta.ubicacionIdm,
                        Horas: this.alerta.horas,
                        FechaHoraDesde: this.alerta.fechaHoraDesde,
                        FechaHoraHasta: this.alerta.fechaHoraHasta,
                        Destinatarios: this.alerta.para,
                        DestinatarioVariableIdm: this.alerta.destinatarioVariableIdm,
                        ConCopia: this.alerta.cc,
                        ConCopiaOculta: this.alerta.cco,
                        Asunto: this.alerta.asunto,
                        Archivo: this.alerta.archivo,
                        ReglasAdicionales: this.alerta.reglasAdicionales
                    },
                    { errorBag: this.errorBag })
                .then((res) => {
                    if (!this.errorBag.hasErrors()) {
                        this.uiService.showMessageSuccess(loc["Operación confirmada"])
                        this.goBack()
                    } else {
                        this.uiService.showMessageError(loc["Operación rechazada"])
                    }
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        async getAlerta(id) {
            this.uiService.showSpinner(true)
            return new ajax()
                .get(GET_ALERTA_URL, { Id: id })
                .then((res) => {
                    this.alerta = res;
                    this.alerta.para = this.alerta.para.split(';').filter(email => email);
                    this.alerta.cc = this.alerta.cc.split(';').filter(email => email);
                    this.alerta.cco = this.alerta.cco.split(';').filter(email => email);
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        goBack() {
            this.$router.push({ name: "list" });
        },
        validateNumericValues(event) {
            this.uiService.admitOnyIntegersAndPositiveNumbers(event);
            if (event.target.value > MAX_HORAS_PERMITIDAS) {
                this.uiService.showMessageError(`${loc['Horas no puede ser mayor a']} ${MAX_HORAS_PERMITIDAS}.`)
                this.alerta.horas = MAX_HORAS_PERMITIDAS;
            }
        },
        onArchivosImportados(archivos) {
            this.alerta.archivo = archivos[0];
        }
    },
};
</script>