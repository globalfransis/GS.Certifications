<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 row-gap-0 sticky-header mt-4">
                <div class="row col-12 d-flex">
                    <p class="h5 col-6">Detalle Comprobante {{ nro }}</p>
                    <div class="col-6 gap-4 d-flex justify-content-end">
                        <!-- QR leído -->
                        <div v-if="comprobante.validacionQR" :key="`qrLeido-${comprobante.id}`"
                            class="d-flex gap-1 align-items-center text-success">
                            <i class="fas fa-check-circle"></i>
                            <span>QR leído</span>
                        </div>

                        <div v-else :key="`qrNoLeido-${comprobante.id}`"
                            class="d-flex gap-1 align-items-center text-danger">
                            <i class="fas fa-times-circle"></i>
                            <span>QR no leído</span>
                        </div>

                        <div v-if="comprobante.estadoValidacionARCAQRId == NO_VALIDADA"
                            :key="`qrNoConstatado-${comprobante.id}`"
                            class="d-flex gap-1 align-items-center text-warning-darker">
                            <i class="fas fa-question-circle"></i>
                            <span>QR sin constatar ARCA</span>
                        </div>

                        <div v-if="comprobante.estadoValidacionARCAQRId == RECHAZADA"
                            :key="`qrInvalido-${comprobante.id}`" class="d-flex gap-2 align-items-center text-danger">
                            <a class="btn btn-outline btn-sm" title="Observaciones ARCA" data-toggle="tooltip"
                                data-bs-toggle="modal" :data-bs-target="`#${observacionesARCAQRId}`">
                                <i class="fas fa-times-circle"></i>
                            </a>
                            <span>QR inválido ARCA</span>
                        </div>


                        <div v-if="comprobante.estadoValidacionARCAQRId == ERROR_VALIDACION"
                            :key="`qrErrorComunicacion-${comprobante.id}`"
                            class="d-flex gap-1 align-items-center text-danger">
                            <a class="text-danger p-0" style="border: 0 !important;" title="Observaciones ARCA" data-toggle="tooltip"
                                data-bs-toggle="modal" :data-bs-target="`#${observacionesARCAQRId}`">
                                <i class="fas fa-times-circle"></i>
                            </a>
                            <span>QR error comunicación ARCA</span>
                        </div>

                        <div v-if="comprobante.estadoValidacionARCAQRId == VALIDADA"
                            :key="`qrConstatado-${comprobante.id}`"
                            class="d-flex gap-1 align-items-center text-success">
                            <i class="fas fa-check-circle"></i>
                            <span>QR constatado ARCA</span>
                        </div>


                        <div v-if="comprobante.estadoValidacionARCAId == NO_VALIDADA"
                            :key="`cmpSinConstatar-${comprobante.id}`"
                            class="d-flex gap-1 align-items-center text-warning-darker">
                            <i class="fas fa-question-circle"></i>
                            <span>Comp. sin constatar ARCA</span>
                        </div>

                        <div v-if="comprobante.estadoValidacionARCAId == RECHAZADA"
                            :key="`cmpInvalido-${comprobante.id}`" class="d-flex gap-1 align-items-center text-danger">
                            <a class="text-danger p-0" style="border: 0 !important;" title="Observaciones ARCA" data-toggle="tooltip"
                                data-bs-toggle="modal" :data-bs-target="`#${observacionesARCAId}`">
                                <i class="fas fa-times-circle"></i>
                            </a>
                            <span>Comp. inválido ARCA</span>
                        </div>


                        <div v-if="comprobante.estadoValidacionARCAId == ERROR_VALIDACION"
                            :key="`cmpErrorComunicacion-${comprobante.id}`"
                            class="d-flex gap-1 align-items-center text-danger">
                            <a class="text-danger p-0" style="border: 0 !important;" title="Observaciones ARCA" data-toggle="tooltip"
                                data-bs-toggle="modal" :data-bs-target="`#${observacionesARCAId}`">
                                <i class="fas fa-times-circle"></i>
                            </a>
                            <span>Comp. error comunicación ARCA</span>
                        </div>

                        <div v-if="comprobante.estadoValidacionARCAId == VALIDADA"
                            :key="`cmpConstatado-${comprobante.id}`"
                            class="d-flex gap-1 align-items-center text-success">
                            <i class="fas fa-check-circle"></i>
                            <span>Comp. constatado ARCA</span>
                        </div>

                        <comprobanteEstado-label :value="comprobante.comprobanteEstadoId" />

                        <cancel-button class="ms-2" @click="goBack">Volver</cancel-button>
                    </div>
                </div>
                <p class="h5 col-12">{{ comprobante.proveedor }}</p>
                <div>
                    <a id="splitLayoutButton" @click="setLayout(LayoutMode.Split)" title="Mostrar vista dividida"
                        data-toggle="tooltip" class="ms-2 btn btn-primary px-2 py-1">
                        <i class="fas fa-columns" aria-hidden="true"></i>
                    </a>
                    <a id="fileLayoutButton" @click="setLayout(LayoutMode.File)" title="Mostrar solo el documento"
                        data-toggle="tooltip" class="btn btn-primary px-2 py-1">
                        <i class="fas fa-file-pdf" aria-hidden="true"></i>
                    </a>
                    <a id="formLayoutButton" @click="setLayout(LayoutMode.Form)" title="Mostrar solo el formulario"
                        data-toggle="tooltip" class="btn btn-primary px-2 py-1">
                        <i class="fas fa-file" aria-hidden="true"></i>
                    </a>
                </div>
            </div>
            <div class="card mt-2">
                <div class="card-body">
                    <div class="row form-container">

                        <div class="col-12 row">
                            <div :id="comprobanteArchivoDivId" class="col-6" style="height: 150vh;">
                                <!-- <div class="col-12 d-flex justify-content-between align-items-center mb-4">
                                    <p class="h5 m-0">Sielse srl.pdf</p>
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("Cabecera") }}
                                    </span>
                                </div> -->
                                <iframe
                                    :src="`${currentLocation}/SupplierWebRepository/Comprobantes/cmp_${comprobante.guid}/${comprobante.nombreArchivo}/${comprobante.nombreArchivo}`"
                                    style="width: 100%; height: 100%; border: none;">
                                </iframe>
                            </div>
                            <div :id="comprobanteFormularioDivId" class="col-6 row">
                                <!-- <div class="col-12 d-flex justify-content-between align-items-center mb-4">
                                    <p class="h5 m-0">Cabecera</p>
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("Cabecera") }}
                                    </span>
                                </div> -->
                                <div class="form-group col-lg-6 col-sm-12 mb-2">
                                    <label class="control-label">Tipo Comprobante</label>
                                    <comprobanteTipo-select v-model="comprobante.comprobanteTipoId" />
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("comprobanteTipoId") }}
                                    </span>
                                </div>
                                <div class="form-group col-lg-6 col-sm-12 mb-2">
                                    <label class="control-label">Fecha Emisión</label>
                                    <input type="date" class="form-control" v-model="comprobante.fechaEmision">
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("fechaEmision") }}
                                    </span>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-2">
                                    <label class="control-label">Punto de Venta</label>
                                    <input type="text" class="form-control"
                                        :value="comprobante.puntoVenta | puntoVenta">
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("puntoVenta") }}
                                    </span>
                                </div>
                                <div class="form-group col-lg-6 col-sm-12 mb-2">
                                    <label class="control-label">Número</label>
                                    <input type="text" class="form-control"
                                        :value="comprobante.numero | nroComprobante">
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("numero") }}
                                    </span>
                                </div>

                                <!-- <div class="form-group col-lg-12 col-sm-12 mb-4">
                                        <label class="control-label">Domicilio Emisor</label>
                                        <input type="text" class="form-control" v-model="comprobante.domicilioPro">
                                        <span class="text-danger field-validation-error">
                                            {{ errorBag.get("domicilioPro") }}
                                        </span>
                                    </div> -->

                                <div class="form-group col-lg-6 col-sm-12 mb-2">
                                    <label class="control-label">Moneda</label>
                                    <currency-select v-model="comprobante.monedaId" />
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("monedaId") }}
                                    </span>
                                </div>
                                <div class="form-group col-lg-6 col-sm-12 mb-2">
                                    <label class="control-label">Cotización</label>
                                    <input class="form-control" :value="comprobante.cotizacion | currency">
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("cotizacion") }}
                                    </span>
                                </div>
                                <div class="form-group col-lg-6 col-sm-12 mb-2">
                                    <label class="control-label">Remito</label>
                                    <input type="text" class="form-control" v-model="comprobante.nroRemito">
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("nroRemito") }}
                                    </span>
                                </div>
                                <div class="form-group col-lg-6 col-sm-12 mb-2">
                                    <label class="control-label">Orden Compra</label>
                                    <input type="text" class="form-control" v-model="comprobante.nroOrdenCompra">
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("nroOrdenCompra") }}
                                    </span>
                                </div>
                                <div class="form-group col-lg-6 col-sm-12 mb-2">
                                    <label class="control-label">Fecha Vencimiento</label>
                                    <input type="date" class="form-control" v-model="comprobante.fechaVencimiento">
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("fechaVencimiento") }}
                                    </span>
                                </div>
                                <div class="form-group col-lg-6 col-sm-12 mb-2">
                                    <label class="control-label">Condición Venta</label>
                                    <condicionVenta-select v-model="comprobante.condicionVentaId" />
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("condicionVenta") }}
                                    </span>
                                </div>
                                <div class="form-group col-lg-6 col-sm-12 mb-2">
                                    <label class="control-label">Tipo Código Autorización</label>
                                    <codigoAutorizacionTipo-select v-model="comprobante.tipoCodigoAutorizacionId" />
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("tipoCodigoAutorizacionId") }}
                                    </span>
                                </div>
                                <div class="form-group col-lg-6 col-sm-12 mb-2">
                                    <label class="control-label">Código Autorización</label>
                                    <input type="text" class="form-control" v-model="comprobante.codigoAutorizacion">
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("codigoAutorizacion") }}
                                    </span>

                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4 mt-4">
                                    <label class="control-label">Importe Neto</label>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4 mt-4">
                                    <input class="form-control" :value="comprobante.importeNeto | currency">
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <label class="control-label">Importe Bonificación</label>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <input class="form-control" :value="comprobante.importeBonificacion | currency">
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <label class="control-label">IVA</label>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <input class="form-control" :value="comprobante.importeIVA | currency">
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <label class="control-label">Imp. Internos</label>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <input class="form-control"
                                        :value="comprobante.importeImpuestosInternos | currency">
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <label class="control-label">Imp. Provinciales</label>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <input class="form-control"
                                        :value="comprobante.importeOtrosTributosProv | currency">
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <label class="control-label">Perc. IVA</label>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <input class="form-control" :value="comprobante.importePercepcionIVA | currency">
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <label class="control-label">Perc. IIBB</label>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <input class="form-control" :value="comprobante.importePercepcionIIBB | currency">
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <label class="control-label">Importe Total</label>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <input class="form-control" :value="comprobante.importeTotal | currency">
                                </div>

                                <!-- <div class="col-12 mb-2 mt-2 d-flex justify-content-end">
                                    <accept-button :disabled="!grants.create" @click="verifyAsync">Constatar
                                        ARCA</accept-button>
                                </div> -->


                                <hr>


                                <div class="col-12 d-flex justify-content-between align-items-center mt-4 mb-4">
                                    <p class="h5 m-0">Detalle</p>
                                </div>
                                <table :id="`${idTable}`" class="table table-bordered table-hover">
                                    <thead class="table-top">
                                        <tr class="text-center align-middle">
                                            <th class="w-2" scope="col">Cant.</th>
                                            <!-- <th class="w-2" scope="col">Un. Medida</th> -->
                                            <th class="w-15" scope="col">Descripción</th>
                                            <th class="w-5" scope="col">Precio Unitario</th>
                                            <!-- <th class="w-5" scope="col">Importe Bonif.</th> -->
                                            <th class="w-5"
                                                v-if="comprobante.comprobanteTipoId != FACTURA_B && comprobante.comprobanteTipoId != FACTURA_C"
                                                scope="col">IVA (%)</th>
                                            <th class="w-5" scope="col">Subtotal</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-if="comprobante.detalles.filter(d => !d.deleted).length === 0"
                                            class="no-data">
                                            <td colspan="100" class="text-center">{{ NO_DATA_MESSAGE }}</td>
                                        </tr>
                                        <template v-for="(cd, index) in comprobante.detalles">
                                            <tr v-if="cd.mode.isDetail && !cd.deleted" :key="index">
                                                <td data-toggle="tooltip" class="text-end align-middle">
                                                    {{ cd.cantidad }}</td>
                                                <!-- <td data-toggle="tooltip" class="align-middle">
                                                    {{ cd.unidadMedida }}
                                                </td> -->
                                                <td data-toggle="tooltip" class="align-middle">
                                                    {{ cd.detalle }}</td>
                                                <td data-toggle="tooltip" class="text-end align-middle">
                                                    {{ cd.precioUnitario | currency }}</td>
                                                <!-- <td data-toggle="tooltip" class="text-end align-middle">{{
                        cd.importeBonificacion | currency }}</td> -->
                                                <td v-if="comprobante.comprobanteTipoId != FACTURA_B && comprobante.comprobanteTipoId != FACTURA_C"
                                                    data-toggle="tooltip" class="text-end align-middle text-center">{{
                        cd.alicuota }}</td>
                                                <td data-toggle="tooltip" class="text-end align-middle">
                                                    {{ cd.subtotal | currency }}</td>
                                                <!-- <td data-toggle="tooltip" class="text-end align-middle">
                        {{ cd.importeIVA | currency }}</td> -->
                                            </tr>
                                        </template>
                                    </tbody>
                                </table>

                                <hr>

                                <div class="col-12 d-flex justify-content-between align-items-center mt-4 mb-4">
                                    <p class="h5 m-0">Impuestos</p>
                                </div>
                                <table :id="`${idTableImpuestos}`" class="table table-bordered table-hover">
                                    <thead class="table-top">
                                        <tr class="text-center align-middle">
                                            <th class="w-5" scope="col">Impuesto</th>
                                            <th class="w-10" scope="col">Descripción</th>
                                            <th class="w-5" scope="col">Subtotal</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-if="comprobante.impuestos.filter(d => !d.deleted).length === 0"
                                            class="no-data">
                                            <td colspan="100" class="text-center">{{ NO_DATA_MESSAGE }}</td>
                                        </tr>
                                        <template v-for="(cd, index) in comprobante.impuestos">
                                            <tr v-if="cd.mode.isDetail && !cd.deleted" :key="index">
                                                <td data-toggle="tooltip" class="align-middle">
                                                    {{ cd.impuesto }}
                                                </td>
                                                <td data-toggle="tooltip" class="align-middle">
                                                    {{ cd.descripcion }}</td>
                                                <td data-toggle="tooltip" class="text-end align-middle">
                                                    {{ cd.importeTotal | currency }}</td>
                                            </tr>
                                        </template>
                                    </tbody>
                                </table>

                                <hr>

                                <div class="col-12 d-flex justify-content-between align-items-center mt-4 mb-4">
                                    <p class="h5 m-0">Percepciones</p>
                                </div>
                                <table :id="`${idTablePercepciones}`" class="table table-bordered table-hover">
                                    <thead class="table-top">
                                        <tr class="text-center align-middle">
                                            <th class="w-5" scope="col">Percepción</th>
                                            <th class="w-10" scope="col">Descripción</th>
                                            <th class="w-5" scope="col">Alícuota (%)</th>
                                            <th class="w-5" scope="col">Subtotal</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-if="comprobante.percepciones.filter(d => !d.deleted).length === 0"
                                            class="no-data">
                                            <td colspan="100" class="text-center">{{ NO_DATA_MESSAGE }}</td>
                                        </tr>
                                        <template v-for="(cd, index) in comprobante.percepciones">
                                            <tr v-if="cd.mode.isDetail && !cd.deleted" :key="index">
                                                <td data-toggle="tooltip" class="align-middle">
                                                    {{ cd.percepcion }}
                                                </td>
                                                <td data-toggle="tooltip" class="align-middle">
                                                    {{ cd.descripcion }}</td>
                                                <td data-toggle="tooltip" class="text-end align-middle">
                                                    {{ cd.alicuota }}</td>
                                                <td data-toggle="tooltip" class="text-end align-middle">
                                                    {{ cd.importeTotal | currency }}</td>
                                            </tr>
                                        </template>
                                    </tbody>
                                </table>

                                <hr>

                                <div v-if="comprobante.comprobanteEstadoId == RECHAZADO"
                                    class="form-group col-lg-12 col-sm-12 mb-4">
                                    <label class="control-label">Motivo Rechazo</label>
                                    <textarea id="motivoRechazoTxtArea" disabled class="form-control" cols="20" rows="4"
                                        v-model="comprobante.motivoRechazo"></textarea>
                                </div>

                                <div v-if="comprobante.comprobanteEstadoId == APROBADO || comprobante.comprobanteEstadoId == AUTORIZADO || comprobante.comprobanteEstadoId == RECHAZADO"
                                    class="form-group col-lg-12 col-sm-12 mb-4">
                                    <label class="control-label">Período</label>
                                    <periodo-select v-model="comprobante.periodoId"
                                        @selected="updatePeriodoAsync($event)"
                                        :disabled="!grants.update || comprobante.comprobanteEstadoId == AUTORIZADO || comprobante.comprobanteEstadoId == RECHAZADO" />
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("periodo") }}
                                    </span>
                                </div>


                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div class="col-12 d-flex justify-content-end gap-2 mb-3 mt-3" v-if="currentLayoutMode != LayoutMode.File">
            <edit-button :disabled="comprobante.propietarioActualId != BACKOFFICE"
                v-if="comprobante.propietarioActualId == BACKOFFICE || comprobante.comprobanteEstadoId == CONFIRMADO || comprobante.comprobanteEstadoId == APROBADO || comprobante.comprobanteEstadoId == AUTORIZADO"
                @click="update">Editar</edit-button>
            <accept-button v-if="comprobante.comprobanteEstadoId == BORRADOR"
                :disabled="!grants.update || comprobante.estadoValidacionARCAId == NO_VALIDADA" @click="confirmAsync">
                Confirmar</accept-button>
            <accept-button v-if="comprobante.comprobanteEstadoId == CONFIRMADO"
                :disabled="!grants.update || comprobante.estadoValidacionARCAId == NO_VALIDADA" @click="approveAsync">
                Aprobar</accept-button>
            <accept-button v-if="comprobante.comprobanteEstadoId == APROBADO"
                :disabled="!grants.update || comprobante.estadoValidacionARCAId == NO_VALIDADA" @click="authorizeAsync">
                Autorizar</accept-button>

            <button :disabled="!grants.delete" class="btn btn-danger btn-sm" @click="deleteAsync">
                Eliminar
            </button>



            <comprobanteRechazo-modal :comprobante="comprobante" @comprobanteRechazado="rejectAsync($event)"
                :idModal="`${comprobanteRechazoId}-${comprobante.id}`" />
            <!-- TODO: chequear si tambien puede ser rechazado cuando ya fue Autorizado o incluso si está en Borrador -->

            <a :disabled="comprobante.propietarioActualId != BACKOFFICE && comprobante.comprobanteEstadoId != BORRADOR"
                v-if="comprobante.comprobanteEstadoId == CONFIRMADO || comprobante.comprobanteEstadoId == APROBADO"
                class="btn btn-outline-danger btn-sm" title="Rechazar comprobante" data-toggle="tooltip"
                data-bs-toggle="modal" :data-bs-target="`#${comprobanteRechazoId}-${comprobante.id}`"
                style="cursor:pointer">
                Rechazar
            </a>

            <observacionesArca-modal :observaciones="comprobante.observacionesARCAQR"
                :idModal="`${observacionesARCAQRId}`" />
            <observacionesArca-modal :observaciones="comprobante.observacionesARCA"
                :idModal="`${observacionesARCAId}`" />

        </div>
    </div>
