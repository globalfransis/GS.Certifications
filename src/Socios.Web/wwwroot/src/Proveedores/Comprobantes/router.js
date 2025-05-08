import Vue from 'vue';
import VueRouter from 'vue-router'
import comprobanteDetail from "./comprobante-detail.vue";
import comprobanteEdit from "./comprobante-edit.vue";
import comprobanteCreate from "./comprobante-create.vue";
import comprobanteList from "./comprobante-list.vue";

Vue.use(VueRouter)

const routes = [
    { name: 'home', path: '/', redirect: '/list' },
    { name: 'list', path: '/list', component: comprobanteList },
    { name: 'detail', path: '/detail/:id', component: comprobanteDetail, props: true },
    { name: 'edit', path: '/edit/:id', component: comprobanteEdit, props: true },
    { name: 'create', path: '/create', component: comprobanteCreate }
]

export default new VueRouter({
    routes
})