<template>
    <PageExplanation class="goals-topbar" is-open :breadcrumbs="`${adminAuth?.name} ${route?.meta?.breadcrumb}`" :logo="adminAuth?.photo">
        <template v-slot:body-content>
            <div class="row justify-content-between align-items-center content-inside user-select-none py-3">
                <div class="col-2">
                    <Image width="100" height="100"
                        src="https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/cart-outline+(1).svg" />
                </div>
                <div class="col-6">
                    <div class="d-flex align-content-center align-items-center">
                        <Image src="/alien.svg" height="80" width="56" />
                        <div>
                            <div class="shadow-out--sm bg-white ms-3 p-3 radius-16 custom-dialog-box">
                                <span class="fs-16 text-sm-bold text-green">Hello {{ adminAuth.name }},</span><br /><br />
                                <span class="fs-14 grey">Let’s create a Quest together! Startups need to complete all the
                                    steps that you will assign to the quest in order to receive the reward. Make it
                                    lucrative!</span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-2">
                    <div>
                        <div class="fs-24 text-md text-center text-warning f-game mb-4">Create an entry</div>

                        <div class="btn-add-layer-2" @click="createEntry">
                            <div class="btn-add-layer-1 d-table mx-auto">
                                <BaseCircleButton class="btn-content blue btn-circle-shadow" width="86" height="86">
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
                    <BaseInput name="search_input" label="Search" placeholder="Search shops by title and description"
                        type="text" ref="searchInput" @input="onInputSearch" class="w-100" />
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
                                <BaseInputSwitch name="is_big_card" label="Details" ref="toggleRef" @onSwitchChange="bigCards" />
                            </li>
                            <li>
                                <BaseSelect :options="statusList" name="status_list" label="Select Category"
                                    placeholder="ALL" value-by="value" label-by="label" ref="selectStatusRef"
                                    @onSelect="onSelectStatus" />
                            </li>
                        </ul>
                    </div>
                </div>
            </Form>
        </div>

        <div class="goal-list-wrapper mb-8">
            <div class="row gy-4">
                <template v-if="!isBigCards">

                    <div class="col-3" v-for="(item, index) in shopItems?.listsData" :key="item"
                        v-if="shopItems?.listsData?.length > 0 && shopItems?.TotalPage != undefined">
                        <ItemEntry :item="item">
                            <template #card-footer>
                                <div class="d-flex justify-content-center align-content-center">
                                    <Button class="red outline mx-3 rounded-md" type="button" @click="deleteItem(item?.item_type, item?.id)">
                                        <Ionicon icon_name="trash" font_size="24" />
                                    </Button>
                                    <router-link :to="`/shop-entry-edit/${item?.name}/${item?.item_type}/${item?.id}?programid=${item.programId}`">
                                        <Button class="orange mx-3 rounded-md" type="button">
                                            <Ionicon icon_name="create" font_size="24" />
                                        </Button>
                                    </router-link>
                                </div>
                            </template>
                        </ItemEntry>
                    </div>
                    <template v-else-if="shopItems?.listsData?.length == 0 && shopItems?.TotalPage == 0">
                        <p class="f-game fs-24 base-grey text-center">Oops! The query was not found. How about trying a
                            different keyword?</p>
                    </template>
                    <div class="col-3" v-for="(n, index) in 12" v-else>
                        <SliderItemSkeleton />
                    </div>

                </template>
                <template v-else>
                    <div class="col-6" v-for="(item, index) in shopItems?.listsData" :key="item"
                        v-if="shopItems?.listsData?.length > 0 && shopItems?.TotalPage != undefined">
                        <BigItemEntry :item="item">
                            <template #card-footer>
                                <div class="d-flex justify-content-center align-content-center">
                                    <Button class="red outline mx-3 rounded-md" type="button"
                                        @click="deleteItem(item?.item_type, item?.id)">Delete
                                        <Ionicon icon_name="trash" class="ms-3" font_size="24" />
                                    </Button>
                                    <router-link :to="`/shop-entry-edit/${item?.name}/${item?.item_type}/${item?.id}?programid=${item.programId}`">
                                        <Button class="orange mx-3 rounded-md" type="button">Edit
                                            <Ionicon icon_name="create" class="ms-3" font_size="24" />
                                        </Button>
                                    </router-link>
                                </div>
                            </template>
                        </BigItemEntry>
                    </div>
                    <template v-else-if="shopItems?.listsData?.length == 0 && shopItems?.TotalPage == 0">
                        <p class="f-game fs-24 base-grey text-center">Oops! The query was not found. How about trying a
                            different keyword?</p>
                    </template>
                    <div class="col-6" v-for="(n, index) in 12" v-else>
                        <BigItemSkeleton />
                    </div>
                </template>
            </div>
        </div>

        <div class="d-flex justify-content-center">
            <div>
                <VueAwesomePaginate :total-items="totalPage" :items-per-page="itemShowPerPage" :max-pages-shown="12"
                    v-model="currentPage" :on-click="onClickHandler" />
            </div>
        </div>

    </div>
