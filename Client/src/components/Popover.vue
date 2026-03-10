<template>
  <div
    class="d-inline-block"
    tabindex="0"
    data-bs-toggle="popover"
    data-bs-trigger="hover focus"
    :data-bs-content="content"
    ref="info"
  >
    <slot />
  </div>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { Popover } from 'bootstrap/dist/js/bootstrap.esm.min.js'

defineProps({
  content: {
    type: String,
  },
});

const info = ref(null);
let infoPopover = null;

onMounted(() => {
    infoPopover = new Popover(info.value);
});

</script>