<template>
    <PageExplanation class="goals-topbar" is-open
    :breadcrumbs="`${adminAuth?.name} ${route?.meta?.breadcrumb}`" :logo="adminAuth?.photo"
    >
        <template v-slot:body-content>
            <div class="row justify-content-between align-items-center content-inside user-select-none py-3">
                <div class="col-2">
                    <Image
                        src="https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/ribbon-outline.png" />
                </div>
                <div class="col-6">
                    <div class="d-flex align-content-center align-items-center">
                        <Image src="/alien.svg" height="80" width="56" />
                        <div>
                            <div class="shadow-out--sm bg-white ms-3 p-3 radius-16 custom-dialog-box">
                                <span class="fs-16 text-sm-bold text-green">Hello {{ adminAuth.name
                                    }},</span><br /><br />
                                <span class="fs-14 grey">Let’s create a Quest together! Startups need to complete all the
                                    steps that you will assign to the quest in order to receive the reward. Make it
                                    lucrative!</span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-2">
                    <div>
                        <div class="fs-24 text-md text-center text-warning f-game mb-4">Create a Quest</div>

                        <div class="btn-add-layer-2" @click="createGoal">
                            <div class="btn-add-layer-1 d-table mx-auto">
                                <BaseCircleButton class="btn-content blue btn-circle-shadow" :class="{
                                    'div-disabled': isButtonClicked,
                                    'button--loading': isButtonClicked
                                }" width="86" height="86">
                                    <div class="d-flex flex-column justify-content-center align-items-center">
                                        <Ionicon icon_name="add-outline" font_size="24" />
                                        <div class="fs-18 f-game">Create</div>
                                    </div>
                                </BaseCircleButton>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </template>
    </PageExplanation>
    <div class="container main-wrapper my-6">

        <div class="mb-6">
            <div class="fs-24 text-sm-bold mb-3">Quests you’ve already created</div>
            <div class="fs-14 grey text-sm-bold">Look up or edit existing quests based on their Categories and Levels.
            </div>
        </div>

        <div class="mb-6">
            <Form class="row align-items-center">
                <div class="col-1">
                    <BaseSelect :options="[12, 24]" name="show_data" label="Show :" :value="12" ref="showEntries"
                        @onSelect="onSelectShowEntries" />
                </div>
                <div class="col-10">
                    <div class="d-flex justify-content-start align-items-center gap-3">
                        <BaseSelect :options="programGroupList" name="program_group_id" label="Program Group" class=""
                            v-if="programGroup.length > 0" value-by="programGroupId" label-by="groupName"
                            placeholder="Select a group" style="width: 300px;" @onSelect="onSelectProgramGroup"
                            :value="programGroupId" ref="programGroupRef" />
                        <BaseInput name="search_input" label="Search"
                            placeholder="Search quests by title and description" type="text" ref="searchInput"
                            @input="onInputSearch" class="w-100" />
                    </div>
                </div>
                <div class="col-1">
                    <div class="btn-group">
                        <Button type="button" class="blue btn-sm rounded-md outline no-edge-radius-md"
                            data-bs-toggle="dropdown" data-bs-auto-close="outside" aria-expanded="false">
                            <Ionicon icon_name="ellipsis-vertical-outline" font_size="24" />
                        </Button>
                        <ul class="dropdown-menu dropdown-menu-lg-end">
                            <li>
                                <BaseInputSwitch name="is_big_card" label="Details" ref="toggleRef"
                                    @onSwitchChange="bigCards" />
                            </li>
                            <li>
                                <BaseSelect :options="categoryOptions" name="level_id"
                                    placeholder="Select a qyest category" class="mb-3 " value-by="id_goal_category"
                                    :empty-model-value="''" @onSelect="onSelectCategory" label="Category"
                                    :hide-selected="false" ref="selectCategoryRef">

                                    <template #selected="{ selected }">
                                        <template v-if="selected">
                                            <div class="d-flex justify-content-start align-items-center">
                                                <Image class="of-contain" :src="selected.logo_link" width="45"
                                                    height="45" />
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
                            </li>
                            <li>
                                <BaseSelect :options="levelIdOptions" name="level_code_number" label="Level"
                                    class="mb-3" placeholder="Select level" @onSelect="onSelectLevel"
                                    ref="selectLevelRef" :hide-selected="false">

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
                            </li>
                            <li>
                                <BaseSelect :options="statusPublishList" name="status_list" label="Status publish"
                                    placeholder="ALL" value-by="value" label-by="label"
                                    @onSelect="onSelectStatusPublish" ref="selectStatusPublishRef" />
                            </li>
                        </ul>
                    </div>
                </div>
            </Form>
        </div>

        <div class="goal-list-wrapper mb-8">
            <div class="row gy-4">
                <template v-if="!isBigCards">
                    <div class="col-3" v-for="(goal, index) in goals.resultData" :key="index"
                        v-if="goals?.resultData?.length > 0 && goals?.TotalPage != undefined">
                        <Goal :goal="goal">
                            <template #card-footer>
                                <div class="d-flex justify-content-center align-content-center">
                                    <Button class="red outline mx-3 rounded-md" type="button"
                                        @click="deleteGoal(goal.id)" :disabled="buttonPublishStatus.disabled">
                                        <Ionicon icon_name="trash" font_size="24" />
                                    </Button>
                                    <router-link :to="`/edit-quest/${goal.id}?programid=${goal.programid}`">
                                        <Button class="orange mx-3 rounded-md" type="button">
                                            <Ionicon icon_name="create" font_size="24" />
                                        </Button>
                                    </router-link>
                                </div>
                            </template>
                        </Goal>
                    </div>
                    <template v-else-if="goals?.resultData?.length == 0 && goals?.TotalPage == 0">
                        <p class="f-game fs-24 base-grey text-center">Oops! The query was not found. How about trying a
                            different keyword?</p>
                    </template>
                    <div class="col-3" v-for="(n, index) in 12" v-else>
                        <SliderItemSkeleton />
                    </div>
                </template>
                <template v-else>
                    <div class="col-6" v-for="(goal, index) in goals.resultData" :key="index"
                        v-if="goals?.resultData?.length > 0 && goals?.TotalPage != undefined">
                        <BigCardGoal :goal="goal">
                            <template #card-footer>
                                <div class="d-flex justify-content-center align-content-center">
                                    <Button class="red outline mx-3 rounded-md" type="button"
                                        @click="deleteGoal(goal.id)" :disabled="buttonPublishStatus.disabled">Delete
                                        <Ionicon icon_name="trash" class="ms-3" font_size="24" />
                                    </Button>
                                    <router-link :to="`/edit-quest/${goal.id}?programid=${goal.programid}`">
                                        <Button class="orange mx-3 rounded-md" type="button">Edit
                                            <Ionicon icon_name="create" class="ms-3" font_size="24" />
                                        </Button>
                                    </router-link>
                                </div>
                            </template>
                        </BigCardGoal>
                    </div>
                    <template v-else-if="goals?.resultData?.length == 0 && goals?.TotalPage == 0">
                        <p class="f-game fs-24 base-grey text-center">Oops! The query was not found. How about trying a
                            different keyword?</p>
                    </template>
                    <div class="col-6" v-for="(n, index) in 12" v-else>
                        <BigItemSkeleton />
                    </div>
                </template>
            </div>
        </div>

        <div class="d-flex justify-content-center mb-6">
            <div>
                <VueAwesomePaginate :total-items="totalPage" :items-per-page="itemShowPerPage" :max-pages-shown="12"
                    v-model="currentPage" :on-click="onClickHandler" />
            </div>
        </div>

        <div class="mb-6">
            <div class="fs-24 text-sm-bold mb-3">Customize your Quest</div>
            <div class="fs-14 grey text-sm-bold">Customize your quests based on need. Different templates for various
                data
                types like events, forms, registration flows, feedbacks, etc.</div>
            <div class="bg-white shadow-out--sm py-3 px-2 radius-16 text-center mt-6 user-select-none cursor-pointer">
                <div class="row">
                    <div class="col-3">
                        <Image src="/goal%20template.svg" class="img-fluid greyscale" height="416" width="270" />
                        <div class="f-game fs-16 text-disabled">Coming soon</div>
                    </div>
                    <div class="col-3">
                        <Image src="/profile%20template.svg" class="img-fluid greyscale" height="416" width="270" />
                        <div class="f-game fs-16 text-disabled">Coming soon</div>
                    </div>
                    <div class="col-3">
                        <Image src="/content%20template.svg" class="img-fluid greyscale" height="416" width="270" />
                        <div class="f-game fs-16 text-disabled">Coming soon</div>
                    </div>
                    <div class="col-3">
                        <Image src="/event%20template.svg" class="img-fluid greyscale" height="416" width="270" />
                        <div class="f-game fs-16 text-disabled">Coming soon</div>
                    </div>
                </div>
            </div>
        </div>

        <div class="mb-6">
            <div class="fs-24 text-sm-bold mb-3">How do quests work?</div>
            <div class="fs-14 grey text-sm-bold">To create a quest, you must assign a level and category to it.
                After that you need to follow the flow where you add Quest details, rewards, steps and fields.
                Publish the quest for startups for them to complete and receive the rewards.</div>
        </div>
        <Accordion :accordion-data="accordionObj" />

    </div>
