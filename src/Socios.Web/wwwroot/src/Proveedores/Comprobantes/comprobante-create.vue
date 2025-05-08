<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 d-flex justify-content-between sticky-header mt-4">
                <div class="row col-12 d-flex">
                    <p class="h5 col-6">Nuevo Comprobante</p>
                    <div class="col-6 d-flex justify-content-end">
                        <cancel-button class="ms-2" @click="cancel">Volver</cancel-button>
                    </div>
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
            </div>
            <div class="card mt-2">
                <div class="card-body">
                    <div class="row">
                        <div class="form-group col-sm-12 mb-4 row">
                            <div class="col-8">
                                <label class="control-label">Importar Comprobante</label>
                                <importar-comprobante idModal="__modal_ComprobanteArchivo" ref="importarComprobante"
                                    title="Comprobante" :disabled="!updateGrant"
                                    @archivosUpdated="onComprobanteAnalyzed($event)" />
                                <span class="text-danger field-validation-error">
                                    {{ errorBag.get("comprobanteError") }}
                                </span>
                            </div>
                        </div>

                        <hr>

                        <div class="col-12 row">
                            <div class="col-6" :id="comprobanteArchivoDivId" style="height: 150vh;">
                                <div class="col-12 d-flex justify-content-between align-items-center mb-4">
                                    <p>No se ha cargado ningún archivo</p>
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("Cabecera") }}
                                    </span>
                                </div>
                            </div>
                            <div class="col-6 row" :id="comprobanteFormularioDivId">
                                <div class="form-group col-lg-6 col-sm-12 mb-2 required">
                                    <label class="control-label">Tipo Comprobante</label>
                                    <comprobanteTipo-select v-model="comprobante.comprobanteTipoId" />
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("comprobanteTipoId") }}
                                    </span>
                                </div>
                                <div class="form-group col-lg-6 col-sm-12 mb-2 required">
                                    <label class="control-label">Fecha Emisión</label>
                                    <input type="date" class="form-control" v-model="comprobante.fechaEmision">
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("fechaEmision") }}
                                    </span>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-2 required">
                                    <label class="control-label">Punto de Venta</label>
                                    <input type="text" class="form-control" v-model="comprobante.puntoVenta">
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("puntoVenta") }}
                                    </span>
                                </div>
                                <div class="form-group col-lg-6 col-sm-12 mb-2 required">
                                    <label class="control-label">Número</label>
                                    <input type="text" class="form-control" v-model="comprobante.numero">
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("numero") }}
                                    </span>
                                </div>

                                <!-- <div class="form-group col-lg-12 col-sm-12 mb-4 required">
                                        <label class="control-label">Domicilio Emisor</label>
                                        <input type="text" class="form-control" v-model="comprobante.domicilioPro">
                                        <span class="text-danger field-validation-error">
                                            {{ errorBag.get("domicilioPro") }}
                                        </span>
                                    </div> -->

                                <div class="form-group col-lg-6 col-sm-12 mb-2 required">
                                    <label class="control-label">Moneda</label>
                                    <currency-select v-model="comprobante.monedaId" />
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("monedaId") }}
                                    </span>
                                </div>
                                <div class="form-group col-lg-6 col-sm-12 mb-2">
                                    <label class="control-label">Cotización</label>
                                    <input type="number" step="0.01" class="form-control"
                                        v-model="comprobante.cotizacion">
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

                                <div class="form-group col-lg-6 col-sm-12 mb-2 required">
                                    <label class="control-label">Tipo Código Autorización</label>
                                    <codigoAutorizacionTipo-select v-model="comprobante.tipoCodigoAutorizacionId" />
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("tipoCodigoAutorizacionId") }}
                                    </span>
                                </div>
                                <div class="form-group col-lg-6 col-sm-12 mb-2 required">
                                    <label class="control-label">Código Autorización</label>
                                    <input type="text" class="form-control" v-model="comprobante.codigoAutorizacion">
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("codigoAutorizacion") }}
                                    </span>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4 mt-4 required">
                                    <label class="control-label">Importe Neto</label>
                                </div>
                                <div class="form-group col-lg-6 col-sm-12 mb-4 mt-4">
                                    <input type="number" step="0.01" class="form-control"
                                        v-model="comprobante.importeNeto">
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("totalNeto") }}
                                    </span>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <label class="control-label">Importe Bonificación</label>
                                </div>
                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <input type="number" step="0.01" class="form-control"
                                        v-model="comprobante.importeBonificacion">
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("importeBonificacion") }}
                                    </span>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <label class="control-label">IVA</label>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <input type="number" step="0.01" class="form-control"
                                        v-model="comprobante.importeIVA">
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("importeIVA") }}
                                    </span>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <label class="control-label">Imp. Internos</label>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <input type="number" step="0.01" class="form-control"
                                        v-model="comprobante.importeImpuestosInternos">
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("importeImpuestosInternos") }}
                                    </span>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <label class="control-label">Imp. Provinciales</label>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <input type="number" step="0.01" class="form-control"
                                        v-model="comprobante.importeOtrosTributosProv">
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("importeOtrosTributosProv") }}
                                    </span>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <label class="control-label">Perc. IVA</label>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <input type="number" step="0.01" class="form-control"
                                        v-model="comprobante.importePercepcionIVA">
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("importePercepcionIVA") }}
                                    </span>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <label class="control-label">Perc. IIBB</label>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4">
                                    <input type="number" step="0.01" class="form-control"
                                        v-model="comprobante.importePercepcionIIBB">
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("importePercepcionIIBB") }}
                                    </span>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4 required">
                                    <label class="control-label">Importe Total</label>
                                </div>

                                <div class="form-group col-lg-6 col-sm-12 mb-4 required">
                                    <input type="number" step="0.01" class="form-control"
                                        v-model="comprobante.importeTotal">
                                    <span class="text-danger field-validation-error">
                                        {{ errorBag.get("importeTotal") }}
                                    </span>
                                </div>

                                <!-- <div class="col-12 mb-2 mt-2 d-flex justify-content-end">
                                    <accept-button :disabled="!grants.create" @click="verifyAsync">Constatar
                                        ARCA</accept-button>
                                </div> -->


                                <hr>

                                <div class="col-12 d-flex justify-content-between align-items-center mt-4 mb-4">
                                    <div>
                                        <p class="h5 m-0">Detalle</p>
                                        <span class="text-danger field-validation-error">
                                            {{ errorBag.get("detalles") }}
                                        </span>
                                    </div>
                                    <button type="button" class="btn btn-outline-primary btn-sm" @click="addNewRow()">
                                        <b><i class="fas fa-plus"></i>Agregar</b>
                                    </button>
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
                                            <th class="w-2" scope="col">Acciones</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-if="comprobante.detalles.filter(d => !d.deleted).length === 0"
                                            class="no-data">
                                            <td colspan="100" class="text-center">{{ NO_DATA_MESSAGE }}</td>
                                        </tr>
                                        <template v-for="(cd, index) in comprobante.detalles">
                                            <tr v-if="cd.mode.isDetail && !cd.deleted"
                                                :class="hasErrors(index) ? 'table-danger' : ''" :key="index">
                                                <td data-toggle="tooltip"
                                                    :title="errorBag.get(`detalles[${index}].Cantidad`)"
                                                    :class="errorBag.get(`detalles[${index}].Cantidad`) ? 'border-danger-gs text-danger text-end align-middle' : 'text-end align-middle'">
                                                    {{ cd.cantidad }}</td>
                                                <!-- <td data-toggle="tooltip"
                                                    :title="errorBag.get(`detalles[${index}].UnidadMedidaId`)"
                                                    :class="errorBag.get(`detalles[${index}].UnidadMedidaId`) ? 'border-danger-gs text-danger align-middle' : 'align-middle'">
                                                    <unidadMedida-select disabled v-model="cd.unidadMedidaId" />
                                                </td> -->
                                                <td data-toggle="tooltip"
                                                    :title="errorBag.get(`detalles[${index}].Detalle`)"
                                                    :class="errorBag.get(`detalles[${index}].Detalle`) ? 'border-danger-gs text-danger align-middle' : 'align-middle'">
                                                    {{ cd.detalle }}</td>
                                                <td data-toggle="tooltip"
                                                    :title="errorBag.get(`detalles[${index}].PrecioUnitario`)"
                                                    :class="errorBag.get(`detalles[${index}].PrecioUnitario`) ? 'border-danger-gs text-danger text-end align-middle' : 'text-end align-middle'">
                                                    {{ cd.precioUnitario | currency }}</td>
                                                <!-- <td data-toggle="tooltip" class="text-end align-middle">{{
                            cd.importeBonificacion | currency }}</td> -->
                                                <td v-if="comprobante.comprobanteTipoId != FACTURA_B && comprobante.comprobanteTipoId != FACTURA_C"
                                                    data-toggle="tooltip" class="text-end align-middle text-center">{{
                            cd.alicuota }}</td>
                                                <td data-toggle="tooltip"
                                                    :title="errorBag.get(`detalles[${index}].Subtotal`)"
                                                    :class="errorBag.get(`detalles[${index}].Subtotal`) ? 'border-danger-gs text-danger text-end align-middle' : 'text-end align-middle'">
                                                    {{ cd.subtotal | currency }}</td>
                                                <!-- <td data-toggle="tooltip"
                        :title="errorBag.get(`detalles[${index}].ImporteIVA`)"
                        :class="errorBag.get(`detalles[${index}].ImporteIVA`) ? 'border-danger-gs text-danger text-end align-middle' : 'text-end align-middle'">
                        {{ cd.importeIVA | currency }}</td> -->
                                                <td data-toggle="tooltip" class="text-center align-middle">
                                                    <div class="d-inline-flex">
                                                        <inline-edit @click="update(cd)" :enabled="cd.mode.isDetail" />
                                                        <inline-delete @click="remove(cd, index)"
                                                            :enabled="cd.mode.isDetail" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <comprobanteDetalle-form :key="index" :index="index"
                                                :comprobanteDetalle="cd" @cancel-edit="cancelEdit(index)"
                                                v-if="cd.mode.isEdit" :tipoComprobante="comprobante.comprobanteTipoId"
                                                @edit-finished="editFinished($event, index)" />
                                        </template>
                                    </tbody>
                                </table>

                                <hr>

                                <div class="col-12 d-flex justify-content-between align-items-center mt-4 mb-4">
                                    <div>
                                        <p class="h5 m-0">Impuestos</p>
                                        <span class="text-danger field-validation-error">
                                            {{ errorBag.get("impuestos") }}
                                        </span>
                                    </div>
                                    <button type="button" class="btn btn-outline-primary btn-sm"
                                        @click="addNewImpuesto()">
                                        <b><i class="fas fa-plus"></i>Agregar</b>
                                    </button>
                                </div>

                                <table :id="`${idTableImpuestos}`" class="table table-bordered table-hover mb-0">
                                    <thead class="table-top">
                                        <tr class="text-center align-middle">
                                            <th class="w-5" scope="col">Impuesto</th>
                                            <th class="w-10" scope="col">Descripción</th>
                                            <th class="w-5" scope="col">Subtotal</th>
                                            <th class="w-5" scope="col">Acciones</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-if="comprobante.impuestos.filter(i => !i.deleted).length === 0"
                                            class="no-data">
                                            <td colspan="100" class="text-center">{{ NO_DATA_MESSAGE }}</td>
                                        </tr>
                                        <template v-for="(i, index) in comprobante.impuestos">
                                            <tr v-if="i.mode.isDetail && !i.deleted"
                                                :class="impuestosHasErrors(index) ? 'table-danger' : ''" :key="index">
                                                <td data-toggle="tooltip"
                                                    :title="errorBag.get(`impuestos[${index}].ImpuestoId`)"
                                                    :class="errorBag.get(`impuestos[${index}].ImpuestoId`) ? 'border-danger-gs text-danger align-middle' : 'align-middle'">
                                                    <impuesto-select disabled v-model="i.impuestoId" />
                                                </td>
                                                <td data-toggle="tooltip"
                                                    :title="errorBag.get(`impuestos[${index}].Descripcion`)"
                                                    :class="errorBag.get(`impuestos[${index}].Descripcion`) ? 'border-danger-gs text-danger align-middle' : 'align-middle'">
                                                    {{ i.descripcion }}</td>
                                                <td data-toggle="tooltip"
                                                    :title="errorBag.get(`impuestos[${index}].ImporteTotal`)"
                                                    :class="errorBag.get(`impuestos[${index}].ImporteTotal`) ? 'border-danger-gs text-danger text-end align-middle' : 'text-end align-middle'">
                                                    {{ i.importeTotal | currency }}</td>
                                                <td data-toggle="tooltip" class="text-center align-middle">
                                                    <div class="d-inline-flex">
                                                        <inline-edit @click="updateImpuesto(i)"
                                                            :enabled="i.mode.isDetail" />
                                                        <inline-delete @click="removeImpuesto(i, index)"
                                                            :enabled="i.mode.isDetail" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <comprobanteImpuesto-form :key="index" :index="index"
                                                :comprobanteImpuesto="i" @cancel-edit="cancelEditImpuesto(index)"
                                                v-if="i.mode.isEdit"
                                                @edit-finished="editFinishedImpuesto($event, index)" />
                                        </template>
                                    </tbody>
                                </table>

                                <hr>

                                <div class="col-12 d-flex justify-content-between align-items-center mt-4 mb-4">
                                    <div>
                                        <p class="h5 m-0">Percepciones</p>
                                        <span class="text-danger field-validation-error">
                                            {{ errorBag.get("percepciones") }}
                                        </span>
                                    </div>
                                    <button type="button" class="btn btn-outline-primary btn-sm"
                                        @click="addNewPercepcion()">
                                        <b><i class="fas fa-plus"></i>Agregar</b>
                                    </button>
                                </div>
                                <table :id="`${idTablePercepciones}`" class="table table-bordered table-hover mb-0">
                                    <thead class="table-top">
                                        <tr class="text-center align-middle">
                                            <th class="w-5" scope="col">Percepción</th>
                                            <th class="w-10" scope="col">Descripción</th>
                                            <th class="w-5" scope="col">Alícuota (%)</th>
                                            <th class="w-5" scope="col">Subtotal</th>
                                            <th class="w-5" scope="col">Acciones</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-if="comprobante.percepciones.filter(d => !d.deleted).length === 0"
                                            class="no-data">
                                            <td colspan="100" class="text-center">{{ NO_DATA_MESSAGE }}</td>
                                        </tr>
                                        <template v-for="(p, index) in comprobante.percepciones">
                                            <tr v-if="p.mode.isDetail && !p.deleted"
                                                :class="percepcionesHasErrors(index) ? 'table-danger' : ''"
                                                :key="index">
                                                <td data-toggle="tooltip"
                                                    :title="errorBag.get(`percepciones[${index}].PercepcionId`)"
                                                    :class="errorBag.get(`percepciones[${index}].PercepcionId`) ? 'border-danger-gs text-danger align-middle' : 'align-middle'">
                                                    <percepcion-select disabled v-model="p.percepcionId" />
                                                </td>
                                                <td data-toggle="tooltip"
                                                    :title="errorBag.get(`percepciones[${index}].Descripcion`)"
                                                    :class="errorBag.get(`percepciones[${index}].Descripcion`) ? 'border-danger-gs text-danger align-middle' : 'align-middle'">
                                                    {{ p.descripcion }}</td>
                                                <td data-toggle="tooltip"
                                                    :title="errorBag.get(`percepciones[${index}].Alicuota`)"
                                                    :class="errorBag.get(`percepciones[${index}].Alicuota`) ? 'border-danger-gs text-danger text-end align-middle' : 'text-end align-middle'">
                                                    {{ p.alicuota }}</td>
                                                <td data-toggle="tooltip"
                                                    :title="errorBag.get(`percepciones[${index}].ImporteTotal`)"
                                                    :class="errorBag.get(`percepciones[${index}].ImporteTotal`) ? 'border-danger-gs text-danger text-end align-middle' : 'text-end align-middle'">
                                                    {{ p.importeTotal | currency }}</td>
                                                <td data-toggle="tooltip" class="text-center align-middle">
                                                    <div class="d-inline-flex">
                                                        <inline-edit @click="updatePercepcion(p)"
                                                            :enabled="p.mode.isDetail" />
                                                        <inline-delete @click="removePercepcion(p, index)"
                                                            :enabled="p.mode.isDetail" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <comprobantePercepcion-form :key="index" :index="index"
                                                :importeNeto="comprobante.importeNeto" :comprobantePercepcion="p"
                                                @cancel-edit="cancelEditPercepcion(index)" v-if="p.mode.isEdit"
                                                @edit-finished="editFinishedPercepcion($event, index)" />
                                        </template>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </div>
        <!-- <div class="col-12 d-flex justify-content-end gap-2 mb-3 mt-3">
            <button tabindex="12" @click.prevent="saveAsync" class="btn btn-secondary btn-sm">
                <i class="fas fa-save"></i>
                Guardar
            </button>
            <accept-button :disabled="!grants.create" @click="createAsync">Validar</accept-button>
            <cancel-button @click="cancel">Cancelar</cancel-button>
        </div> -->
    </div>
