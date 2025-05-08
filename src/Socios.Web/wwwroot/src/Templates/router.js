import Vue from 'vue';
import VueRouter from 'vue-router'
import templateDetail from "./template-detail.vue";
import templateEdit from "./template-edit.vue";
import templateCreate from "./template-create.vue";
import templateList from "./template-list.vue";

Vue.use(VueRouter)

const routes = [
    { name: 'home', path: '/', redirect: '/list' },
    { name: 'list', path: '/list', component: templateList },
    { name: 'detail', path: '/detail/:id', component: templateDetail, props: true },
    { name: 'edit', path: '/edit/:id', component: templateEdit, props: true },
    { name: 'create', path: '/create', component: templateCreate }
]

export default new VueRouter({
    routes
})