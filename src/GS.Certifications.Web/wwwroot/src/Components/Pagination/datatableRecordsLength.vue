<template>
  <div class="dataTables_length col-6 row gsf-records-length d-flex align-items-center" :id="`${idRecordsLength}`">
    <label class="col-auto">{{loc["Se muestran"]}}</label>
    <div class="col-2 gsf-pagination-select">
    <select :name="`${idRecordsLength}`" v-model="currentRecordsLength" class="form-select form-select-sm">
      <option :value="10">10</option>
      <option :value="25">25</option>
      <option :value="50">50</option>
      <option :value="100">100</option>
    </select>
  </div>
    <label class="col-auto">{{loc["registros por página"]}}</label>
  </div>
</template>

<script>
import loc from "@/common/commonLoc";

export default {
  name: "datatableRecordsLength",

  components: {
    loc
  },

  props: {
    id: null,
    value: Number,
  },

  data: function () {
    return {
      loc,
      idRecordsLength: `${this.id}_length`,
    };
  },

  computed: {
    currentRecordsLength: {
      get() {
        return this.value ? this.value : 25;
      },
      set(newRecordsLength) {
        this.$emit('input', newRecordsLength)
        this.$emit('changed')
      },
    }
  },

  async mounted() {
  },

  methods: {
  }
};
</script>

<style scoped>
.gsf-records-length {
  position: absolute;
}
.gsf-pagination-select {
  margin-left: -15px;
  margin-right: -15px;
}
</style>