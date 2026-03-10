<template>
  <li class="nav-item" role="presentation">
    <button
      class="nav-link"
      :class="is_active ? 'active' : ''"
      data-bs-toggle="pill"
      :data-bs-target="'#' + target"
    >
      <slot />
    </button>
  </li>
</template>

<script setup>
defineProps({
  target: {
    type: String,
  },
  is_active: {
    type: Boolean,
  },
});
</script>