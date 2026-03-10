<template>
    <div class="card-wrapper radius-16 bg-white px-5 pb-5 pt-4 shadow-out--sm">
        <div class="mb-c-2" v-if="props.title != ''">
            <div class="card-wrapper-title f-game fs-24 text-center" v-if="props.title != undefined || props.title != ''">{{ props.title }}</div>
            <div class="card-wrapper-description fs-14 grey text-sm-bold my-5">{{ props.description }}</div>
        </div>
        <slot/>
    </div>
</template>

<script setup>
import Button from "@/components/Button.vue"
import Ionicon from "@/components/Ionicon.vue";

const props = defineProps({
    title: {
        type: String
    },
    description: {
        type: String
    },
    id: {
        type: Number
    },
    hasOption: {
        type: Boolean
    },
    option: {
        type: Array
    },
    displayOrder: {
        type: Number
    },
    label: {
        type: String
    }
});

</script>

<style scoped>
/* .card-wrapper .card-wrapper-description {
    display: none;
} */
.card-wrapper.show-description .card-wrapper-description {
    display: block;
}
.goal-builder-active .card-wrapper-title {
    font-size: 32px;
    color: #416094;
}
</style>