import Vue from 'vue';
import empresasCrud from './empresas-crud';
import store from './store';
import router from './router'

import '@/common/common-filters'
import '@/common/ui-extensions'


new Vue({
    el: "#empresas-app",
    store,
    router,
    components: { empresasCrud },
})