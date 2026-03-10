<template>
    <div class="d-flex align-items-center py-2 px-3 cursor-pointer user-select-none current">
        <div class="flex-shrink-0">
            <div class="step-number text-sm-bold shadow--lg"><div class="step-number-text">{{ props.step }}</div></div>
        </div>
        <div class="flex-grow-1 ms-3">
            <div class="me-auto">
                <div class="text-sm-bold fs-14 step-title">{{ props.title }}</div>
                <span class="text-sm-bold grey fs-14 step-info">Step
                    {{ props.currentStep }}/{{ props.totalStep }}</span>
            </div>
        </div>
        <div class="flex-grow-2">
            <Ionicon icon_name="chevron-forward" class="grey-light-1" font_size="32" />
        </div>
    </div>
</template>

<script setup>
import Ionicon from "@/components/Ionicon.vue"

const props = defineProps({
    step: Number,
    title: String,
    currentStep: Number,
    totalStep: Number
});
</script>

<style scoped>
.step-number {
    border-radius: 50%;
    width: 48px;
    height: 48px;
    padding: 12px;
    text-align: center;
    background: #c2c2c2;
    border: 1px solid #c2c2c2;
    color: #fff;
}
.current {
    background: #0F934C;
    box-shadow: 0 4px 8px rgb(15 147 76 / 35%);
    border-radius: 1rem;
}

.current .step-number {
    background: #fff;
    border: 1px solid #DFAE55;
    color: #C69030;
    font-weight: 900;
}

.current .step-title {
    color: #fff;
}

.current .step-info {
    color: #FFE3B0;
}
.current ion-icon {
    color: #FFE3B0;
}
.step-title {
    max-height: 24px;
    line-height: 1.5rem;
    display: block;
    max-width: 100%;
    overflow: hidden;
    text-overflow: ellipsis;
    word-break: break-all;
}
</style>