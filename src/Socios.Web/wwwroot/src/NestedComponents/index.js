import Vue from 'vue';
import headerComp from './header-comp';
import detailComp from './detail-comp';

new Vue({

    el: "#nested-components-app",
    components: { headerComp, detailComp }

})