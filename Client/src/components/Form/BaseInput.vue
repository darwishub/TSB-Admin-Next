<template>
  <div
    class="base-input"
  >

    <label :for="idField" class="form-label f-game fs-20 base-grey">{{ label }}<span class="text-danger" v-if="props.isRequired">*</span></label>
      <template v-if="isNumberInput">
        <input 
          :name="name" 
          :id="idField" 
          :type="props.type" 
          :placeholder="props.placeholder" 
          @blur="handleBlur" 
          @input="onInput"
          class="form-control" 
          :class="{ 'is-invalid': !!errorMessage, 'is-valid': meta.valid }"
          onkeypress="return event.charCode >= 48 && event.charCode <= 57"
          ref="inputValueRef"
          :value="inputValue"
          :disabled="props.isDisabled"
          />
      </template>
      <template v-else-if="isMoneyInput">
        <input 
          :name="name" 
          :id="idField" 
          :type="props.type" 
          :placeholder="props.placeholder" 
          @blur="handleBlur" 
          class="form-control" 
          :class="{ 'is-invalid': !!errorMessage, 'is-valid': meta.valid }"
          onkeypress="return event.charCode >= 48 && event.charCode <= 57"
          ref="inputRef"
          :value="formattedValue"
          :disabled="props.isDisabled"
          />
      </template>
      <template v-else>
        <input
          :name="name"
          :id="idField"
          :type="props.type"
          :placeholder="props.placeholder"
          @input="onInput"
          @blur="handleBlur"
          class="form-control"
          :class="{ 'is-invalid': !!errorMessage, 'is-valid': meta.valid }"
          ref="inputValueRef"
          :value="inputValue"
          :disabled="props.isDisabled"
          />
      </template>

    <div 
      v-show="errorMessage || meta.valid"
      :class="{ 'invalid-feedback': !!errorMessage, 'valid-feedback': meta.valid }"
      >
          {{ errorMessage || successMessage }}
    </div>
  </div>
</template>

<script setup>
import { computed, ref, watch, toRefs } from "vue"
import { useField } from 'vee-validate';
import { string, number } from "yup";
import { useCurrencyInput } from "vue-currency-input";

const props = defineProps({
type: {
  type: String,
  default: 'text',
},
typeRule: {
  type: String,
  default: ""
},
value: {
  type: [String, Number]
},
name: {
  type: String,
},
maxData: {
  type: Number,
  default: 0
},
label: {
  type: String,
},
successMessage: {
  type: String,
},
placeholder: {
  type: String,
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
},
hideCurrencySymbol: {
  type: Boolean,
  default: false  
}
});
const emits = defineEmits(['getInputValue', 'update:modelValue', 'change']);
const { value } = toRefs(props);
const Type = {
'text' : string().nullable().required(),
'email' : string().nullable().email().required(),
'url' : string().url().nullable().required()
}

const TypeWithRule = {
'text': {
  'linkedin' : string()
      .matches(
        /^(http(s)?:\/\/)?([\w]+\.)?linkedin\.com\/(pub|in|profile|company)/,
          'this must be a valid LinkedIn URL'
      )
      .required("this is a required field"),
  'text' : string().nullable().required(),
  'number': number().nullable().required("this is a required field"),
  'money': string().nullable().required("this is a required field"),
  'email' : string().nullable().email().required(),
  'url' : string().url().nullable().required(),
  'coins' : number().max(50000).required("Max coins is 50.000")
}
}

const inputRefNull = ref(null);
const { inputRef, numberValue, formattedValue } = useCurrencyInput({
"currency": "USD",
"currencyDisplay": props.typeRule == 'money' && !props.hideCurrencySymbol ? "symbol" : "hidden",
"precision": 0,
"hideCurrencySymbolOnFocus": props.typeRule == 'money' ? false : true,
"hideGroupingSeparatorOnFocus": props.typeRule == 'money' ? false : true,
"hideNegligibleDecimalDigitsOnFocus": false,
"autoDecimalDigits": false,
"valueScaling": "precision",
"useGrouping": props.typeRule == 'money' ? true : false,
"accountingSign": false
});

const isNumberInput = computed(() => {
  return defaultType.value == 'text' && props.typeRule == 'number' || defaultType.value == 'text' && props.typeRule == 'coins';
});

const isMoneyInput = computed(() => {
  return defaultType.value == 'text' && props.typeRule == 'money';
})

const name = computed(() => props.name);

const idField = computed(() => {
return String(name.value).replace(/[^0-9a-z]/gi, '');
});

const defaultType = computed(() => {
  return props.type == "" ? 'text' : props.type;
})

const fieldRules = computed(() => {
  return props.isRequired ? props.typeRule == "" || props.typeRule == undefined ? Type[props.type] : TypeWithRule[defaultType.value][props.typeRule] : undefined;
});

const hasCustomeRule = computed(() => {
return props.ruleValidation !== undefined && props.isRequired ? props.ruleValidation : fieldRules.value;
});

const {
value: inputValue,
errorMessage,
handleBlur,
handleChange,
resetField,
meta,
validate,
setErrors
} = useField(name.value, hasCustomeRule.value, {
initialValue: value.value,
});

const inputValueRef = ref();
defineExpose({
inputValueRef,
validate,
handleChange,
resetField
});

const onInput = (e) => {
emits('getInputValue', e.target.value);
handleChange(e.target.value);
}

watch(numberValue, (newValue) => {

  if (newValue === '') {
    handleChange(undefined);
    return;
  }

  const newNumber = Number(newValue);

  if (Number.isNaN(newNumber)) {
    return;
  }
  handleChange(newNumber);

});

watch(inputValue, (newValue) => {

if(props.typeRule == 'number' || props.typeRule == 'coins') {
  
    if (newValue === '') {
      handleChange(undefined);
      return;
    }

    const newNumber = Number(newValue);

    if (Number.isNaN(newNumber)) {
      return;
    }

    handleChange(newNumber);
}

});

</script>

<style scoped>
.base-input input {
  -webkit-box-shadow: inset 4px 4px 4px rgb(65 96 148 / 20%);
  -moz-box-shadow: inset 4px 4px 4px rgb(65 96 148 / 20%);
  box-shadow: inset 4px 4px 4px rgb(65 96 148 / 20%);
  -webkit-border-radius: 20px;
  -moz-border-radius: 20px;
  border-radius: 1rem;
  border: none;
  height: 50px;
  padding: 1rem 2.5rem 1rem 1.25rem !important;
  color: #113A62;
  font-weight: 500;
  font-size: 14px;
}
</style>