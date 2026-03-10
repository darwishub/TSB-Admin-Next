<template>
    <div class="shadow-in--sm-2 radius-16 goal-value w-100">
        <template v-if="isArray">
            <ul class="ps-3 mb-0">
                <li v-for="(item, index) in items" class="grey">
                    <Image
                        :src='`https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${encodeURIComponent(item?.trim())}`'
                        width="96" height="96" class="rounded-md" v-if="isImage && !imageError" @error="onError" 
                        />
                    <template v-else-if="isImage && imageError">
                        <p class="text-danger text-break-all fs-14 mb-0">Unable to load this image</p>
                    </template>
                    <div v-if="isPdf">
                        <div class="d-flex justify-content-between align-items-center" v-if="isLoaded">
                            <div class="d-flex justify-content-start align-items-center mb-3">
                                <Button class="grey rounded-md me-2" type="button" @click="onPrev">
                                    <Ionicon icon_name="chevron-back" font_size="24" />
                                </Button>
                                <Button class="grey rounded-md me-2" type="button" @click="onNext">
                                    <Ionicon icon_name="chevron-forward" font_size="24" />
                                </Button>
                                <Button class="grey rounded-md me-2" type="button" @click="onPrint(index)">
                                    <Ionicon icon_name="print" font_size="24" />
                                </Button>
                                <Button class="grey rounded-md" type="button"
                                    @click="onOpen(`https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${encodeURIComponent(item?.trim())}`)">
                                    <Ionicon icon_name="open" font_size="24" />
                                </Button>

                            </div>
                            <p class="f-game text-info fs-20 ms-2">{{ currentPage }} / {{ totalPages }}</p>
                        </div>
                        <p class="f-game text-info fs-20" v-else-if="!error && !isLoaded">Loading document, please wait...
                        </p>
                        <p class="text-danger text-break-all fs-14" v-else-if="error">{{ errorMessage }}</p>
                        <VuePdfEmbed :page="currentPage" @loaded="onLoaded" @rendering-failed="onRenderFailed"
                            @loading-failed="onLoadFailed" ref="pdfRef"
                            :source="`https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${encodeURIComponent(item?.trim())}`" />
                    </div>
                </li>
            </ul>
        </template>
        <template v-else>
            <Image
                :src='`https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${encodeURIComponent(items?.trim())}`'
                width="96" height="96" class="rounded-md" v-if="isImage" />
            <div v-if="isPdf">
                <div class="d-flex justify-content-between align-items-center" v-if="isLoaded">
                    <div class="d-flex justify-content-start align-items-center mb-3">
                        <Button class="grey rounded-md me-2" type="button" @click="onPrev">
                            <Ionicon icon_name="chevron-back" font_size="24" />
                        </Button>
                        <Button class="grey rounded-md me-2" type="button" @click="onNext">
                            <Ionicon icon_name="chevron-forward" font_size="24" />
                        </Button>
                        <Button class="grey rounded-md me-2" type="button" @click="onPrint(0)">
                            <Ionicon icon_name="print" font_size="24" />
                        </Button>
                        <Button class="grey rounded-md" type="button"
                            @click="onOpen(`https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${encodeURIComponent(items?.trim())}`)">
                            <Ionicon icon_name="open" font_size="24" />
                        </Button>

                    </div>
                    <p class="f-game text-info fs-20 ms-2">{{ currentPage }} / {{ totalPages }}</p>
                </div>
                <p class="f-game text-info fs-20" v-else-if="!error && !isLoaded">Loading document, please wait...
                </p>
                <p class="text-danger text-break-all fs-14" v-else-if="error">{{ errorMessage }}</p>
                <VuePdfEmbed :page="currentPage" @loaded="onLoaded" @rendering-failed="onRenderFailed"
                    @loading-failed="onLoadFailed" ref="pdfRef"
                    :source="`https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${encodeURIComponent(item?.trim())}`" />
            </div>
        </template>
    </div>
</template>

<script setup>
import { computed, ref } from "vue";
import Image from "@/components/Image.vue"
import VuePdfEmbed from 'vue-pdf-embed'
import Ionicon from "@/components/Ionicon.vue"
import Button from "@/components/Button.vue"


const props = defineProps({
    value: {
        type: String
    }
});

let currentPage = ref(1);
let totalPages = ref(0);
let isLoaded = ref(false);
let error = ref(false);
let errorMessage = ref('');
let pdfRef = ref();

const items = computed(() => {
    if (props.value.includes('[') && props.value.includes(']')) {
        return JSON.parse(props.value);
    } else {
        return props.value;
    }
});

const isArray = computed(() => {
    return props.value.includes('[') && props.value.includes(']');
});

const checkFileType = (item, type) => {
    const fileType = item.split('.').pop().toLowerCase();
    return fileType === type || item.toLowerCase().includes(type);
}

const isPdf = computed(() => {
    if (isArray.value) {
        return items.value.some(item => checkFileType(item, 'pdf'));
    } else {
        return checkFileType(props.value, 'pdf');
    }
});

const isImage = computed(() => {
    if (isArray.value) {
        return items.value.some(item => ['jpg', 'jpeg', 'png'].some(type => checkFileType(item, type)));
    } else {
        return ['jpg', 'jpeg', 'png'].some(type => checkFileType(props.value, type));
    }
});

const onPrev = () => {
    if (currentPage.value > 1) {
        currentPage.value--;
    }
};

let imageError = ref(false);
const onError = () => {
    imageError.value = true;
};

const onNext = () => {
    if (currentPage.value < totalPages.value) {
        currentPage.value++;
    }
};
const onLoaded = (data) => {
    totalPages.value = data.numPages;
    isLoaded.value = true;
};

const onPrint = (index) => {
    pdfRef.value[index].print();
};

const onOpen = (url) => {
    window.open(url, '_blank');
};

const onRenderFailed = (e) => {
    isLoaded.value = false;
    error.value = true;
    errorMessage.value = e.message;
};

const onLoadFailed = (e) => {
    isLoaded.value = false;
    error.value = true;
    errorMessage.value = e.message;
};

</script>

<style scoped>
.goal-value {
    width: fit-content;
    padding: 1rem 2rem;
    margin: 0 auto;
}

ul {
    list-style: none;
    padding: 0px;
}
</style>