</template>

<script setup>
import PageExplanation from "@/components/PageExplanation.vue"
import TextLevelGoal from "@/components/Goals/TextLevelGoal.vue"
import LevelBar from "@/components/Goals/LevelBar.vue"
import Ionicon from "@/components/Ionicon.vue"
import Image from "@/components/Image.vue"
import BigItemEntry from "@/components/Shop/BigItemEntry.vue"
import { computed, ref, inject, reactive } from "vue"
import { storeToRefs } from 'pinia';
import Button from "@/components/Button.vue"
import BaseSelect from "@/components/Form/BaseSelect.vue"
import BaseInput from "@/components/Form/BaseInput.vue"
import { Form } from 'vee-validate'
import { onMounted } from "vue"
import { adminAuthStore } from "@/stores/adminAuthStore"
import { useShopStore } from "@/stores/shopStore"
import BaseCircleButton from "@/components/Button/BaseCircleButton.vue"
import { useRouter, useRoute } from "vue-router";
import Swal from 'sweetalert2'
import { useDebounceFn, watchDebounced } from '@vueuse/core'
import BigItemSkeleton from "@/components/Skeletons/BigItemSkeleton.vue"
import SliderItemSkeleton from "@/components/Skeletons/SliderItemSkeleton.vue"
import ItemEntry from "@/components/Shop/ItemEntry.vue"
import BaseInputSwitch from "@/components/Form/BaseInputSwitch.vue"

const { adminAuth, programGroup } = storeToRefs(adminAuthStore());
const { fetchProgramGroup, fetchProgram } = adminAuthStore();
const { shopItems } = storeToRefs(useShopStore());
const { fetchAllShopItems, DeleteItem } = useShopStore();
const totalPage = ref(1);
const currentPage = ref(1);
const showEntries = ref();
const itemShowPerPage = ref();
const searchInput = ref();
const programGroupId = ref(0);

const levelId = ref(2);
const levelIdOptions = [2, 3, 4];
const axios = inject("axios");
const selectLevelRef = ref();
// const selectCategoryRef = ref();
const selectStatusRef = ref();
const router = useRouter();
const route = useRoute();

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


const statusList = [
    {
        label: 'ALL',
        value: ""
    },
    {
        label: 'Events',
        value: 0,
    },
    {
        label: 'Benefits',
        value: 1,
    },
];

const buttonPublishStatus = reactive({
    loading: false,
    success: false,
    fail: false,
    disabled: false
});

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


const categoryOptions = computed(() => {
    return categoryIdOptions[levelId.value];
});

const onSelectShowEntries = async (selected) => {
    shopItems.value = [];
    await fetchAllShopItems(searchInput.value?.inputValueRef.value, filteredLevel.value, adminAuth.value.programId, selected, 1, programGroupIdFunc.value, isProgramGroupALL.value);
    itemShowPerPage.value = selected;
    currentPage.value = 1;
    totalPage.value = shopItems.value?.TotalPage;
};

const onSelectStatus = async (e) => {
    shopItems.value = [];
    await fetchAllShopItems(searchInput.value?.inputValueRef.value, e, adminAuth.value.programId, showEntries.value?.selectRef?.selected, 1, programGroupIdFunc.value, isProgramGroupALL.value);
    itemShowPerPage.value = showEntries.value?.selectRef.selected;
    currentPage.value = 1;
    totalPage.value = shopItems.value?.TotalPage;
};

watchDebounced(
    () => adminAuth.value.programId,
    async (newValue, oldValue) => {
        shopItems.value = [];
        await fetchProgramGroup(adminAuth.value.programId);
        await fetchAllShopItems(searchInput.value?.inputValueRef.value, filteredLevel.value, adminAuth.value.programId, showEntries.value?.selectRef.selected, 1, programGroupIdFunc.value, isProgramGroupALL.value);
        itemShowPerPage.value = showEntries.value?.selectRef.selected;
        totalPage.value = shopItems.value?.TotalPage;
        currentPage.value = 1;
    },
    1000
);

const onSelectProgramGroup = async (id) => {
    programGroupId.value = id;
    shopItems.value = [];
        await fetchProgramGroup(adminAuth.value.programId);
        await fetchAllShopItems(searchInput.value?.inputValueRef.value, filteredLevel.value, adminAuth.value.programId, showEntries.value?.selectRef.selected, 1, programGroupIdFunc.value, isProgramGroupALL.value);
        itemShowPerPage.value = showEntries.value?.selectRef.selected;
        totalPage.value = shopItems.value?.TotalPage;
        currentPage.value = 1;
}

