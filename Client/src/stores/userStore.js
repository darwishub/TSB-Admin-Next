import { defineStore } from 'pinia'
import axios from 'axios'
import { ref, computed } from 'vue'

export const userStore = defineStore('userStore', () => {
    const userProfile = ref();
    const userConnections = ref();
    // const DeleteItem = async (id) => {
    //     const { status, data } = await axios.post(`api/academy/DeleteItem?id=${id}`);
    //     if (status == 200) {
    //         academyItems.value = data;
    //     }
    // }

    const fetchUserById = async (id) => {
        try {
            const { data, status } = await axios.get(`/api/User/GetUserById?id=${id}`);
            if (status == 200) {
                userProfile.value = data;
            }
        } catch (error) {
            console.error(error);
        }
    }

    const fetchConnectionsByUserId = async (startupId) => {
        try {
            const { data, status } = await axios.get(`/api/User/GetRecommendationStartups?startupId=${startupId}`);
            if (status == 200) {
                userConnections.value = data;
            }
        } catch (error) {
            console.error(error);
        }
    }


    return { fetchUserById, userProfile, userConnections, fetchConnectionsByUserId };
});