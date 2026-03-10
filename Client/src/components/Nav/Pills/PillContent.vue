<template>
  <div class="tab-pane fade" :class="[is_active ? 'active' : '', is_show ? 'show' : '']" :id="target">
    <slot />
  </div>
</template>

<script setup>
defineProps({
  target: {
    type: String,
  },
  is_active: {
    type: Boolean,
  },
  is_show: {
    type: Boolean,
  },
});
</script>