import Vue from 'vue';
import requisitosCrud from './requisitos-crud';
import router from './router'

import '@/common/common-filters'
import '@/common/ui-extensions'


new Vue({
    el: "#requisitos-app",
    router,
    components: { requisitosCrud },
})