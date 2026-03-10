<template>
    <template v-if="stepListLength <= 4">
        <div class="d-flex align-items-center py-2 px-3 cursor-pointer user-select-none mb-3"
            v-for="(step,index) in stepList" :key="index"
            :class="[ step.step < stepListLength ? 'mb-3' : '', firstIncompleteStep.step == step.step && !step.is_step_complete ? 'current' : '', step.is_step_complete ? 'complete' : '']"
            >
            <div class="flex-shrink-0">
                <div class="step-number text-sm-bold shadow--lg"><div class="step-number-text">{{ index }}</div></div>
            </div>
            <div class="flex-grow-1 ms-3">
                <div class="me-auto">
                    <div class="text-sm-bold fs-14 step-title">{{
                    step.step_title
                    }}</div>
                    <span class="text-sm-bold grey fs-14 step-info">Step
                        {{
                        index
                        }}/{{
                            stepListLength - 1
                        }}</span>
                </div>
            </div>
            <div class="flex-grow-2">
                <Ionicon icon_name="chevron-forward" class="grey-light-1" font_size="32" />
            </div>
        </div>
    </template>
    <template v-else-if="stepListLength > 4 && remainingStep.length < 1">
        <div class="d-flex align-items-center py-2 px-3 cursor-pointer user-select-none mb-3"
            v-for="(step,index) in getLast4Step" :key="index"
            :class="[ firstIncompleteStepFromLast4.step == step.step && !step.is_step_complete ? 'current' : '', step.is_step_complete ? 'complete' : '']">
            <div class="flex-shrink-0">
                <div class="step-number text-sm-bold shadow--lg">{{ index }}</div>
            </div>
            <div class="flex-grow-1 ms-3">
                <div class="me-auto">
                    <div class="text-sm-bold fs-14 step-title">{{
                    step.step_title
                    }}</div>
                    <span class="text-sm-bold grey fs-14 step-info">Step
                        {{
                        index
                        }}/{{
                            stepListLength - 1
                        }}</span>
                </div>
            </div>
            <div class="flex-grow-2">
                <Ionicon icon_name="chevron-forward" class="grey-light-1" font_size="32" />
            </div>
        </div>
    </template>
    <template v-else>
        <div class="d-flex align-items-center py-2 px-3 cursor-pointer user-select-none mb-3"
            v-for="(step,index) in showStepToCard" :key="index"
            :class="[ firstIncompleteStepFromShowStepToCard.step == step.step && !step.is_step_complete ? 'current' : '', step.is_step_complete ? 'complete' : '']">
            <div class="flex-shrink-0">
                <div class="step-number text-sm-bold shadow--lg">{{ index }}</div>
            </div>
            <div class="flex-grow-1 ms-3">
                <div class="me-auto">
                    <div class="text-sm-bold fs-14 step-title">{{
                    step.step_title
                    }}</div>
                    <span class="text-sm-bold grey fs-14 step-info">Step
                        {{
                        index
                        }}/{{
                            stepListLength - 1
                        }}</span>
                </div>
            </div>
            <div class="flex-grow-2">
                <Ionicon icon_name="chevron-forward" class="grey-light-1" font_size="32" />
            </div>
        </div>

        <div class="d-flex align-items-center py-2 px-3 cursor-pointer user-select-none mb-3">
            <div class="flex-shrink-0">
                <div class="step-number text-sm-bold shadow--lg">+{{ remainingStep.length }}</div>
            </div>
            <div class="flex-grow-1 ms-3">
                <div class="me-auto">
                    <div class="text-sm-bold fs-14 step-title">+{{
                    remainingStep.length
                    }} More Steps</div>
                    <span class="text-sm-bold grey fs-14 step-info">Go to the goal page.</span>
                </div>
            </div>
            <div class="flex-grow-2">
                <Ionicon icon_name="chevron-forward" class="grey-light-1" font_size="32" />
            </div>
        </div>
    </template>
</template>


<script setup>
import { computed } from "vue";
import Ionicon from "@/components/Ionicon.vue";

const props = defineProps({
    stepList: {
        type: Array
    },
});

//total steps in a goal
const stepListLength = computed(() => props.stepList != null ? props.stepList.length : 0);
//get first incomplete step
const firstIncompleteStep = computed(() => props.stepList.find((element, index) => { 
        if(index != props.stepList.length - 1) {
            return !element.is_step_complete;
        }else{
            return props.stepList[index]
        }
    }));
//get first incomplete step index
const firstIncompleteStepIndex = computed(() => props.stepList.indexOf(firstIncompleteStep.value))
//show 3 step in card
const showStepToCard = computed(() => props.stepList.slice(firstIncompleteStepIndex.value, firstIncompleteStepIndex.value + 3));
//get first incomplete step from 3 step that shows in the card
const firstIncompleteStepFromShowStepToCard = computed(() => showStepToCard.value.find((element, index) => { 
        if(index != showStepToCard.value.length - 1) {
            return !element.is_step_complete;
        }else{
            return showStepToCard.value[index]
        }
    }));
// check remaining step after the first 3 show to the card
const remainingStep = computed(() => {
    let showStepToCardLastStep = showStepToCard.value[showStepToCard.value.length - 1];
    let showStepToCardLastStepIndex = props.stepList.indexOf(showStepToCardLastStep);  
    return props.stepList.slice(showStepToCardLastStepIndex + 1, stepListLength.value)
});
// if there is no remaining step, get last 4 step
const getLast4Step = computed(() => props.stepList.slice(Math.max(props.stepList.length - 4, 1)));
//get first incomplete step from the last 4 step
const firstIncompleteStepFromLast4 = computed(() => getLast4Step.value.find((element, index) => { 
        if(index != getLast4Step.value.length - 1) {
            return !element.is_step_complete;
        }else{
            return getLast4Step.value[index]
        }
    }));
// const checkIncompleteGoalsExist = computed(() => props.stepList.indexOf(firstIncompleteStep.value));
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

.complete .step-number {
    background: #FFE3B0;
    border: 1px solid #C69030;
    color: #C69030;
}

.current {
    background: #416094;
    box-shadow: 0px 4px 8px rgb(65 96 148 / 35%);
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
.step-title {
    max-height: 24px;
    line-height: 1.5rem;
    display: block;
    max-width: 100%;
    overflow: hidden;
    text-overflow: ellipsis;
    word-break: break-all;
}
.current .step-info {
    color: #FFE3B0;
}
.current ion-icon {
    color: #FFE3B0;
}
</style>