const filteredLevel = computed(() => {
    return selectStatusRef.value?.selectRef.selected.value != undefined ? selectStatusRef.value?.selectRef.selected.value : "";

});

const createEntry = () => {

    return router.push(`/shop-entry`);

};

const deleteItem = (content_type, id) => {
    if (confirm('Are you sure you want to delete this item?')) {
        buttonPublishStatus.disabled = true;
        DeleteItem(content_type, id).then(() => {
            shopItems.value = [];
            fetchAllShopItems(searchInput.value?.inputValueRef.value, filteredLevel.value, adminAuth.value.programId, showEntries.value?.selectRef.selected, 1, programGroupIdFunc.value, isProgramGroupALL.value);
            currentPage.value = 1;
            itemShowPerPage.value = showEntries.value?.selectRef.selected;
        }).finally(() => {
            buttonPublishStatus.disabled = false;
            Toast.fire({
                icon: 'success',
                title: 'The item was deleted successfully',
                background: '#ACE7A0',
                iconColor: '#5BBA47',
                color: '#258212'
            });
        });
    }
}
let isBigCards = ref(false);
const bigCards = () => {
    isBigCards.value = !isBigCards.value;
}

onMounted(async () => {
    shopItems.value = [];
    await fetchAllShopItems(searchInput.value?.inputValueRef.value, filteredLevel.value, adminAuth.value.programId, showEntries.value?.selectRef.selected, 1, programGroupIdFunc.value, isProgramGroupALL.value);
    totalPage.value = shopItems.value?.TotalPage;
    itemShowPerPage.value = showEntries.value?.selectRef.selected;
    currentPage.value = 1;
    await fetchProgramGroup(adminAuth.value.programId);
});

const onInputSearch = useDebounceFn(async (text) => {
    shopItems.value = [];
    await fetchAllShopItems(searchInput.value?.inputValueRef.value, filteredLevel.value, adminAuth.value.programId, showEntries.value?.selectRef.selected, 1, programGroupIdFunc.value, isProgramGroupALL.value);
    itemShowPerPage.value = showEntries.value?.selectRef.selected;
    currentPage.value = 1;
    totalPage.value = shopItems.value?.TotalPage;

}, 500);

const onClickHandler = async (number) => {
    currentPage.value = number;
    shopItems.value = [];
    await fetchAllShopItems(searchInput.value?.inputValueRef.value, filteredLevel.value, adminAuth.value.programId, showEntries.value?.selectRef.selected, number, programGroupIdFunc.value, isProgramGroupALL.value);
}

</script>

<style scoped>
/* btn circle */
.btn-circle {
    padding: 6px 0px;
    border-radius: 50%;
    text-align: center;
    font-size: 10px;
}

.btn-circle-shadow {
    box-shadow: 2px 2px 4px rgb(114 142 171 / 10%), 6px 6px 50px #ffffff, 4px 4px 40px rgb(111 140 176 / 61%) !important;
}

.btn-circle-shadow:focus {
    outline: solid 11px #fff !important;
}

.button--loading .button__text {
    visibility: hidden;
    opacity: 0;
}

.button--loading::after {
    content: "";
    position: absolute;
    width: 24px;
    height: 24px;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    margin: auto;
    border: 3px solid #c2c2c2;
    border-top-color: #fafafe;
    border-radius: 50%;
    animation: button-loading-spinner 0.6s linear infinite;
}

@keyframes fadeIn {
    0% {
        opacity: 0;
    }

    100% {
        visibility: visible;
        opacity: 1;
    }
}

@-webkit-keyframes loading-btn--fade-in {
    0% {
        opacity: 0;
    }

    100% {
        opacity: 1;
    }
}

@keyframes loading-btn--fade-in {
    0% {
        opacity: 0;
    }

    100% {
        opacity: 1;
    }
}

@keyframes button-loading-spinner {
    to {
        transform: rotate(360deg);
    }
}

.btn-add-layer-1 {
    box-shadow: inset -4px -4px 9px rgb(255 255 255 / 88%), inset 4px 4px 14px #c1d5ee;
    border-radius: 100%;
    max-width: 120px;
}

.btn-add-layer-2 {
    margin: 0 auto;
    outline: solid 1rem #fff;
    border-radius: 100%;
    max-width: 140px;
    display: table;
    box-shadow: 2px 2px 4px rgb(114 142 171 / 10%), 6px 6px 50px #ffffff, 4px 4px 40px rgb(111 140 176 / 61%);
}

.btn-circle {
    padding: 6px 0;
    border-radius: 50%;
    text-align: center;
    font-size: 10px;
}

.btn-content {
    outline: solid 11px #fff;
}

.btn-circle-shadow {
    box-shadow: 2px 2px 4px #728eab1a, 6px 6px 50px #fff, 4px 4px 40px #6f8cb09c !important;
}
</style>