<template>
    <div>
        <div class="input-group">
            <div class="btn-group d-flex" role="group">
                <div class="input-group">

                    <button @click.prevent="toggleDay(1)" class="btn btn-sm first-btn" v-bind="$attrs" v-bind:class="[bitMaskValue & 1 ? enableClass : disableClass, error ? 'invalid': '']" tabindex="-1">{{daysNames[0]}}</button>
                    <button @click.prevent="toggleDay(2)" class="btn btn-sm mid-btn" v-bind="$attrs" v-bind:class="[bitMaskValue & 2 ? enableClass : disableClass, error ? 'invalid': '']" tabindex="-1">{{daysNames[1]}}</button>
                    <button @click.prevent="toggleDay(4)" class="btn btn-sm mid-btn" v-bind="$attrs" v-bind:class="[bitMaskValue & 4 ? enableClass : disableClass, error ? 'invalid': '']" tabindex="-1">{{daysNames[2]}}</button>
                    <button @click.prevent="toggleDay(8)" class="btn btn-sm mid-btn" v-bind="$attrs" v-bind:class="[bitMaskValue & 8 ? enableClass : disableClass, error ? 'invalid': '']" tabindex="-1">{{daysNames[3]}}</button>
                    <button @click.prevent="toggleDay(16)" class="btn btn-sm mid-btn" v-bind="$attrs" v-bind:class="[bitMaskValue & 16 ? enableClass : disableClass, error ? 'invalid': '']" tabindex="-1">{{daysNames[4]}}</button>
                    <button @click.prevent="toggleDay(32)" class="btn btn-sm mid-btn" v-bind="$attrs" v-bind:class="[bitMaskValue & 32 ? enableClass : disableClass, error ? 'invalid': '']" tabindex="-1">{{daysNames[5]}}</button>
                    <button @click.prevent="toggleDay(64)" class="btn btn-sm mid-btn" v-bind="$attrs" v-bind:class="[bitMaskValue & 64 ? enableClass : disableClass, error ? 'invalid': '']" tabindex="-1">{{daysNames[6]}}</button>

                    <input type="text"
                           :class="['form-control', error ? 'invalid': '']"
                           v-model="stringValue"
                           v-bind="$attrs"
                           maxlength="7"
                           @input="handleInput"
                           autocomplete="off" />
                </div>
            </div>
        </div>
        <span v-if="showError" class="text-danger">{{error}}</span>
    </div>
</template>

<script>
    const sundayString = 0
    const mondayString = 1
    const tuesdayString = 2
    const wednesdayString = 3
    const thrusdayString = 4
    const fridayString = 5
    const saturdayString = 6

    const sundayValue = 1
    const mondayValue = 2
    const tuesdayValue = 4
    const wednesdayValue = 8
    const thrusdayValue = 16
    const fridayValue = 32
    const saturdayValue = 64

    export default {
        components: {},
        inheritAttrs: false,
        name: "week-days",
        props: {
            value: String,
            tooltipText: String,
            error: String,
            showError: { type: Boolean, default: true },
            daysNamesCsv: {
                type: String,
                default: "Su,Mo,Tu,We,Th,Fr,Sa"
            },
        },
        data: function () {
            return {
                stringValue: Boolean,
                enableClass: 'btn-primary selected',
                disableClass: 'btn-outline-secondary',
                daysNames: this.daysNamesCsv.split(",")
            };
        },
        mounted() {
            this.stringValue = this.value
        },
        computed: {
            bitMaskValue() { return this.toWeekDayBitMask(this.value) },
        },
        methods: {
            toWeekDayString(val) {

                let days = ''

                if (val & sundayValue) days += sundayString
                if (val & mondayValue) days += mondayString
                if (val & tuesdayValue) days += tuesdayString
                if (val & wednesdayValue) days += wednesdayString
                if (val & thrusdayValue) days += thrusdayString
                if (val & fridayValue) days += fridayString
                if (val & saturdayValue) days += saturdayString

                return days
            },
            toWeekDayBitMask(days) {

                if (days == null) return 0

                if (!days.trim()) return 0

                //if (!validateString(days)) return 0

                let val = 0

                if (days.includes(sundayString)) { val = val + sundayValue; }
                if (days.includes(mondayString)) { val = val + mondayValue; }
                if (days.includes(tuesdayString)) { val = val + tuesdayValue; }
                if (days.includes(wednesdayString)) { val = val + wednesdayValue; }
                if (days.includes(thrusdayString)) { val = val + thrusdayValue; }
                if (days.includes(fridayString)) { val = val + fridayValue; }
                if (days.includes(saturdayString)) { val = val + saturdayValue; }

                return val
            },
            toggleDay(dayBitWise) {
                let newBitMask = this.bitMaskValue
                newBitMask ^= dayBitWise
                this.$emit('input', this.toWeekDayString(newBitMask))
            },
            //validateString = days => days.match(/^0?1?2?3?4?5?6?$/),
            handleInput: function (e) { this.$emit('input', e.target.value) }
        },
        watch: {
            value: function (newVal) { this.stringValue = this.toWeekDayString(this.toWeekDayBitMask(newVal)) }
        },
    };
</script>

<style scoped>
    input {
        width: 10ch !important;
    }

    button {
        
        padding: 0.3rem;
        min-width: 35px;
    }
        button.first-btn {
            border-color: #CED4DA;
        }
            button.first-btn.selected {
                border-right-color: white !important;
            }

        button.mid-btn {
            border-color: #CED4DA;
        }

        button.selected {
            background-color: darkblue;
            border-color: midnightblue;
        }

        

        button[disabled] {
            /*background-color: #e9ecef;*/
            pointer-events: none;
        }

        button.first-btn.invalid {
            border-color: #dc3545;
            
        }

        button.mid-btn.invalid {
            border-top-color: #dc3545;
            border-bottom-color: #dc3545;
            border-right-color: #ffcccc;
            border-left-color: #ffcccc;
        }

    input.invalid {
        border-top-color: #dc3545;
        border-bottom-color: #dc3545;
        border-right-color: #dc3545 !important;
    }
</style>