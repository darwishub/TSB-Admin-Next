<template>
    <form>

        <PageExplanation class="no-toggle" is-open :breadcrumbs="`${adminAuth?.name} ${route?.meta?.breadcrumb}`" :logo="adminAuth?.photo">
            <template v-slot:top-content>
                <div class="d-flex justify-content-between align-items-center">
                    <div class="d-flex justify-content-start align-items-center">
                        <span class="d-inline-block">
                            <Button class="grey rounded-md p-1 ps-3 pe-3 me-3" type="button" @click="onPreview">
                                <div class="d-flex justify-content-center align-items-center">
                                    <div class="f-game fs-24">Preview</div>
                                    <Ionicon icon_name="eye-outline" class="ms-1" font_size="20" />
                                </div>
                            </Button>
                        </span>

                    </div>
                    <div class="d-flex justify-content-end align-items-center">
                        <Button class="blue rounded-md p-1 ps-3 pe-2" type="button" @click="onPublish"
                            :disabled="buttonPublishStatus.disabled" :loading="buttonPublishStatus.loading">
                            <div class="d-flex justify-content-center align-items-center">
                                <div class="f-game fs-24">Publish</div>
                                <Ionicon icon_name="chevron-forward-outline" class="ms-1" font_size="24" />
                            </div>
                        </Button>

                    </div>

                </div>
            </template>
            <template v-slot:body-content>
                <div class="mt-3 mb-3">
                    <div class="row justify-content-between">
                        <div class="col-5">  
                            <BaseInputSwitch
                            name="exclusive"
                            label="Set as an Exclusive content"
                            class="mt-3"
                            ref="toggleRef" 
                            @onSwitchChange="selectExclusive"
                            />
                            <BaseSelect :options="statusList" name="content_type" label="Choose type of content"
                                placeholder="Select the content type" value-by="value" label-by="label" class="mb-3"
                                @onSelect="selectContentType" is-required
                                :value="statusList.find(x => x.value == itemDetail?.ContentType)?.value" />
                            <BaseInput v-if="(contentType == 0 || itemDetail?.ContentType == 0) || (contentType == 1 && exclusive == false) || (itemDetail?.ContentType == 1 && itemDetail?.Exclusive == false)" name="Url" label="URL" class="mb-3" placeholder="Enter the URL here"
                                is-required type-rule="url"/>
                            <BaseSelect :options="programGroupList" name="program_group_id" label="Program Group" class="mb-3 w-100"
                                v-if="programGroup.length > 0" value-by="programGroupId" label-by="groupName"
                                placeholder="Select a group" style="width: 300px;" @onSelect="onSelectProgramGroup"
                                :value="statusList.find(x => x.value == itemDetail?.ProgramGroupId)?.value" />
                        </div>
                        <div class="col-5">
                            <BaseInput name="title" label="Title" class="mb-3" placeholder="Enter the content' title here"
                                is-required />

                            <BaseTextarea name="description" label="Description" class="mb-3"
                                placeholder="Enter the content' description here" is-required />
                            <BaseTags label="Add Content' Tags" name="tags" placeholder="Press enter to add a tag"
                                class="mb-3" />

                        </div>
                    </div>
                </div>

            </template>
        </PageExplanation>

        <div class="goal-details-hero py-6 shadow--lg"
            :style="[imgUrl != null ? { 'background-image': 'url(' + imgUrl + ')' } : { 'background-color': '#c2c2c2' }]">
            <div class="container">
                <div class="position-relative">
                    <div class="d-flex align-items-center add-goal-text bg-white py-1 px-3 radius-8 shadow-out--sm"
                        @click="addGoalImage" id="goalimg">
                        <div class="flex-shrink-0">
                            <Image src="/img-upload.svg" width="24" height="24" />
                        </div>
                        <div class="flex-grow-1">
                            <div class="f-game fs-20 grey ms-3">{{ `${imgUrl != null ? 'Change' : 'Upload'} Picture Max 1 MB` }}
                            </div>
                            <div v-show="errorMessage || meta.valid" class="text-danger text-bold fs-14 ms-3">
                                {{ errorMessage }}
                            </div>
                        </div>
                    </div>
                    <input class="goal-img-input" type="file" @blur="handleBlur" name="goal_img"
                        accept="image/png, image/jpeg, image/jpg" @change="onGoalImageChange" ref="goalImageRef">
                    <input type="hidden" name="file_url">

                </div>

                <nav>
                    <div class="nav nav-tabs" id="nav-tab" role="tablist">
                        <button class="nav-link active" id="nav-home-tab" data-bs-toggle="tab" data-bs-target="#nav-home"
                            type="button" role="tab" aria-controls="nav-home" aria-selected="true">Settings</button>
                    </div>
                </nav>
                <div class="tab-content shadow-out--sm" id="nav-tabContent">
                    <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab"
                        tabindex="0">
                        <div class="row">
                            <div class="col-6">
                                
                                <BaseInput name="rewards" 
                                label="Amount of coins reward"
                                class="mb-3 w-100"
                                    placeholder="Enter amount of coins here"
                                    type-rule="coins" is-required />

                            </div>
                            <div class="col-6">
                                <BaseSelect :options="levelIdOptions" name="level_code_number" type-rule="number"
                                    placeholder="Select level" class="custom-select-option mb-3" label="Choose level"
                                    :value="selectLevel"
                                    @onSelect="onSelectLevel"
                                    >

                                    <template #selected="{ selected }">
                                        <template v-if="selected">
                                            <div class="d-flex justify-content-start align-items-center">
                                                <TextLevelGoal :levelNumber="selected" />
                                                <LevelBar class="ms-3" :levelNumber="selected" />
                                            </div>
                                        </template>
                                        <template v-else>
                                            <div class="fs-14 base-grey">Select a quest level</div>
                                        </template>
                                    </template>

                                    <template #option="{ option }">
                                        <div class="d-flex justify-content-start align-items-center">
                                            <TextLevelGoal :levelNumber="option" />
                                            <LevelBar class="ms-3" :levelNumber="option" />
                                        </div>
                                    </template>

                                </BaseSelect>

                                <BaseSelect :options="categoryOptions" name="level_id" placeholder="Select level"
                                    class="mb-3 custom-select-option" value-by="id" :empty-model-value="''"
                                    label="Choose category">

                                    <template #selected="{ selected }">
                                        <template v-if="selected">
                                            <div class="d-flex justify-content-start align-items-center">
                                                <Image class="of-contain" :src="selected.logo_link" width="96"
                                                    height="96" />
                                                <div class="fs-24 base-grey ms-3 f-game">{{ selected.name }}</div>
                                            </div>
                                        </template>
                                        <template v-else>
                                            <div class="fs-14 base-grey">Select a quest category</div>
                                        </template>
                                    </template>

                                    <template #option="{ option }">
                                        <div class="d-flex justify-content-start align-items-center">
                                            <Image class="of-contain" :src="option.logo_link" width="45" height="45" />
                                            <div class="fs-20 base-grey ms-3 f-game">{{ option.name }}</div>
                                        </div>
                                    </template>
                                </BaseSelect>
                            </div>
                        </div>
                    </div>
                </div>
                
            </div>
            
        </div>

        <div v-if="(exclusive == true) || (contentType == 1 && exclusive == true) || (itemDetail?.ContentType == 1 && itemDetail?.Exclusive == true)" class="container">
            <div class="shadow-out--sm radius-16 px-5 pb-5 pt-4 bg-white mb-6 text-editor">
                <p class="mb-3 f-game fs-24">Content Article</p>
                <QuillEditor v-model:content="textContent" theme="snow" name="content" toolbar="full"
                    placeholder="Compose detail content here..." contentType="html" @update:content="onContentUpdate" />
                <div class="text-danger text-bold fs-14 mt-1" v-show="contentErrorMessage">{{ contentErrorMessage }}</div>
            </div>
        </div>
    </form>
