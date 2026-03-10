<template>
    <div class="preview-goal">
        <PageExplanation class="goals-topbar" :breadcrumbs="`${adminAuth?.name} ${route?.meta?.breadcrumb}`" :logo="adminAuth?.photo" >
            <template v-slot:body-content>
        <div class="row justify-content-between align-items-start content-inside user-select-none py-3">
            <div class="col-5">
                <div class="d-flex align-content-start align-items-center">
                    <Image src="/alien.svg" height="80" width="56" />
                    <div>
                        <div class="shadow-out--sm bg-white ms-3 p-3 radius-16 custom-dialog-box">
                            <span class="fs-14 grey">
                                <div class="fs-24 f-game text-green text-center">Welcome To The Startup Buddy Next</div>
                                Well done, you already took the first step to your journey. I am TSB NEXT Buddy assistant. Call me ”Buddy” for short.
Let’s get started with your set-up. Click on the big button on my right to start. 
                            </span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-3">
                <div>
                        <div class="fs-24 text-md text-center text-warning f-game mb-4">Let's Get Started</div>

                            <div 
                                class="btn-add-layer-2"
                                >
                                <div class="d-table mx-auto">
                                    <BaseCircleButton class="btn-content blue btn-circle-shadow" 
                                        @click="onStart"
                                        width="133" height="133">
                                        <div class="d-flex flex-column justify-content-center align-items-center">
                                            <Ionicon icon_name="add-outline" font_size="24" />
                                            <div class="fs-18 f-game">Start set up</div>
                                        </div>
                                    </BaseCircleButton>
                                </div>
                            </div>

                    </div>
            </div>
        </div>
    </template>
    </PageExplanation>

    <div class="container mt-6" id="form-section">
            <div class="row mb-8 gx-5">
                <div class="col-4">
                    <div class="bg-white p-3 body-content radius-16 shadow-out--sm sticky-step-left sticky-top">
                        <div class="text-sm-bold fs-14 mb-3">Quests Step</div>
                        <div 
                            class="d-flex align-items-center py-2 px-3 cursor-pointer user-select-none"
                            :class="[index < onboardingObj.steps.length ? 'mb-3' : '', 
                            currentStepRef == index && !userOnboarding?.steps[index].is_step_complete || currentStepRef == index ? 'current' : '', 
                            userOnboarding?.steps[index].is_step_complete && currentStepRef != index ? 'complete' : '',
                            ]"
                            v-for="(step, index) in onboardingObj.steps" :key="step.key"
                            @click="viewStep(index)"
                            >
        
                            <div class="flex-shrink-0">
                                <div class="step-number text-sm-bold shadow--lg">{{ index }}</div>
                            </div>
                            <div class="flex-grow-1 ms-3">
                                <div class="me-auto">
                                    <div class="text-sm-bold fs-14 step-title">{{ step.step_title }}</div>
                                    <span class="text-sm-bold grey fs-14 step-info">Step
                                    {{
                                    index
                                    }}/{{
                                        onboardingObj.steps.length - 1
                                    }}</span>
                                </div>
                            </div>
                            <div class="flex-grow-2">
                                <Ionicon icon_name="chevron-forward" class="grey-light-1" font_size="32" />
                            </div>
                        </div>

                        <div class="d-flex align-items-center py-2 px-3 cursor-pointer user-select-none mt-3 radius-16"
                            v-on="isAllStepsCompleted ? { click: onClickReward } : {}"
                            :class="{ 'gold-bg': isAllStepsCompleted && currentStepRef < 0 }">
                            <div class="flex-shrink-0">
                                <Image src="/reward.png" class="rounded-lg" width="48" height="48" />
                            </div>
                            <div class="flex-grow-1 ms-3">
                                <div class="me-auto">
                                    <div class="text-sm-bold fs-14"
                                        :class="isAllStepsCompleted && currentStepRef < 0 ? 'text-white ' : 'text-warning'">
                                        Reward</div>
                                    <span class="text-sm-bold fs-14"
                                        :class="isAllStepsCompleted && currentStepRef < 0 ? 'text-white ' : 'text-warning'">{{
                                            onboardingObj.goal_reward }}</span>
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
                    :style="[ onboardingObj.file_url != null ? {'background-image': 'url(' + onboardingObj.file_url + ')'} : {'background-color': '#c2c2c2'}]"
                    >
                    
                        <form>

                            <div v-for="(step, index_step) in onboardingObj?.steps" :key="step">
                                <div v-for="(card, index_card) in step.cards" :key="card">
                             
                                    <GoalCardWrapper
                                        v-bind="{
                                            title : card.title,
                                            label : card.schema.props.label,
                                            description : card.description,
                                            id : index_card,
                                            'has-option' : card.has_option,
                                            option : card.option,
                                            'display-order' : card.display_order
                                        }"
                                        :class="[step.step > 0 ? '' : 'show-description', card.schema.field_code == 0 && index_step > 0 ? 'd-none' : '']"
                                        class="mb-6"
                                        v-if="currentStepRef == index_step"
                                    >

                                    <BaseInput :name="`steps[${index_step}].id_goal_step`" type="hidden" :value="index_step" class="d-none" />
                                    
                                        <template v-if="card?.schema?.field_code == 4">
                                            <component 
                                                v-for="(option, index) in card?.schema?.option" :key="option"
                                                :is="dynamicFields[card?.schema?.field_code]" 
                                                v-bind="{
                                                name: `steps[${index_step}].cards[${index_card}].value`,
                                                type : card?.schema?.props?.type,
                                                'type-rule' : card?.schema?.props?.type_rule,
                                                label : card?.schema?.props?.label,
                                                placeholder : card?.schema?.props?.placeholder,
                                                'is-required' : card?.schema?.props?.required,
                                                'is-disabled' : card?.schema?.props?.disabled,
                                                options: card?.schema?.option,
                                                'max-data' : card?.schema?.props?.max_data,
                                                'is-multiple' : card?.schema?.props?.allow_multiple,
                                                'is-value-visible' : true,
                                                value: option,
                                                id: index,
                                                'hide-selected': false
                                                }" 
                                            />
                                        </template>
                                        <template v-else>
                                            <iframe v-if="step.step == 795" width="625" height="315" src="https://www.youtube.com/embed/-RRq5uSdxLM" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>
                                            <component 
                                                :is="dynamicFields[card?.schema?.field_code]" 
                                                v-bind="{
                                                name: `steps[${index_step}].cards[${index_card}].value`,
                                                type : card?.schema?.props?.type,
                                                'type-rule' : card?.schema?.props?.type_rule,
                                                label : card?.schema?.props?.label,
                                                placeholder : card?.schema?.props?.placeholder,
                                                'is-required' : card?.schema?.props?.required,
                                                'is-disabled' : card?.schema?.props?.disabled,
                                                options: card?.schema?.option,
                                                'max-data' : card?.schema?.props?.max_data,
                                                'is-multiple' : card?.schema?.props?.allow_multiple,
                                                'is-value-visible' : true,
                                                'files-type': card?.schema?.props?.files_type,
                                                'text-button' : card?.schema?.props?.text_button,
                                                'hide-selected': false,
                                                'value' : userOnboarding?.steps[index_step]?.cards[index_card]?.value
                                                }" 
                                                :ref="(el) => {
                                                    if(card?.schema?.field_code == 8){
                                                        filesRef = el;
                                                    }
                                                }"
                                            />
 
                                        </template>

                                    </GoalCardWrapper>
                                </div>

                            </div>

                            <Button type="button" class="green mx-auto d-table rounded-md"
                                v-if="!userOnboarding?.steps[0].is_step_complete && activeStepIndex == 0 || activeStepIndex > 0"
                                @click="onSubmit" :loading="buttonSubmitStatus.loading"
                                :disabled="buttonSubmitStatus.disabled">
                                <div class="d-flex justify-content-center align-items-center">
                                    <div class="f-game fs-24">Submit</div>
                                    <Ionicon icon_name="chevron-forward-outline" class="ms-3" font_size="24" />
                                </div>
                            </Button>
                            
                        </form>

                        <div class="card-wrapper radius-16 bg-white p-5 shadow-in--sm"
                            v-if="isAllStepsCompleted && currentStepRef < 0">
                            <div class="d-flex align-items-center justify-content-center">
                                <div class="text-center">
                                    <div class="f-game fs-32 text-md text-warning mb-3">Congratulations!!
                                        <div class="f-game fs-32 text-md text-warning">you have completed your
                                            profile!!</div>
                                    </div>
                                    <Image src="/reward.png" class="rounded-lg mb-3" width="64" height="64" />
                                    <div class="d-flex align-items-center justify-content-center mb-3">
                                        <span class="text-sm-bold fs-32 text-warning me-3 lh-c-1">{{
                                            onboardingObj.goal_reward }}</span>
                                        <Image src="/coin-2.svg" height="40" width="40"></Image>
                                    </div>


                                    <Button @click="goToDashboard" :loading="buttonSubmitStatus.loading"
                                :disabled="buttonSubmitStatus.disabled" type="button" class="blue mx-auto d-table rounded-md">
                                        <div class="d-flex justify-content-center align-items-center">
                                            <div class="f-game fs-24">Go to Dashboard</div>
                                        </div>
                                    </Button>


                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>


    </div>
