<template>
    <div class="preview-goal">
        <PageExplanation :logo="adminAuth?.photo" :breadcrumbs="`${adminAuth?.name} ${route?.meta?.breadcrumb}`">
            <template v-slot:top-content>
                <div class="d-flex justify-content-between">
                    <div class="d-flex justify-content-start align-items-center">
                        <Ionicon icon_name="eye-outline" class="me-1 disabled" font_size="20" />
                        <div class="text-sm-bold text-grey fs-14">Quest Preview</div>
                    </div>
                    <div class="d-flex justify-content-end align-items-center">

                        <Button class="blue rounded-md p-1 ps-3 pe-2 ms-3 invisible" type="button">
                            <div class="d-flex justify-content-center align-items-center">
                                <div class="f-game fs-24">Publish</div>
                                <Ionicon icon_name="chevron-forward-outline" class="ms-1" font_size="24" />
                            </div>
                        </Button>

                    </div>
                </div>
            </template>
            <template v-slot:body-content>
                <div class="mt-3 mb-5">
                    <div class="stepper-wrapper align-items-start">
                        <div class="stepper-item" v-for="(step, index) in preview_goal?.steps" :key="step">

                            <div class="f-game fs-24 lh-1 mb-3 invisible">Reward</div>
                            <div class="px-3 py-2 mb-2">
                                <div @click="viewStep(index)">
                                    <NumberTextCircle :number-text="index" height="48" width="48" text-color="text-sm-bold"
                                        class="step-counter cursor-pointer" :class="[
                                            currentStepRef == index ? 'green-bg text-white' : 'gray-bg-2 text-white',
                                            // step.is_step_complete && currentStepRef != index ? 'orange-bg-light-2 border-orange text-warning' : ''
                                        ]" />
                                </div>
                            </div>
                            <div class="fs-12 text-sm-bold mb-2 text-center step-title-sm">{{ step.step_title }}</div>
                            <div class="fs-12 text-sm-bold grey">Step {{
                                index
                            }}/{{ preview_goal?.steps.length - 1 }}</div>

                        </div>

                        <div class="stepper-item" v-if="steps.length < 7">
                            <div class="f-game fs-24 lh-1 mb-3 text-warning">Reward</div>
                            <div
                                class="goal-reward-wrapper gray-bg-2 radius-8 shadow--sm py-2 px-3 step-counter cursor-pointer user-select-none">
                                <div class="d-flex align-items-center">
                                    <div class="flex-shrink-0">
                                        <Image src="/reward.png" class="rounded-lg" height="48" width="48" />
                                    </div>
                                    <div class="flex-grow-1 ms-3">
                                        <div class="f-game fs-24 lh-1 text-white">Earn
                                            <Image src="/coin-2.svg" />
                                        </div>
                                        <div class="text-sm-bold fs-14 text-white">{{ preview_goal?.goal_reward ?? 0 }} TSB
                                            coins</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="stepper-item" v-else>
                            <div class="f-game fs-24 lh-1 mb-3 text-warning">Reward</div>
                            <div
                                class="goal-reward-wrapper gray-bg-2 radius-8 shadow--sm py-2 px-3 step-counter cursor-pointer user-select-none">
                                <div class="d-flex flex-column align-items-center">
                                    <div class="">
                                        <Image src="/reward.png" class="rounded-lg" height="48" width="48" />
                                    </div>
                                    <div class="">
                                        <div class="f-game fs-24 lh-1 text-white text-center">Earn
                                            <Image src="/coin-2.svg" />
                                        </div>
                                        <div class="text-sm-bold fs-14 text-white text-center">{{ preview_goal?.goal_reward
                                        }} TSB
                                            coins</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </template>
        </PageExplanation>

        <div class="goal-details-hero py-6 shadow--lg mb-6"
            :style="[checkFileUrl != '' ? { 'background-image': 'url(' + preview_goal?.file_url + ')' } : { 'background-color': '#c2c2c2' }]">
            <div class="container">
                <nav>
                    <div class="nav nav-tabs" id="nav-tab" role="tablist">
                        <button class="nav-link active" id="nav-home-tab" data-bs-toggle="tab" data-bs-target="#nav-home"
                            type="button" role="tab" aria-controls="nav-home" aria-selected="true">Quest Card</button>
                        <button class="nav-link" id="nav-profile-tab" data-bs-toggle="tab" data-bs-target="#nav-profile"
                            type="button" role="tab" aria-controls="nav-profile" aria-selected="false">Tips + Help
                            <Image src="/alien.svg" class="of-contain" height="32" width="32" />
                        </button>
                    </div>
                </nav>
                <div class="tab-content shadow--sm" id="nav-tabContent">
                    <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab"
                        tabindex="0">
                        <div class="row">
                            <div class="col-9">
                                <div class="text-green f-game fs-24 mb-3 lh-1">{{ preview_goal?.goal_title }}</div>
                                <div class="text-warning fs-12 mb-3 text-sm-bold">{{ preview_goal?.goal_subtitle }}</div>
                                <div class="d-flex align-items-center">
                                    <TextLevelGoal
                                        :level-number="preview_goal?.level_code_number == '' ? 1 : preview_goal?.level_code_number"
                                        class="me-3" />
                                    <LevelBar
                                        :level-number="preview_goal?.level_code_number == '' ? 1 : preview_goal?.level_code_number" />
                                </div>
                                <div class="row justify-content-center align-items-center mb-5">
                                    <div class="col-4">
                                        <Image :src="categoryUrl" width="160" height="160"
                                            class="of-contain mx-auto d-table" 
                                            :class="{ 'greyscale' : preview_goal?.level_code_number == 1 }"
                                            />
                                    </div>
                                    <div class="col-8">
                                        <div class="fs-12 text-sm-bold grey">{{ preview_goal?.goal_description }}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="text-warning f-game fs-24 mb-3 lh-1">Reward:</div>
                                <div class="d-flex justify-content-start align-items-center mb-3">
                                    <Image src="/reward.png" class="rounded-lg" width="80" height="80" />
                                    <div class="text-sm-bold text-warning fs-24 ms-2">{{ preview_goal?.goal_reward }}
                                        <Image src="/coin-2.svg" />
                                    </div>
                                </div>
                                <Badge class="mb-3 goal-label green me-3" v-if="preview_goal?.goal_label?.length > 0"
                                    v-for="label in preview_goal?.goal_label">{{ label }}</Badge>
                            </div>
                        </div>
                        <div class="row justify-content-center">
                            <div class="col-4">
                                <div @click="viewCurrentStep">
                                    <Step :step="currentStepRef" :title="currentStepData?.step_title"
                                        :current-step="currentStepRef" :total-step="preview_goal?.steps.length - 1" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab"
                        tabindex="0">
                        <div class="text-sm-bold grey">{{ preview_goal?.goal_help_text }}</div>
                    </div>
                </div>
            </div>
        </div>

        <div class="container" id="form-section">
            <div class="mb-6">
                <div class="fs-24 text-sm-bold mb-3">{{ currentStepData?.step_title }} <span class="grey">{{
                    currentStepData?.step
                }}/{{ preview_goal?.steps.length - 1 }}</span></div>
                <div class="fs-14 grey text-sm-bold">{{ currentStepData?.step_description }}</div>
            </div>
            <div class="row mb-8 gx-5">
                <div class="col-4">
                    <div class="bg-white p-3 body-content radius-16 shadow-out--sm sticky-step-left sticky-top">
                        <div class="text-sm-bold fs-14 mb-3">Quests Step</div>
                        <div class="d-flex align-items-center py-2 px-3 cursor-pointer user-select-none" :class="[index < steps.length ? 'mb-3' : '',
                        activeStepIndex == index && !step.value.is_step_complete || currentStepRef == index ? 'current' : '',
                            // step.value.is_step_complete ? 'complete' : ''
                        ]" v-for="(step, index) in steps" :key="step.key" @click="viewStep(index)">
                            <div class="flex-shrink-0">
                                <div class="step-number text-sm-bold shadow--lg">{{ index }}</div>
                            </div>
                            <div class="flex-grow-1 ms-3">
                                <div class="me-auto">
                                    <div class="text-sm-bold fs-14 step-title">{{ step.value.step_title }}</div>
                                    <span class="text-sm-bold grey fs-14 step-info">Step
                                        {{
                                            index
                                        }}/{{
    steps.length - 1
}}</span>
                                </div>
                            </div>
                            <div class="flex-grow-2">
                                <Ionicon icon_name="chevron-forward" class="grey-light-1" font_size="32" />
                            </div>
                        </div>

                        <div class="d-flex align-items-center py-2 px-3 cursor-pointer user-select-none mt-3">
                            <div class="flex-shrink-0">
                                <Image src="/reward.png" class="rounded-lg" width="48" height="48" />
                            </div>
                            <div class="flex-grow-1 ms-3">
                                <div class="me-auto">
                                    <div class="text-sm-bold fs-14 text-warning">Reward</div>
                                    <span class="text-sm-bold fs-14 text-warning">{{ preview_goal?.goal_reward }}</span>
                                </div>
                            </div>
                            <div class="flex-grow-2">
                                <Image src="/coin-2.svg" height="24" width="24"></Image>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-8">
                    <div class="goal-tasks-wrapper radius-16 shadow-out--sm"
                        :style="[ checkFileUrl != '' ? { 'background-image': 'url(' + preview_goal?.file_url + ')' } : { 'background-color': '#c2c2c2' }]">
                        <Form @submit.prevents="onSubmit">

                            <div v-for="(step, index_step) in preview_goal?.steps" :key="step">
                                <div v-for="(card, index_card) in step.cards" :key="card">
                                    <GoalCardWrapper v-bind="{
                                        title: card.title,
                                        label: card.schema.props.label,
                                        description: card.description,
                                        id: index_card,
                                        'has-option': card.has_option,
                                        option: card.option,
                                        'display-order': card.display_order
                                    }" :class="step.step > 0 ? '' : 'show-description'" class="mb-6"
                                        v-if="currentStepRef == index_step">

                                        <template v-if="card?.schema?.field_code == 4">
                                            <label class="form-label f-game fs-20 base-grey">{{ card?.schema?.props?.label }}<span class="text-danger" v-if="card?.schema?.props?.required">*</span></label>
                                            <component v-for="(option, index) in card?.schema?.option" :key="option"
                                                :is="dynamicFields[card?.schema?.field_code]" v-bind="{
                                                    name: `field_${step.step}_${index_card}`,
                                                    type: card?.schema?.props?.type,
                                                    'type-rule': card?.schema?.props?.type_rule,
                                                    label: card?.schema?.props?.label,
                                                    placeholder: card?.schema?.props?.placeholder,
                                                    'is-required': card?.schema?.props?.required,
                                                    'is-disabled': card?.schema?.props?.disabled,
                                                    options: card?.schema?.option,
                                                    'max-data': card?.schema?.props?.max_data,
                                                    'is-multiple': card?.schema?.props?.allow_multiple,
                                                    'is-value-visible': true,
                                                    value: option,
                                                    id: index,
                                                }" />
                                        </template>
                                        <template v-else>
                                            <component :is="dynamicFields[card?.schema?.field_code]" v-bind="{
                                                name: `field_${step.step}_${index_card}`,
                                                type: card?.schema?.props?.type,
                                                'type-rule': card?.schema?.props?.type_rule,
                                                label: card?.schema?.props?.label,
                                                placeholder: card?.schema?.props?.placeholder,
                                                'is-required': card?.schema?.props?.required,
                                                'is-disabled': card?.schema?.props?.disabled,
                                                options: card?.schema?.option,
                                                'max-data': card?.schema?.props?.max_data,
                                                'is-multiple': card?.schema?.props?.allow_multiple,
                                                'is-value-visible': true,
                                                'files-type': card?.schema?.props?.files_type,
                                                'text-button': card?.schema?.props?.text_button
                                            }" />
                                        </template>

                                    </GoalCardWrapper>
                                </div>
                            </div>
                            <!-- <Button class="green mx-auto d-table rounded-sm" disabled>Submit</Button> -->
                        </Form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>