</template>

<script setup>
import { useRouter, useRoute } from "vue-router";
import PageExplanation from "@/components/PageExplanation.vue"
import GoalCategories from "@/components/Goals/GoalCategories.vue"
import BigArrowFeatures from "@/components/Goals/BigArrowFeatures.vue"
import TextLevelGoal from "@/components/Goals/TextLevelGoal.vue"
import LevelBar from "@/components/Goals/LevelBar.vue"
import NumberTextCircle from "@/components/Goals/NumberWithCircle.vue"
import Ionicon from "@/components/Ionicon.vue"
import Image from "@/components/Image.vue"
import BigCardGoal from "@/components/Goals/BigCardGoal.vue"
import { computed, ref, inject, reactive, onMounted } from "vue"
import SwitchCheckbox from "@/components/SwitchCheckbox.vue"
import Accordion from "@/components/Accordion.vue";
import { useGoalsStore } from "@/stores/goalsStore"
import { storeToRefs } from 'pinia';
import Button from "@/components/Button.vue"
import BaseSelect from "@/components/Form/BaseSelect.vue"
import BaseInput from "@/components/Form/BaseInput.vue"
import BaseInputSwitch from "@/components/Form/BaseInputSwitch.vue"
import BaseCircleButton from "@/components/Button/BaseCircleButton.vue"
import { Form } from 'vee-validate'
import Swal from 'sweetalert2'
import { useDebounceFn, watchDebounced } from '@vueuse/core'
import BigItemSkeleton from "@/components/Skeletons/BigItemSkeleton.vue"
import SliderItemSkeleton from "@/components/Skeletons/SliderItemSkeleton.vue"
import Goal from "@/components/Goals/Goal.vue"

