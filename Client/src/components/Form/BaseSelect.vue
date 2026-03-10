<template>
  <div class="base-select" :id="idField">
      <label class="form-label f-game fs-20 base-grey">{{ props.label }}<span class="text-danger" v-if="props.isRequired">*</span></label>
        <VueSelect
          :name="name"
          v-model="inputValue" 
          :options="getOptions" 
          :placeholder="props.placeholder"
          @update:modelValue="[handleChange, emits('onSelect', $event)]"
          @selected="onSelected"
          @blur="handleBlur"
          :close-on-select="true"
          :class="{ 'is-invalid': !!errorMessage, 'is-valid': meta.valid }"
          :label-by="props.labelBy"
          :value-by="props.valueBy"
          :empty-model-value="props.emptyModelValue"
          :hide-selected="props.hideSelected"
          :searchable="isMultiple"
          :multiple="isMultiple"
          ref="selectRef"
        >
            <template #label="{ selected }">
              <slot :selected="selected" name="selected"></slot>
            </template>
            <template #dropdown-item="{ option }">
              <slot :option="option" name="option"></slot>
            </template>
        </VueSelect>

        <Badge class="mt-2 me-2 green" v-for="label in currentValue" v-if="props.isMultiple">{{ label }} </Badge>
      <div 
      v-show="errorMessage || meta.valid"
      :class="{ 'invalid-feedback': !!errorMessage, 'valid-feedback': meta.valid }"
      >
          {{ errorMessage || props.successMessage }}
    </div>

  </div>

</template>

<script setup>
import VueSelect from 'vue-next-select'
import { computed, ref, toRefs, toRaw } from "vue"
import { useField, useIsFieldDirty } from 'vee-validate';
import Badge from "@/components/Badge.vue"
import { string, array } from "yup";

const props = defineProps({
type: {
  type: String,
},
options: {
  type: Array
},
typeRule: {
  type: String
},
value: {
  type: [String, Number, Object],
  default: ''
},
maxData: {
  type: Number,
  default: 0
},
name: {
  type: String,
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
  type: String
},
valueBy: {
  type: String
},
emptyModelValue: {
  type: String,
  default: ''
},
ruleValidation: {
  type: [Function, Object]
},
hideSelected: {
  type: Boolean,
  default: true
}

});
const emits = defineEmits(['onSelect', 'onSelected']);
const selectRef = ref();

const name = computed(() => {
  return props.name;
});

const { value : defaultValue, options } = toRefs(props);

const idField = computed(() => {
  return String(name.value).replace(/[^0-9a-z]/gi, '');
});
const isNumberSelect = computed(() => {
  return props.typeRule == 'number';
});
const getOptionsValue = computed(() => {
return options.value ?? [];
})
const optionDataNumber = computed(() => {
  let arrList = ref([]);
  for(let i = getOptionsValue.value[0]; i <= getOptionsValue.value[1]; i++){
    arrList.value.push(i);
  }
  return arrList.value;
});
const getOptions = computed(() => {
  return isNumberSelect.value ? optionDataNumber.value : getOptionsValue.value;
});
const fieldRules = computed(() => {
  return props.isRequired ? props.isMultiple ? array().of(string().nullable().required()).min(1).required() : string().ensure().required() : undefined;
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
initialValue: defaultValue.value,
});

const currentValue = computed(() => {
  return Array.isArray(inputValue.value) ? inputValue.value : inputValue.value != "" && props.isMultiple == true  ? JSON.parse(inputValue.value): [];
});

const selectedValue = (e) => {
  return e;
}

const onSelected = (e) => {
  emits('onSelected', e)
}

defineExpose({
  selectRef,
  selectedValue,
  handleChange
});

</script>

<style src="vue-next-select/dist/index.min.css"></style>