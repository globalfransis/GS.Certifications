import Vue from 'vue';
import VueRouter from 'vue-router'
import requisitosFQSList from "./requisitosFQS-list.vue";

Vue.use(VueRouter)

const routes = [
    { name: 'home', path: '/', redirect: '/list' },
    { name: 'list', path: '/list', component: requisitosFQSList },
]

export default new VueRouter({
    routes
})