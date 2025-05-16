import Vue from 'vue';
import solicitudcertificacionCrud from './solicitudCertificacion-crud';
import store from './store';
import router from './router'

import '@/common/common-filters'
import '@/common/ui-extensions'


new Vue({
    el: "#solicitudCertificacion-app",
    store,
    router,
    components: { solicitudcertificacionCrud },
})