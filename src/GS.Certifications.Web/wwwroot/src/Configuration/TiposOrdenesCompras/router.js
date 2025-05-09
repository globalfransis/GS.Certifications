import Vue from 'vue';
import VueRouter from 'vue-router'
import tiposOrdenesComprasDetail from "./tiposOrdenesCompras-detail.vue";
import tiposOrdenesComprasEdit from "./tiposOrdenesCompras-edit.vue";
import tiposOrdenesComprasCreate from "./tiposOrdenesCompras-create.vue";
import tiposOrdenesComprasList from "./tiposOrdenesCompras-list.vue";

Vue.use(VueRouter)

const routes = [
    { name: 'home', path: '/', redirect: '/list' },
    { name: 'list', path: '/list', component: tiposOrdenesComprasList },
    { name: 'detail', path: '/detail/:id', component: tiposOrdenesComprasDetail, props: true },
    { name: 'edit', path: '/edit/:id', component: tiposOrdenesComprasEdit, props: true },
    { name: 'create', path: '/create', component: tiposOrdenesComprasCreate }
]

export default new VueRouter({
    routes
})