</template>

<script>
import CancelButton from "@/components/forms/cancel-button.vue";
import UiService from "@/common/uiService";
import EditButton from "@/components/forms/edit-button.vue";

import Comprobante from './Comprobante' // Modificar por la clase dto correspondiente

import commonMixin from '@/Common/Mixins/commonMixin';
import detailMixin from '@/Common/Mixins/detailMixin';

import AcceptButton from "@/components/forms/accept-button.vue";

import CancelButton from "@/components/forms/cancel-button.vue";
import UiService from "@/common/uiService";

import Comprobante from './Comprobante';

import commonMixin from '@/Common/Mixins/commonMixin';

import importarComprobante from '@/Components/ImportadorArchivos/Importadores/importar-comprobante'

import comprobanteDetalleForm from './comprobanteDetalle-form'
import comprobanteImpuestoForm from './comprobanteImpuesto-form'
import comprobantePercepcionForm from './comprobantePercepcion-form'
import comprobanteRechazoModal from './Modal/comprobanteRechazo-modal'

import inlineEdit from "@/components/forms/inline-edit-button.vue";
import inlineDelete from "@/components/forms/inline-delete-button.vue";
import inlineCancel from "@/components/forms/inline-cancel-button.vue";

import categoriaTipoSelect from "@/Selects/categoriaTipo-select.vue";
import comprobanteTipoSelect from "@/Selects/comprobanteTipo-select.vue";
import codigoAutorizacionTipoSelect from "@/Selects/codigoAutorizacionTipo-select.vue";
import currencySelect from "@/Selects/currency-select.vue";
import unidadMedidaSelect from "@/Selects/unidadMedida-select.vue";
import percepcionSelect from "@/Selects/percepcion-select.vue";
import impuestoSelect from "@/Selects/impuesto-select.vue";
import periodoSelect from "@/Selects/periodo-select.vue";

