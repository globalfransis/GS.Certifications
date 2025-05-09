import Vue from 'vue';
import ordenesComprasCrud from './ordenesCompras-crud.vue'
import store from './store';
import router from './router'

import '@/common/common-filters'
import '@/common/ui-extensions'


new Vue({
    el: "#ordenesCompras",
    store,
    router,
    components: { ordenesComprasCrud },
})