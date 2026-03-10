<template>
  <div class="base-select" :id="idField">
    <label class="form-label f-game fs-20 base-grey">{{ props.label }}<span class="text-danger" v-if="props.isRequired">*</span></label>
    <VueSelect 
      :name="name" 
      v-model="inputValue" 
      :options="CountriesList" 
      :placeholder="props.placeholder"
      @update:modelValue="[handleChange, emits('onSelect', $event)]" 
      @blur="handleBlur" 
      :close-on-select="true"
      :class="{ 'is-invalid': !!errorMessage || !meta.dirty, 'is-valid': meta.valid && meta.dirty }"
      :label-by="props.labelBy" 
      :value-by="props.valueBy" 
      :empty-model-value="props.emptyModelValue" 
      :searchable="props.searchable"
      :search-placeholder="props.searchPlaceholder"
      :hide-selected="true"
      @selected="onSelected"
      ref="selectRef">
      
      <template #label="{ selected }">
        <slot :selected="selected" name="selected"></slot>
      </template>
      <template #dropdown-item="{ option }">
        <slot :option="option" name="option"></slot>
      </template>
    </VueSelect>

    <div v-show="errorMessage || meta.valid"
      :class="{ 'invalid-feedback': !!errorMessage || !meta.dirty, 'valid-feedback': meta.valid && meta.dirty }">
      {{ errorMessage || props.successMessage }}
    </div>

  </div>

</template>

<script setup>
import VueSelect from 'vue-next-select'
import { countries } from "i18n-iso-countries/langs/en.json"
import { computed, ref, toRefs } from "vue"
import { useField, useIsFieldDirty } from 'vee-validate';
import { string } from "yup";

const props = defineProps({
  type: {
    type: String,
  },
  typeRule: {
    type: String
  },
  value: {
    type: [String, Number],
    default: ''
  },
  maxData: {
    type: Number,
    default: 0
  },
  name: {
    type: String,
    required: true,
  },
  label: {
    type: String,
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
  },
  labelBy: {
    type: String,
    default: "label"
  },
  valueBy: {
    type: String,
    default: "value"
  },
  emptyModelValue: {
    type: String,
    default: ''
  },
  ruleValidation: {
    type: [Function, Object]
  },
  searchable: {
    type: Boolean,
    default: true
  },
  searchPlaceholder: {
    type: String,
    default: "Select on these options"

  }
});
const emits = defineEmits(['onSelect']);
const selectRef = ref();
const onSelected = (e) => {
  selectRef.value.input.input.value = e.label;
}

const CountriesList = computed(() => {
  return Object.keys(countries).map((key) => ({ value: key, label: Array.isArray(countries[key]) ? countries[key][0] : countries[key] }));
});

const name = computed(() => {
  return props.name;
})
const idField = computed(() => {
  return String(name.value).replace(/[^0-9a-z]/gi, '');
});

const fieldRules = computed(() => {
  return props.isRequired ? string().ensure().required() : undefined;
});

const hasCustomeRule = computed(() => {
  return props.ruleValidation !== undefined && props.isRequired ? props.ruleValidation : fieldRules.value;
});

const {
  value: inputValue,
  errorMessage,
  handleBlur,
  handleChange,
  meta,
} = useField(name.value, hasCustomeRule.value, {
  initialValue: ""
});

const selectedValue = (e) => {
  return e;
}

defineExpose({
  selectRef,
  selectedValue
});

</script>

<style src="vue-next-select/dist/index.min.css">

</style>