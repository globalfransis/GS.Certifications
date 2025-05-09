import Vue from 'vue';
import VueRouter from 'vue-router'
import ordenesComprasDetail from "./ordenesCompras-detail.vue";
import ordenesComprasEdit from "./ordenesCompras-edit.vue";
import ordenesComprasCreate from "./ordenesCompras-create.vue";
import ordenesComprasList from "./ordenesCompras-list.vue";

Vue.use(VueRouter)

const routes = [
    { name: 'home', path: '/', redirect: '/list' },
    { name: 'list', path: '/list', component: ordenesComprasList },
    { name: 'detail', path: '/detail/:id', component: ordenesComprasDetail, props: true },
    { name: 'edit', path: '/edit/:id', component: ordenesComprasEdit, props: true },
    { name: 'create', path: '/create', component: ordenesComprasCreate }
]

export default new VueRouter({
    routes
})