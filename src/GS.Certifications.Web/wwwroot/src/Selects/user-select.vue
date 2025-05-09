<template>
  <select v-model="selected" class="form-control">
    <!-- <option :disabled="!showOption" sel :value="0">{{ this.showOption ? loc['Sin especificar'] : loc['Seleccionar'] }}</option> -->
    <option :disabled="!showOption" sel :value="0">{{loc['Sin especificar']}}</option>
    <option v-for="user in users" :key="user.id" :value="user.id"> {{ user.login }} </option>
  </select>
</template>
<script>
  import ajax from "@/common/ajaxWrapper";
  import loc from "@/common/commonLoc";
  export default {
    components: {},
    name: "user-select",
    props: {
      value: Number,
      params: Object,
      except: Array,
      getSelectedData: Function
    },
    data: function () {
      return {
        users: Array,
        loc
      };
    },
    computed: {
      selected: {
        get() { return this.value; },
        set(value) { this.onChange(value); },
      },
    },
    methods: {
      onChange(value) {
        this.$emit("selected", this.users.find(u => u.id == value));
      },
    },
    mounted() {
      var excepts = this.except;
      new ajax()
        .get(baseUrl + "/api/user/GetUsers", this.params, {})
        .then((res) => {
          var list = res;

          if (excepts.length > 0) {
            this.users = list.filter(function (itm) {
              return !excepts.some((e) => {
                return e == itm.id && e != 0;
              });
            });
          } else {
            this.users = list;
          }
        })
        .catch((ex) => {
          console.log(
            "Error recuperando usuarios: " + ex + JSON.stringify(this.params)
          );
        });
    },
  };
</script>