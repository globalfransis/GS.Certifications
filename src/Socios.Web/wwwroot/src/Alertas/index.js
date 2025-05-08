import Vue from 'vue';
import alertasCrud from './alertas-crud';
import router from './router'
import '@/common/common-filters'
import '@/common/ui-extensions'


new Vue({
    el: "#alertas-app",
    router,
    components: { alertasCrud },
})