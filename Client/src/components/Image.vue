<template>
  <img
    class="img-tsb"
    :style="{ 
        height: props.height + 'px !important',
        width: props.width + 'px !important' 
    }"
  />
</template>

<script setup>
const props = defineProps({
    height: {
        type: String
    },
    width: {
        type: String
    }
});
</script>

<style scoped>
.img-tsb {
    object-fit: cover;
}
</style>