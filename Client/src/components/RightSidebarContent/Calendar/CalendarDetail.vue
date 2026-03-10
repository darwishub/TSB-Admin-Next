<template>
    <div>
        <div class="fs-18 text-sm-bold mb-3">{{ data?.name }}</div>
        <div class="d-flex align-items-center mb-3">
            <Ionicon name="calendar" class="me-2 info" font_size="24" />
            <div class="text-sm-bold">{{ formattedDate }}</div>
        </div>
        <div class="d-flex align-items-center mb-3">
            <Ionicon name="time" class="me-2 info" font_size="24" />
            <div class="text-sm-bold">{{ formattedStartTime }} - {{ formattedEndTime }}</div>
        </div>
        <div class="d-flex align-items-center mb-3">
            <a class="fs-16" :href="data?.conferenceData?.entryPoints[0]?.uri" target="_blank">
                <Button class="blue rounded-md" type="button">Join Meeting
                    <Ionicon icon_name="videocam" class="ms-3" font_size="24" />
                </Button>
            </a>
        </div>
        <div class="my-3">
            <p class="fs-16 mb-2 text-sm-bold">Guests:</p>
            <div class="d-flex jsutify-content-between align-items-center mb-2" v-for="(guest, index) in guestData"
                :key="index">
                <Ionicon icon_name="person-circle" class="info" font_size="24" />
                <span class="fs-14 ms-3 text-break">{{ guest.email }}</span>
            </div>

        </div>
    </div>
</template>

<script setup>
import { computed, ref, inject, onUpdated } from "vue";
import Ionicon from "@/components/Ionicon.vue"
import Button from "@/components/Button.vue"
import { calendarStore } from "@/stores/calendarStore";
import { adminAuthStore } from "@/stores/adminAuthStore"
import { storeToRefs } from 'pinia';
import BaseInput from "@/components/Form/BaseInput.vue"
import Swal from 'sweetalert2'
import { userNotificationsStore } from "@/stores/notificationsStore"
import { useSignalR } from '@dreamonkey/vue-signalr';
const props = defineProps({
    data: Object
});
const { adminAuth } = storeToRefs(adminAuthStore());
const { CancelEvent, fetchCalendarList, InviteEmailToJoinEvent } = calendarStore();
const { sendNotificationToEmail } = userNotificationsStore();

const data = computed(() => {
    return props.data;
});
const signalr = useSignalR();
const Toast = Swal.mixin({
    toast: true,
    position: 'top',
    showConfirmButton: false,
    timer: 5000,
    timerProgressBar: true,
    showCloseButton: true,
    didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
});

let { closeRightSidebar } = inject("rightPanelStatus");
let guestData = ref(data.value?.guests);
onUpdated(() => {
    guestData.value = data.value?.guests;
})
let isLoading = ref(false);
let btnClickedId = ref(null);

let formattedDate = computed(() => {
    let date = new Date(data.value?.date);
    let day = date.getDate();
    let months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    let month = months[date.getMonth()];
    let year = date.getFullYear();

    let formattedDate = `${day}, ${month} ${year}`;
    return formattedDate;
});

let formattedStartTime = computed(() => {
    let startTimeObj = new Date(data.value?.startTime);
    return startTimeObj.toLocaleTimeString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true });
});

let formattedEndTime = computed(() => {
    let endTimeObj = new Date(data.value?.endTime);
    return endTimeObj.toLocaleTimeString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true });
});

const onRemoveMeeting = async () => {
    isLoading.value = true;
    btnClickedId.value = 1;
    await CancelEvent(data.value?.id);
    await fetchCalendarList(adminAuth.value?.email);
    sendNotificationToEmail({
        Email: data.value?.extendedProperties.private.bookerEmail,
        IsAction: true,
        Message: '' + adminAuth.value.name + ' has canceled an appointment with you on ' + formattedDate.value + ' at ' + formattedStartTime.value + ' - ' + formattedEndTime.value + '.',
        Link: `/calendar`,
        HasButton: false,
        ButtonLabel: '',
        SuccessMessage: '',
        Redirect: '',
        NotifyToTeam: false,
        Clicked: true
    }).then(() => {
        signalr.invoke('SendNotificationToGroup', data.value?.extendedProperties.private.bookerEmail, "");
        signalr.invoke('SendCalendar', data.value?.extendedProperties.private.bookerEmail, "");
        fetchUsersData();
    })
    isLoading.value = false;
    btnClickedId.value = null;
    closeRightSidebar();
    Toast.fire({
        icon: 'warning',
        title: 'Meeting has been canceled!',
        background: '#DFAE55',
        iconColor: '#fff',
        color: '#fff'
    });
};
const inputGuestRef = ref();
const addGuest = async () => {
    if (inputGuestRef.value?.inputValueRef?.value) {
        isLoading.value = true;
        btnClickedId.value = 0;
        let formData = new FormData();
        let values = {
            id: data.value?.id,
            guestEmail: [
                inputGuestRef.value?.inputValueRef?.value
            ]
        }
        formData.append('data', JSON.stringify(values));
        try {
            const { data, status } = await InviteEmailToJoinEvent(formData);
            if(status == 200){
                guestData.value.push({ email: inputGuestRef.value?.inputValueRef?.value });
            }
            inputGuestRef.value?.resetField();
        } catch (error) {
            console.error("An error occurred:", error);
        } finally {
            btnClickedId.value = null;
            isLoading.value = false;
        }
    }
}

</script>