import comprobanteEstadoSelect from "@/Selects/comprobanteEstado-select.vue";
import comprobanteEstadoLabel from "@/Selects/comprobanteEstado-label.vue";

import condicionVentaSelect from "@/Selects/condicionVenta-select.vue";

import LayoutMode from "./LayoutMode";

import observacionesArcaModal from './Modal/observacionesArca-modal'

const NO_DATA_MESSAGE = "No hay datos";

// Estados ARCA
const VALIDADA = 1;
const RECHAZADA = 2;
const ERROR_VALIDACION = 3;
const NO_VALIDADA = 4;

// Estados Comprobante
const CONFIRMADO = 4;
const APROBADO = 6;
const RECHAZADO = 7;
const BORRADOR = 8;
const AUTORIZADO = 9;

// Tipos de comprobantes
const FACTURA_B = 6;
const FACTURA_C = 17;

// Origen de comprobante
const PROVEEDOR = 1;
const BACKOFFICE = 2;
const CORREO = 3;

export default {
    components: {
        AcceptButton,
        CancelButton,
        importarComprobante,
        comprobanteDetalleForm,
        comprobanteImpuestoForm,
        comprobantePercepcionForm,
        inlineEdit,
        inlineDelete,
        inlineCancel,
        categoriaTipoSelect,
        codigoAutorizacionTipoSelect,
        currencySelect,
        comprobanteTipoSelect,
        unidadMedidaSelect,
        impuestoSelect,
        percepcionSelect,
        EditButton,
        comprobanteEstadoSelect,
        periodoSelect,
        comprobanteEstadoSelect,
        comprobanteEstadoLabel,
        condicionVentaSelect,
        comprobanteRechazoModal,
        observacionesArcaModal

    },
    mixins: [commonMixin, detailMixin],
    name: "comprobante-detail",
    data: function () {
        return {
            comprobante: new Comprobante(),
            idTable: `__comprobanteDetalles`,
            idTableImpuestos: `__comprobanteImpuestos`,
            idTablePercepciones: `__comprobantePercepciones`,
            uiService: new UiService(),
            CONFIRMADO,
            RECHAZADO,
            nro: '',
            NO_DATA_MESSAGE,
            periodoId: null,
            comprobanteRechazoId: "comprobanteRechazoModal",
            comprobanteArchivoDivId: "__comprobanteArchivo",
            comprobanteFormularioDivId: "__comprobanteFormulario",
            observacionesARCAQRId: "__observacionesARCAQRId",
            observacionesARCAId: "__observacionesARCAId",
            LayoutMode,
            currentLayoutMode: LayoutMode.Split,
            // Estados validacion en ARCA
            VALIDADA,
            RECHAZADA,
            ERROR_VALIDACION,
            NO_VALIDADA,
            // Estados comprobante
            APROBADO,
            AUTORIZADO,
            CONFIRMADO,
            RECHAZADO,
            BORRADOR,
            // Tipos de comprobante
            FACTURA_B,
            FACTURA_C,
            // Origen de comprobante
            PROVEEDOR,
            BACKOFFICE,
            CORREO
        };
    },
    computed: {
        currentLocation() {
            return window.location.origin;
        },
        grants() {
            return this.$store.getters.getGrants;
        },
        errorBag() {
            return this.$store.getters.getErrorBag;
        }
    },
    async mounted() {
        await this.init();
    },
    methods: {
        setLayout(mode) {
            this.currentLayoutMode = mode;
            this.currentLayoutMode.apply(this.comprobanteArchivoDivId, this.comprobanteFormularioDivId);
        },
        async init() {
            if (this.$route.params.id) {
                await this.getAsync(this.$route.params.id);
                await this.getDatosProveedorAsync();
                // Referencia al valor original
                this.periodoId = this.comprobante.periodoId;

                // Esto ya no es necesario
                // if (this.comprobante.comprobanteEstadoId == CONFIRMADO) {
                //     document.getElementById("motivoRechazoTxtArea").removeAttribute("disabled")
                // }
            }
            else this.goBack();
        },
        async getDatosProveedorAsync() {
            this.uiService.showSpinner(true);
            await this.$store.dispatch("getDatosProveedorAsync", this.comprobante.empresaId)
                .then((res) => {
                    this.comprobante.proveedor = res.razonSocial;
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        async getAsync(id) {
            this.uiService.showSpinner(true);
            await this.$store.dispatch("getAsync", id)
                .then((res) => {
                    this.comprobante = new Comprobante(res);
                    this.nro = this.comprobante.comprobante;
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        async updatePeriodoAsync(periodo) {
            if (this.periodoId != periodo.id) {
                if (
                    await this.uiService.confirmActionModal(
                        `¿Está seguro que desea asignar el comprobante ${String(parseInt(this.comprobante.numero)).padStart(8, '0')} al período <b>${periodo.vigencia}</b>?`,
                        "Aceptar",
                        "Cancelar"
                    )
                ) {
                    this.uiService.showSpinner(true)
                    await this.$store.dispatch("updatePeriodoAsync", this.comprobante)
                        .then(() => {
                            if (!this.errorBag.hasErrors()) {
                                this.uiService.showMessageSuccess("Operación confirmada");
                                // Actualizamos el valor original si la actualización fue ok
                                this.periodoId = periodo.id;
                                this.getAsync(this.comprobante.id);
                            } else {
                                this.uiService.showMessageError("Operación rechazada");
                                // Revertimos al valor original en caso de error
                                this.comprobante.periodoId = this.periodoId;
                            }
                        })
                        .finally(() => {
                            this.uiService.showSpinner(false);
                        });
                } else {
                    this.comprobante.periodoId = this.periodoId;
                }
            }
        },
        async deleteAsync() {
            if (
                await this.uiService.confirmActionModal(
                    `¿Está seguro que desea eliminar el comprobante ${String(parseInt(this.comprobante.numero)).padStart(8, '0')}?`,
                    "Aceptar",
                    "Cancelar"
                )
            ) {
                this.uiService.showSpinner(true)
                await this.$store.dispatch("deleteAsync", this.comprobante)
                    .then(() => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            this.getAsync(this.comprobante.id);
                        } else {
                            this.uiService.showMessageError("Operación rechazada")
                        }
                    })
                    .finally(() => {
                        this.uiService.showSpinner(false);
                    });
            }
        },
        async confirmAsync() {
            if (
                await this.uiService.confirmActionModal(
                    `¿Está seguro que desea Confirmar el comprobante ${String(parseInt(this.comprobante.numero)).padStart(8, '0')}?`,
                    "Aceptar",
                    "Cancelar"
                )
            ) {

                this.uiService.showSpinner(true)
                await this.$store.dispatch("confirmAsync", this.comprobante)
                    .then(() => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            this.getAsync(this.comprobante.id);
                        } else {
                            this.uiService.showMessageError("Operación rechazada")
                        }
                    })
                    .finally(() => {
                        this.uiService.showSpinner(false);
                    });
            }
        },
        async authorizeAsync() {
            if (
                await this.uiService.confirmActionModal(
                    `¿Está seguro que desea Autorizar el comprobante ${String(parseInt(this.comprobante.numero)).padStart(8, '0')}?`,
                    "Aceptar",
                    "Cancelar"
                )
            ) {
                this.uiService.showSpinner(true)
                await this.$store.dispatch("authorizeAsync", this.comprobante)
                    .then(() => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            this.getAsync(this.comprobante.id);
                        } else {
                            this.uiService.showMessageError("Operación rechazada")
                        }
                    })
                    .finally(() => {
                        this.uiService.showSpinner(false);
                    });
            }
        },
        goDetail() {
            this.$router.push({ name: "detail", params: { id: this.comprobante.id } });
        },
        async approveAsync() {
            if (
                await this.uiService.confirmActionModal(
                    `¿Está seguro que desea Aprobar el comprobante ${String(parseInt(this.comprobante.numero)).padStart(8, '0')}?`,
                    "Aceptar",
                    "Cancelar"
                )
            ) {
                this.uiService.showSpinner(true)
                await this.$store.dispatch("approveAsync", this.comprobante)
                    .then(async () => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            await this.getAsync(this.comprobante.id);
                        } else {
                            this.uiService.showMessageError("Operación rechazada")
                        }
                    })
                    .finally(() => {
                        this.uiService.showSpinner(false);
                    });
            }
        },
        async rejectAsync() {
            this.uiService.showSpinner(true)
            await this.$store.dispatch("rejectAsync", this.comprobante)
                .then(async () => {
                    if (!this.errorBag.hasErrors()) {
                        this.uiService.showMessageSuccess("Operación confirmada")
                        await this.getAsync(this.comprobante.id);
                    } else {
                        this.uiService.showMessageError("Operación rechazada")
                    }
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        goBack() {
            this.$router.push({ name: "home", query: { fromDetail: true } });
        },
        update() {
            this.$router.push({ name: "edit", params: { id: this.comprobante.id } });
        }
    },
};
</script>