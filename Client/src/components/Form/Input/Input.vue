<template>
    <input
      :type="type"
      class="form-control"
      :placeholder="placeholder"
      v-model="value"
      :name="name"
      v-bind="$attrs"
    />
    <slot/>
</template>

<script setup>
import { useField } from "vee-validate";

const props = defineProps({
  placeholder: {
    type: String,
  },
  type: {
    type: String,
  },
  name: { 
    type: String, 
    required: true 
  },
});

const { value, errorMessage } = useField(props.name);

</script>

