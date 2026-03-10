<template>
    <div class="card-goal-wrapper card shadow--sm radius-16">
        <div class="card-header bg-white radius-16 shadow--md">
            <div class="d-flex justify-content-between align-items-start">
                <Badge class="mb-3 goal-label me-2 blue invisible">
                    No Data
                </Badge>
                <div
                    class="shadow-in--sm radius-16 d-flex align-items-center align-content-center card-goal-info py-1 px-3"
                    :class="{ 'invisible' : !props.item?.item_type == 0 }"
                    >
                    <div class="text-sm-bold fs-12 text-uppercase text-warning">{{ eventDate }}</div>
                </div>
            </div>
            <div class="card-goal-title text-sm-bold f-game ls-1 text-center">{{
                    props.item?.name
                }}</div>
        </div>
        <div class="card-body card-img p-0"
            :style="'background: url(https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/' + encodeURIComponent(props.item?.logo.trim()) + ')'">
        </div>
        <div class="card-footer bg-white radius-16 shadow--md cursor-pointer user-select-none d-flex align-items-center justify-content-center">
            <slot name="card-footer">
            </slot>
        </div>
        <!-- <div class="card-footer radius-16 shadow--md bg-white">
            <div class="d-flex justify-content-between align-content-start card-goal-info">
                <Button type="button" class="mx-auto d-table rounded-md w-100 btn-game px-0 py-1"
                :class="props.item?.action_type == 0 ? 'blue' : props.item?.action_type == 1 ? 'gold' : 'green'"
                >
                    <div class="d-flex justify-content-center align-items-center">
                        <div class="text-button text-border f-game fs-20 ls-1">
                            {{ props.item?.action_type == 0 || props.item?.action_type == 2 ? 'JOIN' : 'PREMIUM'  }}
                        </div>
                        <div class="text-button fs-24 ms-3">
                            {{ props.item?.action_type == 0 ? 'FREE' : props.item?.action_type == 1 ? props.item?.coins?.toLocaleString("en-US")  : `+ ${props.item?.coins?.toLocaleString("en-US")}` }}
                            <Image src="/coin.svg" v-if="props.item?.action_type != 0"></Image>
                        </div>
                    </div>
                </Button>
            </div>
        </div> -->
    </div>

</template>

<script setup>
import Badge from "@/components/Badge.vue"
import Image from "@/components/Image.vue"
import Ionicon from "@/components/Ionicon.vue";
import Button from "@/components/Button.vue"
import { computed } from "vue"
import { useDateFormat } from '@vueuse/core'

const props = defineProps({
    item: {
        type: Object
    },
});

const labels = computed(() => {
    return props.item.tags != "[]" ? props.item?.tags.replace(/\[|\]|\r|\n|\s+|"/g, '').split(',') : []
});

const eventDate = computed(() => {
    if(props.item?.date != undefined){
        let date = useDateFormat(props.item?.date, 'DD MMM YYYY');
        return date.value;
    }  
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