</template>

<script>
import AcceptButton from "@/components/forms/accept-button.vue";

import CancelButton from "@/components/forms/cancel-button.vue";
import UiService from "@/common/uiService";

import Comprobante from './Comprobante';
import ComprobanteDetalle from './ComprobanteDetalle';
import ComprobanteImpuesto from "./ComprobanteImpuesto";
import ComprobantePercepcion from "./ComprobantePercepcion";

import commonMixin from '@/Common/Mixins/commonMixin';

import importarComprobante from '@/Components/ImportadorArchivos/Importadores/importar-comprobante'

import comprobanteDetalleForm from './comprobanteDetalle-form'
import comprobanteImpuestoForm from './comprobanteImpuesto-form'
import comprobantePercepcionForm from './comprobantePercepcion-form'

import inlineEdit from "@/components/forms/inline-edit-button.vue";
import inlineDelete from "@/components/forms/inline-delete-button.vue";
import inlineCancel from "@/components/forms/inline-cancel-button.vue";

import categoriaTipoSelect from "@/Selects/categoriaTipo-select.vue";
import comprobanteTipoSelect from "@/Selects/comprobanteTipo-select.vue";
import codigoAutorizacionTipoSelect from "@/Selects/codigoAutorizacionTipo-select.vue";
import currencySelect from "@/Selects/currency-select.vue";
import unidadMedidaSelect from "@/Selects/unidadMedida-select.vue";
import impuestoSelect from "@/Selects/impuesto-select.vue";
import percepcionSelect from "@/Selects/percepcion-select.vue";
import comprobanteEstadoSelect from "@/Selects/comprobanteEstado-select.vue";
import comprobanteEstadoLabel from "@/Selects/comprobanteEstado-label.vue";

