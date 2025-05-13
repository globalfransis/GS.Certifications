import Vue from 'vue';
import VueRouter from 'vue-router'
import solicitudCertificacionDetail from "./solicitudCertificacion-detail.vue";
import solicitudCertificacionEdit from "./solicitudCertificacion-edit.vue";
import solicitudCertificacionCreate from "./solicitudCertificacion-create.vue";
import solicitudCertificacionList from "./solicitudCertificacion-list.vue";

Vue.use(VueRouter)

const routes = [
    { name: 'home', path: '/', redirect: '/list' },
    { name: 'list', path: '/list', component: solicitudCertificacionList },
    { name: 'detail', path: '/detail/:id', component: solicitudCertificacionDetail, props: true },
    { name: 'edit', path: '/edit/:id', component: solicitudCertificacionEdit, props: true },
    { name: 'create', path: '/create', component: solicitudCertificacionCreate }
]

export default new VueRouter({
    routes
})