</template>

<script setup>

import NumberTextCircle from "@/components/Goals/NumberWithCircle.vue"
import Image from "@/components/Image.vue"
import LevelBar from "@/components/Goals/LevelBar.vue"
import TextLevelGoal from "@/components/Goals/TextLevelGoal.vue"
import Button from "@/components/Button.vue"
import Badge from "@/components/Badge.vue"
import Steps from "@/components/Goals/Steps.vue"
import { storeToRefs } from 'pinia';
import { useRoute, useRouter } from "vue-router"
import { computed, onMounted, ref, reactive, markRaw, defineAsyncComponent, toRaw } from "vue"
import "swiper/css";
import "swiper/css/scrollbar";
import { Scrollbar } from "swiper";
import Testimony from "@/components/Testimony.vue"
import { Swiper, SwiperSlide } from "swiper/vue";
import Accordion from "@/components/Accordion.vue";
import Goal from "@/components/Goals/Goal.vue"
import Step from "@/components/Goals/Step.vue"
import Ionicon from "@/components/Ionicon.vue"
import BaseCircleButton from "@/components/Button/BaseCircleButton.vue"
import PageExplanation from "@/components/PageExplanation.vue"
import { adminAuthStore } from "@/stores/adminAuthStore"

//======= Dynamic form fields
import GoalCardWrapper from "@/components/Goals/GoalCardWrapper.vue"
import BaseInput from "@/components/Form/BaseInput.vue"
import BaseSelect from "@/components/Form/BaseSelect.vue"
import BaseDatePicker from "@/components/Form/BaseDatePicker.vue"
import BaseTextarea from "@/components/Form/BaseTextarea.vue"
import BaseInputSwitch from "@/components/Form/BaseInputSwitch.vue"
import BaseInputCheckbox from "@/components/Form/BaseInputCheckbox.vue"
import BasePickSlider from "@/components/Form/BasePickSlider.vue"
import BaseFileUploader from "@/components/Form/BaseFileUploader.vue"
import BaseTextareaAutosize from "@/components/Form/BaseTextareaAutosize.vue"
import BaseInputPhone from '@/components/Form/BaseInputPhone.vue'
import BaseSelectCountries from '@/components/Form/BaseSelectCountries.vue'

