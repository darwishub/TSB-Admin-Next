<template>
    <PageExplanation class="goals-topbar" is-open :logo="adminAuth?.photo" :breadcrumbs="`${adminAuth?.name} ${route?.meta?.breadcrumb}`">
        <template v-slot:body-content>

            <div class="my-5 row justify-content-between align-items-center">
                <div class="col-9">
                    <div class="text-sm-bold f-game ls-1 fs-24 mb-3">{{ shopEntryPreview?.title }}</div>
                    <p class="mb-0 fs-16 mb-4" v-html="shopEntryPreview?.description"></p>
                </div>

                <div class="col-3">
                    <Badge class="mb-3 goal-label blue me-3" v-if="shopEntryPreview?.tags?.length > 0"
                        v-for="label in shopEntryPreview?.tags">{{ label }}</Badge>

                    <div class="shadow-in--sm radius-16 d-flex justify-content-center card-goal-info px-3 mb-3 py-3"
                        v-if="shopEntryPreview?.content_type == 0">
                        <div class="text-sm-bold fs-24 text-warning text-uppercase text-center">{{ eventDate }}</div>
                    </div>

                    <Button type="button" class="mx-auto d-table rounded-md btn-game px-0 py-1 w-100"
                        :class="shopEntryPreview?.price_type == 0 ? 'blue' : shopEntryPreview?.price_type == 1 ? 'gold' : 'green'">
                        <div class="d-flex justify-content-center align-items-center">
                            <div class="text-button text-border f-game fs-20 ls-1">
                                {{ shopEntryPreview?.price_type == 0 || shopEntryPreview?.price_type == 2 ? 'JOIN' :
                                    'PREMIUM' }}
                            </div>
                            <div class="text-button fs-24 ms-3">
                                {{ shopEntryPreview?.price_type == 0 ? 'FREE' : shopEntryPreview?.price_type == 1 ?
                                    shopEntryPreview?.price?.toLocaleString("en-US") : `+
                                ${shopEntryPreview?.price?.toLocaleString("en-US")}` }}
                                <Image src="/coin.svg" v-if="shopEntryPreview?.price_type != 0"></Image>
                            </div>
                        </div>
                    </Button>
                </div>
            </div>
        </template>
    </PageExplanation>
    <div class="goal-details-hero py-6 shadow--lg mb-6"
        :style="[shopEntryPreview?.goal_img != null ? { 'background-image': 'url(' + shopEntryPreview?.file_url + ')' } : { 'background-color': '#c2c2c2' }]">
    </div>
    <div class="container">

        <div class="shadow-out--sm radius-16 px-5 pb-5 pt-4 mb-6 bg-white content-wrapper">
            <div class="text-sm-bold f-game ls-1 text-center fs-24 my-3">{{ shopEntryPreview?.title }}</div>
            <p class="mb-0 fs-16 mb-3"
                v-if="shopEntryPreview?.content_type == 0 && shopEntryPreview?.event_address != undefined && shopEntryPreview?.event_type == 2 
                || shopEntryPreview?.content_type == 0 && shopEntryPreview?.event_address != undefined && shopEntryPreview?.event_type == 0 
                ">
                <b>Address: </b> {{ shopEntryPreview?.event_address }}
            </p>
            <p class="mb-0 fs-16 mb-3"
                v-if="shopEntryPreview?.content_type == 0 && shopEntryPreview?.event_url != undefined && shopEntryPreview?.event_type == 2 
                || shopEntryPreview?.content_type == 0 && shopEntryPreview?.event_url != undefined && shopEntryPreview?.event_type == 1 
                "><b>Link: </b>
                <a :href="shopEntryPreview?.event_url" target="_blank">{{ shopEntryPreview?.event_url }}</a>
            </p>
            <div v-html="shopEntryPreview?.content"></div>
            <!-- <Button type="button" class="mx-auto d-table rounded-md btn-game px-0 py-1 w-25"
                :class="shopEntryPreview?.price_type == 0 ? 'blue' : shopEntryPreview?.price_type == 1 ? 'gold' : 'green'">
                <div class="d-flex justify-content-center align-items-center">
                    <div class="text-button text-border f-game fs-20 ls-1">
                        {{ shopEntryPreview?.price_type == 0 || shopEntryPreview?.price_type == 2 ? 'JOIN' : 'PREMIUM' }}
                    </div>
                    <div class="text-button fs-24 ms-3">
                        {{ shopEntryPreview?.price_type == 0 ? 'FREE' : shopEntryPreview?.price_type == 1 ? shopEntryPreview?.price?.toLocaleString("en-US") : `+
                        ${shopEntryPreview?.price?.toLocaleString("en-US")}` }}
                        <Image src="/coin.svg" v-if="shopEntryPreview?.price_type != 0"></Image>
                    </div>
                </div>
            </Button> -->
        </div>

    </div>
</template>

<script setup>
import { computed, onMounted, ref, reactive, defineAsyncComponent, toRaw, watch } from "vue"
import { storeToRefs } from 'pinia';
import PageExplanation from "@/components/PageExplanation.vue"
import Button from "@/components/Button.vue"
import Image from "@/components/Image.vue"
import { useShopStore } from "@/stores/shopStore"
import { useDateFormat } from '@vueuse/core'
import Badge from "@/components/Badge.vue"
import { useRoute } from 'vue-router'
import { adminAuthStore } from "@/stores/adminAuthStore"

const { shopEntryPreview } = storeToRefs(useShopStore());
const { shopPreviewRestart } = useShopStore();
const { adminAuth, programGroup } = storeToRefs(adminAuthStore());
const route = useRoute();
const eventDate = computed(() => {
    if (shopEntryPreview.value?.date != undefined) {
        let date = useDateFormat(shopEntryPreview.value?.date, 'DD MMM YYYY');
        return date.value;
    }
});


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
}</style>