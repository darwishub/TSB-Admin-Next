<template>
    <div class="text-sm-bold shadow--lg rounded-lg" :style="{ 
        height: props.height + 'px',
        width: props.width + 'px' 
    }">
        <div class="text-number" :class="textColor">{{ props.numberText }}</div>
    </div>
</template>

<script setup>
const props = defineProps({
    numberText: {
        type: Number
    },
    height: {
        type: String
    },
    width: {
        type: String
    },
    textColor: {
        type: String
    }
});
</script>

<style scoped>
.text-number {
    padding: 12px;
    margin: 0 auto;
    display: table;
}
</style>