<template>
    <PageExplanation class="goals-topbar" is-open :breadcrumbs="`${adminAuth?.name} ${route?.meta?.breadcrumb}`" :logo="adminAuth?.photo">
        <template v-slot:body-content>

            <div class="my-5 row justify-content-between align-items-center">
                <div class="col-9">
                    <!-- <p class="text-sm-bold f-game ls-1 fs-24 mb-3">{{ academyEntryPreview.date }}</p> -->
                    <p class="mb-0 fs-16 mb-4">{{ academyEntryPreview?.description }}</p>
                    <p class="mb-0 fs-16 mb-3" v-if="academyEntryPreview?.content_type == 0 && academyEntryPreview?.event_address != undefined"><b>Address:</b> {{ academyEntryPreview?.title }}</p>
                    <p class="mb-0 fs-16 mb-3" v-if="academyEntryPreview?.content_type == 0 && academyEntryPreview?.event_url != undefined"><b>Link:</b> <a :href="academyEntryPreview?.event_url" target="_blank">{{ academyEntryPreview?.content }}</a></p>
                </div> 

                <div class="col-3">
                    <Badge class="mb-3 goal-label blue me-3" v-if="academyEntryPreview?.tags?.length > 0"
                                    v-for="label in academyEntryPreview?.tags">{{ label }}</Badge>

                    <div class="shadow-in--sm radius-16 d-flex justify-content-center card-goal-info px-3 mb-3 py-3"
                        >
                        <div class="text-sm-bold fs-24 text-warning text-uppercase text-center">{{ createDate }}</div>
                    </div>

                    <Button type="button" class="mx-auto d-table rounded-md btn-game px-0 py-1 w-100 gold">
                        <div class="d-flex justify-content-center align-items-center">
                            <div class="text-button text-border f-game fs-20 ls-1">
                                {{ 'CLAIM COINS' }}
                            </div>
                            <div class="text-button fs-24 ms-3">
                                {{ `${academyEntryPreview?.rewards?.toLocaleString("en-US")}` }}
                                <Image src="/coin.svg"></Image>
                            </div>
                        </div>
                    </Button>
                </div>
            </div>
        </template>
    </PageExplanation>
    <div class="goal-details-hero py-6 shadow--lg mb-6"
        :style="[academyEntryPreview?.goal_img != null ? { 'background-image': 'url(' + academyEntryPreview?.file_url + ')' } : { 'background-color': '#c2c2c2' }]">
    </div>
    <div class="container">
        <div class="shadow-out--sm radius-16 px-5 pb-5 pt-4 mb-6 bg-white content-wrapper">
            <div class="text-sm-bold f-game ls-1 text-center fs-24 my-3">{{ academyEntryPreview?.title }}</div>
            <div v-if="academyEntryPreview?.content_type == 1 && academyEntryPreview?.exclusive == true"  v-html="academyEntryPreview?.content"></div>
            <div class="ratio ratio-16x9" v-if="academyEntryPreview?.content_type == 0">
                <iframe :src="`https://www.youtube.com/embed/${convertUrlToEmbeed}`" title="Youtube Video" frameborder="0" allow="picture-in-picture"
                    allowfullscreen></iframe>
            </div>
            <Button v-if="academyEntryPreview?.content_type == 1 && academyEntryPreview?.exclusive == false" type="button" class="mx-auto d-table rounded-md btn-game px-0 py-1 w-25 mt-3 green">
                <div class="d-flex justify-content-center align-items-center">
                    <div class="text-button text-border f-game fs-20 ls-1">
                        {{ 'READ MORE' }}
                    </div>
                </div>
            </Button>
            <Button v-if="academyEntryPreview?.content_type == 0" @click="onCheckSource(academyEntryPreview?.Url)" type="button" class="mx-auto d-table rounded-md btn-game px-0 py-1 w-25 green mt-3">
                <div class="d-flex justify-content-center align-items-center">
                    <div class="text-button text-border f-game fs-20 ls-1">
                        {{ 'CHECK SOURCE' }}
                    </div>
                </div>
            </Button>
        </div>

    </div>
</template>

<script setup>
import { computed, onMounted, ref, reactive, defineAsyncComponent, toRaw, watch } from "vue"
import { storeToRefs } from 'pinia';
import PageExplanation from "@/components/PageExplanation.vue"
import Button from "@/components/Button.vue"
import Image from "@/components/Image.vue"
import { useAcademyStore } from "@/stores/academyStore"
import { adminAuthStore } from "@/stores/adminAuthStore"
import { useDateFormat } from '@vueuse/core'
import Badge from "@/components/Badge.vue"
import { useRoute } from "vue-router";

const { academyEntryPreview } = storeToRefs(useAcademyStore());
const { academyPreviewRestart } = useAcademyStore();
const { adminAuth, programGroup } = storeToRefs(adminAuthStore());
const route = useRoute();

const createDate = computed(() => {
    if(academyEntryPreview?.value.date != undefined){
        let date = useDateFormat(academyEntryPreview?.value.date, 'DD MMM YYYY');
        return date.value;
    }else{
        let date = new Date();
        let day = date.getDate();
        let monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        let month = monthNames[date.getMonth()];
        let year = date.getFullYear();

        let formattedDate = day + " " + month + " " + year;
        return formattedDate;
    }
});


const onCheckSource = async (link) => {
    if(link != null)
    {
        window.open(link);
    } else {
        window.open(academyEntryPreview?.value.url)
    }   
}

const convertUrlToEmbeed = computed(() => {
    const regExp = /^.*(youtu.be\/|v\/|u\/\w\/|embed\/|watch\?v=|&v=)([^#&?]*).*/;
    const match = academyEntryPreview.value?.Url?.match(regExp);
    console.log(match);
    if(academyEntryPreview.value?.Url != null){
        return (match && match[2].length === 11)
        ? match[2]
        : null;
    } else {
        const match_url = academyEntryPreview.value?.url?.match(regExp);
        console.log(match_url);
        return (match_url && match_url[2].length === 11)
        ? match_url[2]
        : null;
    }
})

onMounted(() => {
   
});


</script>
<style scoped>
.goal-details-hero {
    background-repeat: no-repeat;
    background-attachment: scroll;
    background-position: center;
    background-size: cover;
    background-attachment: scroll;
    height: 500px;
    position: relative;
}

.content-wrapper {
    margin-top: -30rem;
    position: relative;
}
</style>