import PageExplanation from "@/components/PageExplanation.vue"
import NumberTextCircle from "@/components/Goals/NumberWithCircle.vue"
import Image from "@/components/Image.vue"
import LevelBar from "@/components/Goals/LevelBar.vue"
import TextLevelGoal from "@/components/Goals/TextLevelGoal.vue"
import Button from "@/components/Button.vue"
import Badge from "@/components/Badge.vue"
import Steps from "@/components/Goals/Steps.vue"
import { useGoalsStore } from "@/stores/goalsStore"
import { storeToRefs } from 'pinia';
import { useRoute } from "vue-router"
import { computed, onMounted, ref, reactive, watchEffect } from "vue"
import "swiper/css";
import "swiper/css/scrollbar";
import { Scrollbar } from "swiper";
import Testimony from "@/components/Testimony.vue"
import { Swiper, SwiperSlide } from "swiper/vue";
import Accordion from "@/components/Accordion.vue";
import Goal from "@/components/Goals/Goal.vue"
import Step from "@/components/Goals/Step.vue"
import { Form } from 'vee-validate';
import Ionicon from "@/components/Ionicon.vue"
import { adminAuthStore } from "@/stores/adminAuthStore"

//======= Dynamic form fields
import GoalCardWrapper from "@/components/Goals/GoalCardWrapper.vue"
import StepZero from "@/components/Goals/StepZero.vue"
import BaseInput from "@/components/Form/BaseInput.vue"
import BaseSelect from "@/components/Form/BaseSelect.vue"
import BaseDatePicker from "@/components/Form/BaseDatePicker.vue"
import BaseTextarea from "@/components/Form/BaseTextarea.vue"
import BaseInputSwitch from "@/components/Form/BaseInputSwitch.vue"
import BaseInputCheckbox from "@/components/Form/BaseInputCheckbox.vue"
import BasePickSlider from "@/components/Form/BasePickSlider.vue"
import BaseFileUploader from "@/components/Form/BaseFileUploader.vue"
import BaseTextareaAutosize from "@/components/Form/BaseTextareaAutosize.vue"
import BaseInputPhone from "@/components/Form/BaseInputPhone.vue"
import BaseSelectCountries from "@/components/Form/BaseSelectCountries.vue"

