<template>
  <div class="base-input-switch" :id="idField">
  
        <label class="f-game fs-20 base-grey form-label">{{ label }}</label>
  
          <Toggle  
              :name="name"
              @change="[handleChange, emits('onSwitchChange', $event)]"
              @blur="handleBlur"
              :class="{ 'is-invalid': !!errorMessage, 'is-valid': meta.valid }"
              v-model="inputValue"
              :falseValue="falseVal"
              :trueValue="trueVal"
              ref="toggleRef"
          />
  
        <div 
          v-show="errorMessage || meta.valid"
          :class="{ 'invalid-feedback': !!errorMessage, 'valid-feedback': meta.valid }"
          >
              {{ errorMessage || successMessage }}
        </div>
        <div class="fs-16 text-md mt-3 switch-title" :class="{ 'd-none' : !props.isValueVisible }">{{ inputValue }}</div>
  </div>
  
    </template>
  
  
  <script setup>
  import { ref, computed, toRefs } from "vue"
  import { useField } from 'vee-validate';
  import Toggle from '@vueform/toggle'
  import { boolean } from "yup";
  
  const props = defineProps({
    value: {
      type: [Boolean, String, Number],
      default: false
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
    falseValue: {
      type: [String, Number, Boolean],
      default: false
    },
    trueValue: {
      type: [String, Number, Boolean],
      default: true
    },
    options: {
      type: Array
    },
    isValueVisible: {
      type: Boolean,
      default: false,
    }
  });
  
  const name = computed(() => props.name);
  
  const idField = computed(() => {
    return String(props.name).replace(/[^0-9a-z]/gi, '');
  });
  
  const emits = defineEmits(['onSwitchChange']);
  const toggleRef = ref();
  defineExpose({
    toggleRef
  });
  
  const fieldRules = computed(() => {
      return props.isRequired ? boolean().isTrue() : undefined;
  });
  
  const trueVal = computed(() => {
    return props.options !== undefined ? props.options[0] : props.trueValue;
  });
  
  const falseVal = computed(() => {
    return props.options !== undefined ? props.options[1] : props.falseValue;
  });
  
  const {
    value: inputValue,
    errorMessage,
    handleBlur,
    handleChange,
    meta,
  } = useField(name.value, props.isRequired ? value => {
    if(value != trueVal.value){
      return "Action is required."
    }else {
      return true;
    }
  } : undefined, {
    initialValue: props.value,
    validateOnMount: false
  });
  
  </script>
  
  <style src="@vueform/toggle/themes/default.css"></style>
  <style scoped>
  </style>