import { useForm, useFieldArray, FieldArray, useField } from 'vee-validate';

import { useCompanyGoalsStore } from "@/stores/companyGoalsStore"
const { adminAuth } = storeToRefs(adminAuthStore());
const { fetchUsersData } = adminAuthStore();

const { userOnboarding } = storeToRefs(useCompanyGoalsStore());
const { fetchUserOnboardingData, submitOnboarding, finishOnboarding } = useCompanyGoalsStore();
const router = useRouter();
// const dynamicFields = {
//     2 : defineAsyncComponent(() => import('@/components/Form/BaseInput.vue')),
//     3 : defineAsyncComponent(() => import('@/components/Form/BaseSelect.vue')),
//     4 : defineAsyncComponent(() => import('@/components/Form/BaseInputCheckbox.vue')),
//     5 : defineAsyncComponent(() => import('@/components/Form/BaseInputSwitch.vue')),
//     6 : defineAsyncComponent(() => import('@/components/Form/BaseDatePicker.vue')),
//     7 : defineAsyncComponent(() => import('@/components/Form/BasePickSlider.vue')),
//     8 : defineAsyncComponent(() => import('@/components/Form/BaseFileUploader.vue')),
//     9 : defineAsyncComponent(() => import('@/components/Form/BaseTextareaAutosize.vue')),
//     10: defineAsyncComponent(() => import('@/components/Form/BaseTextarea.vue')),
//     11: defineAsyncComponent(() => import('@/components/Form/BaseInputPhone.vue')),
//     12: defineAsyncComponent(() => import('@/components/Form/BaseSelectCountries.vue'))
// }

