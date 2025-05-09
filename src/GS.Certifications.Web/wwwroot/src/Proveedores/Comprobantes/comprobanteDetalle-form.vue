<template>
  <tr class="table-info table-active" :key="formKey">
    <td data-toggle="tooltip" :title="errorBag.get('cantidad')" class="w-5">
      <input type="number" :class="errorBag.get('cantidad') ? 'is-invalid' : ''"
        class="form-control" v-model="detalle.cantidad" />
    </td>
    <!-- <td data-toggle="tooltip" :title="errorBag.get('unidadMedidaId')">
      <unidadMedida-select :class="errorBag.get('unidadMedidaId') ? 'is-invalid' : ''"
        v-model="detalle.unidadMedidaId" />
    </td> -->
    <td data-toggle="tooltip" :title="errorBag.get('detalle')">
      <input maxlength="30" :class="errorBag.get('detalle') ? 'is-invalid' : ''" class="form-control" type="text"
        v-model="detalle.detalle" />
    </td>
    <td data-toggle="tooltip" :title="errorBag.get('precioUnitario')">
      <input type="number" step="0.01"
        :class="errorBag.get('precioUnitario') ? 'is-invalid' : ''" class="form-control"
        v-model="detalle.precioUnitario" />
    </td>
    <!-- <td data-toggle="tooltip" :title="errorBag.get('importeBonificacion')">
      <input type="number" step="0.01"
        :class="errorBag.get('importeBonificacion') ? 'is-invalid' : ''" class="form-control"
        v-model="detalle.importeBonificacion" />
    </td> -->
    <td v-if="tipoComprobante != FACTURA_B && tipoComprobante != FACTURA_C" data-toggle="tooltip" :title="errorBag.get('alicuota')">
      <impuestoAlicuota-select :class="errorBag.get('alicuota') ? 'is-invalid' : ''"
        v-model="detalle.alicuota" />
    </td>
    <td data-toggle="tooltip" :title="errorBag.get('subtotal')">
      <input type="number" step="0.01" :class="errorBag.get('subtotal') ? 'is-invalid' : ''" class="form-control"
        v-model="detalle.subtotal" />
    </td>
    <!-- <td data-toggle="tooltip" :title="errorBag.get('importeIVA')">
      <input type="number" step="0.01" :class="errorBag.get('importeIVA') ? 'is-invalid' : ''" class="form-control"
        v-model="detalle.importeIVA" />
    </td> -->
    <td class="d-none d-md-table-cell text-center" scope="row">
      <inline-accept @click="upsert()" />
      <inline-cancel @click="cancel()" />
    </td>
  </tr>
</template>

<script>
import inlineAccept from "@/components/forms/inline-accept-button.vue";
import inlineCancel from "@/components/forms/inline-cancel-button.vue";
import ErrorBag from "@/common/ErrorBag";
import UiService from "@/common/uiService";

import unidadMedidaSelect from "@/Selects/unidadMedida-select.vue";
import impuestoAlicuotaSelect from "@/Selects/impuestoAlicuota-select.vue";

import ComprobanteDetalle from "./ComprobanteDetalle";

// Tipos de comprobantes
const FACTURA_B = 6;
const FACTURA_C = 17;

export default {
  components: { inlineAccept, inlineCancel, unidadMedidaSelect, impuestoAlicuotaSelect },
  name: "comprobanteDetalle-form",
  props: {
    comprobanteDetalle: Object,
    index: Number,
    tipoComprobante: {
      type: Number,
      required: false,
      default: () => null
    },
  },
  data: function () {
    return {
      detalle: Object.assign({}, this.comprobanteDetalle),
      errorBag: new ErrorBag(),
      uiService: new UiService(),
      formKey: 0,
      FACTURA_B,
      FACTURA_C
    };
  },
  methods: {
    cancel() {
      this.detalle = new ComprobanteDetalle();
      this.$emit("cancel-edit", this.index);
    },

    upsert() {
      this.errorBag.clear();
      // Validar detalle agregado / modificado
      this.validarFormulario();

      if (!this.errorBag.hasErrors()) {
        this.$emit("edit-finished", this.detalle);
        this.uiService.showMessageSuccess(`Operación confirmada.`);
      } else {
        this.uiService.showMessageErrorAndFocus("Operación rechazada.");
      }
    },
    actualizarSubtotal() {
      const cantidad = parseInt(this.detalle.cantidad) || 0;
      const precioUnitario = parseFloat(this.detalle.precioUnitario) || 0;
      const importeBonificacion = parseFloat(this.detalle.importeBonificacion) || 0;
      const alicuota = parseFloat(this.detalle.alicuota) || 0;

      this.detalle.subtotal = cantidad * precioUnitario - importeBonificacion;

      if (alicuota) {
        this.detalle.importeIVA = this.detalle.subtotal + this.detalle.subtotal * alicuota / 100;
      } else {
        this.detalle.importeIVA = this.detalle.subtotal;
      }
    },
    validarFormulario() {
      if (!this.detalle.precioUnitario && this.detalle.precioUnitario !== 0) this.errorBag.addError('precioUnitario', "El campo 'Precio Unitario' es obligatorio");
      if (!this.detalle.detalle) this.errorBag.addError('detalle', ["El campo 'Detalle' es obligatorio"]);
      if (!this.detalle.cantidad) this.errorBag.addError('cantidad', ["El campo 'Cantidad' es obligatorio"]);
      if (!this.detalle.subtotal && this.detalle.subtotal !== 0) this.errorBag.addError('subtotal', ["El campo 'Subtotal' es obligatorio"]);
      this.formKey++;
    }
  },
  computed: {},
  watch: {
  },
  mounted() {
  },
};
</script>