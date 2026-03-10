<template>
    <div class="container">
        <div class="row">
            <!-- <div class="col-2">
                <div class="list-group cursor-pointer list-group-flush mt-6">
                    <router-link :to="menu.to" class="list-group-item" exact-active-class="active" v-for="(menu, index) in menuList"
                    :class="[menu?.name == 'Availability' && adminAuth?.role == '1000' || menu?.name == 'Availability' && adminAuth?.role == '1100' ? 'd-none' : '']"
                    >
                        {{ menu.name }}
                    </router-link>
                </div>
            </div> -->
            <div class="col-12">
                <slot/>
            </div>
        </div>
    </div>
</template>

<script setup>
import { adminAuthStore } from "@/stores/adminAuthStore"
import { storeToRefs } from 'pinia';

const { adminAuth } = storeToRefs(adminAuthStore());
const menuList = [
    {
        name: "Calendar",
        to: "/calendar"
    },
]
</script>

<style scoped>
.list-group-item {
    border: none;
}
</style>