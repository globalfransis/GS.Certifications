import Vue from 'vue';
import VueRouter from 'vue-router'
import impuestosDetail from "./impuestos-detail.vue";
import impuestosEdit from "./impuestos-edit.vue";
import impuestosCreate from "./impuestos-create.vue";
import impuestosList from "./impuestos-list.vue";

Vue.use(VueRouter)

const routes = [
    { name: 'home', path: '/', redirect: '/list' },
    { name: 'list', path: '/list', component: impuestosList },
    { name: 'detail', path: '/detail/:id', component: impuestosDetail, props: true },
    { name: 'edit', path: '/edit/:id', component: impuestosEdit, props: true },
    { name: 'create', path: '/create', component: impuestosCreate }
]

export default new VueRouter({
    routes
})