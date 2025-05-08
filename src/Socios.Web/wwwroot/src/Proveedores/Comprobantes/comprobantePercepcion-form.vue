<template>
  <tr class="table-info table-active" :key="formKey">
    <td data-toggle="tooltip" :title="errorBag.get('percepcionId')">
      <percepcion-select @selected="completarDetalle($event)" :class="errorBag.get('percepcionId') ? 'is-invalid' : ''"
        v-model="percepcionDetalle.percepcionId" />
    </td>
    <td data-toggle="tooltip" :title="errorBag.get('descripcion')">
      <input maxlength="30" :class="errorBag.get('descripcion') ? 'is-invalid' : ''" class="form-control" type="text"
        v-model="percepcionDetalle.descripcion" />
    </td>
    <td data-toggle="tooltip" :title="errorBag.get('alicuota')">
      <input @input="actualizarSubtotal" type="number" step="0.01" :class="errorBag.get('alicuota') ? 'is-invalid' : ''" class="form-control"
        v-model="percepcionDetalle.alicuota" />
    </td>
    <td data-toggle="tooltip" :title="errorBag.get('importeTotal')">
      <input type="number" step="0.01" :class="errorBag.get('importeTotal') ? 'is-invalid' : ''" class="form-control"
        v-model="percepcionDetalle.importeTotal" />
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

import percepcionSelect from "@/Selects/percepcion-select.vue";

import ComprobantePercepcion from "./ComprobantePercepcion";

export default {
  components: { inlineAccept, inlineCancel, percepcionSelect },
  name: "comprobantePercepcion-form",
  props: {
    comprobantePercepcion: Object,
    index: Number,
    importeNeto: null
  },
  data: function () {
    return {
      percepcionDetalle: Object.assign({}, this.comprobantePercepcion),
      errorBag: new ErrorBag(),
      uiService: new UiService(),
      formKey: 0
    };
  },
  methods: {
    completarDetalle(detalle) {
      this.percepcionDetalle.descripcion = detalle.descripcion;
      this.percepcionDetalle.alicuota = detalle.alicuota;
    },
    cancel() {
      this.percepcionDetalle = new ComprobantePercepcion();
      this.$emit("cancel-edit", this.index);
    },

    upsert() {
      this.errorBag.clear();
      // Validar detalle agregado / modificado
      this.validarFormulario();

      if (!this.errorBag.hasErrors()) {
        this.$emit("edit-finished", this.percepcionDetalle);
        this.uiService.showMessageSuccess(`Operación confirmada.`);
      } else {
        this.uiService.showMessageErrorAndFocus("Operación rechazada.");
      }
    },
    actualizarSubtotal() {
      this.percepcionDetalle.importeTotal = this.importeNeto * this.percepcionDetalle.alicuota / 100;
    },
    validarFormulario() {
      if (!this.percepcionDetalle.percepcionId) this.errorBag.addError('percepcionId', "El campo 'Percepción' es obligatorio");
      if (!this.percepcionDetalle.descripcion) this.errorBag.addError('descripcion', ["El campo 'Descripción' es obligatorio"]);
      if (!this.percepcionDetalle.alicuota) this.errorBag.addError('alicuota', ["El campo 'Alícuota' es obligatorio"]);
      if (!this.percepcionDetalle.importeTotal && this.percepcionDetalle.importeTotal !== 0) this.errorBag.addError('importeTotal', ["El campo 'Subtotal' es obligatorio"]);
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