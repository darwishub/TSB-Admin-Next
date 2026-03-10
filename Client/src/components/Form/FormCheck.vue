<template>
      <input
      class="form-check-input"
      type="checkbox"
      :value="modelValue"
      @change="$emit('update:modelValue', $event.target.value)"
      v-bind="$attrs"
    />
    <slot/>
</template>

<script setup>
defineProps({
  modelValue: {
    type: Boolean,
  },
});

defineEmits(["update:modelValue"]);
</script>