import condicionVentaSelect from "@/Selects/condicionVenta-select.vue";

import LayoutMode from "./LayoutMode"

const NO_DATA_MESSAGE = "No hay datos";
const VALIDADA = 1;
const RECHAZADA = 2;
const ERROR_VALIDACION = 3;
const NO_VALIDADA = 4;

// Tipos de comprobantes
const FACTURA_B = 6;
const FACTURA_C = 17;

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
        comprobanteEstadoSelect,
        comprobanteEstadoLabel,
        condicionVentaSelect
    },
    mixins: [commonMixin],
    name: "comprobante-create",

    data: function () {
        return {
            comprobante: new Comprobante(),
            uiService: new UiService(),
            idTable: `__comprobanteDetalles`,
            idTableImpuestos: `__comprobanteImpuestos`,
            idTablePercepciones: `__comprobantePercepciones`,
            comprobanteArchivoDivId: "__comprobanteArchivo",
            comprobanteFormularioDivId: "__comprobanteFormulario",
            LayoutMode,
            currentLayoutMode: LayoutMode.Split,
            NO_DATA_MESSAGE,
            // Estados validacion en ARCA
            VALIDADA,
            RECHAZADA,
            ERROR_VALIDACION,
            NO_VALIDADA,
            // Tipos de comprobante
            FACTURA_B,
            FACTURA_C
        };
    },
    computed: {
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
        impuestosHasErrors(index) {
            let hasErrors = this.errorBag.get(`impuestos[${index}].ImpuestoId`) || this.errorBag.get(`impuestos[${index}].Descripcion`) || this.errorBag.get(`impuestos[${index}].ImporteTotal`);

            return hasErrors;
        },
        percepcionesHasErrors(index) {
            let hasErrors = this.errorBag.get(`percepciones[${index}].PercepcionId`) || this.errorBag.get(`percepciones[${index}].Descripcion`) || this.errorBag.get(`percepciones[${index}].ImporteTotal`);

            return hasErrors;
        },
        hasErrors(index) {
            let hasErrors = this.errorBag.get(`detalles[${index}].UnidadMedidaId`) || this.errorBag.get(`detalles[${index}].Detalle`) || this.errorBag.get(`detalles[${index}].PrecioUnitario`) || this.errorBag.get(`detalles[${index}].Subtotal`) || this.errorBag.get(`detalles[${index}].Cantidad`)


            return hasErrors;
        },
        addNewRow() {
            let nuevoItem = new ComprobanteDetalle();
            nuevoItem.mode.setCreate();

            this.comprobante.detalles.push(nuevoItem);
        },
        addNewImpuesto() {
            let nuevoItem = new ComprobanteImpuesto();
            nuevoItem.mode.setCreate();

            this.comprobante.impuestos.push(nuevoItem);
        },
        addNewPercepcion() {
            let nuevoItem = new ComprobantePercepcion();
            nuevoItem.mode.setCreate();

            this.comprobante.percepciones.push(nuevoItem);
        },
        update(cd) {
            this.uiService.onlyDestroyDataTablesManual(this.idTable);
            cd.mode.setUpdate();
        },
        updateImpuesto(i) {
            this.uiService.onlyDestroyDataTablesManual(this.idTableImpuestos);
            i.mode.setUpdate();
        },
        updatePercepcion(p) {
            this.uiService.onlyDestroyDataTablesManual(this.idTablePercepciones);
            p.mode.setUpdate();
        },
        async remove(cd, index) {
            if (
                await this.uiService.confirmActionModal(
                    `¿Está seguro que desea eliminar el item ${cd.detalle} x${cd.cantidad}?`,
                    "Aceptar",
                    "Cancelar"
                )
            ) {
                cd.deleted = true;
                this.removeRow(index);
                this.uiService.showMessageSuccess(`Se ha eliminado el item ${cd.detalle} x${cd.cantidad}`);
            }
        },
        async removeImpuesto(cd, index) {
            if (
                await this.uiService.confirmActionModal(
                    `¿Está seguro que desea eliminar el impuesto ${cd.descripcion}?`,
                    "Aceptar",
                    "Cancelar"
                )
            ) {
                cd.deleted = true;
                this.removeRowImpuesto(index);
                this.uiService.showMessageSuccess(`Se ha eliminado el impuesto ${cd.descripcion}`);
            }
        },
        async removePercepcion(cd, index) {
            if (
                await this.uiService.confirmActionModal(
                    `¿Está seguro que desea eliminar la percepción ${cd.descripcion}?`,
                    "Aceptar",
                    "Cancelar"
                )
            ) {
                cd.deleted = true;
                this.removeRowPercepcion(index);
                this.uiService.showMessageSuccess(`Se ha eliminado la percepción ${cd.descripcion}`);
            }
        },
        onComprobanteAnalyzed(e) {
            // this.errorBag.clear();
            // this.uiService.onlyDestroyDataTablesManual(this.idTable);

            // this.comprobante = new Comprobante(e[0]);

            // this.$nextTick(() => {
            //     this.uiService.transformToDataTablesManual(this.idTable);
            // });
            if (!this.errorBag.hasErrors()) {
                this.goEdit(e[0], true);
            }
        },
        async init() {
            // Si no hay permisos de alta, volvemos a la lista
            if (!this.grants.create) this.$router.push({ name: "home" });
        },
        cancel() {
            this.$router.push({ name: "home" });
        },
        goDetail(id) {
            this.$router.push({ name: "detail", params: { id: id } });
        },
        goEdit(id, fromAnalysis) {
            if (fromAnalysis) {
                this.$router.push({ name: "edit", params: { id: id }, query: { fromAnalysis: true } });
            } else {
                this.$router.push({ name: "edit", params: { id: id } });
            }
        },
        async verifyAsync() {
            this.uiService.showSpinner(true);

            await this.$store.dispatch("verifyAsync", this.comprobante)
                .then((id) => {
                    if (!this.errorBag.hasErrors()) {
                        this.uiService.showMessageSuccess("Operación confirmada");
                        this.goEdit(id);
                    } else {
                        this.uiService.showMessageError("Operación rechazada");

                        this.uiService.onlyDestroyDataTablesManual(this.idTable);
                        this.uiService.onlyDestroyDataTablesManual(this.idTableImpuestos);
                        this.uiService.onlyDestroyDataTablesManual(this.idTablePercepciones);

                        this.comprobante.detalles = Object.assign([], this.comprobante.detalles.filter(d => !d.deleted));
                        this.comprobante.impuestos = Object.assign([], this.comprobante.impuestos.filter(d => !d.deleted));
                        this.comprobante.percepciones = Object.assign([], this.comprobante.percepciones.filter(d => !d.deleted));

                        this.$nextTick(() => {
                            this.uiService.transformToDataTablesManual(this.idTable);
                            this.uiService.transformToDataTablesManual(this.idTableImpuestos);
                            this.uiService.transformToDataTablesManual(this.idTablePercepciones);
                        });
                    }
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });

        },
        async createAsync() {
            this.uiService.showSpinner(true);

            this.comprobante.detalles = Object.assign([], this.comprobante.detalles.filter(d => !d.deleted));

            await this.$store.dispatch("postAsync", this.comprobante)
                .then((id) => {
                    if (!this.errorBag.hasErrors()) {
                        this.uiService.showMessageSuccess("Operación confirmada");
                        this.goDetail(id);
                    } else {
                        this.uiService.showMessageError("Operación rechazada");

                        this.uiService.onlyDestroyDataTablesManual(this.idTable);
                        this.uiService.onlyDestroyDataTablesManual(this.idTableImpuestos);
                        this.uiService.onlyDestroyDataTablesManual(this.idTablePercepciones);

                        this.comprobante.detalles = Object.assign([], this.comprobante.detalles.filter(d => !d.deleted));
                        this.comprobante.impuestos = Object.assign([], this.comprobante.impuestos.filter(d => !d.deleted));
                        this.comprobante.percepciones = Object.assign([], this.comprobante.percepciones.filter(d => !d.deleted));

                        this.$nextTick(() => {
                            this.uiService.transformToDataTablesManual(this.idTable);
                            this.uiService.transformToDataTablesManual(this.idTableImpuestos);
                            this.uiService.transformToDataTablesManual(this.idTablePercepciones);
                        });
                    }
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },

        async saveAsync() {
            this.uiService.showSpinner(true);

            this.comprobante.detalles = Object.assign([], this.comprobante.detalles.filter(d => !d.deleted));

            await this.$store.dispatch("saveAsync", this.comprobante)
                .then((id) => {
                    if (!this.errorBag.hasErrors()) {
                        this.uiService.showMessageSuccess("Borrador guardado correctamente");
                        this.goDetail(id);
                    } else {
                        this.uiService.showMessageError("Operación rechazada");

                        this.uiService.onlyDestroyDataTablesManual(this.idTable);
                        this.uiService.onlyDestroyDataTablesManual(this.idTableImpuestos);
                        this.uiService.onlyDestroyDataTablesManual(this.idTablePercepciones);

                        this.comprobante.detalles = Object.assign([], this.comprobante.detalles.filter(d => !d.deleted));
                        this.comprobante.impuestos = Object.assign([], this.comprobante.impuestos.filter(d => !d.deleted));
                        this.comprobante.percepciones = Object.assign([], this.comprobante.percepciones.filter(d => !d.deleted));

                        this.$nextTick(() => {
                            this.uiService.transformToDataTablesManual(this.idTable);
                            this.uiService.transformToDataTablesManual(this.idTableImpuestos);
                            this.uiService.transformToDataTablesManual(this.idTablePercepciones);
                        });
                    }
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },

        cancelEdit(key) {
            clearMessage();
            if (this.comprobante.detalles[key].mode.isUpdate) {
                this.comprobante.detalles[key].mode.setDetail();
            } else {
                this.removeRow(key);
            }
        },
        cancelEditImpuesto(key) {
            clearMessage();
            if (this.comprobante.impuestos[key].mode.isUpdate) {
                this.comprobante.impuestos[key].mode.setDetail();
            } else {
                this.removeRowImpuesto(key);
            }
        },
        cancelEditPercepcion(key) {
            clearMessage();
            if (this.comprobante.percepciones[key].mode.isUpdate) {
                this.comprobante.percepciones[key].mode.setDetail();
            } else {
                this.removeRowPercepcion(key);
            }
        },
        removeRow(index) {
            this.uiService.onlyDestroyDataTablesManual(this.idTable);

            // this.comprobante.detalles.splice(index, 1);
            // this.comprobante.detalles[index].deleted = true;

            if (this.comprobante.detalles[index].mode.isCreate) {
                this.comprobante.detalles.splice(index, 1);
            }
            this.errorBag.addError(`detalles[${index}].UnidadMedidaId`, "");
            this.errorBag.addError(`detalles[${index}].Detalle`, "");
            this.errorBag.addError(`detalles[${index}].PrecioUnitario`, "");
            this.errorBag.addError(`detalles[${index}].Subtotal`, "");
            this.errorBag.addError(`detalles[${index}].Cantidad`, "");


            this.$nextTick(() => {
                this.uiService.transformToDataTablesManual(this.idTable);
            });
        },
        removeRowImpuesto(index) {
            this.uiService.onlyDestroyDataTablesManual(this.idTableImpuestos);


            if (this.comprobante.impuestos[index].mode.isCreate) {
                this.comprobante.impuestos.splice(index, 1);
            }
            this.errorBag.addError(`impuestos[${index}].impuestoId`, "");
            this.errorBag.addError(`impuestos[${index}].descripcion`, "");
            this.errorBag.addError(`impuestos[${index}].importeTotal`, "");


            this.$nextTick(() => {
                this.uiService.transformToDataTablesManual(this.idTableImpuestos);
            });
        },
        removeRowPercepcion(index) {
            this.uiService.onlyDestroyDataTablesManual(this.idTablePercepciones);


            if (this.comprobante.percepciones[index].mode.isCreate) {
                this.comprobante.percepciones.splice(index, 1);
            }
            this.errorBag.addError(`percepciones[${index}].percepcionId`, "");
            this.errorBag.addError(`percepciones[${index}].alicuota`, "");
            this.errorBag.addError(`percepciones[${index}].descripcion`, "");
            this.errorBag.addError(`percepciones[${index}].importeTotal`, "");


            this.$nextTick(() => {
                this.uiService.transformToDataTablesManual(this.idTablePercepciones);
            });
        },
        editFinished(detalle, index) {
            // detalle.mode.setDetail();
            this.uiService.onlyDestroyDataTablesManual(this.idTable);
            this.comprobante.detalles[index] = Object.assign({}, detalle);
            this.comprobante.detalles[index].mode.setDetail();

            // detalle.mode.setDetail();

            this.$nextTick(() => {
                this.uiService.transformToDataTablesManual(this.idTable);
            });

        },
        editFinishedImpuesto(detalle, index) {
            this.uiService.onlyDestroyDataTablesManual(this.idTableImpuestos);
            this.comprobante.impuestos[index] = Object.assign({}, detalle);
            this.comprobante.impuestos[index].mode.setDetail();

            this.$nextTick(() => {
                this.uiService.transformToDataTablesManual(this.idTableImpuestos);
            });

        },
        editFinishedPercepcion(detalle, index) {
            this.uiService.onlyDestroyDataTablesManual(this.idTablePercepciones);
            this.comprobante.percepciones[index] = Object.assign({}, detalle);
            this.comprobante.percepciones[index].mode.setDetail();

            this.$nextTick(() => {
                this.uiService.transformToDataTablesManual(this.idTablePercepciones);
            });
        }
    },
};
</script>

<style>
.vertical {
    border-left: 1px solid black;
    height: 200px;
}

.form-select {
    padding: .2rem 2rem .2rem .5rem;

}

.form-control {
    padding: .2rem 2rem .2rem .5rem;
}
</style>