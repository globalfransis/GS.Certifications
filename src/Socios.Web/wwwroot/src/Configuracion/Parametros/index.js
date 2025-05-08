import Vue from 'vue';
import '@/common/common-filters'
import '@/common/ui-extensions'
import store from './store'
import router from './router'
import parametrosCrud from './parametros-crud';

new Vue({
    el: "#parametros-app",
    components: { parametrosCrud },
    store,
    router
})