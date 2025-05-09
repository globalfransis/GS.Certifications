import Vue from 'vue';
import conceptosGastosTiposCrud from './conceptosGastosTipos-crud.vue'
import store from './store';
import router from './router'

import '@/common/common-filters'
import '@/common/ui-extensions'


new Vue({
    el: "#conceptosGastosTipos",
    store,
    router,
    components: { conceptosGastosTiposCrud },
})