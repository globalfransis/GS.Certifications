import Vue from 'vue';
import VueRouter from 'vue-router'
import definicionesDetail from "./definiciones-detail.vue";
import definicionesList from "./definiciones-list.vue";
Vue.use(VueRouter)

const routes = [
    { name: 'home', path: '/', redirect: '/list' },
    { name: 'list', path: '/list', component: definicionesList },
    { name: 'detail', path: '/detail/:id', component: definicionesDetail, props: true },
]

export default new VueRouter({
    routes
})