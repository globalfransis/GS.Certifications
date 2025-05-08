import Vue from 'vue';
import VueRouter from 'vue-router'
import reglasdefinicionesDetail from "./reglasDefiniciones-detail.vue";
import reglasDefinicionesList from "./reglasDefiniciones-list.vue";
Vue.use(VueRouter)

const routes = [
    { name: 'home', path: '/', redirect: '/list' },
    { name: 'list', path: '/list', component: reglasDefinicionesList },
    { name: 'detail', path: '/detail/:id', component: reglasdefinicionesDetail, props: true },
]

export default new VueRouter({
    routes
})