</template>

<script setup>
import PageExplanation from "@/components/PageExplanation.vue"
import Image from "@/components/Image.vue"
import LevelBar from "@/components/Goals/LevelBar.vue"
import TextLevelGoal from "@/components/Goals/TextLevelGoal.vue"
import Button from "@/components/Button.vue"
import Ionicon from "@/components/Ionicon.vue"
import { useAcademyStore } from "@/stores/academyStore"
import { adminAuthStore } from "@/stores/adminAuthStore"
import { storeToRefs } from 'pinia';
import { computed, onMounted, ref, reactive, markRaw, inject } from "vue"
import { watchDebounced, useDebounceFn } from '@vueuse/core'
import { useForm, useFieldArray, FieldArray, useField, Field } from 'vee-validate';
import { number, mixed, string } from "yup";
import { Motion, Presence } from "motion/vue"
import { useRouter, useRoute } from "vue-router";
import Swal from 'sweetalert2'
import { Tooltip } from "bootstrap/dist/js/bootstrap.esm.min.js";

import BaseInput from "@/components/Form/BaseInput.vue"
import BaseSelect from "@/components/Form/BaseSelect.vue"
import BaseDatePicker from "@/components/Form/BaseDatePicker.vue"
import BaseTextarea from "@/components/Form/BaseTextarea.vue"
import BaseTags from "@/components/Form/BaseTags.vue"
import BaseInputSwitch from "@/components/Form/BaseInputSwitch.vue"
import { QuillEditor } from '@vueup/vue-quill'
import '@vueup/vue-quill/dist/vue-quill.snow.css';


