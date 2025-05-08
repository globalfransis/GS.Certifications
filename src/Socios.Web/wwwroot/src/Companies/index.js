import Vue from 'vue';
import router from './router'
import store from './store'
import companiesCrud from './companies-crud';
import '@/common/common-filters'
import '@/common/ui-extensions'


new Vue({
    el: "#companies-app",
    components: { companiesCrud },
    router,
    store
})