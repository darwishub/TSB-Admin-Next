<template>
  <input
    type="radio"
    :value="props.modelValue"
    @change="$emit('update:modelValue', $event.target.value)"
    :name="props.name"
  />
</template>

<script setup>
const props = defineProps({
  modelValue: {
    type: String,
  },
  name: { 
    type: String, 
    required: true 
  },
});

defineEmits(["update:modelValue"]);
</script>