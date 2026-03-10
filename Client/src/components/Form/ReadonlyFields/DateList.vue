<template>
    <div class="shadow-in--sm-2 radius-16 goal-value w-100">
        <ul v-if="isArray" class="ps-3 mb-0">
            <li v-for="(item, index) in items" class="grey">
                <p class="fs-14 mb-0 grey text-break-all">{{ item }}</p>
            </li>
        </ul>
        <p v-else class="fs-14 mb-0 grey text-break-all">{{ props.value }}</p>
    </div>
</template>

<script setup>
import { computed } from "vue";
const props = defineProps({
    value: {
        type: String
    }
});

const items = computed(() => {
    if (props.value.includes('[') && props.value.includes(']')) {
        return JSON.parse(props.value);
    } else {
        return props.value;
    }
});

const isArray = computed(() => {
    return props.value.includes('[') && props.value.includes(']');
});

</script>

<style scoped>
.goal-value {
    width: fit-content;
    padding: 1rem 2rem;
    margin: 0 auto;
}
</style>