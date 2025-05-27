import Vue from 'vue';
import actividadUsuariosCrud from './actividadUsuarios-crud';
import store from './store';
import router from './router'
import '@/common/common-filters'
import '@/common/ui-extensions'

import '@/common/common-filters'
import '@/common/ui-extensions'


new Vue({
    el: "#actividadUsuarios",
    store,
    router,
    components: { actividadUsuariosCrud },
})