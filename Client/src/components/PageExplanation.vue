<template>
    <div class="page-info shadow--sm">
        <div class="bg-white shadow-out--sm py-2 fixed-top sub-bar">
            <div class="container">
                <slot name="top-content"></slot>
            </div>
        </div>
        <div class="bg-white py-3 mt-6 breadcrumb-page">
            <div class="container" v-auto-animate="{
                easing: 'ease',
                duration: 400,
            }">
                <div class="d-flex justify-content-between align-items-center">
                    <div class="d-flex justify-content-start align-items-center user-select-none">
                        <Image
                            :src="`https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${ logo }`"
                            alt="DP" class="rounded-lg shadow me-3" width="35" height="35" 
                            v-if="!logoHidden"
                            />
                        <div :class="{ invisible: breadcrumbHidden }">
                            <span class="fs-16 text-info text-sm-bold">{{ props.breadcrumbs }}</span>
                        </div>
                    </div>
                    <div class="cursor-pointer" id="toggle-info-body" @click="isOpen = !isOpen">
                        <Ionicon icon_name="chevron-up-circle" class="toggle-transition disabled"
                            :class="isOpen ? 'toggle-down' : 'toggle-up'" font_size="32" />
                    </div>
                </div>
                <div v-if="isOpen">
                    <slot name="body-content"></slot>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import Image from "@/components/Image.vue";
import Ionicon from "@/components/Ionicon.vue";
import { storeToRefs } from 'pinia';
import { adminAuthStore } from "@/stores/adminAuthStore"
import { ref, onMounted } from "vue"
import { useRoute } from "vue-router";

const props = defineProps({
    isOpen: {
        type: Boolean,
        default: false
    },
    breadcrumbHidden: {
        type: Boolean,
        default: false
    },
    logo: {
        type: String,
    },
    logoHidden: {
        type: Boolean,
        default: false
    },
    breadcrumbs: {
        type: String
    }
})

const isOpen = ref(props.isOpen);
const breadcrumbHidden = ref(props.breadcrumbHidden);
const route = useRoute();
const { adminAuth } = storeToRefs(adminAuthStore());
</script>

<style scoped>
.sub-bar {
    top: 64px;
    left: 80px;
    width: calc(100% - 80px);
    transition: all 0.5s ease;
    z-index: 1000;
}

.sidebar.open~.home-section .sub-bar {
    left: 216px;
    width: calc(100% - 216px);
}
</style>