import Vue from 'vue';
import percepcionesCrud from './percepciones-crud.vue'
import store from './store';
import router from './router'

import '@/common/common-filters'
import '@/common/ui-extensions'


new Vue({
    el: "#percepciones",
    store,
    router,
    components: { percepcionesCrud },
})