import { adminAuthStore } from "@/stores/adminAuthStore"
const { adminAuth, programList, programGroup, currentProgram, currentProgramGroup } = storeToRefs(adminAuthStore());
const router = useRouter();
const route = useRoute();
const { goals } = storeToRefs(useGoalsStore());
const { fetchProgramGroup, fetchProgram } = adminAuthStore();
const { fetchGoals, DeleteGoal, goalDefault, goalFieldSchema, SaveGoal } = useGoalsStore();

const totalPage = ref(1);
const currentPage = ref(1);
const showEntries = ref();
const authStore = adminAuthStore();
const itemShowPerPage = ref();
const searchInput = ref();
const programGroupId = ref(0);
const programId = ref(0);

const programGroupList = computed(() => {
    return [
        {
            programGroupId: 0,
            groupName: "ALL"
        },
        {
            programGroupId: -1,
            groupName: "No Group"
        },
        ...programGroup.value
    ];
});

const programGroupRef = ref();
const isProgramGroupALL = computed(() => {
    return programGroupRef.value?.selectRef?.selected?.groupName == 'ALL' ? true : false
});

const programGroupIdFunc = computed(() => {
    return programGroupRef.value ? (programGroupRef.value.selectRef.selected.groupName == 'ALL' || programGroupRef.value.selectRef.selected.groupName == 'No Group' ? 0 : programGroupRef.value.selectRef.selected.programGroupId) : 0;
});

watchDebounced(
    () => adminAuth.value.programId,
    async (newValue, oldValue) => {
        goals.value = [];
        await fetchProgramGroup(adminAuth.value.programId);
        await fetchGoals(filteredCategory.value, filteredLevel.value, showEntries.value?.selectRef.selected, 1, adminAuth.value.programId, filteredStatusPublish.value, searchInput.value?.inputValueRef.value ?? "", programGroupIdFunc.value, isProgramGroupALL.value).then(() => {
            itemShowPerPage.value = showEntries.value?.selectRef.selected;
            totalPage.value = goals.value?.TotalPage;
            currentPage.value = 1;
        });
    },
    1000
);

const onSelectProgramGroup = async (id) => {
    programGroupId.value = id;
    goals.value = [];
    await fetchProgramGroup(adminAuth.value.programId);
    await fetchGoals(filteredCategory.value, filteredLevel.value, showEntries.value?.selectRef.selected, 1, adminAuth.value.programId, filteredStatusPublish.value, searchInput.value?.inputValueRef.value ?? "", programGroupIdFunc.value, isProgramGroupALL.value).then(() => {
        itemShowPerPage.value = showEntries.value?.selectRef.selected;
        totalPage.value = goals.value?.TotalPage;
        currentPage.value = 1;
    });
}

