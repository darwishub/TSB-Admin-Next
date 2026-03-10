import { defineStore } from 'pinia'
import axios from 'axios'
import { ref, computed, reactive } from 'vue'

export const userNotificationsStore = defineStore('useNotifications', () => {

    const notificationList = ref([]);
    const notificationPersonalList = ref([]);
    const notificationTeamList = ref([]);

    const sendNotification = async (params) => {
        return await axios.post(`/api/Notifications/SendNotifications`, params);
    };

    const updateClicked = async (id) => {
        return await axios.post(`/api/Notifications/UpdateClicked?id_notification=${id}`);
    };

    const sendNotificationToEmail = async (params) => {
        return await axios.post(`/api/Notifications/SendNotificationsToEmail`, params);
    }

    const updateReadStatus = async (userid, startupid) => {
        return await axios.post(`/api/Notifications/UpdateNotifications?userid=${userid}&startupid=${startupid}`).then(() => {
            fetchPersonalNotifications(userid, startupid);
            fetchTeamNotifications(userid, startupid);
        });
    }

    const fetchNotifications = async (userid, startupid) => {
        const params = {
            userid: userid,
            startupid: startupid
        }
        const { data, status } = await axios.get(`/api/Notifications/GetNotifications`, { params });
        if (status == 200) {
            notificationList.value = data;
        };
        return data;
    };

    const fetchPersonalNotifications = async (userid, startupid) => {
        const params = {
            userid: userid,
            startupid: startupid
        }
        const { data, status } = await axios.get(`/api/Notifications/GetPersonalNotifications`, { params });
        if (status == 200) {
            notificationPersonalList.value = data;
        };
        return data;
    };

    const fetchTeamNotifications = async (userid, startupid) => {
        const params = {
            userid: userid,
            startupid: startupid
        }
        const { data, status } = await axios.get(`/api/Notifications/GetTeamNotifications`, { params });
        if (status == 200) {
            notificationTeamList.value = data;
        };
        return data;
    };

    const clearNotifications = async (startupid, userid) => {
        const { data, status } = await axios.post(`/api/Notifications/ClearNotifications?startupid=${startupid}&userid=${userid}`);
        if (status == 200) {
            notificationList.value = [];
        }
    }

    return {
        sendNotification, notificationList, fetchNotifications, updateReadStatus, clearNotifications, sendNotificationToEmail, fetchPersonalNotifications, fetchTeamNotifications,
        notificationPersonalList, notificationTeamList, updateClicked
    }

}, {
    persist: {
        storage: sessionStorage,
    }
});