<template>
    <div class="shadow-in--sm-2 radius-16 goal-value w-100">
        <p class="fs-14 mb-0 grey text-break-all">{{ props.value }}</p>
    </div>
</template>

<script setup>
const props = defineProps({
    value: {
        type: String
    }
});
</script>

<style scoped>
.goal-value {
    width: fit-content;
    padding: 1rem 2rem;
    margin: 0 auto;
}
</style>