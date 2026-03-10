<template>
  <div class="base-pick-slider">
    <label class="f-game fs-20 base-grey mb-3">{{ props.label }}</label>
      <Slider 
          v-model="sliderValue"
          :name="name"  
          :lazy="false"
          :min="minValue"
          :max="maxValue"
          :class="{ 'is-invalid': !!errorMessage, 'is-valid': meta.valid }"
          :tooltips="false"
      />
      <br/>
      <div 
      v-show="errorMessage || meta.valid"
      :class="{ 'invalid-feedback': !!errorMessage, 'valid-feedback': meta.valid }"
      >
          {{ errorMessage || successMessage }}
    </div>

      <div class="mx-auto d-table fs-16 text-md" v-if="isTypeRuleText">{{ valueTransform }}</div>
      <div class="mx-auto d-table fs-16 text-md" v-else>{{ sliderValue }}</div>

  </div>
</template>

<script setup>
import Slider from '@vueform/slider'
import { useField } from 'vee-validate';
import { string } from "yup";
import { computed, ref, watch } from "vue"

const props = defineProps({
  type: {
  type: String,
  default: 'text',
},
typeRule: {
  type: String
},
value: {
  type: String,
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
isMultiple: {
  type: Boolean,
  default: false
},
options: {
  type: Array
},
});

const name = computed(() => props.name);

const isTypeRuleText = computed(() => {
return props.typeRule =='text';
});

const minValue = computed(() => {
return isTypeRuleText.value ? props.options.indexOf(props.options[0]) : parseInt(props.options[0]);
});
const sliderValue = ref(minValue.value);

const maxValue = computed(() => {
return isTypeRuleText.value ? props.options.indexOf(props.options[props.options.length - 1]) : parseInt(props.options[props.options.length - 1]);
});

const valueTransform = computed(() => {
if(isTypeRuleText.value){
  return props.options[sliderValue.value];
} 
});

const setInitValue = computed(() => {
  return isTypeRuleText.value ? valueTransform.value : minValue.value;
});

const {
errorMessage,
handleBlur,
handleChange,
meta
} = useField(name.value, undefined, {
  initialValue : setInitValue.value
});

watch(sliderValue, (newValue) => {

  if (isTypeRuleText.value) {
    const textValue = props.options[sliderValue.value];
    handleChange(textValue);
  } else {
    handleChange(newValue);
  }

});


</script>

<style src="@vueform/slider/themes/default.css">

</style>
<style scoped>

</style>