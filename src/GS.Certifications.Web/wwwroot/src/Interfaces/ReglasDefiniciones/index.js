import Vue from 'vue';
import reglasdefinicionesCrud from './reglasDefiniciones-crud';
import router from './router'
import '@/common/common-filters'
import '@/common/ui-extensions'


new Vue({
    el: "#reglasDefiniciones-app",
    router,
    components: { reglasdefinicionesCrud },
})