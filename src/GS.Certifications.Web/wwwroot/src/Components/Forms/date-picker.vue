<template>

  <input type="date" v-model="selected" class="form-control" :disabled="!enabled">
</template>
<script>
const yearsAddToCurrentDate = 5;
const previousYearsToCurrentDate = -5;
const minCharacters = 10;
export default {
  components: {},
  name: "date-picker",
  props: {
    value: String,
    showOption: true,
    enabled: {
      type: Boolean,
      default: () => true,
    },
    applyminmax: {
      type: Boolean,
      default: () => false,
    },
  },
  data: function () {
    return {};
  },
  computed: {
    selected: {
      get() {
        return this.value;
      },
      set(value) {
        this.onChange(value);
        // if (value.length === minCharacters) {
        //   value = this.validateDate(value) ? value : null;
        // }
      },
    },
    maxDate() {
      return this.maxYear + "-" + this.month + "-" + this.day;
    },
    maxYear() {
      return this.year + yearsAddToCurrentDate;
    },
    minYear() {
      return this.year + previousYearsToCurrentDate;
    },
    minDate() {
      return this.minYear + "-" + this.month + "-" + this.day;
    },
    getTodayValueWithFormat() {
      return this.day + "/" + this.month + "/" + this.year;
    },
    today() {
      return new Date();
    },
    day() {
      return String(this.today.getDate()).padStart(2, "0");
    },
    month() {
      return String(this.today.getMonth() + 1).padStart(2, "0");
    },
    year() {
      return this.today.getFullYear();
    },
  },
  methods: {
    onChange(value) {
      if (value === "" || value === null) {
        value = null;
      } else {
        value = value;
      }
      this.$emit("input", value);
    },
    validateDate(value) {
      let dateParts = value.split("-");
      let year = parseInt(dateParts[0]);

      if (year > this.maxYear || year < this.minYear) {
        return false;
      }
      return true;
    },
  },
  mounted() {
    // this.getMaxday();
  },
};
</script>

<style scoped>
*,
*:before,
*:after {
  padding: 0;
  margin: 0;
  box-sizing: border-box;
}

input[type="date"] {
  background-color: #0080ff;
  position: relative;
  padding: 6px;
  font-family: "Roboto Mono", monospace;
  color: #ffffff;
  font-size: 16px;
  border: none;
  outline: none;
  border-radius: 5px;
}
::-webkit-calendar-picker-indicator {
  background-color: #ffffff;
  padding: 5px;
  cursor: pointer;
  border-radius: 3px;
}
</style>