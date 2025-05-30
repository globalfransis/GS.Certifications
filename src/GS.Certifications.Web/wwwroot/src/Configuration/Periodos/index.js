import Vue from 'vue';
import periodosCrud from './periodos-crud.vue'
import store from './store';
import router from './router'

import '@/common/common-filters'
import '@/common/ui-extensions'


new Vue({
    el: "#periodos",
    store,
    router,
    components: { periodosCrud },
})