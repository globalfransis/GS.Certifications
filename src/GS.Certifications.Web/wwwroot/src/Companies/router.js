import Vue from 'vue';
import VueRouter from 'vue-router'
import companiesForm from "./companies-form.vue";
import companiesList from "./companies-list.vue";

Vue.use(VueRouter)

const routes = [
    { name: 'create', path: '/form', component: companiesForm, props: true },
    { name: 'detail', path: '/detail/:id', component: companiesForm, props: true },
    { name: 'edit', path: '/edit/:id', component: companiesForm, props: true },
    { name: 'home', path: '/', redirect: '/list' },
    { name: 'list', path: '/list', component: companiesList }
]

export default new VueRouter({
    routes
})