const dynamicFields = {
    2: markRaw(BaseInput),
    3: markRaw(BaseSelect),
    4: markRaw(BaseInputCheckbox),
    5: markRaw(BaseInputSwitch),
    6: markRaw(BaseDatePicker),
    7: markRaw(BasePickSlider),
    8: markRaw(BaseFileUploader),
    9: markRaw(BaseTextareaAutosize),
    10: markRaw(BaseTextarea),
    11: markRaw(BaseInputPhone),
    12: markRaw(BaseSelectCountries),
}

const { handleSubmit, setFieldValue, values, errors, resetForm } = useForm();

const buttonSubmitStatus = reactive({
    loading: false,
    success: false,
    fail: false,
    disabled: false
});

const onStart = () => {
    document.getElementById('form-section').scrollIntoView();
}

const filesRef = ref();

const onSubmit = handleSubmit(async values => {

    let formData = new FormData();

    formData.append('data', JSON.stringify(values?.steps[currentStepRef.value]));

    if (filesRef.value != null) {

        formData.append('file[]', toRaw(filesRef.value.FilesObj[0]));
    }

    buttonSubmitStatus.loading = true;
    buttonSubmitStatus.disabled = true;

submitOnboarding(formData).then(() => {

        fetchUserOnboardingData().then(() => {
            fetchUsersData();
            buttonSubmitStatus.loading = false;
            buttonSubmitStatus.disabled = false;

        })
            .finally(() => {

                if (!isAllStepsCompleted.value) {
                    currentStepRef.value += 1;
                } else {
                    currentStepRef.value = -1;
                }

            });
    });

    // document.getElementById('form-section')?.scrollIntoView();

}, ({ errors }) => {

    buttonSubmitStatus.loading = false;
    buttonSubmitStatus.disabled = false;
    const fieldName = Object.keys(errors)[0];
    const idField = String(fieldName).replace(/[^0-9a-z]/gi, '');
    document.getElementById(idField)?.scrollIntoView();
    document.getElementById(idField)?.focus();

});

const goToDashboard = async () => {


    buttonSubmitStatus.loading = true;
    buttonSubmitStatus.disabled = true;

    await finishOnboarding().then(() => {
        buttonSubmitStatus.loading = false;
        buttonSubmitStatus.disabled = false;
        fetchUsersData().then(() => {
            return router.push("/home");
        });
        
    }).finally(() => {
        buttonSubmitStatus.loading = false;
        buttonSubmitStatus.disabled = false;
    });

}

const route = useRoute();

const preview_goal = ref();

const levelCodeNumber = computed(() => {
    return preview_goal.value?.level_code_number != undefined ? preview_goal.value?.level_code_number != "" ? preview_goal.value?.level_code_number : 2 : 2;
});

const onClickReward = () => {
    currentStepRef.value = -1;
    document.getElementById('form-section')?.scrollIntoView();
}

