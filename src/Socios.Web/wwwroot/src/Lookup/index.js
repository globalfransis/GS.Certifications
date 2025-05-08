import Vue from 'vue';
import lookupComp from './lookup-comp';
import lookupDisenoComp from './lookup-diseno-comp';
import lookupSearchComp from './lookup-search-comp';

new Vue({

    el: "#lookup-app",
    components: { lookupComp, lookupSearchComp, countryLookupComp, lookupDisenoComp  }

})