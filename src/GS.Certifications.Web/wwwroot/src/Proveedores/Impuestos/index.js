import Vue from 'vue';
import impuestosCrud from './impuestos-crud.vue'
import store from './store';
import router from './router'

import '@/common/common-filters'
import '@/common/ui-extensions'


new Vue({
    el: "#impuestos",
    store,
    router,
    components: { impuestosCrud },
})