import Vue from 'vue';
import VueRouter from 'vue-router'
import notificationManagementForm from "./notificationManagement-form.vue";
import notificationManagementList from "./notificationManagement-list.vue";
Vue.use(VueRouter)

const routes = [
    // { name: 'create', path: '/form', component: notificationManagementForm, props: true },
    // { name: 'detail', path: '/detail/cliente/:id/contrato/:contratoId?/instalacion/:instalacionId?', component: notificationManagementForm, props: true },
    { name: 'edit', path: '/edit/:notificacionId', component: notificationManagementForm, props: true },
    { name: 'home', path: '/', redirect: '/list' },
    { name: 'list', path: '/list', component: notificationManagementList }
]

export default new VueRouter({
    routes
})