const axios = inject("axios");
const router = useRouter();
const route = useRoute();
const { adminAuth, programGroup } = storeToRefs(adminAuthStore());
const { academyEntryPreview, itemDetail } = storeToRefs(useAcademyStore());
const { fetchUserOnboardingData, SubmitAcademyEntry, GetAcademyItem, SubmitEditAcademyEntry } = useAcademyStore();
const { fetchProgramGroup } = adminAuthStore();
const { handleSubmit, setFieldValue, values, errors, resetForm, validate: validateForm, meta: metaForm } = useForm();
const { currentProgramId, selectProgramRef } = inject('programSelection')
const imgUrl = ref(null);
const goalImageRef = ref();
const programGroupId = ref(0);
const levelIdOptions = [2, 4];
const programGroupList = computed(() => {
    return [
        {
            programGroupId: 0,
            groupName: "No Group"
        },
        ...programGroup.value
    ];
});
const statusList = [
    {
        label: 'Video',
        value: 0
    },
    {
        label: 'Article',
        value: 1,
    },
]

const categoryIdOptions = {
    // 1 : [
    //         {
    //             id: 1,
    //             logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/personel-icon-0.png",
    //             name: "Team",
    //             id_goal_category: 1
    //         },
    //         {
    //             id: 5,
    //             logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/company-icon-0.png",
    //             name: "Company",
    //             id_goal_category: 2
    //         },
    //         {
    //             id: 9,
    //             logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/concept-icon-0.png",
    //             name: "Service",
    //             id_goal_category: 3
    //         },
    //         {
    //             id: 13,
    //             logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/growth-icon-0.png",
    //             name: "Growth",
    //             id_goal_category: 4
    //         },
    //     ],
    2: [
        {
            id: 2,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/personel-icon-1.png",
            name: "Team",
            id_goal_category: 1
        },
        {
            id: 6,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/company-icon-1.png",
            name: "Company",
            id_goal_category: 2
        },
        {
            id: 10,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/concept-icon-1.png",
            name: "Service",
            id_goal_category: 3
        },
        {
            id: 14,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/growth-icon-1.png",
            name: "Growth",
            id_goal_category: 4
        },

    ],
    3: [
        {
            id: 3,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/personel-icon-2.png",
            name: "Team",
            id_goal_category: 1
        },
        {
            id: 7,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/company-icon-2.png",
            name: "Company",
            id_goal_category: 2
        },
        {
            id: 11,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/concept-icon-2.png",
            name: "Service",
            id_goal_category: 3
        },
        {
            id: 15,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/growth-icon-2.png",
            name: "Growth",
            id_goal_category: 4
        },
    ],
    4: [
        {
            id: 4,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/personel-icon-3.png",
            name: "Team",
            id_goal_category: 1
        },
        {
            id: 8,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/company-icon-3.png",
            name: "Company",
            id_goal_category: 2
        },
        {
            id: 12,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/concept-icon-3.png",
            name: "Service",
            id_goal_category: 3
        },
        {
            id: 16,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/growth-icon-3.png",
            name: "Growth",
            id_goal_category: 4
        },
    ]
};

