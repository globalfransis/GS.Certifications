import Vue from 'vue';
import VueRouter from 'vue-router'
import periodosDetail from "./periodos-detail.vue";
import periodosEdit from "./periodos-edit.vue";
import periodosCreate from "./periodos-create.vue";
import periodosList from "./periodos-list.vue";

Vue.use(VueRouter)

const routes = [
    { name: 'home', path: '/', redirect: '/list' },
    { name: 'list', path: '/list', component: periodosList },
    { name: 'detail', path: '/detail/:id', component: periodosDetail, props: true },
    { name: 'edit', path: '/edit/:id', component: periodosEdit, props: true },
    { name: 'create', path: '/create', component: periodosCreate }
]

export default new VueRouter({
    routes
})