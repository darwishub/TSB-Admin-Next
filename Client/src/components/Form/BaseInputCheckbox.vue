<template>
  
  <div class="form-check base-input-checkbox">
  
        <label :for="`${idField}${props.id}`" class="form-check-label ms-2 text-md fs-14 user-select-none cursor-pointer">{{
            props.value
        }}</label>

        <input 
          :name="name" 
          class="form-check-input" 
          :type="checkFieldType" 
          :id="`${idField}${props.id}`"
          :value="props.value"
          @change="handleChange"
          ref="checkboxRef"
          :class="{ 'is-invalid': !!errorMessage, 'is-valid': meta.valid }"
          :checked="checked"
        >
        
        <div 
            v-if="errorMessage && props.id == props.options.length - 1 || meta.valid && props.id == props.options.length - 1"
            :class="{ 'invalid-feedback': !!errorMessage, 'valid-feedback': meta.valid }"
          >
            {{ errorMessage || successMessage }}
        </div>

  </div>

</template>

<script setup>
import { ref, computed, toRefs } from "vue"
import { useField, Field } from 'vee-validate';
import { string, array } from "yup";

const props = defineProps({
  options: {
    type: Array
  },
  type: {
    type: String,
    default: 'checkbox',
  },
  name: {
    type: String,
    required: true,
  },
  label: {
    type: String,
  },
  value: {
    type: String,
  },
  placeholder: {
    type: String,
    default: '',
  },
  successMessage: {
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
  id: {
    type: [String, Number]
  },
  typeRule: {
    type: String
  },
  maxData: {
    type: Number
  },
  isMultiple: {
    type: Boolean
  },
  isValueVisible: {
    type: Boolean
  }
});
const checkboxRef = ref();

const checkFieldType = computed(() => {
  return props.type === 'checkbox' ? props.type : 'radio' 
});

const name = props.name;

const idField = computed(() => {
  return String(name).replace(/[^0-9a-z]/gi, '');
});

const isCheckbox = computed(() => {
  return props.type === 'checkbox' ? true : false;
});

const fieldRules = computed(() => {
  return props.isRequired ? isCheckbox.value ? array().of(string().required().nullable()).min(1).required() : string().required() : undefined;
});

const {
  value,
  errorMessage,
  handleBlur,
  handleChange,
  checked,
  meta,
} = useField(name, fieldRules.value, {
  type: checkFieldType.value,
  checkedValue: props.value,
});

defineExpose({
  checkboxRef,
  handleChange,
  checked
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

.form-check .form-check-input {
  margin-left: 0px;
}

.base-input-checkbox .form-check-input {
  box-shadow: inset 4px 4px 4px rgb(65 96 148 / 20%) !important;
  border-radius: 100px;
  border: none;
}

.base-input-checkbox .form-check-input:checked {
  background-color: #0F934C;
  -webkit-box-shadow: 2px 2px 4px rgba(114, 142, 171, 0.1), -6px -6px 20px #FFFFFF, 4px 4px 20px rgba(111, 140, 176, 0.41) !important;
  -moz-box-shadow: 2px 2px 4px rgba(114, 142, 171, 0.1), -6px -6px 20px #FFFFFF, 4px 4px 20px rgba(111, 140, 176, 0.41) !important;
  box-shadow: 2px 2px 4px rgba(114, 142, 171, 0.1), -6px -6px 20px #FFFFFF, 4px 4px 20px rgba(111, 140, 176, 0.41) !important;
}

.form-check-input.is-invalid:focus,
.was-validated .form-check-input:invalid:focus,
.form-check-input.is-valid:focus,
.was-validated .form-check-input:valid:focus {
  box-shadow: none;
}

.form-check.base-input-checkbox {
  margin-bottom: 0.5rem;
}
</style>