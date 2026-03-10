<template>
    <div
      class="base-textarea"
    >
      <label :for="idField" class="form-label f-game fs-20 base-grey">{{ label }}<span class="text-danger" v-if="props.isRequired">*</span></label>
      <textarea 
        :name="name"
        :id="idField"
        :placeholder="placeholder"
        @input="handleChange"
        @blur="handleBlur"
        class="form-control"
        v-model="inputValue"
        :class="{ 'is-invalid': !!errorMessage, 'is-valid': meta.valid }"
        rows="5">
    </textarea>
  
      <div 
        v-show="errorMessage || meta.valid"
        :class="{ 'invalid-feedback': !!errorMessage, 'valid-feedback': meta.valid }"
        >
            {{ errorMessage || successMessage }}
      </div>
    </div>
  </template>

<script setup>
import { ref, computed, toRefs } from "vue"
import { useField } from 'vee-validate';
import { string } from "yup"

const props = defineProps({
  value: {
    type: [String, Boolean, Number],
    default: '',
  },
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
  ruleValidation: {
    type: [Function, Object]
  }
});

const name = computed(() => props.name);

const fieldRules = computed(() => {
    return props.isRequired ? string().nullable().required() : undefined;
});

const hasCustomeRule = computed(() => {
return props.ruleValidation !== undefined && props.isRequired ? props.ruleValidation : fieldRules.value;
});

const idField = computed(() => {
  return String(name.value).replace(/[^0-9a-z]/gi, '');
});

const {
  value: inputValue,
  errorMessage,
  handleBlur,
  handleChange,
  meta,
} = useField(name.value, hasCustomeRule.value, {
  initialValue: props.value,
});

</script>

<style scoped>
.base-textarea textarea {
    -webkit-box-shadow: inset 4px 4px 4px rgb(65 96 148 / 20%);
    -moz-box-shadow: inset 4px 4px 4px rgb(65 96 148 / 20%);
    box-shadow: inset 4px 4px 4px rgb(65 96 148 / 20%);
    -webkit-border-radius: 20px;
    -moz-border-radius: 20px;
    border-radius: 1rem;
    border: none;
    padding: 1rem 1.25rem !important;
    color: #113A62;
    font-weight: 500;
    font-size: 14px;
}
</style>