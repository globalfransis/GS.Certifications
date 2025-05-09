import Vue from 'vue';
import notificationmanagementCrud from './notificationManagement-crud';
import router from './router'
import '@/common/common-filters'
import '@/common/ui-extensions'


new Vue({
    el: "#notifications-app",
    router,
    components: { notificationmanagementCrud },
})