<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 d-flex justify-content-between sticky-header mt-4">
                <div class="row col-12 d-flex">
                    <p class="h5 col-6">Modificación Comprobante {{ nro }}</p>
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
                                <input type="number" step="0.01" class="form-control" v-model="comprobante.cotizacion">
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
                                <input type="number" step="0.01" class="form-control" v-model="comprobante.importeNeto">
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
                                <input type="number" step="0.01" class="form-control" v-model="comprobante.importeIVA">
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
                                        <comprobanteDetalle-form :key="index" :index="index" :comprobanteDetalle="cd"
                                            @cancel-edit="cancelEdit(index)" v-if="cd.mode.isEdit"
                                            :tipoComprobante="comprobante.comprobanteTipoId"
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
                                <button type="button" class="btn btn-outline-primary btn-sm" @click="addNewImpuesto()">
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
                                        <comprobanteImpuesto-form :key="index" :index="index" :comprobanteImpuesto="i"
                                            @cancel-edit="cancelEditImpuesto(index)" v-if="i.mode.isEdit"
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
                                            :class="percepcionesHasErrors(index) ? 'table-danger' : ''" :key="index">
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
        <div class="col-12 d-flex justify-content-end gap-2 mb-3 mt-3" v-if="currentLayoutMode != LayoutMode.File">
            <!-- TODO: podriamos advertir al usuario que al guardar como borrador se perdera la constatancion de ARCA -->
            <button :disabled="comprobante.propietarioActualId != PROVEEDOR"
                v-if="comprobante.comprobanteEstadoId != AUTORIZADO" @click.prevent="saveAsync"
                class="btn btn-secondary btn-sm">
                <i class="fas fa-save"></i>
                Guardar
            </button>
            <accept-button :disabled="!grants.update || comprobante.propietarioActualId != PROVEEDOR"
                @click="updateAsync">
                Validar</accept-button>
            <!-- <accept-button v-if="comprobante.comprobanteEstadoId == BORRADOR" :disabled="!grants.update && comprobante.estadoValidacionARCAId != VALIDADA" @click="confirmAsync" class="me-2">
                Confirmar</accept-button> -->
            <!-- <accept-button v-if="comprobante.comprobanteEstadoId == CONFIRMADO" :disabled="!grants.update && comprobante.estadoValidacionARCAId != VALIDADA" @click="approveAsync" class="me-2">
                Aprobar</accept-button>
            <accept-button v-if="comprobante.estadoValidacionARCAId == VALIDADA && comprobante.comprobanteEstadoId == APROBADO" :disabled="!grants.update && comprobante.estadoValidacionARCAId != VALIDADA" @click="authorizeAsync" class="me-2">
                Autorizar</accept-button> -->
            <!-- REVISAR: quizas si sea necesario mantener el cancelar (es mas claro para el usuario que se descartan los cambios) -->
            <cancel-button @click="cancel">Cancelar</cancel-button>

            <observacionesArca-modal :observaciones="comprobante.observacionesARCAQR"
                :idModal="`${observacionesARCAQRId}`" />
            <observacionesArca-modal :observaciones="comprobante.observacionesARCA"
                :idModal="`${observacionesARCAId}`" />
        </div>
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