import { useForm, useFieldArray, FieldArray, useField } from 'vee-validate';

const { adminAuth, programGroup } = storeToRefs(adminAuthStore());

const dynamicFields = {
    0: StepZero,
    2: BaseInput,
    3: BaseSelect,
    4: BaseInputCheckbox,
    5: BaseInputSwitch,
    6: BaseDatePicker,
    7: BasePickSlider,
    8: BaseFileUploader,
    9: BaseTextareaAutosize,
    10: BaseTextarea,
    11: BaseInputPhone,
    12: BaseSelectCountries
}

const onSubmit = (values) => {
}

const route = useRoute();
const { goal, goals, preview_goal, img_preview } = storeToRefs(useGoalsStore());
const { fetchGoal, fetchGoals } = useGoalsStore();

const levelCodeNumber = computed(() => {
    return preview_goal.value?.level_code_number != undefined ? preview_goal.value?.level_code_number != "" ? preview_goal.value?.level_code_number : 2 : 2;
});

const levelId = computed(() => {
    return preview_goal.value?.level_id != undefined ? preview_goal.value?.level_id != "" ? preview_goal.value?.level_id : 2 : 2;
});

const categoryUrl = computed(() => {
    return categoryIdOptions[levelCodeNumber.value][levelId.value]?.logo_link != undefined ? categoryIdOptions[levelCodeNumber.value][levelId.value]?.logo_link : 'https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/personel-icon-1.png';
});

