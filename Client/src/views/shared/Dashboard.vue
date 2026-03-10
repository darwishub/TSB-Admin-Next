<template>
  <div>
    <nav class="sidebar" :class="showMenu ? 'open' : 'close'" v-if="currentRouteName != 'UserOnboarding'">
      <div class="menu-control" @click="openMenu">
        <Ionicon icon_name="chevron-forward-circle" class="toggle-transition disabled"
          :class="showMenu ? 'toggle-up' : 'toggle-down'" font_size="32" />
      </div>
      <ul class="nav-list">
        <li v-for="(menu, index) in menuObj" :key="index">
          <router-link :to="menu.to" :data-bs-toggle="showMenu ? '' : 'tooltip'" data-bs-placement="right"
            :data-bs-title="menu.name" exact-active-class="active" v-slot="{ isActive }">
            <Ionicon :icon_name="isActive ? menu.icon_active : menu.icon" class="menu-icon" font_size="24" />
            <span class="links_name hide">{{ menu.name }}</span>
          </router-link>
          <hr class="hr-tsb o-50 my-c-2" v-if="index == 0" />
        </li>
      </ul>
    </nav>

    <div class="home-section">
      <nav class="navbar topbar topbar-transition bg-white sticky-top py-2 shadow--sm"
        :class="{ animate : topBarAnimation }">
        <div class="d-flex justify-content-between w-100 align-items-center">
          <div>
            <Ionicon icon_name="notifications-outline" class="menu-icon ms-5 opacity-0" font_size="32" />
          </div>
          <div class="container user-select-none">
          <div class="d-flex justify-content-between align-items-center w-100">
            <div class="tsb-point-wrapper">
              <div class="d-flex justify-content-start">
                <!-- <div
                  class="shadow-in--sm radius-16 d-flex justify-content-between px-3 align-items-center point-info me-3">
                  <div class="text-info fs-32 text-md f-game mx-auto d-table">Admin</div>
                </div> -->
                <div
                  class="shadow-in--sm radius-16 d-flex justify-content-between px-3 align-items-center point-info me-3"
                  >
                  <div class="text-warning fs-24 text-bold">{{adminAuth.coins}}</div>
                  <Image src="/coin.svg" height="24" width="24" class="user-coins"></Image>
                </div>
                <BaseSelect
                    class="no-label normalize"
                    name="select_program"
                    type-rule="text"
                    @onSelect="onSelectProgram"
                    :options="programList"
                    :value="currentProgram?.Programid"
                    :placeholder="adminAuth.programId == 0 ? 'ALL' : ''"
                    value-by="Programid"
                    label-by="Name"
                    :empty-model-value="''"
                    v-if="adminAuth.role == 'Configuration Access' && currentRouteName != 'UserOnboarding' && currentRouteName != 'UserProfile'"
                    style="width: 300px;"
                    ref="selectProgramRef"
                  />
                  
              </div>
            </div>
            <div class="mobile-toggler" @click="openMenu">
              <Ionicon icon_name="menu-outline" class="menu-icon" font_size="40" />
            </div>
            <div>
              <ul class="info-list">
                <li v-for="(data, index) in filteredTopMenu" :key="index" :class="{ 'd-none': data.id == 0 || data.id == 2 }"
                    @click="openSidebar(data.id)" v-show="adminAuth.onBoardingStatus">
                    <div class="position-relative">
                      <Ionicon :icon_name="data.icon_name" class="menu-icon" font_size="32" />
                      <!-- <span class="position-absolute badge rounded-pill notification-dot bg-danger"
                        v-if="countInComingNotifications > 0 && data.id">
                        {{ countInComingNotifications }}
                        <span class="visually-hidden">unread messages</span>
                      </span> -->
                    </div>
                  </li>
                <li>
                  <div class="profile-menu">
                    <Dropdown position="end" :showToggle="false" :minWidth="12">
                      <template #menu-content>
                        <Image
                        :src="adminAuth.photo != null ? `${'https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/' + adminAuth.photo}` : 'https://www.w3schools.com/howto/img_avatar.png'"
                          class="shadow--sm rounded-lg user-select-none" height="48" width="48" />
                      </template>
                      <template #menu-list>
                        <li v-if="currentRouteName != 'UserOnboarding'">
                          <div class="" v-for="(menu, index) in profileMenuObj" :key="index">

                            <router-link :to="menu.to" class="d-flex align-items-center mb-3 no-link">
                              <div class="flex-shrink-0">
                              <Ionicon :icon_name="menu.icon" class="va-middle grey-light-1"
                                :font_size="index == 1 ? '20' : '16'" />
                            </div>
                            <div class="flex-grow-1 ms-3">
                              <span class="menu-title">{{ menu.name }}<br />
                                <span class="menu-info">{{ menu.info }}</span>
                              </span>
                            </div>
                            </router-link>
                            
                          </div>
                        </li>
                        <li>
                          <Button class="red form-control rounded-md" @click="logOut()">Sign out
                            <Ionicon icon_name="log-out" class="ms-3" font_size="24" />
                          </Button>
                        </li>
                      </template>
                    </Dropdown>
                  </div>
                </li>
                </ul>
            </div>
          </div>
          
        </div>
        <div>
          <Transition mode="out-in">
            <div v-if="Object.keys(activedSidebarObj).length > 0" @click="closeRightSidebar" class="cursor-pointer">
            <Ionicon :icon_name="activedSidebarObj.icon_name" class="menu-icon white me-5" font_size="32" />
          </div>
          <div v-else>
            <Ionicon icon_name="notifications-outline" class="menu-icon white ms-5 opacity-0" font_size="32" />
          </div>
          </Transition>
        </div>
        </div>
      </nav>

       <div class="right-panel" :class="openRightSidebar ? 'open' : 'close'">
      <div class="d-flex align-items-center justify-content-end"
      :class="[ route.name == 'Home' ? 'mt-7 mb-3' : 'my-3' ]"
      >
        <button class="btn btn-default p-0" @click="closeRightSidebar">
          <Ionicon icon_name="close-outline" class="grey-light-1" font_size="40" />
        </button>
      </div>
      <component :is="rightSidebarContent" v-bind="activedSidebarProps" />
    </div>

