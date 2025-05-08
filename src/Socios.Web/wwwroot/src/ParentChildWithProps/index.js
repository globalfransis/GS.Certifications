import Vue from 'vue';
import headerComp from './header-comp';
import detailComp from './detail-comp';

new Vue({
    el: "#parent-child-with-props-app",
    components: { headerComp, detailComp }
})