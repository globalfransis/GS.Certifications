import Vue from 'vue';
import VueRouter from 'vue-router'
import actividadUsuariosList from "./actividadUsuarios-list.vue";

Vue.use(VueRouter)

const routes = [
    { name: 'home', path: '/', redirect: '/list' },
    { name: 'list', path: '/list', component: actividadUsuariosList },
]

export default new VueRouter({
    routes
})