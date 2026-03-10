<template>
    <div class="type text-black fs-24 text-border-orange f-game ls-1" :class="{
        'text-disabled': props.levelNumber == 1,
        'text-info': props.levelNumber == 2,
        'text-green': props.levelNumber == 3,
        'text-warning': props.levelNumber > 3,
    }">
        Lvl: {{ textMasking }}</div>
</template>

<script setup>
import { computed } from "vue";

const props = defineProps({
    levelNumber: {
        type: Number
    }
});

const textMasking = computed(() => {
    return props.levelNumber > 3 ? "MAX" : props.levelNumber - 1;
})

</script>