const { handleSubmit, setFieldValue, values, errors, resetForm } = useForm();

let { remove: stepRemove, update: stepUpdate, push: stepPush, fields: steps, replace: stepReplace } = useFieldArray('steps');

onMounted(() => {
    let storageData = JSON.parse(window.localStorage.getItem('goals'));
    preview_goal.value = storageData.preview_goal;
    stepReplace(storageData.preview_goal.steps);
    currentStepRef.value = currentStep.value;
});

// watchEffect(() => {

//     let storageData = JSON.parse(window.localStorage.getItem('goals'));
//     preview_goal.value = storageData.preview_goal;
//     stepReplace(storageData.preview_goal.steps);
//     currentStepRef.value = currentStep.value;

//     window.addEventListener('storage', () => {

//         storageData = JSON.parse(window.localStorage.getItem('goals'));
//         preview_goal.value = storageData.preview_goal;
//         stepReplace(storageData.preview_goal.steps);

//     });
// },{
//     flush: 'post'
// });

let goalData = reactive({
    total_points: 0
});

const currentStep = computed(() => {
    return steps.value.length - 1;
});

const currentStepData = computed(() => {
    return steps.value[currentStepRef.value]?.value;
});

const activeStep = computed(() => {
    return steps.value.find((element, index) => {
        if (index != currentStepRef.value) {
            return !element.value.is_step_complete;
        } else {
            return steps.value[index];
        }
    });
});

const activeStepIndex = computed(() => {
    return steps.value.indexOf(activeStep.value);
});

const currentStepRef = ref(currentStep.value);

const viewStep = (stepIndex) => {
    currentStepRef.value = stepIndex;
    document.getElementById('form-section').scrollIntoView();
}
const viewCurrentStep = () => {
    document.getElementById('form-section').scrollIntoView();
}
const modules = [Scrollbar];

const checkFileUrl = computed(() => {
    let words = preview_goal.value?.file_url;
    let n = words.split("/");
    let result = n[n.length - 1];
    return result;
})

const categoryIdOptions = {
    1: {
        1: {
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/personel-icon-1.png",
        }
    },
    2: {
        2: {
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/personel-icon-1.png",
        },
        6: {
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/company-icon-1.png",
        },
        10: {
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/concept-icon-1.png",
        },
        14: {
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/growth-icon-1.png",
        },
    },
    3: {
        3: {
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/personel-icon-2.png",
        },
        7: {
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/company-icon-2.png",
        },
        11: {
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/concept-icon-2.png",
        },
        15: {
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/growth-icon-2.png",
        },
    },
    4: {
        4: {
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/personel-icon-3.png",
        },
        8: {
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/company-icon-3.png",
        },
        12: {
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/concept-icon-3.png",
        },
        16: {
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/growth-icon-3.png",
        },
    }
};