const categoryOptions = ref(categoryIdOptions[2])

const FILE_SIZE = 1000 * 1000;
const SUPPORTED_FORMATS = [
    "image/jpg",
    "image/jpeg",
    "image/png"
];

const { value: fileValue, meta, errorMessage, handleChange, resetField, validate, errors: goalImgErrors, setValue } = useField('goal_img',
    mixed().required("An Image file is required").test('FILE_SIZE', "Image file is too big, Max 1MB.", (value) => {

        return (
            typeof value != 'string' ?
                (goalImageRef.value?.files[0]?.size <= FILE_SIZE ? true : false)
                : true)
    }).test('FILE_TYPE', "Image file has unsupported format.", (value) => {
        return (
            typeof value != 'string' ?
                (SUPPORTED_FORMATS.includes(goalImageRef.value?.files[0]?.type) ? true : false)
                : true)
    })
);

const { setValue: setFileName, handleChange: changeFileName } = useField('file_url');
const textContent = ref();
const { setValue: setContent, handleChange: onContentChange, errorMessage: contentErrorMessage } = useField('content', string().nullable(), {
    initialValue: textContent.value
});
const onContentUpdate = useDebounceFn((val) => {
    onContentChange(val)
}, 1000)
const onGoalImageChange = async (e) => {
    const file = e.target?.files[0];
    setValue(file);
    await validate();

    if (goalImgErrors.value.length == 0) {
        let generatedFile = URL.createObjectURL(file);
        changeFileName(generatedFile);
        imgUrl.value = generatedFile;
        handleChange(file.name);
    } else {
        imgUrl.value = null;
        changeFileName(null);
    }
};

const addGoalImage = () => {
    goalImageRef.value.click();
}

const onPreview = async () => {
    let route = router.resolve({ path: "/preview-academy-entry" });
    window.open(route.href);
};
const actionType = ref(0);
const contentType = ref();
const eventType = ref();
const applyTo = ref();
const exclusive = ref(false);

const selectActionType = (val) => {
    if(val == 0){
        setFieldValue("price", 0);
    }
    actionType.value = val;
}
const selectContentType = (val) => {
    contentType.value = val;
}
const selectExclusive = (val) => {
    exclusive.value = val;
}
const selectApplyTo = (val) => {
    applyTo.value = val;
}
const buttonPublishStatus = reactive({
    loading: false,
    disabled: false
});
const onPublish = handleSubmit(async values => {
    let formData = new FormData();
    if (goalImageRef.value?.files[0] !== undefined) {
        formData.append('file', goalImageRef.value?.files[0], goalImageRef.value?.files[0]?.name)
    }
    formData.append('data', JSON.stringify(values));
    formData.append('programId', adminAuth.value.programId);
    formData.append('item_id', route.params.id);

    buttonPublishStatus.loading = true;
    buttonPublishStatus.disabled = true;
    SubmitEditAcademyEntry(formData).then(() => {
        return router.push("/academy");
    }).finally(() => {
        buttonPublishStatus.loading = false;
        buttonPublishStatus.disabled = false;
    }).catch(() => {
        buttonPublishStatus.loading = false;
        buttonPublishStatus.disabled = false;
    });
    
}, ({ errors }) => {
    console.log(errors)
});