const levelId = computed(() => {
    return preview_goal.value?.level_id != undefined ? preview_goal.value?.level_id != "" ? preview_goal.value?.level_id : 2 : 2;
});

const categoryUrl = computed(() => {
    return categoryIdOptions[levelCodeNumber.value][levelId.value]?.logo_link != undefined ? categoryIdOptions[levelCodeNumber.value][levelId.value]?.logo_link : 'https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/personel-icon-1.png';
});

const isAllStepsCompleted = computed(() => {
     return userOnboarding?.value?.steps.map(x => x.is_step_complete).every(element => element === true);
});

let { remove: stepRemove, update: stepUpdate, push: stepPush, fields: steps, replace : stepReplace } = useFieldArray('steps');

onMounted(async () => {
    await fetchUserOnboardingData();
    // preview_goal.value = onboardingObj;
    // stepReplace(onboardingObj.steps);
    resetForm();
    for(let x = 0; x < onboardingObj.steps?.length; x++){

        setFieldValue(`steps[${x}].is_step_complete`, userOnboarding.value?.steps[x]?.is_step_complete);
        let cardsCount = onboardingObj.steps[x]?.cards.length;

        for(let i = 0; i < cardsCount; i++){

            setFieldValue(`steps[${x}].cards[${i}].id`, onboardingObj.steps[x]?.cards[i]?.id);
            setFieldValue(`steps[${x}].cards[${i}].field_code`, onboardingObj.steps[x]?.cards[i]?.schema?.field_code);
            // setFieldValue(`steps[${x}].cards[${i}].value`, userOnboarding.value?.steps[x]?.cards[i]?.value);

        }

    }
    currentStepRef.value = activeStepIndex.value;
});

