<template>
    <tr class="table-info table-active" :key="formKey">
        <td data-toggle="tooltip">
            <monedasEmpresa-select v-model="monedaDetalle.currencyId" :listaMonedasUsadas="listaMonedasUsadas"/>
        </td>

        <!-- <span class="text-danger field-validation-error" data-valmsg-for="currencyId" data-valmsg-replace="true">
            {{ errorBag.get("currencyId") }}
        </span> -->

        <td class="text-center align-middle">
            <input placeholder="Moneda predeterminada" id="monedaPorDefecto"
             type="checkbox" class="form-check-input"
              v-model="monedaDetalle.monedaPorDefecto"/>
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

import monedasEmpresaSelect from "@/Components/Empresas/monedasEmpresa-select";

import EmpresaMoneda from "./EmpresaMoneda"

const AFIRMATIVO = true;
const NEGATIVO = false;

export default {
    components: { inlineAccept, inlineCancel, monedasEmpresaSelect },
    name: "empresaMoneda-form",
    props: {
        listaMonedasUsadas: Array,
        empresaMoneda: Object,
        index: Number
    },
    data: function () {
        return {
            AFIRMATIVO,
            NEGATIVO,

            monedaDetalle: Object.assign({}, this.empresaMoneda),

            //monedaAnterior: this.empresaMoneda.currencyId,

            errorBag: new ErrorBag(),
            uiService: new UiService(),
            formKey: 0
        };
    },
    methods: {
        cancel() {
            this.monedaDetalle = new EmpresaMoneda();
            this.$emit("cancel-edit", this.index);
        },

        upsert() {
            this.errorBag.clear();
            // Validar monedaDetalle agregado / modificado
            this.validarFormulario();
            
            if (!this.errorBag.hasErrors()) {
                this.$emit("edit-finished", this.monedaDetalle);
                this.uiService.showMessageSuccess(`Operación confirmada.`);
            } else {
                this.uiService.showMessageErrorAndFocus("Operación rechazada.");
            }
        },
        actualizarSubtotal() {
        },
    validarFormulario() {
        if (!this.monedaDetalle.currencyId) this.errorBag.addError("currencyId", ["El campo 'Moneda' es obligatorio"]);
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