<div class="center-section">
  <router-view></router-view>
  <!-- <router-view v-slot="{ Component }">
          <transition name="fade" mode="out-in">
            <component :is="Component" />
          </transition>
        </router-view> -->
</div>

</div>
</div>
</template>

<script setup>
import { inject, provide, onMounted, ref, markRaw, shallowRef, reactive, computed } from "vue";
import { useRouter, useRoute } from "vue-router";
import Ionicon from "@/components/Ionicon.vue";
import Dropdown from "@/components/Dropdown.vue";
import { Tooltip } from "bootstrap/dist/js/bootstrap.esm.min.js";
import Image from "@/components/Image.vue";
import Button from "@/components/Button.vue";
import Notification from "@/components/RightSidebarContent/Notification.vue"
import DefaultBody from "@/components/RightSidebarContent/DefaultBody.vue"
import BaseSelect from "@/components/Form/BaseSelect.vue"

import { adminAuthStore } from "@/stores/adminAuthStore"
import { storeToRefs } from 'pinia';

const { adminAuth, programList, currentProgram } = storeToRefs(adminAuthStore());
const { fetchProgram } = adminAuthStore();

const axios = inject("axios");
const router = useRouter();
const route = useRoute();
// const cookies = inject("$cookies");
const showMenu = ref(false);
const openRightSidebar = ref(false);
let rightSidebarContent = shallowRef(Notification);
const topBarAnimation = ref(false);
let activedSidebarObj = reactive({});
let activedSidebarProps = reactive({});
const currentRouteName = computed(() => router.currentRoute.value.name)

let openSidebar = (id) => {
  changeActivedSidebarMenu(id);
  passRightSidebarContent(sidebarObj.value[id].component_name);
}
const closeRightSidebar = () => {
  openRightSidebar.value = !openRightSidebar.value;
  topBarAnimation.value = false;
  Object.keys(activedSidebarObj).forEach(key => delete activedSidebarObj[key]);
}
let passRightSidebarContent = (comp) => {
  rightSidebarContent.value = markRaw(comp);
}
let changeActivedSidebarMenu = (idx) => {
  openRightSidebar.value = openRightSidebar.value == true ? openRightSidebar.value : !openRightSidebar.value;
  topBarAnimation.value = true;
  Object.assign(activedSidebarObj, sidebarObj.value[idx]);
}
let passRightSidebarContentProps = (obj) => {
  Object.assign(activedSidebarProps, obj);
}
provide('rightPanelStatus', {
  passRightSidebarContent,
  changeActivedSidebarMenu,
  passRightSidebarContentProps,
  closeRightSidebar,
  openRightSidebar
});

