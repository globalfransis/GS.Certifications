import Vue from 'vue';
import VueRouter from 'vue-router'
import empresasForm from "./empresas-form.vue";
import empresasEdit from "./empresas-edit.vue";
import empresasCreate from "./empresas-create.vue";
import empresasList from "./empresas-list.vue";

import usuariosExternosForm from './usuarios-externos-form.vue';
import usuariosExternosCreate from './usuarios-externos-create.vue';
import usuariosExternosUpdate from './usuarios-externos-update.vue';

import usuariosInternosForm from './usuarios-internos-form.vue';
import usuariosInternosCreate from './usuarios-internos-create.vue';
import usuariosInternosUpdate from './usuarios-internos-update.vue';

import documentosComprasForm from './documentos-compras-form.vue';
import conceptosGastosForm from './conceptos-gastos-form.vue';

import configuracionAnalisisForm from './configuracion-analisis-form.vue';

Vue.use(VueRouter)

const routes = [
    { name: 'home', path: '/', redirect: '/list' },
    { name: 'list', path: '/list', component: empresasList },
    { name: 'detail', path: '/detail/:id', component: empresasForm, props: true },
    { name: 'edit', path: '/edit/:id', component: empresasEdit, props: true },
    { name: 'create', path: '/create', component: empresasCreate },

    { name: 'usuariosExternosForm', path: '/detail/:id/usuarioExterno', component: usuariosExternosForm, props: true },
    { name: "usuariosExternosCreate", path: "/detail/:id/usuarioExterno/create",
        component: usuariosExternosCreate, props: true },
    { name: "usuariosExternosUpdate", path: "/detail/:id/usuarioExterno/Update/:usuarioId",
        component: usuariosExternosUpdate, props: true },

    { name: 'usuariosInternosForm', path: '/detail/:id/usuarioInterno', component: usuariosInternosForm, props: true },
    { name: "usuariosInternosCreate", path: "/detail/:id/usuarioInterno/create",
        component: usuariosInternosCreate, props: true },
    { name: "usuariosInternosUpdate", path: "/detail/:id/usuarioInterno/Update/:usuarioId",
        component: usuariosInternosUpdate, props: true },

    { name: 'documentosComprasForm', path: '/detail/:id/documentoCompra', component: documentosComprasForm, props: true },

    { name: 'conceptosGastosForm', path: '/detail/:id/conceptoGasto', component: conceptosGastosForm, props: true },

    { name: 'configuracionAnalisisForm', path: '/detail/:id/configuracionAnalisis', component: configuracionAnalisisForm, props: true },
]

export default new VueRouter({
    routes
})