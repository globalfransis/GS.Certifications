import Vue from 'vue';
import VueRouter from 'vue-router'
import parametrosLista from './parametros-lista.vue';
import parametrosForm from './parametros-form.vue';

Vue.use(VueRouter)

const routes = [
        { name: 'create', path: '/create', component: parametrosForm},
        { name: 'detail', path: '/detail/:id', component: parametrosForm, props: true },
        { name: 'edit', path: '/edit/:id', component: parametrosForm, props: true },
        { name: 'home', path: '/', redirect: '/list' },
        { name: 'list', path: '/list', component: parametrosLista },
]

export default new VueRouter({
    routes
}) 