const menuObj = [
  {
    name: "Dashboard",
    icon: "grid-outline",
    icon_active: "grid",
    to: "/home",
  },
  {
    name: "Quests",
    icon: "ribbon-outline",
    icon_active: "ribbon",
    to: "/quests",
  },
  {
    name: "Shop",
    icon: "cart-outline",
    icon_active: "cart",
    to: "/shop",
  },
  {
    name: "Academy",
    icon: "school-outline",
    icon_active: "school",
    to: "/academy",
  },
  {
    name: "Calendar",
    icon: "calendar-outline",
    icon_active: "calendar",
    to: "/calendar",
  },
  // {
  //   name: "Game",
  //   icon: "stats-chart-outline",
  //   icon_active: "stats-chart",
  //   to: "/game",
  // },
  // {
  //   name: "Events",
  //   icon: "calendar-outline",
  //   icon_active: "calendar",
  //   to: "/events",
  // },
  // {
  //   name: "Community",
  //   icon: "people-outline",
  //   icon_active: "people",
  //   to: "/community",
  // },
  // {
  //   name: "Help",
  //   icon: "help-circle-outline",
  //   icon_active: "help-circle",
  //   to: "/help",
  // }
];

const profileMenuObj = [
  {
    name: "My Profile",
    info: "View your profile",
    icon: "person-outline",
    to: "/settings"
  },
  // {
  //   name: "Team",
  //   info: "Manage your team",
  //   icon: "people-outline",
  // },
  // {
  //   name: "Settings",
  //   info: "Account settings",
  //   icon: "settings-outline",
  // },
];

let sidebarObj = shallowRef([
  {
    'id' : 0,
    'icon_name' : 'information-circle-outline',
    'component_name' : DefaultBody
  },
  {
    'id' : 1,
    'icon_name' : 'notifications-outline',
    'component_name' : Notification
  },
]);

const filteredTopMenu = computed(() => {
  let result = sidebarObj.value.findIndex(i => i.icon_name === activedSidebarObj.icon_name);
  return sidebarObj.value.filter(item => sidebarObj.value.indexOf(item) != result);
});

const logOut = async () => {
  await axios.post("api/Auth/LogOut", {}, { withCredentials: true });
  axios.defaults.headers.common["authorization"] = "";
  // cookies.remove("tsb-auth-admin");

  adminAuth.value.email = "";
  adminAuth.value.programId = null;
  // adminAuth.value.role = "";

  localStorage.removeItem('adminAuth');

  await router.push("/signin");
};

let openMenu = () => {
  showMenu.value = !showMenu.value;
};
let currentProgramId = ref(0);
const onSelectProgram = (newProgramId) => {
  adminAuth.value.programId = newProgramId;
  currentProgramId.value = newProgramId;
}
const selectProgramRef = ref();
provide('programSelection', {
  currentProgramId,
  selectProgramRef
});

onMounted(async () => {
  await fetchProgram();
  new Tooltip(document.body, {
    selector: "[data-bs-toggle='tooltip']",
    trigger: "hover",
  });
});

</script>

<style scoped src="/src/assets/css/sidebar.css">

</style>
<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
.v-enter-active,
.v-leave-active {
  transition: opacity 0.2s ease;
}

.v-enter-from,
.v-leave-to {
  opacity: 0;
}
.navbar ul.info-list {
  padding: 0px;
  list-style: none;
  display: inline-flex;
  margin-bottom: 0;
}

.navbar ul.info-list .profile-menu {
  cursor: pointer;
}

.navbar ul.info-list>li {
  margin-left: 3rem;
  line-height: 40px;
  cursor: pointer;
}

.navbar ul.info-list>li .menu-icon {
  color: #fff;
  cursor: pointer;
  transition: all 0.5s ease;
}

.topbar-transition {
  transition: all 0.5s ease;
}

.topbar {
  background: linear-gradient(269.34deg, rgba(65, 96, 148, 1) -25%, rgba(65, 96, 148, 0) 100%);
  background-size: 60% auto;
  background-position: right;
  background-repeat: no-repeat;
}

.topbar.animate {
  background-size: 10% auto;
}

.topbar.animate .info-list li .menu-icon {
  color: #c2c2c2;
}

.profile-menu .menu-title {
  font-size: 14px;
  color: #696969;
  line-height: initial;
  display: block;
}

.profile-menu .menu-info {
  font-size: 12px;
  color: #c2c2c2;
}

.tsb-point-wrapper .point-info {
  width: 100vw;
  max-width: 176px;
}
</style>