<template>
    <div class="card-goal-wrapper card shadow--sm radius-16">
        <div class="card-header bg-white radius-16 shadow--md">
            <div class="d-flex justify-content-between align-items-center mb-3">

                <!-- <div class="d-flex justify-content-end align-items-center">
                    <Badge class="goal-label me-2" :class="props.goal?.is_locked == false ? 'green' : 'grey'"
                    v-for="label in labels" v-if="labels.length > 0">
                    {{ label }}
                </Badge> -->
                <Badge class="goal-label me-2 invisible" :class="props.goal?.is_locked == false ? 'green' : 'grey'">
                    No Data
                </Badge>
                <Ionicon :icon_name="props.goal.is_published ? 'eye' : 'eye-off'"
                    :class="props.goal.is_published ? 'green-2' : 'danger'" font_size="20" />
            </div>
            <div class="card-goal-title text-sm-bold f-game ls-1 text-center"
                :class="{ 'text-disabled': props.goal?.is_locked }">{{
                    props.goal?.title
                }}</div>
        </div>
        <div class="card-body card-img p-0"
            :style="'background: url(https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/' + encodeURIComponent(props.goal?.logo.trim()) + ')'"
            :class="{ 'greyscale opacity-25': props.goal?.is_locked }">
        </div>
        <div
            class="card-footer bg-white radius-16 shadow--md cursor-pointer user-select-none d-flex align-items-center justify-content-center">
            <slot name="card-footer">
            </slot>
        </div>
        <!-- <div class="card-footer radius-16 shadow--md"
            :class="[props.goal.is_locked ? 'gray-bg-2' : 'bg-white', !props.goal.is_locked && props.goal.is_goal_complete && !props.goal.is_rewards_collected || !props.goal.is_locked && props.goal.is_goal_complete && props.goal.is_rewards_collected ? 'gold-bg' : '']">
            <div class="d-flex justify-content-between align-content-start card-goal-info"
                v-if="!props.goal?.is_locked && !props.goal?.is_goal_complete && !props.goal?.is_rewards_collected">
                <div
                    class="shadow-in--sm radius-16 d-flex align-items-center align-content-center card-goal-info py-1 px-3">
                    <StepsWithCircle number-text="0" height="40" width="40" text-color="text-white fs-12"
                        class="green-bg me-2" :class="[currentStep > 0 ? 'green-bg' : 'gray-bg']"
                        :current-step="currentStep" :total-step="props.goal?.steps.length" />
                    <div class="text-sm-bold fs-12">
                        Steps
                    </div>
                </div>
                <div class="text-sm-bold goal-reward">
                    <div class="f-game fs-24 text-warning text-reward">REWARD:</div>
                    <div class="text-warning fs-24">{{ props.goal?.total_points }}<Image class="ms-1" src="/coin.svg">
                        </Image>
                    </div>
                </div>
            </div>
            <div class="d-flex justify-content-center align-items-center card-goal-info" style="margin-top: 0.75rem;"
                v-else-if="!props.goal.is_locked && props.goal.is_goal_complete && !props.goal.is_rewards_collected">
                <div class="f-game fs-24 text-white text-reward me-3">COLLECT NOW:</div>
                <div class="text-white fs-24 text-sm-bold">{{ props.goal.total_points }}<Image class="ms-1" src="/coin.svg">
                    </Image>
                </div>
            </div>
            <div class="d-flex justify-content-center align-items-center card-goal-info" style="margin-top: 0.75rem;"
                v-else-if="!props.goal.is_locked && props.goal.is_goal_complete && props.goal.is_rewards_collected">
                <div class="f-game fs-24 text-white text-reward me-3">COLLECTED</div>
            </div>
            <div class="mx-auto d-table text-center" v-else>
                <Ionicon icon_name="lock-closed-outline" font_size="24" class="warning" />
                <div class="grey fs-14 text-white">Requires {{ getGoalCategoryName }}</div>
                <div class="f-game fs-24 grey lh-c-1 text-white">LVL {{ getGoalLevelName }}</div>
            </div>
        </div> -->
    </div>
</template>

<script setup>
import Badge from "@/components/Badge.vue"
import Image from "@/components/Image.vue"
import Ionicon from "@/components/Ionicon.vue";
import StepsWithCircle from "@/components/Goals/StepsWithCircle.vue";
import { computed } from "vue"
import Button from "@/components/Button.vue"

const props = defineProps({
    goal: {
        type: Object
    },
});

const labels = computed(() => {
    return props.goal.label != "[]" ? props.goal?.label.replace(/\[|\]|\r|\n|\s+|"/g, '').split(',') : []
})

const currentStep = computed(() => {
    const stepObj = props.goal?.steps.find(element => { return !element.is_step_complete });
    return props.goal?.steps.indexOf(stepObj);
});

const getGoalCategoryName = computed(() => {
    const Category = {
        1: 'TEAM',
        2: 'COMPANY',
        3: 'SERVICE',
        4: 'GROWTH'
    }
    return Category[props.goal?.id_category];
});

const getGoalLevelName = computed(() => {
    const Category = {
        1: '0',
        2: '1',
        3: '2',
        4: 'MAX'
    }
    return Category[props.goal?.code_level];
});

</script>

<style scoped>
.card-footer {
    height: 76px;
}

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