onMounted(async () => {
    goals.value = [];
    await fetchGoals(filteredCategory.value, filteredLevel.value, showEntries.value?.selectRef.selected, 1, adminAuth.value.programId, filteredStatusPublish.value, searchInput.value?.inputValueRef.value ?? "", programGroupIdFunc.value, isProgramGroupALL.value).then(() => {
        totalPage.value = goals.value?.TotalPage;
        itemShowPerPage.value = showEntries.value?.selectRef.selected;
        currentPage.value = 1;
    });
    await fetchProgramGroup(adminAuth.value.programId);

});

// authStore.$subscribe(async () => {
//     goals.value = [];
//     await fetchGoals(filteredCategory.value, filteredLevel.value, showEntries.value?.selectRef.selected, 1, adminAuth.value.programId, filteredStatusPublish.value, searchInput.value?.inputValueRef.value ?? "").then(() => {
//         totalPage.value = goals.value?.TotalPage;
//         itemShowPerPage.value = showEntries.value?.selectRef.selected;
//     });

// });


const onClickHandler = async (number) => {
    currentPage.value = number;
    goals.value = [];
    await fetchGoals(filteredCategory.value, filteredLevel.value, showEntries.value?.selectRef.selected, number, adminAuth.value.programId, filteredStatusPublish.value, searchInput.value?.inputValueRef.value ?? "", programGroupIdFunc.value, isProgramGroupALL.value);
    itemShowPerPage.value = showEntries.value?.selectRef.selected;
};

const accordionObj = [
    {
        'title': 'What is a QUEST?',
        'content': 'A quest is a set of tasks startups need to complete in order to earn a reward (The Startup Buddy Coins). Your startups can use these coins for networking, access to events etc. Quests on the Dashboard can have up to 5 STEPS. Rewards are granted after completing all the steps for one quest. Forms, feedback, pitch deck uploads, registrations, events etc. can all be Quests'
    },
    {
        'title': 'What is a STEP?',
        'content': 'A step is a form of input that you require from the startups like company financial details, pitch decks, feedback etc. Each step can have up to 3 fields. You can choose from multiple field types (text, sliders, dropdown, dates, uploads, etc.). These fields are used to collect data, files, feedback, registrations, etc, from startups'
    },
    {
        'title': 'How to create a Quest?',
        'content': 'Click on the “Create” (link to create) button at the top of the screen. You will be redirected to the Quests section. Select a level and category for the quest. Then fill out the quest details and reward you want to provide after completion. Add steps to the quest and publish it so that startups can access and complete them'
    },
    {
        'title': 'What are levels and categories?',
        'content': ' Each quest can be divided into 3 levels (Lvl 1 - Lvl Max) based on their importance. Also, you can divide the quests into 4 categories (Company, Team, Service and Growth). Example: Important details about companies (valuation, funding etc.) will come under LVL 1 for Company, Team details will come under LVL 1 for Team, etc.)'
    }
];
const levelId = ref(2);
const levelIdOptions = [2, 3, 4];
const axios = inject("axios");
const selectLevelRef = ref();
const selectCategoryRef = ref();
const selectStatusPublishRef = ref();

const filteredCategory = computed(() => {
    return selectCategoryRef.value.selectRef.selected.id_goal_category != undefined ? selectCategoryRef.value.selectRef.selected.id_goal_category : "";
});

const filteredLevel = computed(() => {
    return selectLevelRef.value.selectRef.selected != undefined ? selectLevelRef.value.selectRef.selected : "";

});

const filteredStatusPublish = computed(() => {
    return selectStatusPublishRef.value.selectRef.selected.value != undefined ? selectStatusPublishRef.value.selectRef.selected.value : "";

});

const onInputSearch = useDebounceFn(async (text) => {
    goals.value = [];
    await fetchGoals(filteredCategory.value, filteredLevel.value, showEntries.value?.selectRef.selected, 1, adminAuth.value.programId, filteredStatusPublish.value, text.target.value, programGroupIdFunc.value, isProgramGroupALL.value).then(() => {
        itemShowPerPage.value = showEntries.value?.selectRef.selected;
        totalPage.value = goals.value?.TotalPage;
        currentPage.value = 1;
    });

}, 500);

const onSelectLevel = async (e) => {
    goals.value = [];
    const level = e != "" ? e : 2;
    levelId.value = level;
    await fetchGoals(filteredCategory.value, filteredLevel.value, showEntries.value?.selectRef.selected, 1, adminAuth.value.programId, filteredStatusPublish.value, searchInput.value?.inputValueRef.value ?? "", programGroupIdFunc.value, isProgramGroupALL.value).then(() => {
        itemShowPerPage.value = showEntries.value?.selectRef.selected;
        totalPage.value = goals.value?.TotalPage;
        currentPage.value = 1;
    });

}

