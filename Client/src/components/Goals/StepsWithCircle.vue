<template>
    <div class="text-sm-bold shadow--lg rounded-lg d-flex align-items-center align-content-center" :style="{ 
        height: props.height + 'px',
        width: props.width + 'px' 
    }">
        <div class="text-number" :class="textColor">{{ props.currentStep + '/' + props.totalStep  }}</div>
    </div>
</template>

<script setup>
const props = defineProps({
    currentStep: {
        type: Number
    },
    totalStep: {
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
    margin: 0 auto;
    display: table;
}
</style>