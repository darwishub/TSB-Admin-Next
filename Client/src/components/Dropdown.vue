<template>
  <div class="dropdown">
    <a
      :class="{'dropdown-toggle' : showToggle }"
      data-bs-toggle="dropdown"
      aria-expanded="false"
    >
      <slot name="menu-content"></slot>
    </a>
    <ul class="dropdown-menu" :class="['dropdown-menu-' + position ]" :style="{ minWidth: minWidth + 'rem' }">
      <slot name="menu-list"></slot>
    </ul>
  </div>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { Dropdown } from 'bootstrap/dist/js/bootstrap.bundle.min.js'

defineProps({
    position: {
        type: String
    },
    showToggle: {
        type: Boolean,
        default: true
    },
    minWidth: {
        type: Number
    }
});

onMounted(() => {

    new Dropdown(document.body, {
        autoClose: true,
    });

});

</script>