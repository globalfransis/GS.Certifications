<template>
  <tr class="table-info table-active" :key="formKey">
    <td data-toggle="tooltip" :title="errorBag.get('impuestoId')">
      <impuesto-select @selected="completarDetalle($event)" :class="errorBag.get('impuestoId') ? 'is-invalid' : ''"
        v-model="impuestoDetalle.impuestoId" />
    </td>
    <td data-toggle="tooltip" :title="errorBag.get('descripcion')">
      <input maxlength="30" :class="errorBag.get('descripcion') ? 'is-invalid' : ''" class="form-control" type="text"
        v-model="impuestoDetalle.descripcion" />
    </td>
    <td data-toggle="tooltip" :title="errorBag.get('importeTotal')">
      <input type="number" step="0.01" :class="errorBag.get('importeTotal') ? 'is-invalid' : ''" class="form-control"
        v-model="impuestoDetalle.importeTotal" />
    </td>
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

import impuestoSelect from "@/Selects/impuesto-select.vue";

import ComprobanteImpuesto from "./ComprobanteImpuesto";

export default {
  components: { inlineAccept, inlineCancel, impuestoSelect },
  name: "comprobanteImpuesto-form",
  props: {
    comprobanteImpuesto: Object,
    index: Number
  },
  data: function () {
    return {
      impuestoDetalle: Object.assign({}, this.comprobanteImpuesto),
      errorBag: new ErrorBag(),
      uiService: new UiService(),
      formKey: 0
    };
  },
  methods: {
    completarDetalle(detalle) {
      this.impuestoDetalle.descripcion = detalle.descripcion;
      // TODO: ver si autocompletamos también el total (se aplica la alicuota sobre el neto) -> recibimos el neto por prop y se calcula el subtotal
    },
    cancel() {
      this.impuestoDetalle = new ComprobanteImpuesto();
      this.$emit("cancel-edit", this.index);
    },

    upsert() {
      this.errorBag.clear();
      // Validar impuestoDetalle agregado / modificado
      this.validarFormulario();

      if (!this.errorBag.hasErrors()) {
        this.$emit("edit-finished", this.impuestoDetalle);
        this.uiService.showMessageSuccess(`Operación confirmada.`);
      } else {
        this.uiService.showMessageErrorAndFocus("Operación rechazada.");
      }
    },
    actualizarSubtotal() {
      // const cantidad = parseFloat(this.impuestoDetalle.cantidad) || 0;
      // const precioUnitario = parseFloat(this.impuestoDetalle.precioUnitario) || 0;
      // const importeBonificacion = parseFloat(this.impuestoDetalle.importeBonificacion) || 0;

      // this.impuestoDetalle.subtotal = cantidad * precioUnitario - importeBonificacion;
    },
    validarFormulario() {
      if (!this.impuestoDetalle.impuestoId) this.errorBag.addError('impuestoId', "El campo 'Impuesto' es obligatorio");
      if (!this.impuestoDetalle.descripcion) this.errorBag.addError('descripcion', ["El campo 'Descripción' es obligatorio"]);
      if (!this.impuestoDetalle.importeTotal && this.impuestoDetalle.importeTotal !== 0) this.errorBag.addError('importeTotal', ["El campo 'Subtotal' es obligatorio"]);
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