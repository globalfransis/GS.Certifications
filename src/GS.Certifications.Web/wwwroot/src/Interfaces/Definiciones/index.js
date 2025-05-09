import Vue from 'vue';
import definicionesCrud from './definiciones-crud';
import router from './router'
import '@/common/common-filters'
import '@/common/ui-extensions'


new Vue({
    el: "#definiciones-app",
    router,
    components: { definicionesCrud },
})