<template>
    <div ref="top">
        <div class="col-12">
            <div class="col-12 mt-4">
                <p class="h5">Titulo</p> <!-- Por ejemplo: Alta de Usuario -->
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <!-- Agregar campos del formulario de alta -->
                        <!-- Este es un ejemplo -->
                        <div class="form-group col-lg-3 col-sm-12 mb-4">
                            <label class="control-label">Descripción</label>
                            <input type="text" class="form-control" v-model="templateDto.descripcion">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12 d-flex justify-content-end gap-2 mb-3 mt-3">
            <accept-button :disabled="!grants.create" @click="createAsync">Aceptar</accept-button>
            <cancel-button @click="cancel">Cancelar</cancel-button>
        </div>
    </div>
</template>

<script>
import AcceptButton from "@/components/forms/accept-button.vue";

import CancelButton from "@/components/forms/cancel-button.vue";
import UiService from "@/common/uiService";

import TemplateDto from './templateDto' // Modificar por la clase dto correspondiente

import commonMixin from '@/Common/Mixins/commonMixin';

export default {
    components: {
        AcceptButton,
        CancelButton
    },
    mixins: [commonMixin],
    name: "template-create", // Modificar

    data: function () {
        return {
            templateDto: new TemplateDto(),
            uiService: new UiService()
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
        async createAsync() {
            this.uiService.showSpinner(true)
            await this.$store.dispatch("postAsync", this.templateDto)
                .then((id) => {
                    if (!this.errorBag.hasErrors()) {
                        this.uiService.showMessageSuccess("Operación confirmada")
                        this.goDetail(id);
                    } else {
                        this.uiService.showMessageError("Operación rechazada")
                    }
                })
                .finally(() => {
                    this.uiService.showSpinner(false);
                });
        }
    },
};
</script>