const onboardingObj = {
    "level_code_number": 2,
    "level_id": 2,
    "goal_title": "fdv",
    "goal_subtitle": "dfvfd",
    "goal_description": "vfdvfd",
    "goal_help_text": "fdvdfv",
    "goal_reward": 100,
    "goal_label": [],
    "goal_img": "cf6a9wp6630190jpg",
    "goal_id": "401",
    "promo_goal": false,
    "file_url": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/cf6a9wp6630190jpg",
    "steps": [
        {
            "step": 795,
            "id_goal_step": 0,
            "step_title": "Introduction Video",
            "step_description": "",
            "is_step_complete": false,
            "cards": [
                {
                    "id": 664,
                    "help_text": "",
                    "title": "Welcome to The Startup Buddy",
                    "description": "This is an explanatory video about the platform you are using. Watch this explanatory video to get an easier experience using this platform.",
                    "schema": {
                        "field_code": 0,
                        "field_code_type": 0,
                        "props": {
                            "placeholder": "",
                            "text_button": "",
                            "type": "",
                            "type_rule": "",
                            "label": "",
                            "value": "",
                            "required": true,
                            "disabled": false,
                            "max_data": 0,
                            "allow_multiple": false,
                            "files_type": []
                        },
                        "has_option": false,
                        "option": [],
                        "display_order": 0,
                        "success_message": ""
                    }
                }
            ]
        },
        {
            "step": 803,
            "id_goal_step": 0,
            "step_title": "Personal Information",
            "step_description": "",
            "is_step_complete": false,
            "cards": [
                {
                    "id": 676,
                    "help_text": "",
                    "title": "",
                    "description": "",
                    "schema": {
                        "field_code": 2,
                        "field_code_type": 0,
                        "props": {
                            "placeholder": "First Name",
                            "text_button": "",
                            "type": "",
                            "type_rule": "text",
                            "label": "First Name",
                            "value": "",
                            "required": true,
                            "disabled": false,
                            "max_data": 0,
                            "allow_multiple": false,
                            "files_type": []
                        },
                        "has_option": false,
                        "option": [],
                        "display_order": 0,
                        "success_message": ""
                    }
                },
                {
                    "id": 676,
                    "help_text": "",
                    "title": "",
                    "description": "",
                    "schema": {
                        "field_code": 2,
                        "field_code_type": 0,
                        "props": {
                            "placeholder": "Last Name",
                            "text_button": "",
                            "type": "",
                            "type_rule": "text",
                            "label": "Last Name",
                            "value": "",
                            "required": true,
                            "disabled": false,
                            "max_data": 0,
                            "allow_multiple": false,
                            "files_type": []
                        },
                        "has_option": false,
                        "option": [],
                        "display_order": 0,
                        "success_message": ""
                    }
                },
                {
                    "id": 677,
                    "help_text": "",
                    "title": "",
                    "description": "",
                    "schema": {
                        "field_code": 2,
                        "field_code_type": 0,
                        "props": {
                            "placeholder": "LinkedIn",
                            "text_button": "",
                            "type": "",
                            "type_rule": "linkedin",
                            "label": "LinkedIn",
                            "value": "",
                            "required": true,
                            "disabled": false,
                            "max_data": 0,
                            "allow_multiple": false,
                            "files_type": []
                        },
                        "has_option": false,
                        "option": [],
                        "display_order": 0,
                        "success_message": ""
                    }
                },
                
            ]
        },
        {
            "step": 805,
            "id_goal_step": 0,
            "step_title": "Personal Information",
            "step_description": "",
            "is_step_complete": false,
            "cards": [
            {
                    "id": 678,
                    "help_text": "",
                    "title": "",
                    "description": "",
                    "schema": {
                        "field_code": 10,
                        "field_code_type": 0,
                        "props": {
                            "placeholder": "Tell us about your self",
                            "text_button": "",
                            "type": "",
                            "type_rule": "",
                            "label": "Introduce yourself",
                            "value": "",
                            "required": true,
                            "disabled": false,
                            "max_data": 0,
                            "allow_multiple": false,
                            "files_type": []
                        },
                        "has_option": false,
                        "option": [],
                        "display_order": 0,
                        "success_message": ""
                    }
                },
                {
                    "id": 698,
                    "help_text": "",
                    "title": "",
                    "description": "",
                    "schema": {
                        "field_code": 8,
                        "field_code_type": 0,
                        "props": {
                            "label": "Profile Image",
                            "placeholder": "Profile Image",
                            "text_button": "Upload a profile image here",
                            "max_data": 1,
                            "allow_multiple": false,
                            "files_type": [
                                "image/png",
                                "image/jpeg"
                            ],
                            "required": true
                        }
                    }
                }
            ]
        }
        
    ]
};

let goalData = reactive({
    total_points : 0
});

const currentStep = computed(() => {
    return onboardingObj.steps.length - 1;
});

const currentStepData = computed(() => {
    return onboardingObj.steps[currentStepRef.value];
});

const activeStep = computed(() => {
    return userOnboarding?.value?.steps.find((element, index) => {
        if (index != currentStepRef.value) {
            return !element.is_step_complete;
        } else {
            return userOnboarding?.value.steps[index];
        }
    });
});

const activeStepIndex = computed(() => {
    return userOnboarding?.value?.steps.indexOf(activeStep.value);
});

const currentStepRef = ref();

const viewStep = (stepIndex) => {
    currentStepRef.value = stepIndex;
    document.getElementById('form-section').scrollIntoView();
}
const viewCurrentStep = () => {
    document.getElementById('form-section').scrollIntoView();
}
const modules = [Scrollbar];