const onSelectCategory = async () => {
    goals.value = [];
    await fetchGoals(filteredCategory.value, filteredLevel.value, showEntries.value?.selectRef.selected, 1, adminAuth.value.programId, filteredStatusPublish.value, searchInput.value?.inputValueRef.value ?? "", programGroupIdFunc.value, isProgramGroupALL.value).then(() => {
        itemShowPerPage.value = showEntries.value?.selectRef.selected;
        totalPage.value = goals.value?.TotalPage;
        currentPage.value = 1;
    });

}

const onSelectStatusPublish = async (e) => {
    goals.value = [];
    await fetchGoals(filteredCategory.value, filteredLevel.value, showEntries.value?.selectRef.selected, 1, adminAuth.value.programId, filteredStatusPublish.value, searchInput.value?.inputValueRef.value ?? "", programGroupIdFunc.value, isProgramGroupALL.value).then(() => {
        itemShowPerPage.value = showEntries.value?.selectRef.selected;
        totalPage.value = goals.value?.TotalPage;
        currentPage.value = 1;
    });

}

const onSelectShowEntries = async (selected) => {
    goals.value = [];
    await fetchGoals(filteredCategory.value, filteredLevel.value, showEntries.value?.selectRef.selected, 1, adminAuth.value.programId, filteredStatusPublish.value, searchInput.value?.inputValueRef.value ?? "", programGroupIdFunc.value, isProgramGroupALL.value).then(() => {
        totalPage.value = goals.value.TotalPage;
        itemShowPerPage.value = showEntries.value?.selectRef.selected;
        currentPage.value = 1;
    });
    currentPage.value = 1;
    itemShowPerPage.value = selected;
}
const isButtonClicked = ref(false);
const createGoal = () => {

    isButtonClicked.value = true;

    let formData = new FormData();

    formData.append('data', JSON.stringify(goalDefault));
    formData.append('data_ui', JSON.stringify(goalFieldSchema));
    formData.append('programId', adminAuth.value.programId);

    SaveGoal(formData).then((res) => {
        if (res.status === 200) {

            return router.push(`/edit-quest/${res.data}`);

        }
    }).then(() => {
        isButtonClicked.value = false;
    });

}

const categoryIdOptions = {
    // 1: [
    //     {
    //         id: 1,
    //         logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/personel-icon-0.png",
    //         name: "Team",
    //         id_goal_category: 1
    //     },
    //     {
    //         id: 5,
    //         logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/company-icon-0.png",
    //         name: "Company",
    //         id_goal_category: 2
    //     },
    //     {
    //         id: 9,
    //         logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/concept-icon-0.png",
    //         name: "Service",
    //         id_goal_category: 3
    //     },
    //     {
    //         id: 13,
    //         logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/growth-icon-0.png",
    //         name: "Growth",
    //         id_goal_category: 4
    //     },

    // ],
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

const statusPublishList = [
    {
        label: 'ALL',
        value: 2
    },
    {
        label: 'Published',
        value: 1,
    },
    {
        label: 'Unpublished / Draft',
        value: 0,
    },
]

const categoryOptions = computed(() => {
    return categoryIdOptions[levelId.value];
});
let isBigCards = ref(false);
const bigCards = () => {
    isBigCards.value = !isBigCards.value;
}
const buttonPublishStatus = reactive({
    loading: false,
    success: false,
    fail: false,
    disabled: false
});

const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 5000,
    timerProgressBar: true,
    showCloseButton: true,
    didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
});

const deleteGoal = async (id) => {
    if (confirm('Are you sure you want to delete this quest?')) {
        buttonPublishStatus.disabled = true;
        DeleteGoal(id).then((res) => {
            goals.value = [];
            fetchGoals(filteredCategory.value, filteredLevel.value, showEntries.value?.selectRef.selected, 1, adminAuth.value.programId, filteredStatusPublish.value, searchInput.value?.inputValueRef.value ?? "", programGroupIdFunc.value, isProgramGroupALL.value);
            currentPage.value = 1;
            itemShowPerPage.value = showEntries.value?.selectRef.selected;
        }).finally(() => {
            buttonPublishStatus.disabled = false;
            Toast.fire({
                icon: 'success',
                title: 'The quest was deleted successfully',
                background: '#ACE7A0',
                iconColor: '#5BBA47',
                color: '#258212'
            });
        });
    }
}


</script>

<style scoped></style>