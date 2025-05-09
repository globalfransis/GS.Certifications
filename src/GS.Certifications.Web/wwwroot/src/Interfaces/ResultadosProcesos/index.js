import Vue from 'vue';
import resultadosprocesosCrud from './resultadosProcesos-crud';
import router from './router'
import '@/common/common-filters'
import '@/common/ui-extensions'


new Vue({
    el: "#resultadosProcesos-app",
    router,
    components: { resultadosprocesosCrud },
})