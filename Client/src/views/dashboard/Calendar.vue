<template>
    <PageExplanation class="relative goals-topbar no-toggle" style="z-index: 99;" :breadcrumbs="`${adminAuth?.name} ${route?.meta?.breadcrumb}`" :logo="adminAuth?.photo">
    </PageExplanation>
    <ScheduleWrapper>
        <pro-calendar :events="calendarEvent" :loading="false" :config="cfg" view="month" :date="dateNow"
            @calendarClosed="void 0" @fetchEvents="void 0" class="mb-6" ref="calendarRef"
            >
            <template #closeButton>
            </template>
            <template #sideEvent="{ dateSelected, calendarEvents }">
             
            </template>
        </pro-calendar>
    </ScheduleWrapper>
</template>

<script setup>
import "vue-pro-calendar/style";
import { ref, onMounted, inject, computed } from "vue";
import { E_CustomEvents } from "vue-pro-calendar";
import CalendarDetail from "@/components/RightSidebarContent/Calendar/CalendarDetail.vue";
import { calendarStore } from "@/stores/calendarStore";
import { storeToRefs } from 'pinia';
import ScheduleWrapper from "@/components/Calendar/ScheduleWrapper.vue";
import { adminAuthStore } from "@/stores/adminAuthStore"
import { useSignalR } from '@dreamonkey/vue-signalr';
import PageExplanation from "@/components/PageExplanation.vue"
import { useRoute } from "vue-router";
import { watchDebounced } from "@vueuse/core";

const { adminAuth } = storeToRefs(adminAuthStore());
const { fetchCalendarList, fetchCalendarLogs } = calendarStore();
const { calendarList, calendarLogs } = storeToRefs(calendarStore());
let { passRightSidebarContent, changeActivedSidebarMenu, passRightSidebarContentProps, openRightSidebar, activedSidebarObj, closeRightSidebar } = inject("rightPanelStatus");
const dateNow = ref();
const route = useRoute();
const cfg = {
    viewEvent: {
        icon: true,
        text: "",
    },
    reportEvent: undefined,
    searchPlaceholder: "",
    eventName: "",
    closeText: "",
    nativeDatepicker: false,
    todayButton: true,
    firstDayOfWeek: 1,
};

const calendarEvent = computed(() => {
    return calendarList.value?.Calendars?.items.map((item) => {
        return {
            id: item.id,
            name: item.summary,
            date: item.start.dateTime,
            startTime: item.start.dateTime,
            endTime: item.end.dateTime,
            keywords: "",
            comment: "",
            guests: item.attendees,
            conferenceData: item.conferenceData,
            extendedProperties: item.extendedProperties,
        }
    });
});

const getCalendarEventById = (id) => {
    return calendarEvent.value.find((item) => item.id === id);
}

const viewDetail = (id) => {
    passRightSidebarContent(CalendarDetail);
    changeActivedSidebarMenu(2);
    passRightSidebarContentProps({
        data: getCalendarEventById(id)
    });
}

const calendarRef = ref();

watchDebounced(
  () => adminAuth.value.programId, 
  async (newValue, oldValue) => {
    await fetchCalendarList(newValue);
    if(openRightSidebar.value){
        closeRightSidebar();
    }
  },
  0
);

onMounted(async () => {
    dateNow.value = new Date();
    [E_CustomEvents.VIEW, E_CustomEvents.REPORT].forEach(e => {
        document.body.addEventListener(e, (event) => {
            viewDetail(event.detail.id);
        });
    });
    await fetchCalendarList(adminAuth.value.programId);
    await fetchCalendarLogs();
});


</script>

<style scoped>

</style>