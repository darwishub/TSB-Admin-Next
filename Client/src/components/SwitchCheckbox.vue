<template>
    <div class="form-check form-switch">
        <input class="form-check-input" type="checkbox" role="switch" :id="props.id">
        <label class="form-check-label" :for="props.for">
            <slot/>
        </label>
    </div>
</template>

<script setup>
    const props = defineProps({
        id: {
            type: String
        },
        for: {
            type: String
        }
    });
</script>