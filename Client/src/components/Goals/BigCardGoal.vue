<template>
    <div class="card-goal-wrapper card shadow--sm radius-16">
        <div class="card-header bg-white radius-16 shadow--md">
            <div class="row justify-content-between align-items-center">
                <div class="col-8">
                    <div class="d-flex justify-content-start align-items-center">
                        <Badge 
                        class="goal-label me-2" 
                        :class="props.goal.is_locked == false ? 'blue' : 'grey'"
                        v-for="label in labels"
                        >{{ label }} </Badge>
                    </div>
                </div>
                <div class="col-4">
                    <div class="d-flex justify-content-end align-items-center">
                        <Ionicon :icon_name="props.goal.is_published ? 'eye' : 'eye-off'" :class="props.goal.is_published ? 'green-2' : 'danger'" font_size="20" />
                        <TextLevelGoal :level-number="props.goal.code_level" class="ms-3"></TextLevelGoal>
                        <div class="category-logo ms-3">
                            <Image class="img-fluid of-contain" :src="props.goal.goal_category_logo" width="30" height="30">
                            </Image>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card-goal-title text-sm-bold f-game ls-1 text-center fs-24 my-3"
                :class="{ 'text-disabled': props.goal.is_locked }">{{
                props.goal.title
                }}</div>

            <div class="text-sm-bold fs-14 grey goal-description mx-auto">{{ props.goal.description }}</div>
        </div>
        
        <div class="card-body card-img p-5" :style="[ props.goal?.logo != null ? {'background-image': 'url(' + `https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${ encodeURIComponent(props.goal?.logo?.trim()) }` + ')'} : { 'background': 'linear-gradient(180deg,rgba(65, 96, 148, 0.15) 75%, #416094 100%)' }]"
            :class="{ 'greyscale opacity-25': props.goal.is_locked }"
            >
            <div class="bg-white p-3 body-content radius-16 shadow--md">
                <div class="text-sm-bold fs-14 mb-3">Goals Step</div>
                <Steps :step-list="props.goal.steps" />
            </div>
        </div>
        <div class="card-footer bg-white radius-16 shadow--sm p-3 cursor-pointer user-select-none">
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
import { computed, onMounted, ref } from "vue"

const props = defineProps({
    goal: {
        type: Object
    }
});

const labels = computed(() => {
    return props.goal.label != "[]" ? props.goal?.label.replace(/\[|\]|\r|\n|\s+|"/g,'').split(',') : []
})

const currentStep = computed(() => {
    return props.goal.steps.find(element => { return !element.is_step_complete }).step;
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
    max-height: 480px;
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
/* .card-goal-wrapper .card-body .body-content {
    max-width: 80%;
}  */
</style>