import commonMixin from '@/Common/Mixins/commonMixin';
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
        comprobanteEstadoSelect,
        comprobanteEstadoLabel,
        condicionVentaSelect,
        observacionesArcaModal
    },
    mixins: [commonMixin],
    name: "comprobante-edit",
    data: function () {
        return {
            comprobante: new Comprobante(),
            uiService: new UiService(),
            nro: '',
            NO_DATA_MESSAGE,
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
            // Si no hay permisos de modificación, volvemos a la lista
            if (!this.grants.update) this.$router.push({ name: "home" });
            else {
                if (this.$route.params.id) await this.getAsync(this.$route.params.id);
                else this.cancel();

                if (this.$route.query.fromAnalysis) {
                    await this.updateAsync().then(() => {
                        const query = Object.assign({}, this.$route.query);
                        delete query.fromAnalysis;
                        this.$router.replace({ query });
                    });
                }
            }
        },
        async getAsync(id) {
            this.uiService.showSpinner(true)
            await this.$store.dispatch("getAsync", id)
                .then((res) => {
                    this.comprobante = new Comprobante(res);
                    this.nro = this.comprobante.comprobante;
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
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
        completarFormularioComprobante(e) {
            this.errorBag.clear();
            this.uiService.onlyDestroyDataTablesManual(this.idTable);

            this.comprobante = new Comprobante(e[0]);

            this.$nextTick(() => {
                this.uiService.transformToDataTablesManual(this.idTable);
            });
        },
        goDetail() {
            this.$router.push({ name: "detail", params: { id: this.comprobante.id } });
        },
        cancel() {
            this.$router.push({ name: "home", query: { fromDetail: true } });
        },
        async updateAsync() {
            this.errorBag.clear();
            this.uiService.showSpinner(true)
            return await this.$store.dispatch("putAsync", this.comprobante)
                .then(() => {
                    if (!this.errorBag.hasErrors()) {
                        this.uiService.showMessageSuccess("Operación confirmada")
                        this.goDetail();
                    } else {
                        this.uiService.showMessageError("Operación rechazada")
                    }
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        },
        async saveAsync() {
            if (this.comprobante.estadoValidacionARCAId == VALIDADA) {
                if (
                    await this.uiService.confirmActionModal(
                        `ATENCIÓN: Se perderá la constatación con ARCA.\n¿Continuar?`,
                        "Aceptar",
                        "Cancelar"
                    )
                ) {
                    this.errorBag.clear();
                    this.uiService.showSpinner(true);
                    await this.$store.dispatch("updateDraftAsync", this.comprobante)
                        .then(() => {
                            if (!this.errorBag.hasErrors()) {
                                this.uiService.showMessageSuccess("Operación confirmada")
                                this.goDetail();
                            } else {
                                this.uiService.showMessageError("Operación rechazada")
                            }
                        })
                        .finally(() => {
                            this.uiService.showSpinner(false);
                        });
                }
            } else {
                this.uiService.showSpinner(true)
                await this.$store.dispatch("updateDraftAsync", this.comprobante)
                    .then(() => {
                        if (!this.errorBag.hasErrors()) {
                            this.uiService.showMessageSuccess("Operación confirmada")
                            this.goDetail();
                        } else {
                            this.uiService.showMessageError("Operación rechazada")
                        }
                    })
                    .finally(() => {
                        this.uiService.showSpinner(false);
                    });
            }
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

            this.errorBag.addError(`detalles[${index}].UnidadMedidaId`, "");
            this.errorBag.addError(`detalles[${index}].Detalle`, "");
            this.errorBag.addError(`detalles[${index}].PrecioUnitario`, "");
            this.errorBag.addError(`detalles[${index}].Subtotal`, "");
            this.errorBag.addError(`detalles[${index}].Cantidad`, "");

            // detalle.mode.setDetail();

            this.$nextTick(() => {
                this.uiService.transformToDataTablesManual(this.idTable);
            });

        },
        editFinishedImpuesto(detalle, index) {
            this.uiService.onlyDestroyDataTablesManual(this.idTableImpuestos);
            this.comprobante.impuestos[index] = Object.assign({}, detalle);
            this.comprobante.impuestos[index].mode.setDetail();

            this.errorBag.addError(`impuestos[${index}].impuestoId`, "");
            this.errorBag.addError(`impuestos[${index}].descripcion`, "");
            this.errorBag.addError(`impuestos[${index}].importeTotal`, "");

            this.$nextTick(() => {
                this.uiService.transformToDataTablesManual(this.idTableImpuestos);
            });

        },
        editFinishedPercepcion(detalle, index) {
            this.uiService.onlyDestroyDataTablesManual(this.idTablePercepciones);
            this.comprobante.percepciones[index] = Object.assign({}, detalle);
            this.comprobante.percepciones[index].mode.setDetail();

            this.errorBag.addError(`percepciones[${index}].percepcionId`, "");
            this.errorBag.addError(`percepciones[${index}].alicuota`, "");
            this.errorBag.addError(`percepciones[${index}].descripcion`, "");
            this.errorBag.addError(`percepciones[${index}].importeTotal`, "");


            this.$nextTick(() => {
                this.uiService.transformToDataTablesManual(this.idTablePercepciones);
            });
        }
    },
};
</script>
<!-- 
<style>
#__comprobanteDetalles td.border-danger-gs {
    border: 3px solid #dc3545 !important;
}

#__comprobanteImpuestos td.border-danger-gs {
    border: 3px solid #dc3545 !important;
}

#__comprobantePercepciones td.border-danger-gs {
    border: 3px solid #dc3545 !important;
}
</style> -->