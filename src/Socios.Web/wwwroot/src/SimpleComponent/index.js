import Vue from 'vue';
import simpleComp from './simple-comp';

new Vue({
    el: "#simple-app",
    components: { simpleComp } //this declaration (simpleComp) must be equal to the tag (minus dashes), in this case <simple-comp>
})