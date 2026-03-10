<template>
    <PageExplanation class="no-toggle goals-topbar" :breadcrumbs="`${adminAuth?.name} ${route?.meta?.breadcrumb}`" :logo="adminAuth?.photo" />
    <div class="container">
        <div class="row mt-6">
            <div class="col-8">
                <div class="row">
                    <div class="col-2">
                        <Image v-if="userProfile?.user?.Photo != null || userProfile?.user?.Photo != ''"
                            :src="`https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${userProfile?.user?.Photo}`"
                            alt="DP" class="rounded-lg shadow" width="96" height="96" @error="setImageToDefault" />
                        <Image v-else src="https://www.w3schools.com/howto/img_avatar.png" alt="DP"
                            class="rounded-lg shadow" width="96" height="96" />
                    </div>
                    <div class="col-10">
                        <p class="fs-24 text-sm-bold mb-3">{{ userProfile?.userProfile?.Name }}</p>
                        <p class="fs-14 text-sm-bold">{{ userProfile?.userProfile?.Description }}</p>
                    </div>
                </div>
                <div class="row">
                    <p class="my-4 f-game fs-24 text-info">Connections</p>
                    <div class="col-12 col-lg-4 mb-3" v-for="(user, index) in userConnections?.listProfile"
                        v-if="userConnections?.listProfile?.length > 0">
                        <div class="card-goal-wrapper card shadow-out--sm radius-16 h-100 border-none">
                            <div class="py-3">
                                <!-- <div class="card-header bg-white radius-16 border-none">
                                    <div class="d-flex justify-content-between">
                                        <Badge class="align-self-start" :class="member?.Status != 0 ? 'green' : 'grey'">
                                            {{ member?.Status == 0 ? 'Inactive' :
        'Active' }}
                                        </Badge>
                                    </div>
                                </div> -->
                                <div class="card-body px-3">
                                    <div class="d-flex justify-content-start align-items-center mb-3"
                                        style="word-break: break-all;">
                                        <Image
                                            :src="user?.Photo == null || user?.Photo == '' ? `https://www.w3schools.com/howto/img_avatar.png` : `https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${user?.Photo}`"
                                            height="64" width="64" class="img-fluid rounded-lg shadow img-team me-3" />
                                        <div>
                                            <div class="text-md fs-14 profile-name text-break">{{
        userConnections?.query[index]?.Name }}
                                            </div>
                                            <!-- <small class="base-grey profile-name">{{ userConnections?.query[index]?.Description }}</small> -->
                                        </div>
                                    </div>
                                    <p class="fs-14 text-limit mb-0">{{ userConnections?.query[index]?.Description }}</p>
                                </div>
                                <div class="d-flex justify-content-center">
                                    <router-link :to="`/user-profile/${ userConnections?.query[index]?.Userid }/${ userConnections?.startupIds[index] }`">
                                        <Button type="button" class="blue btn-sm outline rounded-md no-edge-radius-md">
                                            View Profile
                                        </Button>
                                    </router-link>
                                </div>
                            </div>
                        </div>
                    </div>
                    <template v-else>
                        <p class="f-game fs-24 base-grey text-center">No connections.</p>
                    </template>
                </div>
            </div>
            <div class="col-4">
                <div class="bg-white shadow-out--sm ps-5 pb-3 pe-5 pt-4 radius-16">
                    <div class="d-flex flex-column justify-content-center align-items-center mb-4">
                        <div class="w-100 mb-3">
                            <p class="text-sm-bold fs-16 text-info">User Role</p>
                            <div
                                class="shadow-in--sm radius-16 d-flex justify-content-between px-4 py-2 align-items-center point-info">
                                <div class="fs-14 text-break text-info">{{ userProfile?.userProfile?.Joinedas == '1000'
        ? 'Startup' : userProfile?.userProfile?.Joinedas
            == '0001' ? 'Investor' : 'Mentor' }} ({{ userProfile?.is_member ? 'Member' : 'Owner'
                                    }})</div>
                            </div>
                        </div>
                        <div class="w-100 mb-3">
                            <p class="text-sm-bold fs-16 text-info">Program</p>
                            <div
                                class="shadow-in--sm radius-16 d-flex justify-content-between px-4 py-2 align-items-center point-info">
                                <div class="text-info fs-14 text-break">{{ userProfile?.Program }}</div>
                            </div>
                        </div>
                        <div class="w-100 mb-3">
                            <p class="text-sm-bold fs-16 text-info">Email</p>
                            <div
                                class="shadow-in--sm radius-16 d-flex justify-content-between px-4 py-2 align-items-center point-info">
                                <div class="text-info fs-14 text-break">{{ userProfile?.user?.Email }}</div>
                            </div>
                        </div>
                        <div class="w-100 mb-3">
                            <p class="text-sm-bold fs-16 text-info">LinkedIn</p>
                            <div
                                class="shadow-in--sm radius-16 d-flex justify-content-between px-4 py-2 align-items-center point-info">
                                <div class="text-info fs-14 text-break">{{ userProfile?.userProfile?.Linkedin ?
        userProfile?.userProfile?.Linkedin : 'No data' }}</div>
                            </div>
                        </div>
                        <div class="w-100 mb-3">
                            <p class="text-sm-bold fs-16 text-info">Profile Status</p>
                            <div
                                class="shadow-in--sm radius-16 d-flex justify-content-between px-4 py-2 align-items-center point-info">
                                <div class="fs-14 text-break"
                                    :class="[userProfile?.user?.UserOnBoardingStatus ? 'text-green' : 'text-danger']">{{
        userProfile?.user?.UserOnBoardingStatus ? 'Completed' : 'Incomplete' }}</div>
                            </div>
                        </div>
                        <router-link :to="`/company-profile/${ userProfile?.StartupId }`" v-if="userProfile?.userProfile?.Joinedas == '1000'">
                            <Button type="button" class="blue btn-sm outline rounded-md no-edge-radius-md w-100">
                                View Company Profile
                            </Button>
                        </router-link>
                    </div>
                </div>
            </div>
        </div>

    </div>
</template>

<script setup>
import PageExplanation from "@/components/PageExplanation.vue"
import Badge from "@/components/Badge.vue"
import Image from "@/components/Image.vue"
import Ionicon from "@/components/Ionicon.vue";
import { computed, onMounted, ref, defineAsyncComponent, reactive } from "vue"
import { useRoute, useRouter, onBeforeRouteUpdate } from "vue-router"
import { storeToRefs } from 'pinia';
import { userStore } from "@/stores/userStore"
import { useCompanyGoalsStore } from "@/stores/companyGoalsStore"
import { adminAuthStore } from "@/stores/adminAuthStore"
import Button from "@/components/Button.vue"

const { userProfile, userConnections } = storeToRefs(userStore());
const { fetchUserById, fetchConnectionsByUserId } = userStore();
const { adminAuth } = storeToRefs(adminAuthStore());

const route = useRoute();
const router = useRouter();

const setImageToDefault = (e) => {
    e.target.src = "https://www.w3schools.com/howto/img_avatar.png";
}

onMounted(async () => {
    await fetchUserById(route.params.userid);
    await fetchConnectionsByUserId(route.params.startupid);
});

onBeforeRouteUpdate(async (to, from, next) => {
    await fetchUserById(to.params.userid);
    await fetchConnectionsByUserId(to.params.startupid);
    next();
});


</script>

<style scoped>
.text-limit {
    height: 3.75rem;
    line-height: 1.25rem;
    display: block;
    max-width: 100%;
    overflow: hidden;
    text-overflow: ellipsis;
}
</style>
