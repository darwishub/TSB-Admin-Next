<template>
    <div class="level-bar" :class="{ 
        'gray-bg-2' : props.levelNumber == 1,
        'blue-bg' : props.levelNumber == 2,
        'green-bg' : props.levelNumber == 3,
        'gold-bg' : props.levelNumber > 3,
    }"></div>
</template>

<script setup>
const props = defineProps({
    levelNumber: {
        type: Number,
        default: 0
    }
});
</script>

<style scoped>

</style>