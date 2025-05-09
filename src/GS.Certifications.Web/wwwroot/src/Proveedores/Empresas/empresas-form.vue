<template>
    <div>
        <ul class="nav nav-tabs mt-4" role="tablist">
            <li class="nav-item" role="presentation">
                <button class="nav-link" :class="{ active: currentTab === 'empresaDetail' }" 
                        @click="changeTab('empresaDetail')">
                    Detalle de Empresa Portal
                </button>
            </li>
            <li class="nav-item" role="presentation">
                <button class="nav-link" :class="{ active: currentTab === 'usuariosExternosForm' }" 
                        @click="changeTab('usuariosExternosForm')">
                    Usuarios Web
                </button>
            </li>
            <li class="nav-item" role="presentation">
                <button class="nav-link" :class="{ active: currentTab === 'usuariosInternosForm' }" 
                        @click="changeTab('usuariosInternosForm')">
                    Usuarios Backoffice
                </button>
            </li>
            <li class="nav-item" role="presentation">
                <button class="nav-link" :class="{ active: currentTab === 'documentosComprasForm' }" 
                        @click="changeTab('documentosComprasForm')">
                    Documentos de Compras
                </button>
            </li>
            <li class="nav-item" role="presentation">
                <button class="nav-link" :class="{ active: currentTab === 'conceptosGastosForm' }" 
                        @click="changeTab('conceptosGastosForm')">
                    Conceptos de Gastos
                </button>
            </li>
            <li class="nav-item" role="presentation">
                <button class="nav-link" :class="{ active: currentTab === 'configuracionAnalisisForm' }" 
                        @click="changeTab('configuracionAnalisisForm')">
                    Configuracion / Analisis
                </button>
            </li>
        </ul>
        <div class="tab-content" id="pills-tabContent">
            <empresa-detail v-if="currentTab === 'empresaDetail'" class="card-tab" ref="empresaDetalle"/>
            <usuarios-externos-form v-else-if="currentTab === 'usuariosExternosForm'" class="card-tab" ref="usuariosExternosForm"/>
            <usuarios-internos-form v-else-if="currentTab === 'usuariosInternosForm'" class="card-tab" ref="usuariosInternosForm"/>
            <documentos-compras-form v-else-if="currentTab === 'documentosComprasForm'" class="card-tab" ref="documentosComprasForm"/>
            <conceptos-gastos-form v-else-if="currentTab === 'conceptosGastosForm'" class="card-tab" ref="conceptosGastosForm"/>
            <configuracion-analisis-form v-else-if="currentTab === 'configuracionAnalisisForm'" class="card-tab" ref="configuracionAnalisisForm"/>
        </div>
    </div>
</template>

<script>
import empresaDetail from "./empresas-detail.vue";
import usuariosExternosForm from "./usuarios-externos-form.vue";
import usuariosInternosForm from "./usuarios-internos-form.vue";
import documentosComprasForm from "./documentos-compras-form.vue";
import conceptosGastosForm from "./conceptos-gastos-form.vue";
import configuracionAnalisisForm from "./configuracion-analisis-form.vue";

export default {
    name: "empresas-form",
    components: {
        empresaDetail,
        usuariosExternosForm,
        usuariosInternosForm,
        documentosComprasForm,
        conceptosGastosForm,
        configuracionAnalisisForm
    },
    data() {
        return {
            currentTab: this.$route.query.tab || 'empresaDetail' // Usa la pestaña de la URL o el valor por defecto
        };
    },
    watch: {
        '$route.query.tab'(newTab) {
            if (newTab) {
                this.currentTab = newTab;
            }
        }
    },
    methods: {
        changeTab(tab) {
            this.currentTab = tab;
            this.$router.replace({ query: { tab } }); // Modifica la URL sin recargar
        }
    },
    mounted() {
        if (this.$route.query.tab) {
            this.currentTab = this.$route.query.tab;
        }
    }
};
</script>