import Vue from 'vue';
import tiposOrdenesComprasCrud from './tiposOrdenesCompras-crud.vue'
import store from './store';
import router from './router'

import '@/common/common-filters'
import '@/common/ui-extensions'


new Vue({
    el: "#tiposOrdenesCompras",
    store,
    router,
    components: { tiposOrdenesComprasCrud },
})