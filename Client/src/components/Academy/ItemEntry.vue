<template>
    <div class="card-goal-wrapper card shadow--sm radius-16">
        <div class="card-header bg-white radius-16 shadow--md">
            <div class="d-flex justify-content-between align-items-start">
                <Badge class="mb-3 goal-label me-2 green invisible">
                    No Label
                </Badge>
            </div>
            <div class="card-goal-title text-sm-bold f-game ls-1 text-center">{{
                    props.item?.Title
                }}</div>
        </div>
        <div class="card-body card-img p-0"
            :style="'background: url(https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/' + encodeURIComponent(props.item?.Logo?.trim()) + ')'">
        </div>
        <div class="card-footer bg-white radius-16 shadow--md cursor-pointer user-select-none d-flex align-items-center justify-content-center">
            <slot name="card-footer">
            </slot>
        </div>
    </div>

</template>

<script setup>
import Badge from "@/components/Badge.vue"
import Image from "@/components/Image.vue"
import Ionicon from "@/components/Ionicon.vue";
import Button from "@/components/Button.vue"
import { computed } from "vue"

const props = defineProps({
    item: {
        type: Object
    },
});

const labels = computed(() => {
    // return props.goal.label != "[]" ? props.goal?.label.replace(/\[|\]|\r|\n|\s+|"/g, '').split(',') : []
    return props.item?.Tag
});


</script>

<style scoped>
.card-goal-wrapper {
    width: 17rem;
}

.card-goal-wrapper .card-header {
    margin-bottom: -1rem;
}

.card-goal-wrapper .card-footer {
    margin-top: -1rem;
}

.card-goal-wrapper .card-header,
.card-goal-wrapper .card-footer {
    z-index: 99;
    border: none;
}

.card-goal-wrapper .card-img {
    height: 10rem;
    background-position: center !important;
    background-size: cover !important;
    background-repeat: no-repeat !important;
}

.card-goal-wrapper.card {
    border: none;
}

.card-goal-wrapper .card-goal-title {
    height: 3rem;
    line-height: 1rem;
    display: block;
    max-width: 100%;
    overflow: hidden;
    text-overflow: ellipsis;
}

.card-goal-info .goal-reward .text-reward {
    line-height: 1.5rem;
}

.card-goal-wrapper .goal-label {
    height: auto;
    line-height: 1rem;
    display: block;
    max-width: 100%;
    overflow: hidden;
    text-overflow: ellipsis;
}
</style>