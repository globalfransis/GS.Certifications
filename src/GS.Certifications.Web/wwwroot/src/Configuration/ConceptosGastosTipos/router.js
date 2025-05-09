import Vue from 'vue';
import VueRouter from 'vue-router'
import conceptosGastosTiposDetail from "./conceptosGastosTipos-detail.vue";
import conceptosGastosTiposEdit from "./conceptosGastosTipos-edit.vue";
import conceptosGastosTiposCreate from "./conceptosGastosTipos-create.vue";
import conceptosGastosTiposList from "./conceptosGastosTipos-list.vue";

Vue.use(VueRouter)

const routes = [
    { name: 'home', path: '/', redirect: '/list' },
    { name: 'list', path: '/list', component: conceptosGastosTiposList },
    { name: 'detail', path: '/detail/:id', component: conceptosGastosTiposDetail, props: true },
    { name: 'edit', path: '/edit/:id', component: conceptosGastosTiposEdit, props: true },
    { name: 'create', path: '/create', component: conceptosGastosTiposCreate }
]

export default new VueRouter({
    routes
})