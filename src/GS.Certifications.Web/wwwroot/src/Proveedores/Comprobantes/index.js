import Vue from 'vue';
import comprobanteCrud from './comprobante-crud';
import store from './store';
import router from './router'

import '@/common/common-filters'
import '@/common/ui-extensions'


new Vue({
    el: "#comprobante-app",
    store,
    router,
    components: { comprobanteCrud },
})