const onSelectLevel = (id) => {
    selectLevel.value = id;
    categoryOptions.value = categoryIdOptions[id];
    setFieldValue('level_id', "");
}

watchDebounced(
  () => adminAuth.value.programId, 
  async (newValue, oldValue) => {
    await fetchProgramGroup(adminAuth.value.programId);
  },
  0
);

const onSelectProgramGroup = async (id) => {
    adminAuth.value.programGroupId = id;
}

const selectLevel = ref(1);
onMounted(async () => {

    if (route.query.programid != undefined && route.query.programid > 0 && adminAuth.value.role == "Configuration Access") {
        adminAuth.value.programId = Number(route.query.programid);
        selectProgramRef.value?.handleChange(Number(route.query.programid));
    }

    await GetAcademyItem(route.params?.id);
    resetForm({
        values: {
            content_type: itemDetail.value?.ContentType,
            title: itemDetail.value?.Title,
            description: itemDetail.value?.Description,
            tags: itemDetail.value?.Tag != "[]" ? itemDetail.value?.Tag?.replace(/\[|\]|\r|\n|\s+|"/g, '').split(',') : [],
            rewards: itemDetail.value?.Rewards,
            file_url: `https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${encodeURIComponent(itemDetail.value?.Logo.trim())}`,
            Url: itemDetail.value?.Url,
            date: itemDetail.value?.DateCreated,
            goal_img: itemDetail.value?.Logo != "" ? itemDetail.value?.Logo : null,
            content: itemDetail.value?.ContentArticle,
            exclusive: itemDetail.value?.Exclusive,
            program_group_id: itemDetail.value?.ProgramGroupId,
        }
    });
   
    if(itemDetail.value?.IdCategory != 1){
        setFieldValue("level_id", itemDetail.value?.IdCategory);
        categoryOptions.value = categoryIdOptions[itemDetail.value?.IdLevel];
    }

    if( itemDetail.value?.IdLevel != 1){
        setFieldValue("level_code_number", itemDetail.value?.IdLevel);
        selectLevel.value = itemDetail.value?.IdLevel;
    }

    imgUrl.value = itemDetail.value?.Logo != "" ? `https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${encodeURIComponent(itemDetail.value?.Logo.trim())}` : null;
    textContent.value = itemDetail.value?.ContentArticle;
    
    academyEntryPreview.value = values;

    watchDebounced(
        values,
        () => {
            academyEntryPreview.value = values;
        },
        {
            debounce: 1000, maxWait: 5000
        },
    );
    await fetchProgramGroup(adminAuth.value.programId);
});

</script>

<style scoped>
.goal-details-hero {
    background-repeat: no-repeat;
    background-attachment: scroll;
    background-position: center;
    background-size: cover;
    background-attachment: scroll;
    height: auto;
    position: relative;
    max-height: 500px;
}

.goal-tasks-wrapper {
    background-repeat: no-repeat;
    background-attachment: scroll;
    background-position: center;
    height: auto;
    min-height: 500px;
    padding: 4rem;
    background-size: cover;
}

.field-menu-control {
    position: absolute;
    right: 0;
}

.sticky-step-left {
    top: 144px;
    z-index: 9;
}

.goal-img-input {
    position: absolute;
    right: 0;
    display: none;
}

.add-goal-text {
    position: absolute;
    right: 0;
    cursor: pointer;
    user-select: none;
}

.text-editor {
    margin-top: -2rem;
    position: relative;
}
</style>