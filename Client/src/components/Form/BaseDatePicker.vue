<template>
    <div class="base-date-picker">
        <label class="form-label f-game fs-20 base-grey">{{ props.label }}<span class="text-danger" v-if="props.isRequired">*</span></label>
        <Datepicker 
            :name="name"
            :value="inputValue"
            :placeholder="props.placeholder"
            @update:modelValue="handleChange"
            @blur="handleBlur"
            :class="{ 'is-invalid': !!errorMessage, 'is-valid': meta.valid }"
            v-model="inputValue"
            :range="isDateRange" 
            :multiCalendars="isDateRange"
            autoApply 
            :closeOnAutoApply="true"
            :clearable="false"
        />
        <div 
        v-show="errorMessage || meta.valid"
        :class="{ 'invalid-feedback': !!errorMessage, 'valid-feedback': meta.valid }"
        >
            {{ errorMessage || props.successMessage }}
      </div>
    </div>
</template>

<script setup>
import { ref, computed } from 'vue';
import Datepicker from '@vuepic/vue-datepicker';
import { useField } from 'vee-validate';
import '@vuepic/vue-datepicker/dist/main.css'
import { string, array } from "yup"

const props = defineProps({
    name: {
        type: String,
        required: true,
    },
    label: {
        type: String,
        required: true,
    },
    successMessage: {
        type: String,
        default: '',
    },
    placeholder: {
        type: String,
        default: '',
    },
    isRequired: {
        type: Boolean,
        default: false
    },
    isDisabled: {
        type: Boolean,
        default: false
    },
    isMultiple: {
        type: Boolean,
        default: false
    }
});

const name = computed(() => props.name);
const isDateRange = computed(() => {
    return props.isMultiple;
});
const fieldRules = computed(() => {
    return props.isRequired ? isDateRange.value ? array().of(string().nullable(true).required()).nullable(true).required() : string().required() : undefined;
});

const {
  value: inputValue,
  errorMessage,
  handleBlur,
  handleChange,
  meta,
} = useField(name.value, fieldRules.value);

</script>

<style scoped>

</style>