const testimonies = [
    {
        "id_review": 1,
        "content_review": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sed eleifend tellus. Sed nec condimentum sem. Pellentesque volutpat accumsan auctor.",
        "reviewer": "Imroatul Faizah",
        "reviewer_company": "The Startup Buddy",
        "logo_company": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg"
    },
    {
        "id_review": 2,
        "content_review": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sed eleifend tellus. Sed nec condimentum sem. Pellentesque volutpat accumsan auctor.",
        "reviewer": "Robin",
        "reviewer_company": "The Startup Buddy",
        "logo_company": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg"
    },
    {
        "id_review": 3,
        "content_review": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sed eleifend tellus. Sed nec condimentum sem. Pellentesque volutpat accumsan auctor.",
        "reviewer": "Darwis Arifin",
        "reviewer_company": "The Startup Buddy",
        "logo_company": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg"
    },
    {
        "id_review": 3,
        "content_review": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sed eleifend tellus. Sed nec condimentum sem. Pellentesque volutpat accumsan auctor.",
        "reviewer": "Darwis Arifin",
        "reviewer_company": "The Startup Buddy",
        "logo_company": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg"
    },
    {
        "id_review": 3,
        "content_review": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sed eleifend tellus. Sed nec condimentum sem. Pellentesque volutpat accumsan auctor.",
        "reviewer": "Darwis Arifin",
        "reviewer_company": "The Startup Buddy",
        "logo_company": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg"
    },
    {
        "id_review": 3,
        "content_review": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sed eleifend tellus. Sed nec condimentum sem. Pellentesque volutpat accumsan auctor.",
        "reviewer": "Darwis Arifin",
        "reviewer_company": "The Startup Buddy",
        "logo_company": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg"
    },
    {
        "id_review": 3,
        "content_review": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sed eleifend tellus. Sed nec condimentum sem. Pellentesque volutpat accumsan auctor.",
        "reviewer": "Darwis Arifin",
        "reviewer_company": "The Startup Buddy",
        "logo_company": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg"
    },
    {
        "id_review": 3,
        "content_review": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sed eleifend tellus. Sed nec condimentum sem. Pellentesque volutpat accumsan auctor.",
        "reviewer": "Darwis Arifin",
        "reviewer_company": "The Startup Buddy",
        "logo_company": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg"
    }
]

const accordionObj = [
    {
        'title': 'What is a quest?',
        'content': 'This is the first item\'s accordion body. It is shown by default, until the collapse plugin adds the appropriate classes that we use to style each element. These classes control the overall appearance, as well as the showing and hiding via CSS transitions. You can modify any of this with custom CSS or overriding our default variables. It\'s also worth noting that just about any HTML can go within the .accordion-body, though the transition does limit overflow.'
    },
    {
        'title': 'How to claim this benefit?',
        'content': 'This is the first item\'s accordion body. It is shown by default, until the collapse plugin adds the appropriate classes that we use to style each element. These classes control the overall appearance, as well as the showing and hiding via CSS transitions. You can modify any of this with custom CSS or overriding our default variables. It\'s also worth noting that just about any HTML can go within the .accordion-body, though the transition does limit overflow.'
    },
    {
        'title': 'Am i handsome?',
        'content': 'This is the first item\'s accordion body. It is shown by default, until the collapse plugin adds the appropriate classes that we use to style each element. These classes control the overall appearance, as well as the showing and hiding via CSS transitions. You can modify any of this with custom CSS or overriding our default variables. It\'s also worth noting that just about any HTML can go within the .accordion-body, though the transition does limit overflow.'
    },
    {
        'title': 'Who am i?',
        'content': 'This is the first item\'s accordion body. It is shown by default, until the collapse plugin adds the appropriate classes that we use to style each element. These classes control the overall appearance, as well as the showing and hiding via CSS transitions. You can modify any of this with custom CSS or overriding our default variables. It\'s also worth noting that just about any HTML can go within the .accordion-body, though the transition does limit overflow.'
    }
];

const categoryIdOptions = {
    2 : {
            2: {
                logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/personel-icon-1.png",
            },
            6 : {
                logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/company-icon-1.png",
            },
            10 : {
                logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/concept-icon-1.png",
            },
            14 : {
                logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/growth-icon-1.png",
            },
    },
    3 : {
            3: {
                logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/personel-icon-2.png",
            },
            7 : {
                logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/company-icon-2.png",
            },
            11 : {
                logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/concept-icon-2.png",
            },
            15 : {
                logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/growth-icon-2.png",
            },
    },
    4 :  {
            4 : {
                logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/personel-icon-3.png",
            },
            8 : {
                logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/company-icon-3.png",
            },
            12 : {
                logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/concept-icon-3.png",
            },
            16 : {
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
    top: 84px;
    z-index: 9;
}
</style>