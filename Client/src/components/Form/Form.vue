<template>
    <form @submit.prevent="action">
        <slot/>
    </form>
</template>

<script setup>
defineProps({
    action: {
        type: Function
    }
});
</script>