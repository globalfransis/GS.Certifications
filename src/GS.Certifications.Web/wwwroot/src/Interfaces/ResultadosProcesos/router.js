import Vue from 'vue';
import VueRouter from 'vue-router'
import resultadoprocesosDetail from "./resultadosProcesos-detail.vue";
import resultadosprocesosList from "./resultadosProcesos-list.vue";
Vue.use(VueRouter)

const routes = [
    { name: 'home', path: '/', redirect: '/list' },
    { name: 'list', path: '/list', component: resultadosprocesosList },
    { name: 'detail', path: '/detail/:id', component: resultadoprocesosDetail, props: true },
]

export default new VueRouter({
    routes
})