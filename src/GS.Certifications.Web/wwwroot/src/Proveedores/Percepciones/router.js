import Vue from 'vue';
import VueRouter from 'vue-router'
import percepcionesDetail from "./percepciones-detail.vue";
import percepcionesEdit from "./percepciones-edit.vue";
import percepcionesCreate from "./percepciones-create.vue";
import percepcionesList from "./percepciones-list.vue";

Vue.use(VueRouter)

const routes = [
    { name: 'home', path: '/', redirect: '/list' },
    { name: 'list', path: '/list', component: percepcionesList },
    { name: 'detail', path: '/detail/:id', component: percepcionesDetail, props: true },
    { name: 'edit', path: '/edit/:id', component: percepcionesEdit, props: true },
    { name: 'create', path: '/create', component: percepcionesCreate }
]

export default new VueRouter({
    routes
})