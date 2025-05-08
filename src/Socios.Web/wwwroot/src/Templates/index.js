import Vue from 'vue';
import templateCrud from './template-crud';
import store from './store';
import router from './router'

import '@/common/common-filters'
import '@/common/ui-extensions'


new Vue({
    el: "#template-app",
    store,
    router,
    components: { templateCrud },
})