</script>
<style scoped>
.horizontal-step-wrapper ul {
    list-style: none;
}

.stepper-wrapper {
    margin-top: auto;
    display: -webkit-box;
    display: -webkit-flex;
    display: -ms-flexbox;
    display: flex;
    -webkit-box-pack: justify;
    -webkit-justify-content: space-between;
    -ms-flex-pack: justify;
    justify-content: space-between;
}

.stepper-item {
    position: relative;
    display: -webkit-box;
    display: -webkit-flex;
    display: -ms-flexbox;
    display: flex;
    -webkit-box-orient: vertical;
    -webkit-box-direction: normal;
    -webkit-flex-direction: column;
    -ms-flex-direction: column;
    flex-direction: column;
    -webkit-box-align: center;
    -webkit-align-items: center;
    -ms-flex-align: center;
    align-items: center;
    -webkit-box-flex: 1;
    -webkit-flex: 1;
    -ms-flex: 1;
    flex: 1;
}

@media (max-width: 768px) {
    .stepper-item {
        font-size: 12px;
    }
}

.stepper-item::before {
    position: absolute;
    content: "";
    width: 100%;
    top: 4.5rem;
    left: -50%;
    z-index: 2;
    height: 5px;
    margin-bottom: 1rem;
    overflow: hidden;
    font-size: .75rem;
    font-weight: 600;
    -webkit-box-shadow: inset 2px 2px 5px #b8b9be, inset -3px -3px 7px #fff;
    box-shadow: inset 2px 2px 5px #b8b9be, inset -3px -3px 7px #fff;
    background-color: #fafbfe;
    -webkit-border-radius: 100px;
    border-radius: 100px;
}

.step-counter {
    position: relative;
    z-index: 5;
}

.stepper-item.completed::after {
    position: absolute;
    content: "";
    width: 100%;
    top: 4.5rem;
    height: 5px;
    left: 50%;
    z-index: 3;
    background: -o-linear-gradient(132.3deg, rgba(144, 88, 51, 0.4) 0%, rgba(255, 255, 255, 0.4) 105.18%), #FFE3B0;
    background: linear-gradient(317.7deg, rgba(144, 88, 51, 0.4) 0%, rgba(255, 255, 255, 0.4) 105.18%), #FFE3B0;
    background-blend-mode: soft-light, normal;
    border: 1px solid rgba(255, 255, 255, 0.4);
    -webkit-box-shadow: inset -2.5px -2.5px 5px rgb(250 251 255 / 10%), inset 2.5px 2.5px 5px #905833;
    box-shadow: inset -2.5px -2.5px 5px rgb(250 251 255 / 10%), inset 2.5px 2.5px 5px #905833;
    -webkit-border-radius: 50px;
    border-radius: 50px;
}

.stepper-item:first-child::before {
    content: none;
}

.stepper-item:last-child::after {
    content: none;
}

.goal-details-hero {
    background-repeat: no-repeat;
    background-attachment: scroll;
    background-position: center;
    background-size: cover;
    background-attachment: scroll;
    /* height: 100vh;
    max-height: 637px; */
    position: relative;
}

.goal-tasks-wrapper {
    background-repeat: no-repeat;
    background-attachment: scroll;
    background-position: center;
    height: 100%;
    min-height: 500px;
    padding: 4rem;
    background-size: cover;
}

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
    background: #0F934C;
    box-shadow: 0 4px 8px #0f934c59;
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

.step-title-sm {
    max-height: 48px;
    line-height: 1rem;
    display: block;
    max-width: 100%;
    overflow: hidden;
    text-overflow: ellipsis;
    word-break: break-all;
}

.current .step-info {
    color: #FFE3B0;
}

.swiper {
    width: 100%;
    height: 100%;
    -webkit-user-select: none;
    -moz-user-select: none;
    -ms-user-select: none;
    user-select: none;
}

.swiper-slide {
    width: auto;
}

.swiper-overflow-container {
    overflow-x: hidden;
}

.swiper-overflow-container .container {
    overflow: visible;
}

.swiper-overflow-container .swiper-container {
    overflow: visible;
}

.testimony-wrapper {
    width: 100%;
    max-width: 354px;
}

.current ion-icon {
    color: #FFE3B0;
}

.sticky-step-left {
    top: 144px;
    z-index: 9;
}
</style>