<template>
    <div class="card-goal-wrapper card shadow--sm radius-16">
        <div class="card-header bg-white radius-16 shadow--md">
            <div class="row justify-content-between align-items-center">
                <div class="col-8">
                    <div class="d-flex justify-content-start align-items-center">
                        <Badge class="goal-label me-2 green" v-for="label in labels">{{ label }} </Badge>
                    </div>
                </div>
                <div class="col-4">
                    <div class="d-flex justify-content-end align-items-center">
                        <TextLevelGoal :level-number="props.item?.IdLevel"  
                        :class="{invisible: props.item?.IdLevel == 1}"
                        />
                        <div class="category-logo ms-3"
                        :class="{invisible: props.item?.IdLevel == 1}"
                        >
                            <Image class="img-fluid of-contain" :src="props.item?.CategoryLogo" width="30" height="30">
                            </Image>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card-goal-title text-sm-bold f-game ls-1 text-center fs-24 my-3">{{
                    props.item.Title
                }}</div>
            <div class="d-flex justify-content-between">
                <div class="text-sm-bold fs-14 grey goal-description mx-auto">{{ props.item?.Description }}</div>
                <div
                    class="shadow-in--sm radius-16 d-flex align-items-center align-content-center card-goal-info py-1 px-3"
                    v-if="props.item?.item_type == 0"
                    >
                    <div class="text-sm-bold fs-16 text-uppercase text-warning-2 lh-c-1">{{ DateCreated }}</div>
                </div>
            </div>
        </div>
        <div class="card-body card-img p-5"
            :style="'background: linear-gradient(180deg,rgba(65, 96, 148, 0.15) 75%, #416094 100%),url(' + logoUrlTransform + ')!important'">
        </div>
        <div class="card-footer radius-16 shadow--sm p-3 cursor-pointer user-select-none bg-white">
            <slot name="card-footer">
            </slot>
        </div>
    </div>
</template>

<script setup>
import Ionicon from "@/components/Ionicon.vue";
import Badge from "@/components/Badge.vue"
import Image from "@/components/Image.vue"
import Steps from "@/components/Goals/Steps.vue"
import StepsWithCircle from "@/components/Goals/StepsWithCircle.vue"
import TextLevelGoal from "@/components/Goals/TextLevelGoal.vue"
import { computed } from "vue"
import Button from "@/components/Button.vue"
import { useDateFormat } from '@vueuse/core'


const props = defineProps({
    item: {
        type: Object
    }
});

const labels = computed(() => {
    return props.item?.Tag != "[]" ? props.item?.Tag.replace(/\[|\]|\r|\n|\s+|"/g, '').split(',') : [];
})

const logoUrlTransform = computed(() => {
    return `https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${encodeURIComponent(props.item?.Logo?.trim())}`;
});

const DateCreated = computed(() => {
    if(props.item?.DateCreated != undefined){
        let date = useDateFormat(props.item?.DateCreated, 'DD MMM YYYY');
        return date.value;
    }  
});

</script>

<style scoped>
.card-goal-wrapper .card-header {
    margin-bottom: -1.5rem;
    padding: 1rem 1rem 1.5rem 1rem;
}

.card-goal-wrapper .card-footer {
    margin-top: -1.5rem;
}

.card-goal-wrapper .card-header,
.card-goal-wrapper .card-footer {
    z-index: 99;
    border: none;
}

.card-goal-wrapper .card-img {
    background-position: center !important;
    background-size: cover !important;
    background-repeat: no-repeat !important;
    display: flex;
    justify-content: center;
    align-items: start;
    height: 1000px;
    max-height: 360px;
}

.card-goal-wrapper.card {
    border: none;
}

.card-goal-wrapper .card-goal-title {
    height: 48px;
    line-height: 1.5rem;
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

.card-goal-wrapper .goal-description {
    max-width: 75%;
    height: 4rem;
    display: block;
    overflow: hidden;
    text-overflow: ellipsis;
}

.card-goal-wrapper .card-body .body-content {
    max-width: 366px;
    width: 100%;
}
</style>