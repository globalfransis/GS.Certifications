import Vue from 'vue';
import VueRouter from 'vue-router'
import alertasForm from "./alertas-form.vue";
import alertasList from "./alertas-list.vue";
Vue.use(VueRouter)

const routes = [
    { name: 'home', path: '/', redirect: '/list' },
    { name: 'list', path: '/list', component: alertasList },
    { name: 'create', path: '/form', component: alertasForm, props: true },
    { name: 'edit', path: '/edit/:alertaId', component: alertasForm, props: true },
    { name: 'detail', path: '/detail/:alertaId', component: alertasForm, props: true },
]

export default new VueRouter({
    routes
})